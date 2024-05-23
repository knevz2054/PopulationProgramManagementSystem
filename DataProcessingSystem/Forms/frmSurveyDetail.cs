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
    public partial class frmSurveyDetail : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmSurveyDetail()
        {
            InitializeComponent();
        }

        private void frmUpdateHouse_Load(object sender, EventArgs e)
        {
            int personID = db.tblIndividuals.Where(x => x.tblHouse.ID == frmViewSurvey.houseID && x.Head == "Yes").Select(x => x.ID).SingleOrDefault();
            tblIndividual individual = db.tblIndividuals.Find(personID);

            lblHead.Text = individual.lastName + ", " + individual.firstName + " " + individual.middleName;

            lblBarangay.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblPurok.tblBarangay.brgyName).SingleOrDefault().ToString();
            lblPurok.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblPurok.purokName).SingleOrDefault().ToString();
            lblHouseNo.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.houseNumber).SingleOrDefault().ToString();
            lblAddress.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.houseAddress).SingleOrDefault().ToString();
            lblMembersCount.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblIndividuals.Count()).SingleOrDefault().ToString();
            lblWaterSource.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblWaterSource.sourceName).SingleOrDefault().ToString();
            lblDrinkingWater.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblDrinkingWater.sourceName).SingleOrDefault().ToString();
            lblToiletFacility.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblToiletFacility.facilityName).SingleOrDefault().ToString();
            lblWasteDisposal.Text = db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblWasteDisposal.disposalName).SingleOrDefault().ToString();
            lblFamilyIncome.Text = "Php " + db.tblSurveys.Where(x => x.houseID == frmViewSurvey.houseID).Select(x => x.tblHouse.tblIndividuals.Sum(xx => xx.Income)).SingleOrDefault().ToString();

            foreach (var item in db.tblIndividuals.Where(x => x.houseID == frmViewSurvey.houseID))
            {
                ListViewItem lvi = new ListViewItem(item.lastName);
                lvi.SubItems.Add(item.firstName);
                lvi.SubItems.Add(item.middleName);
                lvi.SubItems.Add(item.Gender);
                lvi.SubItems.Add(item.birthDay.ToShortDateString());
                lvi.SubItems.Add(item.Age.ToString());
                lvi.SubItems.Add(item.civilStatus);
                lvi.SubItems.Add(item.tblOccupation.occupationName);
                lvi.SubItems.Add(item.Income.ToString());
                lvi.SubItems.Add(item.Relationship);
                lvi.SubItems.Add(item.member4ps);
                lvi.SubItems.Add(item.wantsTo);
                lvi.SubItems.Add(item.Pregnant);
                lvi.SubItems.Add(item.tblFamilyPlanningMethod.methodName);
                lvi.SubItems.Add(item.tblReason.reasonName);
                lvMember.Items.Add(lvi);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
