using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
// ВАЖНО: не подключаем OpenXml ChartDrawing namespaces, чтобы не было неоднозначностей с Legend/Series.
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MinFinderWinForms
{
    public partial class Lab8_LeastSquaresForm : Form
    {
        public Lab8_LeastSquaresForm()
        {
            InitializeComponent();
            InitGrid();
            InitChart();

            miCalc.Click += (_, __) => Calculate();
            miGen.Click += (_, __) => GenerateData();
            miClear.Click += (_, __) => ClearAll();
            miImportExcel.Click += (_, __) => ImportExcel();
            miExportExcel.Click += (_, __) => ExportExcel();
            miImportGoogle.Click += (_, __) => ImportGoogleFromClipboard();
            miExportGoogle.Click += (_, __) => ExportGoogleToClipboard();
            miExit.Click += (_, __) => Close();

            // небольшой пример
            GenerateData();
        }

        // ---------------- UI init ----------------
        private void InitGrid()
        {
            dgv.AllowUserToAddRows = true;
            dgv.AllowUserToDeleteRows = true;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible = false;

            dgv.Columns.Clear();
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colX", HeaderText = "x" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colY", HeaderText = "y" });
        }

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
        }

        private void ClearAll()
        {
            dgv.Rows.Clear();
            txtOut.Text = "—";
            lblInfo.Text = "Готово.";

            InitChart();
        }

        // ---------------- Helpers ----------------
        private void Warn(string msg)
        {
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private static bool TryParseDouble(string? s, out double v)
        {
            v = 0;
            if (s == null) return false;
            s = s.Trim().Replace(',', '.');
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v);
        }

        private bool TryReadPoints(out List<(double x, double y)> pts)
        {
            pts = new();

            for (int r = 0; r < dgv.Rows.Count; r++)
            {
                var row = dgv.Rows[r];
                if (row.IsNewRow) continue;

                var sx = row.Cells[0].Value?.ToString();
                var sy = row.Cells[1].Value?.ToString();

                if (string.IsNullOrWhiteSpace(sx) && string.IsNullOrWhiteSpace(sy))
                    continue;

                if (!TryParseDouble(sx, out double x))
                {
                    Warn($"Некорректное x в строке {r + 1}.");
                    return false;
                }
                if (!TryParseDouble(sy, out double y))
                {
                    Warn($"Некорректное y в строке {r + 1}.");
                    return false;
                }

                pts.Add((x, y));
            }

            if (pts.Count < 2)
            {
                Warn("Нужно минимум 2 точки.");
                return false;
            }

            // проверим, что не все x одинаковые (иначе матрица выродится для линейной)
            if (pts.Select(p => p.x).Distinct().Count() == 1)
            {
                Warn("Все x одинаковые — аппроксимация невозможна (матрица вырождается).");
                return false;
            }

            return true;
        }

        // ---------------- LSM core ----------------
        // Fit polynomial degree d: y ≈ a0 + a1*x + ... + ad*x^d
        private static double[] FitPolynomialLeastSquares(List<(double x, double y)> pts, int degree)
        {
            int m = degree + 1;

            // Normal equations: (A^T A) a = A^T y
            // where A[i,j] = x_i^j
            double[,] ATA = new double[m, m];
            double[] ATy = new double[m];

            foreach (var (x, y) in pts)
            {
                double[] pow = new double[m];
                pow[0] = 1.0;
                for (int j = 1; j < m; j++) pow[j] = pow[j - 1] * x;

                for (int r = 0; r < m; r++)
                {
                    ATy[r] += pow[r] * y;
                    for (int c = 0; c < m; c++)
                        ATA[r, c] += pow[r] * pow[c];
                }
            }

            return SolveLinearSystem(ATA, ATy);
        }

        // Gaussian elimination with partial pivoting
        private static double[] SolveLinearSystem(double[,] A, double[] b)
        {
            int n = b.Length;
            double[,] M = (double[,])A.Clone();
            double[] x = (double[])b.Clone();

            for (int k = 0; k < n; k++)
            {
                // pivot
                int piv = k;
                double best = Math.Abs(M[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double v = Math.Abs(M[i, k]);
                    if (v > best) { best = v; piv = i; }
                }

                if (best < 1e-14)
                    throw new InvalidOperationException("Матрица вырождена или близка к вырожденной (недостаточно разнообразные точки).");

                if (piv != k)
                {
                    // swap rows in M
                    for (int j = k; j < n; j++)
                    {
                        double tmp = M[k, j];
                        M[k, j] = M[piv, j];
                        M[piv, j] = tmp;
                    }
                    // swap in x
                    double t = x[k];
                    x[k] = x[piv];
                    x[piv] = t;
                }

                // eliminate
                for (int i = k + 1; i < n; i++)
                {
                    double factor = M[i, k] / M[k, k];
                    M[i, k] = 0;
                    for (int j = k + 1; j < n; j++)
                        M[i, j] -= factor * M[k, j];
                    x[i] -= factor * x[k];
                }
            }

            // back-substitution
            double[] sol = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; j++)
                    sum -= M[i, j] * sol[j];
                sol[i] = sum / M[i, i];
            }
            return sol;
        }

        private static double PolyValue(double[] a, double x)
        {
            double s = 0;
            double p = 1;
            for (int i = 0; i < a.Length; i++)
            {
                s += a[i] * p;
                p *= x;
            }
            return s;
        }

        private static string PolyToString(double[] a)
        {
            // a0 + a1*x + a2*x^2
            var sb = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                double coef = a[i];
                string sign = (coef >= 0 && i > 0) ? " + " : (coef < 0 && i > 0) ? " - " : "";
                sb.Append(sign);

                double abs = Math.Abs(coef);
                if (i == 0) sb.Append($"{coef:G10}");
                else
                {
                    sb.Append($"{abs:G10}*x");
                    if (i >= 2) sb.Append("^" + i);
                }
            }
            return sb.ToString();
        }

        private static double Sse(List<(double x, double y)> pts, double[] a)
        {
            double s = 0;
            foreach (var (x, y) in pts)
            {
                double e = y - PolyValue(a, x);
                s += e * e;
            }
            return s;
        }

        // ---------------- Main calc ----------------
        private void Calculate()
        {
            try
            {
                if (!TryReadPoints(out var pts))
                    return;

                // аппроксимации
                double[] a1 = FitPolynomialLeastSquares(pts, degree: 1);
                double[] a2 = FitPolynomialLeastSquares(pts, degree: 2);

                double sse1 = Sse(pts, a1);
                double sse2 = Sse(pts, a2);

                txtOut.Text =
                    "Метод наименьших квадратов\n\n" +
                    "n = 1 (линейная): y = a0 + a1*x\n" +
                    $"a0 = {a1[0]:G17}\n" +
                    $"a1 = {a1[1]:G17}\n" +
                    $"Формула: y = {PolyToString(a1)}\n" +
                    $"SSE = {sse1:G17}\n\n" +
                    "n = 2 (квадратичная): y = a0 + a1*x + a2*x^2\n" +
                    $"a0 = {a2[0]:G17}\n" +
                    $"a1 = {a2[1]:G17}\n" +
                    $"a2 = {a2[2]:G17}\n" +
                    $"Формула: y = {PolyToString(a2)}\n" +
                    $"SSE = {sse2:G17}\n\n" +
                    "Чем меньше SSE, тем лучше аппроксимация.";

                Plot(pts, a1, a2);

                lblInfo.Text = "Готово. Построены точки и две аппроксимации.";
            }
            catch (Exception ex)
            {
                Warn(ex.Message);
            }
        }

        private void Plot(List<(double x, double y)> pts, double[] a1, double[] a2)
        {
            InitChart();

            var sPts = new System.Windows.Forms.DataVisualization.Charting.Series("Точки");
            sPts.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            sPts.MarkerSize = 7;
            sPts.ChartArea = "Main";
            sPts.Legend = "Legend";

            foreach (var p in pts)
                sPts.Points.AddXY(p.x, p.y);

            chart1.Series.Add(sPts);

            // диапазон x
            double xmin = pts.Min(p => p.x);
            double xmax = pts.Max(p => p.x);
            if (Math.Abs(xmax - xmin) < 1e-12) { xmax = xmin + 1; }

            int samples = 400;

            var sL = new System.Windows.Forms.DataVisualization.Charting.Series("Аппрокс. n=1");
            sL.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sL.BorderWidth = 2;
            sL.ChartArea = "Main";
            sL.Legend = "Legend";

            var sQ = new System.Windows.Forms.DataVisualization.Charting.Series("Аппрокс. n=2");
            sQ.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            sQ.BorderWidth = 2;
            sQ.ChartArea = "Main";
            sQ.Legend = "Legend";

            for (int i = 0; i <= samples; i++)
            {
                double x = xmin + (xmax - xmin) * i / samples;
                sL.Points.AddXY(x, PolyValue(a1, x));
                sQ.Points.AddXY(x, PolyValue(a2, x));
            }

            chart1.Series.Add(sL);
            chart1.Series.Add(sQ);
        }

        // ---------------- Generation ----------------
        private void GenerateData()
        {
            ClearAll();

            // генерируем точки вокруг квадратичной зависимости с шумом
            var rnd = new Random();
            int n = 20;

            // "истинная" функция: y = 2 - 0.5x + 0.3x^2 + noise
            for (int i = 0; i < n; i++)
            {
                double x = -5 + 10.0 * i / (n - 1);
                double y = 2 - 0.5 * x + 0.3 * x * x + (rnd.NextDouble() - 0.5) * 2.0; // шум [-1;1]
                dgv.Rows.Add(x.ToString("G10", CultureInfo.InvariantCulture), y.ToString("G10", CultureInfo.InvariantCulture));
            }

            lblInfo.Text = "Данные сгенерированы. Нажмите «Рассчитать».";
        }

        // ---------------- Google Table (clipboard TSV) ----------------
        private void ImportGoogleFromClipboard()
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    Warn("В буфере обмена нет текста. Скопируйте 2 колонки (x и y) из Google Sheets и повторите.");
                    return;
                }

                string text = Clipboard.GetText();
                var lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length == 0)
                {
                    Warn("Пустые данные в буфере.");
                    return;
                }

                dgv.Rows.Clear();

                foreach (var ln in lines)
                {
                    // обычно из Google Sheets копируется TSV (табами)
                    var parts = ln.Split('\t');
                    if (parts.Length < 2) parts = ln.Split(';', ',', ' ');

                    if (parts.Length < 2) continue;

                    if (!TryParseDouble(parts[0], out double x) || !TryParseDouble(parts[1], out double y))
                        continue;

                    dgv.Rows.Add(x.ToString("G10", CultureInfo.InvariantCulture), y.ToString("G10", CultureInfo.InvariantCulture));
                }

                lblInfo.Text = "Импорт из Google Table выполнен (через буфер обмена).";
            }
            catch (Exception ex)
            {
                Warn(ex.Message);
            }
        }

        private void ExportGoogleToClipboard()
        {
            try
            {
                if (!TryReadPoints(out var pts)) return;

                // TSV: x<TAB>y
                var sb = new StringBuilder();
                foreach (var (x, y) in pts)
                {
                    sb.Append(x.ToString("G17", CultureInfo.InvariantCulture));
                    sb.Append('\t');
                    sb.Append(y.ToString("G17", CultureInfo.InvariantCulture));
                    sb.AppendLine();
                }

                Clipboard.SetText(sb.ToString());
                lblInfo.Text = "Скопировано в буфер (TSV). Вставьте в Google Sheets.";
            }
            catch (Exception ex)
            {
                Warn(ex.Message);
            }
        }

        // ---------------- Excel (OpenXML) ----------------
        private void ImportExcel()
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                Title = "Импорт точек из Excel"
            };

            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                var pts = ReadPointsFromXlsx(ofd.FileName);
                dgv.Rows.Clear();
                foreach (var p in pts)
                    dgv.Rows.Add(p.x.ToString("G10", CultureInfo.InvariantCulture), p.y.ToString("G10", CultureInfo.InvariantCulture));

                lblInfo.Text = $"Импорт Excel: загружено точек {pts.Count}.";
            }
            catch (Exception ex)
            {
                Warn("Excel импорт: " + ex.Message);
            }
        }

        private void ExportExcel()
        {
            if (!TryReadPoints(out var pts)) return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel files (*.xlsx)|*.xlsx",
                Title = "Экспорт точек в Excel",
                FileName = "LeastSquaresPoints.xlsx"
            };

            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                WritePointsToXlsx(sfd.FileName, pts);
                lblInfo.Text = "Экспорт Excel выполнен.";
            }
            catch (Exception ex)
            {
                Warn("Excel экспорт: " + ex.Message);
            }
        }

        private static List<(double x, double y)> ReadPointsFromXlsx(string path)
        {
            var pts = new List<(double x, double y)>();

            using var doc = SpreadsheetDocument.Open(path, false);
            var wbPart = doc.WorkbookPart ?? throw new InvalidOperationException("Нет WorkbookPart.");
            var sheet = wbPart.Workbook.Sheets?.Elements<Sheet>().FirstOrDefault()
                        ?? throw new InvalidOperationException("Нет листов в книге.");

            var wsPart = (WorksheetPart)wbPart.GetPartById(sheet.Id!);
            var sheetData = wsPart.Worksheet.Elements<SheetData>().FirstOrDefault()
                            ?? throw new InvalidOperationException("Нет данных на листе.");

            foreach (var row in sheetData.Elements<Row>())
            {
                var cells = row.Elements<Cell>().ToList();
                if (cells.Count < 2) continue;

                string sx = GetCellText(wbPart, cells[0]);
                string sy = GetCellText(wbPart, cells[1]);

                if (!TryParseDouble(sx, out double x) || !TryParseDouble(sy, out double y))
                    continue;

                pts.Add((x, y));
            }

            if (pts.Count == 0)
                throw new InvalidOperationException("Не найдено ни одной корректной пары (x,y) в первых двух колонках.");

            return pts;
        }

        private static void WritePointsToXlsx(string path, List<(double x, double y)> pts)
        {
            if (File.Exists(path)) File.Delete(path);

            using var doc = SpreadsheetDocument.Create(path, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook);
            var wbPart = doc.AddWorkbookPart();
            wbPart.Workbook = new Workbook();
            var wsPart = wbPart.AddNewPart<WorksheetPart>();
            wsPart.Worksheet = new Worksheet(new SheetData());

            var sheets = doc.WorkbookPart!.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet
            {
                Id = doc.WorkbookPart.GetIdOfPart(wsPart),
                SheetId = 1,
                Name = "Points"
            };
            sheets.Append(sheet);

            var sheetData = wsPart.Worksheet.GetFirstChild<SheetData>()!;

            // header
            sheetData.AppendChild(new Row(new[]
            {
                MakeTextCell("A1", "x"),
                MakeTextCell("B1", "y")
            }));

            uint r = 2;
            foreach (var (x, y) in pts)
            {
                var row = new Row { RowIndex = r };
                row.Append(
                    MakeNumberCell($"A{r}", x),
                    MakeNumberCell($"B{r}", y)
                );
                sheetData.Append(row);
                r++;
            }

            wbPart.Workbook.Save();
        }

        private static Cell MakeTextCell(string cellRef, string text)
        {
            return new Cell
            {
                CellReference = cellRef,
                DataType = CellValues.String,
                CellValue = new CellValue(text)
            };
        }

        private static Cell MakeNumberCell(string cellRef, double value)
        {
            return new Cell
            {
                CellReference = cellRef,
                DataType = CellValues.Number,
                CellValue = new CellValue(value.ToString("G17", CultureInfo.InvariantCulture))
            };
        }

        private static string GetCellText(WorkbookPart wbPart, Cell cell)
        {
            string raw = cell.CellValue?.Text ?? "";

            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                var sst = wbPart.SharedStringTablePart?.SharedStringTable;
                if (sst == null) return "";
                if (int.TryParse(raw, out int id))
                {
                    var item = sst.Elements<SharedStringItem>().ElementAtOrDefault(id);
                    return item?.InnerText ?? "";
                }
            }

            return raw;
        }
    }
}
