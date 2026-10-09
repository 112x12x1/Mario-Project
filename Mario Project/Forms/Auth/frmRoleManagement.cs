using System;
using System.Drawing;
using System.Windows.Forms;

namespace MarioGameSystem.Forms.Auth
{
    public partial class frmRoleManagement : Form
    {
        private DataGridView grid;

        public frmRoleManagement()
        {
            InitControls();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(600, 400);
            this.Text = "Role Management";

            grid = new DataGridView { Location = new Point(20, 20), Size = new Size(560, 340), AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            this.Controls.Add(grid);
        }
    }
}