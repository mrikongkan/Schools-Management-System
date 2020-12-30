using Newtonsoft.Json;
using Schools_Management_System.SchoolManagementClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using Schools_Management_System.School_Data_Model;
using Image = System.Drawing.Image;


namespace Schools_Management_System.School_Board
{
    public partial class ManageStudentsMain : ChildParents
    {
        // Variables Initializations For Global---------------
        private string studentShiftName;  
        private DateTime AdmissionDate;
        private string imagePath;
        private string fileName;
        private string pathString;
        private string folder = Application.StartupPath + "\\Images";
        private int data = 0;
        private string studentGender;
        string fileNameNew = "";

        // Variables Initializations For Global---------------

        // Object Instance Creations-----------------

        schoolmanagementsystemEntities schoolDBEntity = new schoolmanagementsystemEntities();
        StudentInformation studentInfo = new StudentInformation();
        GuirdianInformation guirdianInfo = new GuirdianInformation();
        PermanentAddress studentPerInfo = new PermanentAddress();
        PresentAddress studentPreInfo = new PresentAddress();
        StudentShiftInfo studentShift = new StudentShiftInfo();
        ImagesForAll imagestu = new ImagesForAll();
        District district = new District();
        Upazila upazila = new Upazila();
        ShowDataInGridView sdata = new ShowDataInGridView();


        // Object Instance Creations-----------------

        public ManageStudentsMain()
        {            
            InitializeComponent();
            pictureBoxStudents.Image = Properties.Resources.DefaultImage;
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (System.Windows.Forms.Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }
        // Custom function/Method For Custom Use---------------

        private void DataFillReset()
        {
            textBoxStufdentName.Text = "";
            textBoxFatherName.Text = "";
            textBoxMotherName.Text = "";
            dateTimePickerStudents.Value = DateTime.Now;
            textBoxStudentAge.Text = "";
            comboBoxStudentsClass.SelectedIndex = 0;
            textBox1RollNo.Text = "";
            textBoxBirthReg.Text = "";
            textBoxEmailId.Text = "";
            textBoxStudentSession.Text = "";
            comboBoxGender.Text = "";
            textBoxMobileNo.Text = "";
            textBoxNationality.Text = "";
        }

        public void refreshDistrictForPresentAddress(System.Windows.Forms.ComboBox DistrictCombo)
        {
            DistrictCombo.Items.Add("--Select District--");
            DistrictCombo.SelectedIndex = 0;
            var districts = schoolDBEntity.Districts.ToList();
            foreach (var district in districts)
            {
                DistrictCombo.Items.Add(district);
            }
            DistrictCombo.ValueMember = "DistrictID";
            DistrictCombo.DisplayMember = "DistrictName";
        }

        public void datagridviewShowImage(DataGridView imageViewer)
        {          
            imageViewer.Refresh();
            int i = 0;
            foreach (DataGridViewRow rows in imageViewer.Rows)
            {
                Bitmap image = (Bitmap)Image.FromFile(pathString);
                Bitmap imageRS = new Bitmap(image, new Size(80, 80));
                if (pathString != null)
                {
                    rows.Cells["StudentImageGV"].Value = image;
                    imageViewer[0, i].Value = imageRS;
                    rows.Height = 80;
                    i++;
                }
                else
                {
                    MessageBox.Show("Invalid Path");
                }
            }
        }
        System.Drawing.Image stuImage;
        public string SaveImage(string _imagePath)
        {
            fileName = Path.GetFileNameWithoutExtension(_imagePath);
            string extension = Path.GetExtension(_imagePath);
            string changefileName = fileName.Replace(fileName,textBoxStufdentName.Text);
            fileNameNew = changefileName + DateTime.Now.ToString("yymmssfff") + extension;                        
            pathString = System.IO.Path.Combine(folder, fileNameNew);
            Image saveImage = pictureBoxStudents.Image;
            saveImage.Save(pathString);
            return fileNameNew;
        }
        public void refreshUpazilaForPresentAddress(System.Windows.Forms.ComboBox UpazilaCombo, System.Windows.Forms.ComboBox DistrictCom)
        {
            district.DistrictID = DistrictCom.SelectedIndex;
            var upazilas = from upa in schoolDBEntity.Upazilas where upa.DistrictID.Value.Equals(district.DistrictID) select new { upa.UpazilaID, upa.UpazilaName };
            var comboupazila = upazilas.ToList();
            UpazilaCombo.Items.Clear();
            UpazilaCombo.Items.Add("--Select Upazila--");
            UpazilaCombo.SelectedItem = "--Select Upazila--";
            if (comboupazila != null)
            {
                foreach (var newupazila in comboupazila)
                {
                    UpazilaCombo.Items.Add(newupazila);
                }
                UpazilaCombo.ValueMember = "UpazilaID";
                UpazilaCombo.DisplayMember = "UpazilaName";
            }
        }

        // Custom function/Method For Custom Use---------------

        //This Form Main things-----------------------
        private void ManageStudentsMain_Load(object sender, EventArgs e)
        {
            comboBoxShowDataStandard.SelectedIndex = 0 ;            
            dateTimePickerStudents.Value = DateTime.Today;
            buttonShowData.Visible = true;
            dateTimePickerStudentAdmissionDate.Value = DateTime.Today;            
            if (panelDayorMorningContainer.Visible == false)
            {
                panelDayorMorningContainer.Show();
                panelStudentDataViewer.BringToFront();
            }
            comboBoxGender.SelectedIndex = 0;
            comboBoxReligion.SelectedIndex = 0;
            comboBoxStudentsClass.SelectedIndex = 0;
            comboBoxGuirdianGenderSel.SelectedIndex = 0;
        }

        public override void iconButtonClose_Click(object sender, EventArgs e)
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
        //This Form Main things-----------------------

        // Universal Button for this Form --------------

        public void studentDataLoad(DataGridView dataview)
        {
            try
            {
                var query = (from s in schoolDBEntity.StudentInformations
                            join i in schoolDBEntity.ImagesForAlls on s.StudentID equals i.StudentID
                            join f in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals f.ShiftID
                            where (s.StudentShiftInfo.ShiftName == studentShiftName && s.StudentStandar == comboBoxStudentsClass.Text)
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

                BindingSource bi = new BindingSource();
                bi.DataSource = query.ToList();
                dataview.DataSource = bi;
                //datagridviewShowImage(dataview);
                dataview.Refresh();
                dataview.AutoGenerateColumns = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            }
        }        


        public override void buttonAdd_Click(object sender, EventArgs e)
        {
            if (buttonAdd.Text == "Add")
            {
                if (checkBoxForMorning.CheckState == CheckState.Checked || checkBoxForDayShift.CheckState == CheckState.Checked)
                {
                    buttonShowData.Visible = false;
                    panelSudentFormOne.Visible = true;
                    panelSudentFormOne.Show();
                    panelSudentFormOne.BringToFront();
                    buttonAdd.Text = "Cancel";
                    buttonSave.Visible = true;
                    buttonSave.Text = "Next";
                }
                else
                {
                    SchoolManagement.ShowMessage("Please Select Any Shift Then Press \"Add\" Button!", "Try Again!", "Warning");
                }
            }
            else
            {
                buttonShowData.Visible = true;
                buttonAdd.Text = "Add";
                buttonAdd.Visible = false;
                buttonSave.Visible = false;
                panelSudentFormOne.Visible = false;
                panelDayorMorningContainer.Visible = true;
                SchoolManagement.Enable_Reset(panelSudentFormOne, tableLayoutPanelStudentFormOne);
                SchoolManagement.Enable_Reset(panelMornigShiftButton, tableLayoutPanelMorningShiftButton);
                SchoolManagement.Enable_Reset(panelDayShiftButton, tableLayoutPanelDayShiftButton);                
            }
        }

        //string selectRow;
        public override void buttonDelete_Click(object sender, EventArgs e)
        {
            //Delete data
            int total = dataGridViewShowShiftwiseData.Rows.Cast<DataGridViewRow>().Where(p => Convert.ToBoolean(p.Cells["SelectGV"].Value) == true).Count();

            //List<DataGridViewRow> total = (from row in dataGridViewShowShiftwiseData.Rows.Cast<DataGridViewRow>()
                                                  //where Convert.ToBoolean(row.Cells["SelectGV"].Value) == true
                                                  //select row).ToList();

            if (total > 0)
            {
                string message = $"Are you sure want to delete {total} row?";
                if (total > 1)
                    message = $"Are you sure want to delete {total} rows";
                if (MessageBox.Show(message, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    for (int i = dataGridViewShowShiftwiseData.RowCount - 1; i >= 0; i--)
                    {
                        DataGridViewRow row = dataGridViewShowShiftwiseData.Rows[i];
                        //Check row selected
                        if (Convert.ToBoolean(row.Cells["SelectGV"].Value) == true)
                        {
                            var DelID = Convert.ToInt32(row.Cells[0].Value);
                            //var query = (from s in schoolDBEntity.StudentInformations
                            //             join st in schoolDBEntity.StudentShiftInfoes on s.ShiftID equals st.ShiftID
                            //             join t in schoolDBEntity.GuirdianInformations on s.StudentID equals t.StudentID
                            //             join b in schoolDBEntity.PresentAddresses on s.StudentID equals b.StudentID
                            //             join c in schoolDBEntity.PermanentAddresses on s.StudentID equals c.StudentID
                            //             join d in schoolDBEntity.ImagesForAlls on s.StudentID equals d.StudentID
                            //             where(s.StudentID == DelID)
                            //             select s);

                            //PlanDG.ItemsSource = query;
                            var deleteRecord = schoolDBEntity.StudentInformations.First(b => b.StudentID == DelID);
                            schoolDBEntity.StudentInformations.Remove(deleteRecord);

                            //schoolDBEntity.StudentInformations.Remove((StudentInformation)row.DataBoundItem);
                            //tableStudentInformationsBindingSource.RemoveAt(row.Index);
                            //foreach (var item in query)
                            //{
                            //    if (!studentShift.StudentInformations.Contains(item))
                            //        schoolDBEntity.StudentInformations.Remove(item);
                            //}
                        }
                    }
                    dataGridViewShowShiftwiseData.Columns.Clear();
                    studentDataLoad(dataGridViewShowShiftwiseData);
                    buttonEdit.Text = "Save";
                }
            }
            else
            {
                SchoolManagement.ShowMessage("At First Select One or More Row!", "Try Again!", "Warning!");
            }























            //List<DataGridViewRow> selectedRows = (from row in dataGridViewShowShiftwiseData.Rows.Cast<DataGridViewRow>()
            //                                      where Convert.ToBoolean(row.Cells["SelectGV"].Value) == true
            //                                      select row).ToList();
            //string message = $"Are You Want To Delete {selectedRows} Row?";
            //if (selectedRows != null)
            //{
            //    message = $"Are You Want To Delete {selectedRows} Row?";
            //}
            //else
            //{
            //    SchoolManagement.ShowMessage("At First Select One or More Row!", "Try Again!", "Warning!");
            //    return;
            //}
            //if (MessageBox.Show(string.Format("Do you want to delete {0} rows?", selectedRows.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    foreach (DataGridViewRow row in selectedRows)
            //    {
            //        for (int i = dataGridViewShowShiftwiseData.RowCount - 1; i >= 0; i--)
            //        {
            //            DataGridViewRow row = dataGridViewShowShiftwiseData.Rows[i];
            //            if (Convert.ToBoolean(row.Cells["SelectGV"].Value) == true)
            //            {
            //                schoolDBEntity.StudentInformations.Remove((StudentInformation)row.DataBoundItem);
            //                tableStudentInformationsBindingSource.RemoveAt(i);
            //                var DelID = Convert.ToInt32(row.Cells[0].Value.ToString());
            //                var childs = schoolDBEntity.StudentInformations.Where(c => c.ShiftID == DelID);
            //                foreach (var item in childs)
            //                {
            //                    if (!studentShift.StudentInformations.Contains(item))
            //                        schoolDBEntity.StudentInformations.Remove(item);
            //                }

            //                //var deleteRecord = schoolDBEntity.StudentInformations.First(b => b.StudentID == DelID);
            //                //schoolDBEntity.StudentInformations.Remove(deleteRecord);                           
            //            }
            //        }
            //        dataGridViewShowShiftwiseData.AllowUserToAddRows = false;
            //        dataGridViewShowShiftwiseData.Columns.Clear();
            //        sdata.dataLoadDefault(dataGridViewShowShiftwiseData);
            //    }
            //}





            //int i = 0;
            //int total;
            //List<int> selectedRow = new List<int>();           
            //try
            //{
            //    total = dataGridViewShowShiftwiseData.Rows.Cast<DataGridViewRow>().Where(p => Convert.ToBoolean(p.Cells["SelectGV"].Value) == true).Count();
            //    labelShowDataTablesDetails.Text = "Total Students :" + total.ToString();
            //    string message = $"Are You Want To Delete {total} Row?";
            //    if (total > 0)
            //    {
            //        for (i = 0; i <= dataGridViewShowShiftwiseData.RowCount - 1; i++)
            //        {
            //            selectedRow.Add(i);
            //        }
            //        message = $"Are You Want To Delete {total} Row?";
            //    }
            //    else
            //    {
            //        SchoolManagement.ShowMessage("At First Select One or More Row!", "Try Again!", "Warning!");
            //        return;
            //    }
            //    if (MessageBox.Show(message, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //    {
            //        for (i = dataGridViewShowShiftwiseData.RowCount - 1; i >= 0; i--)
            //        {
            //            DataGridViewRow row = dataGridViewShowShiftwiseData.Rows[i];
            //            if (Convert.ToBoolean(row.Cells["SelectGV"].Value) == true)
            //            {
            //                schoolDBEntity.StudentInformations.Remove((StudentInformation)row.DataBoundItem);
            //                tableStudentInformationsBindingSource.RemoveAt(i);
            //                var DelID = Convert.ToInt32(row.Cells[0].Value.ToString());
            //                var childs = schoolDBEntity.StudentInformations.Where(c => c.ShiftID  == DelID);
            //                foreach (var item in childs)
            //                {
            //                    if (!studentShift.StudentInformations.Contains(item))
            //                        schoolDBEntity.StudentInformations.Remove(item);
            //                }

            //                //var deleteRecord = schoolDBEntity.StudentInformations.First(b => b.StudentID == DelID);
            //                //schoolDBEntity.StudentInformations.Remove(deleteRecord);                           
            //            }
            //        }                    
            //        dataGridViewShowShiftwiseData.AllowUserToAddRows = false;
            //        dataGridViewShowShiftwiseData.Columns.Clear();
            //        sdata.dataLoadDefault(dataGridViewShowShiftwiseData);
            //    }
            //    buttonEdit.Text = "Save";
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Please Check Your Data Fields!", ex.Message);
            //}

        }

        public async override void buttonEdit_Click(object sender, EventArgs e)
        {
            if (buttonEdit.Text == "Save")
            {
                tableStudentInformationsBindingSource.EndEdit();
               await schoolDBEntity.SaveChangesAsync();
                dataGridViewShowShiftwiseData.Refresh();
                SchoolManagement.ShowMessage("Selected Data Is Successfully Deleted!","Thank You!","Success");
                buttonEdit.Text = "Edit";
            }
        }
        public override void buttonShowData_Click(object sender, EventArgs e)
        {
            if (buttonShowData.Text == "Show")
            {
                sdata.dataLoadDefault(dataGridViewShowShiftwiseData, labelShowDataTablesDetails);
                buttonAdd.Visible = false;
                panelDataShowShift.Visible = true;
                panelDataShowShift.Show();
                panelDataShowShift.BringToFront();
                buttonShowData.Text = "Cancel";
                buttonEdit.Visible = true;
                buttonDelete.Visible = true;
                buttonAdd.Text = "Add";                
            }
            else
            {
                buttonAdd.Visible = true;
                buttonShowData.Text = "Show";              
                panelDayorMorningContainer.Visible = true;
                panelDayorMorningContainer.Show();
                panelDayorMorningContainer.BringToFront();
                buttonEdit.Visible = false;
                buttonDelete.Visible = false;
                SchoolManagement.Enable_Reset(panelShowDataShift, tableLayoutPanelShowDataMorning);
                SchoolManagement.Enable_Reset(panelShowDataDayShift, tableLayoutPanelShowDataDayShift);
                SchoolManagement.Enable_Reset(panelShowDataComboBoxCon, tableLayoutPanelShowDataComboboxCon);
                SchoolManagement.Enable_Reset(panelShowDataGender, tableLayoutPanelShowDataGender);
            }
        }
        public override void buttonSave_Click(object sender, EventArgs e)
        {
            if (buttonSave.Text == "Next")
            {
                refreshDistrictForPresentAddress(comboBoxDistrictForPresentAddress);
                refreshDistrictForPresentAddress(comboBoxDistrictForPermanentAddress);
                if (textBoxStufdentName.Text == "" || textBoxFatherName.Text == "" || textBoxMotherName.Text == "" || dateTimePickerStudents.Value == DateTime.Now || comboBoxGender.SelectedIndex == 0
                || textBoxNationality.Text == "" || comboBoxReligion.SelectedIndex == 0 || textBoxBirthReg.Text == "" || textBoxMobileFM.Text == "" || textBoxEmailId.Text == "" || textBoxStudentAge.Text == ""
                || comboBoxStudentsClass.SelectedIndex == 0 || textBox1RollNo.Text == "" || textBoxStudentSession.Text == "" || textBoxGuirdianName.Text == "" || textBoxGuirdianOccupation.Text == ""
                || comboBoxGuirdianGenderSel.SelectedIndex == 0 || textBoxGuirdianEmail.Text == "" || textBoxGuirdianRelation.Text == "" || dateTimePickerStudentAdmissionDate.Value == null)
                {
                    SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                }
                else
                {
                    try
                    {
                        studentShift = new StudentShiftInfo
                        {
                            ShiftName = studentShiftName.ToString(),
                        };

                        studentInfo = new StudentInformation
                        {
                            StudentName = textBoxStufdentName.Text.Trim(),
                            StudentFather_sName = textBoxFatherName.Text.Trim(),
                            StudentMother_sName = textBoxMotherName.Text.Trim(),
                            DateOfBirth = dateTimePickerStudents.Value.Date,
                            StudentAge = textBoxStudentAge.Text,
                            StudentStandar = comboBoxStudentsClass.Text,
                            SudentRollNo = textBox1RollNo.Text,
                            StudentBirthReg = textBoxBirthReg.Text,
                            StudentEmail = textBoxEmailId.Text,
                            SudentSession = textBoxStudentSession.Text,
                            StudentGender = comboBoxGender.Text,
                            StudentMobile = textBoxMobileNo.Text.Trim(),
                            StudentNationality = textBoxNationality.Text.Trim(),
                            StudentAdmissionDate = AdmissionDate,
                            StudentDurations = textBoxStudentDurations.Text.Trim(),
                            ShiftID = studentShift.ShiftID
                        };

                        guirdianInfo = new GuirdianInformation
                        {
                            GuirdianName = textBoxGuirdianName.Text.Trim(),
                            GuirdianGender = comboBoxGuirdianGenderSel.Text,
                            GuirdianOccupation = textBoxGuirdianOccupation.Text,
                            GuirdianRelation = textBoxGuirdianRelation.Text,
                            GuirdianEmail = textBoxGuirdianEmail.Text,
                            GuirdianMobile = textBoxMobileFM.Text,
                            StudentID = studentInfo.StudentID
                        };
                        schoolDBEntity.StudentShiftInfoes.Add(studentShift);
                        schoolDBEntity.StudentInformations.Add(studentInfo);
                        schoolDBEntity.GuirdianInformations.Add(guirdianInfo);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Try Again Carefully!", ex.Message);
                    }

                    if (panelpanelStudentAddressContainer.Visible == false)
                    {
                        buttonAdd.Visible = false;                        
                        panelpanelStudentAddressContainer.Visible = true;
                        panelpanelStudentAddressContainer.Show();
                        panelpanelStudentAddressContainer.BringToFront();
                        buttonSave.Text = "Save";                       
                    }
                }
            }
            else if (buttonSave.Text == "Save")
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    if (textBoxVillageForPresentAddress.Text == "" || textBoxRoadForPresentAddress.Text == "" || comboBoxDistrictForPresentAddress.SelectedIndex == 0 || comboBoxUpazillaForPresentAddress.SelectedIndex == 0
                        || textBoxPostOfficeForPresentAddress.Text == "" || textBoxPostCodeForPresentAddress.Text == "" || textBoxImagePathForStudent.Text == "")
                    {
                        SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                    }
                    else
                    {

                        try
                        {
                            studentPreInfo = new PresentAddress
                            {
                                PresentVllage = textBoxVillageForPresentAddress.Text,
                                PresentRoad = textBoxRoadForPresentAddress.Text,
                                PresentDistrict = comboBoxDistrictForPresentAddress.Text,
                                PresentUpazila = comboBoxUpazillaForPresentAddress.Text,
                                PresentPostOffice = textBoxPostOfficeForPresentAddress.Text,
                                PresentPostCode = textBoxPostCodeForPresentAddress.Text,
                                StudentID = studentInfo.StudentID
                            };

                            studentPerInfo = new PermanentAddress
                            {
                                PermanentVillage = textBoxVillageForPresentAddress.Text,
                                PermanentRoad = textBoxRoadForPresentAddress.Text,
                                PermanentDistrict = comboBoxDistrictForPresentAddress.Text,
                                PermanentUpazila = comboBoxUpazillaForPresentAddress.Text,
                                PermanentPostOffice = textBoxPostOfficeForPresentAddress.Text,
                                PermanentPostCode = textBoxPostCodeForPresentAddress.Text,
                                StudentID = studentInfo.StudentID
                            };

                            SaveImage(imagePath);
                            imagestu = new ImagesForAll
                            {
                                ImagePath = fileNameNew.Trim(),
                                StudentID = studentInfo.StudentID
                            };
                            schoolDBEntity.PresentAddresses.Add(studentPreInfo);
                            schoolDBEntity.PermanentAddresses.Add(studentPerInfo);
                            schoolDBEntity.ImagesForAlls.Add(imagestu);

                            data = schoolDBEntity.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Try Again Carefully!", ex.Message);
                        }
                        if (data > 0)
                        {
                            SchoolManagement.ShowMessage("Data Added Successfully!", "Thank You!", "Success");
                            panelStudentDataViewer.Show();
                            studentDataLoad(dataGridViewStudentDataViewer);
                            panelStudentDataViewer.BringToFront();
                            buttonEdit.Visible = true;
                            buttonDelete.Visible = true;
                            buttonShowData.Visible = true;
                            buttonSave.Visible = false;
                            buttonAdd.Visible = false;
                            SchoolManagement.Enable_Reset(panelMornigShiftButton, tableLayoutPanelMorningShiftButton);
                            SchoolManagement.Enable_Reset(panelDayShiftButton, tableLayoutPanelDayShiftButton);
                        }
                        else
                        {
                            SchoolManagement.ShowMessage("Data Added Failed!", "Try Again!", "Error!");
                        }
                    }
                }
                else
                {
                    if (textBoxVillageForPresentAddress.Text == "" || textBoxRoadForPresentAddress.Text == "" || comboBoxDistrictForPresentAddress.SelectedIndex == 0 || comboBoxUpazillaForPresentAddress.SelectedIndex == 0 || comboBoxDistrictForPermanentAddress.SelectedIndex == 0
                        || textBoxPostOfficeForPresentAddress.Text == "" || textBoxPostCodeForPresentAddress.Text == "" || textBoxImagePathForStudent.Text == "" || textBoxVilageForPermanentAddress.Text == "" || textBoxRoadForPermanentAddress.Text == "" || comboBoxUpazillaForPermanentAddress.SelectedIndex == 0
                        || textBoxPostOfficeForPermanentAddress.Text == "" || textBoxPostCodeForPermanentAddress.Text == "")
                    {
                        SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                    }
                    else
                    {
                        try
                        {
                            studentPreInfo = new PresentAddress
                            {
                                PresentVllage = textBoxVillageForPresentAddress.Text,
                                PresentRoad = textBoxRoadForPresentAddress.Text,
                                PresentDistrict = comboBoxDistrictForPresentAddress.Text,
                                PresentUpazila = comboBoxUpazillaForPresentAddress.Text,
                                PresentPostOffice = textBoxPostOfficeForPresentAddress.Text,
                                PresentPostCode = textBoxPostCodeForPresentAddress.Text,
                                StudentID = studentInfo.StudentID
                            };


                            studentPerInfo = new PermanentAddress
                            {
                                PermanentVillage = textBoxVillageForPresentAddress.Text,
                                PermanentRoad = textBoxRoadForPresentAddress.Text,
                                PermanentDistrict = comboBoxDistrictForPresentAddress.Text,
                                PermanentUpazila = comboBoxUpazillaForPresentAddress.Text,
                                PermanentPostOffice = textBoxPostOfficeForPresentAddress.Text,
                                PermanentPostCode = textBoxPostCodeForPresentAddress.Text,
                                StudentID = studentInfo.StudentID
                            };


                            SaveImage(imagePath);
                            imagestu = new ImagesForAll
                            {
                                ImagePath = fileNameNew.Trim(),
                                StudentID = studentInfo.StudentID
                            };

                            schoolDBEntity.PresentAddresses.Add(studentPreInfo);
                            schoolDBEntity.PermanentAddresses.Add(studentPerInfo);
                            schoolDBEntity.ImagesForAlls.Add(imagestu);
                            data = schoolDBEntity.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Try Again Carefully!", ex.Message);
                        }
                        if (data > 0)
                        {
                            SchoolManagement.ShowMessage("Data Added Successfully!", "Thank You!", "Success");
                            panelStudentDataViewer.Show();
                            studentDataLoad(dataGridViewStudentDataViewer);
                            panelStudentDataViewer.BringToFront();
                            buttonEdit.Visible = true;
                            buttonDelete.Visible = true;
                            buttonShowData.Visible = true;
                            buttonSave.Visible = false;
                            buttonAdd.Visible = false;
                            SchoolManagement.Enable_Reset(panelMornigShiftButton, tableLayoutPanelMorningShiftButton);
                            SchoolManagement.Enable_Reset(panelDayShiftButton, tableLayoutPanelDayShiftButton);
                        }
                        else
                        {
                            SchoolManagement.ShowMessage("Data Added Failed!", "Try Again!", "Error!");
                        }
                    }
                }

            }
            else
            {

            }
        }

        // Universal Button for this Form --------------

        

        //Student Form One Details---------------
        private void textBoxStufdentName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStufdentName.Text == "")
            {
                labelStudentErr.Visible = true;
            }
            else
            {
                labelStudentErr.Visible = false;
            }
        }

        private void textBoxFatherName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxFatherName.Text == "") { labelFatherErr.Visible = true; } else { labelFatherErr.Visible = false; }
        }

        private void textBoxMotherName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMotherName.Text == "") { labelMotherErr.Visible = true; } else { labelMotherErr.Visible = false; }
        }

        private void dateTimePickerStudents_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerStudents.Value == DateTime.Today) { labelBirthErr.Visible = true; } else { labelBirthErr.Visible = false; }
            try
            {
                SchoolManagement.CalculateAge(dateTimePickerStudents, textBoxStudentAge);
            }
            catch
            {
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    dateTimePickerStudents.MaxDate = DateTime.Today;
                }
                SchoolManagement.ShowMessage("You Are Trying For Invalid Date!", "Try Again!", "Error!");
            }
        }

        private void comboBoxGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGender.SelectedIndex <= 0) { labelGenderErr.Visible = true; } else { labelGenderErr.Visible = false; }
        }

        private void textBoxBirthReg_TextChanged(object sender, EventArgs e)
        {
            if (textBoxBirthReg.Text == "") { labelBirthRegErr.Visible = true; } else { labelBirthRegErr.Visible = false; }
        }

        private void comboBoxReligion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxReligion.SelectedIndex <= 0) { labelReligionErr.Visible = true; } else { labelReligionErr.Visible = false; }
        }

        private void textBoxEmailId_TextChanged(object sender, EventArgs e)
        {
            if (textBoxEmailId.Text == "") { labelEmailErr.Visible = true; } else { labelEmailErr.Visible = false; }
        }

        private void textBoxNationality_TextChanged(object sender, EventArgs e)
        {
            if (textBoxNationality.Text == "") { labelNationalityErr.Visible = true; } else { labelNationalityErr.Visible = false; }
        }

        private void comboBoxStudentsClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStudentsClass.SelectedIndex <= 0) { labelStdentClassErr.Visible = true; } else { labelStdentClassErr.Visible = false; }
        }

        private void textBox1RollNo_TextChanged(object sender, EventArgs e)
        {
            if (textBox1RollNo.Text == "") { labelRollNoErr.Visible = true; } else { labelRollNoErr.Visible = false; }
        }

        private void textBoxStudentAge_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStudentAge.Text == "")
            {
                labelAgeErr.Visible = true;
                textBoxStudentAge.Enabled = false;
            }
            else
            {
                labelAgeErr.Visible = false;
                textBoxStudentAge.Enabled = false;
            }

        }
        private void textBoxMobileFM_TextChanged(object sender, EventArgs e)
        {
            if (textBoxMobileFM.Text == "") { labelMobileErr.Visible = true; } else { labelMobileErr.Visible = false; }
        }

        private void textBoxStudentSession_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStudentSession.Text == "") { labelStudentsSessionErr.Visible = true; } else { labelStudentsSessionErr.Visible = false; }
        }

        private void textBoxGuirdianName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGuirdianName.Text == "") { labelGuirdianNameErr.Visible = true; } else { labelGuirdianNameErr.Visible = false; }
        }

        private void comboBoxGuirdianGenderSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxGuirdianGenderSel.SelectedIndex <= 0) { labelGuirdianGenderErr.Visible = true; } else { labelGuirdianGenderErr.Visible = false; }
        }

        private void textBoxGuirdianOccupation_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGuirdianOccupation.Text == "") { labelGuirdianOccupationErr.Visible = true; } else { labelGuirdianOccupationErr.Visible = false; }
        }

        private void textBoxGuirdianRelation_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGuirdianRelation.Text == "") { labelGuirdianRelationErr.Visible = true; } else { labelGuirdianRelationErr.Visible = false; }
        }

        private void textBoxGuirdianEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxGuirdianEmail.Text == "") { labelGuirdianEmailErr.Visible = true; } else { labelGuirdianEmailErr.Visible = false; }
        }

        private void dateTimePickerStudentAdmissionDate_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerStudentAdmissionDate.Value == DateTime.Today)
            {
                AdmissionDate = DateTime.Today;
                labelStudentAdmissionErr.Visible = true;
            }
            else
            {
                AdmissionDate = dateTimePickerStudentAdmissionDate.Value;
                labelStudentAdmissionErr.Visible = false;
            }

            try
            {
                SchoolManagement.StudentDurationCal(dateTimePickerStudentAdmissionDate, textBoxStudentDurations);
            }
            catch
            {
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    dateTimePickerStudents.MaxDate = DateTime.Today;
                }
                SchoolManagement.ShowMessage("You Are Trying For Invalid Date!", "Try Again!", "Error!");
            }
        }
        //Student Form One Details---------------

        //Student Form Two Details---------------

        private void textBoxVillageForPresentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxVillageForPresentAddress.Text == "") { labelVillageForPresentAddressError.Visible = true; } else { labelVillageForPresentAddressError.Visible = false; }
        }

        private void textBoxRoadForPresentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRoadForPresentAddress.Text == "") { labelRoadForPresentAddressError.Visible = true; } else { labelRoadForPresentAddressError.Visible = false; }
        }

        private void comboBoxDistrictForPresentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {                    
            
            if (comboBoxDistrictForPresentAddress.SelectedIndex <= 0)
            {                
                labelDistrictForPresentAddressError.Visible = true;
                comboBoxUpazillaForPresentAddress.Enabled = false;
            }
            else
            {
                refreshUpazilaForPresentAddress(comboBoxUpazillaForPresentAddress, comboBoxDistrictForPresentAddress);
                labelDistrictForPresentAddressError.Visible = false;
                comboBoxUpazillaForPresentAddress.Enabled = true;
                comboBoxUpazillaForPermanentAddress.Enabled = true;
            }
        }

        private void comboBoxUpazillaForPresentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUpazillaForPresentAddress.SelectedIndex <= 0)
            {
                labelUpazillaForPresentAddressError.Visible = true;
            }
            else
            {
                labelUpazillaForPresentAddressError.Visible = false;
            }
        }

        private void textBoxPostOfficeForPresentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPostOfficeForPresentAddress.Text == "") { labelPostOfficeForPresentAddressError.Visible = true; } else { labelPostOfficeForPresentAddressError.Visible = false; }
        }

        private void tableLayoutPanelRoadForPermanentAddress_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxPostCodeForPresentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPostCodeForPresentAddress.Text == "") { labelPostCodeForPresentAddressError.Visible = true; } else { labelPostCodeForPresentAddressError.Visible = false; }
        }

        private void textBoxVilageForPermanentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxVilageForPermanentAddress.Text == "")
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    textBoxVilageForPermanentAddress.Text = textBoxVillageForPresentAddress.Text;
                    textBoxVilageForPermanentAddress.Enabled = false;
                }
                else
                {
                    labelVillageForPermanentAddressError.Visible = true;
                    textBoxVilageForPermanentAddress.Enabled = true;
                }
            }
            else
            { labelVillageForPermanentAddressError.Visible = false; }
        }

        private void textBoxRoadForPermanentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxRoadForPermanentAddress.Text == "")
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    textBoxRoadForPermanentAddress.Text = textBoxRoadForPresentAddress.Text;
                    textBoxRoadForPermanentAddress.Enabled = false;
                }
                else
                {
                    labelRoadForPermanentAddressError.Visible = true;
                    textBoxRoadForPermanentAddress.Enabled = true;
                }
            }
            else
            { labelRoadForPermanentAddressError.Visible = false; }
        }

        private void comboBoxDistrictForPermanentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (comboBoxDistrictForPermanentAddress.SelectedIndex <= 0)
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    comboBoxDistrictForPermanentAddress.SelectedIndex = comboBoxDistrictForPresentAddress.SelectedIndex;
                    comboBoxDistrictForPermanentAddress.Enabled = false;
                }
                else
                {
                    labelDistrictForPermanentAddressError.Visible = true;
                    comboBoxDistrictForPermanentAddress.Enabled = true;

                }
                comboBoxUpazillaForPermanentAddress.Enabled = false;
            }
            else
            {
                refreshUpazilaForPresentAddress(comboBoxUpazillaForPermanentAddress, comboBoxDistrictForPermanentAddress);
                labelDistrictForPermanentAddressError.Visible = false;
                comboBoxUpazillaForPermanentAddress.Enabled = true;
            }
        }

        private void comboBoxUpazillaForPermanentAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxUpazillaForPermanentAddress.SelectedIndex <= 0)
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    comboBoxUpazillaForPermanentAddress.SelectedItem = comboBoxUpazillaForPresentAddress.SelectedItem;
                    comboBoxUpazillaForPermanentAddress.Enabled = false;
                }
                else
                {
                    labelUpazillaForPermanentAddressError.Visible = true;
                    comboBoxUpazillaForPermanentAddress.Enabled = true;
                }
            }
            else
            {
                labelUpazillaForPermanentAddressError.Visible = false;
            }
        }

        private void textBoxPostOfficeForPermanentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPostOfficeForPermanentAddress.Text == "")
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    textBoxPostOfficeForPermanentAddress.Text = textBoxPostOfficeForPresentAddress.Text;
                    textBoxPostOfficeForPermanentAddress.Enabled = false;
                }
                else
                {
                    labelPostOfficeForPermanentAddressError.Visible = true;
                    textBoxPostOfficeForPermanentAddress.Enabled = true;
                }
            }
            else
            { labelPostOfficeForPermanentAddressError.Visible = false; }
        }

        private void textBoxPostCodeForPermanentAddress_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPostCodeForPermanentAddress.Text == "")
            {
                if (checkBoxSameasPresentAddress.Checked)
                {
                    textBoxPostCodeForPermanentAddress.Text = textBoxPostCodeForPresentAddress.Text;
                    textBoxPostCodeForPermanentAddress.Enabled = false;
                }
                else
                {
                    labelPostCodeForPermanentAddressError.Visible = true;
                    textBoxPostCodeForPermanentAddress.Enabled = true;
                }
            }
            else
            {
                labelPostCodeForPermanentAddressError.Visible = false;
            }
        }

        private void checkBoxSameasPresentAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSameasPresentAddress.Checked)
            {
                textBoxVilageForPermanentAddress.Text = textBoxVillageForPresentAddress.Text;
                textBoxRoadForPermanentAddress.Text = textBoxRoadForPresentAddress.Text;
                comboBoxDistrictForPermanentAddress.SelectedIndex = comboBoxDistrictForPresentAddress.SelectedIndex;
                comboBoxUpazillaForPermanentAddress.SelectedItem = comboBoxUpazillaForPresentAddress.SelectedItem;
                textBoxPostOfficeForPermanentAddress.Text = textBoxPostOfficeForPresentAddress.Text;
                textBoxPostCodeForPermanentAddress.Text = textBoxPostCodeForPresentAddress.Text;
                textBoxVilageForPermanentAddress.Enabled = false;
                textBoxRoadForPermanentAddress.Enabled = false;
                comboBoxDistrictForPermanentAddress.Enabled = false;
                comboBoxUpazillaForPermanentAddress.Enabled = false;
                textBoxPostOfficeForPermanentAddress.Enabled = false;
                textBoxPostCodeForPermanentAddress.Enabled = false;


            }
            else
            {
                foreach (System.Windows.Forms.Control control in this.Controls)
                {
                    textBoxVilageForPermanentAddress.Text = null;
                    textBoxRoadForPermanentAddress.Text = null;
                    comboBoxDistrictForPermanentAddress.SelectedItem = 0;
                    comboBoxUpazillaForPermanentAddress.SelectedItem = 0;
                    textBoxPostOfficeForPermanentAddress.Text = null;
                    textBoxPostCodeForPermanentAddress.Text = null;
                }
                labelPostCodeForPermanentAddressError.Visible = true;
                labelPostOfficeForPermanentAddressError.Visible = true;
                labelUpazillaForPermanentAddressError.Visible = true;
                labelDistrictForPermanentAddressError.Visible = true;
                labelRoadForPermanentAddressError.Visible = true;
                labelVillageForPermanentAddressError.Visible = true;
                textBoxVilageForPermanentAddress.Enabled = true;
                textBoxRoadForPermanentAddress.Enabled = true;
                comboBoxDistrictForPermanentAddress.Enabled = true;
                comboBoxUpazillaForPermanentAddress.Enabled = false;
                textBoxPostOfficeForPermanentAddress.Enabled = true;
                textBoxPostCodeForPermanentAddress.Enabled = true;
            }
        }        
        
        private void buttonStudentImageBrowse_Click(object sender, EventArgs e)
        {            
            if (buttonStudentImageBrowse.Text == "Browse")
            {
                DialogResult dr = openFileDialogForImage.ShowDialog();
                if (dr == DialogResult.OK)
                {                                      
                    try
                    {
                        openFileDialogForImage.Filter = "(*.jpg;*.jpeg;*.png;*.bmp;*.*)|*.jpg;*.jpeg;*.png; *.bmp;*.*";
                        stuImage = new Bitmap(openFileDialogForImage.FileName);
                        pictureBoxStudents.Image = stuImage;                        
                        pictureBoxStudents.SizeMode = PictureBoxSizeMode.StretchImage;                        
                        imagePath = openFileDialogForImage.FileName;
                        textBoxImagePathForStudent.Text = imagePath;
                        textBoxImagePathForStudent.Enabled = false;                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please Select Correct File Format!", ex.Message);                      
                    }                    
                }
            }
            else
            {
                pictureBoxStudents.Image = Properties.Resources.DefaultImage;               
                textBoxImagePathForStudent.Clear();
            }            
        }
       
    private void textBoxImagePathForStudent_TextChanged(object sender, EventArgs e)
        {
            if (textBoxImagePathForStudent.Text == "")
            {
                textBoxImagePathForStudent.Visible = false;                              
                labelImagePath.Visible = false;
                buttonStudentImageBrowse.Text = "Browse";
                labelImagePathErr.Visible = true;
            }
            else
            {
                textBoxImagePathForStudent.Visible = true;
                labelImagePathErr.Enabled = false;
                labelImagePath.Visible = true;
                buttonStudentImageBrowse.Text = "Clear";
            }
        }

        private void labelImagePath_Click(object sender, EventArgs e)
        {
            if (labelImagePath.Visible == true)
            {
                labelImagePath.Visible = false;
            }
            else
            {
                labelImagePath.Visible = true;
            }
        }

        //Student Form Two Details---------------
        
       // DataGridView For Data Show------------------
        private void dataGridViewStudentDataViewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //con.Open();
            //SqlCommand cmd = new SqlCommand("select * from Table_Student_Informations", con);            
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //dataGridViewStudentDataViewer.DataSource = dt;
            //con.Close();
        }

        private void dataGridViewStudentDataViewer_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridViewStudentDataViewer.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridViewStudentDataViewer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
           
        }

        private void dataGridViewShowShiftwiseData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridViewShowShiftwiseData.Rows[e.RowIndex].Cells[2].Value = (e.RowIndex + 1).ToString();
        }

        // DataGridView For Data Show------------------

        // Morning or Day For Student Data Filled-------------
        private void checkBoxForMorning_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxForMorning.CheckState == CheckState.Checked)
            {
                studentShiftName = "Morning Shift";
                buttonAdd.Visible = true;
                checkBoxForDayShift.Enabled = false;
                buttonShowData.Visible = false;
            }
            else
            {
                studentShiftName = "Day Shift";
                buttonAdd.Visible = false;
                checkBoxForDayShift.Enabled = true;
                buttonShowData.Visible = true;
            }
        }

        private void checkBoxForDayShift_CheckedChanged(object sender, EventArgs e)
        {           
            if (checkBoxForDayShift.CheckState == CheckState.Checked)
            {
                studentShiftName = "Day Shift";
                buttonAdd.Visible = true;
                checkBoxForMorning.Enabled = false;
                buttonShowData.Visible = false;
            }
            else
            {
                studentShiftName = "Morning Shift";
                buttonAdd.Visible = false;
                checkBoxForMorning.Enabled = true;
                buttonShowData.Visible = true;
            }
        }

        // Morning or Day For Student Data Filled-------------

        // Show Morning/Day Data in Data Grid View----------------          

        private void checkBoxShowMorningData_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowMorningData.CheckState == CheckState.Checked)
            {
                studentShiftName = "Morning Shift";
                checkBoxShowDayData.Enabled = false;                
                if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                       
                }
                else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if(checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else
                {
                    sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                }               
            }           
            else
            {
                checkBoxShowDayData.Enabled = true;
                studentShiftName = "Day Shift";
                if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                    }
                       
                }
                else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, labelShowDataTablesDetails);
                    }
                        
                }
                else
                {
                    sdata.dataLoadDefault(dataGridViewShowShiftwiseData, labelShowDataTablesDetails);
                }               
            }
        }

        private void checkBoxShowDayData_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowDayData.CheckState == CheckState.Checked)
            {                
                studentShiftName = "Day Shift";
                checkBoxShowMorningData.Enabled = false;               
                if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                   
                }
                else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else
                {
                    sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                }
            }
            else
            {
                checkBoxShowMorningData.Enabled = true;                
                studentShiftName = "Morning Shift";
                if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, labelShowDataTablesDetails);
                    }
                }
                else
                {
                    sdata.dataLoadDefault(dataGridViewShowShiftwiseData, labelShowDataTablesDetails);
                }               
            }
        }

        private void comboBoxShowDataStandard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxShowDataStandard.SelectedIndex != 0)
            {
                if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                }
                else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }                        
                }
                else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }                    
                }
                else
                {
                    sdata.dataFilterUseComboBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, labelShowDataTablesDetails);
                }                   
            }
            else
            {
                if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                    }                   
                }
                else if (checkBoxShowDataMale.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
                {
                    if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                    }                    
                }
                else
                {
                    sdata.dataLoadDefault(dataGridViewShowShiftwiseData, labelShowDataTablesDetails);
                }
            }
            
        }

        private void checkBoxShowDataMale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowDataMale.CheckState == CheckState.Checked)
            {
                studentGender = "Male";
                checkBoxShowDataFemale.Enabled = false;                
                if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                    
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if(checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }
                }
                else
                {
                    sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                }
                   
            }
            else
            {
                checkBoxShowDataFemale.Enabled = true;
                studentGender = "Female";
                if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                {
                    if(comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, labelShowDataTablesDetails);
                    }                        
                }
                else
                {
                    sdata.dataLoadDefault(dataGridViewShowShiftwiseData, labelShowDataTablesDetails);
                }                
            }
        }

        private void checkBoxShowDataFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowDataFemale.CheckState == CheckState.Checked)
            {                
                studentGender = "Female";
                checkBoxShowDataMale.Enabled = false;               
                if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                   
                }
                else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShiftGender(dataGridViewShowShiftwiseData, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxCheckBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBoxGender(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentGender, labelShowDataTablesDetails);
                    }                   
                }
                else
                {
                    sdata.dataFilterUseCheckBoxGender(dataGridViewShowShiftwiseData, studentGender, labelShowDataTablesDetails);
                }
            }
            else
            {
                checkBoxShowDataMale.Enabled = true;
                studentGender = "Male";
                if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                {
                    if (comboBoxShowDataStandard.SelectedIndex != 0)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseCheckBoxShift(dataGridViewShowShiftwiseData, studentShiftName, labelShowDataTablesDetails);
                    }                    
                }
                else if (comboBoxShowDataStandard.SelectedIndex != 0)
                {
                    if (checkBoxShowMorningData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else if (checkBoxShowDayData.CheckState == CheckState.Checked)
                    {
                        sdata.dataFilterUseComboBoxShift(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, studentShiftName, labelShowDataTablesDetails);
                    }
                    else
                    {
                        sdata.dataFilterUseComboBox(dataGridViewShowShiftwiseData, comboBoxShowDataStandard, labelShowDataTablesDetails);
                    }                    
                }
                else
                {
                    sdata.dataLoadDefault(dataGridViewShowShiftwiseData, labelShowDataTablesDetails);
                }               
            }
        }

        private void dataGridViewShowShiftwiseData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (dataGridViewShowShiftwiseData.Columns[e.ColumnIndex].Name == "StudentImageGV")
            //{
            //    e.Value = Bitmap.FromFile(e.Value.ToString());
            //    e.FormattingApplied = true;
            //}
        }

        private void dataGridViewShowShiftwiseData_DoubleClick(object sender, EventArgs e)
        {
            //ShowStudentsImageFloat showImage = new ShowStudentsImageFloat();
            //string imagePath = dataGridViewShowShiftwiseData.CurrentRow.Cells["ImaePathGV"].Value.ToString();
            //Image img = Image.FromFile(imagePath);
            //showImage.pictureBoxStudentImageShow.Image = img;
            //if(showImage.Text == "")
            //{
            //    showImage.Text = dataGridViewShowShiftwiseData.CurrentRow.Cells["StudentNameGV"].Value.ToString();
            //}
            //else
            //{
            //    showImage.Text = "";
            //}
            //showImage.ShowDialog();

        }

        private void dataGridViewShowShiftwiseData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string text = "";

            try
            {
                ShowStudentsImageFloat showImage = new ShowStudentsImageFloat();
                if (e.ColumnIndex == dataGridViewShowShiftwiseData.Columns["StudentNameGV"].Index)
                {
                    string imageName = dataGridViewShowShiftwiseData.CurrentRow.Cells["ImaePathGV"].Value.ToString();                    
                    if (imageName != "")
                    {
                        string imagePath = System.IO.Path.Combine(folder, imageName);
                        Bitmap img = (Bitmap)Image.FromFile(imagePath);
                        text = dataGridViewShowShiftwiseData.CurrentRow.Cells["StudentNameGV"].Value.ToString();
                        showImage.pictureBoxStudentImageShow.Image = img;
                    }
                    else
                    {
                        SchoolManagement.ShowMessage("Image Not Available!!", "Try Again!", "Warning");
                    }
                    showImage.Text = text;
                    showImage.ShowDialog();
                }
                else
                {
                    SchoolManagement.ShowMessage("Please Choose Correct Column!", "Try Again!", "Warning");
                    showImage.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image not Found!", ex.Message);
            }
        }

        //private void dataGridViewShowShiftwiseData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    var senderGrid = (DataGridView)sender;
        //    if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
        //    {
        //        if (e.ColumnIndex == dataGridViewShowShiftwiseData.Columns["StudentNameGV"].Index)
        //        {
        //            var row = senderGrid.CurrentRow.Cells;
        //            string ID = Convert.ToString(row["columnId"].Value); //This is to fetch the id or any other info
        //            MessageBox.Show("ColumnName selected");
        //        }
        //    }
        //}

        // Show Morning/Day Data in Data Grid View----------------
    }
}
