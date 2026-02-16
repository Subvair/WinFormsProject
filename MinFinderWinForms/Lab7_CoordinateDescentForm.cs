using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCalc;

namespace MinFinderWinForms
{
    public partial class Lab7_CoordinateDescentForm : Form
    {
        private sealed record StepState(int K, double X, double Y, double F, string Note);

        private readonly List<StepState> _steps = new();
        private int _stepIndex = 0;

        public Lab7_CoordinateDescentForm()
        {
            InitializeComponent();
            InitChart();

            miCalc.Click += (_, __) => Calculate();
            miStep.Click += (_, __) => StepForward();
            miGen.Click += (_, __) => GenerateExample();
            miClear.Click += (_, __) => ClearAll();
            miExit.Click += (_, __) => Close();

            GenerateExample();
        }

        // ---------------- UI ----------------
        private void InitChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Legends.Clear();

            var area = new System.Windows.Forms.DataVisualization.Charting.ChartArea("Main");
            area.AxisX.Title = "x";
            area.AxisY.Title = "y";
            area.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            area.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chart1.ChartAreas.Add(area);

            chart1.Legends.Add(new System.Windows.Forms.DataVisualization.Charting.Legend("Legend"));

            var sPath = new System.Windows.Forms.DataVisualization.Charting.Series("Траектория");
            sPath.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sPath.BorderWidth = 2;
            sPath.ChartArea = "Main";
            sPath.Legend = "Legend";
            chart1.Series.Add(sPath);

            var sPoint = new System.Windows.Forms.DataVisualization.Charting.Series("Текущий шаг");
            sPoint.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            sPoint.MarkerSize = 10;
            sPoint.ChartArea = "Main";
            sPoint.Legend = "Legend";
            chart1.Series.Add(sPoint);
        }

        private void SetChartBounds(double xmin, double xmax, double ymin, double ymax)
        {
            var area = chart1.ChartAreas["Main"];
            area.AxisX.Minimum = xmin;
            area.AxisX.Maximum = xmax;
            area.AxisY.Minimum = ymin;
            area.AxisY.Maximum = ymax;
        }

        private void GenerateExample()
        {
            txtF.Text = "(x-1)*(x-1) + (y+2)*(y+2)"; // минимум (1,-2)
            txtX0.Text = "5";
            txtY0.Text = "5";
            txtE.Text = "0.000001";

            txtXmin.Text = "-2";
            txtXmax.Text = "6";
            txtYmin.Text = "-6";
            txtYmax.Text = "6";

            lblInfo.Text = "Пример заполнен. Нажмите «Рассчитать».";
        }

        private void ClearAll()
        {
            _steps.Clear();
            _stepIndex = 0;
            lstSteps.Items.Clear();
            lblResult.Text = "—";
            lblInfo.Text = "Готово.";

            InitChart();
        }

        private void Warn(string msg)
        {
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // ---------------- Parsing ----------------
        private static bool TryParseDouble(string? s, out double v)
        {
            v = 0;
            if (s == null) return false;
            s = s.Trim().Replace(',', '.');
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v);
        }

        private bool TryReadInputs(out string f, out double x0, out double y0, out double eps,
                                   out double xmin, out double xmax, out double ymin, out double ymax)
        {
            f = (txtF.Text ?? "").Trim();
            x0 = y0 = eps = xmin = xmax = ymin = ymax = 0;

            if (string.IsNullOrWhiteSpace(f)) { Warn("Введите f(x,y)."); return false; }
            if (!TryParseDouble(txtX0.Text, out x0)) { Warn("Некорректное x0."); return false; }
            if (!TryParseDouble(txtY0.Text, out y0)) { Warn("Некорректное y0."); return false; }
            if (!TryParseDouble(txtE.Text, out eps) || eps <= 0) { Warn("e должно быть > 0."); return false; }

            if (!TryParseDouble(txtXmin.Text, out xmin)) { Warn("Некорректное xmin."); return false; }
            if (!TryParseDouble(txtXmax.Text, out xmax)) { Warn("Некорректное xmax."); return false; }
            if (!TryParseDouble(txtYmin.Text, out ymin)) { Warn("Некорректное ymin."); return false; }
            if (!TryParseDouble(txtYmax.Text, out ymax)) { Warn("Некорректное ymax."); return false; }

            if (xmin >= xmax || ymin >= ymax)
            {
                Warn("Проверьте границы: xmin < xmax и ymin < ymax.");
                return false;
            }

            if (!TryEval(f, x0, y0, out _, out var err))
            {
                Warn("Ошибка формулы: " + err);
                return false;
            }

            return true;
        }

        // ---------------- Eval ----------------
        private static bool TryEval(string f, double x, double y, out double val, out string err)
        {
            val = 0;
            err = "";
            try
            {
                var expr = new Expression(f, EvaluateOptions.IgnoreCase);
                expr.Parameters["x"] = x;
                expr.Parameters["y"] = y;

                object? res = expr.Evaluate();
                if (res == null)
                {
                    err = "Пустой результат.";
                    return false;
                }

                val = Convert.ToDouble(res, CultureInfo.InvariantCulture);

                if (double.IsNaN(val) || double.IsInfinity(val))
                {
                    err = "NaN/Infinity (возможно деление на 0/неверная область определения).";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        // ---------------- Golden section (1D) ----------------
        private static double GoldenSectionMin(Func<double, double> f, double a, double b, double eps, int maxIter = 2000)
        {
            // a < b
            const double phi = 1.6180339887498948482; // (1+sqrt(5))/2
            double x1 = b - (b - a) / phi;
            double x2 = a + (b - a) / phi;

            double f1 = f(x1);
            double f2 = f(x2);

            int it = 0;
            while ((b - a) > eps && it < maxIter)
            {
                it++;

                if (f1 > f2)
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;

                    x2 = a + (b - a) / phi;
                    f2 = f(x2);
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;

                    x1 = b - (b - a) / phi;
                    f1 = f(x1);
                }
            }

            return (a + b) / 2.0;
        }

        private double MinimizeX_Golden(string f, double yFixed, double xmin, double xmax, double eps)
        {
            double A = Math.Min(xmin, xmax);
            double B = Math.Max(xmin, xmax);

            double F1D(double x)
            {
                if (!TryEval(f, x, yFixed, out var v, out _)) return double.PositiveInfinity;
                return v;
            }

            return GoldenSectionMin(F1D, A, B, eps);
        }

        private double MinimizeY_Golden(string f, double xFixed, double ymin, double ymax, double eps)
        {
            double A = Math.Min(ymin, ymax);
            double B = Math.Max(ymin, ymax);

            double F1D(double y)
            {
                if (!TryEval(f, xFixed, y, out var v, out _)) return double.PositiveInfinity;
                return v;
            }

            return GoldenSectionMin(F1D, A, B, eps);
        }

        // ---------------- Main algorithm ----------------
        private void Calculate()
        {
            ClearAll();

            if (!TryReadInputs(out string f, out double x, out double y, out double eps,
                               out double xmin, out double xmax, out double ymin, out double ymax))
                return;

            SetChartBounds(xmin, xmax, ymin, ymax);

            if (!TryEval(f, x, y, out double fxy, out var err0))
            {
                Warn(err0);
                return;
            }

            // старт
            _steps.Add(new StepState(0, x, y, fxy, "Старт"));

            int maxOuterIter = 200; // обычно хватит 5-30
            for (int k = 1; k <= maxOuterIter; k++)
            {
                double oldX = x, oldY = y, oldF = fxy;

                // 1) минимизация по x при фиксированном y
                double xNew = MinimizeX_Golden(f, y, xmin, xmax, eps);
                if (!TryEval(f, xNew, y, out double fAfterX, out _))
                    fAfterX = fxy;

                x = xNew; fxy = fAfterX;
                _steps.Add(new StepState(_steps.Count, x, y, fxy, "min по x"));

                // 2) минимизация по y при фиксированном x
                double yNew = MinimizeY_Golden(f, x, ymin, ymax, eps);
                if (!TryEval(f, x, yNew, out double fAfterY, out _))
                    fAfterY = fxy;

                y = yNew; fxy = fAfterY;
                _steps.Add(new StepState(_steps.Count, x, y, fxy, "min по y"));

                double move = Math.Sqrt((x - oldX) * (x - oldX) + (y - oldY) * (y - oldY));
                double df = Math.Abs(fxy - oldF);

                // остановка
                if (move <= eps && df <= eps)
                    break;
            }

            // вывод истории
            foreach (var s in _steps)
            {
                lstSteps.Items.Add($"k={s.K,-4} x={s.X,12:G10}  y={s.Y,12:G10}  f={s.F,14:G12}   [{s.Note}]");
            }

            // траектория
            PlotPath();

            var last = _steps.Last();
            lblResult.Text =
                $"Минимум найден приближённо:\r\n" +
                $"x* ≈ {last.X:G17}\r\n" +
                $"y* ≈ {last.Y:G17}\r\n" +
                $"f(x*,y*) ≈ {last.F:G17}\r\n" +
                $"Шагов (точек): {_steps.Count - 1}";

            _stepIndex = 0;
            RenderStep();

            lblInfo.Text = "Готово. Нажимайте «Шаг» для показа текущей точки на траектории.";
        }

        private void PlotPath()
        {
            if (chart1.Series.IndexOf("Траектория") < 0) return;
            var s = chart1.Series["Траектория"];
            s.Points.Clear();

            foreach (var st in _steps)
                s.Points.AddXY(st.X, st.Y);
        }

        private void RenderStep()
        {
            if (_steps.Count == 0) return;

            int idx = Math.Max(0, Math.Min(_stepIndex, _steps.Count - 1));
            var st = _steps[idx];

            if (chart1.Series.IndexOf("Текущий шаг") >= 0)
            {
                var sp = chart1.Series["Текущий шаг"];
                sp.Points.Clear();
                sp.Points.AddXY(st.X, st.Y);
            }

            lblInfo.Text = $"Шаг {idx}/{_steps.Count - 1}: x={st.X:G10}, y={st.Y:G10}, f={st.F:G12} [{st.Note}]";
        }

        private void StepForward()
        {
            if (_steps.Count == 0)
            {
                Warn("Сначала нажмите «Рассчитать».");
                return;
            }

            _stepIndex++;
            if (_stepIndex >= _steps.Count) _stepIndex = _steps.Count - 1;

            RenderStep();
        }
    }
}
