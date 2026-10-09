using System;
using System.Drawing;
using System.Windows.Forms;
using MarioGameSystem.Helpers;
using MarioGameSystem.Forms.Categories;
using MarioGameSystem.Forms.GameEngine;
using MarioGameSystem.Forms.Transactions;
using MarioGameSystem.Forms.Reports;

namespace MarioGameSystem.Forms.Auth
{
    public partial class frmMain : Form
    {
        private MenuStrip menuBar;
        private ToolStripMenuItem mnuUser, mnuRole, mnuCategory, mnuGame, mnuShop, mnuReport;

        public frmMain()
        {
            InitControls();
            ApplyRBAC();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(1000, 600);
            this.Text = "Mario Game Engine & Admin Dashboard";
            this.IsMdiContainer = true;

            menuBar = new MenuStrip();

            mnuUser = new ToolStripMenuItem("User Management", null, (s, e) => OpenForm(new frmUserManagement()));
            mnuRole = new ToolStripMenuItem("Role Management", null, (s, e) => OpenForm(new frmRoleManagement()));
            mnuCategory = new ToolStripMenuItem("Categories");
            mnuCategory.DropDownItems.Add("Levels", null, (s, e) => OpenForm(new frmCategoryLevel()));
            mnuCategory.DropDownItems.Add("Items", null, (s, e) => OpenForm(new frmCategoryItem()));
            mnuCategory.DropDownItems.Add("Enemies", null, (s, e) => OpenForm(new frmCategoryEnemy()));

            mnuGame = new ToolStripMenuItem("Play Game", null, (s, e) => OpenForm(new frmLevelSelect()));
            mnuShop = new ToolStripMenuItem("Item Shop", null, (s, e) => OpenForm(new frmTransactionShop()));
            mnuReport = new ToolStripMenuItem("Reports & Chart", null, (s, e) => OpenForm(new frmChartDashboard()));

            menuBar.Items.AddRange(new ToolStripItem[] { mnuUser, mnuRole, mnuCategory, mnuGame, mnuShop, mnuReport });
            this.MainMenuStrip = menuBar;
            this.Controls.Add(menuBar);
        }

        private void ApplyRBAC()
        {
            mnuUser.Visible = UserSession.Permissions.Contains("USER_MGMT");
            mnuRole.Visible = UserSession.Permissions.Contains("ROLE_MGMT");
            mnuCategory.Visible = UserSession.Permissions.Contains("CAT_LEVEL");
            mnuGame.Visible = UserSession.Permissions.Contains("GAME_PLAY");
            mnuShop.Visible = UserSession.Permissions.Contains("SHOP");
            mnuReport.Visible = UserSession.Permissions.Contains("REPORT");
        }

        private void OpenForm(Form f)
        {
            f.MdiParent = this;
            f.StartPosition = FormStartPosition.CenterScreen;
            f.Show();
        }
    }
}