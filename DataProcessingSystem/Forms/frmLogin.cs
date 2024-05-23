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
    public partial class frmLogin : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static int userID;
        bool permission = false;
        public static string position;
        public static string access;
        public frmLogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '\u25CF';
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (db.tblAdmins.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text) > 0)
            {
                MessageBox.Show("You have logged in as System Admin...", "Success!");
                permission = true;
                userID = db.tblAdmins.Select(x => x.ID).SingleOrDefault();
                position = "System Admin";

                tblLog log = new tblLog();
                log.ActivityLog = "System Admin has logged in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                this.Close();
            }

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Status == "Banned") > 0)
            {
                string fullName = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Status == "Banned").Select(x => x.FullName).SingleOrDefault();
                MessageBox.Show("You are registered but dont have access yet. Please contact the System Administrator...", "Failed to log-in!");

                tblLog log = new tblLog();
                log.ActivityLog = fullName + " has attempted to log-in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                return;
            }

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Admin") > 0)
            {
                MessageBox.Show("You have logged in as City Admin...", "Success!");
                permission = true;
                userID = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Admin")
                    .Select(x => x.ID).SingleOrDefault();

                position = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Admin")
                   .Select(x => x.Position).SingleOrDefault();

                access = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Admin")
                  .Select(x => x.Access).SingleOrDefault();

                string fullName = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Admin")
                   .Select(x => x.FullName).SingleOrDefault();

                tblLog log = new tblLog();
                log.ActivityLog = position + " " + fullName + " has logged in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                this.Close();
            }

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Viewer") > 0)
            {
                MessageBox.Show("You have logged in as Viewer...", "Success!");
                permission = true;
                userID = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Viewer")
                    .Select(x => x.ID).SingleOrDefault();

                position = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Viewer")
                   .Select(x => x.Position).SingleOrDefault();

                access = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Viewer")
                  .Select(x => x.Access).SingleOrDefault();

                string fullName = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Viewer")
                   .Select(x => x.FullName).SingleOrDefault();

                tblLog log = new tblLog();
                log.ActivityLog = position + " " + fullName + " has logged in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                this.Close();
            }

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Encoder") > 0)
            {
                MessageBox.Show("You have logged in as City Encoder...", "Success!");
                permission = true;
                userID = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Encoder")
                    .Select(x => x.ID).SingleOrDefault();

                position = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Encoder")
                    .Select(x => x.Position).SingleOrDefault();

                access = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Encoder")
                    .Select(x => x.Access).SingleOrDefault();

                string fullName = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "City Encoder")
                    .Select(x => x.FullName).SingleOrDefault();

                tblLog log = new tblLog();
                log.ActivityLog = position + " " + fullName + " has logged in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                this.Close();
            }

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Admin") > 0)
            {
                MessageBox.Show("You have logged in as Barangay Admin...", "Success!");
                permission = true;
                userID = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Admin")
                    .Select(x => x.ID).Single();

                position = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Admin")
                   .Select(x => x.Position).SingleOrDefault();

                access = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Admin")
                   .Select(x => x.Access).SingleOrDefault();

                string fullName = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Admin")
                  .Select(x => x.FullName).SingleOrDefault();

                tblLog log = new tblLog();
                log.ActivityLog = position + " " + fullName + " has logged in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                this.Close();
            }

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Encoder") > 0)
            {
                MessageBox.Show("You have logged in as Barangay Encoder...", "Success!");
                permission = true;
                userID = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Encoder")
                    .Select(x => x.ID).SingleOrDefault();

                position = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Encoder")
                   .Select(x => x.Position).SingleOrDefault();

                access = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Encoder")
                   .Select(x => x.Access).SingleOrDefault();

                string fullName = db.tblUsers.Where(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text && x.Position == "Barangay Encoder")
                  .Select(x => x.FullName).SingleOrDefault();

                tblLog log = new tblLog();
                log.ActivityLog = position + " " + fullName + " has logged in...";
                log.DateTime = DateTime.Now;
                db.tblLogs.Add(log);
                db.SaveChanges();

                this.Close();
            }

            else
            {
                MessageBox.Show("Account not found...", "Failed to log-in!");
                return;
            }

        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!permission)
                Application.Exit();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!permission)
                Application.Exit();
        }

        private void lnkRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmRegistration fr = new frmRegistration();
            fr.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
