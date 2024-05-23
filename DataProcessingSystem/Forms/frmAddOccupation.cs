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
    public partial class frmAddOccupation : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static bool edit = false;
        public frmAddOccupation()
        {
            InitializeComponent();
            btnAdd.Enabled = false;
        }

        private void FrmAddOccupation_Load(object sender, EventArgs e)
        {
            if (edit == true)
            {
                btnAdd.Text = "Update";
                txtOcupation.Text = db.tblOccupations.Where(x => x.ID == frmCategoryList.occupationId).Select(x => x.occupationName).SingleOrDefault();
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if(btnAdd.Text == "Add")
            {
                if (db.tblOccupations.Count(x => x.occupationName == txtOcupation.Text.Trim()) > 0)
                {
                    MessageBox.Show(txtOcupation.Text + " is already listed...", "Error!");
                    return;
                }

                tblOccupation occ = new tblOccupation();
                occ.occupationName = txtOcupation.Text.Trim();

                db.tblOccupations.Add(occ);
                db.SaveChanges();

                MessageBox.Show(txtOcupation.Text + " has been added to list of Occupations...", "Success!");
                txtOcupation.Clear();

                tblLog log = new tblLog();
                log.ActivityLog = txtOcupation.Text + " has been added by System Admin to list of Occupations...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();
            }

            if (btnAdd.Text == "Update")
            {
                if (db.tblOccupations.Count(x => x.occupationName == txtOcupation.Text.Trim() && x.ID != frmCategoryList.occupationId) > 0)
                {
                    MessageBox.Show(txtOcupation.Text + " is already listed...", "Error!");
                    return;
                }

                tblOccupation occupation = db.tblOccupations.Find(frmCategoryList.occupationId);
                occupation.occupationName = txtOcupation.Text.Trim();
                string oldName = txtOcupation.Text;
                db.SaveChanges();

                MessageBox.Show("Update Successful...", "Success!");
                tblLog log = new tblLog();
                log.ActivityLog = oldName + " has been changed to " + txtOcupation.Text + " by System Admin...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                edit = false;
                frmCategoryList categoryList = (frmCategoryList)Application.OpenForms["frmCategoryList"];
                categoryList.LoadOccupation();
                this.Close();
            }

        }

        private void TxtOcupation_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtOcupation.Text.Length > 0;
        }
    }
}
