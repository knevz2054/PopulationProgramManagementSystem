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
    public partial class frmUserPermit : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        int ID = frmViewUser.IDuser;
        public frmUserPermit()
        {
            InitializeComponent();
        }

        private void frmUserPermit_Load(object sender, EventArgs e)
        {
            lblFullName.Text = db.tblUsers.Where(x => x.ID == ID).Select(x => x.FullName).SingleOrDefault();
            lblGender.Text = db.tblUsers.Where(x => x.ID == ID).Select(x => x.Gender).SingleOrDefault();
            lblUsername.Text = db.tblUsers.Where(x => x.ID == ID).Select(x => x.Username).SingleOrDefault();
            lblPassword.Text = db.tblUsers.Where(x => x.ID == ID).Select(x => x.Password).SingleOrDefault();
            lblDateRegistered.Text = db.tblUsers.Where(x => x.ID == ID).Select(x => x.DateRegistered.ToString()).SingleOrDefault();
            chkBanned.Checked = db.tblUsers.Where(x => x.ID == ID).Select(x => x.Status).SingleOrDefault() == "Banned";

            cbPosition.Items.Add("City Admin");
            cbPosition.Items.Add("City Encoder");
            cbPosition.Items.Add("Barangay Admin");
            cbPosition.Items.Add("Barangay Encoder");
            cbPosition.Items.Add("Viewer");

            foreach (var v in db.tblBarangays)
                cbAccess.Items.Add(v.brgyName);

            string position = db.tblUsers.Where(x => x.ID == ID).Select(x => x.Position).SingleOrDefault();
            cbPosition.Text = position;

            string access = db.tblUsers.Where(x => x.ID == ID).Select(x => x.Access).SingleOrDefault();
            cbAccess.Text = access;
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAccess.Items.Clear();
            if (cbPosition.Text == "City Admin" || cbPosition.Text == "City Encoder" || cbPosition.Text == "Viewer")
            {
                cbAccess.Items.Add("City");
                cbAccess.SelectedIndex = 0;
                cbAccess.Enabled = false;
            }
            
            else
            {
                foreach (var v in db.tblBarangays)
                    cbAccess.Items.Add(v.brgyName);

                cbAccess.Enabled = true;
            }
        }

        private void cbAccess_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            tblUser user = db.tblUsers.SingleOrDefault(x => x.ID == ID);
            if (cbPosition.SelectedItem == null || cbAccess.SelectedItem == null)
            {
                MessageBox.Show("Select Possition and Access...", "Error!");
            }
            user.Position = cbPosition.Text;
            user.Access = cbAccess.Text;
            user.Status = chkBanned.Checked ? "Banned" : "Active";
            user.DateAssigned = DateTime.Now;

            db.SaveChanges();

            if (!chkBanned.Checked)
                MessageBox.Show("You have granted " + lblFullName.Text + " an access to the system as " + cbPosition.Text + "...");
            else
                MessageBox.Show("You have banned " + lblFullName.Text + " to access the system...");


            tblLog log = new tblLog();

            if (!chkBanned.Checked && frmLogin.position == "System Admin")
                log.ActivityLog = lblFullName.Text + " has been assigned as " + cbPosition.Text + " by System Admin";


            //if (!chkBanned.Checked && frmLogin.position != "System Admin")
            //{
            //    string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
            //    log.ActivityLog = lblFullName.Text + " has been assigned as " + cbPosition.Text + " by " + frmLogin.position + " " + fullName;

            //}

            if (chkBanned.Checked && frmLogin.position == "System Admin")
                log.ActivityLog = lblFullName.Text + ", has been banned by System Admin";


            //if (chkBanned.Checked && frmLogin.position != "System Admin")
            //{
            //    string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
            //    log.ActivityLog = lblFullName.Text + ", has been banned by " + frmLogin.position + " " + fullName;
            //}

            log.DateTime = DateTime.Now;
            db.tblLogs.Add(log);
            db.SaveChanges();

            frmViewUser fvu = (frmViewUser)Application.OpenForms["frmViewUser"];
            fvu.loadDGVuser();

            this.Close();
        }

    }
}
