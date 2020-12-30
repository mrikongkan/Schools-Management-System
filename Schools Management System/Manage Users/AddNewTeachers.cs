using Schools_Management_System.SchoolManagementClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schools_Management_System.Manage_Users
{
    public partial class AddNewTeachers : Form
    {
        public AddNewTeachers()
        {
            InitializeComponent();
            pictureBoxImageForTeacher.Image = Properties.Resources.DefaultImage;
        }

        private void iconButtonClose_Click(object sender, EventArgs e)
        {
            //SchoolManagement.OpenChildForms(new AddNewTeachers(), panelChildFormContainer);            
            DialogResult confirm = MessageBox.Show("Are You Sure To Close This Window?", "Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
               // this.Close();
                SchoolManagement.ShowWindow(new TeachersManagement(), this);
            }
            else
            {
               
            }
        }
    }
}
