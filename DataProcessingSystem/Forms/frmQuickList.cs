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
    public partial class frmQuickList : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmQuickList()
        {
            InitializeComponent();
        }

        private void frmQuickList_Load(object sender, EventArgs e)
        {
            if (frmViewSummary.Category == "Household")
            {
                dgvList.DataSource = db.tblHouses.Where(x => x.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName)
                .Select(x => new
                {
                    HouseNumber = x.houseNumber,
                    HouseIncome = x.tblIndividuals.Sum(xx => xx.Income),
                    Head = x.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " +
                          x.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " +
                          x.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault().Substring(0, 1) + ".",
                    MemberCounts = x.tblIndividuals.Count(),
                    Purok = x.tblPurok.purokName,
                    WaterSource = x.tblWaterSource.sourceName,
                    DrinkingWaterSource = x.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblWasteDisposal.disposalName,
                }).OrderBy(x => x.Purok).ThenBy(x => x.Head).ToList();

                dgvList.Columns[0].Width = 90;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 150;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 149;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 149;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 149;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 150;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 150;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 150;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 150;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblHouses.Count(x => x.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName);
            }

            if (frmViewSummary.Category == "Population")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName);
                //var bd = DateTime.Now.Year;
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName)
                 .Select(x => new
                 {
                     LastName = x.lastName,
                     FirstName = x.firstName,
                     MiddleName = x.middleName,
                     Gender = x.Gender,
                     CivilStatus = x.civilStatus,
                     BirthDay = x.birthDay,
                     //Age = (bd - (x.birthDay.Year)),
                     Age = x.Age,
                     Purok = x.tblHouse.tblPurok.purokName,
                     HouseID = x.houseID,
                 }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 150;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 150;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 150;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 150;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 150;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 150;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 150;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 150;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 150;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Family Planning Method User")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName != "No Method");
                var bd = DateTime.Now.Year;
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName != "No Method")
                     .Select(x => new
                     {
                         LastName = x.lastName,
                         FirstName = x.firstName,
                         MiddleName = x.middleName,
                         Gender = x.Gender,
                         CivilStatus = x.civilStatus,
                         BirthDay = x.birthDay,
                         Age = (bd - (x.birthDay.Year)),
                         Purok = x.tblHouse.tblPurok.purokName,
                         Method = x.tblFamilyPlanningMethod.methodName,
                         HouseID = x.houseID,
                     }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 146;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[9].Width = 146;
                dgvList.Columns[9].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Unmet")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && xx.wantsTo == "Yes" || xx.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                           || xx.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && xx.tblFamilyPlanningMethod.methodName == "Calendar"
                           || xx.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && xx.tblFamilyPlanningMethod.methodName == "Abstinence" || xx.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && xx.tblFamilyPlanningMethod.methodName == "Herbal");

                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.wantsTo == "Yes" || x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == "Withdrawal"
                           || x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == "Rhythm" || x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == "Calendar"
                           || x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == "Abstinence" || x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == "Herbal")
                     .Select(x => new
                     {
                         LastName = x.lastName,
                         FirstName = x.firstName,
                         MiddleName = x.middleName,
                         Gender = x.Gender,
                         CivilStatus = x.civilStatus,
                         BirthDay = x.birthDay,
                         Age = x.Age,
                         Purok = x.tblHouse.tblPurok.purokName,
                         Method = x.wantsTo == "Yes" ? "No Method/Expressed Interest" : x.tblFamilyPlanningMethod.methodName,
                         HouseID = x.houseID,
                     }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 130;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 175;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[9].Width = 175;
                dgvList.Columns[9].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Teenage Pregnancies")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age <= 19 && x.Pregnant == "Yes");
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age <= 19 && x.Pregnant == "Yes")
                      .Select(x => new
                      {
                          LastName = x.lastName,
                          FirstName = x.firstName,
                          MiddleName = x.middleName,
                          Gender = x.Gender,
                          CivilStatus = x.civilStatus,
                          BirthDay = x.birthDay,
                          Age = x.Age,
                          Purok = x.tblHouse.tblPurok.purokName,
                          HouseID = x.houseID,
                      }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 146;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Pregnant")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName &&  x.Pregnant == "Yes");
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName &&  x.Pregnant == "Yes")
                      .Select(x => new
                      {
                          LastName = x.lastName,
                          FirstName = x.firstName,
                          MiddleName = x.middleName,
                          Gender = x.Gender,
                          CivilStatus = x.civilStatus,
                          BirthDay = x.birthDay,
                          Age = x.Age,
                          Purok = x.tblHouse.tblPurok.purokName,
                          HouseID = x.houseID,
                      }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 146;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Women of Reproductive Age")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Gender == "Female" && x.Age >= 15 && x.Age <= 49);
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Gender == "Female" && x.Age >= 15 && x.Age <= 49)
                      .Select(x => new
                      {
                          LastName = x.lastName,
                          FirstName = x.firstName,
                          MiddleName = x.middleName,
                          Gender = x.Gender,
                          CivilStatus = x.civilStatus,
                          BirthDay = x.birthDay,
                          Age = x.Age,
                          Purok = x.tblHouse.tblPurok.purokName,
                          HouseID = x.houseID,
                      }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[8].Width = 146;
                dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Age")
            {
                if(frmViewSummary.index == 0)
                {
                    lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName  && x.Age >= 0 && x.Age <= 9);
                    dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 0 && x.Age <= 9)
                          .Select(x => new
                          {
                              LastName = x.lastName,
                              FirstName = x.firstName,
                              MiddleName = x.middleName,
                              Gender = x.Gender,
                              CivilStatus = x.civilStatus,
                              BirthDay = x.birthDay,
                              Age = x.Age,
                              Purok = x.tblHouse.tblPurok.purokName,
                          }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                    dgvList.Columns[0].Width = 146;
                    dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[1].Width = 146;
                    dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[2].Width = 146;
                    dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[3].Width = 146;
                    dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[4].Width = 146;
                    dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[5].Width = 146;
                    dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[6].Width = 146;
                    dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[7].Width = 146;
                    dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }

                if (frmViewSummary.index == 1)
                {
                    lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 10 && x.Age <= 19);
                    dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 10 && x.Age <= 19)
                          .Select(x => new
                          {
                              LastName = x.lastName,
                              FirstName = x.firstName,
                              MiddleName = x.middleName,
                              Gender = x.Gender,
                              CivilStatus = x.civilStatus,
                              BirthDay = x.birthDay,
                              Age = x.Age,
                              Purok = x.tblHouse.tblPurok.purokName,
                          }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                    dgvList.Columns[0].Width = 146;
                    dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[1].Width = 146;
                    dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[2].Width = 146;
                    dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[3].Width = 146;
                    dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[4].Width = 146;
                    dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[5].Width = 146;
                    dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[6].Width = 146;
                    dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[7].Width = 146;
                    dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }

                if (frmViewSummary.index == 2)
                {
                    lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 20 && x.Age <= 59);
                    dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 20 && x.Age <= 59)
                          .Select(x => new
                          {
                              LastName = x.lastName,
                              FirstName = x.firstName,
                              MiddleName = x.middleName,
                              Gender = x.Gender,
                              CivilStatus = x.civilStatus,
                              BirthDay = x.birthDay,
                              Age = x.Age,
                              Purok = x.tblHouse.tblPurok.purokName,
                          }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                    dgvList.Columns[0].Width = 146;
                    dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[1].Width = 146;
                    dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[2].Width = 146;
                    dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[3].Width = 146;
                    dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[4].Width = 146;
                    dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[5].Width = 146;
                    dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[6].Width = 146;
                    dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[7].Width = 146;
                    dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }

                if (frmViewSummary.index == 3)
                {
                    lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 60);
                    dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Age >= 60)
                          .Select(x => new
                          {
                              LastName = x.lastName,
                              FirstName = x.firstName,
                              MiddleName = x.middleName,
                              Gender = x.Gender,
                              CivilStatus = x.civilStatus,
                              BirthDay = x.birthDay,
                              Age = x.Age,
                              Purok = x.tblHouse.tblPurok.purokName,
                          }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                    dgvList.Columns[0].Width = 146;
                    dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[1].Width = 146;
                    dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[2].Width = 146;
                    dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[3].Width = 146;
                    dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[4].Width = 146;
                    dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[5].Width = 146;
                    dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[6].Width = 146;
                    dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[7].Width = 146;
                    dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }
            }

            if (frmViewSummary.Category == "Sector")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblOccupation.occupationName == frmViewSummary.Sector && x.Age >= 18);
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblOccupation.occupationName == frmViewSummary.Sector && x.Age >= 18)
                      .Select(x => new
                      {
                          LastName = x.lastName,
                          FirstName = x.firstName,
                          MiddleName = x.middleName,
                          Gender = x.Gender,
                          CivilStatus = x.civilStatus,
                          BirthDay = x.birthDay,
                          Age = x.Age,
                          Purok = x.tblHouse.tblPurok.purokName,
                      }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Gender")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Gender == frmViewSummary.Gender);
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.Gender == frmViewSummary.Gender)
                      .Select(x => new
                      {
                          LastName = x.lastName,
                          FirstName = x.firstName,
                          MiddleName = x.middleName,
                          Gender = x.Gender,
                          CivilStatus = x.civilStatus,
                          BirthDay = x.birthDay,
                          Age = x.Age,
                          Purok = x.tblHouse.tblPurok.purokName,
                      }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "4ps")
            {
                lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.member4ps == "Yes");
                dgvList.DataSource = db.tblIndividuals.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.member4ps == "Yes")
                      .Select(x => new
                      {
                          LastName = x.lastName,
                          FirstName = x.firstName,
                          MiddleName = x.middleName,
                          Gender = x.Gender,
                          CivilStatus = x.civilStatus,
                          BirthDay = x.birthDay,
                          Age = x.Age,
                          Purok = x.tblHouse.tblPurok.purokName,
                      }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                dgvList.Columns[0].Width = 146;
                dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[1].Width = 146;
                dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[2].Width = 146;
                dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[3].Width = 146;
                dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[4].Width = 146;
                dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[5].Width = 146;
                dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[6].Width = 146;
                dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                dgvList.Columns[7].Width = 146;
                dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            }

            if (frmViewSummary.Category == "Method")
            {
                if(frmViewSummary.Method == "Expressed Interest")
                {
                    lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => (x.civilStatus == "Married" || x.civilStatus == "Live-In") && (x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.wantsTo == "Yes"));
                    dgvList.DataSource = db.tblIndividuals.Where(x => (x.civilStatus == "Married" || x.civilStatus == "Live-In") && (x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.wantsTo == "Yes"))
                          .Select(x => new
                          {
                              LastName = x.lastName,
                              FirstName = x.firstName,
                              MiddleName = x.middleName,
                              Gender = x.Gender,
                              CivilStatus = x.civilStatus,
                              BirthDay = x.birthDay,
                              Age = x.Age,
                              Purok = x.tblHouse.tblPurok.purokName,
                              Method = "No Method/Expressed Interest",
                              HouseID = x.houseID,
                          }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                    dgvList.Columns[0].Width = 146;
                    dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[1].Width = 146;
                    dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[2].Width = 146;
                    dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[3].Width = 146;
                    dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[4].Width = 146;
                    dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[5].Width = 146;
                    dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[6].Width = 146;
                    dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[7].Width = 146;
                    dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[8].Width = 146;
                    dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[9].Width = 146;
                    dgvList.Columns[9].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }
                else
                {
                    lblBarangay.Text = frmViewSummary.BrgyName + ": " + db.tblIndividuals.Count(x => (x.civilStatus == "Married" || x.civilStatus == "Live-In") && (x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == frmViewSummary.Method));
                    dgvList.DataSource = db.tblIndividuals.Where(x => (x.civilStatus == "Married" || x.civilStatus == "Live-In") && (x.tblHouse.tblPurok.tblBarangay.brgyName == frmViewSummary.BrgyName && x.tblFamilyPlanningMethod.methodName == frmViewSummary.Method))
                          .Select(x => new
                          {
                              LastName = x.lastName,
                              FirstName = x.firstName,
                              MiddleName = x.middleName,
                              Gender = x.Gender,
                              CivilStatus = x.civilStatus,
                              BirthDay = x.birthDay,
                              Age = x.Age,
                              Purok = x.tblHouse.tblPurok.purokName,
                              Method = x.tblFamilyPlanningMethod.methodName,
                              HouseID = x.houseID,
                          }).OrderBy(x => x.Purok).ThenBy(x => x.LastName).ToList();

                    dgvList.Columns[0].Width = 146;
                    dgvList.Columns[0].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[1].Width = 146;
                    dgvList.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[2].Width = 146;
                    dgvList.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[3].Width = 146;
                    dgvList.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[4].Width = 146;
                    dgvList.Columns[4].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[5].Width = 146;
                    dgvList.Columns[5].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[6].Width = 146;
                    dgvList.Columns[6].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[7].Width = 146;
                    dgvList.Columns[7].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[8].Width = 146;
                    dgvList.Columns[8].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvList.Columns[9].Width = 146;
                    dgvList.Columns[9].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = frmViewSummary.BrgyName;

            for (int i = 1; i <= dgvList.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvList.Columns[i - 1].HeaderText;

            for (int i = 0; i < dgvList.Rows.Count; i++)
            {
                for (int j = 0; j < dgvList.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j + 1] = dgvList.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }
    }
}
