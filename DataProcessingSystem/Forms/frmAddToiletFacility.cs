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
    public partial class frmAddToiletFacility : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddToiletFacility()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddToiletFacility_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtToiletFacility.Text = db.tblToiletFacilities.Where(x => x.ID == frmCategoryList.tfId).Select(x => x.facilityName).SingleOrDefault();
                txtNumber.Text = db.tblToiletFacilities.Where(x => x.ID == frmCategoryList.tfId).Select(x => x.facilityNumber).SingleOrDefault().ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblToiletFacility tf = new tblToiletFacility();
                if (db.tblToiletFacilities.Count(x => x.facilityName == txtToiletFacility.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtToiletFacility.Text + " is already listed in Toilet Facility...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblToiletFacilities.Count(x => x.facilityNumber == num) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Toilet Facility...", "Error!");
                    return;
                }

                tf.facilityName = txtToiletFacility.Text.Trim();
                tf.facilityNumber = int.Parse(txtNumber.Text);

                db.tblToiletFacilities.Add(tf);
                db.SaveChanges();

                MessageBox.Show(txtNumber.Text + ". " + txtToiletFacility.Text + " has been added to list of Toilet Facility...", "Success!");
                txtToiletFacility.Clear();
                txtNumber.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtToiletFacility.Text + " has been added by System Admin to list of Toilet Facility...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblToiletFacilities.Count(x => x.facilityName == txtToiletFacility.Text.Trim() && x.ID != frmCategoryList.tfId) > 0)
                {
                    MessageBox.Show(txtToiletFacility.Text + " is already listed in Toilet Facilities...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblToiletFacilities.Count(x => x.facilityNumber == num && x.ID != frmCategoryList.tfId) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Toilet Facilities...", "Error!");
                    return;
                }
                tblToiletFacility tf = db.tblToiletFacilities.Find(frmCategoryList.tfId);
                tf.facilityName = txtToiletFacility.Text.Trim();
                tf.facilityNumber = int.Parse(txtNumber.Text);
                string oldName = txtToiletFacility.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been Modified by System Admin.";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadToiletFacility();
                this.Close();
            }

        }
        private void TxtBoxTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtToiletFacility.Text.Length > 0 && txtNumber.Text.Length > 0;
        }
    }
}
