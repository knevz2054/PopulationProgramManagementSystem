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
    public partial class frmAddWasteDisposal : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddWasteDisposal()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddWasteDisposal_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtWasteDisposal.Text = db.tblWasteDisposals.Where(x => x.ID == frmCategoryList.wdId).Select(x => x.disposalName).SingleOrDefault();
                txtNumber.Text = db.tblWasteDisposals.Where(x => x.ID == frmCategoryList.wdId).Select(x => x.disposalNumber).SingleOrDefault().ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblWasteDisposal wd = new tblWasteDisposal();
                if (db.tblWasteDisposals.Count(x => x.disposalName == txtWasteDisposal.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtWasteDisposal.Text + " is already listed in Waste Disposal...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblWasteDisposals.Count(x => x.disposalNumber == num) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in  Waste Disposal...", "Error!");
                    return;
                }

                wd.disposalName = txtWasteDisposal.Text.Trim();
                wd.disposalNumber = int.Parse(txtNumber.Text);

                db.tblWasteDisposals.Add(wd);
                db.SaveChanges();

                MessageBox.Show(txtNumber.Text + ". " + txtWasteDisposal.Text + " has been added to list of Waste Disposal...", "Success!");
                txtWasteDisposal.Clear();
                txtNumber.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtWasteDisposal.Text + " has been added by System Admin to list of Waste Disposal...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblWasteDisposals.Count(x => x.disposalName == txtWasteDisposal.Text.Trim() && x.ID != frmCategoryList.wdId) > 0)
                {
                    MessageBox.Show(txtWasteDisposal.Text + " is already listed in Waste Disposals...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblWasteDisposals.Count(x => x.disposalNumber == num && x.ID != frmCategoryList.wdId) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Waste Disposals...", "Error!");
                    return;
                }
                tblWasteDisposal wd = db.tblWasteDisposals.Find(frmCategoryList.wdId);
                wd.disposalName = txtWasteDisposal.Text.Trim();
                wd.disposalNumber = int.Parse(txtNumber.Text);
                string oldName = txtWasteDisposal.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been Modified by System Admin.";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadWasteDisposal();
                this.Close();
            }

        }

        private void TxtBoxTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtWasteDisposal.Text.Length > 0 && txtNumber.Text.Length > 0;
        }
        private void BtnAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
