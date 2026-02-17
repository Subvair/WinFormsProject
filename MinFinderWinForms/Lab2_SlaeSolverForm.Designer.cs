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

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miLoadExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.miGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.miAddRow = new System.Windows.Forms.ToolStripMenuItem();
            this.miRemoveRow = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();

            this.panelTop = new System.Windows.Forms.Panel();
            this.btnSolve = new System.Windows.Forms.Button();
            this.cbGauss = new System.Windows.Forms.CheckBox();
            this.cbJordan = new System.Windows.Forms.CheckBox();
            this.cbCramer = new System.Windows.Forms.CheckBox();

            this.dgv = new System.Windows.Forms.DataGridView();

            this.tableBottom = new System.Windows.Forms.TableLayoutPanel();
            this.txtGauss = new System.Windows.Forms.TextBox();
            this.txtJordan = new System.Windows.Forms.TextBox();
            this.txtCramer = new System.Windows.Forms.TextBox();

            this.lblInfo = new System.Windows.Forms.Label();

            this.menuStrip1.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.tableBottom.SuspendLayout();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miClear,
            this.miLoadExcel,
            this.miGenerate,
            this.miAddRow,
            this.miRemoveRow,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 28);

            this.miClear.Text = "Очистить";
            this.miLoadExcel.Text = "Загрузить из Excel";
            this.miGenerate.Text = "Сгенерировать";
            this.miAddRow.Text = "+";
            this.miRemoveRow.Text = "-";
            this.miExit.Text = "Выход";

            // panelTop
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 42;
            this.panelTop.Padding = new System.Windows.Forms.Padding(10, 6, 10, 6);

            this.btnSolve.Text = "Рассчитать";
            this.btnSolve.Width = 110;
            this.btnSolve.Height = 28;
            this.btnSolve.Left = 10;
            this.btnSolve.Top = 7;

            this.cbGauss.Text = "Методом Гаусса";
            this.cbGauss.AutoSize = true;
            this.cbGauss.Left = 140;
            this.cbGauss.Top = 10;
            this.cbGauss.Checked = true;

            this.cbJordan.Text = "Методом Гаусса-Жордана";
            this.cbJordan.AutoSize = true;
            this.cbJordan.Left = 300;
            this.cbJordan.Top = 10;
            this.cbJordan.Checked = true;

            this.cbCramer.Text = "Методом Крамера";
            this.cbCramer.AutoSize = true;
            this.cbCramer.Left = 535;
            this.cbCramer.Top = 10;
            this.cbCramer.Checked = true;

            this.panelTop.Controls.Add(this.btnSolve);
            this.panelTop.Controls.Add(this.cbGauss);
            this.panelTop.Controls.Add(this.cbJordan);
            this.panelTop.Controls.Add(this.cbCramer);

            // dgv
            this.dgv.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgv.Height = 260;
            this.dgv.BackgroundColor = System.Drawing.Color.White;

            // bottom table
            this.tableBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableBottom.ColumnCount = 3;
            this.tableBottom.RowCount = 1;
            this.tableBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333f));
            this.tableBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333f));
            this.tableBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333f));

            this.txtGauss.Multiline = true;
            this.txtGauss.ReadOnly = true;
            this.txtGauss.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGauss.Dock = System.Windows.Forms.DockStyle.Fill;

            this.txtJordan.Multiline = true;
            this.txtJordan.ReadOnly = true;
            this.txtJordan.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJordan.Dock = System.Windows.Forms.DockStyle.Fill;

            this.txtCramer.Multiline = true;
            this.txtCramer.ReadOnly = true;
            this.txtCramer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCramer.Dock = System.Windows.Forms.DockStyle.Fill;

            this.tableBottom.Controls.Add(this.txtGauss, 0, 0);
            this.tableBottom.Controls.Add(this.txtJordan, 1, 0);
            this.tableBottom.Controls.Add(this.txtCramer, 2, 0);

            // lblInfo
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Height = 28;
            this.lblInfo.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Text = "Готово.";

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 560);
            this.Controls.Add(this.tableBottom);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lab2_SlaeSolverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР2: Решение СЛАУ (A·X + B = 0)";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.tableBottom.ResumeLayout(false);
            this.tableBottom.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miLoadExcel;
        private System.Windows.Forms.ToolStripMenuItem miGenerate;
        private System.Windows.Forms.ToolStripMenuItem miAddRow;
        private System.Windows.Forms.ToolStripMenuItem miRemoveRow;
        private System.Windows.Forms.ToolStripMenuItem miExit;

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnSolve;
        private System.Windows.Forms.CheckBox cbGauss;
        private System.Windows.Forms.CheckBox cbJordan;
        private System.Windows.Forms.CheckBox cbCramer;

        private System.Windows.Forms.DataGridView dgv;

        private System.Windows.Forms.TableLayoutPanel tableBottom;
        private System.Windows.Forms.TextBox txtGauss;
        private System.Windows.Forms.TextBox txtJordan;
        private System.Windows.Forms.TextBox txtCramer;

        private System.Windows.Forms.Label lblInfo;
    }
}
