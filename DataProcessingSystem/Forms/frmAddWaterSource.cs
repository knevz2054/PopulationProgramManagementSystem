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
    public partial class frmAddWaterSource : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddWaterSource()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddWaterSource_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtWatersource.Text = db.tblWaterSources.Where(x => x.ID == frmCategoryList.wsId).Select(x => x.sourceName).SingleOrDefault();
                txtNumber.Text = db.tblWaterSources.Where(x => x.ID == frmCategoryList.wsId).Select(x => x.sourceNumber).SingleOrDefault().ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblWaterSource ws = new tblWaterSource();
                if (db.tblWaterSources.Count(x => x.sourceName == txtWatersource.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtWatersource.Text + " is already listed in Water Source...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblWaterSources.Count(x => x.sourceNumber == num) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Water Source...", "Error!");
                    return;
                }

                ws.sourceName = txtWatersource.Text.Trim();
                ws.sourceNumber = int.Parse(txtNumber.Text);

                db.tblWaterSources.Add(ws);
                db.SaveChanges();

                MessageBox.Show(txtNumber.Text + ". " + txtWatersource.Text + " has been added to list of Water Source...", "Success!");
                txtWatersource.Clear();
                txtNumber.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtWatersource.Text + " has been added by System Admin to list of Water Source...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblWaterSources.Count(x => x.sourceName == txtWatersource.Text.Trim() && x.ID != frmCategoryList.wsId) > 0)
                {
                    MessageBox.Show(txtWatersource.Text + " is already listed in Water Source...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblWaterSources.Count(x => x.sourceNumber == num && x.ID != frmCategoryList.wsId) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Water Source...", "Error!");
                    return;
                }
                tblWaterSource ws = db.tblWaterSources.Find(frmCategoryList.wsId);
                ws.sourceName = txtWatersource.Text.Trim();
                ws.sourceNumber = int.Parse(txtNumber.Text);
                string oldName = txtWatersource.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been Modified by System Admin.";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadWaterSource();
                this.Close();
            }

        }

        private void TxtBoxTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtWatersource.Text.Length > 0 && txtNumber.Text.Length > 0;
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
