using System;
using System.Drawing;
using System.Windows.Forms;
using MarioGameSystem.Helpers;

namespace MarioGameSystem.Forms.Auth
{
    public partial class frmChangePassword : Form
    {
        private TextBox txtOld, txtNew;
        private Button btnSave;

        public frmChangePassword()
        {
            InitControls();
        }

        private void InitControls()
        {
            this.ClientSize = new Size(300, 200);
            this.Text = "Change Password";

            Label l1 = new Label { Text = "Old Password:", Location = new Point(20, 20), AutoSize = true };
            txtOld = new TextBox { Location = new Point(20, 45), Width = 240, PasswordChar = '*' };

            Label l2 = new Label { Text = "New Password:", Location = new Point(20, 80), AutoSize = true };
            txtNew = new TextBox { Location = new Point(20, 105), Width = 240, PasswordChar = '*' };

            btnSave = new Button { Text = "Update", Location = new Point(20, 145), Width = 240, Height = 30 };
            btnSave.Click += BtnSave_Click;

            this.Controls.AddRange(new Control[] { l1, txtOld, l2, txtNew, btnSave });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtOld.Text) || string.IsNullOrWhiteSpace(txtNew.Text))
                {
                    MessageBox.Show("All fields required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string newSalt = SecurityManager.GenerateSalt();
                string newHash = SecurityManager.HashPassword(txtNew.Text, newSalt);
                MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}