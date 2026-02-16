using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace MinFinderWinForms
{
    public partial class Lab5_SortingVisualizerForm : Form
    {
        private readonly HttpClient _http = new HttpClient();

        public Lab5_SortingVisualizerForm()
        {
            InitializeComponent();

            // MenuStrip actions
            miRun.Click += async (_, __) => await RunBenchAsync();
            miClear.Click += (_, __) => ClearAll();
            miExit.Click += (_, __) => Close();

            miGenerate.Click += (_, __) => GenerateToGrid();
            miLoadExcel.Click += (_, __) => LoadFromExcel();
            miSaveExcel.Click += (_, __) => SaveToExcel();
            miLoadGoogleCsv.Click += async (_, __) => await LoadFromGoogleCsvAsync();
            miExportCsv.Click += (_, __) => ExportToCsv();

            // UI actions
            btnParseToGrid.Click += (_, __) => ParseInputToGrid();

            // defaults
            rbAsc.Checked = true;
            nudTests.Value = 1;

            nudN.Value = 50;
            nudMin.Value = -100;
            nudMax.Value = 100;

            cbBubble.Checked = true;
            cbInsertion.Checked = true;
            cbQuick.Checked = true;

            SyncEnabledColumn();
            cbBubble.CheckedChanged += (_, __) => SyncEnabledColumn();
            cbInsertion.CheckedChanged += (_, __) => SyncEnabledColumn();
            cbQuick.CheckedChanged += (_, __) => SyncEnabledColumn();
            cbShaker.CheckedChanged += (_, __) => SyncEnabledColumn();
            cbBogo.CheckedChanged += (_, __) => SyncEnabledColumn();
        }

        // ---------- Helpers ----------
        private bool IsDesc => rbDesc.Checked;

        private void ClearAll()
        {
            txtInput.Clear();
            txtGoogleCsv.Clear();
            dgvInput.Rows.Clear();
            dgvResults.Rows.Clear();
            PrepareResultsGrid();
        }

        private void SyncEnabledColumn()
        {
            // чтобы в таблице результатов галочки соответствовали CheckBox'ам
            if (dgvResults.Rows.Count == 0) PrepareResultsGrid();

            SetEnabled("Пузырьковая", cbBubble.Checked);
            SetEnabled("Вставкой", cbInsertion.Checked);
            SetEnabled("Быстрая", cbQuick.Checked);
            SetEnabled("Шейкерная", cbShaker.Checked);
            SetEnabled("Bogo", cbBogo.Checked);
        }

        private void SetEnabled(string algName, bool enabled)
        {
            int row = FindRow(algName);
            if (row >= 0)
                dgvResults.Rows[row].Cells[colEnabled.Index].Value = enabled;
        }

        private void ParseInputToGrid()
        {
            dgvInput.Rows.Clear();

            var tokens = (txtInput.Text ?? "")
                .Split(new[] { ' ', '\t', '\r', '\n', ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var t in tokens)
                dgvInput.Rows.Add(t.Trim());

            // проверка
            _ = TryReadArrayFromGrid(out _, showErrors: true);
        }

        private void GenerateToGrid()
        {
            int n = (int)nudN.Value;
            int min = (int)nudMin.Value;
            int max = (int)nudMax.Value;

            if (min > max)
            {
                MessageBox.Show("Min не может быть больше Max.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rnd = new Random();
            dgvInput.Rows.Clear();
            for (int i = 0; i < n; i++)
                dgvInput.Rows.Add(rnd.Next(min, max + 1).ToString(CultureInfo.InvariantCulture));
        }

        private bool TryReadArrayFromGrid(out int[] arr, bool showErrors)
        {
            var list = new List<int>();

            foreach (DataGridViewRow row in dgvInput.Rows)
            {
                if (row.IsNewRow) continue;
                string s = (row.Cells[0].Value ?? "").ToString()!.Trim();
                if (string.IsNullOrWhiteSpace(s)) continue;

                if (!int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
                {
                    if (showErrors)
                        MessageBox.Show($"Некорректное число: \"{s}\"", "Ошибка ввода",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    arr = Array.Empty<int>();
                    return false;
                }

                list.Add(v);
            }

            if (list.Count == 0)
            {
                if (showErrors)
                    MessageBox.Show("Нет данных. Введите числа в таблицу, либо вставьте строку и нажмите «Вставить в таблицу».",
                        "Нет данных", MessageBoxButtons.OK, MessageBoxIcon.Information);
                arr = Array.Empty<int>();
                return false;
            }

            // BOGO ограничение (иначе может долго)
            if (cbBogo.Checked && list.Count > 100)
            {
                if (showErrors)
                    MessageBox.Show("BOGO слишком медленный. Ограничение: N <= 100.", "BOGO",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                arr = Array.Empty<int>();
                return false;
            }

            arr = list.ToArray();
            return true;
        }

        private void PrepareResultsGrid()
        {
            dgvResults.Rows.Clear();

            AddAlgRow("Пузырьковая", cbBubble.Checked);
            AddAlgRow("Вставкой", cbInsertion.Checked);
            AddAlgRow("Быстрая", cbQuick.Checked);
            AddAlgRow("Шейкерная", cbShaker.Checked);
            AddAlgRow("Bogo", cbBogo.Checked);
        }

        private void AddAlgRow(string name, bool enabled)
        {
            int r = dgvResults.Rows.Add();
            dgvResults.Rows[r].Cells[colEnabled.Index].Value = enabled;
            dgvResults.Rows[r].Cells[colAlg.Index].Value = name;
            dgvResults.Rows[r].Cells[colIter.Index].Value = 0;
            dgvResults.Rows[r].Cells[colTime.Index].Value = "0";
            dgvResults.Rows[r].Cells[colResult.Index].Value = "";
            dgvResults.Rows[r].DefaultCellStyle.Font = dgvResults.Font; // сброс жирного
        }

        private int FindRow(string algName)
        {
            foreach (DataGridViewRow r in dgvResults.Rows)
            {
                if ((r.Cells[colAlg.Index].Value?.ToString() ?? "") == algName)
                    return r.Index;
            }
            return -1;
        }

        private static string FormatArray(int[] a)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
            {
                sb.Append(a[i].ToString(CultureInfo.InvariantCulture));
                if (i != a.Length - 1) sb.Append(' ');
                if ((i + 1) % 16 == 0) sb.AppendLine();
            }
            return sb.ToString();
        }

        private void MarkFastest()
        {
            double best = double.PositiveInfinity;
            int bestRow = -1;

            foreach (DataGridViewRow r in dgvResults.Rows)
            {
                bool enabled = Convert.ToBoolean(r.Cells[colEnabled.Index].Value ?? false);
                if (!enabled) continue;

                if (!double.TryParse((r.Cells[colTime.Index].Value ?? "").ToString(),
                        NumberStyles.Float, CultureInfo.InvariantCulture, out double ms))
                    continue;

                if (ms > 0 && ms < best)
                {
                    best = ms;
                    bestRow = r.Index;
                }
            }

            if (bestRow >= 0)
            {
                dgvResults.Rows[bestRow].DefaultCellStyle.Font =
                    new System.Drawing.Font(dgvResults.Font, System.Drawing.FontStyle.Bold);
            }
        }

        // ---------- Main run ----------
        private async Task RunBenchAsync()
        {
            // если пользователь ввёл строку, но не залил в grid — подстрахуемся
            if (dgvInput.Rows.Count <= 1 && !string.IsNullOrWhiteSpace(txtInput.Text))
                ParseInputToGrid();

            if (!TryReadArrayFromGrid(out var baseArr, showErrors: true))
                return;

            int tests = (int)nudTests.Value;
            if (tests <= 0) tests = 1;

            PrepareResultsGrid();
            SyncEnabledColumn();

            var tasks = new List<Task>();

            tasks.Add(RunOneIfEnabled("Пузырьковая", cbBubble.Checked, baseArr, tests, BubbleSort));
            tasks.Add(RunOneIfEnabled("Вставкой", cbInsertion.Checked, baseArr, tests, InsertionSort));
            tasks.Add(RunOneIfEnabled("Быстрая", cbQuick.Checked, baseArr, tests, QuickSort));
            tasks.Add(RunOneIfEnabled("Шейкерная", cbShaker.Checked, baseArr, tests, ShakerSort));
            tasks.Add(RunOneIfEnabled("Bogo", cbBogo.Checked, baseArr, tests, BogoSort));

            await Task.WhenAll(tasks);

            MarkFastest();
        }

        private async Task RunOneIfEnabled(
            string algName,
            bool enabled,
            int[] baseArr,
            int tests,
            Func<int[], bool, (long iterations, int[] sorted)> sorter)
        {
            if (!enabled) return;

            await Task.Run(() =>
            {
                double totalMs = 0;
                long totalIter = 0;
                int[]? last = null;

                for (int t = 0; t < tests; t++)
                {
                    var a = (int[])baseArr.Clone();

                    var sw = Stopwatch.StartNew();
                    var (iters, sorted) = sorter(a, IsDesc);
                    sw.Stop();

                    totalMs += sw.Elapsed.TotalMilliseconds;
                    totalIter += iters;
                    last = sorted;
                }

                double avgMs = totalMs / tests;
                long avgIter = totalIter / tests;

                string res = last == null ? "" : FormatArray(last);

                BeginInvoke(new Action(() =>
                {
                    int row = FindRow(algName);
                    if (row < 0) return;

                    dgvResults.Rows[row].Cells[colIter.Index].Value = avgIter;
                    dgvResults.Rows[row].Cells[colTime.Index].Value = avgMs.ToString("0.####", CultureInfo.InvariantCulture);
                    dgvResults.Rows[row].Cells[colResult.Index].Value = res;
                }));
            });
        }

        // ---------- Sorting implementations ----------
        private static (long iterations, int[] sorted) BubbleSort(int[] a, bool desc)
        {
            long it = 0;
            int n = a.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < n - 1 - i; j++)
                {
                    it++;
                    bool needSwap = !desc ? a[j] > a[j + 1] : a[j] < a[j + 1];
                    if (needSwap)
                    {
                        (a[j], a[j + 1]) = (a[j + 1], a[j]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }

            return (it, a);
        }

        private static (long iterations, int[] sorted) InsertionSort(int[] a, bool desc)
        {
            long it = 0;

            for (int i = 1; i < a.Length; i++)
            {
                int key = a[i];
                int j = i - 1;

                while (j >= 0)
                {
                    it++;
                    bool move = !desc ? a[j] > key : a[j] < key;
                    if (!move) break;

                    a[j + 1] = a[j];
                    j--;
                }

                a[j + 1] = key;
            }

            return (it, a);
        }

        private static (long iterations, int[] sorted) ShakerSort(int[] a, bool desc)
        {
            long it = 0;
            int left = 0, right = a.Length - 1;

            while (left < right)
            {
                bool swapped = false;

                for (int i = left; i < right; i++)
                {
                    it++;
                    bool needSwap = !desc ? a[i] > a[i + 1] : a[i] < a[i + 1];
                    if (needSwap)
                    {
                        (a[i], a[i + 1]) = (a[i + 1], a[i]);
                        swapped = true;
                    }
                }
                right--;

                for (int i = right; i > left; i--)
                {
                    it++;
                    bool needSwap = !desc ? a[i - 1] > a[i] : a[i - 1] < a[i];
                    if (needSwap)
                    {
                        (a[i - 1], a[i]) = (a[i], a[i - 1]);
                        swapped = true;
                    }
                }
                left++;

                if (!swapped) break;
            }

            return (it, a);
        }

        private static (long iterations, int[] sorted) QuickSort(int[] a, bool desc)
        {
            long it = 0;

            void Q(int l, int r)
            {
                int i = l, j = r;
                int pivot = a[(l + r) / 2];

                while (i <= j)
                {
                    while (true)
                    {
                        it++;
                        if (!desc ? a[i] < pivot : a[i] > pivot) i++;
                        else break;
                    }
                    while (true)
                    {
                        it++;
                        if (!desc ? a[j] > pivot : a[j] < pivot) j--;
                        else break;
                    }

                    if (i <= j)
                    {
                        if (i != j) (a[i], a[j]) = (a[j], a[i]);
                        i++; j--;
                    }
                }

                if (l < j) Q(l, j);
                if (i < r) Q(i, r);
            }

            if (a.Length > 1)
                Q(0, a.Length - 1);

            return (it, a);
        }

        private static (long iterations, int[] sorted) BogoSort(int[] a, bool desc)
        {
            long it = 0;
            var rnd = new Random();

            bool Sorted()
            {
                for (int i = 1; i < a.Length; i++)
                {
                    it++;
                    if (!desc)
                    {
                        if (a[i - 1] > a[i]) return false;
                    }
                    else
                    {
                        if (a[i - 1] < a[i]) return false;
                    }
                }
                return true;
            }

            var sw = Stopwatch.StartNew();
            const int maxIter = 200_000;
            const int maxMs = 4000;

            while (!Sorted())
            {
                if (it > maxIter || sw.ElapsedMilliseconds > maxMs)
                    break;

                for (int i = a.Length - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    (a[i], a[j]) = (a[j], a[i]);
                }
            }

            return (it, a);
        }

        // ---------- Excel / Google / CSV ----------
        private void LoadFromExcel()
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Excel (*.xlsx)|*.xlsx";
            if (ofd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var wb = new XLWorkbook(ofd.FileName);
                var ws = wb.Worksheets.First();

                dgvInput.Rows.Clear();

                int row = 1;
                while (true)
                {
                    string s = ws.Cell(row, 1).GetString().Trim();
                    if (string.IsNullOrWhiteSpace(s)) break;

                    dgvInput.Rows.Add(s);
                    row++;
                    if (row > 10000) break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Excel ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToExcel()
        {
            if (!TryReadArrayFromGrid(out var arr, showErrors: true))
                return;

            using var sfd = new SaveFileDialog();
            sfd.Filter = "Excel (*.xlsx)|*.xlsx";
            sfd.FileName = "lab5_data.xlsx";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var wb = new XLWorkbook();
                var ws = wb.AddWorksheet("Data");

                for (int i = 0; i < arr.Length; i++)
                    ws.Cell(i + 1, 1).Value = arr[i];

                // можно ещё вывести результаты на отдельный лист
                var ws2 = wb.AddWorksheet("Results");
                ws2.Cell(1, 1).Value = "Алгоритм";
                ws2.Cell(1, 2).Value = "Средние итерации";
                ws2.Cell(1, 3).Value = "Среднее время (ms)";

                int r = 2;
                foreach (DataGridViewRow row in dgvResults.Rows)
                {
                    bool enabled = Convert.ToBoolean(row.Cells[colEnabled.Index].Value ?? false);
                    if (!enabled) continue;

                    ws2.Cell(r, 1).Value = row.Cells[colAlg.Index].Value?.ToString() ?? "";
                    ws2.Cell(r, 2).Value = row.Cells[colIter.Index].Value?.ToString() ?? "";
                    ws2.Cell(r, 3).Value = row.Cells[colTime.Index].Value?.ToString() ?? "";
                    r++;
                }

                wb.SaveAs(sfd.FileName);
                MessageBox.Show("Сохранено в Excel.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Excel ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadFromGoogleCsvAsync()
        {
            string url = (txtGoogleCsv.Text ?? "").Trim();
            if (string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Вставь CSV-ссылку Google Sheets (Publish to web → CSV).", "Нет ссылки",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                string csv = await _http.GetStringAsync(url);
                var values = ParseCsvFirstColumn(csv);

                dgvInput.Rows.Clear();
                foreach (var v in values)
                    dgvInput.Rows.Add(v.ToString(CultureInfo.InvariantCulture));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Google CSV ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCsv()
        {
            if (!TryReadArrayFromGrid(out var arr, showErrors: true))
                return;

            using var sfd = new SaveFileDialog();
            sfd.Filter = "CSV (*.csv)|*.csv";
            sfd.FileName = "lab5_data.csv";
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var sw = new StreamWriter(sfd.FileName);
                foreach (var v in arr)
                    sw.WriteLine(v.ToString(CultureInfo.InvariantCulture));

                MessageBox.Show("CSV сохранён. Его можно импортировать в Google Sheets.", "OK",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "CSV ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static List<int> ParseCsvFirstColumn(string csv)
        {
            var list = new List<int>();
            using var sr = new StringReader(csv);

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Trim();
                if (line.Length == 0) continue;

                string first = line.Split(new[] { ',', ';', '\t' }, 2)[0].Trim().Trim('"');
                if (int.TryParse(first, NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
                    list.Add(v);
            }

            return list;
        }
    }
}
