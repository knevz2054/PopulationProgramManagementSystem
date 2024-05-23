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
using DataProcessingSystem.Class;

namespace DataProcessingSystem
{
    public partial class frmSurveyForm : Form
    {
        DataProcessingSystemEntities db = new DataProcessingSystemEntities();
        int ID = 1;
        int memID;
        int houseNum;
        List<Member> listMember = new List<Member>();
        public frmSurveyForm()
        {
            InitializeComponent();
            dgvMember.EnableHeadersVisualStyles = false;
            dgvMember.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMember.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
            dgvMember.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvMember.RowsDefaultCellStyle.ForeColor = Color.CadetBlue;

            chkWantsTo.Enabled = false;
            chkPregnant.Enabled = false;
        }

        private void BtnAddMember_Click(object sender, EventArgs e)
        {
            if (btnAddMember.Text == "Add Member")
            {
                if (validate())
                {
                    AddMember();
                    dgvMember.DataSource = null;
                    dgvMember.DataSource = listMember;
                    GridHeader();
                    clear();
                    pnlTop.Enabled = listMember.Count == 0;
                }
                chkConfirmation.Enabled = listMember.Count > 0;
            }
            if (btnAddMember.Text == "Update")
            {
                if (validateUpdate())
                {
                    UpdateMember();
                    dgvMember.DataSource = null;
                    dgvMember.DataSource = listMember;
                    GridHeader();
                    clear();
                    pnlTop.Enabled = listMember.Count == 0;
                    btnAddMember.Text = "Add Member";
                }
                chkConfirmation.Enabled = listMember.Count > 0;
            }
        }

        private void FrmSurveyForm_Load(object sender, EventArgs e)
        {
            if (frmLogin.access != "City")
            {
                foreach (var v in db.tblBarangays.Where(x => x.brgyName == frmLogin.access))
                    cbBarangay.Items.Add(v.brgyName);
            }
            else
            {
                foreach (var v in db.tblBarangays)
                    cbBarangay.Items.Add(v.brgyName);
            }

            foreach (var v in db.tblConductors)
                cbConductor.Items.Add(v.fullName);

            foreach (var v in db.tblWaterSources.Where(x => x.sourceName != "Not Surveyed"))
                cbWaterSource.Items.Add(v.sourceNumber + ". " + v.sourceName);

            foreach (var v in db.tblDrinkingWaters.Where(x => x.sourceName != "Not Surveyed"))
                cbDrinkingWS.Items.Add(v.sourceNumber + ". " + v.sourceName);

            foreach (var v in db.tblToiletFacilities.Where(x => x.facilityName != "Not Surveyed"))
                cbTF.Items.Add(v.facilityNumber + ". " + v.facilityName);

            foreach (var v in db.tblWasteDisposals.Where(x => x.disposalName != "Not Surveyed"))
                cbWD.Items.Add(v.disposalNumber + ". " + v.disposalName);

            foreach (var v in db.tblOccupations)
                cbOccupation.Items.Add(v.occupationName);

            foreach (var v in db.tblFamilyPlanningMethods)
                cbFPM.Items.Add(v.methodNumber + ". " + v.methodName);

            dgvMember.DataSource = listMember.ToList();
            GridHeader();
        }

        private void DgvMember_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMember.Columns[e.ColumnIndex].HeaderText == "Edit")
            {
                memID = int.Parse(dgvMember.CurrentRow.Cells[2].Value.ToString());
                txtLastName.Text = dgvMember.CurrentRow.Cells[3].Value.ToString();
                txtFirstName.Text = dgvMember.CurrentRow.Cells[4].Value.ToString();
                txtMI.Text = dgvMember.CurrentRow.Cells[5].Value.ToString();
                cbGender.Text = dgvMember.CurrentRow.Cells[6].Value.ToString();
                dtpBirthday.Text = dgvMember.CurrentRow.Cells[7].Value.ToString();
                cbCivilStatus.Text = dgvMember.CurrentRow.Cells[8].Value.ToString();
                cbRelationship.Text = dgvMember.CurrentRow.Cells[9].Value.ToString();
                cbOccupation.Text = dgvMember.CurrentRow.Cells[10].Value.ToString();
                txtIncome.Text = dgvMember.CurrentRow.Cells[11].Value.ToString();
                if (dgvMember.CurrentRow.Cells[12].Value.ToString() == "Yes")
                    chk4ps.Checked = true;
                cbFPM.Text = dgvMember.CurrentRow.Cells[13].Value.ToString();
                cbReasonFP.Text = dgvMember.CurrentRow.Cells[14].Value.ToString();
                if (dgvMember.CurrentRow.Cells[15].Value.ToString() == "Yes")
                    chkWantsTo.Checked = true;
                if (dgvMember.CurrentRow.Cells[16].Value.ToString() == "Yes")
                    chkPregnant.Checked = true;
                if (dgvMember.CurrentRow.Cells[17].Value.ToString() == "Yes")
                {
                    chkHead.Checked = true;
                    chkHead.Enabled = true;
                }
                btnAddMember.Text = "Update";
                chkConfirmation.Enabled = false;
                chkConfirmation.Checked = false;
            }
            if (dgvMember.Columns[e.ColumnIndex].HeaderText == "Delete")
            {
                memID = int.Parse(dgvMember.CurrentRow.Cells[2].Value.ToString());
                string text = string.Format("Are you sure you want to delete {0}, {1}?", dgvMember.CurrentRow.Cells[3].Value.ToString(), dgvMember.CurrentRow.Cells[4].Value.ToString());
                DialogResult dr = MessageBox.Show(text, "Confirmation!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                {
                    Member member = listMember.Find(x => x.ID == memID);
                    listMember.Remove(member);
                    dgvMember.DataSource = null;
                    dgvMember.DataSource = listMember;
                    GridHeader();
                    pnlTop.Enabled = listMember.Count == 0;
                }
                btnSubmit.Enabled = listMember.Count > 0;
                chkHead.Enabled = listMember.Count > 0 || listMember.Count(x => x.Head == "Yes") == 0;
                if (listMember.Count == 0)
                {
                    chkConfirmation.Checked = false;
                    chkConfirmation.Enabled = false;
                }

            }
        }
        //A method that add data to list
        public void AddMember()
        {
            Member member = new Member();
            member.ID = ID;
            member.LastName = txtLastName.Text.Trim();
            member.FirstName = txtFirstName.Text.Trim();
            member.MiddleName = txtMI.Text.Trim();
            member.Gender = cbGender.Text;
            member.BirthDate = DateTime.Parse(dtpBirthday.Text).Date;
            member.CivilStatus = cbCivilStatus.Text;
            member.Relationship = cbRelationship.Text;
            member.Occupation = cbOccupation.Text;
            if (txtIncome.Text == string.Empty)
                member.Income = 0;
            else
                member.Income = decimal.Parse(txtIncome.Text);

            if (chk4ps.Checked)
                member.Member4ps = "Yes";
            else
                member.Member4ps = "No";

            member.FPM = cbFPM.Text;

            member.Reason = cbReasonFP.Text;

            member.fpmID = db.tblFamilyPlanningMethods.Where(x => x.methodNumber + ". " + x.methodName == cbFPM.Text).Select(x => x.ID).SingleOrDefault();

            member.reasonID = db.tblReasons.Where(x => x.reasonNumber + ". " + x.reasonName == cbReasonFP.Text).Select(x => x.ID).SingleOrDefault();

            member.occupID = db.tblOccupations.Where(x => x.occupationName == cbOccupation.Text).Select(x => x.ID).SingleOrDefault();

            if (chkWantsTo.Checked)
                member.WantsTo = "Yes";
            else
                member.WantsTo = "No";

            if (chkPregnant.Checked)
                member.Pregnant = "Yes";
            else
                member.Pregnant = "No";

            if (chkHead.Checked)
                member.Head = "Yes";
            else
                member.Head = "No";

            listMember.Add(member);
            ID++;

            MessageBox.Show("A member has been added to list...", "Success!");
            if (listMember.Count(x => x.Head == "Yes") > 0)
            {
                chkHead.Checked = false;
                chkHead.Enabled = false;
            }

        }

        //A method that updates data from list
        public void UpdateMember()
        {
            foreach (var v in listMember.Where(x => x.ID == memID))
            {
                v.LastName = txtLastName.Text.Trim();
                v.FirstName = txtFirstName.Text.Trim();
                v.MiddleName = txtMI.Text.Trim();
                v.Gender = cbGender.Text;
                v.BirthDate = DateTime.Parse(dtpBirthday.Text).Date;
                v.CivilStatus = cbCivilStatus.Text;
                v.Relationship = cbRelationship.Text;
                v.Occupation = cbOccupation.Text;
                if (txtIncome.Text == string.Empty)
                    v.Income = 0;
                else
                    v.Income = decimal.Parse(txtIncome.Text);

                if (chk4ps.Checked)
                    v.Member4ps = "Yes";
                else
                    v.Member4ps = "No";

                v.FPM = cbFPM.Text;

                v.Reason = cbReasonFP.Text;

                v.fpmID = db.tblFamilyPlanningMethods.Where(x => x.methodNumber + ". " + x.methodName == cbFPM.Text).Select(x => x.ID).SingleOrDefault();

                v.reasonID = db.tblReasons.Where(x => x.reasonNumber + ". " + x.reasonName == cbReasonFP.Text).Select(x => x.ID).SingleOrDefault();


                if (chkWantsTo.Checked)
                    v.WantsTo = "Yes";
                else
                    v.WantsTo = "No";

                if (chkPregnant.Checked)
                    v.Pregnant = "Yes";
                else
                    v.Pregnant = "No";

                if (chkHead.Checked)
                    v.Head = "Yes";
                else
                    v.Head = "No";

            }
            MessageBox.Show("Update Successful", "Success!");
            if (listMember.Count(x => x.Head == "Yes") > 0)
            {
                chkHead.Checked = false;
                chkHead.Enabled = false;
            }

        }

        //A method that clears household member field
        public void clear()
        {
            txtLastName.Clear();
            txtFirstName.Clear();
            txtMI.Clear();
            cbGender.SelectedItem = null;
            dtpBirthday.Value = DateTime.Now.Date;
            cbCivilStatus.SelectedItem = null;
            cbRelationship.SelectedItem = null;
            if (chkHead.Checked)
                chkHead.Checked = false;
            cbOccupation.SelectedItem = null;
            txtIncome.Clear();
            if (chk4ps.Checked)
                chk4ps.Checked = false;
            cbFPM.SelectedItem = null;
            if (chkWantsTo.Checked)
                chkWantsTo.Checked = false;
            if (chkPregnant.Checked)
            {
                chkPregnant.Checked = false;
            }
            chkPregnant.Enabled = false;
        }

        //A method that changes header text in datagridview
        public void GridHeader()
        {
            dgvMember.Columns[2].HeaderText = "ID";
            dgvMember.Columns[2].Width = 45;

            dgvMember.Columns[3].HeaderText = "Last Name";
            dgvMember.Columns[3].Width = 83;

            dgvMember.Columns[4].HeaderText = "First Name";
            dgvMember.Columns[4].Width = 83;

            dgvMember.Columns[5].HeaderText = "Middle Name";
            dgvMember.Columns[5].Width = 94;

            dgvMember.Columns[6].HeaderText = "Gender";
            dgvMember.Columns[6].Width = 74;

            dgvMember.Columns[7].HeaderText = "Birthday";
            dgvMember.Columns[7].Width = 74;

            dgvMember.Columns[8].HeaderText = "Civil Status";
            dgvMember.Columns[8].Width = 85;

            dgvMember.Columns[9].HeaderText = "Relationship";
            dgvMember.Columns[9].Width = 75;

            dgvMember.Columns[10].HeaderText = "Occupation";
            dgvMember.Columns[10].Width = 74;

            dgvMember.Columns[11].HeaderText = "Income";
            dgvMember.Columns[11].Width = 74;

            dgvMember.Columns[12].HeaderText = "4Ps";
            dgvMember.Columns[12].Width = 45;

            dgvMember.Columns[13].HeaderText = "FPM";
            dgvMember.Columns[13].Width = 70;

            dgvMember.Columns[14].HeaderText = "Reason";
            dgvMember.Columns[14].Width = 70;

            dgvMember.Columns[15].HeaderText = "Wants To";
            dgvMember.Columns[15].Width = 80;

            dgvMember.Columns[16].HeaderText = "Pregnant";
            dgvMember.Columns[16].Width = 70;

            dgvMember.Columns[17].HeaderText = "Head";
            dgvMember.Columns[17].Width = 42;

            dgvMember.Columns[18].Visible = false;

            dgvMember.Columns[19].Visible = false;

            dgvMember.Columns[20].Visible = false;
        }

        //A method that validates before adding data to list
        public bool validate()
        {
            if (txtHouseNumber.Text != string.Empty)
                houseNum = int.Parse(txtHouseNumber.Text.Trim());

            if (cbBarangay.SelectedItem == null)
                MessageBox.Show("Select Barangay...", "Error!");

            else if (cbPurok.SelectedItem == null)
                MessageBox.Show("Select Purok...", "Error!");

            else if (txtHouseNumber.Text == string.Empty)
                MessageBox.Show("Create House number...", "Error!");

            else if (db.tblHouses.Count(x => x.houseNumber == houseNum && x.tblPurok.tblBarangay.brgyName == cbBarangay.Text) > 0)
                MessageBox.Show("House number is already listed in Brgy. " + cbBarangay.Text + "...", "Error!");

            else if (txtAddress.Text == string.Empty)
                MessageBox.Show("Create House address...", "Error!");

            else if (cbConductor.SelectedItem == null)
                MessageBox.Show("Select Conductor...", "Error!");

            else if (txtRespondent.Text == string.Empty)
                MessageBox.Show("Respondent is required...", "Error!");

            else if (cbWaterSource.SelectedItem == null)
                MessageBox.Show("Select Water source...", "Error!");

            else if (cbDrinkingWS.SelectedItem == null)
                MessageBox.Show("Select Drinking Water source...", "Error!");

            else if (cbTF.SelectedItem == null)
                MessageBox.Show("Select Toilet Facility...", "Error!");

            else if (cbWD.SelectedItem == null)
                MessageBox.Show("Select Waste Disposal...", "Error!");

            else if (listMember.Count(x => x.FirstName == txtFirstName.Text && x.MiddleName == txtMI.Text && x.LastName == txtLastName.Text) > 0)
                MessageBox.Show("Member with the same name is already listed...", "Error!");

            else if (listMember.Count(x => x.Head == "Yes") > 0 && chkHead.Checked)
                MessageBox.Show("Only one (1) Head per household...", "Error!");

            else if (txtLastName.Text == string.Empty)
                MessageBox.Show("Lastname is required...", "Error!");

            else if (txtFirstName.Text == string.Empty)
                MessageBox.Show("Firstname is required...", "Error!");

            else if (txtMI.Text == string.Empty)
                MessageBox.Show("Middlename is required...", "Error!");

            else if (cbGender.SelectedItem == null)
                MessageBox.Show("Gender is required...", "Error!");

            else if (cbCivilStatus.SelectedItem == null)
                MessageBox.Show("Civil status is required...", "Error!");

            else if (cbRelationship.SelectedItem == null)
                MessageBox.Show("Relationship is required...", "Error!");

            else if (cbOccupation.SelectedItem == null)
                MessageBox.Show("Occupation is required...", "Error!");

            else if (cbFPM.SelectedItem == null)
                MessageBox.Show("Family Planning Method is required...", "Error!");

            else if (cbReasonFP.SelectedItem == null)
                MessageBox.Show("Reason for using Family Method is required...", "Error!");

            else if (dtpDate.Value.Date == DateTime.Now.Date)
            {
                string text = string.Format("The value of date surveyed is the date today. Do you want to continue?");
                DialogResult dr = MessageBox.Show(text, "Confirmation!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    return true;
                else
                    return false;
            }

            else if (dtpBirthday.Value.Date == DateTime.Now.Date)
            {
                string text = string.Format("The value of date of birth is the date today. Do you want to continue?");
                DialogResult dr = MessageBox.Show(text, "Confirmation!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    return true;
                else
                    return false;
            }

            else
                return true;

            return false;
        }

        //A method that validates before updating data from list
        public bool validateUpdate()
        {
            if (listMember.Count(x => x.FirstName == txtFirstName.Text && x.MiddleName == txtMI.Text && x.LastName == txtLastName.Text && x.ID != memID) > 0)
                MessageBox.Show("Member with the same name is already listed...", "Error!");

            else if (listMember.Count(x => x.Head == "Yes" && x.ID != memID) > 0 && chkHead.Checked)
                MessageBox.Show("Only one (1) Head per household...", "Error!");

            else if (txtLastName.Text == string.Empty)
                MessageBox.Show("Lastname is required...", "Error!");

            else if (txtFirstName.Text == string.Empty)
                MessageBox.Show("Firstname is required...", "Error!");

            else if (txtMI.Text == string.Empty)
                MessageBox.Show("Middlename is required...", "Error!");

            else if (cbGender.SelectedItem == null)
                MessageBox.Show("Gender is required...", "Error!");

            else if (cbCivilStatus.SelectedItem == null)
                MessageBox.Show("Civil status is required...", "Error!");

            else if (cbRelationship.SelectedItem == null)
                MessageBox.Show("Relationship is required...", "Error!");

            else if (cbOccupation.SelectedItem == null)
                MessageBox.Show("Occupation is required...", "Error!");

            else if (cbFPM.SelectedItem == null)
                MessageBox.Show("Family Planning Method is required...", "Error!");

            else if (cbReasonFP.SelectedItem == null)
                MessageBox.Show("Reason for using Family Method is required...", "Error!");

            else if (dtpBirthday.Value.Date == DateTime.Now.Date)
            {
                string text = string.Format("The value of date of birth is the date today. Do you want to continue?");
                DialogResult dr = MessageBox.Show(text, "Confirmation!", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK)
                    return true;
                else
                    return false;
            }

            else
                return true;
            return false;
        }


        private void TxtIncome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void CbBarangay_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbPurok.Items.Clear();
            foreach (var v in db.tblPuroks.Where(x => x.tblBarangay.brgyName == cbBarangay.Text))
                cbPurok.Items.Add(v.purokName);
        }

        private void CbFPM_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbReasonFP.Items.Clear();
            if (cbFPM.SelectedIndex == 17)
            {
                foreach (var v in db.tblReasons)
                    cbReasonFP.Items.Add(v.reasonNumber + ". " + v.reasonName);
                cbReasonFP.SelectedIndex = 3;
                cbReasonFP.Enabled = false;
                chkWantsTo.Enabled = true;
            }
            else
            {
                foreach (var v in db.tblReasons.Where(x => x.reasonNumber != 4))
                    cbReasonFP.Items.Add(v.reasonNumber + ". " + v.reasonName);
                cbReasonFP.Enabled = true;
                chkWantsTo.Enabled = false;
            }
            if (cbFPM.SelectedIndex != 17)
                chkWantsTo.Checked = false;

        }

        private void CbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkPregnant.Enabled = cbGender.Text == "Female";
            if (cbGender.Text == "Female")
                chkPregnant.Checked = false;
            cbCivilStatus.Items.Clear();
            if (cbGender.Text == "Male")
            {
                cbCivilStatus.Items.Add("Married");
                cbCivilStatus.Items.Add("Single");
                cbCivilStatus.Items.Add("Live-In");
                cbCivilStatus.Items.Add("Separated");
                cbCivilStatus.Items.Add("Widower");
            }

            if (cbGender.Text == "Female")
            {
                cbCivilStatus.Items.Add("Married");
                cbCivilStatus.Items.Add("Single");
                cbCivilStatus.Items.Add("Live-In");
                cbCivilStatus.Items.Add("Separated");
                cbCivilStatus.Items.Add("Widow");
            }
            if (!chkHead.Checked)
                cbRelationship.Items.Clear();

            if (cbGender.Text == "Male" && listMember.Count(x => x.Head == "Yes" && x.Gender == "Female") > 0)
            {
                cbRelationship.Items.Add("Husband");
                cbRelationship.Items.Add("Father");
                cbRelationship.Items.Add("Father In-Law");
                cbRelationship.Items.Add("Son");
                cbRelationship.Items.Add("Son In-Law");
                cbRelationship.Items.Add("Brother");
                cbRelationship.Items.Add("Brother  In-Law");
                cbRelationship.Items.Add("Grand Son");
                cbRelationship.Items.Add("Relative");
            }

            if (cbGender.Text == "Male" && listMember.Count(x => x.Head == "Yes" && x.Gender == "Male") > 0)
            {
                cbRelationship.Items.Add("Father");
                cbRelationship.Items.Add("Father In-Law");
                cbRelationship.Items.Add("Son");
                cbRelationship.Items.Add("Son In-Law");
                cbRelationship.Items.Add("Brother");
                cbRelationship.Items.Add("Brother  In-Law");
                cbRelationship.Items.Add("Grand Son");
                cbRelationship.Items.Add("Relative");
            }

            if (cbGender.Text == "Male" && listMember.Count(x => x.Head == "Yes") == 0 && !chkHead.Checked)
            {
                cbRelationship.Items.Add("Landlord");
                cbRelationship.Items.Add("Relative");
                cbRelationship.Items.Add("Tenant");
            }

            if (cbGender.Text == "Female" && listMember.Count(x => x.Head == "Yes" && x.Gender == "Male") > 0)
            {
                cbRelationship.Items.Add("Wife");
                cbRelationship.Items.Add("Mother");
                cbRelationship.Items.Add("Mother In-Law");
                cbRelationship.Items.Add("Daughter");
                cbRelationship.Items.Add("Daughter In-Law");
                cbRelationship.Items.Add("Sister");
                cbRelationship.Items.Add("Sister In-Law");
                cbRelationship.Items.Add("Grand Daughter");
                cbRelationship.Items.Add("Relative");
            }

            if (cbGender.Text == "Female" && listMember.Count(x => x.Head == "Yes" && x.Gender == "Female") > 0)
            {
                cbRelationship.Items.Add("Mother");
                cbRelationship.Items.Add("Mother In-Law");
                cbRelationship.Items.Add("Daughter");
                cbRelationship.Items.Add("Daughter In-Law");
                cbRelationship.Items.Add("Sister");
                cbRelationship.Items.Add("Sister In-Law");
                cbRelationship.Items.Add("Grand Daughter");
                cbRelationship.Items.Add("Relative");
            }

            if (cbGender.Text == "Female" && listMember.Count(x => x.Head == "Yes") == 0 && !chkHead.Checked)
            {
                cbRelationship.Items.Add("Landlady");
                cbRelationship.Items.Add("Relative");
                cbRelationship.Items.Add("Tenant");
            }
        }

        private void ChkHead_CheckedChanged(object sender, EventArgs e)
        {
            cbRelationship.Items.Clear();
            cbRelationship.Enabled = !chkHead.Checked;
            if (!chkHead.Checked && cbGender.Text == "Male")
            {
                cbRelationship.Items.Add("Landlord");
                cbRelationship.Items.Add("Relative");
                cbRelationship.Items.Add("Tenant");
                chkPregnant.Enabled = false;
            }

            if (!chkHead.Checked && cbGender.Text == "Female")
            {
                cbRelationship.Items.Add("Landlady");
                cbRelationship.Items.Add("Relative");
                cbRelationship.Items.Add("Tenant");
                chkPregnant.Enabled = true;
            }

            if (chkHead.Checked == true)
            {
                cbRelationship.Items.Add("Head");
                cbRelationship.SelectedIndex = 0;
                cbRelationship.Enabled = false;
            }

        }

        private void CbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listMember.Count(x => x.Relationship == "Head") > 0)
            {
                if (cbRelationship.Text == "Wife" || cbRelationship.Text == "Husband")
                {
                    cbFPM.Text = listMember.Where(x => x.Relationship == "Head").Select(x => x.FPM).SingleOrDefault();
                    cbFPM.Enabled = false;

                    cbReasonFP.Text = listMember.Where(x => x.Relationship == "Head").Select(x => x.Reason).SingleOrDefault();
                    cbReasonFP.Enabled = false;

                    if (listMember.Count(x => x.Relationship == "Head" && x.WantsTo == "Yes") > 0)
                    {
                        chkWantsTo.Checked = true;
                        chkWantsTo.Enabled = false;
                    }
                }
                else
                {
                    cbFPM.Enabled = true;
                    cbReasonFP.Enabled = true;
                    cbFPM.Items.Clear();
                    cbReasonFP.Items.Clear();
                    foreach (var v in db.tblFamilyPlanningMethods)
                        cbFPM.Items.Add(v.methodNumber + ". " + v.methodName);
                }
            }
        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            int houseNum = int.Parse(txtHouseNumber.Text.Trim());
            if (db.tblHouses.Count(x => x.houseNumber == houseNum && x.tblPurok.tblBarangay.brgyName == cbBarangay.Text) > 0)
            {
                MessageBox.Show("House number is already listed in Brgy. " + cbBarangay.Text + "...", "Error!");
                return;
            }
            btnSubmit.Enabled = false;

            tblHouse house = new tblHouse();
            house.houseNumber = int.Parse(txtHouseNumber.Text.Trim());
            house.houseAddress = txtAddress.Text.Trim();
            house.purokID = db.tblPuroks.Where(x => x.purokName == cbPurok.Text && x.tblBarangay.brgyName == cbBarangay.Text).Select(x => x.ID).SingleOrDefault();
            house.dwID = db.tblDrinkingWaters.Where(x => x.sourceNumber + ". " + x.sourceName == cbDrinkingWS.Text).Select(x => x.ID).SingleOrDefault();
            house.wID = db.tblWaterSources.Where(x => x.sourceNumber + ". " + x.sourceName == cbWaterSource.Text).Select(x => x.ID).SingleOrDefault();
            house.wdID = db.tblWasteDisposals.Where(x => x.disposalNumber + ". " + x.disposalName == cbWD.Text).Select(x => x.ID).SingleOrDefault();
            house.tfID = db.tblToiletFacilities.Where(x => x.facilityNumber + ". " + x.facilityName == cbTF.Text).Select(x => x.ID).SingleOrDefault();

            db.tblHouses.Add(house);
            db.SaveChanges();

            foreach (var v in listMember)
            {
                tblIndividual individual = new tblIndividual();
                individual.firstName = v.FirstName;
                individual.middleName = v.MiddleName;
                individual.lastName = v.LastName;
                individual.Gender = v.Gender;
                individual.birthDay = v.BirthDate;

                int x = 0;
                x = DateTime.Now.Year - v.BirthDate.Year;
                individual.Age = x;

                individual.civilStatus = v.CivilStatus;
                individual.occupationID = v.occupID;
                individual.Income = v.Income;
                individual.Relationship = v.Relationship;
                individual.Head = v.Head;
                individual.member4ps = v.Member4ps;
                individual.wantsTo = v.WantsTo;
                individual.Pregnant = v.Pregnant;
                individual.fpmID = v.fpmID;
                individual.reasonID = v.reasonID;
                individual.houseID = house.ID;

                db.tblIndividuals.Add(individual);
                db.SaveChanges();
            }
            tblRespondent resp = new tblRespondent();
            resp.fullName = txtRespondent.Text.Trim();
            db.tblRespondents.Add(resp);
            db.SaveChanges();

            tblSurvey survey = new tblSurvey();
            survey.conductorID = db.tblConductors.Where(x => x.fullName == cbConductor.Text).Select(x => x.ID).SingleOrDefault();
            survey.respondentID = resp.ID;
            survey.houseID = house.ID;
            survey.endUserID = frmLogin.userID;
            survey.Date = dtpDate.Value.Date;

            db.tblSurveys.Add(survey);
            db.SaveChanges();

            MessageBox.Show("Information of house number " + txtHouseNumber.Text + " in " + cbBarangay.Text + ", " + cbPurok.Text +
                " has been succesfully added to database...", "Success!");

            tblLog log = new tblLog();
            string fullName = db.tblUsers.Where(x => x.ID == frmLogin.userID).Select(x => x.FullName).SingleOrDefault();
            log.ActivityLog = "Household no. " + houseNum + " in " + cbPurok.Text + " of " + cbBarangay.Text + " has beed added to list by " + fullName + "...";
            log.DateTime = DateTime.Now;
            db.tblLogs.Add(log);
            db.SaveChanges();

            listMember.Clear();

            txtHouseNumber.Clear();
            txtRespondent.Clear();
            cbDrinkingWS.SelectedItem = null;
            cbWaterSource.SelectedItem = null;
            cbWD.SelectedItem = null;
            cbTF.SelectedItem = null;

            cbGender.SelectedItem = null;
            cbCivilStatus.SelectedItem = null;
            dtpBirthday.Value = DateTime.Now.Date;
            if (!chkHead.Enabled)
                chkHead.Enabled = true;
            if (!cbFPM.Enabled)
                cbFPM.Enabled = true;
            if (!cbReasonFP.Enabled)
                cbReasonFP.Enabled = true;

            dgvMember.DataSource = null;
            dgvMember.DataSource = listMember;
            GridHeader();
            pnlTop.Enabled = true;
            ID = 1;

            chkConfirmation.Checked = false;
            chkConfirmation.Enabled = false;
            chkNotSurveyed.Checked = false;
        }

        private void TxtHouseNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnAddConductor_Click(object sender, EventArgs e)
        {
            frmEnumerator cond = new frmEnumerator();
            cond.ShowDialog();
        }

        public void refConductor()
        {
            cbConductor.Items.Clear();
            foreach (var v in db.tblConductors)
                cbConductor.Items.Add(v.fullName);
        }

        private void chkConfirmation_CheckedChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = chkConfirmation.Checked;
            btnAddMember.Enabled = !chkConfirmation.Checked;
        }

        private void chkNotSurveyed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNotSurveyed.Checked)
            {
                cbDrinkingWS.Items.Clear();
                cbWaterSource.Items.Clear();
                cbTF.Items.Clear();
                cbWD.Items.Clear();

                cbDrinkingWS.Items.Add(db.tblDrinkingWaters.Where(x => x.sourceName == "Not Surveyed").Select(x => x.sourceNumber).SingleOrDefault() + ". " + "Not Surveyed");
                cbWaterSource.Items.Add(db.tblWaterSources.Where(x => x.sourceName == "Not Surveyed").Select(x => x.sourceNumber).SingleOrDefault() + ". " + "Not Surveyed");
                cbTF.Items.Add(db.tblToiletFacilities.Where(x => x.facilityName == "Not Surveyed").Select(x => x.facilityNumber).SingleOrDefault() + ". " + "Not Surveyed");
                cbWD.Items.Add(db.tblWasteDisposals.Where(x => x.disposalName == "Not Surveyed").Select(x => x.disposalNumber).SingleOrDefault() + ". " + "Not Surveyed");

                cbDrinkingWS.Text = db.tblDrinkingWaters.Where(x => x.sourceName == "Not Surveyed").Select(x => x.sourceNumber).SingleOrDefault() + ". " + "Not Surveyed";
                cbDrinkingWS.Enabled = false;

                cbWaterSource.Text = db.tblWaterSources.Where(x => x.sourceName == "Not Surveyed").Select(x => x.sourceNumber).SingleOrDefault() + ". " + "Not Surveyed";
                cbWaterSource.Enabled = false;

                cbTF.Text = db.tblToiletFacilities.Where(x => x.facilityName == "Not Surveyed").Select(x => x.facilityNumber).SingleOrDefault() + ". " + "Not Surveyed";
                cbTF.Enabled = false;
               
                cbWD.Text = db.tblWasteDisposals.Where(x => x.disposalName == "Not Surveyed").Select(x => x.disposalNumber).SingleOrDefault() + ". " + "Not Surveyed";
                cbWD.Enabled = false;
                
            }

            else
            {
                cbDrinkingWS.Enabled = true;
                cbWaterSource.Enabled = true;
                cbTF.Enabled = true;
                cbWD.Enabled = true;

                cbDrinkingWS.Items.Clear();
                cbWaterSource.Items.Clear();
                cbTF.Items.Clear();
                cbWD.Items.Clear();

                foreach (var v in db.tblWaterSources.Where(x => x.sourceName != "Not Surveyed"))
                    cbWaterSource.Items.Add(v.sourceNumber + ". " + v.sourceName);

                foreach (var v in db.tblDrinkingWaters.Where(x => x.sourceName != "Not Surveyed"))
                    cbDrinkingWS.Items.Add(v.sourceNumber + ". " + v.sourceName);

                foreach (var v in db.tblToiletFacilities.Where(x => x.facilityName != "Not Surveyed"))
                    cbTF.Items.Add(v.facilityNumber + ". " + v.facilityName);

                foreach (var v in db.tblWasteDisposals.Where(x => x.disposalName != "Not Surveyed"))
                    cbWD.Items.Add(v.disposalNumber + ". " + v.disposalName);
            }
        }
    }
}
