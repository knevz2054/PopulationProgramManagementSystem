using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using DataProcessingSystem.Data;

namespace DataProcessingSystem
{
    public partial class frmGraph : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public frmGraph()
        {
            InitializeComponent();
        }

        private void frmGraph_Load(object sender, EventArgs e)
        {
            if (frmViewSummary.graphName == "Households")
            {
                
                chart1.Series[0].Name = "Households";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName),
                    Percentage = (db.tblHouses.Count() == 0) ? "0%" : ((decimal)(db.tblHouses.Count(xx => xx.tblPurok.tblBarangay.brgyName == x.brgyName)) / ((decimal)db.tblHouses.Count()) * 100) + "%",
                }).OrderByDescending(x => x.Count).ToList();

                GridGraph();
               
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Population")
            {
                chart1.Series[0].Name = "Population";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName),
                    Percentage = (db.tblIndividuals.Count() == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName)) / ((decimal)db.tblIndividuals.Count()) * 100) + "%",
                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "FPM")
            {
                chart1.Series[0].Name = "Family Planning Method Users";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName != "No Method"),
                    Percentage = (db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName != "No Method") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName != "No Method")) / ((decimal)db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName != "No Method")) * 100) + "%",
                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Unmet")
            {
                chart1.Series[0].Name = "Unmet Needs";
                var ListData = db.tblBarangays.Select(x => new
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
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Teenage")
            {
                chart1.Series[0].Name = "Teenage Pregnancies";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes" && xx.Age <= 19),
                    Percentage = (db.tblIndividuals.Count(xx => xx.Pregnant == "Yes" && xx.Age <= 19) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes" && xx.Age <= 19)) / ((decimal)db.tblIndividuals.Count(xx => xx.Pregnant == "Yes" && xx.Age <= 19)) * 100) + "%",

                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Pregnant")
            {
                chart1.Series[0].Name = "Pregnant";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes"),
                    Percentage = (db.tblIndividuals.Count(xx => xx.Pregnant == "Yes") == 0) ? "0%" : ((db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Pregnant == "Yes")) / ((decimal)db.tblIndividuals.Count(xx => xx.Pregnant == "Yes")) * 100) + "%",

                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Reproductive")
            {
                chart1.Series[0].Name = "Women of Reproductive Age";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49),
                    Percentage = (db.tblIndividuals.Count(xx => xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49)) / ((decimal)db.tblIndividuals.Count(xx => xx.Gender == "Female" && xx.Age >= 15 && xx.Age <= 49)) * 100) + "%",

                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Age")
            {

                if (frmViewSummary.Age == "Child")
                {
                    chart1.Series[0].Name = "Child: Ages 0 - 9";
                    var ListData = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 0 && xx.Age <= 9),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 0 && xx.Age <= 9) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 0 && xx.Age <= 9)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 0 && xx.Age <= 9)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    GridGraph();
                    int i = 0;
                    foreach (var item in ListData)
                    {
                        this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                        this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                        i++;
                    }
                }

                if (frmViewSummary.Age == "Adolescent")
                {
                    chart1.Series[0].Name = "Adolescent: Ages 10 - 19";
                    var ListData = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 10 && xx.Age <= 19),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 10 && xx.Age <= 19) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 10 && xx.Age <= 19)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 10 && xx.Age <= 19)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();

                    GridGraph();
                    
                    int i = 0;
                    foreach (var item in ListData)
                    {
                        this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                        this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                        i++;
                    }
                }

                if (frmViewSummary.Age == "Adult")
                {
                    chart1.Series[0].Name = "Adult: Ages 20 - 59";
                    var ListData = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 20 && xx.Age <= 59),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 20 && xx.Age <= 59) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 20 && xx.Age <= 59)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 20 && xx.Age <= 59)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    GridGraph();
                    int i = 0;
                    foreach (var item in ListData)
                    {
                        this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                        this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                        i++;
                    }
                }

                if (frmViewSummary.Age == "SC")
                {
                    chart1.Series[0].Name = "Senior Citezen: Ages 60 and Above";
                    var ListData = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 60),
                        Percentage = (db.tblIndividuals.Count(xx => xx.Age >= 60) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Age >= 60)) / ((decimal)db.tblIndividuals.Count(xx => xx.Age >= 60)) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    GridGraph();
                    int i = 0;
                    foreach (var item in ListData)
                    {
                        this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                        this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                        i++;
                    }
                }
            }

            if (frmViewSummary.graphName == "Sector")
            {
                chart1.Series[0].Name = frmViewSummary.Sector;
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblOccupation.occupationName == frmViewSummary.Sector && xx.Age >= 18),
                    Percentage = (db.tblIndividuals.Count(xx => xx.tblOccupation.occupationName == frmViewSummary.Sector && xx.Age >= 18) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblOccupation.occupationName == frmViewSummary.Sector && xx.Age >= 18)) / ((decimal)db.tblIndividuals.Count(xx => xx.tblOccupation.occupationName == frmViewSummary.Sector && xx.Age >= 18)) * 100) + "%",

                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Gender")
            {
                chart1.Series[0].Name = frmViewSummary.Gender;
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == frmViewSummary.Gender),
                    Percentage = (db.tblIndividuals.Count(xx => xx.Gender == frmViewSummary.Gender) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.Gender == frmViewSummary.Gender)) / ((decimal)db.tblIndividuals.Count(xx => xx.Gender == frmViewSummary.Gender)) * 100) + "%",

                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "4Ps")
            {
                chart1.Series[0].Name = "4Ps Member";
                var ListData = db.tblBarangays.Select(x => new
                {
                    Barangay = x.brgyName,
                    Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.member4ps == "Yes"),
                    Percentage = (db.tblIndividuals.Count(xx => xx.member4ps == "Yes") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.member4ps == "Yes")) / ((decimal)db.tblIndividuals.Count(xx => xx.member4ps == "Yes")) * 100) + "%",

                }).OrderByDescending(x => x.Count).ToList();
                GridGraph();
                int i = 0;
                foreach (var item in ListData)
                {
                    this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                    this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                    i++;
                }
            }

            if (frmViewSummary.graphName == "Method")
            {
                if (frmViewSummary.Method == "Expressed Interest")
                {
                    chart1.Series[0].Name = "Family Planning Method Used: " + "Expressed Interest";
                    var ListData = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => (xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In")),
                        Percentage = (db.tblIndividuals.Count(xx => (xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => (xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In"))) / ((decimal)db.tblIndividuals.Count(xx => (xx.wantsTo == "Yes") && (xx.civilStatus == "Married" || xx.civilStatus == "Live-In"))) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    GridGraph();
                    int i = 0;
                    foreach (var item in ListData)
                    {
                        this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                        this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                        i++;
                    }
                }

                else
                {
                    chart1.Series[0].Name = "Family Planning Method Used: " + frmViewSummary.Method;
                    var ListData = db.tblBarangays.Select(x => new
                    {
                        Barangay = x.brgyName,
                        Count = db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == frmViewSummary.Method && xx.civilStatus == "Married" || xx.civilStatus == "Live-In"),
                        Percentage = (db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName == frmViewSummary.Method && xx.civilStatus == "Married" || xx.civilStatus == "Live-In") == 0) ? "0%" : ((decimal)(db.tblIndividuals.Count(xx => xx.tblHouse.tblPurok.tblBarangay.brgyName == x.brgyName && xx.tblFamilyPlanningMethod.methodName == frmViewSummary.Method && xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) / ((decimal)db.tblIndividuals.Count(xx => xx.tblFamilyPlanningMethod.methodName == frmViewSummary.Method && xx.civilStatus == "Married" || xx.civilStatus == "Live-In")) * 100) + "%",

                    }).OrderByDescending(x => x.Count).ToList();
                    GridGraph();
                    int i = 0;
                    foreach (var item in ListData)
                    {
                        this.chart1.Series[0].Points.AddXY(item.Barangay, item.Count);
                        this.chart1.Series[0].Points[i].Label = item.Count.ToString();
                        i++;
                    }
                }
            }
            
        }
        public void GridGraph()
        {
            chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            chart1.ChartAreas["ChartArea1"].AxisY.Interval = 50;
            chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineWidth = 0;
            chart1.ChartAreas["ChartArea1"].AxisX.MinorGrid.LineWidth = 0;
            chart1.ChartAreas["ChartArea1"].AxisY.MinorGrid.LineWidth = 0;
        }
    }
}
