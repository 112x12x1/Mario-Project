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
            this.ClientSize = new Size(400, 300);
            this.Text = "Select Level";

            Button btn1 = new Button { Text = "Level 1", Location = new Point(50, 50), Size = new Size(100, 40) };
            btn1.Click += (s, e) => {
                frmGamePlay game = new frmGamePlay("Player1");
                game.ShowDialog();
            };

            this.Controls.Add(btn1);
        }
    }
}