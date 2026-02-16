namespace MinFinderWinForms
{
    partial class Lab4_ConvergenceForm
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
            this.miClear = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();

            this.lblA = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.lblE = new System.Windows.Forms.Label();
            this.lblFx = new System.Windows.Forms.Label();

            this.txtA = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtE = new System.Windows.Forms.TextBox();
            this.txtFx = new System.Windows.Forms.TextBox();

            this.lblInfo = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();

            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCalc,
            this.miStep,
            this.miClear,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1280, 28);
            this.menuStrip1.TabIndex = 0;

            // miCalc
            this.miCalc.Name = "miCalc";
            this.miCalc.Size = new System.Drawing.Size(96, 24);
            this.miCalc.Text = "Рассчитать";

            // miStep
            this.miStep.Name = "miStep";
            this.miStep.Size = new System.Drawing.Size(48, 24);
            this.miStep.Text = "Шаг";

            // miClear
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(83, 24);
            this.miClear.Text = "Очистить";

            // miExit
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(66, 24);
            this.miExit.Text = "Выход";

            // lblA
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(14, 42);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(23, 20);
            this.lblA.TabIndex = 1;
            this.lblA.Text = "a:";

            // lblB
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(14, 78);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(23, 20);
            this.lblB.TabIndex = 2;
            this.lblB.Text = "b:";

            // lblE
            this.lblE.AutoSize = true;
            this.lblE.Location = new System.Drawing.Point(14, 114);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(23, 20);
            this.lblE.TabIndex = 3;
            this.lblE.Text = "e:";

            // lblFx
            this.lblFx.AutoSize = true;
            this.lblFx.Location = new System.Drawing.Point(14, 150);
            this.lblFx.Name = "lblFx";
            this.lblFx.Size = new System.Drawing.Size(39, 20);
            this.lblFx.TabIndex = 4;
            this.lblFx.Text = "f(x):";

            // txtA
            this.txtA.Location = new System.Drawing.Point(60, 39);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(200, 27);
            this.txtA.TabIndex = 5;

            // txtB
            this.txtB.Location = new System.Drawing.Point(60, 75);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(200, 27);
            this.txtB.TabIndex = 6;

            // txtE
            this.txtE.Location = new System.Drawing.Point(60, 111);
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(200, 27);
            this.txtE.TabIndex = 7;

            // txtFx
            this.txtFx.Location = new System.Drawing.Point(60, 147);
            this.txtFx.Name = "txtFx";
            this.txtFx.Size = new System.Drawing.Size(560, 27);
            this.txtFx.TabIndex = 8;

            // lblInfo
            this.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInfo.Location = new System.Drawing.Point(650, 39);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(300, 71);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.Padding = new System.Windows.Forms.Padding(6);

            // lblStatus
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStatus.Location = new System.Drawing.Point(650, 117);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(300, 57);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(6);
            this.lblStatus.Text = "Статус: ожидание";

            // chart1
            this.chart1.Location = new System.Drawing.Point(18, 190);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(1240, 560);
            this.chart1.TabIndex = 11;

            // Lab4_ConvergenceForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 770);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtFx);
            this.Controls.Add(this.txtE);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.txtA);
            this.Controls.Add(this.lblFx);
            this.Controls.Add(this.lblE);
            this.Controls.Add(this.lblB);
            this.Controls.Add(this.lblA);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Lab4_ConvergenceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР4: Метод Ньютона (пошагово)";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miCalc;
        private System.Windows.Forms.ToolStripMenuItem miStep;
        private System.Windows.Forms.ToolStripMenuItem miClear;
        private System.Windows.Forms.ToolStripMenuItem miExit;

        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblE;
        private System.Windows.Forms.Label lblFx;

        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtE;
        private System.Windows.Forms.TextBox txtFx;

        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblStatus;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
