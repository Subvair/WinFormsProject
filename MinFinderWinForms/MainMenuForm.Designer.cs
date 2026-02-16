namespace MinFinderWinForms
{
    partial class MainMenuForm
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
            this.miLabs = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab2 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab3 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab4 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab5 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab6 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab7 = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenLab8 = new System.Windows.Forms.ToolStripMenuItem();
            this.miExit = new System.Windows.Forms.ToolStripMenuItem();

            this.btnLab1 = new System.Windows.Forms.Button();
            this.btnLab2 = new System.Windows.Forms.Button();
            this.btnLab3 = new System.Windows.Forms.Button();
            this.btnLab4 = new System.Windows.Forms.Button();
            this.btnLab5 = new System.Windows.Forms.Button();
            this.btnLab6 = new System.Windows.Forms.Button();
            this.btnLab7 = new System.Windows.Forms.Button();
            this.btnLab8 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();

            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();

            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miLabs,
            this.miExit});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);

            this.miLabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenLab1,
            this.miOpenLab2,
            this.miOpenLab3,
            this.miOpenLab4,
            this.miOpenLab5,
            this.miOpenLab6,
            this.miOpenLab7,
            this.miOpenLab8});
            this.miLabs.Text = "Лабораторные";

            this.miOpenLab1.Text = "ЛР1: Минимум";
            this.miOpenLab2.Text = "ЛР2: СЛАУ";
            this.miOpenLab3.Text = "ЛР3: Золотое сечение";
            this.miOpenLab4.Text = "ЛР4: Метод Ньютона";
            this.miOpenLab5.Text = "ЛР5: Сортировки";
            this.miOpenLab6.Text = "ЛР6: Интегрирование";
            this.miOpenLab7.Text = "ЛР7: Покоординатный спуск";
            this.miOpenLab8.Text = "ЛР8: Метод наименьших квадратов";

            this.miExit.Text = "Выход";

            int y = 40;

            this.btnLab1 = new System.Windows.Forms.Button()
            {
                Text = "ЛР1: Поиск минимума функции",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab2 = new System.Windows.Forms.Button()
            {
                Text = "ЛР2: Решение СЛАУ (Гаусс / Жордан / Крамер)",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab3 = new System.Windows.Forms.Button()
            {
                Text = "ЛР3: Метод золотого сечения",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab4 = new System.Windows.Forms.Button()
            {
                Text = "ЛР4: Метод Ньютона",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab5 = new System.Windows.Forms.Button()
            {
                Text = "ЛР5: Алгоритмы сортировки",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab6 = new System.Windows.Forms.Button()
            {
                Text = "ЛР6: Численное интегрирование",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab7 = new System.Windows.Forms.Button()
            {
                Text = "ЛР7: Метод покоординатного спуска",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;

            this.btnLab8 = new System.Windows.Forms.Button()
            {
                Text = "ЛР8: Метод наименьших квадратов",
                Location = new System.Drawing.Point(20, y),
                Size = new System.Drawing.Size(760, 35)
            };
            y += 40;


            this.Controls.Add(this.btnLab1);
            this.Controls.Add(this.btnLab2);
            this.Controls.Add(this.btnLab3);
            this.Controls.Add(this.btnLab4);
            this.Controls.Add(this.btnLab5);
            this.Controls.Add(this.btnLab6);
            this.Controls.Add(this.btnLab7);
            this.Controls.Add(this.btnLab8);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.menuStrip1);

            this.MainMenuStrip = this.menuStrip1;
            this.ClientSize = new System.Drawing.Size(800, 420);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Меню лабораторных";

            this.menuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem miLabs;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab1;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab2;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab3;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab4;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab5;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab6;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab7;
        private System.Windows.Forms.ToolStripMenuItem miOpenLab8;
        private System.Windows.Forms.ToolStripMenuItem miExit;

        private System.Windows.Forms.Button btnLab1;
        private System.Windows.Forms.Button btnLab2;
        private System.Windows.Forms.Button btnLab3;
        private System.Windows.Forms.Button btnLab4;
        private System.Windows.Forms.Button btnLab5;
        private System.Windows.Forms.Button btnLab6;
        private System.Windows.Forms.Button btnLab7;
        private System.Windows.Forms.Button btnLab8;
        private System.Windows.Forms.Button btnExit;
    }
}
