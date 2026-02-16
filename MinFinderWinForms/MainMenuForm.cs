using System.Windows.Forms;

namespace MinFinderWinForms
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();

            miOpenLab1.Click += (_, __) => OpenLab(new Lab1_MinFinderForm());
            miOpenLab2.Click += (_, __) => OpenLab(new Lab2_SlaeSolverForm());
            miOpenLab3.Click += (_, __) => OpenLab(new Lab3_GoldenSectionForm());
            miOpenLab4.Click += (_, __) => OpenLab(new Lab4_ConvergenceForm());
            miOpenLab5.Click += (_, __) => OpenLab(new Lab5_SortingVisualizerForm());
            miOpenLab6.Click += (_, __) => OpenLab(new Lab6_IntegrationForm());
            miOpenLab7.Click += (_, __) => OpenLab(new Lab7_CoordinateDescentForm());
            miOpenLab8.Click += (_, __) => OpenLab(new Lab8_LeastSquaresForm());

            miExit.Click += (_, __) => Close();

            btnLab1.Click += (_, __) => OpenLab(new Lab1_MinFinderForm());
            btnLab2.Click += (_, __) => OpenLab(new Lab2_SlaeSolverForm());
            btnLab3.Click += (_, __) => OpenLab(new Lab3_GoldenSectionForm());
            btnLab4.Click += (_, __) => OpenLab(new Lab4_ConvergenceForm());
            btnLab5.Click += (_, __) => OpenLab(new Lab5_SortingVisualizerForm());
            btnLab6.Click += (_, __) => OpenLab(new Lab6_IntegrationForm());
            btnLab7.Click += (_, __) => OpenLab(new Lab7_CoordinateDescentForm());
            btnLab8.Click += (_, __) => OpenLab(new Lab8_LeastSquaresForm());

            btnExit.Click += (_, __) => Close();
        }

        private void OpenLab(Form labForm)
        {
            labForm.StartPosition = FormStartPosition.CenterScreen;
            labForm.Show();
        }
    }
}
