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
    public partial class frmSearchPregnant : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSearchPregnant()
        {
            InitializeComponent();
        }

        private void frmSearchPregnant_Load(object sender, EventArgs e)
        {
            dgvPregnant.DataSource = db.tblIndividuals.Where(x => x.Pregnant == "Yes").Select(x => new
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
            }).OrderBy(x => x.Age).ToList();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Catbalogan City";

            for (int i = 1; i < dgvPregnant.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvPregnant.Columns[i].HeaderText;

            for (int i = 0; i < dgvPregnant.Rows.Count; i++)
            {
                for (int j = 1; j < dgvPregnant.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvPregnant.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }
    }
}
