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
    public partial class frmViewSurvey : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static int houseID;
        public static int houseNo;
        public static int surveyID;
        public static int respID;
        public frmViewSurvey()
        {
            InitializeComponent();
        }

        private void frmViewEncoded_Load(object sender, EventArgs e)
        {
            loadGrid();
        }

        private void dgvSurvey_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvSurvey.Columns[e.ColumnIndex].HeaderText == "View")
            {
                houseID = int.Parse(dgvSurvey.CurrentRow.Cells[3].Value.ToString());
                frmSurveyDetail fuh = new frmSurveyDetail();
                fuh.ShowDialog();
            }
            if (dgvSurvey.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                surveyID = int.Parse(dgvSurvey.CurrentRow.Cells[2].Value.ToString());
                string text = string.Format("Are you sure you want to delete selected survey?");
                DialogResult dr = MessageBox.Show(text, "Confirmation", MessageBoxButtons.OKCancel);
                if(dr == DialogResult.OK)
                {
                    respID = db.tblSurveys.Where(x => x.ID == surveyID).Select(x => x.respondentID).SingleOrDefault();
                    houseID = db.tblSurveys.Where(x => x.ID == surveyID).Select(x => x.houseID).SingleOrDefault();
                    houseNo = db.tblHouses.Where(x => x.ID == houseID).Select(x => x.houseNumber).SingleOrDefault();
                    string purok = db.tblHouses.Where(x => x.ID == houseID).Select(x => x.tblPurok.purokName).SingleOrDefault();
                    string brgy = db.tblHouses.Where(x => x.ID == houseID).Select(x => x.tblPurok.tblBarangay.brgyName).SingleOrDefault();

                    tblSurvey survey = db.tblSurveys.Find(surveyID);
                    db.tblSurveys.Remove(survey);
                    db.SaveChanges();

                    tblRespondent respondent = db.tblRespondents.Find(respID);
                    db.tblRespondents.Remove(respondent);
                    db.SaveChanges();

                    List<tblIndividual> listIndi = db.tblIndividuals.Where(x => x.houseID == houseID).ToList();

                    for(int i = 0; i < listIndi.Count(); i++)
                    {
                        db.tblIndividuals.Remove(listIndi[i]);
                        db.SaveChanges();
                    }
                   
                    tblHouse house = db.tblHouses.Find(houseID);
                    db.tblHouses.Remove(house);
                    db.SaveChanges();
                                       
                    tblLog log = new tblLog();
                    string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
                    log.ActivityLog = "Selected survey with Household no. " + houseNo + " in " + purok + " of " + brgy + " has beed remove from list by " + fullName + "...";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();
                }
            }
            loadGrid();
        }
        public void loadGrid()
        {
            if (frmLogin.position == "Barangay Encoder")
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.tblUser.ID == frmLogin.userID).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).SingleOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).SingleOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).SingleOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).ToList();
            }
            else if (frmLogin.position == "Barangay Admin")
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmLogin.access).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).ToList();
            }
            else if(frmLogin.position == "City Encoder")
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.tblUser.ID == frmLogin.userID).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).ToList();
            }
            else
            {
                dgvSurvey.DataSource = db.tblSurveys.Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).OrderBy(x => x.Barangay).ToList();

            }
        }

        private void txtHouseID_TextChanged(object sender, EventArgs e)
        {
            if (frmLogin.position == "Barangay Encoder")
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.tblUser.ID == frmLogin.userID && x.houseID.ToString().Contains(txtHouseID.Text)).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).SingleOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).SingleOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).SingleOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).ToList();
            }
            else if (frmLogin.position == "Barangay Admin")
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.tblHouse.tblPurok.tblBarangay.brgyName == frmLogin.access && x.houseID.ToString().Contains(txtHouseID.Text)).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).ToList();
            }
            else if (frmLogin.position == "City Encoder")
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.tblUser.ID == frmLogin.userID && x.houseID.ToString().Contains(txtHouseID.Text)).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).ToList();
            }
            else
            {
                dgvSurvey.DataSource = db.tblSurveys.Where(x => x.houseID.ToString().Contains(txtHouseID.Text)).Select(x => new
                {
                    Id = x.ID,
                    HouseID = x.tblHouse.ID,
                    HouseNumb = x.tblHouse.houseNumber,
                    Barangay = x.tblHouse.tblPurok.tblBarangay.brgyName,
                    Purok = x.tblHouse.tblPurok.purokName,
                    HeadOfFamily = x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.lastName).FirstOrDefault() + ", " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.firstName).FirstOrDefault() + " " + x.tblHouse.tblIndividuals.Where(xx => xx.Head == "Yes").Select(xx => xx.middleName).FirstOrDefault() + ". ",
                    WaterSource = x.tblHouse.tblWaterSource.sourceName,
                    DrinkingSource = x.tblHouse.tblDrinkingWater.sourceName,
                    ToiletFacility = x.tblHouse.tblToiletFacility.facilityName,
                    WasteDisposal = x.tblHouse.tblWasteDisposal.disposalName,
                    NoOfMember = x.tblHouse.tblIndividuals.Count(),
                }).OrderByDescending(x => x.HouseID).OrderBy(x => x.Barangay).ToList();
            }
        }

    }
}
