using FontAwesome.Sharp;
using Microsoft.Reporting.WinForms;
using Schools_Management_System.School_Data_Model;
using Schools_Management_System.SchoolManagementClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Schools_Management_System.School_Board
{
    public partial class StudentMonthlyFess : Form
    {
        schoolmanagementsystemEntities schoolDBEntity = new schoolmanagementsystemEntities();
        StudentMonthlyFeesData monthlyFeesData;
        StudentMonthlyFeesAmount monthlyFeesAmount;
        StudentMonthlyFee studentmonthlyFee;
        StudentInformation studentinfo = new StudentInformation();
        private string folder = Application.StartupPath + "\\Images";
        TextBox newTextOne;
        TextBox newTextTwo;
        public int studentIDForPayment = 0;
        int rowpositions;
        int rowIndex = 0;
        int n = 0;
        Decimal totalPayment = 0;
        Decimal paidAmount = 0;
        int value = 0;
        public StudentMonthlyFess()
        {
            InitializeComponent();
            pictureBoxSeletedStudentPicture.Image = Properties.Resources.DefaultImage;
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }


        private void iconButtonClose_Click(object sender, EventArgs e)
        {
            if (panelStudentSelectedRDCLFor.Visible == true)
            {
                panelStudentSelectedRDCLFor.Visible = false;
                panelStudentSelectedRDCLFor.SendToBack();
                panelSelectedStudentDuePayment.Visible = true;
                panelSelectedStudentDuePayment.BringToFront();
                panelSelectedStudentDuePayment.Dock = DockStyle.Fill;
                comboBoxStudentSelectedName.Visible = true;
                comboBoxStudentSelectedShift.Visible = true;
                comboBoxStudentSelectedClass.Visible = true;
                textBoxStudentSlectedRoll.Visible = true;
                iconButtonStudentSelectedSubmit.Visible = true;
            }
            else
            {
                DialogResult confirm = MessageBox.Show("Are You Sure To Close This Window?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    this.Close();

                }
                else
                {

                }
            }
        }
        // Database Sector For Load Student Name------------
        public void PaymentReportShoeFor()
        {
            panelStudentSelectedRDCLFor.Visible = true;
            panelStudentSelectedRDCLFor.Dock = DockStyle.Fill;
            panelStudentSelectedRDCLFor.BringToFront();
            comboBoxStudentSelectedName.Visible = false;
            comboBoxStudentSelectedShift.Visible = false;
            comboBoxStudentSelectedClass.Visible = false;
            textBoxStudentSlectedRoll.Visible = false;
            iconButtonStudentSelectedSubmit.Visible = false;
            //panelStudentMonthlyFeeUpper.Size = new Size(951,40);
            reportViewerSelectedStudentPaymentFor.ProcessingMode = ProcessingMode.Local;
            var queryStudentInfo = (from s in schoolDBEntity.StudentInformations
                                    where (s.StudentID == studentIDForPayment)
                                    select new
                                    {
                                        StudentName = s.StudentName,
                                        StudentFather_sName = s.StudentFather_sName,
                                        StudentMother_sName = s.StudentMother_sName,
                                        DateOfBirth = s.DateOfBirth,
                                        StudentAge = s.StudentAge,
                                        StudentStandar = s.StudentStandar,
                                        SudentRollNo = s.SudentRollNo,
                                        StudentBirthReg = s.StudentBirthReg,
                                        StudentEmail = s.StudentEmail,
                                        SudentSession = s.SudentSession,
                                        StudentMobile = s.StudentMobile,
                                        StudentNationality = s.StudentNationality,
                                        StudentAdmissionDate = s.StudentAdmissionDate,
                                        StudentGender = s.StudentGender

                                    }).ToList();

            var queryStudentImage = schoolDBEntity.ImagesForAlls.Where(s => s.StudentID == studentIDForPayment).Select(s => s.ImagePath).FirstOrDefault();
            var queryStudentShift = (from s in schoolDBEntity.StudentInformations
                                     join f in schoolDBEntity.StudentShiftInfoes on s.StudentID equals f.ShiftID
                                     where (s.StudentID == studentIDForPayment)
                                     select new
                                     {
                                         f.ShiftName
                                     }).ToList();

            var queryStudentGuirdianInfo = (from s in schoolDBEntity.StudentInformations
                                            join g in schoolDBEntity.GuirdianInformations on s.StudentID equals g.StudentID
                                            where (s.StudentID == studentIDForPayment)
                                            select new
                                            {
                                                g.GuirdianName,
                                                g.GuirdianOccupation,
                                                g.GuirdianRelation,
                                                g.GuirdianGender,
                                                g.GuirdianMobile,
                                                g.GuirdianEmail
                                            }).ToList();

            var queryStudentMonthlyFeeData = (from s in schoolDBEntity.StudentInformations
                                              join d in schoolDBEntity.StudentMonthlyFeesDatas on s.StudentID equals d.StudentID
                                              where (s.StudentID == studentIDForPayment)
                                              select new
                                              {
                                                  d.MonthlyPaymentFor
                                              }).ToList();

            var queryStudentMonthlyFeeAmount = (from s in schoolDBEntity.StudentInformations
                                                join a in schoolDBEntity.StudentMonthlyFeesAmounts on s.StudentID equals a.StudentID
                                                where (s.StudentID == studentIDForPayment)
                                                select new
                                                {
                                                    a.MonthlyFeeAmountFor
                                                }).ToList();

            var queryStudentMonthlyFee = (from s in schoolDBEntity.StudentInformations
                                          join t in schoolDBEntity.StudentMonthlyFees on s.StudentID equals t.StudentID
                                          where (s.StudentID == studentIDForPayment)
                                          select new
                                          {
                                              t.PaidAmount,
                                              t.TotalAmount,
                                              t.AmountPayDate
                                          }).ToList();
            var queryPermanentAddress = (from s in schoolDBEntity.StudentInformations
                                         join per in schoolDBEntity.PermanentAddresses on s.StudentID equals per.StudentID
                                         where (s.StudentID == studentIDForPayment)
                                         select new
                                         {
                                             per.PermanentVillage,
                                             per.PermanentRoad,
                                             per.PermanentDistrict,
                                             per.PermanentPostOffice,
                                             per.PermanentUpazila,
                                             per.PermanentPostCode
                                         }).ToList();
            var queryPresentAddress = (from s in schoolDBEntity.StudentInformations
                                       join pre in schoolDBEntity.PresentAddresses on s.StudentID equals pre.StudentID
                                       where (s.StudentID == studentIDForPayment)
                                       select new
                                       {
                                           pre.PresentVllage,
                                           pre.PresentRoad,
                                           pre.PresentDistrict,
                                           pre.PresentUpazila,
                                           pre.PresentPostOffice,
                                           pre.PresentPostCode
                                       }).ToList();
            reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Clear();
            reportViewerSelectedStudentPaymentFor.LocalReport.EnableExternalImages = true;
            var strmain1 = Application.StartupPath;
            var strmain2 = strmain1.Replace(@"\bin\Debug", "");
            string FilePath = @"file:\" + folder + @"\" + queryStudentImage;
            if (queryStudentInfo.Count > 0)
            {
                //reportViewerSelectedStudentPaymentFor.LocalReport.ReportPath = @"D:\School Management System\School Management System\Schools Management System\Schools Management System\Report For Payment\StudentPaymentSystems.rdlc";
                reportViewerSelectedStudentPaymentFor.LocalReport.ReportPath = strmain2 + @"\Report For Payment\StudentPaymentSystems.rdlc";
                ReportDataSource datasourcestudentInfo = new ReportDataSource("DataSetStudentsInformations", queryStudentInfo);
                //ReportDataSource datasourceStudentImage = new ReportDataSource("DataSetImageForStudents", FilePath);                
                ReportParameter[] param = new ReportParameter[1];
                param[0] = new ReportParameter("ImgPath", FilePath, true);
                ReportDataSource datasourceStudentShift = new ReportDataSource("DataSetShift", queryStudentShift);
                ReportDataSource datasourceStudentGuirdianInfo = new ReportDataSource("DataSetStudentGuirdianInfo", queryStudentGuirdianInfo);
                ReportDataSource datasourceStudentMonthlyFeeData = new ReportDataSource("DataSetMonthlyFeeData", queryStudentMonthlyFeeData);
                ReportDataSource datasourceStudentMonthlyFeeAmount = new ReportDataSource("DataSetMonthlyFeeAmount", queryStudentMonthlyFeeAmount);
                ReportDataSource datasourceStudentmonthlyFee = new ReportDataSource("DataSetMonthlyFee", queryStudentMonthlyFee);
                ReportDataSource datasourcepermanentAddress = new ReportDataSource("DataSetPermanentAddress", queryPermanentAddress);
                ReportDataSource datasourcePresentAddress = new ReportDataSource("DataSetPresentAddress", queryPresentAddress);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourcepermanentAddress);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourcePresentAddress);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourceStudentMonthlyFeeData);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourceStudentMonthlyFeeAmount);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourceStudentmonthlyFee);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourceStudentGuirdianInfo);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourcestudentInfo);
                //reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourceStudentImage);
                reportViewerSelectedStudentPaymentFor.LocalReport.SetParameters(param);
                reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Add(datasourceStudentShift);
                reportViewerSelectedStudentPaymentFor.RefreshReport();
            }
            else
            {
                SchoolManagement.ShowMessage("Data Is No Available!", "Try Again!", "Warning");
            }
        }
        public void LoadStudentSelectedName(System.Windows.Forms.ComboBox studentSelected)
        {
            studentSelected.Items.Add("--Select Student--");
            studentSelected.SelectedIndex = 0;
            var studentName = from s in schoolDBEntity.StudentInformations
                              select s.StudentName;
            foreach (var stuName in studentName)
            {
                studentSelected.Items.Add(stuName);
            }
            studentSelected.ValueMember = "StudentID";
            studentSelected.DisplayMember = "StudentName";
            studentSelected.Visible = true;
        }
        public void LoadStudentSelectedName(System.Windows.Forms.ComboBox studentSelected, System.Windows.Forms.ComboBox studentShift)
        {
            if (studentShift.SelectedIndex != 0)
            {
                var studentNameOnShift = from s in schoolDBEntity.StudentInformations
                                         join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                         where (f.ShiftName == studentShift.Text)
                                         select s.StudentName;
                if (studentNameOnShift.Count() != 0)
                {
                    studentSelected.Items.Add("--Select Student--");
                    studentSelected.SelectedIndex = 0;
                    foreach (var stuName in studentNameOnShift)
                    {
                        studentSelected.Items.Add(stuName);
                    }
                    studentSelected.ValueMember = "StudentID";
                    studentSelected.DisplayMember = "StudentName";
                    studentSelected.Visible = true;
                }
                else
                {
                    studentSelected.Visible = false;
                    SchoolManagement.ShowMessage("No Data is Found!", "Try Again!", "Warning");
                }
            }
            else
            {
                SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
            }
        }
        public void LoadStudentSelectedName(System.Windows.Forms.ComboBox studentSelected, System.Windows.Forms.ComboBox studentShift, System.Windows.Forms.ComboBox studentStand)
        {
            if (studentShift.SelectedIndex != 0 && studentStand.SelectedIndex != 0)
            {
                var studentNameonShiftStand = from s in schoolDBEntity.StudentInformations
                                              join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                              where (f.ShiftName == studentShift.Text && s.StudentStandar == studentStand.Text)
                                              select s.StudentName;
                if (studentNameonShiftStand.Count() != 0)
                {
                    studentSelected.Items.Add("--Select Student--");
                    studentSelected.SelectedIndex = 0;
                    foreach (var stuName in studentNameonShiftStand)
                    {
                        studentSelected.Items.Add(stuName);
                    }
                    studentSelected.ValueMember = "StudentID";
                    studentSelected.DisplayMember = "StudentName";
                    studentSelected.Visible = true;
                }
                else
                {
                    SchoolManagement.ShowMessage("No Data is Found!", "Try Again!", "Warning");
                    studentSelected.Visible = false;
                }
            }
            else
            {
                SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
            }
        }
        public void LoadStudentSelectedName(System.Windows.Forms.ComboBox studentSelected, System.Windows.Forms.ComboBox studentShift, System.Windows.Forms.ComboBox studentStand, TextBox studentRoll)
        {
            try
            {
                if (studentShift.SelectedIndex != 0 && studentStand.SelectedIndex != 0 && studentRoll.Text != "")
                {
                    var studentName = from s in schoolDBEntity.StudentInformations
                                      join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                      where (f.ShiftName == studentShift.Text && s.StudentStandar == studentStand.Text && s.SudentRollNo == studentRoll.Text)
                                      select s.StudentName;
                    if (studentName.Count() != 0)
                    {
                        studentSelected.Items.Add("--Select Student--");
                        foreach (var stuName in studentName)
                        {
                            studentSelected.Items.Add(stuName);
                            studentSelected.SelectedIndex = 0;
                        }

                        studentSelected.ValueMember = "StudentID";
                        studentSelected.DisplayMember = "StudentName";
                        studentSelected.Visible = true;
                    }
                    else
                    {
                        studentSelected.Items.Add("--Select Student--");
                        studentSelected.SelectedIndex = 0;
                        SchoolManagement.ShowMessage("No Data is Found!", "Try Again!", "Warning");
                    }
                }
                else
                {
                    SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }


        // Database Sector For Load Student Name------------
        private void iconButtonStudentSelectedSubmit_Click(object sender, EventArgs e)
        {
            if (iconButtonStudentSelectedSubmit.Text == "SUBMIT")
            {
                if (comboBoxStudentSelectedShift.SelectedIndex > 0 && comboBoxStudentSelectedClass.SelectedIndex > 0 && textBoxStudentSlectedRoll.Text != "")
                {
                    iconButtonStudentSelectedSubmit.Text = "RESET";
                    comboBoxStudentSelectedName.Items.Clear();
                    LoadStudentSelectedName(comboBoxStudentSelectedName, comboBoxStudentSelectedShift, comboBoxStudentSelectedClass, textBoxStudentSlectedRoll);
                }
                else
                {
                    SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
                }
            }
            else if (iconButtonStudentSelectedSubmit.Text == "SHOW")
            {
                if (comboBoxStudentSelectedShift.SelectedIndex > 0 && comboBoxStudentSelectedClass.SelectedIndex > 0 && textBoxStudentSlectedRoll.Text != "")
                {
                    var query = schoolDBEntity.StudentInformations.Where(s => s.StudentName == comboBoxStudentSelectedName.Text && s.StudentShiftInfo.ShiftName == comboBoxStudentSelectedShift.Text && s.StudentStandar == comboBoxStudentSelectedClass.Text && s.SudentRollNo == textBoxStudentSlectedRoll.Text).SingleOrDefault();
                    if (query != null)
                    {
                        labelStudentSelectedName.Text = query.StudentName;
                        labelSelectedStudentClass.Text = query.StudentStandar;
                        labelSelectedStudentRollNo.Text = query.SudentRollNo;
                        labelSelectedStudentGender.Text = query.StudentGender;
                        labelSelectedStudentDateofBirth.Text = query.DateOfBirth.ToString("MM / dd / yyyy");
                        labelStudentSelectedFatherName.Text = query.StudentFather_sName;
                        labelStudentSelectedMothername.Text = query.StudentMother_sName;
                        labelStudentSelectedEmail.Text = query.StudentEmail;
                        labelStudentSelectedSession.Text = query.SudentSession;
                        labelStudentSelectedMobile.Text = query.StudentMobile;
                        labelStdentSelectedName.Text = query.StudentShiftInfo.ShiftName;
                        labelSelectedStudentBirthRegFor.Text = query.StudentBirthReg;
                        labelSelectedStudentAgeFor.Text = query.StudentAge;
                        labelSelectedStudentsAdmissionDate.Text = query.StudentAdmissionDate.ToString("MM / dd / yyyy");
                        DateTime duraionsStudent = query.StudentAdmissionDate;
                        SchoolManagement.StudentDurationCals(duraionsStudent, labelSelectedStudentDurations);
                        //labelSelectedStudentDurations.Text = duraionsStudent.ToString("MM / dd / yyyy");
                        var selectStudentImage = query.ImagesForAlls.Select(s => s.ImagePath).SingleOrDefault();
                        string imagePath = System.IO.Path.Combine(folder, selectStudentImage);
                        Bitmap img = (Bitmap)Image.FromFile(imagePath);
                        pictureBoxSeletedStudentPicture.Image = img;
                        var studentpaymentStatus = schoolDBEntity.StudentMonthlyFees.Where(s => s.StudentID == studentIDForPayment).SingleOrDefault();
                        if (studentpaymentStatus == null)
                        {
                            circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(44, 53, 57);
                            circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(140, 0, 26);
                            circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(140, 0, 26);
                            labelSelectedStudentPaymentStatusText.Text = "Payment Status:" + "0 % " + "Paid";
                            labelSelectedStudentPaymentStatusText.BringToFront();
                            panelSelectedStudentPaymentConformations.Visible = false;
                            panelStudentSelectedMonthlyFee.Visible = true;
                            panelStudentSelectedMonthlyFee.BringToFront();
                            panelStudentSelectedMonthlyFee.Dock = DockStyle.Fill;
                            circularProgressBarPaymentStatus.OuterColor = Color.Red;
                            circularProgressBarPaymentStatus.ForeColor = Color.Red;
                            circularProgressBarPaymentStatus.Value = 0;
                            circularProgressBarPaymentStatus.Text = "0%";

                        }
                        else
                        {
                            panelSelectedStudentDuePayment.Visible = true;
                            panelSelectedStudentDuePayment.BringToFront();
                            panelSelectedStudentDuePayment.Dock = DockStyle.Fill;
                            totalPayment = studentpaymentStatus.TotalAmount;
                            paidAmount = studentpaymentStatus.PaidAmount;
                            value = Convert.ToInt32((100 / totalPayment) * paidAmount);
                            if (paidAmount == totalPayment)
                            {
                                circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(44, 53, 57);
                                circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(89, 232, 23);
                                circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(89, 232, 23);

                            }
                            else
                            {
                                circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(44, 53, 57);
                                circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(52, 128, 23);
                                circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(63, 255, 0);
                            }
                            circularProgressBarPaymentStatus.Value = value;
                            circularProgressBarPaymentStatus.Text = value + "%";
                            labelTotalAmountForSelectedStudent.Text = totalPayment.ToString();
                            labelLastPayAMountFor.Text = paidAmount.ToString();
                            var DueAmount = totalPayment - paidAmount;
                            labelSeletedStudentsAmountDue.Text = DueAmount.ToString();
                            labelTotalAmountForSelectedStudent.Visible = false;
                            panelSelectedStudentPaymentConformations.Visible = false;
                            panelStudentSelectedMonthlyFee.Visible = false;
                            labelSelectedStudentPaymentStatusText.Text = "Payment Status:" + value + " % " + "Paid";
                        }
                        iconButtonStudentSelectedSubmit.Visible = false;
                        comboBoxStudentSelectedShift.Visible = false;
                        comboBoxStudentSelectedClass.Visible = false;
                        textBoxStudentSlectedRoll.Visible = false;
                    }
                    else
                    {
                        iconButtonStudentSelectedSubmit.Text = "RESET";
                        SchoolManagement.ShowMessage("No Data is Found!", "Try Again!", "Warning");
                    }
                }
                else
                {
                    SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
                }
            }
            else
            {
                comboBoxStudentSelectedName.Items.Clear();
                LoadStudentSelectedName(comboBoxStudentSelectedName);
                iconButtonStudentSelectedSubmit.Text = "SUBMIT";
                comboBoxStudentSelectedShift.SelectedIndex = 0;
                comboBoxStudentSelectedClass.SelectedIndex = 0;
                textBoxStudentSlectedRoll.Text = "";
            }
        }

        private void StudentMonthlyFess_Load(object sender, EventArgs e)
        {
            comboBoxStudentSelectedShift.SelectedIndex = 0;
            comboBoxStudentSelectedClass.SelectedIndex = 0;
        }

        private void comboBoxStudentSelectedName_SelectedIndexChanged(object sender, EventArgs e)
        {
            showSelectedStudentsData();
        }

        private void comboBoxStudentSelectedShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudentSelectedShift.SelectedIndex == 0)
            {
                comboBoxStudentSelectedClass.SelectedIndex = 0;
                if (comboBoxStudentSelectedClass.SelectedIndex == 0)
                {
                    textBoxStudentSlectedRoll.Text = "";
                    if (textBoxStudentSlectedRoll.Text == "")
                    {
                        comboBoxStudentSelectedName.Items.Clear();
                        LoadStudentSelectedName(comboBoxStudentSelectedName);
                        //comboBoxStudentSelectedName.Visible = true;
                    }
                }
            }
            else
            {
                comboBoxStudentSelectedClass.Visible = true;
                //SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
            }
        }

        private void comboBoxStudentSelectedClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudentSelectedClass.SelectedIndex == 0)
            {
                textBoxStudentSlectedRoll.Text = "";
                if (comboBoxStudentSelectedShift.SelectedIndex == 0)
                {
                    comboBoxStudentSelectedClass.SelectedIndex = 0;
                    if (textBoxStudentSlectedRoll.Text == "")
                    {
                        comboBoxStudentSelectedName.Items.Clear();
                        LoadStudentSelectedName(comboBoxStudentSelectedName);
                        //comboBoxStudentSelectedName.Visible = true;
                    }
                }
            }
            else
            {
                textBoxStudentSlectedRoll.Visible = true;
                //SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
            }
        }

        private void textBoxStudentSlectedRoll_TextChanged(object sender, EventArgs e)
        {

            if (textBoxStudentSlectedRoll.Text == "")
            {
                if (comboBoxStudentSelectedClass.SelectedIndex == 0)
                {
                    if (comboBoxStudentSelectedShift.SelectedIndex == 0)
                    {
                        comboBoxStudentSelectedName.Items.Clear();
                        LoadStudentSelectedName(comboBoxStudentSelectedName);
                        //comboBoxStudentSelectedName.Visible = true;
                    }
                }
            }
            else
            {
                iconButtonStudentSelectedSubmit.Visible = true;
                //SchoolManagement.ShowMessage("Please Select Those Fields Carefully!", "Try Again!", "Warning");
            }
        }
        private void resetSelectedStudentFileds()
        {
            labelStudentSelectedName.Text = "";
            labelSelectedStudentClass.Text = "";
            labelSelectedStudentRollNo.Text = "";
            labelSelectedStudentGender.Text = "";
            labelSelectedStudentDateofBirth.Text = "";
            labelStudentSelectedFatherName.Text = "";
            labelStudentSelectedMothername.Text = "";
            labelStudentSelectedEmail.Text = "";
            labelStudentSelectedSession.Text = "";
            labelStudentSelectedMobile.Text = "";
            labelStdentSelectedName.Text = "";
            labelSelectedStudentBirthRegFor.Text = "";
            labelSelectedStudentAgeFor.Text = "";
            labelSelectedStudentsAdmissionDate.Text = "";
            labelSelectedStudentDurations.Text = "";
            pictureBoxSeletedStudentPicture.Image = Properties.Resources.DefaultImage;
            labelSelectedStudentPaymentStatusText.Text = " ";
            circularProgressBarPaymentStatus.Value = 0;
            circularProgressBarPaymentStatus.Text = "0%";
        }
        public void showSelectedStudentsData()
        {
            try
            {
                if (comboBoxStudentSelectedName.SelectedIndex == 0)
                {
                    resetSelectedStudentFileds();
                    tableLayoutPanelDynamicFieldsSections.Controls.Clear();
                    tableLayoutPanelDynamicFieldsSections.RowStyles.Clear();
                    tableLayoutPanelSelectedStudentPaymentConformations.Controls.Clear();
                    tableLayoutPanelSelectedStudentPaymentConformations.RowStyles.Clear();
                    rowIndex = 0;
                    panelSelectedStudentPaymentConformations.Visible = false;
                    panelStudentSelectedMonthlyFee.Visible = false;
                    iconButtonAddRemove.Enabled = true;
                    comboBoxStudentSelectedShift.Visible = true;
                    comboBoxStudentSelectedClass.Visible = true;
                    textBoxStudentSlectedRoll.Visible = true;
                    iconButtonStudentSelectedSubmit.Visible = true;
                    iconButtonPaymentDynamicFields.Enabled = true;
                    iconButtonPaymentDynamicFields.Visible = false;
                    iconButtonSelcetedPartFront.Visible = false;
                    panelSelectedStudentDuePayment.Visible = false;
                    panelStudentSelectedRDCLFor.Visible = false;
                    reportViewerSelectedStudentPaymentFor.Clear();
                    reportViewerSelectedStudentPaymentFor.Refresh();
                }
                else
                {
                    tableLayoutPanelDynamicFieldsSections.Controls.Clear();
                    tableLayoutPanelDynamicFieldsSections.RowStyles.Clear();
                    tableLayoutPanelSelectedStudentPaymentConformations.Controls.Clear();
                    tableLayoutPanelSelectedStudentPaymentConformations.RowStyles.Clear();
                    rowIndex = 0;
                    iconButtonAddRemove.Enabled = true;
                    iconButtonPaymentDynamicFields.Enabled = true;
                    iconButtonPaymentDynamicFields.Visible = false;
                    iconButtonSelcetedPartFront.Visible = false;
                    var studentSID = schoolDBEntity.StudentInformations.
                                   Where(s => s.StudentName == comboBoxStudentSelectedName.Text).
                                    Select(s => s.StudentID).Count();
                    if (studentSID > 1)
                    {
                        resetSelectedStudentFileds();
                        iconButtonStudentSelectedSubmit.Text = "SHOW";
                        comboBoxStudentSelectedShift.SelectedIndex = 0;
                        comboBoxStudentSelectedShift.Visible = true;
                        comboBoxStudentSelectedClass.Visible = true;
                        textBoxStudentSlectedRoll.Visible = true;
                        iconButtonStudentSelectedSubmit.Visible = true;
                        comboBoxStudentSelectedClass.SelectedIndex = 0;
                        textBoxStudentSlectedRoll.Text = "";
                        SchoolManagement.ShowMessage("Please Select Name By 'Shift', 'Class' & 'Roll No.'!", "Try Again!", "Warning");
                    }
                    else
                    {
                        var query = schoolDBEntity.StudentInformations.Where(s => s.StudentName == comboBoxStudentSelectedName.Text).SingleOrDefault();
                        labelStudentSelectedName.Text = query.StudentName;
                        labelSelectedStudentClass.Text = query.StudentStandar;
                        labelSelectedStudentRollNo.Text = query.SudentRollNo;
                        labelSelectedStudentGender.Text = query.StudentGender;
                        labelSelectedStudentDateofBirth.Text = query.DateOfBirth.ToString("MM / dd / yyyy");
                        labelStudentSelectedFatherName.Text = query.StudentFather_sName;
                        labelStudentSelectedMothername.Text = query.StudentMother_sName;
                        labelStudentSelectedEmail.Text = query.StudentEmail;
                        labelStudentSelectedSession.Text = query.SudentSession;
                        labelStudentSelectedMobile.Text = query.StudentMobile;
                        labelStdentSelectedName.Text = query.StudentShiftInfo.ShiftName;
                        labelSelectedStudentBirthRegFor.Text = query.StudentBirthReg;
                        labelSelectedStudentAgeFor.Text = query.StudentAge;
                        labelSelectedStudentsAdmissionDate.Text = query.StudentAdmissionDate.ToString("MM / dd / yyyy");
                        DateTime duraionsStudent = query.StudentAdmissionDate;
                        SchoolManagement.StudentDurationCals(duraionsStudent, labelSelectedStudentDurations);
                        //labelSelectedStudentDurations.Text = duraionsStudent.ToString("MM / dd / yyyy");
                        var selectStudentImage = query.ImagesForAlls.Select(s => s.ImagePath).SingleOrDefault();
                        string imagePath = System.IO.Path.Combine(folder, selectStudentImage);
                        Bitmap img = (Bitmap)Image.FromFile(imagePath);
                        pictureBoxSeletedStudentPicture.Image = img;
                        studentIDForPayment = query.StudentID;
                        var studentpaymentStatus = schoolDBEntity.StudentMonthlyFees.Where(s => s.StudentID == studentIDForPayment).SingleOrDefault();
                        if (studentpaymentStatus == null)
                        {
                            circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(44, 53, 57);
                            circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(140, 0, 26);
                            circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(140, 0, 26);
                            labelSelectedStudentPaymentStatusText.Text = "Payment Status: " + "0 % " + " Paid";
                            labelSelectedStudentPaymentStatusText.BringToFront();
                            panelSelectedStudentPaymentConformations.Visible = false;
                            panelStudentSelectedMonthlyFee.Visible = true;
                            panelStudentSelectedMonthlyFee.BringToFront();
                            panelStudentSelectedMonthlyFee.Dock = DockStyle.Fill;
                            circularProgressBarPaymentStatus.OuterColor = Color.Red;
                            circularProgressBarPaymentStatus.ForeColor = Color.Red;
                            circularProgressBarPaymentStatus.Value = 0;
                            circularProgressBarPaymentStatus.Text = "0%";

                        }
                        else
                        {
                            panelSelectedStudentDuePayment.Visible = true;
                            panelSelectedStudentDuePayment.BringToFront();
                            panelSelectedStudentDuePayment.Dock = DockStyle.Fill;
                            circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(0, 127, 255);
                            totalPayment = studentpaymentStatus.TotalAmount;
                            paidAmount = studentpaymentStatus.PaidAmount;
                            value = Convert.ToInt32((100 / totalPayment) * paidAmount);
                            if (paidAmount == totalPayment)
                            {
                                circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(44, 53, 57);
                                circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(89, 232, 23);
                                circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(89, 232, 23);

                            }
                            else
                            {
                                circularProgressBarPaymentStatus.OuterColor = Color.FromArgb(44, 53, 57);
                                circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(52, 128, 23);
                                circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(63, 255, 0);
                            }
                            circularProgressBarPaymentStatus.Value = value;
                            circularProgressBarPaymentStatus.Text = value + "%";
                            labelTotalAmountForSelectedStudent.Text = totalPayment.ToString();
                            labelLastPayAMountFor.Text = paidAmount.ToString();
                            var DueAmount = totalPayment - paidAmount;
                            labelSeletedStudentsAmountDue.Text = DueAmount.ToString();
                            panelStudentSelectedMonthlyFee.Visible = false;
                            panelSelectedStudentPaymentConformations.Visible = false;
                            panelStudentSelectedMonthlyFee.Visible = false;
                            labelSelectedStudentPaymentStatusText.Text = "Payment Status: " + value + " % " + "Paid";
                        }
                        comboBoxStudentSelectedShift.Visible = false;
                        comboBoxStudentSelectedClass.Visible = false;
                        textBoxStudentSlectedRoll.Visible = false;
                        iconButtonStudentSelectedSubmit.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Select Name By 'Shift', 'Class' & 'Roll No.'!", ex.Message);
            }

        }
        private List<Control> GetAllControls(Control container, List<Control> list)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox) list.Add(c);
                //if (c is Label) list.Add(c);
                //if (c is IconButton) list.Add(c);
                if (c.Controls.Count > 0)
                    list = GetAllControls(c, list);
            }

            return list;
        }

        private void iconButtonAddRemove_Click(object sender, EventArgs e)
        {
            n = rowIndex;
            tableLayoutPanelDynamicFieldsSections.RowStyles.Clear();
            tableLayoutPanelDynamicFieldsSections.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            newTextOne = new TextBox();
            newTextOne.Name = "PaymentFor_" + n.ToString();
            newTextOne.Dock = DockStyle.Top;
            newTextOne.BorderStyle = BorderStyle.None;
            tableLayoutPanelDynamicFieldsSections.Controls.Add(newTextOne, 1, rowIndex);
            newTextOne.TextChanged += new System.EventHandler(PaymentFor_TextChanged);
            //newTextOne.Validating += new CancelEventHandler(PaymentFor_Validating);

            Label lb = new Label();
            lb.Text = "*";
            lb.Font = new Font("Times New Roman", 15);
            lb.ForeColor = Color.White;
            lb.Visible = false;
            lb.Dock = DockStyle.Top;
            lb.Name = "PaymentForErr_" + n.ToString();
            tableLayoutPanelDynamicFieldsSections.Controls.Add(lb, 2, rowIndex);

            newTextTwo = new TextBox();
            newTextTwo.Name = "AmountFor_" + n.ToString();
            newTextTwo.Dock = DockStyle.Top;
            newTextTwo.BorderStyle = BorderStyle.None;
            tableLayoutPanelDynamicFieldsSections.Controls.Add(newTextTwo, 4, rowIndex);
            newTextTwo.TextChanged += new System.EventHandler(AmountFor_TextChanged);
            newTextOne.KeyPress += new KeyPressEventHandler(AmountFor_KeyPress);

            Label lbs = new Label();
            lbs.Text = "*";
            lbs.Font = new Font("Times New Roman", 15);
            lbs.ForeColor = Color.White;
            lbs.Visible = false;
            lbs.Dock = DockStyle.Top;
            lbs.Name = "AmountForErr_" + n.ToString();
            tableLayoutPanelDynamicFieldsSections.Controls.Add(lbs, 5, rowIndex);

            IconButton newIconButton = new IconButton();
            newIconButton.FlatStyle = FlatStyle.Flat;
            newIconButton.FlatAppearance.BorderSize = 1;
            newIconButton.Cursor = Cursors.Hand;
            newIconButton.AutoSize = false;
            newIconButton.IconColor = Color.White;
            newIconButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            newIconButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            newIconButton.IconSize = 20;
            newIconButton.Name = "iconButtonAddRemove_" + n.ToString();
            newIconButton.IconChar = FontAwesome.Sharp.IconChar.TimesCircle;
            newIconButton.Dock = DockStyle.Top;
            newIconButton.EnabledChanged += new System.EventHandler(this.newIconButton_EnabledChanged);
            newIconButton.Click += new System.EventHandler(this.newIconButton_Click);
            tableLayoutPanelDynamicFieldsSections.Controls.Add(newIconButton, 7, rowIndex);
            iconButtonPaymentDynamicFields.Visible = true;
            comboBoxStudentSelectedShift.Visible = false;
            rowIndex += 1;
        }

        private void PaymentFor_Validating(object sender, EventArgs e)
        {
            //Regex forText = new Regex("^[A-Z][a-zA-Z]*$");
            //var PaymentFor = sender as TextBox;
            //if (PaymentFor != null)
            //{
            //    if (forText.IsMatch(PaymentFor.Text))
            //    {
            //        int index = int.Parse(PaymentFor.Name.Split('_')[1]);
            //        tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentForErr_" + index, true)[0].Visible = false;
            //        tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = false;
            //    }
            //    else
            //    {
            //        PaymentFor.Text = null;
            //        SchoolManagement.ShowMessage("Only Valid Text Will Be Entered Here!", "Try Again!", "Warning");
            //        //PaymentFor.Focus();
            //        //return;
            //    }
            //}
            //else
            //{
            //    iconButtonPaymentDynamicFields.Visible = false;
            //}
        }
        private void AmountFor_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //var AmountFor = sender as TextBox;
            ////Regex regex = new Regex("^(-?[1-9]+\\d*([.]\\d+)?)$|^(-?0[.]\\d*[1-9]+)$|^0$");
            //if (AmountFor != null)
            //{
            //    if (e.KeyChar == '.' && AmountFor.Text.Contains("."))
            //    {
            //        // Stop more than one dot Char
            //        e.Handled = true;
            //    }
            //    else if (e.KeyChar == '.' && AmountFor.Text.Length == 0)
            //    {
            //        // Stop first char as a dot input
            //        e.Handled = true;
            //    }
            //    else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            //    {
            //        // Stop allow other than digit and control
            //        e.Handled = true;
            //    }
            //    else
            //    {
            //        AmountFor.Text = "";
            //        SchoolManagement.ShowMessage("Only Numbers Will Be Entered Here!", "Try Again!", "Warning");
            //        //AmountFor.Focus();
            //        //return;
            //    }
            //}
            //else
            //{
            //    iconButtonPaymentDynamicFields.Visible = false;
            //}
        }
        private void AmountFor_TextChanged(object sender, EventArgs e)
        {
            var AmountFor = sender as TextBox;
            if (AmountFor != null)
            {
                if (string.IsNullOrWhiteSpace(AmountFor.Text))
                {
                    int index = int.Parse(AmountFor.Name.Split('_')[1]);
                    tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountForErr_" + index, true)[0].Visible = true;
                    tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = true;
                }
                else
                {
                    if (decimal.TryParse(AmountFor.Text, out decimal isparsable))
                    {
                        int index = int.Parse(AmountFor.Name.Split('_')[1]);
                        tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountForErr_" + index, true)[0].Visible = false;
                        tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = false;
                    }
                    else
                    {
                        AmountFor.Text = "";
                        SchoolManagement.ShowMessage("Only Numbers Will Be Entered Here!", "Try Again!", "Warning");
                        AmountFor.Focus();
                        AmountFor.SelectAll();
                    }

                }
            }
        }
        private void PaymentFor_TextChanged(object sender, EventArgs e)
        {
            var PaymentFor = sender as TextBox;
            Regex forText = new Regex(@"^[a-zA-Z'.\s]{1,50}");
            if (PaymentFor != null)
            {
                if (string.IsNullOrWhiteSpace(PaymentFor.Text))
                {
                    int index = int.Parse(PaymentFor.Name.Split('_')[1]);
                    tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentForErr_" + index, true)[0].Visible = true;
                    tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = true;
                }
                else
                {
                    if (forText.IsMatch(PaymentFor.Text))
                    {
                        int index = int.Parse(PaymentFor.Name.Split('_')[1]);
                        tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentForErr_" + index, true)[0].Visible = false;
                        tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = false;
                    }
                    else
                    {
                        PaymentFor.Text = "";
                        SchoolManagement.ShowMessage("Only Valid Text Will Be Entered Here!", "Try Again!", "Warning");
                        PaymentFor.Focus();
                        PaymentFor.SelectAll();
                    }

                }
            }
        }

        private void newIconButton_Click(object sender, EventArgs e)
        {
            var newIconButton = sender as IconButton;
            if (newIconButton != null)
            {
                int index = int.Parse(newIconButton.Name.Split('_')[1]);
                if (tableLayoutPanelDynamicFieldsSections.Controls.Contains(tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentFor_" + index, true)[0])
                  && tableLayoutPanelDynamicFieldsSections.Controls.Contains(tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountFor_" + index, true)[0])
                  && tableLayoutPanelDynamicFieldsSections.Controls.Contains(tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentForErr_" + index, true)[0])
                  && tableLayoutPanelDynamicFieldsSections.Controls.Contains(tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountForErr_" + index, true)[0])
                  && tableLayoutPanelDynamicFieldsSections.Controls.Contains(tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0]))
                {
                    tableLayoutPanelDynamicFieldsSections.Controls.Remove(tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentFor_" + index, true)[0]);
                    tableLayoutPanelDynamicFieldsSections.Controls.Remove(tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountFor_" + index, true)[0]);
                    tableLayoutPanelDynamicFieldsSections.Controls.Remove(tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentForErr_" + index, true)[0]);
                    tableLayoutPanelDynamicFieldsSections.Controls.Remove(tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountForErr_" + index, true)[0]);
                    tableLayoutPanelDynamicFieldsSections.Controls.Remove(tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0]);
                    rowpositions = tableLayoutPanelDynamicFieldsSections.GetRow(newIconButton);
                    //List<Control> controlLists = new List<Control>();
                    //GetAllControls(tableLayoutPanelDynamicFieldsSections, controlLists);
                    //foreach (var newControlList in controlLists)
                    //{
                    //    var removeCon = tableLayoutPanelDynamicFieldsSections.Controls.Count;
                    //    for (int i = (rowpositions+1); i < removeCon; i++)
                    //    {
                    //        newControlList.Name.Replace("PaymentFor_" + i, "PaymentFor_" + rowpositions);
                    //        newControlList.Name.Replace("AmountFor_" + i, "AmountFor_" + rowpositions);
                    //        newControlList.Name.Replace("PaymentForErr_" + i, "PaymentForErr_" + rowpositions);
                    //        newControlList.Name.Replace("AmountForErr_" + i, "AmountForErr_" + rowpositions);
                    //        newControlList.Name.Replace("iconButtonAddRemove_" + i, "iconButtonAddRemove_" + rowpositions);
                    //    }
                    //}
                    ////rowpositions = 1;
                    tableLayoutPanelDynamicFieldsSections.RowStyles.Clear();
                }
                else
                {
                    rowpositions = 0;
                }

            }
            else
            {
                iconButtonPaymentDynamicFields.Visible = false;
            }
        }

        Label studnetPayTotalAmount;
        int inputFieldCount;
        private void iconButtonPaymentDynamicFields_Click(object sender, EventArgs e)
        {
            double totalAmount = 0;
            inputFieldCount = tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>().Count();
            var inputField = tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>();
            if (inputFieldCount != 0)
            {
                if (tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>().Any(t => string.IsNullOrWhiteSpace(t.Text)))
                {
                    SchoolManagement.ShowMessage("Please Fill Up Those Fields Carefully!", "Try Again!", "Warning");

                }
                else
                {
                    panelSelectedStudentPaymentConformations.Visible = true;
                    panelSelectedStudentPaymentConformations.BringToFront();
                    panelSelectedStudentPaymentConformations.Dock = DockStyle.Fill;
                    //tableLayoutPanelSelectedStudentPaymentConformations.RowStyles.Clear();
                    //tableLayoutPanelSelectedStudentPaymentConformations.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    List<Control> controlLists = new List<Control>();
                    GetAllControls(tableLayoutPanelDynamicFieldsSections, controlLists);

                    foreach (var newControlList in controlLists)
                    {
                        Label studnetPayData = new Label();
                        //studnetPayData.Text = inputFields.Text;
                        //studnetPayData.Name = inputFields.Name.ToString();
                        studnetPayData.TextAlign = ContentAlignment.MiddleLeft;
                        studnetPayData.BorderStyle = BorderStyle.FixedSingle;
                        studnetPayData.Font = new Font("Times New Roman", 15);
                        studnetPayData.ForeColor = Color.White;
                        studnetPayData.Visible = true;
                        studnetPayData.AutoSize = false;
                        studnetPayData.Dock = DockStyle.Top;
                        int newFieldsCol = int.Parse(newControlList.Name.Split('_')[1]);
                        int rowforNew = tableLayoutPanelDynamicFieldsSections.GetRow(newControlList);
                        if (newControlList.Name.Contains("PaymentFor_" + newFieldsCol))
                        {
                            studnetPayData.Text = newControlList.Text;
                            studnetPayData.Name = newControlList.Name.ToString();
                            tableLayoutPanelSelectedStudentPaymentConformations.Controls.Add(studnetPayData, 0, rowforNew);
                        }
                        else
                        {
                            studnetPayData.Text = newControlList.Text;
                            studnetPayData.Name = newControlList.Name.ToString();
                            tableLayoutPanelSelectedStudentPaymentConformations.Controls.Add(studnetPayData, 1, rowforNew);
                            totalAmount += double.Parse(newControlList.Text);
                        }
                    }
                    Label studnetPayTotal = new Label();
                    studnetPayTotal.Text = "Total Amount";
                    studnetPayTotal.Name = "Total_Amount".ToString();
                    studnetPayTotal.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    studnetPayTotal.TextAlign = ContentAlignment.MiddleLeft;
                    studnetPayTotal.BorderStyle = BorderStyle.FixedSingle;
                    studnetPayTotal.Font = new Font("Times New Roman", 15);
                    studnetPayTotal.ForeColor = Color.White;
                    studnetPayTotal.Visible = true;
                    studnetPayTotal.AutoSize = false;
                    studnetPayTotal.Dock = DockStyle.Top;
                    tableLayoutPanelSelectedStudentPaymentConformations.Controls.Add(studnetPayTotal, 0, inputFieldCount);

                    studnetPayTotalAmount = new Label();
                    studnetPayTotalAmount.Text = totalAmount.ToString();
                    studnetPayTotalAmount.Name = "Total_Amount_Pay".ToString();
                    studnetPayTotalAmount.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    studnetPayTotalAmount.TextAlign = ContentAlignment.MiddleLeft;
                    studnetPayTotalAmount.BorderStyle = BorderStyle.FixedSingle;
                    studnetPayTotalAmount.Font = new Font("Times New Roman", 15);
                    studnetPayTotalAmount.ForeColor = Color.White;
                    studnetPayTotalAmount.Visible = true;
                    studnetPayTotalAmount.AutoSize = false;
                    studnetPayTotalAmount.Dock = DockStyle.Top;
                    tableLayoutPanelSelectedStudentPaymentConformations.Controls.Add(studnetPayTotalAmount, 1, inputFieldCount);

                    iconButtonSaveButtonForPayments.Visible = true;
                    labelSelectedStudentTotalPaid.Visible = true;
                    textBoxSelectedStudentPayTotal.Visible = true;
                }
            }
            else
            {
                iconButtonPaymentDynamicFields.Visible = false;
                SchoolManagement.ShowMessage("No  Input Fields!", "Try Again!", "Warning");
            }

        }

        private void iconButtonPaymentDynamicFields_VisibleChanged(object sender, EventArgs e)
        {
            var inputFieldCount = tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>().Count();
            if (inputFieldCount != 0)
            {
                iconButtonPaymentDynamicFields.Visible = true;
            }
            else
            {
                iconButtonPaymentDynamicFields.Visible = false;
            }
        }
        double totalAmount = 0;
        private void iconButtonSelcetedPartFront_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>().Any(t => string.IsNullOrWhiteSpace(t.Text)))
            {
                SchoolManagement.ShowMessage("Please Fill Up Those Fields Carefully!", "Try Again!", "Warning");
            }
            else
            {
                var inputField = tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>();
                var inputFieldCount = tableLayoutPanelDynamicFieldsSections.Controls.OfType<TextBox>().Count();
                List<Control> controlLists = new List<Control>();
                GetAllControls(tableLayoutPanelDynamicFieldsSections, controlLists);

                foreach (var newControlList in controlLists)
                {
                    int newFieldsCol = int.Parse(newControlList.Name.Split('_')[1]);
                    int rowforNew = tableLayoutPanelDynamicFieldsSections.GetRow(newControlList);
                    if (newControlList.Name.Contains("PaymentFor_" + newFieldsCol))
                    {
                        if (tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentFor_" + newFieldsCol, true)[0].Text != "")
                        {
                            tableLayoutPanelSelectedStudentPaymentConformations.Controls.Find("PaymentFor_" + newFieldsCol, true)[0].Text = tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentFor_" + newFieldsCol, true)[0].Text;
                        }
                        else
                        {
                            //tableLayoutPanelSelectedStudentPaymentConformations.Controls.Find("PaymentFor_" + i, true)[0].Text = "";
                            SchoolManagement.ShowMessage("Please Fill Up Those Fields Carefully!", "Try Again!", "Warning");
                            break;
                        }
                    }
                    else
                    {
                        if (tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountFor_" + newFieldsCol, true)[0].Text != "")
                        {
                            tableLayoutPanelSelectedStudentPaymentConformations.Controls.Find("AmountFor_" + newFieldsCol, true)[0].Text = tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountFor_" + newFieldsCol, true)[0].Text;
                            totalAmount += double.Parse(tableLayoutPanelSelectedStudentPaymentConformations.Controls.Find("AmountFor_" + newFieldsCol, true)[0].Text);
                        }
                        else
                        {
                            SchoolManagement.ShowMessage("Please Fill Up Those Fields Carefully!", "Try Again!", "Warning");
                            //tableLayoutPanelSelectedStudentPaymentConformations.Controls.Find("AmountFor_" + i, true)[0].Text = " ";
                            break;
                        }
                    }
                }
                panelStudentSelectedMonthlyFee.Visible = false;
                panelSelectedStudentPaymentConformations.Visible = true;
                panelSelectedStudentPaymentConformations.BringToFront();
                panelSelectedStudentPaymentConformations.Dock = DockStyle.Fill;
                studnetPayTotalAmount.Text = totalAmount.ToString();
                iconButtonSelcetedPartFront.Visible = false;
                iconButtonPaymentDynamicFields.Enabled = true;
                iconButtonAddRemove.Enabled = true;
            }
        }

        private void iconButtonSelcetedPartBack_Click(object sender, EventArgs e)
        {
            panelStudentSelectedMonthlyFee.Visible = true;
            panelStudentSelectedMonthlyFee.Show();
            panelStudentSelectedMonthlyFee.BringToFront();
            panelStudentSelectedMonthlyFee.Dock = DockStyle.Fill;
            iconButtonSelcetedPartFront.Visible = true;
            iconButtonPaymentDynamicFields.Enabled = false;
            iconButtonAddRemove.Enabled = false;
            var icnFields = tableLayoutPanelDynamicFieldsSections.Controls.OfType<IconButton>().ToList();
            foreach (var iconfields in icnFields)
            {
                iconfields.Visible = false;
            }
        }
        private void textBoxSelectedStudentPayTotal_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSelectedStudentPayTotal.Text))
            {
                labelPaymentPaidErr.Visible = true;
            }
            else
            {
                if (decimal.TryParse(textBoxSelectedStudentPayTotal.Text, out decimal isparsable))
                {
                    if (double.Parse(textBoxSelectedStudentPayTotal.Text) > double.Parse(studnetPayTotalAmount.Text))
                    {
                        textBoxSelectedStudentPayTotal.Text = " ";
                        SchoolManagement.ShowMessage("Your Value Is Out Of Total Amount!", "Try Again!", "Warning");
                    }
                    else
                    {
                        labelPaymentPaidErr.Visible = false;
                    }
                }
                else
                {
                    textBoxSelectedStudentPayTotal.Text = "";
                    SchoolManagement.ShowMessage("Only Numbers Will Be Entered Here!", "Try Again!", "Warning");
                    labelPaymentPaidErr.Visible = true;
                    textBoxSelectedStudentPayTotal.Focus();
                    textBoxSelectedStudentPayTotal.SelectAll();
                }

            }
        }
        private void iconButtonSaveButtonForPayments_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSelectedStudentPayTotal.Text))
            {
                SchoolManagement.ShowMessage("Please Fill Up This Pay Amount Field!", "Try Again!", "Warning");
            }
            else
            {
                List<Control> controlLists = new List<Control>();
                GetAllControls(tableLayoutPanelDynamicFieldsSections, controlLists);
                string paymentfor = "";
                Decimal amountfor = 0;
                foreach (var newcontrolfields in controlLists)
                {
                    int newFieldsCol = int.Parse(newcontrolfields.Name.Split('_')[1]);
                    if (newcontrolfields.Name.Contains("PaymentFor_" + newFieldsCol))
                    {
                        paymentfor = newcontrolfields.Text;
                        monthlyFeesData = new StudentMonthlyFeesData()
                        {
                            MonthlyPaymentFor = paymentfor,
                            StudentID = studentIDForPayment
                        };
                        schoolDBEntity.StudentMonthlyFeesDatas.Add(monthlyFeesData);
                    }
                    else
                    {
                        amountfor = Decimal.Parse(newcontrolfields.Text);
                        monthlyFeesAmount = new StudentMonthlyFeesAmount()
                        {
                            MonthlyFeeAmountFor = amountfor,
                            StudentID = studentIDForPayment
                        };
                        schoolDBEntity.StudentMonthlyFeesAmounts.Add(monthlyFeesAmount);
                    }
                }
                studentmonthlyFee = new StudentMonthlyFee()
                {
                    PaidAmount = Decimal.Parse(textBoxSelectedStudentPayTotal.Text),
                    TotalAmount = Decimal.Parse(studnetPayTotalAmount.Text),
                    AmountPayDate = DateTime.Today,
                    StudentID = studentIDForPayment
                };
                schoolDBEntity.StudentMonthlyFees.Add(studentmonthlyFee);
                var data = schoolDBEntity.SaveChanges();
                if (data > 0)
                {
                    textBoxSelectedStudentPayTotal.Text = "";
                    var studentpaymentStatus = schoolDBEntity.StudentMonthlyFees.Where(s => s.StudentID == studentIDForPayment).SingleOrDefault();
                    var totalPayment = studentpaymentStatus.TotalAmount;
                    var paidAmount = studentpaymentStatus.PaidAmount;
                    var value = Convert.ToInt32((100 / totalPayment) * paidAmount);
                    labelTotalAmountForSelectedStudent.Text = totalPayment.ToString();
                    labelLastPayAMountFor.Text = paidAmount.ToString();
                    var DueAmount = totalPayment - paidAmount;
                    labelSeletedStudentsAmountDue.Text = DueAmount.ToString();
                    if (paidAmount == totalPayment)
                    {
                        circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(63, 255, 0);
                        circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(63, 255, 0);
                        labelSelectedStudentPaymentStatusText.ForeColor = Color.FromArgb(63, 255, 0);

                    }
                    else
                    {
                        circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(0, 255, 255);
                        labelSelectedStudentPaymentStatusText.ForeColor = Color.FromArgb(0, 255, 255);
                    }
                    circularProgressBarPaymentStatus.Value = value;
                    circularProgressBarPaymentStatus.Text = value + "% PAID";
                    labelSelectedStudentPaymentStatusText.Text = "Payment Status:" + value + " % " + "Paid";
                    iconButtonStudentSelectedSubmit.Visible = true;
                    iconButtonStudentSelectedSubmit.Text = "SUBMIT";
                    comboBoxStudentSelectedShift.Visible = true;
                    comboBoxStudentSelectedShift.SelectedIndex = 0;
                    comboBoxStudentSelectedClass.Visible = true;
                    comboBoxStudentSelectedClass.SelectedIndex = 0;
                    textBoxStudentSlectedRoll.Visible = true;
                    textBoxStudentSlectedRoll.Text = " ";
                    //comboBoxStudentSelectedName.SelectedIndex = 0;
                    SchoolManagement.ShowMessage("Data Added Successfully!", "Thank You!", "Success");
                    PaymentReportShoeFor();
                }
                else
                {
                    SchoolManagement.ShowMessage("Data Added Failed!", "Try Again!", "Error!");
                }
            }
        }
        private void newIconButton_EnabledChanged(object sender, EventArgs e)
        {
            var newIconButton = sender as IconButton;
            if (newIconButton != null)
            {
                int index = int.Parse(newIconButton.Name.Split('_')[1]);
                if (tableLayoutPanelDynamicFieldsSections.Controls.Find("PaymentFor_" + index, true)[0].Text == "" && tableLayoutPanelDynamicFieldsSections.Controls.Find("AmountFor_" + index, true)[0].Text == "")
                {
                    tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = true;
                }
                else
                {
                    tableLayoutPanelDynamicFieldsSections.Controls.Find("iconButtonAddRemove_" + index, true)[0].Enabled = false;
                }

            }
        }

        private void textBoxPayAmountForSelectedStudent_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPayAmountForSelectedStudent.Text))
            {
                labelPayAmountError.Visible = true;
            }
            else
            {
                if (decimal.TryParse(textBoxPayAmountForSelectedStudent.Text, out decimal isparsable))
                {
                    if (double.Parse(textBoxPayAmountForSelectedStudent.Text) > double.Parse(labelSeletedStudentsAmountDue.Text))
                    {
                        textBoxPayAmountForSelectedStudent.Text = " ";
                        SchoolManagement.ShowMessage("Your Value Is Out Of Due Amount!", "Try Again!", "Warning");
                    }
                    else
                    {
                        labelPayAmountError.Visible = false;
                    }
                }
                else
                {
                    textBoxPayAmountForSelectedStudent.Text = "";
                    SchoolManagement.ShowMessage("Only Numbers Will Be Entered Here!", "Try Again!", "Warning");
                    labelPayAmountError.Visible = true;
                    textBoxPayAmountForSelectedStudent.Focus();
                    textBoxPayAmountForSelectedStudent.SelectAll();
                }

            }
        }

        private void iconButtonForSelectedStudentPayment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxPayAmountForSelectedStudent.Text))
            {
                SchoolManagement.ShowMessage("Please Fill Up This Pay Amount Field!", "Try Again!", "Warning");
            }
            else
            {
                var studentpaymentStatus = schoolDBEntity.StudentMonthlyFees.Where(s => s.StudentID == studentIDForPayment).SingleOrDefault();
                var paymentSuccess = schoolDBEntity.StudentMonthlyFees.SingleOrDefault(s => s.StudentID == studentIDForPayment);
                if (paymentSuccess != null)
                {
                    paymentSuccess.PaidAmount = Decimal.Parse(textBoxPayAmountForSelectedStudent.Text) + studentpaymentStatus.PaidAmount;
                    paymentSuccess.AmountPayDate = DateTime.Today;

                }
                schoolDBEntity.SaveChanges();
                labelSeletedStudentsAmountDue.Text = (paymentSuccess.TotalAmount - paymentSuccess.PaidAmount).ToString();
                SchoolManagement.ShowMessage("Your Have Paid Successfully", "Thank You!", "Success");
                var totalPayment = studentpaymentStatus.TotalAmount;
                var paidAmount = studentpaymentStatus.PaidAmount;
                var value = Convert.ToInt32((100 / totalPayment) * paidAmount);
                var DueAmount = totalPayment - paidAmount;
                labelSeletedStudentsAmountDue.Text = DueAmount.ToString();
                if (paidAmount == totalPayment)
                {
                    circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(63, 255, 0);
                    circularProgressBarPaymentStatus.ForeColor = Color.FromArgb(63, 255, 0);
                    labelSelectedStudentPaymentStatusText.ForeColor = Color.FromArgb(63, 255, 0);

                }
                else
                {
                    circularProgressBarPaymentStatus.ProgressColor = Color.FromArgb(0, 255, 255);
                    labelSelectedStudentPaymentStatusText.ForeColor = Color.FromArgb(0, 255, 255);
                }
                circularProgressBarPaymentStatus.Value = value;
                circularProgressBarPaymentStatus.Text = value + "% PAID";
                textBoxPayAmountForSelectedStudent.Text = "";
                labelSelectedStudentPaymentStatusText.Text = "Payment Status:" + value + " % " + "Paid";
                PaymentReportShoeFor();
            }
        }

        private void iconButtonPaymentClose_Click(object sender, EventArgs e)
        {
            reportViewerSelectedStudentPaymentFor.LocalReport.DataSources.Clear();
            reportViewerSelectedStudentPaymentFor.RefreshReport();
            panelStudentSelectedRDCLFor.Visible = false;
            panelStudentSelectedRDCLFor.SendToBack();
            panelSelectedStudentDuePayment.Visible = true;
            panelSelectedStudentDuePayment.BringToFront();
            panelSelectedStudentDuePayment.Dock = DockStyle.Fill;
        }

    }
}
