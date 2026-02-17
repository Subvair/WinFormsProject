using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinFinderWinForms
{
    public partial class Lab2_SlaeSolverForm : Form
    {
        public Lab2_SlaeSolverForm()
        {
            InitializeComponent();
            InitGrid();

            // MenuStrip actions
            miClear.Click += (_, __) => ClearAll();
            miGenerate.Click += (_, __) => Generate();
            miAddRow.Click += (_, __) => AddRow();
            miRemoveRow.Click += (_, __) => RemoveRow();
            miLoadExcel.Click += (_, __) => LoadExcelStub();     // если у тебя уже есть реализация — просто замени этот метод на свою
            miExit.Click += (_, __) => Close();

            btnSolve.Click += async (_, __) => await SolveAsync();

            // дефолт
            Generate();
        }

        private void InitGrid()
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;

            BuildGrid(5); // старт 5x5 как на скрине
        }

        private void BuildGrid(int n)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            for (int j = 0; j < n; j++)
            {
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = $"x{j + 1}",
                    Name = $"colX{j + 1}",
                    SortMode = DataGridViewColumnSortMode.NotSortable
                });
            }

            dgv.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "=",
                Name = "colB",
                SortMode = DataGridViewColumnSortMode.NotSortable
            });

            dgv.Rows.Add(n);

            // чуть симпатичнее
            dgv.Columns[n].Width = 60;
        }

        private void ClearAll()
        {
            foreach (DataGridViewRow r in dgv.Rows)
                foreach (DataGridViewCell c in r.Cells)
                    c.Value = "";

            txtGauss.Text = "";
            txtJordan.Text = "";
            txtCramer.Text = "";
            lblInfo.Text = "Очищено.";
        }

        private void AddRow()
        {
            int n = dgv.Rows.Count;
            if (n >= 50) { Warn("Максимум N = 50"); return; }
            BuildGrid(n + 1);
            lblInfo.Text = $"Размер изменён: N = {n + 1}";
        }

        private void RemoveRow()
        {
            int n = dgv.Rows.Count;
            if (n <= 2) { Warn("Минимум N = 2"); return; }
            BuildGrid(n - 1);
            lblInfo.Text = $"Размер изменён: N = {n - 1}";
        }

        private void Generate()
        {
            int n = dgv.Rows.Count;
            var rnd = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    dgv.Rows[i].Cells[j].Value = rnd.Next(1, 110).ToString(CultureInfo.InvariantCulture);

                dgv.Rows[i].Cells[n].Value = rnd.Next(1, 110).ToString(CultureInfo.InvariantCulture);
            }

            lblInfo.Text = "Сгенерировано. Нажмите «Рассчитать».";
        }

        private static void Warn(string msg) =>
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        private static bool TryParseDouble(object? v, out double x)
        {
            x = 0;
            var s = v?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(s)) return false;
            s = s.Replace(',', '.');
            return double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out x);
        }

        private bool TryReadSystem(out double[,] A, out double[] b, out int n)
        {
            n = dgv.Rows.Count;
            A = new double[n, n];
            b = new double[n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (!TryParseDouble(dgv.Rows[i].Cells[j].Value, out A[i, j]))
                    {
                        Warn($"Некорректное значение A[{i + 1},{j + 1}]");
                        return false;
                    }
                }

                if (!TryParseDouble(dgv.Rows[i].Cells[n].Value, out b[i]))
                {
                    Warn($"Некорректное значение правой части в строке {i + 1}");
                    return false;
                }
            }

            // В твоей постановке: A*X + B = 0 => A*X = -B
            for (int i = 0; i < n; i++) b[i] = -b[i];

            return true;
        }

        private async Task SolveAsync()
        {
            txtGauss.Text = "";
            txtJordan.Text = "";
            txtCramer.Text = "";

            if (!TryReadSystem(out var A0, out var b0, out int n))
                return;

            bool useGauss = cbGauss.Checked;
            bool useJordan = cbJordan.Checked;
            bool useCramer = cbCramer.Checked;

            if (!useGauss && !useJordan && !useCramer)
            {
                Warn("Выберите хотя бы один метод.");
                return;
            }

            lblInfo.Text = "Считаю...";

            if (useGauss)
            {
                var A = (double[,])A0.Clone();
                var b = (double[])b0.Clone();
                var (x, ms, err) = await Task.Run(() => RunWithTime(() => SolveGauss(A, b)));
                txtGauss.Text = err ?? FormatVector("Метод Гаусса", x, ms);
            }

            if (useJordan)
            {
                var A = (double[,])A0.Clone();
                var b = (double[])b0.Clone();
                var (x, ms, err) = await Task.Run(() => RunWithTime(() => SolveJordanGauss(A, b)));
                txtJordan.Text = err ?? FormatVector("Метод Жордана-Гаусса", x, ms);
            }

            if (useCramer)
            {
                if (n > 100)
                {
                    // Крамер крайне медленный на больших n
                    txtCramer.Text = "Метод Крамера:\r\nНе рекомендуется для N > 100 (слишком медленно).";
                }
                else
                {
                    var A = (double[,])A0.Clone();
                    var b = (double[])b0.Clone();
                    var (x, ms, err) = await Task.Run(() => RunWithTime(() => SolveCramer(A, b)));
                    txtCramer.Text = err ?? FormatVector("Метод Крамера", x, ms);
                }
            }

            lblInfo.Text = "Готово.";
        }

        private static (double[]? x, double ms, string? err) RunWithTime(Func<double[]?> action)
        {
            try
            {
                var sw = Stopwatch.StartNew();
                var x = action();
                sw.Stop();
                return (x, sw.Elapsed.TotalMilliseconds, null);
            }
            catch (Exception ex)
            {
                return (null, 0, ex.Message);
            }
        }

        private static string FormatVector(string title, double[]? x, double ms)
        {
            if (x == null) return title + ":\r\nНет решения.";

            var sb = new StringBuilder();
            sb.AppendLine(title + ":");
            for (int i = 0; i < x.Length; i++)
                sb.AppendLine($"x{i + 1} = {x[i]:0.000}");
            sb.AppendLine();
            sb.AppendLine($"Время: {ms:0.000} ms");
            return sb.ToString();
        }

        // ----------------- METHODS -----------------

        // Gaussian elimination with partial pivoting
        private static double[] SolveGauss(double[,] A, double[] b)
        {
            int n = b.Length;

            for (int k = 0; k < n; k++)
            {
                // pivot row
                int piv = k;
                double best = Math.Abs(A[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double v = Math.Abs(A[i, k]);
                    if (v > best) { best = v; piv = i; }
                }
                if (best < 1e-14) throw new InvalidOperationException("Матрица вырождена (det≈0).");

                if (piv != k)
                {
                    for (int j = k; j < n; j++)
                        (A[k, j], A[piv, j]) = (A[piv, j], A[k, j]);
                    (b[k], b[piv]) = (b[piv], b[k]);
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
                x[i] = sum / A[i, i];
            }
            return x;
        }

        // Jordan-Gauss (reduced row echelon form)
        private static double[] SolveJordanGauss(double[,] A, double[] b)
        {
            int n = b.Length;

            for (int k = 0; k < n; k++)
            {
                // pivot
                int piv = k;
                double best = Math.Abs(A[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double v = Math.Abs(A[i, k]);
                    if (v > best) { best = v; piv = i; }
                }
                if (best < 1e-14) throw new InvalidOperationException("Матрица вырождена (det≈0).");

                if (piv != k)
                {
                    for (int j = 0; j < n; j++)
                        (A[k, j], A[piv, j]) = (A[piv, j], A[k, j]);
                    (b[k], b[piv]) = (b[piv], b[k]);
                }

                // normalize row k
                double div = A[k, k];
                for (int j = 0; j < n; j++) A[k, j] /= div;
                b[k] /= div;

                // eliminate other rows
                for (int i = 0; i < n; i++)
                {
                    if (i == k) continue;
                    double factor = A[i, k];
                    A[i, k] = 0;
                    for (int j = 0; j < n; j++)
                        A[i, j] -= factor * A[k, j];
                    b[i] -= factor * b[k];
                }
            }

            // now A ~ I, b is solution
            return b;
        }

        private static double Det(double[,] M)
        {
            int n = M.GetLength(0);
            var A = (double[,])M.Clone();
            double det = 1.0;

            for (int k = 0; k < n; k++)
            {
                int piv = k;
                double best = Math.Abs(A[k, k]);
                for (int i = k + 1; i < n; i++)
                {
                    double v = Math.Abs(A[i, k]);
                    if (v > best) { best = v; piv = i; }
                }

                if (best < 1e-14) return 0.0;

                if (piv != k)
                {
                    for (int j = k; j < n; j++)
                        (A[k, j], A[piv, j]) = (A[piv, j], A[k, j]);
                    det = -det; // swap changes sign
                }

                det *= A[k, k];

                for (int i = k + 1; i < n; i++)
                {
                    double factor = A[i, k] / A[k, k];
                    A[i, k] = 0;
                    for (int j = k + 1; j < n; j++)
                        A[i, j] -= factor * A[k, j];
                }
            }

            return det;
        }

        private static double[] SolveCramer(double[,] A, double[] b)
        {
            int n = b.Length;
            double detA = Det(A);
            if (Math.Abs(detA) < 1e-14)
                throw new InvalidOperationException("det(A)=0, метод Крамера неприменим.");

            var x = new double[n];
            for (int col = 0; col < n; col++)
            {
                var Ai = (double[,])A.Clone();
                for (int row = 0; row < n; row++)
                    Ai[row, col] = b[row];

                x[col] = Det(Ai) / detA;
            }
            return x;
        }

        // Заглушка: если у тебя уже Excel реализован — убери и подключи свой метод
        private void LoadExcelStub()
        {
            MessageBox.Show("Импорт Excel уже был в твоей версии ЛР2. Если хочешь — скинь свой старый код импорта, я встрою его сюда без изменений.",
                "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
