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
    public partial class frmAddBarangay : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddBarangay()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddBarangay_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtBarangay.Text = db.tblBarangays.Where(x => x.ID == frmCategoryList.BrgyId).Select(x => x.brgyName).SingleOrDefault();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Add")
            {
                if (db.tblBarangays.Count(x => x.brgyName == txtBarangay.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtBarangay.Text + " is already listed...", "Error!");
                    return;
                }

                tblBarangay brgy = new tblBarangay();
                brgy.brgyName = txtBarangay.Text.Trim();

                db.tblBarangays.Add(brgy);
                db.SaveChanges();

                MessageBox.Show(txtBarangay.Text + " has been added to list of Barangays...", "Success!");
                txtBarangay.Clear();


                tblLog log = new tblLog();
                log.ActivityLog = txtBarangay.Text + " has been added by System Admin to list of Barangays...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }


            if (btnAdd.Text == "Update")
            {
                if (db.tblBarangays.Count(x => x.brgyName == txtBarangay.Text.Trim() && x.ID != frmCategoryList.BrgyId) > 0)
                {
                    MessageBox.Show(txtBarangay.Text + " is already listed...", "Error!");
                    return;
                }

                tblBarangay barangay  = db.tblBarangays.Find(frmCategoryList.BrgyId);
                barangay.brgyName = txtBarangay.Text.Trim();
                string oldName = txtBarangay.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been changed to " + txtBarangay.Text + " by System Admin...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadBarangay();
                this.Close();
            }

        }

        private void TxtBarangay_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtBarangay.Text.Length > 0;
        }
    }
}
