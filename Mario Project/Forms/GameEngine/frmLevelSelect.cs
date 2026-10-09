using System;
using System.Drawing;
using System.Windows.Forms;

namespace MarioGameSystem.Forms.GameEngine
{
    public partial class frmLevelSelect : Form
    {
        public frmLevelSelect()
        {
            InitControls();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(450, 250);
            this.Text = "Select Level";
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitle = new Label
            {
                Text = "CHỌN MÀN CHƠI",
                Font = new Font("Arial", 16, FontStyle.Bold),
                Location = new Point(120, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            for (int i = 1; i <= 3; i++)
            {
                int levelNum = i;
                Button btnLevel = new Button
                {
                    Text = $"Level {levelNum}",
                    Font = new Font("Arial", 11, FontStyle.Bold),
                    Location = new Point(50 + (i - 1) * 120, 100),
                    Size = new Size(100, 50),
                    BackColor = Color.LightSkyBlue
                };

                btnLevel.Click += (s, e) =>
                {
                    frmGamePlay game = new frmGamePlay("Player1", levelNum);
                    this.Hide();
                    game.ShowDialog();
                    this.Show();
                };

                this.Controls.Add(btnLevel);
            }
        }
    }
}