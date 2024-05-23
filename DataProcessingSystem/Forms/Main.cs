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
    public partial class Main : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public Main()
        {
            InitializeComponent();
            frmLogin login = new frmLogin();
            login.ShowDialog();

            if (frmLogin.position == "City Admin" || frmLogin.position == "Barangay Admin")
            {
                frmAdmin fa = new frmAdmin();
                fa.ShowDialog();
            }

            else if (frmLogin.position == "City Encoder" || frmLogin.position == "Barangay Encoder")
            {
                frmEncoder fe = new frmEncoder();
                fe.ShowDialog();
            }

            else if (frmLogin.position == "Viewer")
            {
                frmViewer fv = new frmViewer();
                fv.ShowDialog();
            }

            else
            {
                frmHome fh = new frmHome();
                fh.TopLevel = false;
                fh.Dock = DockStyle.Fill;
                fh.Show();
                pnlContainer.Controls.Add(fh);

                btnHome.Enabled = false;
                btnSetting.Enabled = true;
                
                lblPosition.Text = frmLogin.position;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
           
            
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main").ToList().ForEach(x => x.Close());

            frmHome fh = new frmHome();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = false;
            btnSetting.Enabled = true;

        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main").ToList().ForEach(x => x.Close());
            frmSettingPanel fs = new frmSettingPanel();
            fs.TopLevel = false;
            fs.Dock = DockStyle.Fill;
            fs.Show();
            pnlContainer.Controls.Add(fs);

            btnHome.Enabled = true;
            btnSetting.Enabled = false;
        }

        private void BtnSurveyForm_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main").ToList().ForEach(x => x.Close());
            frmSurveyForm fsf = new frmSurveyForm();
            fsf.TopLevel = false;
            fsf.Dock = DockStyle.Fill;
            fsf.Show();
            pnlContainer.Controls.Add(fsf);

            btnHome.Enabled = true;
            btnSetting.Enabled = true;
        }

        private void BtnSummary_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main").ToList().ForEach(x => x.Close());

            frmViewSummary fh = new frmViewSummary();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = true;
            btnSetting.Enabled = true;
        }

        private void btnViewSurvey_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main").ToList().ForEach(x => x.Close());

            frmViewSurvey fh = new frmViewSurvey();
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            fh.Show();
            pnlContainer.Controls.Add(fh);

            btnHome.Enabled = true;
            btnSetting.Enabled = true;
        }

        private void BtnQuery_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main").ToList().ForEach(x => x.Close());
            btnHome.Enabled = true;
            btnSetting.Enabled = true;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {

            tblLog log = new tblLog();
            log.ActivityLog = "System Admin has exited the program...";
            log.DateTime = DateTime.Now;
            db.tblLogs.Add(log);
            db.SaveChanges();

            this.Close();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword changePassword = new frmChangePassword();
            changePassword.ShowDialog();
        }
    }
}
