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
    public partial class frmSearch4Ps : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSearch4Ps()
        {
            InitializeComponent();
            txtSearch.Enabled = false;
        }

        private void rbSearch_CheckedChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtSearch.Enabled = groupBox1.Controls.OfType<RadioButton>().Count(x => x.Checked) > 0;
            LoadDefault();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (rbLastName.Checked)
            {
                dgv4Ps.DataSource = null;
                dgv4Ps.DataSource = db.tblIndividuals.Where(x => x.lastName.Contains(txtSearch.Text) && x.member4ps == "Yes").Select(x => new
                {
                    HouseID = x.tblHouse.ID,
                    LastName = x.lastName,
                    FirstName = x.firstName,
                    MiddleName = x.middleName,
                    Gender = x.Gender,
                    CivilStatus = x.civilStatus,
                    Age = x.Age,
                    Birthday = x.birthDay,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HouseNumber = x.tblHouse.houseNumber,
                    Sector = x.tblOccupation.occupationName,
                }).ToList();
            }

            if (rbFirstName.Checked)
            {
                dgv4Ps.DataSource = null;
                dgv4Ps.DataSource = db.tblIndividuals.Where(x => x.firstName.Contains(txtSearch.Text) && x.member4ps == "Yes").Select(x => new
                {
                    HouseID = x.tblHouse.ID,
                    FirstName = x.firstName,
                    MiddleName = x.middleName,
                    LastName = x.lastName,
                    Gender = x.Gender,
                    CivilStatus = x.civilStatus,
                    Age = x.Age,
                    Birthday = x.birthDay,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HouseNumber = x.tblHouse.houseNumber,
                    Sector = x.tblOccupation.occupationName,
                }).ToList();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Checked = false);
            LoadDefault();
            txtSearch.Clear();
        }

        public void LoadDefault()
        {
            dgv4Ps.DataSource = db.tblIndividuals.Where(x => x.member4ps == "Yes").Select(x => new
            {
                HouseID = x.tblHouse.ID,
                FirstName = x.firstName,
                MiddleName = x.middleName,
                LastName = x.lastName,
                Gender = x.Gender,
                CivilStatus = x.civilStatus,
                Age = x.Age,
                Birthday = x.birthDay,
                Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                Purok = x.tblHouse.tblPurok.purokName,
                HouseNumber = x.tblHouse.houseNumber,
                Sector = x.tblOccupation.occupationName,
            }).ToList();
        }

        private void frmSearch4Ps_Load(object sender, EventArgs e)
        {
            LoadDefault();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "4Ps Members";

            for (int i = 1; i < dgv4Ps.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgv4Ps.Columns[i].HeaderText;

            for (int i = 0; i < dgv4Ps.Rows.Count; i++)
            {
                for (int j = 1; j < dgv4Ps.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgv4Ps.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }
    }
}
