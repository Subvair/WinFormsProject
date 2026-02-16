using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NCalc;

namespace MinFinderWinForms
{
    public partial class Lab4_ConvergenceForm : Form
    {
        private List<IterState> _steps = new();
        private int _stepIndex = -1;

        public Lab4_ConvergenceForm()
        {
            InitializeComponent();
            InitChart();

            miCalc.Click += (_, __) => CalculatePrepareSteps();
            miStep.Click += (_, __) => StepNext();
            miClear.Click += (_, __) => ClearAll();
            miExit.Click += (_, __) => Close();

            // пример
            txtA.Text = "-2";
            txtB.Text = "6";
            txtE.Text = "0.001";
            txtFx.Text = "x*x - 4*x + 10";
            lblStatus.Text = "Статус: ожидание";
        }

        // ----------------- Chart -----------------
        private void InitChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            var area = new ChartArea("Main");
            area.AxisX.Title = "x";
            area.AxisY.Title = "f(x)";
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart1.ChartAreas.Add(area);

            // график функции
            var sFunc = new System.Windows.Forms.DataVisualization.Charting.Series("f(x)");
            sFunc.ChartType = SeriesChartType.Line;
            sFunc.BorderWidth = 2;
            sFunc.ChartArea = "Main";
            chart1.Series.Add(sFunc);

            // текущая точка Ньютона x_k
            var sX = new System.Windows.Forms.DataVisualization.Charting.Series("xk");
            sX.ChartType = SeriesChartType.Point;
            sX.MarkerStyle = MarkerStyle.Diamond;
            sX.MarkerSize = 11;
            sX.ChartArea = "Main";
            chart1.Series.Add(sX);

            // вертикальные линии границ интервала [a,b] (для наглядности)
            var sInterval = new System.Windows.Forms.DataVisualization.Charting.Series("interval");
            sInterval.ChartType = SeriesChartType.Line;
            sInterval.BorderWidth = 2;
            sInterval.ChartArea = "Main";
            chart1.Series.Add(sInterval);
        }

        // ----------------- Prepare steps -----------------
        private void CalculatePrepareSteps()
        {
            lblInfo.Text = "";
            _steps.Clear();
            _stepIndex = -1;

            if (!TryReadInputs(out double a, out double b, out double e, out string fx))
                return;

            // проверка формулы
            double mid = (a + b) / 2.0;
            if (!TryEval(fx, mid, out _, out string err))
            {
                MessageBox.Show($"Формула не вычисляется.\nОшибка: {err}",
                    "Ошибка f(x)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // строим график
            PlotFunction(a, b, fx);

            // строим шаги Ньютона
            _steps = BuildStepsNewton(a, b, e, fx);

            if (_steps.Count == 0)
            {
                MessageBox.Show("Не удалось построить шаги метода Ньютона.\n" +
                                "Возможные причины: плохая область определения, f''(x)≈0, или неверная формула.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _stepIndex = 0;
            RenderStep(_steps[_stepIndex], fx);
            lblInfo.Text = $"Шаг 1 / {_steps.Count}\nМетод: Ньютона";
        }

        // ----------------- Step next -----------------
        private void StepNext()
        {
            if (_steps.Count == 0)
            {
                MessageBox.Show("Сначала нажмите «Рассчитать».", "Нет шагов",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_stepIndex < _steps.Count - 1)
                _stepIndex++;

            RenderStep(_steps[_stepIndex], txtFx.Text.Trim());
            lblInfo.Text = $"Шаг {_stepIndex + 1} / {_steps.Count}\nМетод: Ньютона";
        }

        // ----------------- Render step -----------------
        private void RenderStep(IterState st, string fx)
        {
            var sX = chart1.Series["xk"];
            var sInterval = chart1.Series["interval"];

            sX.Points.Clear();
            sInterval.Points.Clear();

            // y-range для вертикальных линий
            var (yMin, yMax) = GetChartYRange();
            double pad = (yMax - yMin) * 0.08;
            double lo = yMin - pad;
            double hi = yMax + pad;

            // вертикальная линия a
            sInterval.Points.AddXY(st.A, lo);
            sInterval.Points.AddXY(st.A, hi);

            // вместо NaN делаем "пустую точку" (безопасно для Chart)
            var empty = new DataPoint();
            empty.IsEmpty = true;
            sInterval.Points.Add(empty);

            sInterval.Points.AddXY(st.B, lo);
            sInterval.Points.AddXY(st.B, hi);

            double y = double.NaN;
            if (TryEval(fx, st.X, out y, out _))
                sX.Points.AddXY(st.X, y);

            lblStatus.Text =
                $"a={st.A:0.######}  b={st.B:0.######}  |b-a|={(st.B - st.A):0.######}\n" +
                $"x_k={st.X:0.######}  f(x_k)={y:0.######}";
        }

        private (double yMin, double yMax) GetChartYRange()
        {
            var sFunc = chart1.Series["f(x)"];
            double min = double.PositiveInfinity, max = double.NegativeInfinity;

            foreach (var p in sFunc.Points)
            {
                double y = p.YValues[0];
                if (double.IsNaN(y) || double.IsInfinity(y)) continue;
                min = Math.Min(min, y);
                max = Math.Max(max, y);
            }

            if (!double.IsFinite(min) || !double.IsFinite(max) || min == max)
                return (-1, 1);

            return (min, max);
        }

        // ----------------- Newton steps -----------------
        // x_{k+1} = x_k - f'(x_k)/f''(x_k)
        // производные считаем численно (центральные разности)
        private List<IterState> BuildStepsNewton(double a, double b, double eps, string fx)
        {
            var steps = new List<IterState>();

            // старт из середины
            double x = (a + b) / 2.0;

            // шаг для производных
            double h = Math.Max(1e-5, eps / 10.0);

            int iters = 0;
            while (iters < 2000)
            {
                iters++;
                steps.Add(new IterState(a, b, x));

                if (!TryDerivatives(fx, x, h, out double d1, out double d2, out _))
                    break;

                if (Math.Abs(d2) < 1e-12) // почти нулевая 2-я производная
                    break;

                double xNext = x - d1 / d2;

                // если улетел за границы, “зажимаем” в интервал
                if (xNext < a) xNext = a;
                if (xNext > b) xNext = b;

                // критерий остановки по шагу
                if (Math.Abs(xNext - x) <= eps)
                {
                    x = xNext;
                    steps.Add(new IterState(a, b, x));
                    break;
                }

                x = xNext;
            }

            return steps;
        }

        private bool TryDerivatives(string fx, double x, double h, out double d1, out double d2, out string error)
        {
            d1 = d2 = double.NaN;
            error = "";

            // f'(x) ~ (f(x+h) - f(x-h)) / (2h)
            // f''(x) ~ (f(x+h) - 2f(x) + f(x-h)) / (h^2)
            if (!TryEval(fx, x + h, out double fp, out error)) return false;
            if (!TryEval(fx, x - h, out double fm, out error)) return false;
            if (!TryEval(fx, x, out double f0, out error)) return false;

            d1 = (fp - fm) / (2.0 * h);
            d2 = (fp - 2.0 * f0 + fm) / (h * h);

            if (double.IsNaN(d1) || double.IsInfinity(d1) || double.IsNaN(d2) || double.IsInfinity(d2))
            {
                error = "Производные NaN/Infinity.";
                return false;
            }
            return true;
        }

        // ----------------- Plot function -----------------
        private void PlotFunction(double a, double b, string fx)
        {
            var sFunc = chart1.Series["f(x)"];
            sFunc.Points.Clear();

            int n = 450;
            double step = (b - a) / n;

            for (int i = 0; i <= n; i++)
            {
                double x = a + i * step;
                if (TryEval(fx, x, out double y, out _))
                    sFunc.Points.AddXY(x, y);
            }

            chart1.ChartAreas["Main"].RecalculateAxesScale();
        }

        // ----------------- Input + eval -----------------
        private bool TryReadInputs(out double a, out double b, out double e, out string fx)
        {
            a = b = e = 0;
            fx = "";

            bool okA = TryParseDouble(txtA.Text, out a);
            bool okB = TryParseDouble(txtB.Text, out b);
            bool okE = TryParseDouble(txtE.Text, out e);
            fx = (txtFx.Text ?? "").Trim();

            if (!okA) return Warn("Некорректное a (вещественное число).", txtA);
            if (!okB) return Warn("Некорректное b (вещественное число).", txtB);
            if (!okE) return Warn("Некорректное e (вещественное число).", txtE);
            if (string.IsNullOrWhiteSpace(fx)) return Warn("Введите f(x), например: x*x - 4*x + 10", txtFx);

            if (!(a < b)) return Warn("Должно быть a < b.", txtA);
            if (e <= 0) return Warn("e должно быть > 0.", txtE);
            if (e >= (b - a)) return Warn("Нужно e < b-a.", txtE);

            return true;
        }

        private bool Warn(string msg, System.Windows.Forms.Control focus)
        {
            MessageBox.Show(msg, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focus?.Focus();
            return false;
        }

        private bool TryParseDouble(string s, out double value)
        {
            s = (s ?? "").Trim().Replace(',', '.');
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private bool TryEval(string fx, double x, out double y, out string error)
        {
            y = double.NaN;
            error = "";
            try
            {
                var expr = new Expression(fx);
                expr.Parameters["x"] = x;

                var result = expr.Evaluate();
                if (result == null) { error = "Результат пустой."; return false; }

                y = Convert.ToDouble(result, CultureInfo.InvariantCulture);
                if (double.IsNaN(y) || double.IsInfinity(y))
                {
                    error = "NaN/Infinity (деление на 0 или неверная область).";
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

        private void ClearAll()
        {
            txtA.Clear();
            txtB.Clear();
            txtE.Clear();
            txtFx.Clear();

            lblInfo.Text = "";
            lblStatus.Text = "Статус: ожидание";

            _steps.Clear();
            _stepIndex = -1;

            foreach (var s in chart1.Series)
                s.Points.Clear();
        }

        // ----------------- Model -----------------
        private struct IterState
        {
            public double A, B, X;

            public IterState(double a, double b, double x)
            {
                A = a;
                B = b;
                X = x;
            }
        }
    }
}
