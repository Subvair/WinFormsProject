namespace MinFinderWinForms
{
    partial class Lab7_CoordinateDescentForm
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

            this.lblF = new System.Windows.Forms.Label();
            this.txtF = new System.Windows.Forms.TextBox();

            this.lblX0 = new System.Windows.Forms.Label();
            this.txtX0 = new System.Windows.Forms.TextBox();
            this.lblY0 = new System.Windows.Forms.Label();
            this.txtY0 = new System.Windows.Forms.TextBox();

            this.lblE = new System.Windows.Forms.Label();
            this.txtE = new System.Windows.Forms.TextBox();

            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblXmin = new System.Windows.Forms.Label();
            this.txtXmin = new System.Windows.Forms.TextBox();
            this.lblXmax = new System.Windows.Forms.Label();
            this.txtXmax = new System.Windows.Forms.TextBox();
            this.lblYmin = new System.Windows.Forms.Label();
            this.txtYmin = new System.Windows.Forms.TextBox();
            this.lblYmax = new System.Windows.Forms.Label();
            this.txtYmax = new System.Windows.Forms.TextBox();

            this.lblStepsTitle = new System.Windows.Forms.Label();
            this.lstSteps = new System.Windows.Forms.ListBox();

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

            // splitContainer
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(1356, 782);
            this.splitContainer1.SplitterDistance = 430;

            this.splitContainer1.Panel2.Controls.Add(this.chart1);

            // chart
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(922, 782);

            // left panel controls
            this.splitContainer1.Panel1.Controls.Add(this.lblF);
            this.splitContainer1.Panel1.Controls.Add(this.txtF);

            this.splitContainer1.Panel1.Controls.Add(this.lblX0);
            this.splitContainer1.Panel1.Controls.Add(this.txtX0);
            this.splitContainer1.Panel1.Controls.Add(this.lblY0);
            this.splitContainer1.Panel1.Controls.Add(this.txtY0);

            this.splitContainer1.Panel1.Controls.Add(this.lblE);
            this.splitContainer1.Panel1.Controls.Add(this.txtE);

            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);

            this.splitContainer1.Panel1.Controls.Add(this.lblStepsTitle);
            this.splitContainer1.Panel1.Controls.Add(this.lstSteps);

            this.splitContainer1.Panel1.Controls.Add(this.lblResult);
            this.splitContainer1.Panel1.Controls.Add(this.lblInfo);

            // f
            this.lblF.AutoSize = true;
            this.lblF.Location = new System.Drawing.Point(14, 14);
            this.lblF.Name = "lblF";
            this.lblF.Size = new System.Drawing.Size(49, 20);
            this.lblF.Text = "f(x,y):";

            this.txtF.Location = new System.Drawing.Point(14, 38);
            this.txtF.Name = "txtF";
            this.txtF.Size = new System.Drawing.Size(400, 27);

            // x0 y0
            this.lblX0.AutoSize = true;
            this.lblX0.Location = new System.Drawing.Point(14, 76);
            this.lblX0.Name = "lblX0";
            this.lblX0.Size = new System.Drawing.Size(26, 20);
            this.lblX0.Text = "x0";

            this.txtX0.Location = new System.Drawing.Point(14, 100);
            this.txtX0.Name = "txtX0";
            this.txtX0.Size = new System.Drawing.Size(120, 27);

            this.lblY0.AutoSize = true;
            this.lblY0.Location = new System.Drawing.Point(154, 76);
            this.lblY0.Name = "lblY0";
            this.lblY0.Size = new System.Drawing.Size(26, 20);
            this.lblY0.Text = "y0";

            this.txtY0.Location = new System.Drawing.Point(154, 100);
            this.txtY0.Name = "txtY0";
            this.txtY0.Size = new System.Drawing.Size(120, 27);

            // e
            this.lblE.AutoSize = true;
            this.lblE.Location = new System.Drawing.Point(294, 76);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(17, 20);
            this.lblE.Text = "e";

            this.txtE.Location = new System.Drawing.Point(294, 100);
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(120, 27);

            // groupBox bounds
            this.groupBox1.Location = new System.Drawing.Point(14, 145);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 110);
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Область графика (границы поиска)";

            this.lblXmin.AutoSize = true;
            this.lblXmin.Location = new System.Drawing.Point(14, 28);
            this.lblXmin.Text = "xmin";
            this.txtXmin.Location = new System.Drawing.Point(60, 26);
            this.txtXmin.Size = new System.Drawing.Size(90, 27);

            this.lblXmax.AutoSize = true;
            this.lblXmax.Location = new System.Drawing.Point(170, 28);
            this.lblXmax.Text = "xmax";
            this.txtXmax.Location = new System.Drawing.Point(214, 26);
            this.txtXmax.Size = new System.Drawing.Size(90, 27);

            this.lblYmin.AutoSize = true;
            this.lblYmin.Location = new System.Drawing.Point(14, 66);
            this.lblYmin.Text = "ymin";
            this.txtYmin.Location = new System.Drawing.Point(60, 64);
            this.txtYmin.Size = new System.Drawing.Size(90, 27);

            this.lblYmax.AutoSize = true;
            this.lblYmax.Location = new System.Drawing.Point(170, 66);
            this.lblYmax.Text = "ymax";
            this.txtYmax.Location = new System.Drawing.Point(214, 64);
            this.txtYmax.Size = new System.Drawing.Size(90, 27);

            this.groupBox1.Controls.Add(this.lblXmin);
            this.groupBox1.Controls.Add(this.txtXmin);
            this.groupBox1.Controls.Add(this.lblXmax);
            this.groupBox1.Controls.Add(this.txtXmax);
            this.groupBox1.Controls.Add(this.lblYmin);
            this.groupBox1.Controls.Add(this.txtYmin);
            this.groupBox1.Controls.Add(this.lblYmax);
            this.groupBox1.Controls.Add(this.txtYmax);

            // steps
            this.lblStepsTitle.AutoSize = true;
            this.lblStepsTitle.Location = new System.Drawing.Point(14, 270);
            this.lblStepsTitle.Text = "История шагов (k, x, y, f):";

            this.lstSteps.Location = new System.Drawing.Point(14, 295);
            this.lstSteps.Name = "lstSteps";
            this.lstSteps.Size = new System.Drawing.Size(400, 184);

            // result textbox
            this.lblResult.Location = new System.Drawing.Point(14, 490);
            this.lblResult.Multiline = true;
            this.lblResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblResult.ReadOnly = true;
            this.lblResult.Size = new System.Drawing.Size(400, 210);
            this.lblResult.Text = "—";

            // info
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Location = new System.Drawing.Point(14, 710);
            this.lblInfo.Size = new System.Drawing.Size(400, 60);
            this.lblInfo.Padding = new System.Windows.Forms.Padding(6);
            this.lblInfo.Text = "Готово.";

            // form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1380, 830);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lab7_CoordinateDescentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР7: Метод покоординатного спуска (Golden Search по координатам)";

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

        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.TextBox txtF;

        private System.Windows.Forms.Label lblX0;
        private System.Windows.Forms.TextBox txtX0;
        private System.Windows.Forms.Label lblY0;
        private System.Windows.Forms.TextBox txtY0;

        private System.Windows.Forms.Label lblE;
        private System.Windows.Forms.TextBox txtE;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblXmin;
        private System.Windows.Forms.TextBox txtXmin;
        private System.Windows.Forms.Label lblXmax;
        private System.Windows.Forms.TextBox txtXmax;
        private System.Windows.Forms.Label lblYmin;
        private System.Windows.Forms.TextBox txtYmin;
        private System.Windows.Forms.Label lblYmax;
        private System.Windows.Forms.TextBox txtYmax;

        private System.Windows.Forms.Label lblStepsTitle;
        private System.Windows.Forms.ListBox lstSteps;

        private System.Windows.Forms.TextBox lblResult;
        private System.Windows.Forms.Label lblInfo;
    }
}
