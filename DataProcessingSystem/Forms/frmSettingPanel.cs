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
    public partial class frmSettingPanel : Form
    {
        public frmSettingPanel()
        {
            InitializeComponent();
        }

        private void FrmSettingPanel_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmSettingPanel").ToList().ForEach(x => x.Close());
            frmCategory fc = new frmCategory();
            fc.TopLevel = false;
            fc.Dock = DockStyle.Fill;
            fc.Show();
            pnlBody.Controls.Add(fc);

            btnAddCategory.Enabled = false;
            btnCategoryList.Enabled = true;
            btnViewUser.Enabled = true;
            btnViewLog.Enabled = true;
        }

        private void btnViewUser_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmSettingPanel").ToList().ForEach(x => x.Close());
            frmViewUser fvu = new frmViewUser();
            fvu.TopLevel = false;
            fvu.Dock = DockStyle.Fill;
            fvu.Show();
            pnlBody.Controls.Add(fvu);

            btnViewUser.Enabled = false;
            btnCategoryList.Enabled = true;
            btnAddCategory.Enabled = true;
            btnViewLog.Enabled = true;
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmSettingPanel").ToList().ForEach(x => x.Close());
            frmViewLog fvl = new frmViewLog();
            fvl.TopLevel = false;
            fvl.Dock = DockStyle.Fill;
            fvl.Show();
            pnlBody.Controls.Add(fvl);

            btnViewUser.Enabled = true;
            btnCategoryList.Enabled = true;
            btnAddCategory.Enabled = true;
            btnViewLog.Enabled = false;
        }

        private void btnCategoryList_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmSettingPanel").ToList().ForEach(x => x.Close());
            frmCategoryList fcl = new frmCategoryList();
            fcl.TopLevel = false;
            fcl.Dock = DockStyle.Fill;
            fcl.Show();
            pnlBody.Controls.Add(fcl);

            btnViewUser.Enabled = true;
            btnCategoryList.Enabled = false;
            btnAddCategory.Enabled = true;
            btnViewLog.Enabled = true;
        }
    }
}
