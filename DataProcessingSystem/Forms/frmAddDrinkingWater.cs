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
    public partial class frmAddDrinkingWater : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddDrinkingWater()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddDrinkingWater_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtDrinkingWater.Text = db.tblDrinkingWaters.Where(x => x.ID == frmCategoryList.dwsId).Select(x => x.sourceName).SingleOrDefault();
                txtNumber.Text = db.tblDrinkingWaters.Where(x => x.ID == frmCategoryList.dwsId).Select(x => x.sourceNumber).SingleOrDefault().ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblDrinkingWater dw = new tblDrinkingWater();
                if (db.tblDrinkingWaters.Count(x => x.sourceName == txtDrinkingWater.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtDrinkingWater.Text + " is already listed...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblDrinkingWaters.Count(x => x.sourceNumber == num) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Drinking Water Source...", "Error!");
                    return;
                }

                dw.sourceName = txtDrinkingWater.Text.Trim();
                dw.sourceNumber = int.Parse(txtNumber.Text);

                db.tblDrinkingWaters.Add(dw);
                db.SaveChanges();

                MessageBox.Show(txtNumber.Text + ". " + txtDrinkingWater.Text + " has been added to list...", "Success!");
                txtDrinkingWater.Clear();
                txtNumber.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtDrinkingWater.Text + " has been added by System Admin to list of Drinking Water Source...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblDrinkingWaters.Count(x => x.sourceName == txtDrinkingWater.Text.Trim() && x.ID != frmCategoryList.dwsId) > 0)
                {
                    MessageBox.Show(txtDrinkingWater.Text + " is already listed in Drinking Water Source...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblDrinkingWaters.Count(x => x.sourceNumber == num && x.ID != frmCategoryList.dwsId) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Drinking Water Source...", "Error!");
                    return;
                }
                tblDrinkingWater ws = db.tblDrinkingWaters.Find(frmCategoryList.dwsId);
                ws.sourceName = txtDrinkingWater.Text.Trim();
                ws.sourceNumber = int.Parse(txtNumber.Text);
                string oldName = txtDrinkingWater.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been Modified by System Admin.";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadDrinkingWaterSource();
                this.Close();
            }

        }

        private void TxtBoxTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtDrinkingWater.Text.Length > 0 && txtNumber.Text.Length > 0;
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
