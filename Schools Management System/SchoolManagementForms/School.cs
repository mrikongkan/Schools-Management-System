using Schools_Management_System.School_Board;
using Schools_Management_System.SchoolManagementClass;
using Schools_Management_System.SchoolManagementForms;
using Schools_Management_System.SchoolManagementForms.New_Windows_Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Drawing.Drawing2D;

namespace Schools_Management_System
{    
    public partial class School : Form
    {       
        private IconButton currentButton;
        private Panel leftBorderButton;
        
        public School()
        {
            InitializeComponent();
            SchoolDashboardDesign();
            leftBorderButton = new Panel();
            leftBorderButton.Size = new Size(5, 45);
            panelSubMenuWrapper.Controls.Add(leftBorderButton);
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }

        }

        private struct RGBColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249,118,176);
            public static Color color3 = Color.FromArgb(253,138,114);
            public static Color color4 = Color.FromArgb(95,77,221);
            public static Color color5 = Color.FromArgb(249,88,155);
            public static Color color6 = Color.FromArgb(24,161,251);
        }
        private void ActivateSenderBtn(Object senderButton, Color color)
        {
            if(senderButton != null)
            {
                DisabledButton();
                currentButton = (IconButton)senderButton;
                currentButton.BackColor = Color.FromArgb(37,36,81);
                currentButton.ForeColor = color;
                currentButton.TextAlign = ContentAlignment.MiddleCenter;
                currentButton.IconColor = color;
                currentButton.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentButton.ImageAlign = ContentAlignment.MiddleRight;
                leftBorderButton.BackColor = color;
                leftBorderButton.Location = new Point(0,currentButton.Location.Y);
                leftBorderButton.Visible = true;
                leftBorderButton.BringToFront();
            }
        }

        private void DisabledButton()
        {
            if(currentButton != null)
            {
                currentButton.BackColor = Color.FromArgb(64, 64, 64);
                currentButton.ForeColor = Color.Gainsboro;
                currentButton.TextAlign = ContentAlignment.MiddleLeft;
                currentButton.IconColor = Color.Gainsboro;
                currentButton.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentButton.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void SchoolDashboardDesign()
        {
            panelAcademicClasses.Visible = false;
            panelmanagestdents.Visible = false;
            panelManagesUsers.Visible = false;           
        }

        private void HideSubMenu()
        {
            if (panelAcademicClasses.Visible == true)
                panelAcademicClasses.Visible = false;
            else
                panelAcademicClasses.Visible = false;
            if (panelmanagestdents.Visible == true)
                panelmanagestdents.Visible = false;
            else
                panelmanagestdents.Visible = false;
            if (panelManagesUsers.Visible == true)
                panelManagesUsers.Visible = false;
            else
                panelManagesUsers.Visible = false;
            if (panelSetting.Visible == true)
                panelSetting.Visible = false;
            else
                panelSetting.Visible = false;
        }

        private void ShowSubMenu(Panel subMenu)
        {
            if(subMenu.Visible == false)
            {
                HideSubMenu();
                subMenu.Visible = true;
            }
            else
            {
                subMenu.Visible = false;
            }
        }
           public Form F;

        
        private void buttonAdminHome_Click(object sender, EventArgs e)
        {
            SchoolManagement.OpenChildForms(new AdminHome(), panelChildFormContainer);            
            ActivateSenderBtn(sender, RGBColors.color6);
        }

        private void iconButtonManageStudent_Click(object sender, EventArgs e)
        {           
            ActivateSenderBtn(sender,RGBColors.color1);            
            ShowSubMenu(panelmanagestdents);
        }

        private void iconButtonManagesUsers_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color2);
            ShowSubMenu(panelManagesUsers);
        }        

        private void buttonAdmitStudents_Click(object sender, EventArgs e)
        {
            SchoolManagement.OpenChildForms(new ManageStudentsMain(), panelChildFormContainer);            
            HideSubMenu();            
        }
        private void buttonStudentInformations_Click(object sender, EventArgs e)
        {
            //SchoolManagement.ShowWindow(new StudentInformations(),this);
            SchoolManagement.OpenChildForms(new StudentInformations(), panelChildFormContainer);
            HideSubMenu();
        }        

        private void buttonStudentFees_Click(object sender, EventArgs e)
        {
           SchoolManagement.OpenChildForms(new StudentMonthlyFess(), panelChildFormContainer);
           HideSubMenu();
        }
        private void buttonbuttonStudentsFacility_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            SchoolManagement.OpenChildForms(new StudentFacility(), panelChildFormContainer);
        }
        private void buttonTeachers_Click(object sender, EventArgs e)
        {
            HideSubMenu();
            SchoolManagement.OpenChildForms(new Manage_Users.TeachersManagement(), panelChildFormContainer);
        }

        private void buttonParents_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void buttonStaff_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void buttonAccountant_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void buttonOthers_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }


        private void buttonStudyMaterials_Click_1(object sender, EventArgs e)
        {
            HideSubMenu();
        }
      
        private void iconButtonHide_Click(object sender, EventArgs e)
        {
            Reset();

            if (panelSideMenu.Width == 250)
            {
                panelSideMenu.Width = 60;
                panelIconFor.Visible = true;
                panelSubMenuWrapper.Width = 58;                
            }
            else
            {
                panelIconFor.Visible = false;
                panelSideMenu.Width = 250;
                panelSubMenuWrapper.Width = 246;
            }
        }

        private void Reset()
        {
            DisabledButton();
            leftBorderButton.Visible = false;

        }

        private void iconButtonAcademicandClasses_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color3);
            ShowSubMenu(panelAcademicClasses);
        }

        private void buttonClasses_Click_1(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void buttonSections_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void buttonSyllabus_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void buttonStudyMaterials_Click(object sender, EventArgs e)
        {
            HideSubMenu();
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            if (panelIconFor.Width == 60)
            {
                panelIconFor.Visible = false;
                panelSideMenu.Width = 250;
                panelSubMenuWrapper.Width = 246;                
            }
            else
            {
                panelSideMenu.Width = 60;
                panelIconFor.Visible = true;
                panelSubMenuWrapper.Width = 58;
            }
        }     
        private void iconButtonExam_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color4);
        }

        private void iconButtonSubjects_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color5);
        }

        private void iconButtonClassRoutine_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color6);
        }

        private void iconButtonDailyAttendance_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color3);
        }

        private void iconButtonSettings_Click(object sender, EventArgs e)
        {
            ActivateSenderBtn(sender, RGBColors.color4);
            ShowSubMenu(panelSetting);
        }
        DateTime timenow;
        private void timerDateTimeSet_Tick(object sender, EventArgs e)
        {
           timenow = DateTime.Now;
           labelDateTime.Text = timenow.ToString();
           //labelDateTimeShow.Text = timenow.ToString();
        }
     
        private void labelDateTimeShow_Paint(object sender, PaintEventArgs e)
        {       
            Font textFont = new Font("Tahoma", 11,FontStyle.Bold);
            Brush textBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);            
            e.Graphics.TranslateTransform(10,200);
            e.Graphics.RotateTransform(-90);
            e.Graphics.DrawString(DateTime.Now.ToString(), textFont,textBrush,0,0);
        }

        private void buttonShowData_Click(object sender, EventArgs e)
        {
            SchoolManagement.OpenChildForms(new StudnetsSMSSends(), panelChildFormContainer);
            HideSubMenu();
        }

        public virtual void iconButtonLogOutButton_Click(object sender, EventArgs e)
        {  
        }

        public virtual void iconButtonLogOutIcon_Click(object sender, EventArgs e)
        {

        }        
    }
}
