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
    public partial class frmEncoder : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmEncoder()
        {
            InitializeComponent();
        }

        private void frmEncoder_Load(object sender, EventArgs e)
        {
            frmHome fh = new frmHome();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = false;
            btnSurveyForm.Enabled = true;
            btnViewSurvey.Enabled = true;

            lblPosition.Text = frmLogin.position;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "frmEncoder" && x.Name != "Main").ToList().ForEach(x => x.Close());

            frmHome fh = new frmHome();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = false;
            btnSurveyForm.Enabled = true;
            btnViewSurvey.Enabled = true;
        }

        private void btnSurveyForm_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "frmEncoder" && x.Name != "Main").ToList().ForEach(x => x.Close());
            frmSurveyForm fsf = new frmSurveyForm();
            fsf.TopLevel = false;
            fsf.Dock = DockStyle.Fill;
            fsf.Show();
            pnlContainer.Controls.Add(fsf);

            btnHome.Enabled = true;
            btnSurveyForm.Enabled = false;
            btnViewSurvey.Enabled = true;
        }

        private void btnViewSurvey_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "frmEncoder" && x.Name != "Main").ToList().ForEach(x => x.Close());
            frmViewSurvey fve = new frmViewSurvey();
            fve.TopLevel = false;
            fve.Dock = DockStyle.Fill;
            fve.Show();
            pnlContainer.Controls.Add(fve);

            btnHome.Enabled = true;
            btnViewSurvey.Enabled = false;
            btnSurveyForm.Enabled = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
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

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword();
            changePassword.ShowDialog();
        }
    }
}
