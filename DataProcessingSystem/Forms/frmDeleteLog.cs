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
    public partial class frmDeleteLog : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmDeleteLog()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '\u25CF';
        }
        private void btnDeleteLogs_Click(object sender, EventArgs e)
        {
            string password = db.tblAdmins.Select(x => x.Password).SingleOrDefault();
            if(password != txtPassword.Text)
            {
                MessageBox.Show("Invalid Password...", "Failed");
                return;
            }
            else
            {
                db.tblLogs.RemoveRange(db.tblLogs);
                db.SaveChanges();
                
                tblLog logs = new tblLog();
                logs.ActivityLog = "Logs has been cleard by System Admin";
                logs.DateTime = DateTime.Now;
                db.tblLogs.Add(logs);
                db.SaveChanges();

                frmViewLog log = (frmViewLog)Application.OpenForms["frmViewLog"];
                log.LoadLogs();
                this.Close();
            }
        }
    }
}
