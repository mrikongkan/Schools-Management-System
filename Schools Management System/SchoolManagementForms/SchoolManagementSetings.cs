using Schools_Management_System.School_Data_Model;
using Schools_Management_System.SchoolManagementClass;
using Schools_Management_System.SchoolManagementForms.New_Windows_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Schools_Management_System.SchoolManagementForms
{
    public partial class SchoolManagementSetings : Form
    {        
        private string userRole;
        private UserProfile userData;
        private UserQuestion userQuestion;
        SchoolHome showParent = new SchoolHome();
        string randomCode;
        public static string to;
        string userName = to;

        string usenamever;
        string usergmailvar;
        public SchoolManagementSetings()
        {
            InitializeComponent();           
            LoadSaveDataLogInUser();
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }
        schoolmanagementsystemEntities schoolDBEntity = new schoolmanagementsystemEntities();
        public void loadQuestionsDetails()
        {
            comboBoxQuestion.Items.Add("--Select Your Security Question Here!--");
            comboBoxQuestion.SelectedIndex = 0;
            var questions = schoolDBEntity.SecurityQuestions.ToList();
            foreach (var question in questions)
            {
                comboBoxQuestion.Items.Add(question);
            }
            comboBoxQuestion.ValueMember = "QuestionID";
            comboBoxQuestion.DisplayMember = "QuestionDetails";
        }
        StringBuilder sb = new StringBuilder();

        private void checkBoxIntdScr_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxIntdScr.Checked)
            {
                textBoxUserName.Text = "";
                UserNameLB.Enabled = false;
                textBoxUserName.Enabled = false;
                textBoxPassword.Text = "";
                PasswordLB.Enabled = false;
                textBoxPassword.Enabled = false;

            }
            else
            {

                UserNameLB.Enabled = true;
                textBoxUserName.Enabled = true;
                PasswordLB.Enabled = true;
                textBoxPassword.Enabled = true;
            }
        }

        private void textBoxDataSource_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDataSource.Text == "")
            {
                DataSourceErr.Visible = true;
            }
            else
            {
                DataSourceErr.Visible = false;
            }
        }

        private void textBoxDatabase_TextChanged(object sender, EventArgs e)
        {
            if (textBoxDatabase.Text == "")
            {
                DatabaseErr.Visible = true;
            }
            else
            {
                DatabaseErr.Visible = false;
            }

        }

        private void textBoxUserName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUserName.Text == "")
            {
                UserNameErr.Visible = true;
            }
            else
            {
                UserNameErr.Visible = false;
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                PasswordError.Visible = true;
            }
            else
            {
                PasswordError.Visible = false;
            }
        }

        private void buttonDataBaseNext_Click(object sender, EventArgs e)
        {
            if (checkBoxIntdScr.Checked)
            {
                if (textBoxDataSource.Text == "" || textBoxDatabase.Text == " ")
                {
                    SchoolManagementClass.SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                }
                else
                {
                    sb.Append("Data Source = " + textBoxDataSource.Text + "; Initial Catalog = " + textBoxDatabase.Text + "; Integrated Security = true; MultipleActivesResultSets = true");
                    File.WriteAllText(SchoolManagement.path + "\\DBConnection.txt", sb.ToString());
                    panelDataBaseContainer.Visible = false;                    
                    panelUserLogIn.Visible = true;
                    if (panelUserLogIn.Visible == true)
                    {
                        iconButtoniconButtonUserRegistration.Visible = true;
                        iconButtonAlreadyRegistered.Visible = true;
                        panelUserLogIn.BringToFront();
                        this.AcceptButton = iconButtonRegistration;
                    }
                    else
                    {
                        iconButtoniconButtonUserRegistration.Visible = false;
                        iconButtonAlreadyRegistered.Visible = false;
                    }
                }
            }
            else
            {
                if (textBoxDataSource.Text == "" || textBoxDatabase.Text == " " || textBoxUserName.Text == "" || textBoxPassword.Text == "")
                {
                    SchoolManagementClass.SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                }
                else
                {
                    sb.Append("Data Source = " + textBoxDataSource.Text + "; Initial Catalog = " + textBoxDatabase.Text + "; User Name = " + textBoxUserName.Text + "; Password = " + textBoxPassword.Text + "; MultipleActivesResultSets = true");
                    File.WriteAllText(SchoolManagement.path + "\\DBConnection.txt", sb.ToString());
                    panelDataBaseContainer.Visible = false;
                    panelUserLogIn.Visible = true;                    
                    if (panelUserLogIn.Visible == true)
                    {
                        iconButtoniconButtonUserRegistration.Visible = true;
                        iconButtonAlreadyRegistered.Visible = true;
                        panelUserLogIn.BringToFront();
                        this.AcceptButton = iconButtonRegistration;
                    }
                    else
                    {
                        iconButtoniconButtonUserRegistration.Visible = false;
                        iconButtonAlreadyRegistered.Visible = false;
                    }
                }
            }
        }

        private void comboBoxQuestion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxQuestion.SelectedIndex <= 0) { SelectQuestions.Visible = true; } else { SelectQuestions.Visible = false; }
        }

        private void SchoolManagementSetings_Load(object sender, EventArgs e)
        {            
            dateTimePickerUserBirthDate.Value = DateTime.Today;
            checkBoxUserEditor.Enabled = true;
            checkBoxUserAdmin.Enabled = true;
            if (File.Exists(SchoolManagement.path + "\\DBConnection.txt"))
            {
                panelUserLogIn.Visible = false;
                if (panelUserLogIn.Visible == true)
                {
                    iconButtoniconButtonUserRegistration.Visible = true;
                    iconButtonAlreadyRegistered.Visible = true;
                    panelUserLogIn.BringToFront();
                    this.AcceptButton = iconButtonRegistration;
                }
                else
                {
                    iconButtoniconButtonUserRegistration.Visible = false;
                    iconButtonAlreadyRegistered.Visible = false;
                }                
                
                panelSecurityQuestionsMiddle.Visible = false;                
                panelDataBaseContainer.Visible = false;
                panelAdminLogInModdile.Visible = true;
                panelAdminLogInModdile.BringToFront();
                this.AcceptButton = iconButtonStudentLogIn;               
            }
            else
            {                
                panelDataBaseContainer.Visible = true;
                //if (panelDataBaseContainer.Visible == true)
                //{
                //    this.MaximumSize = new System.Drawing.Size(650, 650);
                //    showParent.Hide();
                //    showParent.WindowState = FormWindowState.Normal;
                //    showParent.MaximumSize = new System.Drawing.Size(650, 650);
                //}
                panelDataBaseContainer.BringToFront();
                panelSecurityQuestionsMiddle.Visible = false;                
                panelAdminLogInModdile.Visible = false;
                panelUserLogIn.Visible = false;
                this.AcceptButton = buttonDataBaseNext;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxQuestion.SelectedIndex <= 0 || textBoxAnswer.Text == "")
            {
                SchoolManagementClass.SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
            }
            else
            {
                string[] questions;
                questions = new string[11];
                questions[0] = "--Select Your Security Question Here!--";
                questions[1] = "What was the house number and street name you lived in as a child?";
                questions[2] = "What were the last four digits of your childhood telephone number?";
                questions[3] = "What primary school did you attend?";
                questions[4] = "In what town or city was your first full time job?";
                questions[5] = "In what town or city did you meet your spouse/partner?";
                questions[6] = "What is the middle name of your oldest child?";
                questions[7] = "What are the last five digits of your driver's licence number?";
                questions[8] = "What is your grandmother's (on your mother's side) maiden name?";
                questions[9] = "What is your spouse or partner's mother's maiden name?";
                questions[10] = "In what town or city did your mother and father meet?";
                
                int j;                
                int i = 0;
                for (j = 1; j <= questions.Length; j++)
                {
                    if (comboBoxQuestion.SelectedIndex == j)
                    {                        
                        do
                        {                            
                            if (labelMessageForSecurityQuestions.Text == "Please Input Two Question Answer!")
                            {
                                userQuestion = new UserQuestion
                                {
                                    QuestionOne = comboBoxQuestion.Text,
                                    AnswerOne = textBoxAnswer.Text,
                                    UserID = userData.UserId
                                };
                                schoolDBEntity.UserQuestions.Add(userQuestion);
                                labelMessageForSecurityQuestions.Text = "Please Choose Another Question!";
                                labelMessageForSecurityQuestions.ForeColor = Color.OrangeRed;
                                comboBoxQuestion.SelectedIndex = 0;
                                textBoxAnswer.Text = "";
                                StringBuilder sb = new StringBuilder();
                                sb.Append("Questions    " + j + ": " + questions[j] + "\nAnswer       " + j + ": " + textBoxAnswer.Text + "\n");
                                File.WriteAllText(SchoolManagement.path + "\\Question 1.txt", sb.ToString());
                                SchoolManagementClass.SchoolManagement.ShowMessage("Question " + j + " Saved Successfully!", "Thank You!", "Success");                            
                            }
                            else
                            {
                                if (comboBoxQuestion.Text == userQuestion.QuestionOne)
                                {
                                    SchoolManagementClass.SchoolManagement.ShowMessage("Question " + j + " Already Saved Successfully, Chose Another!", "Thank You!", "Error");
                                    comboBoxQuestion.SelectedIndex = 0;
                                    textBoxAnswer.Text = "";
                                    break;
                                }
                                else
                                {                                   

                                    var questionChange = schoolDBEntity.UserQuestions.FirstOrDefault(c => c.SecurityQuestionID == userQuestion.SecurityQuestionID);
                                    if (questionChange == null)
                                    {
                                        //questionChange.QuestionOne = userQuestion.QuestionOne;
                                        //questionChange.AnswerOne = userQuestion.AnswerOne;
                                         userQuestion.QuestionTwo = comboBoxQuestion.Text;
                                         userQuestion.AnswerTwo = textBoxAnswer.Text;
                                        schoolDBEntity.UserQuestions.Add(userQuestion);
                                    }
                                    else
                                    {
                                        schoolDBEntity.Entry(questionChange).CurrentValues.SetValues(userQuestion);
                                    }                                                                
                                    schoolDBEntity.SaveChanges();
                                    labelMessageForSecurityQuestions.Text = "Thank You Very Much!";
                                    labelMessageForSecurityQuestions.ForeColor = Color.GreenYellow;
                                    StringBuilder sb = new StringBuilder();
                                    sb.Append("Questions    " + j + ": " + questions[j] + "\nAnswer       " + j + ": " + textBoxAnswer.Text + "\n");
                                    File.WriteAllText(SchoolManagement.path + "\\Questions 2.txt", sb.ToString());
                                    SchoolManagementClass.SchoolManagement.ShowMessage("Question " + j + " Saved Successfully!", "Thank You!", "Success");
                                    comboBoxQuestion.SelectedIndex = 0;
                                    textBoxAnswer.Text = "";
                                    this.panelUserRegistrationMiddle.Controls.Add(panelAdminLogInModdile);
                                    panelSecurityQuestionsMiddle.Visible = false;
                                    //panelUserLogInButton.Visible = false;
                                    panelDataBaseContainer.Visible = false;
                                    panelAdminLogInModdile.Visible = true;
                                    iconButtoniconButtonUserRegistration.Hide();
                                    this.AcceptButton = iconButtonStudentLogIn;
                                }
                            }
                            i++;
                        }
                        while (i < 1);
                        break;
                    }                    
                }
            }
        }

        private void textBoxAnswer_TextChanged(object sender, EventArgs e)
        {
            if (textBoxAnswer.Text == "") { GiveAns.Visible = true; } else { GiveAns.Visible = false; }
        }

        private void iconButtonStudentLogIn_Click(object sender, EventArgs e)
        {
            if (textBoxLogInUserName.Text == "" || textBoxLogInPassword.Text == "")
            {
                SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");

            }
            else
            {
                try
                {
                    var query = from o in schoolDBEntity.UserProfiles
                                where o.UserName == textBoxLogInUserName.Text && o.UserPassword == textBoxLogInPassword.Text
                                select o;
                    //check if user exists
                    if (query.SingleOrDefault() != null)
                    {
                        SaveDataForActiveUser();
                        SchoolManagement.ShowMessage(" Welcome! You Have Successfully Loged In!", "Thank You!", "Success");
                        SchoolManagement.ShowWindow(new SchoolBoard(), this);
                    }
                    else
                    {
                        SchoolManagement.ShowMessage("Your username or password is incorrect.", "Try Again!", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }       

        private void buttonForgetPass_Click(object sender, EventArgs e)
        {           
            panelAdminLogInModdile.Visible = false;
            panelForgetPassword.Visible = true;
            panelForgetPassword.BringToFront();
            panelCodeVerificationContainer.Visible = true;
            if (panelUserSecurityQuestionsContainer.Visible == false)
            {
                panelUserSecurityQuestionsContainer.Visible = true;
                panelUserSecurityQuestionsContainer.Show();
                panelUserSecurityQuestionsContainer.BringToFront();
                textBoxUserNameForVerification.TabIndex = 0;
                textBoxEmailForVerification.TabIndex = 1;
                iconButtonEmailForVerificationSubmit.TabIndex = 2;
                this.AcceptButton = iconButtonEmailForVerificationSubmit;
            }                     
        }

        private void textBoxLogInUserName_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLogInUserName.Text == "") { LogInUserNameErr.Visible = true; } else { LogInUserNameErr.Visible = false; }
        }

        private void textBoxLogInPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxLogInPassword.Text == "") { labelLogInPasswordErr.Visible = true; } else { labelLogInPasswordErr.Visible = false; }
        }

        private void LogInUserNameErr_Click(object sender, EventArgs e)
        {

        }        

        // User Registration form....
        private void iconButtoniconButtonUserRegistration_Click(object sender, EventArgs e)
        {
            if (panelUserRegistrationForm.Visible == false)
            {
                panelUserRegistrationForm.Visible = true;
                iconButtonAlreadyRegistered.Visible = false;
                this.AcceptButton = iconButtonRegistration;
            }           
        }

        private void textBoxUserFullName_TextChanged(object sender, EventArgs e)
        {
            if(textBoxUserFullName.Text == "") { label1UserFullNameErr.Visible = true; } else { label1UserFullNameErr.Visible = false; }
        }

        private void textBoxUserNames_TextChanged(object sender, EventArgs e)
        {
            var checkUserName = schoolDBEntity.UserProfiles.Any(s => s.UserName.ToString().ToUpper() == textBoxUserNames.Text || s.UserName.ToString().ToLower()== textBoxUserNames.Text);
            if (textBoxUserNames.Text == "")
            { 
                labelUserNameErr.Visible = true; 
            } 
            else
            {
                if (checkUserName)
                {
                    labelUserNameErr.Visible = false;
                    labeltextBoxUserNamesWarning.Text = "Username Already Exits!";
                    textBoxUserNames.Text = "";
                    labeltextBoxUserNamesWarning.Visible = true;
                }
                else
                {
                    labeltextBoxUserNamesWarning.Visible = false;
                }
            }
        }

        private void textBoxuserPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxuserPassword.Text == "") { labelUserPasswordErr.Visible = true; } else { labelUserPasswordErr.Visible = false; }
        }

        private void textBoxUserConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUserConfirmPassword.Text != textBoxuserPassword.Text || textBoxUserConfirmPassword.Text == "") { labelUserConfirmPasswordErr.Visible = true; } else { labelUserConfirmPasswordErr.Visible = false; }
        }

        private void textBoxUserEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUserEmail.Text == "") { labelUserEmailErr.Visible = true; } else { labelUserEmailErr.Visible = false; }
        }

        private void textBoxuserPhoneNo_TextChanged(object sender, EventArgs e)
        {
            if (textBoxuserPhoneNo.Text == "") { labelUserPhoneNoErr.Visible = true; } else { labelUserPhoneNoErr.Visible = false; }
        }

        private void textBoxUserDesignation_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUserDesignation.Text == "") { labelUserDesignationErr.Visible = true; } else { labelUserDesignationErr.Visible = false; }
        }

        private void dateTimePickerUserBirthDate_ValueChanged(object sender, EventArgs e)
        {            
            if (dateTimePickerUserBirthDate.Value == DateTime.Today)
            {
                labelUserBitrhDateErr.Visible = true;              
            } 
            else
            {
                labelUserBitrhDateErr.Visible = false;
            }           
        }

        private void iconButtonRegistration_Click(object sender, EventArgs e)
        {
            loadQuestionsDetails();
            if (checkBoxUserAdmin.Checked == true)
            {                
                if (textBoxUserFullName.Text == "" || textBoxUserNames.Text == "" || textBoxuserPassword.Text == "" || textBoxUserConfirmPassword.Text != textBoxuserPassword.Text || textBoxUserEmail.Text == "" || textBoxuserPhoneNo.Text == "" || textBoxUserDesignation.Text == "" || dateTimePickerUserBirthDate.Value == DateTime.Today || checkBoxUserAdmin.Checked == false)
                {
                    resetForm();
                    SchoolManagementClass.SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                }
                else
                {                    
                    userData = new UserProfile()
                    {
                        UserFullName = textBoxUserFullName.Text.Trim(),
                        UserName = textBoxUserNames.Text.ToLower(),
                        UserPassword = textBoxuserPassword.Text.Trim(),
                        UserGmail = textBoxUserEmail.Text.Trim(),
                        UserPhoneNo = textBoxuserPhoneNo.Text.Trim(),
                        UserDesignation = textBoxUserDesignation.Text.Trim(),
                        UserBirthDate = dateTimePickerUserBirthDate.Value.Date,
                        UserRole = userRole.ToString(),
                        UserRegDate = DateTime.Today
                    };
                    schoolDBEntity.UserProfiles.Add(userData);
                    schoolDBEntity.SaveChanges();                    
                    this.panelUserRegistrationMiddle.Controls.Add(panelSecurityQuestionsMiddle);
                    panelSecurityQuestionsMiddle.Visible = true;
                    panelSecurityQuestionsMiddle.BringToFront();
                    this.AcceptButton = buttonSave;
                    panelUserRegistrationForm.Visible = false;
                    resetForm();
                }
            }
            else
            {
               if (textBoxUserFullName.Text == "" || textBoxUserNames.Text == "" || textBoxuserPassword.Text == "" || textBoxUserConfirmPassword.Text != textBoxuserPassword.Text || textBoxUserEmail.Text == "" || textBoxuserPhoneNo.Text == "" || textBoxUserDesignation.Text == "" || dateTimePickerUserBirthDate.Value == DateTime.Today || checkBoxUserEditor.Checked == false)
                {
                    resetForm();
                    SchoolManagementClass.SchoolManagement.ShowMessage("The * Fields Are Mendaory!", "Try Again!", "Error");
                }
                else
                {                    
                    userData = new UserProfile()
                    {
                        UserFullName = textBoxUserFullName.Text.Trim(),
                        UserName = textBoxUserNames.Text,
                        UserPassword = textBoxuserPassword.Text.Trim(),
                        UserGmail = textBoxUserEmail.Text.Trim(),
                        UserPhoneNo = textBoxuserPhoneNo.Text.Trim(),
                        UserDesignation = textBoxUserDesignation.Text.Trim(),
                        UserBirthDate = dateTimePickerUserBirthDate.Value.Date,
                        UserRole = userRole.ToString()
                    };                   
                    schoolDBEntity.UserProfiles.Add(userData);
                    schoolDBEntity.SaveChanges();
                    this.panelUserRegistrationMiddle.Controls.Add(panelSecurityQuestionsMiddle);
                    panelSecurityQuestionsMiddle.Visible = true;
                    panelSecurityQuestionsMiddle.BringToFront();
                    this.AcceptButton = buttonSave;
                    panelUserRegistrationForm.Visible = false;
                    resetForm();
                }
            }
        }

        private void iconButtonAlreadyRegistered_Click(object sender, EventArgs e)
        {
            if(panelAdminLogInModdile.Visible == false)
            {
                panelAdminLogInModdile.Visible = true;
                this.panelUserRegistrationMiddle.Controls.Add(panelAdminLogInModdile);
                this.AcceptButton = iconButtonStudentLogIn;
                panelUserRegistrationForm.Visible = false;
            }
        }

        private void resetForm()
        {
            checkBoxUserAdmin.Checked = false;
            checkBoxUserAdmin.Enabled = true;
            checkBoxUserEditor.Enabled = true;
            checkBoxUserEditor.Checked = false;
            textBoxUserFullName.Text = "";
            textBoxUserNames.Text = "";
            textBoxuserPassword.Text = "";
            textBoxUserConfirmPassword.Text = "";
            textBoxUserEmail.Text = "";
            textBoxuserPhoneNo.Text = "";
            textBoxUserDesignation.Text = "";
            dateTimePickerUserBirthDate.Value = DateTime.Today;
        }
        private void iconButtonCancel_Click(object sender, EventArgs e)
        {
            resetForm();
            panelUserLogIn.Visible = true;
            iconButtoniconButtonUserRegistration.Visible = true;
            iconButtonAlreadyRegistered.Visible = true;
            panelUserRegistrationForm.Visible = false;
        }

        private void buttonNewSignUp_Click(object sender, EventArgs e)
        {
            panelAdminLogInModdile.Visible = false;
            panelUserLogIn.Visible = true;
            if (panelUserLogIn.Visible == true)
            {
                iconButtoniconButtonUserRegistration.Visible = true;
                iconButtonAlreadyRegistered.Visible = true;
                panelUserLogIn.BringToFront();
                this.AcceptButton = iconButtonRegistration;
            }
            else
            {
                iconButtoniconButtonUserRegistration.Visible = false;
                iconButtonAlreadyRegistered.Visible = false;
            }
        }

        private void checkBoxUserAdmin_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxUserAdmin.CheckState == CheckState.Checked)
            {
                checkBoxUserEditor.Enabled = false;
                userRole = "Admin";
                labelUserRoleSelect.Visible = false;
            }
            else
            {
                checkBoxUserEditor.Enabled = true;
                userRole = "Editor";
                labelUserRoleSelect.Visible = true;
            }
        }

        private void checkBoxUserEditor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUserEditor.CheckState == CheckState.Checked)
            {
                userRole = "Editor";               
                checkBoxUserAdmin.Enabled = false;
                labelUserRoleSelect.Visible = false;
                
            }
            else
            {
                userRole = "Admin";
                checkBoxUserAdmin.Enabled = true;
                labelUserRoleSelect.Visible = true;
            }           
        }

        private void iconButtonPasswordShow_Click(object sender, EventArgs e)
        {
            if(this.iconButtonPasswordShow.IconChar == FontAwesome.Sharp.IconChar.EyeSlash)
            {                
                this.iconButtonPasswordShow.IconChar = FontAwesome.Sharp.IconChar.Eye;
                textBoxLogInPassword.UseSystemPasswordChar = false;
            }
            else
            {                
                this.iconButtonPasswordShow.IconChar = FontAwesome.Sharp.IconChar.EyeSlash;
                textBoxLogInPassword.UseSystemPasswordChar = true;
            }
        }
        private void SaveDataForActiveUser()
        {
            if (checkBoxRememberMe.Checked == true)
            {
                Properties.Settings.Default.UserName = textBoxLogInUserName.Text;
                Properties.Settings.Default.Password = textBoxLogInPassword.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
            }
        }
        private void LoadSaveDataLogInUser()
        {
            if (Properties.Settings.Default.UserName != string.Empty)
            {
                textBoxLogInUserName.Text = Properties.Settings.Default.UserName;
                textBoxLogInPassword.Text = Properties.Settings.Default.Password;
                checkBoxRememberMe.Checked = true;
            }           
        }

        // Forget Password Setting Part
        private void textBoxForgetPasswordEmail_TextChanged(object sender, EventArgs e)
        {
            if (textBoxForgetPasswordEmail.Text == "")
            {
                labellabelForgetPasswordErr.Visible = true;
            }
            else
            {
                labellabelForgetPasswordErr.Visible = false;
            }
        }

        private void textBoxVerifySecurityCode_TextChanged(object sender, EventArgs e)
        {
            if (textBoxVerifySecurityCode.Text == "")
            {
                labelSendCodeInputErr.Visible = true;
            }
            else
            {
                labelSendCodeInputErr.Visible = false;
            }
        }

        private void iconButtonSendConde_Click(object sender, EventArgs e)
        {
            if (textBoxForgetPasswordEmail.Text == "")
            {
                SchoolManagement.ShowMessage("Please Input Your Valid Email Here!","Try Again!","Warning");
            }
            else
            {
                string fromHere, passwordHere, messageBody;
                Random randomCodeFor = new Random();
                randomCode = (randomCodeFor.Next(999999)).ToString();
                MailMessage mailForMessage = new MailMessage();
                to = (textBoxForgetPasswordEmail.Text).ToString();
                fromHere = "mr.kongkan.cseiu@gmail.com";
                passwordHere = "";
                messageBody = "Dear, Your Password Reset Code is :" + " " + randomCode;
                mailForMessage.To.Add(to);
                mailForMessage.From = new MailAddress(fromHere);
                mailForMessage.Body = messageBody;
                mailForMessage.Subject = "Password Reset Code";
                SmtpClient passwordClient = new SmtpClient("smtp.gmail.com");
                passwordClient.EnableSsl = true;
                passwordClient.Port = 587;
                passwordClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                passwordClient.Credentials = new NetworkCredential(fromHere,passwordHere);
                try
                {
                    passwordClient.Send(mailForMessage);
                    textBoxForgetPasswordEmail.Text = "";
                    SchoolManagement.ShowMessage("Your Code Send Successfully!","Thank You","Success");
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Something Wrong, Please Try Again!", ex.Message);
                }

            }
        }

        private void iconButtonSecurityCodeVerify_Click(object sender, EventArgs e)
        {
            if(textBoxVerifySecurityCode.Text == "")
            {
                SchoolManagement.ShowMessage("Please Input Your Verfication Code Here!", "Try Again!", "Warning");
            }
            else
            {
                if(randomCode == textBoxVerifySecurityCode.Text)
                {
                    to = textBoxForgetPasswordEmail.Text;
                    panelCodeVerificationContainer.Visible = false;
                    panelCodeVerificationContainer.Hide();
                    panelResetPasswordContainer.Visible = true;
                    panelResetPasswordContainer.Show();
                    panelResetPasswordContainer.BringToFront();
                    textBoxVerifySecurityCode.Text = "";
                }
                else
                {
                    textBoxVerifySecurityCode.Text = "";
                    SchoolManagement.ShowMessage("Please Input Your Valid Code Here!", "Try Again!", "Warning");
                }
            }
        }

        private void textBoxPasswordConfirm_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPasswordConfirm.Text == "")
            {
                labelNewPassWordInputErr.Visible = true;
            }
            else
            {
                labelNewPassWordInputErr.Visible = false;
            }
        }

        private void textBoxPasswordNew_TextChanged(object sender, EventArgs e)
        {
            if(textBoxPasswordNew.Text == "" || textBoxPasswordConfirm.Text != textBoxPasswordNew.Text)
            {
                labelConfirmPasswordInputErr.Visible = true;
            }
            else
            {
                labelConfirmPasswordInputErr.Visible = false;
            }
        }
       

        private void textBoxSecurityQuestionOneAnswer_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSecurityQuestionOneAnswer.Text == "")
            {
                labelSecurityQuestionOneErr.Visible = true;
            }
            else
            {
                labelSecurityQuestionOneErr.Visible = false;
            }
        }

        private void textBoxSecurityQuestionTwoAnswer_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSecurityQuestionTwoAnswer.Text == "")
            {
                labelSecurityQuestionsTwoErr.Visible = true;
            }
            else
            {
                labelSecurityQuestionsTwoErr.Visible = false;
            }
        }        

        private void textBoxUserNameForVerification_TextChanged(object sender, EventArgs e)
        {
            if(textBoxUserNameForVerification.Text == "")
            {
                labelUserNameForVerificationErr.Visible = true;
            }
            else
            {
                labelUserNameForVerificationErr.Visible = false;
            }
        }

        private void textBoxEmailForVerification_TextChanged(object sender, EventArgs e)
        {
            if(textBoxEmailForVerification.Text == "")
            {
                labelEmailForVerificationErr.Visible = true;
            }
            else
            {
                labelEmailForVerificationErr.Visible = false;
            }
        }

        private void iconButtonEmailForVerificationSubmit_Click(object sender, EventArgs e)
        {
            if (textBoxUserNameForVerification.Text == "" || textBoxEmailForVerification.Text == "")
            {
                SchoolManagement.ShowMessage("Please Input Your Valid Information Here!", "Try Again!", "Warning");
            }
            else
            {
                try
                {
                    usenamever = textBoxUserNameForVerification.Text;
                    usergmailvar = textBoxEmailForVerification.Text;
                    var query = from o in schoolDBEntity.UserProfiles
                                where o.UserName == usenamever && o.UserGmail == usergmailvar
                                select o;
                    //check if user exists
                    if (query.SingleOrDefault() != null)
                    {                        

                        var securityquestionload = schoolDBEntity.UserQuestions.Where(s => s.UserProfile.UserName == usenamever && s.UserProfile.UserGmail == usergmailvar)
                            .Select(s => new
                            {
                                    s.QuestionOne,
                                    s.QuestionTwo
                            }).ToList();

                        if (null != securityquestionload)
                        {
                            foreach (var question in securityquestionload)
                            {
                                labelQuestionsOneDetails.Text = question.QuestionOne;
                                labelQuestionsTwoDetails.Text = question.QuestionTwo;
                            }
                            
                            
                        }

                        if (labelQuestionsOneDetails.Text != "" && labelQuestionsTwoDetails.Text != "")
                        {
                            
                            panelUserSecurityQuestionsContainer.Visible = false;
                            panelUserSecurityQuestionsContainer.Hide();
                            panelCodeVerificationContainer.Visible = false;
                            panelSecurityQuestionsCheck.Visible = true;
                            panelSecurityQuestionsCheck.Show();
                            panelSecurityQuestionsCheck.BringToFront();
                            this.AcceptButton = iconButtonEmailForVerificationSubmit;
                            textBoxUserNameForVerification.Text = "";
                            textBoxEmailForVerification.Text = "";
                        }
                        else
                        {
                            SchoolManagement.ShowMessage(" You Are not Allow For This System! ", "Try Again!", "Warning");
                        }
                    }
                    else
                    {
                        SchoolManagement.ShowMessage("Your username or Email is incorrect.", "Try Again!", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void iconButtonSecurityQuestionSubmit_Click(object sender, EventArgs e)
        {
            if(textBoxSecurityQuestionOneAnswer.Text == "" || textBoxSecurityQuestionTwoAnswer.Text == "")
            {
                SchoolManagement.ShowMessage("Please Answer Both Question Correctly!", "Try Again!", "Error");
            }
            else
            {
                try
                {
                    var AnswerOneQues = textBoxSecurityQuestionOneAnswer.Text;
                    var AnswerTwoQues = textBoxSecurityQuestionTwoAnswer.Text;
                    var query = from o in schoolDBEntity.UserQuestions
                                where o.AnswerOne == AnswerOneQues && o.AnswerTwo == AnswerTwoQues
                                select o;
                    //check if user exists
                    if (query.SingleOrDefault() != null)
                    {
                        panelResetPasswordContainer.Visible = true;
                        panelResetPasswordContainer.Show();
                        panelResetPasswordContainer.BringToFront();
                        panelSecurityQuestionsCheck.Visible = false;
                        panelSecurityQuestionsCheck.Hide();
                        this.AcceptButton = iconButtonPasswordReset;
                    }
                    else
                    {
                        SchoolManagement.ShowMessage("Please Answer The Question Correctly!", "Try Again!", "Error");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }   
        }

        private void SchoolManagementSetings_KeyDown(object sender, KeyEventArgs e)
        {
            Control ctl;
            ctl = (Control)sender;
            if (e.KeyCode == Keys.Tab)
            {
                ctl.SelectNextControl(ActiveControl, true, true, true, true);
            }
            else if (e.KeyCode == Keys.Up)
            {
                ctl.SelectNextControl(ActiveControl, false, true, true, true);
            }
        }

        private void iconButtonPasswordReset_Click(object sender, EventArgs e)
        {
            if (textBoxPasswordConfirm.Text == "")
            {
                SchoolManagement.ShowMessage("Please Input Your New Password Here!", "Try Again!", "Warning");
            }
            else if (textBoxPasswordNew.Text == "")
            {
                SchoolManagement.ShowMessage("Please Input Your  Confirm Password Here!", "Try Again!", "Warning");
            }
            else if (textBoxPasswordConfirm.Text == "" && textBoxPasswordNew.Text == "")
            {
                SchoolManagement.ShowMessage("Please Input Your New Password & Confirm Password Here!", "Try Again!", "Warning");
            }
            else if (textBoxPasswordConfirm.Text != textBoxPasswordNew.Text)
            {
                SchoolManagement.ShowMessage("Your Password is Not Matched!", "Try Again!", "Warning");
            }
            else
            {
                var UserPasswordChange = schoolDBEntity.UserProfiles.SingleOrDefault(s=>s.UserName == usenamever && s.UserGmail == usergmailvar);                                      

                if (UserPasswordChange != null)
                {
                    UserPasswordChange.UserPassword = textBoxPasswordConfirm.Text;                   
                   
                }            
                schoolDBEntity.SaveChanges();
                SchoolManagement.ShowMessage("Your Password Reset Successfully", "Thank You!", "Success");
                panelResetPasswordContainer.Visible = false;
                panelResetPasswordContainer.Hide();
                panelForgetPassword.Visible = false;
                panelForgetPassword.Hide();
                panelAdminLogInModdile.Visible = true;
                panelAdminLogInModdile.Show();
                panelAdminLogInModdile.BringToFront();
                //panelResetPasswordContainer.Controls.Clear();
            }
        }
    }    
}