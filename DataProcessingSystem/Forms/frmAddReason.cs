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
using  System.Security.Cryptography;

namespace DataProcessingSystem
{
    public partial class frmAddReason : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddReason()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddReason_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtReason.Text = db.tblReasons.Where(x => x.ID == frmCategoryList.reasonId).Select(x => x.reasonName).SingleOrDefault();
                txtNumber.Text = db.tblReasons.Where(x => x.ID == frmCategoryList.reasonId).Select(x => x.reasonNumber).SingleOrDefault().ToString();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                tblReason reason = new tblReason();
                if (db.tblReasons.Count(x => x.reasonName == txtReason.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtReason.Text + " is already listed in Reason for using Family Planning...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblReasons.Count(x => x.reasonNumber == num) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Reason for using Family Planning...", "Error!");
                    return;
                }

                reason.reasonName = txtReason.Text.Trim();
                reason.reasonNumber = int.Parse(txtNumber.Text);

                db.tblReasons.Add(reason);
                db.SaveChanges();

                MessageBox.Show(txtNumber.Text + ". " + txtReason.Text + " has been added to list of Reason for using Family Planning...", "Success!");
                txtReason.Clear();
                txtNumber.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtReason.Text + " has been added by System Admin to list of Reason for using Family Planning...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblReasons.Count(x => x.reasonName == txtReason.Text.Trim() && x.ID != frmCategoryList.reasonId) > 0)
                {
                    MessageBox.Show(txtReason.Text + " is already listed in Reason For Using Family Planning...", "Error!");
                    return;
                }
                int num = int.Parse(txtNumber.Text);
                if (db.tblReasons.Count(x => x.reasonNumber == num && x.ID != frmCategoryList.reasonId) > 0)
                {
                    MessageBox.Show("No." + txtNumber.Text + " is already assigned in Reason For Using Family Planning...", "Error!");
                    return;
                }
                tblReason reason = db.tblReasons.Find(frmCategoryList.reasonId);
                reason.reasonName = txtReason.Text.Trim();
                reason.reasonNumber = int.Parse(txtNumber.Text);
                string oldName = txtReason.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been Modified by System Admin.";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadReasonForUsing();
                this.Close();
            }

        }

        private void TxtBoxTextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtReason.Text.Length > 0 && txtNumber.Text.Length > 0;
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
