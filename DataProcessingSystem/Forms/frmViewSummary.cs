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
    public partial class frmViewSummary : Form
    {
        public static string BrgyName;
        public static string Category;
        public static string Sector;
        public static string Age;
        public static string Gender;
        public static string Method;
        public static int index;
        public static string graphName;
        public frmViewSummary()
        {
            InitializeComponent();
            householdGraph.Enabled = frmLogin.position != "Barangay Admin";
            populationGraph.Enabled = frmLogin.position != "Barangay Admin";
            fpGraph.Enabled = frmLogin.position != "Barangay Admin";
            unmetGraph.Enabled = frmLogin.position != "Barangay Admin";
            teenageGraph.Enabled = frmLogin.position != "Barangay Admin";
            pregnantGraph.Enabled = frmLogin.position != "Barangay Admin";
            reproductiveGraph.Enabled = frmLogin.position != "Barangay Admin";
            ageGraph.Enabled = frmLogin.position != "Barangay Admin";
            sectorGraph.Enabled = frmLogin.position != "Barangay Admin";
            genderGraph.Enabled = frmLogin.position != "Barangay Admin";
            _4psGraph.Enabled = frmLogin.position != "Barangay Admin";
            methodGraph.Enabled = frmLogin.position != "Barangay Admin";
        }

        private void frmViewSummary_Load(object sender, EventArgs e)
        {

            using (DataProcessingSystemEntities db = new DataProcessingSystemEntities())
            {
                foreach (var item in db.tblOccupations)
                    cbSector.Items.Add(item.occupationName);

                foreach (var item in db.tblFamilyPlanningMethods)
                    cbMethod.Items.Add(item.methodName);

                cbMethod.Items.Add("Expressed Interest");

                if (frmLogin.position == "City Admin" || frmLogin.position == "System Admin" || frmLogin.position == "Viewer")
                {
                    //Households Summary
                    lblHousehold.Text = lblHousehold.Text + "  " + db.tblHouses.Count().ToString();
                    var listHouse = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName),
                        Percentage = (db.tblHouses.Count() == 0) ? "0%" : ((decimal)(db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName)) / ((decimal)db.tblHouses.Count()) * 100) + "%",
                    }).OrderByDescending(x => x.Count).ToList();
                    dgvHousehold.DataSource = listHouse;
                    dgvHousehold.Columns[1].Width = 142;
                    dgvHousehold.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvHousehold.Columns[2].Width = 90;
                    dgvHousehold.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvHousehold.Columns[3].Width = 90;
                    dgvHousehold.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Population Summary
                    lblPopulation.Text = lblPopulation.Text + "  " + db.tblIndividuals.Count().ToString();
                    var listPopulation = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName),
                        Percentage = (db.tblIndividuals.Count() == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName)) / ((decimal)db.tblIndividuals.Count()) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvPopulation.DataSource = listPopulation;
                    dgvPopulation.Columns[1].Width = 142;
                    dgvPopulation.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPopulation.Columns[2].Width = 90;
                    dgvPopulation.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPopulation.Columns[3].Width = 90;
                    dgvPopulation.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Family Planning Users Summary
                    lblFP.Text = lblFP.Text + "  " + db.tblIndividuals.Count(x => x.tblFamilyPlanningMethod.methodName != "No Method").ToString();
                    var listFP = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName != "No Method"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName != "No Method") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName != "No Method")) / ((decimal)db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName != "No Method")) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvFP.DataSource = listFP;
                    dgvFP.Columns[1].Width = 142;
                    dgvFP.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvFP.Columns[2].Width = 90;
                    dgvFP.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvFP.Columns[3].Width = 90;
                    dgvFP.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Unmet Needs Summary
                    lblUnmet.Text = lblUnmet.Text + "  " + db.tblIndividuals.Count(x => x.wantsTo == "Yes" || x.tblFamilyPlanningMethod.methodName == "Withdrawal"
                    || x.tblFamilyPlanningMethod.methodName == "Rhythm" || x.tblFamilyPlanningMethod.methodName == "Calendar" || x.tblFamilyPlanningMethod.methodName == "Abstinence"
                    || x.tblFamilyPlanningMethod.methodName == "Herbal").ToString();

                    var listUnmet = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Calendar"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Abstinence" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Herbal"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.wantsTo == "Yes" || xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblFamilyPlanningMethod.methodName == "Calendar" || xx.tblFamilyPlanningMethod.methodName == "Abstinence"
                            || xx.tblFamilyPlanningMethod.methodName == "Herbal") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Calendar"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Abstinence" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Herbal")) / ((decimal)db.tblIndividuals.Count(xx => xx.wantsTo == "Yes" || xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblFamilyPlanningMethod.methodName == "Calendar" || xx.tblFamilyPlanningMethod.methodName == "Abstinence"
                            || xx.tblFamilyPlanningMethod.methodName == "Herbal")) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvUnmet.DataSource = listUnmet;
                    dgvUnmet.Columns[1].Width = 142;
                    dgvUnmet.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvUnmet.Columns[2].Width = 90;
                    dgvUnmet.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvUnmet.Columns[3].Width = 90;
                    dgvUnmet.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Teenage Pregnancies Summary
                    lblTeenage.Text = lblTeenage.Text + "  " + db.tblIndividuals.Count(x => x.Pregnant == "Yes" && x.Age <= 19).ToString();
                    var listTeenage = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes" && xx.Age <= 19),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Pregnant == "Yes" && xx.Age <= 19) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes" && xx.Age <= 19)) / ((decimal)db.tblIndividuals.Count(xx => xx.Pregnant == "Yes" && xx.Age <= 19)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvTeenage.DataSource = listTeenage;
                    dgvTeenage.Columns[1].Width = 142;
                    dgvTeenage.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvTeenage.Columns[2].Width = 90;
                    dgvTeenage.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvTeenage.Columns[3].Width = 90;
                    dgvTeenage.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Pregnant Summary
                    lblPregnant.Text = lblPregnant.Text + "  " + db.tblIndividuals.Count(x => x.Pregnant == "Yes").ToString();
                    var listPregnant = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Pregnant == "Yes") == 0) ? "0%" : ((db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes")) / ((decimal)db.tblIndividuals.Count(xx => xx.Pregnant == "Yes")) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvPregnant.DataSource = listPregnant;
                    dgvPregnant.Columns[1].Width = 142;
                    dgvPregnant.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPregnant.Columns[2].Width = 90;
                    dgvPregnant.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPregnant.Columns[3].Width = 90;
                    dgvPregnant.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Women of Reproductive Age Summary
                    lblReproductive.Text = lblReproductive.Text + "  " + db.tblIndividuals.Count(x => x.Gender == "Female" && x.Age >= 15 && x.Age <= 49).ToString();
                    var listReproductive = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49)) / ((decimal)db.tblIndividuals.Count(xx => xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvReproductive.DataSource = listReproductive;
                    dgvReproductive.Columns[1].Width = 142;
                    dgvReproductive.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvReproductive.Columns[2].Width = 90;
                    dgvReproductive.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvReproductive.Columns[3].Width = 90;
                    dgvReproductive.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //4ps Summary
                    lbl4ps.Text = lbl4ps.Text + "  " + db.tblIndividuals.Count(x => x.member4ps == "Yes").ToString();
                    var list4ps = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.member4ps == "Yes"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.member4ps == "Yes") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.member4ps == "Yes")) / ((decimal)db.tblIndividuals.Count(xx => xx.member4ps == "Yes")) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgv4ps.DataSource = list4ps;
                    dgv4ps.Columns[1].Width = 142;
                    dgv4ps.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgv4ps.Columns[2].Width = 90;
                    dgv4ps.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgv4ps.Columns[3].Width = 90;
                    dgv4ps.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }

                if (frmLogin.position == "Barangay Admin")
                {
                    //Households Summary
                    lblHousehold.Text = lblHousehold.Text + "  " + db.tblHouses.Count().ToString();
                    var listHouse = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName), //Percentage = ((db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName)) / (db.tblHouses.Count()) * 100) + "%",
                        Percentage = (db.tblHouses.Count() == 0) ? "0%" : ((decimal)(db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName)) / ((decimal)db.tblHouses.Count()) * 100) + "%",

                    }).ToList();
                    dgvHousehold.DataSource = listHouse;
                    dgvHousehold.Columns[1].Width = 142;
                    dgvHousehold.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvHousehold.Columns[2].Width = 90;
                    dgvHousehold.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvHousehold.Columns[3].Width = 90;
                    dgvHousehold.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Population Summary
                    lblPopulation.Text = lblPopulation.Text + "  " + db.tblIndividuals.Count().ToString();
                    var listPopulation = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName),
                        Percentage = (db.tblIndividuals.Count() == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName)) / ((decimal)db.tblIndividuals.Count()) * 100) + "%",

                    }).ToList();
                    dgvPopulation.DataSource = listPopulation;
                    dgvPopulation.Columns[1].Width = 142;
                    dgvPopulation.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPopulation.Columns[2].Width = 90;
                    dgvPopulation.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPopulation.Columns[3].Width = 90;
                    dgvPopulation.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Family Planning Users Summary
                    lblFP.Text = lblFP.Text + "  " + db.tblIndividuals.Count(x => x.tblFamilyPlanningMethod.methodName != "No Method").ToString();
                    var listFP = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName != "No Method"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName != "No Method") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName != "No Method")) / ((decimal)db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName != "No Method")) * 100) + "%",

                    }).ToList();
                    dgvFP.DataSource = listFP;
                    dgvFP.Columns[1].Width = 142;
                    dgvFP.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvFP.Columns[2].Width = 90;
                    dgvFP.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvFP.Columns[3].Width = 90;
                    dgvFP.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Unemet Needs Summary
                    lblUnmet.Text = lblUnmet.Text + "  " + db.tblIndividuals.Count(x => x.wantsTo == "Yes" || x.tblFamilyPlanningMethod.methodName == "Withdrawal"
                    || x.tblFamilyPlanningMethod.methodName == "Rhythm" || x.tblFamilyPlanningMethod.methodName == "Calendar" || x.tblFamilyPlanningMethod.methodName == "Abstinence"
                    || x.tblFamilyPlanningMethod.methodName == "Herbal").ToString();

                    var listUnmet = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Calendar"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Abstinence" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Herbal"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.wantsTo == "Yes" || xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblFamilyPlanningMethod.methodName == "Calendar" || xx.tblFamilyPlanningMethod.methodName == "Abstinence"
                            || xx.tblFamilyPlanningMethod.methodName == "Herbal") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Calendar"
                            || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Abstinence" || xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == "Herbal")) / ((decimal)db.tblIndividuals.Count(xx => xx.wantsTo == "Yes" || xx.tblFamilyPlanningMethod.methodName == "Withdrawal"
                            || xx.tblFamilyPlanningMethod.methodName == "Rhythm" || xx.tblFamilyPlanningMethod.methodName == "Calendar" || xx.tblFamilyPlanningMethod.methodName == "Abstinence"
                            || xx.tblFamilyPlanningMethod.methodName == "Herbal")) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvUnmet.DataSource = listUnmet;
                    dgvUnmet.Columns[1].Width = 142;
                    dgvUnmet.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvUnmet.Columns[2].Width = 90;
                    dgvUnmet.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvUnmet.Columns[3].Width = 90;
                    dgvUnmet.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Teenage Pregnancies Summary
                    lblTeenage.Text = lblTeenage.Text + "  " + db.tblIndividuals.Count(x => x.Pregnant == "Yes" && x.Age <= 19).ToString();
                    var listTeenage = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes" && xx.Age <= 19),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Pregnant == "Yes" && xx.Age <= 19) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes" && xx.Age <= 19)) / ((decimal)db.tblIndividuals.Count(xx => xx.Pregnant == "Yes" && xx.Age <= 19)) * 100) + "%",

                    }).ToList();
                    dgvTeenage.DataSource = listTeenage;
                    dgvTeenage.Columns[1].Width = 142;
                    dgvTeenage.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvTeenage.Columns[2].Width = 90;
                    dgvTeenage.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvTeenage.Columns[3].Width = 90;
                    dgvTeenage.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Pregnant Summary
                    lblPregnant.Text = lblPregnant.Text + "  " + db.tblIndividuals.Count(x => x.Pregnant == "Yes").ToString();
                    var listPregnant = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Pregnant == "Yes") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes")) / ((decimal)db.tblIndividuals.Count(xx => xx.Pregnant == "Yes")) * 100) + "%",

                    }).ToList();
                    dgvPregnant.DataSource = listPregnant;
                    dgvPregnant.Columns[1].Width = 142;
                    dgvPregnant.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPregnant.Columns[2].Width = 90;
                    dgvPregnant.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvPregnant.Columns[3].Width = 90;
                    dgvPregnant.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //Women of Reproductive Age Summary
                    lblReproductive.Text = lblReproductive.Text + "  " + db.tblIndividuals.Count(x => x.Gender == "Female" && x.Age >= 15 && x.Age <= 49).ToString();
                    var listReproductive = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49)) / ((decimal)db.tblIndividuals.Count(xx => xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49)) * 100) + "%",

                    }).ToList();
                    dgvReproductive.DataSource = listReproductive;
                    dgvReproductive.Columns[1].Width = 142;
                    dgvReproductive.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvReproductive.Columns[2].Width = 90;
                    dgvReproductive.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvReproductive.Columns[3].Width = 90;
                    dgvReproductive.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                    //4ps Summary
                    lbl4ps.Text = lbl4ps.Text + "  " + db.tblIndividuals.Count(x => x.member4ps == "Yes").ToString();
                    var list4ps = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.member4ps == "Yes"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.member4ps == "Yes") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.member4ps == "Yes")) / ((decimal)db.tblIndividuals.Count(xx => xx.member4ps == "Yes")) * 100) + "%",

                    }).ToList();
                    dgv4ps.DataSource = list4ps;
                    dgv4ps.Columns[1].Width = 142;
                    dgv4ps.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgv4ps.Columns[2].Width = 90;
                    dgv4ps.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgv4ps.Columns[3].Width = 90;
                    dgv4ps.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }
            }
        }

        private void cbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSector.Text = "Pop. by Sector:";
            Sector = cbSector.Text;
            using (DataProcessingSystemEntities db = new DataProcessingSystemEntities())
            {
                if (frmLogin.position == "City Admin" || frmLogin.position == "System Admin")
                {
                    //Population by Sector Summary
                    lblSector.Text = lblSector.Text + "  " + db.tblIndividuals.Count(x => x.tblOccupation.occupationName == cbSector.Text && x.Age >= 18).ToString();
                    var listSector = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18),
                        Percentage = (db.tblIndividuals.Count(xx => xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18)) / ((decimal)db.tblIndividuals.Count(xx => xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvSector.DataSource = listSector;
                    dgvSector.Columns[1].Width = 142;
                    dgvSector.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvSector.Columns[2].Width = 90;
                    dgvSector.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvSector.Columns[3].Width = 90;
                    dgvSector.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }

                if (frmLogin.position == "Barangay Admin")
                {
                    //Population by Sector Summary
                    lblSector.Text = lblSector.Text + "  " + db.tblIndividuals.Count(x => x.tblOccupation.occupationName == cbSector.Text && x.Age >= 18).ToString();
                    var listSector = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18),
                        Percentage = (db.tblIndividuals.Count(xx => xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18)) / ((decimal)db.tblIndividuals.Count(xx => xx.tblOccupation.occupationName == cbSector.Text && xx.Age >= 18)) * 100) + "%",

                    }).ToList();
                    dgvSector.DataSource = listSector;
                    dgvSector.Columns[1].Width = 142;
                    dgvSector.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvSector.Columns[2].Width = 90;
                    dgvSector.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvSector.Columns[3].Width = 90;
                    dgvSector.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }
            }
        }

        private void cbAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblAge.Text = "Pop. by Age:";
            using (DataProcessingSystemEntities db = new DataProcessingSystemEntities())
            {
                if (frmLogin.position == "City Admin" || frmLogin.position == "System Admin")
                {
                    //Population by Age Category Summary
                    if (cbAge.SelectedIndex == 0)
                    {
                        //Child Age Summary
                        Age = "Child";
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 0 && x.Age <= 9).ToString();
                        var listChild = db.tblBarangays.Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 0 && xx.Age <= 9),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 0 && xx.Age <= 9) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 0 && xx.Age <= 9)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 0 && xx.Age <= 9)) * 100) + "%",

                        }).OrderByDescending(x => x.Count).ToList();
                        dgvAge.DataSource = listChild;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    if (cbAge.SelectedIndex == 1)
                    {
                        //Adolescent Age Summary
                        Age = "Adolescent";
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 10 && x.Age <= 19).ToString();
                        var listAdolescent = db.tblBarangays.Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 10 && xx.Age <= 19),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 10 && xx.Age <= 19) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 10 && xx.Age <= 19)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 10 && xx.Age <= 19)) * 100) + "%",

                        }).OrderByDescending(x => x.Count).ToList();
                        dgvAge.DataSource = listAdolescent;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    if (cbAge.SelectedIndex == 2)
                    {
                        //Adult Age Summary
                        Age = "Adult";
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 20 && x.Age <= 59).ToString();
                        var listAdult = db.tblBarangays.Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 20 && xx.Age <= 59),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 20 && xx.Age <= 59) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 20 && xx.Age <= 59)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 20 && xx.Age <= 59)) * 100) + "%",

                        }).OrderByDescending(x => x.Count).ToList();
                        dgvAge.DataSource = listAdult;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    if (cbAge.SelectedIndex == 3)
                    {
                        //Senior Citizen Summary
                        Age = "SC";
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 60).ToString();
                        var listSenior = db.tblBarangays.Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 60),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 60) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 60)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 60)) * 100) + "%",

                        }).OrderByDescending(x => x.Count).ToList();
                        dgvAge.DataSource = listSenior;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }
                }

                if (frmLogin.position == "Barangay Admin")
                {
                    //Population by Age Category Summary
                    if (cbAge.SelectedIndex == 0)
                    {
                        //Child Age Summary
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 0 && x.Age <= 9).ToString();
                        var listChild = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 0 && xx.Age <= 9),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 0 && xx.Age <= 9) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 0 && xx.Age <= 9)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 0 && xx.Age <= 9)) * 100) + "%",

                        }).ToList();
                        dgvAge.DataSource = listChild;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    if (cbAge.SelectedIndex == 1)
                    {
                        //Adolescent Age Summary
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 10 && x.Age <= 19).ToString();
                        var listAdolescent = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 10 && xx.Age <= 19),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 10 && xx.Age <= 19) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 10 && xx.Age <= 19)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 10 && xx.Age <= 19)) * 100) + "%",

                        }).ToList();
                        dgvAge.DataSource = listAdolescent;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    if (cbAge.SelectedIndex == 2)
                    {
                        //Adult Age Summary
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 20 && x.Age <= 59).ToString();
                        var listAdult = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 20 && xx.Age <= 59),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 20 && xx.Age <= 59) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 20 && xx.Age <= 59)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 20 && xx.Age <= 59)) * 100) + "%",

                        }).ToList();
                        dgvAge.DataSource = listAdult;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }

                    if (cbAge.SelectedIndex == 3)
                    {
                        //Senior Citizen Summary
                        lblAge.Text = lblAge.Text + "  " + db.tblIndividuals.Count(x => x.Age >= 60).ToString();
                        var listSenior = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 60),
                            Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 60) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 60)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 60)) * 100) + "%",

                        }).ToList();
                        dgvAge.DataSource = listSenior;
                        dgvAge.Columns[1].Width = 142;
                        dgvAge.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[2].Width = 90;
                        dgvAge.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvAge.Columns[3].Width = 90;
                        dgvAge.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }
                }
            }
        }

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblGender.Text = "Pop. by Gender:";
            Gender = cbGender.Text;
            //Population By Gender Summary
            using (DataProcessingSystemEntities db = new DataProcessingSystemEntities())
            {
                Gender = cbGender.Text;
                if (frmLogin.position == "City Admin" || frmLogin.position == "System Admin")
                {
                    lblGender.Text = lblGender.Text + "  " + db.tblIndividuals.Count(x => x.Gender == cbGender.Text).ToString();
                    var listGen = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == cbGender.Text),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Gender == cbGender.Text) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == cbGender.Text)) / ((decimal)db.tblIndividuals.Count(xx => xx.Gender == cbGender.Text)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    dgvGender.DataSource = listGen;
                    dgvGender.Columns[1].Width = 142;
                    dgvGender.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvGender.Columns[2].Width = 90;
                    dgvGender.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvGender.Columns[3].Width = 90;
                    dgvGender.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

                }

                if (frmLogin.position == "Barangay Admin")
                {
                    lblGender.Text = lblGender.Text + "  " + db.tblIndividuals.Count(x => x.Gender == cbGender.Text).ToString();
                    var listGen = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == cbGender.Text),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Gender == cbGender.Text) == 0) ? "0 %" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == cbGender.Text)) / ((decimal)db.tblIndividuals.Count(xx => xx.Gender == cbGender.Text)) * 100) + "%",

                    }).ToList();
                    dgvGender.DataSource = listGen;
                    dgvGender.Columns[1].Width = 142;
                    dgvGender.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvGender.Columns[2].Width = 90;
                    dgvGender.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    dgvGender.Columns[3].Width = 90;
                    dgvGender.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                }
            }
        }

        private void cbMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMethod.Text = "FP Method Used:";
            Method = cbMethod.Text;
            //Population By Gender Summary
            using (DataProcessingSystemEntities db = new DataProcessingSystemEntities())
            {
                if (frmLogin.position == "City Admin" || frmLogin.position == "System Admin")
                {
                    if (cbMethod.Text == "Expressed Interest")
                    {
                        lblMethod.Text = lblMethod.Text + "  " + db.tblIndividuals.Count(x => (x.wantsTo == "Yes") && (x.civilStatus == "Married" || x.civilStatus == "Live-In")).ToString();
                        var listMethod = db.tblBarangays.Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => (xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In")),
                            Percentage = (db.tblIndividuals.Count(xx => (xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => (xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In"))) / ((decimal)db.tblIndividuals.Count(xx => (xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In"))) * 100) + "%",

                        }).OrderByDescending(x => x.Count).ToList();
                        dgvMethod.DataSource = listMethod;
                        dgvMethod.Columns[1].Width = 142;
                        dgvMethod.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[2].Width = 90;
                        dgvMethod.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[3].Width = 90;
                        dgvMethod.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }
                    else
                    {
                        lblMethod.Text = lblMethod.Text + "  " + db.tblIndividuals.Count(x => (x.tblFamilyPlanningMethod.methodName == cbMethod.Text) && (x.civilStatus == "Married" || x.civilStatus == "Live-In")).ToString();
                        var listMethod = db.tblBarangays.Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => (xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == cbMethod.Text) && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In")),
                            Percentage = (db.tblIndividuals.Count(xx => (xx.tblFamilyPlanningMethod.methodName == cbMethod.Text) && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => (xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == cbMethod.Text) && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In"))) / ((decimal)db.tblIndividuals.Count(xx => (xx.tblFamilyPlanningMethod.methodName == cbMethod.Text) && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In"))) * 100) + "%",

                        }).OrderByDescending(x => x.Count).ToList();
                        dgvMethod.DataSource = listMethod;
                        dgvMethod.Columns[1].Width = 142;
                        dgvMethod.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[2].Width = 90;
                        dgvMethod.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[3].Width = 90;
                        dgvMethod.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }
                }

                if (frmLogin.position == "Barangay Admin")
                {
                    if (cbMethod.Text == "Expressed Interest")
                    {
                        lblMethod.Text = lblMethod.Text + "  " + db.tblIndividuals.Count(x => x.wantsTo == "Yes" && x.civilStatus == "Married" || x.civilStatus == "Live-In").ToString();
                        var listMethod = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes" && xx.civilStatus == "Married" || xx.civilStatus == "Live-In"),
                            Percentage = (db.tblIndividuals.Count(xx => xx.wantsTo == "Yes" && xx.civilStatus == "Married" || xx.civilStatus == "Live-In") == 0) ? "0 %" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes" && xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) / ((decimal)db.tblIndividuals.Count(xx => xx.wantsTo == "Yes" && xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) * 100) + "%",

                        }).ToList();
                        dgvMethod.DataSource = listMethod;
                        dgvMethod.Columns[1].Width = 142;
                        dgvMethod.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[2].Width = 90;
                        dgvMethod.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[3].Width = 90;
                        dgvMethod.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }
                    else
                    {
                        lblMethod.Text = lblMethod.Text + "  " + db.tblIndividuals.Count(x => x.tblFamilyPlanningMethod.methodName == cbMethod.Text && x.civilStatus == "Married" || x.civilStatus == "Live-In").ToString();
                        var listMethod = db.tblBarangays.Where(x => x.brgyName == frmLogin.access).Select(x => new
                        {
                            Barangay = x.brgyName,
                            Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == cbMethod.Text && xx.civilStatus == "Married" || xx.civilStatus == "Live-In"),
                            Percentage = (db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName == cbMethod.Text && xx.civilStatus == "Married" || xx.civilStatus == "Live-In") == 0) ? "0 %" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == cbMethod.Text && xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) / ((decimal)db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName == cbMethod.Text && xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) * 100) + "%",

                        }).ToList();
                        dgvMethod.DataSource = listMethod;
                        dgvMethod.Columns[1].Width = 142;
                        dgvMethod.Columns[1].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[2].Width = 90;
                        dgvMethod.Columns[2].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                        dgvMethod.Columns[3].Width = 90;
                        dgvMethod.Columns[3].HeaderCell.Style.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                    }
                }
            }
        }

        private void dgvHousehold_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Household";
            if (dgvHousehold.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvHousehold.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvPopulation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Population";
            if (dgvPopulation.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvPopulation.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvFP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Family Planning Method User";
            if (dgvFP.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvFP.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvUnmet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Unmet";
            if (dgvUnmet.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvUnmet.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvTeenage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Teenage Pregnancies";
            if (dgvTeenage.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvTeenage.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvPregnant_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Pregnant";
            if (dgvPregnant.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvPregnant.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvReproductive_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Women of Reproductive Age";
            if (dgvReproductive.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvReproductive.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvAge_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            index = cbAge.SelectedIndex;
            Category = "Age";
            if (dgvAge.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvAge.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvSector_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Sector";
            Sector = cbSector.Text;
            if (dgvSector.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvSector.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvGender_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Gender";
            Gender = cbGender.Text;
            if (dgvGender.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvGender.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgv4ps_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "4ps";
            if (dgv4ps.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgv4ps.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void dgvMethod_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Category = "Method";
            Method = cbMethod.Text;
            if (dgvMethod.Columns[e.ColumnIndex].HeaderText == "View")
            {
                BrgyName = dgvMethod.CurrentRow.Cells[1].Value.ToString();
                frmQuickList fhl = new frmQuickList();
                fhl.ShowDialog();
            }
        }

        private void btnHousehold_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Households";

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

        private void btnPopulation_Click(object sender, EventArgs e)
        {
            /*Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            ExlApp.Application.Workbooks.Add(Type.Missing);

            for (int i = 1; i < dgvPopulation.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvPopulation.Columns[i].HeaderText;

            for (int i = 0; i < dgvPopulation.Rows.Count; i++)
            {
                for (int j = 1; j < dgvPopulation.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvPopulation.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;*/

            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Population";

            for (int i = 1; i < dgvPopulation.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvPopulation.Columns[i].HeaderText;

            for (int i = 0; i < dgvPopulation.Rows.Count; i++)
            {
                for (int j = 1; j < dgvPopulation.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvPopulation.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnFPuser_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Family Planning Users";

            for (int i = 1; i < dgvFP.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvFP.Columns[i].HeaderText;

            for (int i = 0; i < dgvFP.Rows.Count; i++)
            {
                for (int j = 1; j < dgvFP.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvFP.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnUnmet_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Unmet Needs";

            for (int i = 1; i < dgvUnmet.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvUnmet.Columns[i].HeaderText;

            for (int i = 0; i < dgvUnmet.Rows.Count; i++)
            {
                for (int j = 1; j < dgvUnmet.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvUnmet.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnTeenagePregnancies_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Teenage Pregnancies";

            for (int i = 1; i < dgvTeenage.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvTeenage.Columns[i].HeaderText;

            for (int i = 0; i < dgvTeenage.Rows.Count; i++)
            {
                for (int j = 1; j < dgvTeenage.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvTeenage.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnPregnant_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Pregnant";

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

        private void btnWPA_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Women With Reproductive Age";

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

        private void btnAge_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "By Age " + "(" + cbAge.Text + ")";

            for (int i = 1; i < dgvAge.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvAge.Columns[i].HeaderText;

            for (int i = 0; i < dgvAge.Rows.Count; i++)
            {
                for (int j = 1; j < dgvAge.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvAge.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnSector_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "By Sector " + "(" + cbSector.Text + ")";

            for (int i = 1; i < dgvSector.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvSector.Columns[i].HeaderText;

            for (int i = 0; i < dgvSector.Rows.Count; i++)
            {
                for (int j = 1; j < dgvSector.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvSector.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnGender_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "By Gender " +"(" + cbGender.Text + ")";

            for (int i = 1; i < dgvGender.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvGender.Columns[i].HeaderText;

            for (int i = 0; i < dgvGender.Rows.Count; i++)
            {
                for (int j = 1; j < dgvGender.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvGender.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btn4ps_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "4ps Members";

            for (int i = 1; i < dgv4ps.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgv4ps.Columns[i].HeaderText;

            for (int i = 0; i < dgv4ps.Rows.Count; i++)
            {
                for (int j = 1; j < dgv4ps.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgv4ps.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void btnFPmethod_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application ExlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = ExlApp.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            //worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            worksheet.Name = "Method " + "(" + cbMethod.Text + ")";

            for (int i = 1; i < dgvMethod.Columns.Count; i++)
                ExlApp.Cells[1, i] = dgvMethod.Columns[i].HeaderText;

            for (int i = 0; i < dgvMethod.Rows.Count; i++)
            {
                for (int j = 1; j < dgvMethod.Columns.Count; j++)
                    ExlApp.Cells[i + 2, j] = dgvMethod.Rows[i].Cells[j].Value.ToString();
            }

            ExlApp.Columns.AutoFit();
            ExlApp.Visible = true;
        }

        private void householdGraph_Click(object sender, EventArgs e)
        {
            graphName = "Households";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void populationGraph_Click(object sender, EventArgs e)
        {
            graphName = "Population";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void fpGraph_Click(object sender, EventArgs e)
        {
            graphName = "FPM";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void unmetGraph_Click(object sender, EventArgs e)
        {
            graphName = "Unmet";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void teenageGraph_Click(object sender, EventArgs e)
        {
            graphName = "Teenage";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void pregnantGraph_Click(object sender, EventArgs e)
        {
            graphName = "Pregnant";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void reproductiveGraph_Click(object sender, EventArgs e)
        {
            graphName = "Reproductive";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void ageGraph_Click(object sender, EventArgs e)
        {
            graphName = "Age";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void sectorGraph_Click(object sender, EventArgs e)
        {
            graphName = "Sector";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void genderGraph_Click(object sender, EventArgs e)
        {
            graphName = "Gender";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void _4psGraph_Click(object sender, EventArgs e)
        {
            graphName = "4Ps";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }

        private void methodGraph_Click(object sender, EventArgs e)
        {
            graphName = "Method";
            frmGraph graph = new frmGraph();
            graph.ShowDialog();
        }
    }
}
