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
    public partial class frmCategoryList : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        public static int BrgyId;
        public static int PurokId;
        public static int wsId;
        public static int dwsId;
        public static int tfId;
        public static int wdId;
        public static int fpmId;
        public static int reasonId;
        public static int occupationId;
        public static int enumeratorId;

        public frmCategoryList()
        {
            InitializeComponent();
        }

        private void frmCategoryList_Load(object sender, EventArgs e)
        {
            LoadBarangay();
            LoadPurok();
            LoadWaterSource();
            LoadDrinkingWaterSource();
            LoadToiletFacility();
            LoadWasteDisposal();
            LoadFamilyPlanningMethod();
            LoadReasonForUsing();
            LoadOccupation();
            LoadEnumerator();
        }

        public void LoadBarangay()
        {
            dgvBarangay.ForeColor = Color.Black;
            dgvBarangay.DataSource = db.tblBarangays.Select(x => new
            {
                ID = x.ID,
                Barangay = x.brgyName
            }).ToList();
        }

        public void LoadPurok()
        {
            dgvPurok.ForeColor = Color.Black;
            dgvPurok.DataSource = db.tblPuroks.Select(x => new
            {
                ID = x.ID,
                Purok = x.purokName,
                Barangay = x.tblBarangay.brgyName
            }).OrderBy(x => x.Barangay).ToList();
            
        }

        public void LoadWaterSource()
        {
            dgvWaterSource.ForeColor = Color.Black;
            dgvWaterSource.DataSource = db.tblWaterSources.Select(x => new
            {
                ID = x.ID,
                WaterSource = x.sourceName,
                Number = x.sourceNumber
            }).ToList();

        }

        public void LoadDrinkingWaterSource()
        {
            dgvDrinkingWater.ForeColor = Color.Black;
            dgvDrinkingWater.DataSource = db.tblDrinkingWaters.Select(x => new
            {
                ID = x.ID,
                DrinkingWaterSource = x.sourceName,
                Number = x.sourceNumber
            }).ToList();

        }

        public void LoadToiletFacility()
        {
            dgvToilet.ForeColor = Color.Black;
            dgvToilet.DataSource = db.tblToiletFacilities.Select(x => new
            {
                ID = x.ID,
                ToiletFacility = x.facilityName,
                Number = x.facilityNumber
            }).ToList();

        }

        public void LoadWasteDisposal()
        {
            dgvWasteDisposal.ForeColor = Color.Black;
            dgvWasteDisposal.DataSource = db.tblWasteDisposals.Select(x => new
            {
                ID = x.ID,
                WasteDisposal = x.disposalName,
                Number = x.disposalNumber
            }).ToList();
        }

        public void LoadFamilyPlanningMethod()
        {
            dgvFPM.ForeColor = Color.Black;
            dgvFPM.DataSource = db.tblFamilyPlanningMethods.Select(x => new
            {
                ID = x.ID,
                Method = x.methodName,
                Number = x.methodNumber,
                Type = x.methodType
            }).ToList();
        }

        public void LoadReasonForUsing()
        {
            dgvReason.ForeColor = Color.Black;
            dgvReason.DataSource = db.tblReasons.Select(x => new
            {
                ID = x.ID,
                Reason = x.reasonName,
                Number = x.reasonNumber
            }).ToList();
        }

        public void LoadOccupation()
        {
            dgvOccupation.ForeColor = Color.Black;
            dgvOccupation.DataSource = db.tblOccupations.Select(x => new
            {
                ID = x.ID,
                Occupation = x.occupationName
            }).ToList();
        }

        public void LoadEnumerator()
        {
            dgvEnumerator.ForeColor = Color.Black;
            dgvEnumerator.DataSource = db.tblConductors.Select(x => new
            {
                ID = x.ID,
                Enumerator = x.fullName,
            }).ToList();
        }

        private void dgvBarangay_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvBarangay.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddBarangay.edit = true;
                BrgyId = int.Parse(dgvBarangay.CurrentRow.Cells[2].Value.ToString());
                frmAddBarangay obj = new frmAddBarangay();
                obj.ShowDialog();
            }


            if (dgvEnumerator.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                BrgyId = int.Parse(dgvBarangay.CurrentRow.Cells[2].Value.ToString());
                string name = dgvBarangay.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblHouse.tblPurok.brgyID == BrgyId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblBarangay barangay = db.tblBarangays.Find(BrgyId);
                    db.tblBarangays.Remove(barangay);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Barangays...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Barangays.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadBarangay();
                }
            }
        }

        private void dgvPurok_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPurok.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddPurok.edit = true;
                PurokId = int.Parse(dgvPurok.CurrentRow.Cells[2].Value.ToString());
                frmAddPurok obj = new frmAddPurok();
                obj.ShowDialog();
            }

            if (dgvPurok.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                PurokId = int.Parse(dgvPurok.CurrentRow.Cells[2].Value.ToString());
                string name = dgvPurok.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblHouse.tblPurok.ID == PurokId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblPurok purok = db.tblPuroks.Find(PurokId);
                    db.tblPuroks.Remove(purok);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Puroks...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Puroks.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadPurok();
                }
            }
        }

        private void dgvWaterSource_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvWaterSource.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddWaterSource.edit = true;
                wsId = int.Parse(dgvWaterSource.CurrentRow.Cells[2].Value.ToString());
                frmAddWaterSource obj = new frmAddWaterSource();
                obj.ShowDialog();
            }

            if (dgvWaterSource.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                wsId = int.Parse(dgvWaterSource.CurrentRow.Cells[2].Value.ToString());
                string name = dgvWaterSource.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblHouse.tblWaterSource.ID == wsId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblWaterSource waterSource = db.tblWaterSources.Find(wsId);
                    db.tblWaterSources.Remove(waterSource);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Water Sources...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Water Sources.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadWaterSource();
                }
            }
        }

        private void dgvDrinkingWater_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDrinkingWater.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddDrinkingWater.edit = true;
                dwsId = int.Parse(dgvDrinkingWater.CurrentRow.Cells[2].Value.ToString());
                frmAddDrinkingWater obj = new frmAddDrinkingWater();
                obj.ShowDialog();
            }

            if (dgvDrinkingWater.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                dwsId = int.Parse(dgvDrinkingWater.CurrentRow.Cells[2].Value.ToString());
                string name = dgvDrinkingWater.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblHouse.tblDrinkingWater.ID == dwsId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblDrinkingWater dwaterSource = db.tblDrinkingWaters.Find(dwsId);
                    db.tblDrinkingWaters.Remove(dwaterSource);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Drinking Water Sources...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Drinking Water Sources.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadDrinkingWaterSource();
                }
            }

        }

        private void dgvToilet_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvToilet.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddToiletFacility.edit = true;
                tfId = int.Parse(dgvToilet.CurrentRow.Cells[2].Value.ToString());
                frmAddToiletFacility obj = new frmAddToiletFacility();
                obj.ShowDialog();
            }

            if (dgvToilet.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                tfId = int.Parse(dgvToilet.CurrentRow.Cells[2].Value.ToString());
                string name = dgvToilet.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblHouse.tblToiletFacility.ID == tfId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblToiletFacility facility = db.tblToiletFacilities.Find(tfId);
                    db.tblToiletFacilities.Remove(facility);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Toilet Facilities...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Toilet Facilities.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadToiletFacility();
                }
            }
        }

        private void dgvWasteDisposal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvWasteDisposal.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddWasteDisposal.edit = true;
                wdId = int.Parse(dgvWasteDisposal.CurrentRow.Cells[2].Value.ToString());
                frmAddWasteDisposal obj = new frmAddWasteDisposal();
                obj.ShowDialog();
            }

            if (dgvWasteDisposal.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                wdId = int.Parse(dgvWasteDisposal.CurrentRow.Cells[2].Value.ToString());
                string name = dgvWasteDisposal.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblHouse.tblWasteDisposal.ID == wdId) > 0)
                {
                    MessageBox.Show(name + " has been removed from list of Toilet Facilities...", "Deleted!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblWasteDisposal wasteDisposal = db.tblWasteDisposals.Find(wdId);
                    db.tblWasteDisposals.Remove(wasteDisposal);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Waste Disposals...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Waste Disposals.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadWasteDisposal();
                }
            }
        }

        private void dgvFPM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFPM.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddFamilyPlanningMethod.edit = true;
                fpmId = int.Parse(dgvFPM.CurrentRow.Cells[2].Value.ToString());
                frmAddFamilyPlanningMethod obj = new frmAddFamilyPlanningMethod();
                obj.ShowDialog();
            }

            if (dgvFPM.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                fpmId = int.Parse(dgvFPM.CurrentRow.Cells[2].Value.ToString());
                string name = dgvFPM.CurrentRow.Cells[3].Value.ToString();
                if (db.tblIndividuals.Count(x => x.tblFamilyPlanningMethod.ID == fpmId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblFamilyPlanningMethod method = db.tblFamilyPlanningMethods.Find(fpmId);
                    db.tblFamilyPlanningMethods.Remove(method);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Family Planning Methods...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Family Planning Methods.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadFamilyPlanningMethod();
                }
            }
        }

        private void dgvReason_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvReason.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddReason.edit = true;
                reasonId = int.Parse(dgvReason.CurrentRow.Cells[2].Value.ToString());
                frmAddReason obj = new frmAddReason();
                obj.ShowDialog();
            }

            if (dgvReason.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                reasonId = int.Parse(dgvReason.CurrentRow.Cells[2].Value.ToString());
                string name = dgvReason.CurrentRow.Cells[3].Value.ToString();
                if (db.tblIndividuals.Count(x => x.tblReason.ID == reasonId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblReason reason = db.tblReasons.Find(reasonId);
                    db.tblReasons.Remove(reason);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Reason For Using Family Planning...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Reason For Using Family Planning.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadReasonForUsing();
                }
            }
        }

        private void dgvOccupation_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOccupation.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmAddOccupation.edit = true;
                occupationId = int.Parse(dgvOccupation.CurrentRow.Cells[2].Value.ToString());
                frmAddOccupation obj = new frmAddOccupation();
                obj.ShowDialog();
            }

            if (dgvOccupation.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                occupationId = int.Parse(dgvOccupation.CurrentRow.Cells[2].Value.ToString());
                string name = dgvOccupation.CurrentRow.Cells[3].Value.ToString();
                if (db.tblIndividuals.Count(x => x.tblOccupation.ID == occupationId) > 0)
                {
                    MessageBox.Show(name + " has been surveyed already...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove " + name + " from list?", "Warning!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    tblOccupation occupation = db.tblOccupations.Find(occupationId);
                    db.tblOccupations.Remove(occupation);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Occupations...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Occupations.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadOccupation();
                }
            }
        }

        private void dgvEnumerator_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEnumerator.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                frmEnumerator.edit = true;
                enumeratorId = int.Parse(dgvEnumerator.CurrentRow.Cells[2].Value.ToString());
                frmEnumerator obj = new frmEnumerator();
                obj.ShowDialog();
            }

            if (dgvEnumerator.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                enumeratorId = int.Parse(dgvEnumerator.CurrentRow.Cells[2].Value.ToString());
                string name = dgvEnumerator.CurrentRow.Cells[3].Value.ToString();
                if (db.tblSurveys.Count(x => x.tblConductor.ID == enumeratorId) > 0)
                {
                    MessageBox.Show("The enumerator has already conducted surveys...", "Deletion Failed!");
                    return;
                }

                DialogResult dr = MessageBox.Show("Are you sure you want to remove enumerator from list?", "Warning!", MessageBoxButtons.OKCancel);
                if(dr == DialogResult.OK)
                {
                    tblConductor conductor = db.tblConductors.Find(enumeratorId);
                    db.tblConductors.Remove(conductor);
                    db.SaveChanges();

                    MessageBox.Show(name + " has been removed from list of Enumerators...", "Deleted!");
                    tblLog log = new tblLog();
                    log.ActivityLog = name + " has been removed by System Admin from list of Enumerators.";
                    log.DateTime = DateTime.Now;
                    db.tblLogs.Add(log);
                    db.SaveChanges();

                    LoadEnumerator();
                }
            }
        }
    }
}
