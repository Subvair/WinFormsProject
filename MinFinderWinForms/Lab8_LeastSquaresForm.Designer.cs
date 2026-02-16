namespace MinFinderWinForms
{
    partial class Lab8_LeastSquaresForm
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
            this.miGen = new System.Windows.Forms.ToolStripMenuItem();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miImportGoogle = new System.Windows.Forms.ToolStripMenuItem();
            this.miExportGoogle = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();

            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.txtOut = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lblLeftTitle = new System.Windows.Forms.Label();
            this.lblRightTitle = new System.Windows.Forms.Label();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCalc,
            this.miGen,
            this.miClear,
            this.miImportExcel,
            this.miExportExcel,
            this.miImportGoogle,
            this.miExportGoogle,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1380, 28);

            this.miCalc.Name = "miCalc";
            this.miCalc.Size = new System.Drawing.Size(96, 24);
            this.miCalc.Text = "Рассчитать";

            this.miGen.Name = "miGen";
            this.miGen.Size = new System.Drawing.Size(101, 24);
            this.miGen.Text = "Генерация";

            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(83, 24);
            this.miClear.Text = "Очистить";

            this.miImportExcel.Name = "miImportExcel";
            this.miImportExcel.Size = new System.Drawing.Size(122, 24);
            this.miImportExcel.Text = "Импорт Excel";

            this.miExportExcel.Name = "miExportExcel";
            this.miExportExcel.Size = new System.Drawing.Size(121, 24);
            this.miExportExcel.Text = "Экспорт Excel";

            this.miImportGoogle.Name = "miImportGoogle";
            this.miImportGoogle.Size = new System.Drawing.Size(153, 24);
            this.miImportGoogle.Text = "Импорт Google (Ctrl+C)";

            this.miExportGoogle.Name = "miExportGoogle";
            this.miExportGoogle.Size = new System.Drawing.Size(155, 24);
            this.miExportGoogle.Text = "Экспорт Google (в буфер)";

            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(66, 24);
            this.miExit.Text = "Выход";

            // splitContainer1
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1356, 782);
            this.splitContainer1.SplitterDistance = 520;

            // LEFT panel
            this.splitContainer1.Panel1.Controls.Add(this.lblLeftTitle);
            this.splitContainer1.Panel1.Controls.Add(this.dgv);
            this.splitContainer1.Panel1.Controls.Add(this.txtOut);
            this.splitContainer1.Panel1.Controls.Add(this.lblInfo);

            // RIGHT panel
            this.splitContainer1.Panel2.Controls.Add(this.lblRightTitle);
            this.splitContainer1.Panel2.Controls.Add(this.chart1);

            // lblLeftTitle
            this.lblLeftTitle.AutoSize = true;
            this.lblLeftTitle.Location = new System.Drawing.Point(10, 10);
            this.lblLeftTitle.Name = "lblLeftTitle";
            this.lblLeftTitle.Size = new System.Drawing.Size(265, 20);
            this.lblLeftTitle.Text = "Точки (ввод/импорт/генерация): x, y";

            // dgv
            this.dgv.Location = new System.Drawing.Point(10, 35);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(500, 360);
            this.dgv.TabIndex = 0;

            // txtOut
            this.txtOut.Location = new System.Drawing.Point(10, 410);
            this.txtOut.Multiline = true;
            this.txtOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOut.ReadOnly = true;
            this.txtOut.Name = "txtOut";
            this.txtOut.Size = new System.Drawing.Size(500, 300);
            this.txtOut.TabIndex = 1;
            this.txtOut.Text = "—";

            // lblInfo
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Location = new System.Drawing.Point(10, 720);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Padding = new System.Windows.Forms.Padding(6);
            this.lblInfo.Size = new System.Drawing.Size(500, 52);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "Готово.";

            // lblRightTitle
            this.lblRightTitle.AutoSize = true;
            this.lblRightTitle.Location = new System.Drawing.Point(10, 10);
            this.lblRightTitle.Name = "lblRightTitle";
            this.lblRightTitle.Size = new System.Drawing.Size(311, 20);
            this.lblRightTitle.Text = "График: точки + аппроксимации (n=1 и n=2)";

            // chart1
            this.chart1.Location = new System.Drawing.Point(10, 35);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(816, 737);
            this.chart1.TabIndex = 3;

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 830);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lab8_LeastSquaresForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР8: Метод наименьших квадратов (n=1, n=2)";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miCalc;
        private System.Windows.Forms.ToolStripMenuItem miGen;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miImportExcel;
        private System.Windows.Forms.ToolStripMenuItem miExportExcel;
        private System.Windows.Forms.ToolStripMenuItem miImportGoogle;
        private System.Windows.Forms.ToolStripMenuItem miExportGoogle;
        private System.Windows.Forms.ToolStripMenuItem miExit;

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.TextBox txtOut;
        private System.Windows.Forms.Label lblInfo;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label lblLeftTitle;
        private System.Windows.Forms.Label lblRightTitle;
    }
}
