using System;
using System.Drawing;
using System.Windows.Forms;

namespace MarioGameSystem.Forms.Reports
{
    public partial class frmReportViewer : Form
    {
        public frmReportViewer()
        {
            this.ClientSize = new Size(700, 500);
            this.Text = "RDLC Report Viewer";
        }
    }
}