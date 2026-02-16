namespace MinFinderWinForms
{
    partial class Lab2_SlaeSolverForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miCalc = new System.Windows.Forms.ToolStripMenuItem();
            this.miGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportGoogleCsv = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportCsv = new System.Windows.Forms.ToolStripMenuItem();

            this.lblN = new System.Windows.Forms.Label();
            this.nudN = new System.Windows.Forms.NumericUpDown();

            this.gbMethod = new System.Windows.Forms.GroupBox();
            this.rbGauss = new System.Windows.Forms.RadioButton();
            this.rbJordan = new System.Windows.Forms.RadioButton();
            this.rbCramer = new System.Windows.Forms.RadioButton();

            this.lblGoogle = new System.Windows.Forms.Label();
            this.txtGoogleUrl = new System.Windows.Forms.TextBox();

            this.dgvA = new System.Windows.Forms.DataGridView();
            this.dgvB = new System.Windows.Forms.DataGridView();
            this.dgvX = new System.Windows.Forms.DataGridView();

            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();

            this.lblStatus = new System.Windows.Forms.Label();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudN)).BeginInit();
            this.gbMethod.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvX)).BeginInit();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCalc,
            this.miGenerate,
            this.miClear,
            this.miImportExcel,
            this.miExportExcel,
            this.miImportGoogleCsv,
            this.miExportCsv});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 28);
            this.menuStrip1.TabIndex = 0;

            // miCalc
            this.miCalc.Name = "miCalc";
            this.miCalc.Size = new System.Drawing.Size(96, 24);
            this.miCalc.Text = "Рассчитать";

            // miGenerate
            this.miGenerate.Name = "miGenerate";
            this.miGenerate.Size = new System.Drawing.Size(108, 24);
            this.miGenerate.Text = "Сгенерировать";

            // miClear
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(83, 24);
            this.miClear.Text = "Очистить";

            // miImportExcel
            this.miImportExcel.Name = "miImportExcel";
            this.miImportExcel.Size = new System.Drawing.Size(126, 24);
            this.miImportExcel.Text = "Импорт Excel AB";

            // miExportExcel
            this.miExportExcel.Name = "miExportExcel";
            this.miExportExcel.Size = new System.Drawing.Size(128, 24);
            this.miExportExcel.Text = "Экспорт Excel A,B,X";

            // miImportGoogleCsv
            this.miImportGoogleCsv.Name = "miImportGoogleCsv";
            this.miImportGoogleCsv.Size = new System.Drawing.Size(144, 24);
            this.miImportGoogleCsv.Text = "Импорт Google CSV";

            // miExportCsv
            this.miExportCsv.Name = "miExportCsv";
            this.miExportCsv.Size = new System.Drawing.Size(97, 24);
            this.miExportCsv.Text = "Экспорт CSV";

            // lblN
            this.lblN.AutoSize = true;
            this.lblN.Location = new System.Drawing.Point(14, 40);
            this.lblN.Name = "lblN";
            this.lblN.Size = new System.Drawing.Size(74, 20);
            this.lblN.TabIndex = 1;
            this.lblN.Text = "Размер N:";

            // nudN
            this.nudN.Location = new System.Drawing.Point(94, 38);
            this.nudN.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            this.nudN.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            this.nudN.Name = "nudN";
            this.nudN.Size = new System.Drawing.Size(90, 27);
            this.nudN.TabIndex = 2;
            this.nudN.ValueChanged += new System.EventHandler(this.nudN_ValueChanged);

            // gbMethod
            this.gbMethod.Controls.Add(this.rbCramer);
            this.gbMethod.Controls.Add(this.rbJordan);
            this.gbMethod.Controls.Add(this.rbGauss);
            this.gbMethod.Location = new System.Drawing.Point(210, 32);
            this.gbMethod.Name = "gbMethod";
            this.gbMethod.Size = new System.Drawing.Size(420, 44);
            this.gbMethod.TabIndex = 3;
            this.gbMethod.TabStop = false;
            this.gbMethod.Text = "Метод";

            // rbGauss
            this.rbGauss.AutoSize = true;
            this.rbGauss.Location = new System.Drawing.Point(10, 17);
            this.rbGauss.Name = "rbGauss";
            this.rbGauss.Size = new System.Drawing.Size(72, 24);
            this.rbGauss.TabIndex = 0;
            this.rbGauss.TabStop = true;
            this.rbGauss.Text = "Гаусс";
            this.rbGauss.UseVisualStyleBackColor = true;

            // rbJordan
            this.rbJordan.AutoSize = true;
            this.rbJordan.Location = new System.Drawing.Point(120, 17);
            this.rbJordan.Name = "rbJordan";
            this.rbJordan.Size = new System.Drawing.Size(140, 24);
            this.rbJordan.TabIndex = 1;
            this.rbJordan.TabStop = true;
            this.rbJordan.Text = "Жордан-Гаусс";
            this.rbJordan.UseVisualStyleBackColor = true;

            // rbCramer
            this.rbCramer.AutoSize = true;
            this.rbCramer.Location = new System.Drawing.Point(290, 17);
            this.rbCramer.Name = "rbCramer";
            this.rbCramer.Size = new System.Drawing.Size(83, 24);
            this.rbCramer.TabIndex = 2;
            this.rbCramer.TabStop = true;
            this.rbCramer.Text = "Крамер";
            this.rbCramer.UseVisualStyleBackColor = true;

            // lblGoogle
            this.lblGoogle.AutoSize = true;
            this.lblGoogle.Location = new System.Drawing.Point(14, 86);
            this.lblGoogle.Name = "lblGoogle";
            this.lblGoogle.Size = new System.Drawing.Size(153, 20);
            this.lblGoogle.TabIndex = 4;
            this.lblGoogle.Text = "Google CSV ссылка:";

            // txtGoogleUrl
            this.txtGoogleUrl.Location = new System.Drawing.Point(172, 83);
            this.txtGoogleUrl.Name = "txtGoogleUrl";
            this.txtGoogleUrl.Size = new System.Drawing.Size(820, 27);
            this.txtGoogleUrl.TabIndex = 5;

            // labels A B X
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(14, 125);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(76, 20);
            this.lblA.TabIndex = 6;
            this.lblA.Text = "Матрица A";

            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(780, 125);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(58, 20);
            this.lblB.TabIndex = 7;
            this.lblB.Text = "Вектор B";

            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(1040, 125);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(58, 20);
            this.lblX.TabIndex = 8;
            this.lblX.Text = "Вектор X";

            // dgvA
            this.dgvA.Location = new System.Drawing.Point(18, 150);
            this.dgvA.Name = "dgvA";
            this.dgvA.Size = new System.Drawing.Size(740, 520);
            this.dgvA.TabIndex = 9;

            // dgvB
            this.dgvB.Location = new System.Drawing.Point(784, 150);
            this.dgvB.Name = "dgvB";
            this.dgvB.Size = new System.Drawing.Size(230, 520);
            this.dgvB.TabIndex = 10;

            // dgvX
            this.dgvX.Location = new System.Drawing.Point(1044, 150);
            this.dgvX.Name = "dgvX";
            this.dgvX.Size = new System.Drawing.Size(210, 520);
            this.dgvX.TabIndex = 11;
            this.dgvX.ReadOnly = false;

            // lblStatus
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(1010, 33);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(244, 77);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Статус: ожидание";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(6);

            // Lab2_SlaeSolverForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 700);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.dgvX);
            this.Controls.Add(this.dgvB);
            this.Controls.Add(this.dgvA);
            this.Controls.Add(this.txtGoogleUrl);
            this.Controls.Add(this.lblGoogle);
            this.Controls.Add(this.gbMethod);
            this.Controls.Add(this.nudN);
            this.Controls.Add(this.lblN);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lab2_SlaeSolverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР2: Решение СЛАУ A·X + B = 0";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudN)).EndInit();
            this.gbMethod.ResumeLayout(false);
            this.gbMethod.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miCalc;
        private System.Windows.Forms.ToolStripMenuItem miGenerate;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miImportExcel;
        private System.Windows.Forms.ToolStripMenuItem miExportExcel;
        private System.Windows.Forms.ToolStripMenuItem miImportGoogleCsv;
        private System.Windows.Forms.ToolStripMenuItem miExportCsv;

        private System.Windows.Forms.Label lblN;
        private System.Windows.Forms.NumericUpDown nudN;

        private System.Windows.Forms.GroupBox gbMethod;
        private System.Windows.Forms.RadioButton rbGauss;
        private System.Windows.Forms.RadioButton rbJordan;
        private System.Windows.Forms.RadioButton rbCramer;

        private System.Windows.Forms.Label lblGoogle;
        private System.Windows.Forms.TextBox txtGoogleUrl;

        private System.Windows.Forms.DataGridView dgvA;
        private System.Windows.Forms.DataGridView dgvB;
        private System.Windows.Forms.DataGridView dgvX;

        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblX;

        private System.Windows.Forms.Label lblStatus;
    }
}
