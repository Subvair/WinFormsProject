using NCalc;
using System;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MinFinderWinForms
{
    public partial class Lab3_GoldenSectionForm : Form
    {
        public Lab3_GoldenSectionForm()
        {
            InitializeComponent();
            InitChart();

            miCalc.Click += (_, __) => Calculate();
            miClear.Click += (_, __) => ClearAll();
            miExit.Click += (_, __) => Close();

            // Пример по умолчанию
            txtA.Text = "-2";
            txtB.Text = "6";
            txtE.Text = "0.001";
            txtFx.Text = "x*x - 4*x + 10";
        }

        private void InitChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            var area = new ChartArea("MainArea");
            area.AxisX.Title = "x";
            area.AxisY.Title = "f(x)";
            area.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chart1.ChartAreas.Add(area);

            var sFunc = new Series("f(x)");
            sFunc.ChartType = SeriesChartType.Line;
            sFunc.BorderWidth = 2;
            sFunc.ChartArea = "MainArea";
            chart1.Series.Add(sFunc);

            var sMin = new Series("min");
            sMin.ChartType = SeriesChartType.Point;
            sMin.MarkerStyle = MarkerStyle.Circle;
            sMin.MarkerSize = 10;
            sMin.ChartArea = "MainArea";
            chart1.Series.Add(sMin);
        }

        private void Calculate()
        {
            lblResult.Text = "";

            if (!TryReadInputs(out double a, out double b, out double e, out string fx))
                return;

            // проверка формулы в контрольной точке
            double mid = (a + b) / 2.0;
            if (!TryEval(fx, mid, out _, out string evalErr))
            {
                MessageBox.Show($"Формула не вычисляется.\nПроверьте f(x).\n\nОшибка: {evalErr}",
                    "Некорректная формула", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var (xMin, fMin, iters) = GoldenSectionMin(a, b, e, fx);
            PlotFunction(a, b, fx, xMin, fMin);

            lblResult.Text = $"Метод золотого сечения:\n" +
                             $"x* = {xMin:F6}\n" +
                             $"f(x*) = {fMin:F6}\n" +
                             $"Итераций: {iters}";
        }

        private bool TryReadInputs(out double a, out double b, out double e, out string fx)
        {
            a = b = e = 0;
            fx = "";

            bool okA = TryParseDouble(txtA.Text, out a);
            bool okB = TryParseDouble(txtB.Text, out b);
            bool okE = TryParseDouble(txtE.Text, out e);

            fx = (txtFx.Text ?? "").Trim();

            if (!okA) { Warn("Параметр a введён некорректно (вещественное число).", txtA); return false; }
            if (!okB) { Warn("Параметр b введён некорректно (вещественное число).", txtB); return false; }
            if (!okE) { Warn("Параметр e введён некорректно (вещественное число).", txtE); return false; }

            if (string.IsNullOrWhiteSpace(fx)) { Warn("Введите формулу f(x). Например: x*x - 4*x + 10", txtFx); return false; }
            if (!(a < b)) { Warn("Должно выполняться условие: a < b.", txtA); return false; }
            if (e <= 0) { Warn("Точность e должна быть > 0.", txtE); return false; }
            if (e >= (b - a)) { Warn("Точность e слишком большая (e < b-a).", txtE); return false; }

            return true;
        }

        private void Warn(string msg, Control focus)
        {
            MessageBox.Show(msg, "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focus?.Focus();
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
                    error = "NaN/Infinity (деление на 0 или неверная область определения).";
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

        // ---- Golden section ----
        private (double xMin, double fMin, int iters) GoldenSectionMin(double a, double b, double eps, string fx)
        {
            const double phi = 1.6180339887498948482;
            int iters = 0;

            double x1 = b - (b - a) / phi;
            double x2 = a + (b - a) / phi;

            if (!TryEval(fx, x1, out double f1, out _)) f1 = double.PositiveInfinity;
            if (!TryEval(fx, x2, out double f2, out _)) f2 = double.PositiveInfinity;

            while ((b - a) > eps && iters < 2000)
            {
                iters++;

                if (f1 > f2)
                {
                    a = x1;
                    x1 = x2;
                    f1 = f2;

                    x2 = a + (b - a) / phi;
                    if (!TryEval(fx, x2, out f2, out _)) f2 = double.PositiveInfinity;
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    f2 = f1;

                    x1 = b - (b - a) / phi;
                    if (!TryEval(fx, x1, out f1, out _)) f1 = double.PositiveInfinity;
                }
            }

            double xMin = (a + b) / 2.0;
            if (!TryEval(fx, xMin, out double fMin, out _)) fMin = double.NaN;

            return (xMin, fMin, iters);
        }

        private void PlotFunction(double a, double b, string fx, double xMin, double fMin)
        {
            var sFunc = chart1.Series["f(x)"];
            var sMin = chart1.Series["min"];

            sFunc.Points.Clear();
            sMin.Points.Clear();

            int n = 300;
            double step = (b - a) / n;

            for (int i = 0; i <= n; i++)
            {
                double x = a + i * step;
                if (TryEval(fx, x, out double y, out _))
                    sFunc.Points.AddXY(x, y);
            }

            if (!double.IsNaN(fMin) && !double.IsInfinity(fMin))
                sMin.Points.AddXY(xMin, fMin);

            chart1.ChartAreas["MainArea"].RecalculateAxesScale();
        }

        private void ClearAll()
        {
            txtA.Clear();
            txtB.Clear();
            txtE.Clear();
            txtFx.Clear();
            lblResult.Text = "";
            chart1.Series["f(x)"].Points.Clear();
            chart1.Series["min"].Points.Clear();
        }
    }
}
