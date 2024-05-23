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
    public partial class frmSearchPopulation : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSearchPopulation()
        {
            InitializeComponent();
            txtSearch.Enabled = false;
        }

        private void rbSearch_CheckedChanged(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtSearch.Enabled = groupBox1.Controls.OfType<RadioButton>().Count(x => x.Checked) > 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Checked = false);
            txtSearch.Clear();
            LoadDefault();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (rbLastName.Checked)
            {
                dgvPopulation.DataSource = null;
                dgvPopulation.DataSource = db.tblIndividuals.Where(x => x.lastName.Contains(txtSearch.Text)).Select(x => new
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
                dgvPopulation.DataSource = null;
                dgvPopulation.DataSource = db.tblIndividuals.Where(x => x.firstName.Contains(txtSearch.Text)).Select(x => new
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = rbFirstName.Checked ? rbFirstName.Text : rbLastName.Checked ? rbLastName.Text : "All";

            for (int i = 1; i < dgvPopulation.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvPopulation.Columns[i].HeaderText;

            for (int i = 0; i < dgvPopulation.Rows.Count; i++)
            {
                for (int j = 1; j < dgvPopulation.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvPopulation.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        public void LoadDefault()
        {
            dgvPopulation.DataSource = db.tblIndividuals.Select(x => new
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

        private void frmSearchPopulation_Load(object sender, EventArgs e)
        {
            LoadDefault();
        }
    }
}
