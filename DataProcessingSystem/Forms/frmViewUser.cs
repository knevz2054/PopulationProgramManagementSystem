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
    public partial class frmViewUser : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static int IDuser;
        public frmViewUser()
        {
            InitializeComponent();
            dgvUser.EnableHeadersVisualStyles = false;
            dgvUser.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvUser.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSlateGray;
            dgvUser.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUser.RowsDefaultCellStyle.ForeColor = Color.DarkSlateGray;
            dgvUser.AutoGenerateColumns = false;
        }

        private void frmViewUser_Load(object sender, EventArgs e)
        {
            if (frmLogin.position == "System Admin")
                dgvUser.DataSource = db.tblUsers.ToList();

            if (frmLogin.position == "City Admin")
                dgvUser.DataSource = db.tblUsers.Where(x => x.Position == "Barangay Encoder" || x.Position == "City Encoder" || x.Position == "Barangay Admin").ToList();

            if (frmLogin.position == "Barangay Admin")
                dgvUser.DataSource = db.tblUsers.Where(x => x.Access == frmLogin.access && x.Position == "Barangay Encoder").ToList();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUser.Columns[e.ColumnIndex].HeaderText == "Permission")
            {
                IDuser = int.Parse(dgvUser.CurrentRow.Cells[1].Value.ToString());
                frmUserPermit permit = new frmUserPermit();
                permit.ShowDialog();
            }
        }

        public void loadDGVuser()
        {
            if (frmLogin.position == "System Admin")
                dgvUser.DataSource = db.tblUsers.AsNoTracking().ToList();

            if (frmLogin.position == "City Admin")
                dgvUser.DataSource = db.tblUsers.AsNoTracking().Where(x => x.Position == "City Encoder" || x.Position == "Barangay Encoder");

            if (frmLogin.position == "Barangay Admin")
                dgvUser.DataSource = db.tblUsers.AsNoTracking().Where(x => x.Access == frmLogin.access && x.Position == "Barangay Encoder").ToList();
        }

    }
}
