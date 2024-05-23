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
    public partial class frmSearchHousehold : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSearchHousehold()
        {
            InitializeComponent();
            foreach (var item in db.tblBarangays)
                cbBarangay.Items.Add(item.brgyName);

            pnlGrid.Visible = false;
            groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Enabled = false);
        }

        private void frmSearchHousehold_Load(object sender, EventArgs e)
        {

        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            btnView.Enabled = groupBox1.Controls.OfType<RadioButton>().Count(x => x.Checked) > 0 && cbBarangay.SelectedItem != null;
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (rbNumber.Checked)
            {
                dgvHousehold.DataSource = db.tblHouses.Where(x => x.tblPurok.tblBarangay.brgyName == cbBarangay.Text).Select(x => new
                {
                    ID  = x.ID,
                    HouseNo = x.houseNumber,
                    Member = x.tblIndividuals.Count()
                }).OrderByDescending(x => x.Member).ToList();

                pnlGrid.Visible = true;
            }

            if (rbIncome.Checked)
            {
                dgvHousehold.DataSource = db.tblHouses.Where(x => x.tblPurok.tblBarangay.brgyName == cbBarangay.Text).Select(x => new
                {
                    ID = x.ID,
                    HouseNo = x.houseNumber,
                    Income = x.tblIndividuals.Sum(xx => xx.Income),
                }).OrderByDescending(x => x.Income).ToList();

                pnlGrid.Visible = true;
            }

            if (rbToilet.Checked)
            {
                dgvHousehold.DataSource = db.tblHouses.Where(x => x.tblPurok.tblBarangay.brgyName == cbBarangay.Text && x.tblToiletFacility.facilityName == "Without Toilet").Select(x => new
                {
                    ID = x.ID,
                    HouseNo = x.houseNumber,
                    Toilet = x.tblToiletFacility.facilityName,
                    WaterSource = x.tblWaterSource.sourceName,
                    DrinkingWater = x.tblDrinkingWater.sourceName,
                    WasteDisposal = x.tblWasteDisposal.disposalName
                }).ToList();

                pnlGrid.Visible = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbBarangay.SelectedItem = null;
            dgvHousehold.DataSource = null;
            groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Checked = false);
            pnlGrid.Visible = false;
        }

        private void cbBarangay_SelectedIndexChanged(object sender, EventArgs e)
        {
           if(cbBarangay.SelectedItem == null)
                groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Enabled = false);

           else
                groupBox1.Controls.OfType<RadioButton>().ToList().ForEach(x => x.Enabled = true);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            /*Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = cbBarangay.Text;

            for (int i = 1; i <= dgvHousehold.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvHousehold.Columns[i - 1].HeaderText;

            for (int i = 0; i < dgvHousehold.Rows.Count; i++)
            {
                for (int j = 0; j < dgvHousehold.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j + 1] = dgvHousehold.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;*/

            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = cbBarangay.Text;

            for (int i = 1; i < dgvHousehold.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvHousehold.Columns[i].HeaderText;

            for (int i = 0; i < dgvHousehold.Rows.Count; i++)
            {
                for (int j = 1; j < dgvHousehold.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvHousehold.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }
    }
}
