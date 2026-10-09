using System;
using System.Drawing;
using System.Windows.Forms;
using MarioGameSystem.Helpers;

namespace MarioGameSystem.Forms.Auth
{
    public partial class frmLogin : Form
    {
        private TextBox txtUser, txtPass;
        private Button btnLogin;

        public frmLogin()
        {
            InitControls();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(300, 200);
            this.Text = "Login System";
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblU = new Label { Text = "Username:", Location = new Point(20, 20), AutoSize = true };
            txtUser = new TextBox { Location = new Point(20, 45), Width = 240 };

            Label lblP = new Label { Text = "Password:", Location = new Point(20, 80), AutoSize = true };
            txtPass = new TextBox { Location = new Point(20, 105), Width = 240, PasswordChar = '*' };

            btnLogin = new Button { Text = "Login", Location = new Point(20, 145), Width = 240, Height = 30 };
            btnLogin.Click += BtnLogin_Click;

            this.Controls.AddRange(new Control[] { lblU, txtUser, lblP, txtPass, btnLogin });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
                {
                    MessageBox.Show("Please fill all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                UserSession.UserID = 1;
                UserSession.Username = txtUser.Text.Trim();
                UserSession.RoleID = 1;
                UserSession.Permissions = new System.Collections.Generic.List<string> 
                { 
                    "USER_MGMT", "ROLE_MGMT", "CAT_LEVEL", "CAT_ITEM", "CAT_ENEMY", 
                    "GAME_PLAY", "SHOP", "REPORT" 
                };

                this.Hide();
                frmMain main = new frmMain();
                main.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}