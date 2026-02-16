using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinFinderWinForms
{
    public partial class Lab2_SlaeSolverForm : Form
    {
        private const int NMin = 2;
        private const int NMax = 50;
        private const int MaxNForCramer = 12; // иначе очень долго (N^4)

        public Lab2_SlaeSolverForm()
        {
            InitializeComponent();

            miCalc.Click += async (_, __) => await CalculateAsync();
            miClear.Click += (_, __) => ClearAll();
            miGenerate.Click += (_, __) => GenerateRandom();

            miImportExcel.Click += (_, __) => ImportFromExcel();
            miExportExcel.Click += (_, __) => ExportToExcel();

            miImportGoogleCsv.Click += async (_, __) => await ImportFromGoogleCsvAsync();
            miExportCsv.Click += (_, __) => ExportToCsv();

            // defaults
            nudN.Value = 3;
            rbGauss.Checked = true;

            ApplySize((int)nudN.Value);
        }

        // ---------- UI helpers ----------
        private void ApplySize(int n)
        {
            n = Math.Clamp(n, NMin, NMax);

            // A: n x n
            dgvA.Columns.Clear();
            dgvA.Rows.Clear();
            dgvA.RowHeadersWidth = 60;
            dgvA.AllowUserToAddRows = false;

            for (int j = 0; j < n; j++)
            {
                dgvA.Columns.Add($"c{j}", $"A{j + 1}");
                dgvA.Columns[j].Width = 60;
            }
            dgvA.Rows.Add(n);
            for (int i = 0; i < n; i++)
                dgvA.Rows[i].HeaderCell.Value = $"R{i + 1}";

            // B: n x 1
            dgvB.Columns.Clear();
            dgvB.Rows.Clear();
            dgvB.RowHeadersWidth = 60;
            dgvB.AllowUserToAddRows = false;

            dgvB.Columns.Add("b", "B");
            dgvB.Columns[0].Width = 80;
            dgvB.Rows.Add(n);
            for (int i = 0; i < n; i++)
                dgvB.Rows[i].HeaderCell.Value = $"R{i + 1}";

            // X: n x 1
            dgvX.Columns.Clear();
            dgvX.Rows.Clear();
            dgvX.RowHeadersWidth = 60;
            dgvX.AllowUserToAddRows = false;

            dgvX.Columns.Add("x", "X");
            dgvX.Columns[0].Width = 80;
            dgvX.Rows.Add(n);
            for (int i = 0; i < n; i++)
                dgvX.Rows[i].HeaderCell.Value = $"R{i + 1}";
        }

        private void SetStatus(string text) => lblStatus.Text = text;

        private bool TryGetN(out int n)
        {
            n = (int)nudN.Value;
            if (n < NMin || n > NMax)
            {
                MessageBox.Show($"Размер N должен быть в диапазоне {NMin}..{NMax}.",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool TryReadMatrixVector(out double[,] A, out double[] B)
        {
            A = null!;
            B = null!;
            if (!TryGetN(out int n)) return false;

            A = new double[n, n];
            B = new double[n];

            // читаем A
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    var cell = dgvA.Rows[i].Cells[j].Value?.ToString() ?? "";
                    if (!TryParseDouble(cell, out double v))
                    {
                        MessageBox.Show($"Некорректное значение в A[{i + 1},{j + 1}]",
                            "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvA.CurrentCell = dgvA.Rows[i].Cells[j];
                        dgvA.BeginEdit(true);
                        return false;
                    }
                    A[i, j] = v;
                }

            // читаем B
            for (int i = 0; i < n; i++)
            {
                var cell = dgvB.Rows[i].Cells[0].Value?.ToString() ?? "";
                if (!TryParseDouble(cell, out double v))
                {
                    MessageBox.Show($"Некорректное значение в B[{i + 1}]",
                        "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvB.CurrentCell = dgvB.Rows[i].Cells[0];
                    dgvB.BeginEdit(true);
                    return false;
                }
                B[i] = v;
            }

            return true;
        }

        private bool TryParseDouble(string s, out double value)
        {
            s = (s ?? "").Trim().Replace(',', '.');
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out value);
        }

        private void WriteVectorX(double[] X)
        {
            for (int i = 0; i < X.Length; i++)
                dgvX.Rows[i].Cells[0].Value = X[i].ToString("0.######", CultureInfo.InvariantCulture);
        }

        // ---------- Generate ----------
        private void GenerateRandom()
        {
            if (!TryGetN(out int n)) return;
            ApplySize(n);

            var rnd = new Random();
            double NextVal() => Math.Round(rnd.NextDouble() * 20.0 - 10.0, 3); // [-10..10]

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    dgvA.Rows[i].Cells[j].Value = NextVal();

            for (int i = 0; i < n; i++)
                dgvB.Rows[i].Cells[0].Value = NextVal();

            SetStatus("Сгенерировано случайно.");
        }

        // ---------- Async Calculate + timing ----------
        private async Task CalculateAsync()
        {
            dgvX.ClearSelection();
            SetStatus("Чтение данных...");

            if (!TryReadMatrixVector(out var A, out var B))
                return;

            // A·X + B = 0  =>  A·X = -B
            var rhs = B.Select(v => -v).ToArray();

            string method = GetSelectedMethod();
            if (method == "Крамер" && A.GetLength(0) > MaxNForCramer)
            {
                MessageBox.Show($"Метод Крамера очень медленный. Для него ограничение N <= {MaxNForCramer}.\n" +
                                $"Выберите Гаусса или Жордана-Гаусса для N до 50.",
                    "Ограничение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SetStatus($"Расчёт: {method} (асинхронно) ...");

            var sw = Stopwatch.StartNew();
            try
            {
                double[] X = await Task.Run(() =>
                {
                    // отдельный поток: численные методы
                    return method switch
                    {
                        "Гаусс" => SolveGauss(A, rhs),
                        "Жордан-Гаусс" => SolveGaussJordan(A, rhs),
                        "Крамер" => SolveCramer(A, rhs),
                        _ => throw new InvalidOperationException("Неизвестный метод")
                    };
                });

                sw.Stop();
                WriteVectorX(X);
                SetStatus($"Готово. Время: {sw.ElapsedMilliseconds} ms");
            }
            catch (Exception ex)
            {
                sw.Stop();
                MessageBox.Show($"Ошибка решения СЛАУ:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus($"Ошибка. Время: {sw.ElapsedMilliseconds} ms");
            }
        }

        private string GetSelectedMethod()
        {
            if (rbGauss.Checked) return "Гаусс";
            if (rbJordan.Checked) return "Жордан-Гаусс";
            return "Крамер";
        }

        // ---------- Methods ----------
        // Gauss with partial pivoting
        private static double[] SolveGauss(double[,] A0, double[] b0)
        {
            int n = A0.GetLength(0);
            var A = (double[,])A0.Clone();
            var b = (double[])b0.Clone();

            for (int k = 0; k < n; k++)
            {
                // pivot
                int pivot = k;
                double max = Math.Abs(A[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double v = Math.Abs(A[i, k]);
                    if (v > max) { max = v; pivot = i; }
                }
                if (Math.Abs(max) < 1e-12)
                    throw new InvalidOperationException("Матрица вырождена или близка к вырожденной (нулевой ведущий элемент).");

                // swap rows
                if (pivot != k)
                {
                    for (int j = k; j < n; j++)
                        (A[k, j], A[pivot, j]) = (A[pivot, j], A[k, j]);
                    (b[k], b[pivot]) = (b[pivot], b[k]);
                }

                // eliminate
                for (int i = k + 1; i < n; i++)
                {
                    double factor = A[i, k] / A[k, k];
                    A[i, k] = 0;
                    for (int j = k + 1; j < n; j++)
                        A[i, j] -= factor * A[k, j];
                    b[i] -= factor * b[k];
                }
            }

            // back substitution
            var x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = b[i];
                for (int j = i + 1; j < n; j++)
                    sum -= A[i, j] * x[j];
                if (Math.Abs(A[i, i]) < 1e-12)
                    throw new InvalidOperationException("Матрица вырождена (деление на 0).");
                x[i] = sum / A[i, i];
            }
            return x;
        }

        // Gauss-Jordan
        private static double[] SolveGaussJordan(double[,] A0, double[] b0)
        {
            int n = A0.GetLength(0);
            var A = (double[,])A0.Clone();
            var b = (double[])b0.Clone();

            for (int col = 0; col < n; col++)
            {
                // pivot row
                int pivot = col;
                double max = Math.Abs(A[col, col]);
                for (int r = col + 1; r < n; r++)
                {
                    double v = Math.Abs(A[r, col]);
                    if (v > max) { max = v; pivot = r; }
                }
                if (Math.Abs(max) < 1e-12)
                    throw new InvalidOperationException("Матрица вырождена или близка к вырожденной.");

                // swap
                if (pivot != col)
                {
                    for (int j = 0; j < n; j++)
                        (A[col, j], A[pivot, j]) = (A[pivot, j], A[col, j]);
                    (b[col], b[pivot]) = (b[pivot], b[col]);
                }

                // normalize pivot row
                double div = A[col, col];
                for (int j = 0; j < n; j++) A[col, j] /= div;
                b[col] /= div;

                // eliminate other rows
                for (int r = 0; r < n; r++)
                {
                    if (r == col) continue;
                    double factor = A[r, col];
                    if (Math.Abs(factor) < 1e-15) continue;

                    for (int j = 0; j < n; j++)
                        A[r, j] -= factor * A[col, j];
                    b[r] -= factor * b[col];
                }
            }

            return b; // b became x
        }

        // Cramer's rule using determinants (LU)
        private static double[] SolveCramer(double[,] A0, double[] b0)
        {
            int n = A0.GetLength(0);
            var detA = DeterminantLU(A0);
            if (Math.Abs(detA) < 1e-12)
                throw new InvalidOperationException("det(A)=0, метод Крамера неприменим (матрица вырождена).");

            var x = new double[n];
            for (int k = 0; k < n; k++)
            {
                var Ak = (double[,])A0.Clone();
                for (int i = 0; i < n; i++)
                    Ak[i, k] = b0[i];

                double detAk = DeterminantLU(Ak);
                x[k] = detAk / detA;
            }
            return x;
        }

        private static double DeterminantLU(double[,] A0)
        {
            int n = A0.GetLength(0);
            var A = (double[,])A0.Clone();
            double det = 1.0;
            int sign = 1;

            for (int k = 0; k < n; k++)
            {
                // pivot
                int pivot = k;
                double max = Math.Abs(A[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double v = Math.Abs(A[i, k]);
                    if (v > max) { max = v; pivot = i; }
                }
                if (Math.Abs(max) < 1e-12)
                    return 0.0;

                if (pivot != k)
                {
                    for (int j = 0; j < n; j++)
                        (A[k, j], A[pivot, j]) = (A[pivot, j], A[k, j]);
                    sign *= -1;
                }

                double pivotVal = A[k, k];
                det *= pivotVal;

                // eliminate below
                for (int i = k + 1; i < n; i++)
                {
                    double factor = A[i, k] / pivotVal;
                    A[i, k] = 0;
                    for (int j = k + 1; j < n; j++)
                        A[i, j] -= factor * A[k, j];
                }
            }

            return det * sign;
        }

        // ---------- Import/Export Excel ----------
        private void ImportFromExcel()
        {
            using var ofd = new OpenFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx",
                Title = "Импорт A и B из Excel"
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var wb = new XLWorkbook(ofd.FileName);

                // ожидаем лист "AB" или первый лист
                var ws = wb.Worksheets.FirstOrDefault(x => x.Name.Equals("AB", StringComparison.OrdinalIgnoreCase))
                         ?? wb.Worksheets.First();

                // формат: N строк, N колонок A + последняя колонка B
                // считываем "прямоугольник" чисел
                var used = ws.RangeUsed();
                if (used == null) throw new InvalidOperationException("Лист пустой.");

                int rows = used.RowCount();
                int cols = used.ColumnCount();
                if (rows < 2) throw new InvalidOperationException("Недостаточно строк.");
                if (cols < 3) throw new InvalidOperationException("Недостаточно столбцов (нужно хотя бы 2 для A и 1 для B).");

                int n = rows;
                int nA = cols - 1;
                if (nA != n)
                    throw new InvalidOperationException($"Ожидается формат: N строк и N+1 столбцов (A и B). Сейчас: {rows}x{cols}.");

                if (n < NMin || n > NMax)
                    throw new InvalidOperationException($"N должно быть {NMin}..{NMax}, сейчас N={n}.");

                nudN.Value = n;
                ApplySize(n);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        dgvA.Rows[i].Cells[j].Value = ws.Cell(i + 1, j + 1).GetValue<string>();
                    dgvB.Rows[i].Cells[0].Value = ws.Cell(i + 1, n + 1).GetValue<string>();
                }

                SetStatus("Импорт из Excel выполнен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта Excel:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToExcel()
        {
            if (!TryReadMatrixVector(out var A, out var B))
                return;

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel (*.xlsx)|*.xlsx",
                Title = "Экспорт A, B, X в Excel"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                int n = A.GetLength(0);
                var X = ReadXSafe(n);

                using var wb = new XLWorkbook();
                AddSheetABX(wb, "A", A);
                AddSheetVector(wb, "B", B);
                AddSheetVector(wb, "X", X);

                wb.SaveAs(sfd.FileName);
                SetStatus("Экспорт в Excel выполнен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта Excel:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void AddSheetABX(XLWorkbook wb, string name, double[,] A)
        {
            var ws = wb.Worksheets.Add(name);
            int n = A.GetLength(0);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    ws.Cell(i + 1, j + 1).Value = A[i, j];
        }

        private static void AddSheetVector(XLWorkbook wb, string name, double[] v)
        {
            var ws = wb.Worksheets.Add(name);
            for (int i = 0; i < v.Length; i++)
                ws.Cell(i + 1, 1).Value = v[i];
        }

        private double[] ReadXSafe(int n)
        {
            var x = new double[n];
            for (int i = 0; i < n; i++)
            {
                var s = dgvX.Rows[i].Cells[0].Value?.ToString() ?? "0";
                if (!TryParseDouble(s, out x[i])) x[i] = 0;
            }
            return x;
        }

        // ---------- Google Sheets via CSV ----------
        // В Google Sheets: File -> Share -> Publish to web -> CSV
        private async Task ImportFromGoogleCsvAsync()
        {
            string url = (txtGoogleUrl.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Вставь CSV-ссылку Google Sheets (Publish to web → CSV).",
                    "Нет ссылки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGoogleUrl.Focus();
                return;
            }

            try
            {
                SetStatus("Загрузка CSV...");
                using var http = new HttpClient();
                var csv = await http.GetStringAsync(url);

                // формат CSV: N строк, N+1 столбец (A и B)
                var rows = ParseCsv(csv);
                if (rows.Count < 2) throw new InvalidOperationException("CSV пустой или слишком короткий.");

                // пытаемся определить числовые строки (на случай заголовка)
                rows = rows.Where(r => r.Any(x => double.TryParse(x.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out _))).ToList();

                int n = rows.Count;
                int cols = rows.Max(r => r.Count);
                if (cols != n + 1)
                    throw new InvalidOperationException($"Ожидается формат N строк и N+1 столбцов. Сейчас строк={n}, столбцов={cols}.");

                if (n < NMin || n > NMax)
                    throw new InvalidOperationException($"N должно быть {NMin}..{NMax}, сейчас N={n}.");

                nudN.Value = n;
                ApplySize(n);

                for (int i = 0; i < n; i++)
                {
                    var r = rows[i];
                    for (int j = 0; j < n; j++)
                        dgvA.Rows[i].Cells[j].Value = r[j];
                    dgvB.Rows[i].Cells[0].Value = r[n];
                }

                SetStatus("Импорт из Google CSV выполнен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка импорта Google CSV:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetStatus("Ошибка импорта CSV.");
            }
        }

        private void ExportToCsv()
        {
            if (!TryReadMatrixVector(out var A, out var B))
                return;

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                Title = "Экспорт (A | B) в CSV"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                int n = A.GetLength(0);
                using var sw = new StreamWriter(sfd.FileName);

                // каждая строка: A row + B
                for (int i = 0; i < n; i++)
                {
                    var parts = new List<string>();
                    for (int j = 0; j < n; j++)
                        parts.Add(A[i, j].ToString(CultureInfo.InvariantCulture));
                    parts.Add(B[i].ToString(CultureInfo.InvariantCulture));
                    sw.WriteLine(string.Join(",", parts));
                }

                SetStatus("CSV сохранён (можно импортировать в Google Sheets).");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка экспорта CSV:\n{ex.Message}",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<List<string>> ParseCsv(string csv)
        {
            // простой парсер: без сложных кавычек (для чисел хватает)
            var lines = csv.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<List<string>>();
            foreach (var line in lines)
                result.Add(line.Split(',').Select(x => x.Trim()).ToList());
            return result;
        }

        // ---------- Clear ----------
        private void ClearAll()
        {
            if (!TryGetN(out int n)) return;
            ApplySize(n);
            txtGoogleUrl.Text = "";
            SetStatus("Очищено.");
        }

        // N change
        private void nudN_ValueChanged(object sender, EventArgs e)
        {
            ApplySize((int)nudN.Value);
        }
    }
}
