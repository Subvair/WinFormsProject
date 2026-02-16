namespace MinFinderWinForms
{
    partial class Lab6_IntegrationForm
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
            this.miStep = new System.Windows.Forms.ToolStripMenuItem();
            this.miGen = new System.Windows.Forms.ToolStripMenuItem();
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();

            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.lblFx = new System.Windows.Forms.Label();
            this.txtFx = new System.Windows.Forms.TextBox();
            this.lblA = new System.Windows.Forms.Label();
            this.txtA = new System.Windows.Forms.TextBox();
            this.lblB = new System.Windows.Forms.Label();
            this.txtB = new System.Windows.Forms.TextBox();
            this.lblE = new System.Windows.Forms.Label();
            this.txtE = new System.Windows.Forms.TextBox();

            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRect = new System.Windows.Forms.CheckBox();
            this.cbTrap = new System.Windows.Forms.CheckBox();
            this.cbSimp = new System.Windows.Forms.CheckBox();

            this.lblHist = new System.Windows.Forms.Label();
            this.cmbHistory = new System.Windows.Forms.ComboBox();

            this.lblResultTitle = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.TextBox();

            this.lblInfo = new System.Windows.Forms.Label();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCalc,
            this.miStep,
            this.miGen,
            this.miClear,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1380, 28);

            this.miCalc.Name = "miCalc";
            this.miCalc.Size = new System.Drawing.Size(96, 24);
            this.miCalc.Text = "Рассчитать";

            this.miStep.Name = "miStep";
            this.miStep.Size = new System.Drawing.Size(49, 24);
            this.miStep.Text = "Шаг";

            this.miGen.Name = "miGen";
            this.miGen.Size = new System.Drawing.Size(101, 24);
            this.miGen.Text = "Генерация";

            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(83, 24);
            this.miClear.Text = "Очистить";

            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(66, 24);
            this.miExit.Text = "Выход";

            // splitContainer1
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1356, 782);
            this.splitContainer1.SplitterDistance = 420;

            // Panel1 controls
            this.splitContainer1.Panel1.Controls.Add(this.lblFx);
            this.splitContainer1.Panel1.Controls.Add(this.txtFx);
            this.splitContainer1.Panel1.Controls.Add(this.lblA);
            this.splitContainer1.Panel1.Controls.Add(this.txtA);
            this.splitContainer1.Panel1.Controls.Add(this.lblB);
            this.splitContainer1.Panel1.Controls.Add(this.txtB);
            this.splitContainer1.Panel1.Controls.Add(this.lblE);
            this.splitContainer1.Panel1.Controls.Add(this.txtE);

            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);

            this.splitContainer1.Panel1.Controls.Add(this.lblHist);
            this.splitContainer1.Panel1.Controls.Add(this.cmbHistory);

            this.splitContainer1.Panel1.Controls.Add(this.lblResultTitle);
            this.splitContainer1.Panel1.Controls.Add(this.lblResult);

            this.splitContainer1.Panel1.Controls.Add(this.lblInfo);

            // Panel2 chart
            this.splitContainer1.Panel2.Controls.Add(this.chart1);

            // chart1
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(932, 782);
            this.chart1.TabIndex = 0;

            // labels/textboxes
            this.lblFx.AutoSize = true;
            this.lblFx.Location = new System.Drawing.Point(14, 14);
            this.lblFx.Name = "lblFx";
            this.lblFx.Size = new System.Drawing.Size(39, 20);
            this.lblFx.Text = "f(x):";

            this.txtFx.Location = new System.Drawing.Point(14, 38);
            this.txtFx.Name = "txtFx";
            this.txtFx.Size = new System.Drawing.Size(392, 27);

            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(14, 76);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(19, 20);
            this.lblA.Text = "a";

            this.txtA.Location = new System.Drawing.Point(14, 100);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(120, 27);

            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(154, 76);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(19, 20);
            this.lblB.Text = "b";

            this.txtB.Location = new System.Drawing.Point(154, 100);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(120, 27);

            this.lblE.AutoSize = true;
            this.lblE.Location = new System.Drawing.Point(294, 76);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(17, 20);
            this.lblE.Text = "e";

            this.txtE.Location = new System.Drawing.Point(294, 100);
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(112, 27);

            // groupBox1
            this.groupBox1.Location = new System.Drawing.Point(14, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 110);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Методы (можно несколько)";

            this.cbRect.AutoSize = true;
            this.cbRect.Location = new System.Drawing.Point(14, 28);
            this.cbRect.Name = "cbRect";
            this.cbRect.Size = new System.Drawing.Size(197, 24);
            this.cbRect.Text = "Прямоугольники (mid)";

            this.cbTrap.AutoSize = true;
            this.cbTrap.Location = new System.Drawing.Point(14, 56);
            this.cbTrap.Name = "cbTrap";
            this.cbTrap.Size = new System.Drawing.Size(92, 24);
            this.cbTrap.Text = "Трапеции";

            this.cbSimp.AutoSize = true;
            this.cbSimp.Location = new System.Drawing.Point(14, 84);
            this.cbSimp.Name = "cbSimp";
            this.cbSimp.Size = new System.Drawing.Size(86, 24);
            this.cbSimp.Text = "Симпсон";

            this.groupBox1.Controls.Add(this.cbRect);
            this.groupBox1.Controls.Add(this.cbTrap);
            this.groupBox1.Controls.Add(this.cbSimp);

            // history method
            this.lblHist.AutoSize = true;
            this.lblHist.Location = new System.Drawing.Point(14, 270);
            this.lblHist.Name = "lblHist";
            this.lblHist.Size = new System.Drawing.Size(221, 20);
            this.lblHist.Text = "Просмотр истории разбиений:";

            this.cmbHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHistory.Location = new System.Drawing.Point(14, 295);
            this.cmbHistory.Name = "cmbHistory";
            this.cmbHistory.Size = new System.Drawing.Size(392, 28);

            // result
            this.lblResultTitle.AutoSize = true;
            this.lblResultTitle.Location = new System.Drawing.Point(14, 338);
            this.lblResultTitle.Name = "lblResultTitle";
            this.lblResultTitle.Size = new System.Drawing.Size(85, 20);
            this.lblResultTitle.Text = "Результаты:";

            this.lblResult.Location = new System.Drawing.Point(14, 362);
            this.lblResult.Multiline = true;
            this.lblResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblResult.ReadOnly = true;
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(392, 320);
            this.lblResult.Text = "—";

            // info
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Location = new System.Drawing.Point(14, 700);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(392, 70);
            this.lblInfo.Padding = new System.Windows.Forms.Padding(6);
            this.lblInfo.Text = "Готово.";

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 830);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lab6_IntegrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР6: Численное интегрирование + история разбиений";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miCalc;
        private System.Windows.Forms.ToolStripMenuItem miStep;
        private System.Windows.Forms.ToolStripMenuItem miGen;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miExit;

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;

        private System.Windows.Forms.Label lblFx;
        private System.Windows.Forms.TextBox txtFx;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Label lblE;
        private System.Windows.Forms.TextBox txtE;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbRect;
        private System.Windows.Forms.CheckBox cbTrap;
        private System.Windows.Forms.CheckBox cbSimp;

        private System.Windows.Forms.Label lblHist;
        private System.Windows.Forms.ComboBox cmbHistory;

        private System.Windows.Forms.Label lblResultTitle;
        private System.Windows.Forms.TextBox lblResult;

        private System.Windows.Forms.Label lblInfo;
    }
}
