using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using NCalc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MinFinderWinForms
{
    public partial class Lab6_IntegrationForm : Form
    {
        private enum MethodKind { RectMid, Trapezoid, Simpson }

        private sealed class Step
        {
            public int N { get; init; }
            public double Value { get; init; }
            public double ErrorEst { get; init; } // оценка |I(2N)-I(N)|/(2^p-1)
        }

        // история по методам
        private readonly Dictionary<MethodKind, List<Step>> _history = new();
        private int _stepIndex = 0;

        public Lab6_IntegrationForm()
        {
            InitializeComponent();
            InitChart();

            miCalc.Click += (_, __) => CalculateAll();
            miStep.Click += (_, __) => StepForward();
            miClear.Click += (_, __) => ClearAll();
            miGen.Click += (_, __) => GenerateExample();
            miExit.Click += (_, __) => Close();

            cmbHistory.Items.Clear();
            cmbHistory.Items.Add("Прямоугольники (серединные)");
            cmbHistory.Items.Add("Трапеции");
            cmbHistory.Items.Add("Симпсон");
            cmbHistory.SelectedIndex = 0;

            cmbHistory.SelectedIndexChanged += (_, __) =>
            {
                _stepIndex = 0;
                RenderHistoryStep();
            };

            // пример по умолчанию
            GenerateExample();
        }

        // ---------- UI ----------
        private void InitChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            var area = new System.Windows.Forms.DataVisualization.Charting.ChartArea("Main");
            area.AxisX.Title = "x";
            area.AxisY.Title = "f(x)";
            area.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            area.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chart1.ChartAreas.Add(area);

            chart1.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("Legend"));
        }

        private void ClearAll()
        {
            _history.Clear();
            _stepIndex = 0;

            lblResult.Text = "—";
            lblInfo.Text = "Готово.";

            chart1.Series.Clear();
            InitChart();
        }

        private void GenerateExample()
        {
            txtFx.Text = "sin(x) + x*x/10";
            txtA.Text = "0";
            txtB.Text = "6.28318"; // ~2*pi
            txtE.Text = "0.0001";

            cbRect.Checked = true;
            cbTrap.Checked = true;
            cbSimp.Checked = true;

            lblInfo.Text = "Пример заполнен. Нажмите «Рассчитать».";
        }

        // ---------- Parsing & Eval ----------
        private bool TryReadInputs(out double a, out double b, out double eps, out string fx)
        {
            a = b = eps = 0;
            fx = (txtFx.Text ?? "").Trim();

            if (string.IsNullOrWhiteSpace(fx))
            {
                Warn("Введите f(x).");
                return false;
            }

            if (!TryParseDouble(txtA.Text, out a))
            {
                Warn("Некорректное a.");
                return false;
            }

            if (!TryParseDouble(txtB.Text, out b))
            {
                Warn("Некорректное b.");
                return false;
            }

            if (!TryParseDouble(txtE.Text, out eps) || eps <= 0)
            {
                Warn("Некорректная точность e (должна быть > 0).");
                return false;
            }

            if (Math.Abs(b - a) < 1e-15)
            {
                Warn("Границы a и b не должны совпадать.");
                return false;
            }

            // проверка вычислимости
            double testX = (a + b) / 2.0;
            if (!TryEval(fx, testX, out var y, out var err))
            {
                Warn("Ошибка в формуле f(x): " + err);
                return false;
            }
            if (double.IsNaN(y) || double.IsInfinity(y))
            {
                Warn("f(x) даёт NaN/Infinity на середине интервала.");
                return false;
            }

            return true;
        }

        private static bool TryParseDouble(string? s, out double v)
        {
            v = 0;
            if (s == null) return false;

            // разрешаем ввод с запятой
            s = s.Trim().Replace(',', '.');

            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v);
        }

        private static bool TryEval(string fx, double x, out double y, out string error)
        {
            y = 0;
            error = "";
            try
            {
                var expr = new Expression(fx, EvaluateOptions.IgnoreCase);
                expr.Parameters["x"] = x;

                var res = expr.Evaluate();
                if (res == null)
                {
                    error = "Результат пустой.";
                    return false;
                }

                y = Convert.ToDouble(res, CultureInfo.InvariantCulture);

                if (double.IsNaN(y) || double.IsInfinity(y))
                {
                    error = "NaN/Infinity (возможно деление на 0 или неверная область определения).";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private void Warn(string msg)
        {
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ---------- Main calc ----------
        private void CalculateAll()
        {
            _history.Clear();
            _stepIndex = 0;

            if (!TryReadInputs(out double aIn, out double bIn, out double eps, out string fx))
                return;

            // нормализуем так, чтобы a < b
            double a = Math.Min(aIn, bIn);
            double b = Math.Max(aIn, bIn);
            int sign = (aIn <= bIn) ? 1 : -1;

            if (!cbRect.Checked && !cbTrap.Checked && !cbSimp.Checked)
            {
                Warn("Выберите хотя бы один метод (CheckBox).");
                return;
            }

            // график функции
            PlotFunction(fx, a, b);

            // считаем выбранные методы
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Результаты (подобрано оптимальное N):");

            if (cbRect.Checked)
            {
                var hist = BuildHistory(fx, a, b, eps, MethodKind.RectMid);
                _history[MethodKind.RectMid] = hist;
                var last = hist.Last();
                sb.AppendLine($"• Прямоугольники (серединные): I ≈ {sign * last.Value:G17}, N = {last.N}, err≈ {last.ErrorEst:G6}");
            }

            if (cbTrap.Checked)
            {
                var hist = BuildHistory(fx, a, b, eps, MethodKind.Trapezoid);
                _history[MethodKind.Trapezoid] = hist;
                var last = hist.Last();
                sb.AppendLine($"• Трапеции: I ≈ {sign * last.Value:G17}, N = {last.N}, err≈ {last.ErrorEst:G6}");
            }

            if (cbSimp.Checked)
            {
                var hist = BuildHistory(fx, a, b, eps, MethodKind.Simpson);
                _history[MethodKind.Simpson] = hist;
                var last = hist.Last();
                sb.AppendLine($"• Симпсон: I ≈ {sign * last.Value:G17}, N = {last.N}, err≈ {last.ErrorEst:G6}");
            }

            lblResult.Text = sb.ToString();

            _stepIndex = 0;
            RenderHistoryStep();

            lblInfo.Text = "Готово. Нажимайте «Шаг» для просмотра истории разбиений.";
        }

        // ---------- History build ----------
        private List<Step> BuildHistory(string fx, double a, double b, double eps, MethodKind method)
        {
            // стартовое N
            int n = 4;

            // Simpson требует чётное
            if (method == MethodKind.Simpson && (n % 2 == 1)) n++;

            int p = (method == MethodKind.Simpson) ? 4 : 2;
            double denom = Math.Pow(2, p) - 1; // 3 или 15

            var list = new List<Step>();

            double I1 = ComputeIntegral(fx, a, b, n, method);
            list.Add(new Step { N = n, Value = I1, ErrorEst = double.PositiveInfinity });

            // подбор оптимального N удвоениями
            for (int iter = 0; iter < 25; iter++)
            {
                int n2 = n * 2;
                if (method == MethodKind.Simpson && (n2 % 2 == 1)) n2++;

                double I2 = ComputeIntegral(fx, a, b, n2, method);

                double err = Math.Abs(I2 - I1) / denom;

                list.Add(new Step { N = n2, Value = I2, ErrorEst = err });

                if (err <= eps)
                    break;

                n = n2;
                I1 = I2;
            }

            return list;
        }

        private double ComputeIntegral(string fx, double a, double b, int n, MethodKind method)
        {
            if (n < 1) n = 1;

            double h = (b - a) / n;

            switch (method)
            {
                case MethodKind.RectMid:
                    {
                        double sum = 0;
                        for (int i = 0; i < n; i++)
                        {
                            double xmid = a + (i + 0.5) * h;
                            if (!TryEval(fx, xmid, out double y, out _))
                                y = 0;
                            sum += y;
                        }
                        return sum * h;
                    }

                case MethodKind.Trapezoid:
                    {
                        double sum = 0;

                        if (!TryEval(fx, a, out double ya, out _)) ya = 0;
                        if (!TryEval(fx, b, out double yb, out _)) yb = 0;

                        sum = (ya + yb) / 2.0;

                        for (int i = 1; i < n; i++)
                        {
                            double x = a + i * h;
                            if (!TryEval(fx, x, out double y, out _))
                                y = 0;
                            sum += y;
                        }

                        return sum * h;
                    }

                case MethodKind.Simpson:
                    {
                        // n должно быть чётным
                        if (n % 2 == 1) n++;

                        h = (b - a) / n;

                        if (!TryEval(fx, a, out double fa, out _)) fa = 0;
                        if (!TryEval(fx, b, out double fb, out _)) fb = 0;

                        double sumOdd = 0;
                        double sumEven = 0;

                        for (int i = 1; i < n; i++)
                        {
                            double x = a + i * h;
                            if (!TryEval(fx, x, out double fxv, out _))
                                fxv = 0;

                            if (i % 2 == 1) sumOdd += fxv;
                            else sumEven += fxv;
                        }

                        return (h / 3.0) * (fa + fb + 4.0 * sumOdd + 2.0 * sumEven);
                    }

                default:
                    return 0;
            }
        }

        // ---------- Plot ----------
        private void PlotFunction(string fx, double a, double b)
        {
            chart1.Series.Clear();

            var s = new System.Windows.Forms.DataVisualization.Charting.Series("f(x)");
            s.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            s.BorderWidth = 2;
            s.ChartArea = "Main";
            s.Legend = "Legend";

            int points = 400;
            for (int i = 0; i <= points; i++)
            {
                double x = a + (b - a) * i / points;
                if (TryEval(fx, x, out double y, out _))
                    s.Points.AddXY(x, y);
            }

            chart1.Series.Add(s);
        }

        private MethodKind GetHistoryMethod()
        {
            return cmbHistory.SelectedIndex switch
            {
                0 => MethodKind.RectMid,
                1 => MethodKind.Trapezoid,
                2 => MethodKind.Simpson,
                _ => MethodKind.RectMid
            };
        }

        private void StepForward()
        {
            if (_history.Count == 0)
            {
                Warn("Сначала нажмите «Рассчитать».");
                return;
            }

            var m = GetHistoryMethod();
            if (!_history.TryGetValue(m, out var hist) || hist.Count == 0)
            {
                Warn("Для выбранного метода история не построена (включите его галочкой и рассчитайте).");
                return;
            }

            _stepIndex++;
            if (_stepIndex >= hist.Count) _stepIndex = hist.Count - 1;

            RenderHistoryStep();
        }

        private void RenderHistoryStep()
        {
            if (_history.Count == 0) return;

            var m = GetHistoryMethod();
            if (!_history.TryGetValue(m, out var hist) || hist.Count == 0) return;

            int idx = Math.Max(0, Math.Min(_stepIndex, hist.Count - 1));
            var step = hist[idx];

            // удаляем старые служебные серии
            RemoveSeriesIfExists("Разбиения");
            RemoveSeriesIfExists("Опорные точки");

            // рисуем разбиения (вертикальные линии) и опорные точки метода
            if (!TryReadInputs(out double aIn, out double bIn, out _, out string fx))
                return;

            double a = Math.Min(aIn, bIn);
            double b = Math.Max(aIn, bIn);

            DrawPartitionsAndPoints(fx, a, b, step.N, m);

            lblInfo.Text = $"История: {GetMethodName(m)} | шаг {idx + 1}/{hist.Count} | N={step.N} | I≈{step.Value:G10} | err≈{step.ErrorEst:G6}";
        }

        private string GetMethodName(MethodKind m) => m switch
        {
            MethodKind.RectMid => "Прямоугольники (серединные)",
            MethodKind.Trapezoid => "Трапеции",
            MethodKind.Simpson => "Симпсон",
            _ => "Метод"
        };

        private void RemoveSeriesIfExists(string name)
        {
            if (chart1.Series.IndexOf(name) >= 0)
                chart1.Series.Remove(chart1.Series[name]);
        }

        private void DrawPartitionsAndPoints(string fx, double a, double b, int n, MethodKind m)
        {
            double h = (b - a) / n;

            // серия вертикальных линий разбиений
            var sPart = new System.Windows.Forms.DataVisualization.Charting.Series("Разбиения");
            sPart.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sPart.BorderWidth = 1;
            sPart.ChartArea = "Main";
            sPart.Legend = "Legend";
            sPart.IsVisibleInLegend = true;

            // диапазон Y по уже построенной функции
            var ys = chart1.Series["f(x)"].Points.Select(p => p.YValues[0]).ToList();
            if (ys.Count == 0) { ys.Add(0); ys.Add(1); }
            double yMin = ys.Min();
            double yMax = ys.Max();
            double pad = (yMax - yMin) * 0.08;
            if (pad <= 0) pad = 1;
            yMin -= pad;
            yMax += pad;

            for (int i = 0; i <= n; i++)
            {
                double x = a + i * h;

                // вертикальная линия = 2 точки
                sPart.Points.AddXY(x, yMin);
                sPart.Points.AddXY(x, yMax);

                // разрыв линии БЕЗ NaN: пустая точка
                var empty = new System.Windows.Forms.DataVisualization.Charting.DataPoint();
                empty.IsEmpty = true;
                sPart.Points.Add(empty);
            }

            chart1.Series.Add(sPart);

            // серия опорных точек
            var sPts = new System.Windows.Forms.DataVisualization.Charting.Series("Опорные точки");
            sPts.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            sPts.MarkerSize = 7;
            sPts.ChartArea = "Main";
            sPts.Legend = "Legend";

            if (m == MethodKind.RectMid)
            {
                for (int i = 0; i < n; i++)
                {
                    double x = a + (i + 0.5) * h;
                    if (TryEval(fx, x, out double y, out _))
                        sPts.Points.AddXY(x, y);
                }
            }
            else
            {
                for (int i = 0; i <= n; i++)
                {
                    double x = a + i * h;
                    if (TryEval(fx, x, out double y, out _))
                        sPts.Points.AddXY(x, y);
                }
            }

            chart1.Series.Add(sPts);
        }

    }
}
