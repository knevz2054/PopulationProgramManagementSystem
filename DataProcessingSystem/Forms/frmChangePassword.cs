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
    public partial class frmChangePassword : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmChangePassword()
        {
            InitializeComponent();
            txtOldPass.PasswordChar = '\u25CF';
            txtNewPassword.PasswordChar = '\u25CF';
            txtConfirmPassword.PasswordChar = '\u25CF';
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(frmLogin.position == "System Admin")
            {
                string oldPass = db.tblAdmins.Where(x => x.ID == frmLogin.userID).Select(x => x.Password).SingleOrDefault();

                if (txtOldPass.Text == string.Empty || txtNewPassword.Text == string.Empty || txtConfirmPassword.Text == string.Empty)
                {
                    MessageBox.Show("All fields are required...", "Error!");
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("New Password not match...", "Error!");
                    return;
                }

                if (txtOldPass.Text != oldPass)
                {
                    MessageBox.Show("Incorrect Old Password...", "Error!");
                    return;
                }

                tblAdmin admin = db.tblAdmins.Find(frmLogin.userID);
                admin.Password = txtNewPassword.Text.Trim();
                db.SaveChanges();

                string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
                tblLog log = new tblLog();
                log.ActivityLog = "System Admin has changed password...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                MessageBox.Show("Password successfully changed...", "Success!");
            }
            else
            {
                string oldPass = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.Password).SingleOrDefault();

                if (txtOldPass.Text == string.Empty || txtNewPassword.Text == string.Empty || txtConfirmPassword.Text == string.Empty)
                {
                    MessageBox.Show("All fields are required...", "Error!");
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("New Password not match...", "Error!");
                    return;
                }

                if (txtOldPass.Text != oldPass)
                {
                    MessageBox.Show("Incorrect Old Password...", "Error!");
                    return;
                }

                tblUser user = db.tblUsers.Find(frmLogin.userID);
                user.Password = txtNewPassword.Text.Trim();
                db.SaveChanges();

                string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
                tblLog log = new tblLog();
                log.ActivityLog = frmLogin.position + " " + fullName + " has changed password...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                MessageBox.Show("Password successfully changed...", "Success!");
            }
            this.Close();
        }
    }
}
