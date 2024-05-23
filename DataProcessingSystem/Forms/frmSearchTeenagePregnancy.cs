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
    public partial class frmSearchTeenagePregnancy : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSearchTeenagePregnancy()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Teenage Pregnancy";

            for (int i = 1; i < dgvTeenagePregnancy.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvTeenagePregnancy.Columns[i].HeaderText;

            for (int i = 0; i < dgvTeenagePregnancy.Rows.Count; i++)
            {
                for (int j = 1; j < dgvTeenagePregnancy.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvTeenagePregnancy.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != string.Empty)
            {
                int age = int.Parse(txtSearch.Text);
                if (db.tblIndividuals.Count(x => x.Pregnant == "Yes" && x.Age == age && x.Age <= 19) > 0)
                {
                    dgvTeenagePregnancy.DataSource = db.tblIndividuals.Where(x => x.Pregnant == "Yes" && x.Age == age).Select(x => new
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
                    }).OrderBy(x => x.Age).ToList();
                }
                else
                    dgvTeenagePregnancy.DataSource = null;
            }
            else
                LoadDefault();
        }

        private void frmSearchTeenagePregnancy_Load(object sender, EventArgs e)
        {
            LoadDefault();
        }

        public void LoadDefault()
        {
            dgvTeenagePregnancy.DataSource = db.tblIndividuals.Where(x => x.Pregnant == "Yes" && x.Age <= 19).Select(x => new
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
            }).OrderBy(x => x.Age).ToList();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
