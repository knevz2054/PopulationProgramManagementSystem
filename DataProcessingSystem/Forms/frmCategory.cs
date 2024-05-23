using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataProcessingSystem
{
    public partial class frmCategory : Form
    {
        public frmCategory()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {

        }

        private void BtnAddBarangay_Click(object sender, EventArgs e)
        {
            frmAddBarangay frm = new frmAddBarangay();
            frm.ShowDialog();
        }

        private void BtnAddPurok_Click(object sender, EventArgs e)
        {
            frmAddPurok frm = new frmAddPurok();
            frm.ShowDialog();
        }

        private void BtnAddWS_Click(object sender, EventArgs e)
        {
            frmAddWaterSource frm = new frmAddWaterSource();
            frm.ShowDialog();
        }

        private void BtnAddDWS_Click(object sender, EventArgs e)
        {
            frmAddDrinkingWater frm = new frmAddDrinkingWater();
            frm.ShowDialog();
        }

        private void BtnAddTF_Click(object sender, EventArgs e)
        {
            frmAddToiletFacility frm = new frmAddToiletFacility();
            frm.ShowDialog();
        }

        private void BtnAddWD_Click(object sender, EventArgs e)
        {
            frmAddWasteDisposal frm = new frmAddWasteDisposal();
            frm.ShowDialog();
        }

        private void BtnAddFPM_Click(object sender, EventArgs e)
        {
            frmAddFamilyPlanningMethod frm = new frmAddFamilyPlanningMethod();
            frm.ShowDialog();
        }

        private void BtnAddReason_Click(object sender, EventArgs e)
        {
            frmAddReason frm = new frmAddReason();
            frm.ShowDialog();
        }

        private void BtnAddOccupation_Click(object sender, EventArgs e)
        {
            frmAddOccupation frm = new frmAddOccupation();
            frm.ShowDialog();
        }
    }
}
