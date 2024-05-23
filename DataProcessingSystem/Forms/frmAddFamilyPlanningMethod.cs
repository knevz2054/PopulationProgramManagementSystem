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
    public partial class frmAddFamilyPlanningMethod : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddFamilyPlanningMethod()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddFamilyPlanningMethod_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtFPM.Text = db.tblFamilyPlanningMethods.Where(x => x.ID == frmCategoryList.fpmId).Select(x => x.methodName).SingleOrDefault();
                cbMethodType.Text = db.tblFamilyPlanningMethods.Where(x => x.ID == frmCategoryList.fpmId).Select(x => x.methodType).SingleOrDefault();
                txtNumber.Text = db.tblFamilyPlanningMethods.Where(x => x.ID == frmCategoryList.fpmId).Select(x => x.methodNumber).SingleOrDefault().ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblFamilyPlanningMethod fpm = new tblFamilyPlanningMethod();
                if (db.tblFamilyPlanningMethods.Count(x => x.methodName == txtFPM.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtFPM.Text + " is already listed in Family Planning Methods...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblFamilyPlanningMethods.Count(x => x.methodNumber == num) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Family Planning Methods...", "Error!");
                    return;
                }

                if (cbMethodType.SelectedItem == null)
                {
                    MessageBox.Show("Select a Method Type", "Error!");
                    return;
                }

                fpm.methodName = txtFPM.Text.Trim();
                fpm.methodNumber = int.Parse(txtNumber.Text);
                fpm.methodType = cbMethodType.Text;

                db.tblFamilyPlanningMethods.Add(fpm);
                db.SaveChanges();

                MessageBox.Show(txtNumber.Text + ". " + txtFPM.Text + " has been added to list of Family Planning Methods...", "Success!");
                txtFPM.Clear();
                cbMethodType.SelectedItem = null;
                txtNumber.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtFPM.Text + " has been added by System Admin to list of Family Planning Methods...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblFamilyPlanningMethods.Count(x => x.methodName == txtFPM.Text.Trim() && x.ID != frmCategoryList.fpmId) > 0)
                {
                    MessageBox.Show(txtFPM.Text + " is already listed in Family Planning Methods...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblFamilyPlanningMethods.Count(x => x.methodNumber == num && x.ID != frmCategoryList.fpmId) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Family Planning Methods...", "Error!");
                    return;
                }
                tblFamilyPlanningMethod fpm = db.tblFamilyPlanningMethods.Find(frmCategoryList.fpmId);
                fpm.methodName = txtFPM.Text.Trim();
                fpm.methodNumber = int.Parse(txtNumber.Text);
                fpm.methodType = cbMethodType.Text;
                string oldName = txtFPM.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been Modified by System Admin.";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadFamilyPlanningMethod();
                this.Close();
            }
        }

        private void TxtBoxTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtFPM.Text.Length > 0 && txtNumber.Text.Length > 0;
        }

        private void TxtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
