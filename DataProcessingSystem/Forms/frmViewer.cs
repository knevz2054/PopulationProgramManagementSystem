using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataProcessingSystem.Data;

namespace DataProcessingSystem
{
    public partial class frmViewer : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmViewer()
        {
            InitializeComponent();
        }

        private void frmViewer_Load(object sender, EventArgs e)
        {
            frmHome fh = new frmHome();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = false;
            btnSummary.Enabled = true;
            btnQuery.Enabled = true;

            lblPosition.Text = frmLogin.position;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmViewer").ToList().ForEach(x => x.Close());
            frmHome fh = new frmHome();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = false;
            btnSummary.Enabled = true;
            btnQuery.Enabled = true;
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmViewer").ToList().ForEach(x => x.Close());
            frmViewSummary fh = new frmViewSummary();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = true;
            btnSummary.Enabled = false;
            btnQuery.Enabled = true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmViewer").ToList().ForEach(x => x.Close());
            frmSearch fs = new frmSearch();
            fs.TopLevel = false;
            fs.Dock = DockStyle.Fill;
            fs.Show();
            pnlContainer.Controls.Add(fs);
            btnHome.Enabled = true;
            btnSummary.Enabled = true;
            btnQuery.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            using (DataProcessingSystemEntities db = new DataProcessingSystemEntities())
            {
                string position = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.Position).SingleOrDefault();
                string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();

                tblLog log = new tblLog();
                log.ActivityLog = position + " " + fullName + " has exited the program...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
                Application.Exit();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword();
            changePassword.ShowDialog();
        }

    }
}
