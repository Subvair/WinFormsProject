namespace MinFinderWinForms
{
    partial class Lab1_MinFinderForm
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

            this.lblResult = new System.Windows.Forms.Label();

            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();

            // menuStrip1
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCalc,
            this.miClear,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1100, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";

            // miCalc
            this.miCalc.Name = "miCalc";
            this.miCalc.Size = new System.Drawing.Size(96, 24);
            this.miCalc.Text = "Рассчитать";

            // miClear
            this.miClear.Name = "miClear";
            this.miClear.Size = new System.Drawing.Size(83, 24);
            this.miClear.Text = "Очистить";

            // miExit
            this.miExit.Name = "miExit";
            this.miExit.Size = new System.Drawing.Size(66, 24);
            this.miExit.Text = "Выход";

            // Labels
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(16, 44);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(23, 20);
            this.lblA.TabIndex = 1;
            this.lblA.Text = "a:";

            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(16, 80);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(23, 20);
            this.lblB.TabIndex = 2;
            this.lblB.Text = "b:";

            this.lblE.AutoSize = true;
            this.lblE.Location = new System.Drawing.Point(16, 116);
            this.lblE.Name = "lblE";
            this.lblE.Size = new System.Drawing.Size(23, 20);
            this.lblE.TabIndex = 3;
            this.lblE.Text = "e:";

            this.lblFx.AutoSize = true;
            this.lblFx.Location = new System.Drawing.Point(16, 152);
            this.lblFx.Name = "lblFx";
            this.lblFx.Size = new System.Drawing.Size(39, 20);
            this.lblFx.TabIndex = 4;
            this.lblFx.Text = "f(x):";

            // TextBoxes
            this.txtA.Location = new System.Drawing.Point(70, 41);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(180, 27);
            this.txtA.TabIndex = 5;

            this.txtB.Location = new System.Drawing.Point(70, 77);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(180, 27);
            this.txtB.TabIndex = 6;

            this.txtE.Location = new System.Drawing.Point(70, 113);
            this.txtE.Name = "txtE";
            this.txtE.Size = new System.Drawing.Size(180, 27);
            this.txtE.TabIndex = 7;

            this.txtFx.Location = new System.Drawing.Point(70, 149);
            this.txtFx.Name = "txtFx";
            this.txtFx.Size = new System.Drawing.Size(520, 27);
            this.txtFx.TabIndex = 8;

            // lblResult
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.Location = new System.Drawing.Point(20, 195);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(570, 110);
            this.lblResult.TabIndex = 9;
            this.lblResult.Padding = new System.Windows.Forms.Padding(8);

            // chart1
            this.chart1.Location = new System.Drawing.Point(610, 41);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(470, 520);
            this.chart1.TabIndex = 10;
            this.chart1.Text = "chart1";

            // Lab1_MinFinderForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 580);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lblResult);
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
            this.Name = "Lab1_MinFinderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ЛР1: Поиск минимума функции";

            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miCalc;
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

        private System.Windows.Forms.Label lblResult;

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
