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
    public partial class frmSearch : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities(); 
        public frmSearch()
        {
            InitializeComponent();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            
        }

        private void btnHousehold_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearchHousehold obj = new frmSearchHousehold();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = false;
            btnPopulation.Enabled = true;
            btnFPuser.Enabled = true;
            btn4Ps.Enabled = true;
            btnPregnant.Enabled = true;
            btnTeenage.Enabled = true;
            btnWomen.Enabled = true;
        }

        private void btnPopulation_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearchPopulation obj = new frmSearchPopulation();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = true;
            btnPopulation.Enabled = false;
            btnFPuser.Enabled = true;
            btn4Ps.Enabled = true;
            btnPregnant.Enabled = true;
            btnTeenage.Enabled = true;
            btnWomen.Enabled = true;
        }

        private void btnFPuser_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearchFPUser obj = new frmSearchFPUser();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = true;
            btnPopulation.Enabled = true;
            btnFPuser.Enabled = false;
            btn4Ps.Enabled = true;
            btnPregnant.Enabled = true;
            btnTeenage.Enabled = true;
            btnWomen.Enabled = true;
        }

        private void btn4Ps_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearch4Ps obj = new frmSearch4Ps();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = true;
            btnPopulation.Enabled = true;
            btnFPuser.Enabled = true;
            btn4Ps.Enabled = false;
            btnPregnant.Enabled = true;
            btnTeenage.Enabled = true;
            btnWomen.Enabled = true;
        }

        private void btnPregnant_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearchPregnant obj = new frmSearchPregnant();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = true;
            btnPopulation.Enabled = true;
            btnFPuser.Enabled = true;
            btn4Ps.Enabled = true;
            btnPregnant.Enabled = false;
            btnTeenage.Enabled = true;
            btnWomen.Enabled = true;
        }

        private void btnTeenage_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearchTeenagePregnancy obj = new frmSearchTeenagePregnancy();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = true;
            btnPopulation.Enabled = true;
            btnFPuser.Enabled = true;
            btn4Ps.Enabled = true;
            btnPregnant.Enabled = true;
            btnTeenage.Enabled = false;
            btnWomen.Enabled = true;
        }

        private void btnWomen_Click(object sender, EventArgs e)
        {
            Application.OpenForms.OfType<Form>().Where(x => x.Name != "Main" && x.Name != "frmAdmin" && x.Name != "frmSearch").ToList().ForEach(x => x.Close());
            frmSearchReproductiveAge obj = new frmSearchReproductiveAge();
            obj.TopLevel = false;
            obj.Dock = DockStyle.Fill;
            obj.Show();
            pnlBody.Controls.Add(obj);


            btnHousehold.Enabled = true;
            btnPopulation.Enabled = true;
            btnFPuser.Enabled = true;
            btn4Ps.Enabled = true;
            btnPregnant.Enabled = true;
            btnTeenage.Enabled = true;
            btnWomen.Enabled = false;
        }
    }
}
