using System;
using System.Drawing;
using System.Windows.Forms;

namespace MarioGameSystem.Forms.Auth
{
    public partial class frmUserManagement : Form
    {
        private DataGridView grid;

        public frmUserManagement()
        {
            InitControls();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "User Management";

            grid = new DataGridView { Location = new Point(20, 20), Size = new Size(560, 340), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(grid);
        }
    }
}