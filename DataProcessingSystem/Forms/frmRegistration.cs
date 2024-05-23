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
    public partial class frmRegistration : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmRegistration()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '\u25CF';
            txtConfirmPassword.PasswordChar = '\u25CF';
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {

        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                tblUser user = new tblUser();
                user.FullName = txtFullName.Text.Trim();
                user.Gender = cbGender.Text;
                user.Username = txtUsername.Text;
                user.Status = "Banned";
                user.Password = txtPassword.Text;
                user.DateRegistered = DateTime.Now;

                db.tblUsers.Add(user);
                db.SaveChanges();

                MessageBox.Show("Success!");
                txtFullName.Clear();
                cbGender.SelectedItem = null;
                txtUsername.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();

                tblLog log = new tblLog();
                string fullName = db.tblUsers.Where(x => x.ID == user.ID).Select(x => x.FullName).SingleOrDefault();
                log.ActivityLog = fullName + " has just registered to the system...";
                log.DateTime = DateTime.Now;

                db.tblLogs.Add(log);
                db.SaveChanges();
            }
        }

        public bool validate()
        {
            if (txtFullName.Text == string.Empty)
                MessageBox.Show("Full Name is required...", "Error!");

            else if (cbGender.SelectedItem == null)
                MessageBox.Show("Select Gender...", "Error!");

            else if (txtUsername.Text == string.Empty)
                MessageBox.Show("Username is required...", "Error!");

            else if (txtPassword.Text == string.Empty)
                MessageBox.Show("Password is required...", "Error!");

            else if (txtConfirmPassword.Text == string.Empty)
                MessageBox.Show("Confirm Password is required...", "Error!");

            else if (txtUsername.Text.Length < 6)
                MessageBox.Show("Username must contain atleast 6 characters...", "Error!");

            else if (txtPassword.Text.Length < 6)
                MessageBox.Show("Password must contain atleast 6 characters...", "Error!");

            else if (txtPassword.Text != txtConfirmPassword.Text)
                MessageBox.Show("Password did not match...", "Error!");

            else if (db.tblUsers.Count(x => x.Username == txtUsername.Text) > 0)
                MessageBox.Show("Username is not available...", "Error!");

            else
                return true;

            return false;
        }
    }
}
