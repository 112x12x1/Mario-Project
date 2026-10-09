using System;
using System.Drawing;
using System.Windows.Forms;
using MarioGameSystem.DAL;

namespace MarioGameSystem.Forms.Reports
{
    public partial class frmChartDashboard : Form
    {
        private DataGridView gridScores = null!;
        private ScoreManager scoreManager = null!;

        public frmChartDashboard()
        {
            InitControls();
            scoreManager = new ScoreManager("scores.txt");
            LoadScores();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(650, 400);
            this.Text = "Chart & Leaderboard (Read/Write File)";

            gridScores = new DataGridView { Location = new Point(20, 20), Size = new Size(600, 340), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(gridScores);
        }

        private void LoadScores()
        {
            try
            {
                gridScores.DataSource = scoreManager.GetTopScores(10);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading scores: " + ex.Message);
            }
        }
    }
}