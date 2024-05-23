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
    public partial class frmAddPurok : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddPurok()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddPurok_Load(object sender, EventArgs e)
        {
            
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtPurok.Text = db.tblPuroks.Where(x => x.ID == frmCategoryList.PurokId).Select(x => x.purokName).SingleOrDefault();
                cbBarangay.Items.Add(db.tblPuroks.Where(x => x.ID == frmCategoryList.PurokId).Select(x => x.tblBarangay.brgyName).SingleOrDefault());
                cbBarangay.SelectedIndex = 0;
            }
            foreach (var item in db.tblBarangays)
            {
                cbBarangay.Items.Add(item.brgyName);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblPurok purok = new tblPurok();
                int brgyID = db.tblBarangays.Where(x => x.brgyName == cbBarangay.Text).Select(x => x.ID).SingleOrDefault();
                if (db.tblPuroks.Count(x => x.purokName == txtPurok.Text.Trim() && x.brgyID == brgyID) > 0)
                {
                    MessageBox.Show(txtPurok.Text + " is already listed...", "Error!");
                    return;
                }
                if (cbBarangay.SelectedItem == null)
                {
                    MessageBox.Show("Select Barangay...", "Error!");
                    return;
                }

                purok.purokName = txtPurok.Text.Trim();
                purok.brgyID = brgyID;

                db.tblPuroks.Add(purok);
                db.SaveChanges();

                MessageBox.Show(txtPurok.Text + " has been added to  " + cbBarangay.Text + "...", "Success!");
                

                tblLog log = new tblLog();
                log.ActivityLog = txtPurok.Text + " has been added by System Admin to list of Puroks in " + cbBarangay.Text + ".";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                txtPurok.Clear();
                cbBarangay.SelectedItem = null;
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblPuroks.Count(x => x.purokName == txtPurok.Text.Trim() && x.ID != frmCategoryList.PurokId && x.tblBarangay.brgyName == cbBarangay.Text) > 0)
                {
                    MessageBox.Show(txtPurok.Text + " is already listed...", "Error!");
                    return;
                }

                tblPurok purok = db.tblPuroks.Find(frmCategoryList.PurokId);
                int brgyID = db.tblBarangays.Where(x => x.brgyName == cbBarangay.Text).Select(x => x.ID).SingleOrDefault();
                

                purok.purokName = txtPurok.Text.Trim();
                purok.brgyID = brgyID;
                string oldName = txtPurok.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been changed to " + txtPurok.Text + " by System Admin...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadPurok();
                this.Close();
            }
        }

        private void TxtPurok_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtPurok.Text.Length > 0;
        }
    }
}
