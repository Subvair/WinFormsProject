namespace MinFinderWinForms
{
    partial class Lab5_SortingVisualizerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            menuStrip1 = new MenuStrip();
            miRun = new ToolStripMenuItem();
            miClear = new ToolStripMenuItem();
            miGenerate = new ToolStripMenuItem();
            miLoadExcel = new ToolStripMenuItem();
            miSaveExcel = new ToolStripMenuItem();
            miLoadGoogleCsv = new ToolStripMenuItem();
            miExportCsv = new ToolStripMenuItem();
            miExit = new ToolStripMenuItem();
            cbBubble = new CheckBox();
            cbInsertion = new CheckBox();
            cbQuick = new CheckBox();
            cbShaker = new CheckBox();
            cbBogo = new CheckBox();
            rbAsc = new RadioButton();
            rbDesc = new RadioButton();
            lblN = new Label();
            nudN = new NumericUpDown();
            lblMin = new Label();
            nudMin = new NumericUpDown();
            lblMax = new Label();
            nudMax = new NumericUpDown();
            lblGoogle = new Label();
            txtGoogleCsv = new TextBox();
            dgvResults = new DataGridView();
            colEnabled = new DataGridViewCheckBoxColumn();
            colAlg = new DataGridViewTextBoxColumn();
            colIter = new DataGridViewTextBoxColumn();
            colTime = new DataGridViewTextBoxColumn();
            colResult = new DataGridViewTextBoxColumn();
            lblInput = new Label();
            txtInput = new TextBox();
            btnParseToGrid = new Button();
            lblGrid = new Label();
            dgvInput = new DataGridView();
            colValue = new DataGridViewTextBoxColumn();
            lblTests = new Label();
            nudTests = new NumericUpDown();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudN).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInput).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTests).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { miRun, miClear, miGenerate, miLoadExcel, miSaveExcel, miLoadGoogleCsv, miExportCsv, miExit });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(1120, 24);
            menuStrip1.TabIndex = 23;
            // 
            // miRun
            // 
            miRun.Name = "miRun";
            miRun.Size = new Size(80, 20);
            miRun.Text = "Рассчитать";
            // 
            // miClear
            // 
            miClear.Name = "miClear";
            miClear.Size = new Size(71, 20);
            miClear.Text = "Очистить";
            // 
            // miGenerate
            // 
            miGenerate.Name = "miGenerate";
            miGenerate.Size = new Size(77, 20);
            miGenerate.Text = "Генерация";
            // 
            // miLoadExcel
            // 
            miLoadExcel.Name = "miLoadExcel";
            miLoadExcel.Size = new Size(102, 20);
            miLoadExcel.Text = "Загрузить Excel";
            // 
            // miSaveExcel
            // 
            miSaveExcel.Name = "miSaveExcel";
            miSaveExcel.Size = new Size(106, 20);
            miSaveExcel.Text = "Сохранить Excel";
            // 
            // miLoadGoogleCsv
            // 
            miLoadGoogleCsv.Name = "miLoadGoogleCsv";
            miLoadGoogleCsv.Size = new Size(138, 20);
            miLoadGoogleCsv.Text = "Загрузить Google CSV";
            // 
            // miExportCsv
            // 
            miExportCsv.Name = "miExportCsv";
            miExportCsv.Size = new Size(97, 20);
            miExportCsv.Text = "Экспорт в CSV";
            // 
            // miExit
            // 
            miExit.Name = "miExit";
            miExit.Size = new Size(53, 20);
            miExit.Text = "Выход";
            // 
            // cbBubble
            // 
            cbBubble.AutoSize = true;
            cbBubble.Location = new Point(18, 38);
            cbBubble.Margin = new Padding(3, 2, 3, 2);
            cbBubble.Name = "cbBubble";
            cbBubble.Size = new Size(99, 19);
            cbBubble.TabIndex = 22;
            cbBubble.Text = "Пузырьковая";
            // 
            // cbInsertion
            // 
            cbInsertion.AutoSize = true;
            cbInsertion.Location = new Point(18, 62);
            cbInsertion.Margin = new Padding(3, 2, 3, 2);
            cbInsertion.Name = "cbInsertion";
            cbInsertion.Size = new Size(76, 19);
            cbInsertion.TabIndex = 21;
            cbInsertion.Text = "Вставкой";
            // 
            // cbQuick
            // 
            cbQuick.AutoSize = true;
            cbQuick.Location = new Point(18, 86);
            cbQuick.Margin = new Padding(3, 2, 3, 2);
            cbQuick.Name = "cbQuick";
            cbQuick.Size = new Size(72, 19);
            cbQuick.TabIndex = 20;
            cbQuick.Text = "Быстрая";
            // 
            // cbShaker
            // 
            cbShaker.AutoSize = true;
            cbShaker.Location = new Point(18, 110);
            cbShaker.Margin = new Padding(3, 2, 3, 2);
            cbShaker.Name = "cbShaker";
            cbShaker.Size = new Size(88, 19);
            cbShaker.TabIndex = 19;
            cbShaker.Text = "Шейкерная";
            // 
            // cbBogo
            // 
            cbBogo.AutoSize = true;
            cbBogo.Location = new Point(18, 134);
            cbBogo.Margin = new Padding(3, 2, 3, 2);
            cbBogo.Name = "cbBogo";
            cbBogo.Size = new Size(54, 19);
            cbBogo.TabIndex = 18;
            cbBogo.Text = "Bogo";
            // 
            // rbAsc
            // 
            rbAsc.AutoSize = true;
            rbAsc.Location = new Point(18, 165);
            rbAsc.Margin = new Padding(3, 2, 3, 2);
            rbAsc.Name = "rbAsc";
            rbAsc.Size = new Size(116, 19);
            rbAsc.TabIndex = 17;
            rbAsc.Text = "По возрастанию";
            // 
            // rbDesc
            // 
            rbDesc.AutoSize = true;
            rbDesc.Location = new Point(18, 188);
            rbDesc.Margin = new Padding(3, 2, 3, 2);
            rbDesc.Name = "rbDesc";
            rbDesc.Size = new Size(102, 19);
            rbDesc.TabIndex = 16;
            rbDesc.Text = "По убыванию";
            // 
            // lblN
            // 
            lblN.AutoSize = true;
            lblN.Location = new Point(858, 62);
            lblN.Name = "lblN";
            lblN.Size = new Size(19, 15);
            lblN.TabIndex = 13;
            lblN.Text = "N:";
            // 
            // nudN
            // 
            nudN.Location = new Point(884, 60);
            nudN.Margin = new Padding(3, 2, 3, 2);
            nudN.Maximum = new decimal(new int[] { 2000, 0, 0, 0 });
            nudN.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudN.Name = "nudN";
            nudN.Size = new Size(61, 23);
            nudN.TabIndex = 12;
            nudN.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblMin
            // 
            lblMin.AutoSize = true;
            lblMin.Location = new Point(954, 62);
            lblMin.Name = "lblMin";
            lblMin.Size = new Size(31, 15);
            lblMin.TabIndex = 11;
            lblMin.Text = "Min:";
            // 
            // nudMin
            // 
            nudMin.Location = new Point(989, 60);
            nudMin.Margin = new Padding(3, 2, 3, 2);
            nudMin.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMin.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            nudMin.Name = "nudMin";
            nudMin.Size = new Size(61, 23);
            nudMin.TabIndex = 10;
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Location = new Point(858, 87);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(32, 15);
            lblMax.TabIndex = 9;
            lblMax.Text = "Max:";
            // 
            // nudMax
            // 
            nudMax.Location = new Point(884, 86);
            nudMax.Margin = new Padding(3, 2, 3, 2);
            nudMax.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMax.Minimum = new decimal(new int[] { 100000, 0, 0, int.MinValue });
            nudMax.Name = "nudMax";
            nudMax.Size = new Size(61, 23);
            nudMax.TabIndex = 8;
            // 
            // lblGoogle
            // 
            lblGoogle.AutoSize = true;
            lblGoogle.Location = new Point(858, 112);
            lblGoogle.Name = "lblGoogle";
            lblGoogle.Size = new Size(115, 15);
            lblGoogle.TabIndex = 7;
            lblGoogle.Text = "Google CSV ссылка:";
            // 
            // txtGoogleCsv
            // 
            txtGoogleCsv.Location = new Point(858, 130);
            txtGoogleCsv.Margin = new Padding(3, 2, 3, 2);
            txtGoogleCsv.Name = "txtGoogleCsv";
            txtGoogleCsv.Size = new Size(246, 23);
            txtGoogleCsv.TabIndex = 6;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.Columns.AddRange(new DataGridViewColumn[] { colEnabled, colAlg, colIter, colTime, colResult });
            dgvResults.Location = new Point(149, 30);
            dgvResults.Margin = new Padding(3, 2, 3, 2);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.Size = new Size(700, 195);
            dgvResults.TabIndex = 5;
            // 
            // colEnabled
            // 
            colEnabled.HeaderText = "";
            colEnabled.Name = "colEnabled";
            colEnabled.ReadOnly = true;
            // 
            // colAlg
            // 
            colAlg.HeaderText = "Тип сортировки";
            colAlg.Name = "colAlg";
            colAlg.ReadOnly = true;
            // 
            // colIter
            // 
            colIter.HeaderText = "Среднее количество итераций";
            colIter.Name = "colIter";
            colIter.ReadOnly = true;
            // 
            // colTime
            // 
            colTime.HeaderText = "Среднее время выполнения ms";
            colTime.Name = "colTime";
            colTime.ReadOnly = true;
            // 
            // colResult
            // 
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            colResult.DefaultCellStyle = dataGridViewCellStyle2;
            colResult.HeaderText = "Результат";
            colResult.Name = "colResult";
            colResult.ReadOnly = true;
            // 
            // lblInput
            // 
            lblInput.AutoSize = true;
            lblInput.Location = new Point(18, 232);
            lblInput.Name = "lblInput";
            lblInput.Size = new Size(119, 15);
            lblInput.TabIndex = 4;
            lblInput.Text = "Ввод (через пробел)";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(18, 251);
            txtInput.Margin = new Padding(3, 2, 3, 2);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.ScrollBars = ScrollBars.Vertical;
            txtInput.Size = new Size(666, 84);
            txtInput.TabIndex = 3;
            // 
            // btnParseToGrid
            // 
            btnParseToGrid.Location = new Point(700, 251);
            btnParseToGrid.Margin = new Padding(3, 2, 3, 2);
            btnParseToGrid.Name = "btnParseToGrid";
            btnParseToGrid.Size = new Size(149, 24);
            btnParseToGrid.TabIndex = 2;
            btnParseToGrid.Text = "Вставить в таблицу";
            btnParseToGrid.UseVisualStyleBackColor = true;
            // 
            // lblGrid
            // 
            lblGrid.AutoSize = true;
            lblGrid.Location = new Point(18, 345);
            lblGrid.Name = "lblGrid";
            lblGrid.Size = new Size(181, 15);
            lblGrid.TabIndex = 1;
            lblGrid.Text = "Табличный ввод (DataGridView)";
            // 
            // dgvInput
            // 
            dgvInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInput.Columns.AddRange(new DataGridViewColumn[] { colValue });
            dgvInput.Location = new Point(18, 364);
            dgvInput.Margin = new Padding(3, 2, 3, 2);
            dgvInput.Name = "dgvInput";
            dgvInput.RowHeadersVisible = false;
            dgvInput.Size = new Size(262, 195);
            dgvInput.TabIndex = 0;
            // 
            // colValue
            // 
            colValue.HeaderText = "Value";
            colValue.Name = "colValue";
            // 
            // lblTests
            // 
            lblTests.AutoSize = true;
            lblTests.Location = new Point(858, 34);
            lblTests.Name = "lblTests";
            lblTests.Size = new Size(113, 15);
            lblTests.TabIndex = 15;
            lblTests.Text = "Количество тестов:";
            // 
            // nudTests
            // 
            nudTests.Location = new Point(971, 33);
            nudTests.Margin = new Padding(3, 2, 3, 2);
            nudTests.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudTests.Name = "nudTests";
            nudTests.Size = new Size(61, 23);
            nudTests.TabIndex = 14;
            nudTests.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Lab5_SortingVisualizerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1120, 578);
            Controls.Add(dgvInput);
            Controls.Add(lblGrid);
            Controls.Add(btnParseToGrid);
            Controls.Add(txtInput);
            Controls.Add(lblInput);
            Controls.Add(dgvResults);
            Controls.Add(txtGoogleCsv);
            Controls.Add(lblGoogle);
            Controls.Add(nudMax);
            Controls.Add(lblMax);
            Controls.Add(nudMin);
            Controls.Add(lblMin);
            Controls.Add(nudN);
            Controls.Add(lblN);
            Controls.Add(nudTests);
            Controls.Add(lblTests);
            Controls.Add(rbDesc);
            Controls.Add(rbAsc);
            Controls.Add(cbBogo);
            Controls.Add(cbShaker);
            Controls.Add(cbQuick);
            Controls.Add(cbInsertion);
            Controls.Add(cbBubble);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Lab5_SortingVisualizerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ЛР5: Сортировки (табличный бенчмарк)";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudN).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInput).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTests).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miRun;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miGenerate;
        private System.Windows.Forms.ToolStripMenuItem miLoadExcel;
        private System.Windows.Forms.ToolStripMenuItem miSaveExcel;
        private System.Windows.Forms.ToolStripMenuItem miLoadGoogleCsv;
        private System.Windows.Forms.ToolStripMenuItem miExportCsv;
        private System.Windows.Forms.ToolStripMenuItem miExit;

        private System.Windows.Forms.CheckBox cbBubble;
        private System.Windows.Forms.CheckBox cbInsertion;
        private System.Windows.Forms.CheckBox cbQuick;
        private System.Windows.Forms.CheckBox cbShaker;
        private System.Windows.Forms.CheckBox cbBogo;

        private System.Windows.Forms.RadioButton rbAsc;
        private System.Windows.Forms.RadioButton rbDesc;

        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.NumericUpDown nudN;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.NumericUpDown nudMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.NumericUpDown nudMax;

        private System.Windows.Forms.Label lblGoogle;
        private System.Windows.Forms.TextBox txtGoogleCsv;

        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAlg;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIter;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResult;

        private System.Windows.Forms.Label lblInput;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnParseToGrid;

        private System.Windows.Forms.Label lblGrid;
        private System.Windows.Forms.DataGridView dgvInput;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValue;
        private Label lblTests;
        private NumericUpDown nudTests;
    }
}
