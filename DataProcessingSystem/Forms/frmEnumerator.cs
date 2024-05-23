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
    public partial class frmEnumerator : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmEnumerator()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmConductor_Load(object sender, EventArgs e)
        {
            if(edit == true)
            {
                btnAdd.Text = "Update";
                txtEnumerator.Text = db.tblConductors.Where(x => x.ID == frmCategoryList.enumeratorId).Select(x => x.fullName).SingleOrDefault();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                if (db.tblConductors.Count(x => x.fullName == txtEnumerator.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtEnumerator.Text + " is already listed...", "Error!");
                    return;
                }

                tblConductor cond = new tblConductor();
                cond.fullName = txtEnumerator.Text.Trim();

                db.tblConductors.Add(cond);
                db.SaveChanges();

                MessageBox.Show(txtEnumerator.Text + " has been added to list of Conductors...", "Success!");
                tblLog log = new tblLog();
                string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
                log.ActivityLog = txtEnumerator.Text + " has been added by " + fullName + " to list of Conductors...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                txtEnumerator.Clear();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblConductors.Count(x => x.fullName == txtEnumerator.Text.Trim() && x.ID != frmCategoryList.enumeratorId) > 0)
                {
                    MessageBox.Show(txtEnumerator.Text + " is already listed...", "Error!");
                    return;
                }

                tblConductor cond = db.tblConductors.Find(frmCategoryList.enumeratorId);
                cond.fullName = txtEnumerator.Text.Trim();
                string oldName = txtEnumerator.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been changed to " + txtEnumerator.Text + " by System Admin...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                txtEnumerator.Clear();
                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadEnumerator();
            }
            this.Close();
        }
        private void TxtConductor_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtEnumerator.Text.Length > 0;
        }

        private void FrmConductor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                frmSurveyForm sf = (frmSurveyForm)Application.OpenForms["frmSurveyForm"];
                sf.refConductor();
            }
        }

        private void FrmConductor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                frmSurveyForm sf = (frmSurveyForm)Application.OpenForms["frmSurveyForm"];
                sf.refConductor();
            }
        }
    }
}
