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
    public partial class frmSearchFPUser : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSearchFPUser()
        {
            InitializeComponent();
            pnlGrid.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Checked = false);
            pnlGrid.Visible = false;
            dgvFPuser.DataSource = null;
            cbMethodType.SelectedItem = null;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Method Type";

            for (int i = 1; i < dgvFPuser.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvFPuser.Columns[i].HeaderText;

            for (int i = 0; i < dgvFPuser.Rows.Count; i++)
            {
                for (int j = 1; j < dgvFPuser.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvFPuser.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void cbMethodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlGrid.Visible = cbMethodType.SelectedItem != null;
            if (cbMethodType.SelectedIndex == 0)
            {
                dgvFPuser.DataSource = null;
                dgvFPuser.DataSource = db.tblIndividuals.Where(x => x.tblFamilyPlanningMethod.methodType == "Artificial").Select(x => new
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
                    Method = x.tblFamilyPlanningMethod.methodName,
                    MethodType = x.tblFamilyPlanningMethod.methodType
                }).ToList();
            }

            if (cbMethodType.SelectedIndex == 1)
            {
                dgvFPuser.DataSource = null;
                dgvFPuser.DataSource = db.tblIndividuals.Where(x => x.tblFamilyPlanningMethod.methodType == "Natural").Select(x => new
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
                    Method = x.tblFamilyPlanningMethod.methodName,
                    MethodType = x.tblFamilyPlanningMethod.methodType
                }).ToList();
            }

            if (cbMethodType.SelectedIndex == 2)
            {
                dgvFPuser.DataSource = null;
                dgvFPuser.DataSource = db.tblIndividuals.Where(x => x.tblFamilyPlanningMethod.methodType == "Traditional" && x.tblFamilyPlanningMethod.methodName != "No Method" && x.civilStatus == "Married" || x.civilStatus == "Live-In").Select(x => new
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
                    Method = x.tblFamilyPlanningMethod.methodName,
                    MethodType = x.tblFamilyPlanningMethod.methodType
                }).ToList();
            }
        }
    }
}
