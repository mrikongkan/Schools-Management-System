using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Schools_Management_System.School_Data_Model;
using System.Drawing;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Reporting.WinForms;

namespace Schools_Management_System.SchoolManagementClass
{
    public class ShowDataInGridView
    {
        private string[] studentStandard;
        private string folder = Application.StartupPath + "\\Images";
        schoolmanagementsystemEntities schoolDBEntity = new schoolmanagementsystemEntities();
        ImagesForAll imagestu = new ImagesForAll();

        StudentInformation studentInfo = new StudentInformation();
        public void PaymentReportShoeFor(Microsoft.Reporting.WinForms.ReportViewer paymentReport, int studentIDForPayment)
        {            
            paymentReport.ProcessingMode = ProcessingMode.Local;
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

            var queryStudentImage = (from i in schoolDBEntity.ImagesForAlls
                                     where (i.StudentID == studentIDForPayment)
                                     select new
                                     {
                                         i.ImagePath
                                     }).ToList();
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
            paymentReport.LocalReport.DataSources.Clear();
            var strmain1 = Application.StartupPath;
            var strmain2 = strmain1.Replace(@"\bin\Debug", "");
            if (queryStudentInfo.Count > 0)
            {
                //reportViewerSelectedStudentPaymentFor.LocalReport.ReportPath = @"D:\School Management System\School Management System\Schools Management System\Schools Management System\Report For Payment\StudentPaymentSystems.rdlc";
                paymentReport.LocalReport.ReportPath = strmain2 + @"\Report For Payment\StudentPaymentSystems.rdlc";
                ReportDataSource datasourcestudentInfo = new ReportDataSource("DataSetStudentsInformations", queryStudentInfo);
                ReportDataSource datasourceStudentImage = new ReportDataSource("DataSetImageForStudents", queryStudentImage);
                ReportDataSource datasourceStudentShift = new ReportDataSource("DataSetShift", queryStudentShift);
                ReportDataSource datasourceStudentGuirdianInfo = new ReportDataSource("DataSetStudentGuirdianInfo", queryStudentGuirdianInfo);
                ReportDataSource datasourceStudentMonthlyFeeData = new ReportDataSource("DataSetMonthlyFeeData", queryStudentMonthlyFeeData);
                ReportDataSource datasourceStudentMonthlyFeeAmount = new ReportDataSource("DataSetMonthlyFeeAmount", queryStudentMonthlyFeeAmount);
                ReportDataSource datasourceStudentmonthlyFee = new ReportDataSource("DataSetMonthlyFee", queryStudentMonthlyFee);
                ReportDataSource datasourcepermanentAddress = new ReportDataSource("DataSetPermanentAddress", queryPermanentAddress);
                ReportDataSource datasourcePresentAddress = new ReportDataSource("DataSetPresentAddress", queryPresentAddress);
                paymentReport.LocalReport.DataSources.Add(datasourcepermanentAddress);
                paymentReport.LocalReport.DataSources.Add(datasourcePresentAddress);
                paymentReport.LocalReport.DataSources.Add(datasourceStudentMonthlyFeeData);
                paymentReport.LocalReport.DataSources.Add(datasourceStudentMonthlyFeeAmount);
                paymentReport.LocalReport.DataSources.Add(datasourceStudentmonthlyFee);
                paymentReport.LocalReport.DataSources.Add(datasourceStudentGuirdianInfo);
                paymentReport.LocalReport.DataSources.Add(datasourcestudentInfo);
                paymentReport.LocalReport.DataSources.Add(datasourceStudentImage);
                paymentReport.LocalReport.DataSources.Add(datasourceStudentShift);
                paymentReport.RefreshReport();
            }
            else
            {
                SchoolManagement.ShowMessage("Data Is No Available!", "Try Again!", "Warning");
            }
        }
        public void dataFilterUseCheckBoxShift(DataGridView showStudentsData, string checboxShift, Label textvalu)
        {
            try
            {
                if (checboxShift == "Morning Shift")
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentShiftInfo.ShiftName == "Morning Shift")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource =bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
                else
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentShiftInfo.ShiftName == "Day Shift")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }

        public void dataFilterUseCheckBoxGender(DataGridView showStudentsData, string checboxGender, Label textvalu)
        {
            try
            {
                if (checboxGender == "Male")
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentGender == "Male")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
                else
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentGender == "Female")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }
        public void dataFilterUseCheckBoxShiftGender(DataGridView showStudentsData, string checboxGender, string checboxShift,Label textvalu)
        {
            try
            {
                if (checboxGender == "Male" && checboxShift == "Morning Shift")
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentGender == "Male" && s.StudentShiftInfo.ShiftName == "Morning Shift")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
                else if (checboxGender == "Male" && checboxShift == "Day Shift")
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentGender == "Male" && s.StudentShiftInfo.ShiftName == "Day Shift")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
                else if (checboxGender == "Female" && checboxShift == "Morning Shift")
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentGender == "Female" && s.StudentShiftInfo.ShiftName == "Day Shift")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
                else
                {
                    var query = from s in schoolDBEntity.StudentInformations
                                join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                where (s.StudentGender == "Female" && s.StudentShiftInfo.ShiftName == "Day Shift")
                                select new
                                {
                                    StudentName = s.StudentName,
                                    StudentFather_sName = s.StudentFather_sName,
                                    StudentMother_sName = s.StudentMother_sName,
                                    DateOfBirth = s.DateOfBirth,
                                    StudentAge = s.StudentAge,
                                    StudentStandar = s.StudentStandar,
                                    SudentRollNo = s.SudentRollNo,
                                    StudentGender = s.StudentGender,
                                    ShiftName = s.StudentShiftInfo.ShiftName,
                                    ImagePath = i.ImagePath
                                };
                    BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                    string numRows = showStudentsData.Rows.Count.ToString();
                    textvalu.Text = " Total Student : " +" " + (numRows);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }
        public void dataLoadDefault(DataGridView showStudentsData, Label textvalu)
        {
            try
            {
                var query =  (from s in schoolDBEntity.StudentInformations
                            join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                            join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID              
                            select new
                            {
                                StudentName = s.StudentName,
                                StudentFather_sName = s.StudentFather_sName,
                                StudentMother_sName = s.StudentMother_sName,
                                DateOfBirth = s.DateOfBirth,
                                StudentAge = s.StudentAge,
                                StudentStandar = s.StudentStandar,
                                SudentRollNo = s.SudentRollNo,
                                StudentGender = s.StudentGender,
                                ImagePath = i.ImagePath,
                                ShiftName = f.ShiftName
                            }).ToList();

                //var imageColumn = new DataGridViewImageColumn();                
                //for (var i = 0; i < showStudentsData.Rows.Count; i++)
                //{
                //    var bmpImage = (Bitmap)Image.FromFile(showStudentsData.Rows[i].Cells["ImaePathGV"].Value.ToString(), true);
                //    imageColumn.Image = bmpImage;
                //    imageColumn.ImageLayout = DataGridViewImageCellLayout.Stretch;
                //    imageColumn.Dispose();
                //    //dgvDisplayTiles.Rows.Add();
                //    showStudentsData.Rows[i].Cells["StudentImageGV"].Value = bmpImage;
                //    showStudentsData.Rows[i].Height = 100;
                //    //showStudentsData.Columns.Add(imageColumn);

                //}
            

                BindingSource bi = new BindingSource();
                    bi.DataSource = query.ToList();
                    showStudentsData.DataSource = bi;
                    showStudentsData.Refresh();
                    showStudentsData.AutoGenerateColumns = false;
                string numRows = showStudentsData.Rows.Count.ToString();
                textvalu.Text = " Total Student : " +" " + (numRows);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }       

            
        }

        public void ShowStudentInformationsDestilsByTextSearch(DataGridView showStudentsData, TextBox textvalu)
        {
            try
            {
                showStudentsData.AutoGenerateColumns = false;
                studentStandard = new string[20];
                studentStandard[0] = "--Select Class--";
                studentStandard[1] = "One";
                studentStandard[2] = "Two";
                studentStandard[3] = "Three";
                studentStandard[4] = "Four";
                studentStandard[5] = "Five";
                studentStandard[6] = "Six";
                studentStandard[7] = "Seven";
                studentStandard[8] = "Eight";
                studentStandard[9] = "Nine";
                studentStandard[10] = "Ten";
                foreach (var standard in studentStandard)
                {
                    if (textvalu.Text == standard)
                    {
                        var query = (from s in schoolDBEntity.StudentInformations
                                     join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                     join g in schoolDBEntity.GuirdianInformations on s.StudentID equals g.StudentID
                                     join pre in schoolDBEntity.PresentAddresses on s.StudentID equals pre.StudentID
                                     join per in schoolDBEntity.PermanentAddresses on s.StudentID equals per.StudentID
                                     join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                     where (s.StudentStandar == standard)
                                     select new
                                     {
                                         ImagePath = i.ImagePath,
                                         ShiftName = f.ShiftName,
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
                                         StudentGender = s.StudentGender,
                                         GuirdianInformation = "Guirdian Name: " + g.GuirdianName + ",\n Guirdian Gender : " + g.GuirdianGender + ",\n Guirdian Occupation : " + g.GuirdianOccupation + ",\n Relation With Guirdian : " + g.GuirdianRelation + ",\n Guirdian Email : " + g.GuirdianEmail + ",\n Guirdian Mobile : " +
                                         g.GuirdianMobile,
                                         PresentAddress = "Village : " + pre.PresentVllage + ",\n Road : " + pre.PresentRoad + ",\n District : " + pre.PresentDistrict + ",\n Upazila" + pre.PresentUpazila + ",\n Post Office : " + pre.PresentPostOffice + ",\n Post Code : " + pre.PresentPostCode,
                                         PermanentAddress = "Village : " + per.PermanentVillage + ",\n Road : " + per.PermanentRoad + ",\n District : " + per.PermanentDistrict + ",\n Upazila" + per.PermanentUpazila + ",\n Post Office : " + per.PermanentPostOffice + ",\n Post Code : " + per.PermanentPostCode,

                                     }).ToList();

                        BindingSource bi = new BindingSource();
                        bi.DataSource = query.ToList();
                        showStudentsData.DataSource = bi;

                        showStudentsData.Refresh();
                        //string numRows = showStudentsData.Rows.Count.ToString();
                        //textvalu.Text = " Total Student : " + " " + (numRows);
                    }                    
                }           
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }           
        }
        public void ShowStudentInformationsDestils(DataGridView showStudentsData, TextBox textvalu)
        {
            try
            {

                showStudentsData.AutoGenerateColumns = false;
                var query = (from s in schoolDBEntity.StudentInformations
                             join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                             join g in schoolDBEntity.GuirdianInformations on s.StudentID equals g.StudentID
                             join pre in schoolDBEntity.PresentAddresses on s.StudentID equals pre.StudentID
                             join per in schoolDBEntity.PermanentAddresses on s.StudentID equals per.StudentID
                             join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                             select new
                             {
                                 ImagePath = i.ImagePath,
                                 ShiftName = f.ShiftName,
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
                                 StudentGender = s.StudentGender,
                                 GuirdianInformation = "Guirdian Name: " + g.GuirdianName + ",\n Guirdian Gender : " + g.GuirdianGender + ",\n Guirdian Occupation : " + g.GuirdianOccupation + ",\n Relation With Guirdian : " + g.GuirdianRelation + ",\n Guirdian Email : " + g.GuirdianEmail + ",\n Guirdian Mobile : " +
                                 g.GuirdianMobile,
                                 PresentAddress = "Village : " + pre.PresentVllage + ",\n Road : " + pre.PresentRoad + ",\n District : " + pre.PresentDistrict + ",\n Upazila" + pre.PresentUpazila + ",\n Post Office : " + pre.PresentPostOffice + ",\n Post Code : " + pre.PresentPostCode,
                                 PermanentAddress = "Village : " + per.PermanentVillage + ",\n Road : " + per.PermanentRoad + ",\n District : " + per.PermanentDistrict + ",\n Upazila" + per.PermanentUpazila + ",\n Post Office : " + per.PermanentPostOffice + ",\n Post Code : " + per.PermanentPostCode,

                             }).ToList();

                BindingSource bi = new BindingSource();
                bi.DataSource = query.ToList();
                showStudentsData.DataSource = bi;

                showStudentsData.Refresh();
                //string numRows = showStudentsData.Rows.Count.ToString();
                //textvalu.Text = " Total Student : " + " " + (numRows);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }


        }

        public void dataFilterUseComboBox(DataGridView showStudentsData, ComboBox comboBoxSelect, Label textvalu)
        {
            try
            {
                studentStandard = new string[20];
                studentStandard[0] = "--Select Class--";
                studentStandard[1] = "One";
                studentStandard[2] = "Two";
                studentStandard[3] = "Three";
                studentStandard[4] = "Four";
                studentStandard[5] = "Five";
                studentStandard[6] = "Six";
                studentStandard[7] = "Seven";
                studentStandard[8] = "Eight";
                studentStandard[9] = "Nine";
                studentStandard[10] = "Ten";

                foreach (var standard in studentStandard)
                {
                    if (comboBoxSelect.Text == standard)
                    {
                        var query = from s in schoolDBEntity.StudentInformations
                                    join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                    join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                    where (s.StudentStandar == standard)
                                    select new
                                    {
                                        StudentName = s.StudentName,
                                        StudentFather_sName = s.StudentFather_sName,
                                        StudentMother_sName = s.StudentMother_sName,
                                        DateOfBirth = s.DateOfBirth,
                                        StudentAge = s.StudentAge,
                                        StudentStandar = s.StudentStandar,
                                        SudentRollNo = s.SudentRollNo,
                                        StudentGender = s.StudentGender,
                                        ShiftName = s.StudentShiftInfo.ShiftName,
                                        ImagePath = i.ImagePath
                                    };
                        BindingSource bi = new BindingSource();
                        bi.DataSource = query.ToList();
                        showStudentsData.DataSource = bi;
                        showStudentsData.Refresh();
                        showStudentsData.AutoGenerateColumns = false;
                        string numRows = showStudentsData.Rows.Count.ToString();
                        textvalu.Text = " Total Student : " +" " + (numRows);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }

        }

        public void dataFilterUseComboBoxGender(DataGridView showStudentsData, ComboBox comboBoxSelect, string checboxGender,Label textvalu)
        {
            try
            {
                studentStandard = new string[20];
                studentStandard[0] = "--Select Class--";
                studentStandard[1] = "One";
                studentStandard[2] = "Two";
                studentStandard[3] = "Three";
                studentStandard[4] = "Four";
                studentStandard[5] = "Five";
                studentStandard[6] = "Six";
                studentStandard[7] = "Seven";
                studentStandard[8] = "Eight";
                studentStandard[9] = "Nine";
                studentStandard[10] = "Ten";
                if (checboxGender == "Male")
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentGender == "Male")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
                else
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentGender == "Female")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }


        public void dataFilterUseComboBoxShift(DataGridView showStudentsData, ComboBox comboBoxSelect, string checboxShift,Label textvalu)
        {
            try
            {
                studentStandard = new string[20];
                studentStandard[0] = "--Select Class--";
                studentStandard[1] = "One";
                studentStandard[2] = "Two";
                studentStandard[3] = "Three";
                studentStandard[4] = "Four";
                studentStandard[5] = "Five";
                studentStandard[6] = "Six";
                studentStandard[7] = "Seven";
                studentStandard[8] = "Eight";
                studentStandard[9] = "Nine";
                studentStandard[10] = "Ten";

                if (checboxShift == "Morning Shift")
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentShiftInfo.ShiftName == "Morning Shift")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
                else
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentShiftInfo.ShiftName == "Day Shift")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }

        public void dataFilterUseComboBoxCheckBox(DataGridView showStudentsData, ComboBox comboBoxSelect,string checboxGender, string checboxShift, Label textvalu)
        {
            try
            {
                studentStandard = new string[20];
                studentStandard[0] = "--Select Class--";
                studentStandard[1] = "One";
                studentStandard[2] = "Two";
                studentStandard[3] = "Three";
                studentStandard[4] = "Four";
                studentStandard[5] = "Five";
                studentStandard[6] = "Six";
                studentStandard[7] = "Seven";
                studentStandard[8] = "Eight";
                studentStandard[9] = "Nine";
                studentStandard[10] = "Ten";

                if (checboxGender == "Male" && checboxShift == "Morning Shift")
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentGender == "Male" && s.StudentShiftInfo.ShiftName == "Morning Shift")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
                else if (checboxGender == "Male" && checboxShift == "Day Shift")
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentGender == "Male" && s.StudentShiftInfo.ShiftName == "Day Shift")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
                else if (checboxGender == "Female" && checboxShift == "Day Shift")
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentGender == "Female" && s.StudentShiftInfo.ShiftName == "Day Shift")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
                else
                {
                    foreach (var standard in studentStandard)
                    {
                        if (comboBoxSelect.Text == standard)
                        {
                            var query = from s in schoolDBEntity.StudentInformations
                                        join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                                        join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                                        where (s.StudentStandar == standard && s.StudentGender == "Female" && s.StudentShiftInfo.ShiftName == "Morning Shift")
                                        select new
                                        {
                                            StudentName = s.StudentName,
                                            StudentFather_sName = s.StudentFather_sName,
                                            StudentMother_sName = s.StudentMother_sName,
                                            DateOfBirth = s.DateOfBirth,
                                            StudentAge = s.StudentAge,
                                            StudentStandar = s.StudentStandar,
                                            SudentRollNo = s.SudentRollNo,
                                            StudentGender = s.StudentGender,
                                            ShiftName = s.StudentShiftInfo.ShiftName,
                                            ImagePath = i.ImagePath
                                        };
                            BindingSource bi = new BindingSource();
                            bi.DataSource = query.ToList();
                            showStudentsData.DataSource = bi;
                            showStudentsData.Refresh();
                            showStudentsData.AutoGenerateColumns = false;
                            string numRows = showStudentsData.Rows.Count.ToString();
                            textvalu.Text = " Total Student : " +" " + (numRows);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }
       
    }
}
