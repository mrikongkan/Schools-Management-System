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

namespace Schools_Management_System.SchoolManagementForms.New_Windows_Form
{
    public partial class SchoolBoard : School
    {        
        public SchoolBoard()
        {
            InitializeComponent();
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }

        //Form activeform = null;
        //public void OpenAddSaveEditButton(Form childform)
        //{
        //    if (activeform != null)
        //    {
        //        activeform.Close();
        //    }
        //    activeform = childform;
        //    childform.TopLevel = false;
        //    childform.FormBorderStyle = FormBorderStyle.None;
        //    childform.Dock = DockStyle.Fill;
        //    panelAddSaveEditDelBut.Controls.Add(childform);
        //    panelAddSaveEditDelBut.Tag = childform;
        //    childform.BringToFront();

        //    childform.Show();
        //}

        private void SchoolBoard_Load(object sender, EventArgs e)
        {
           
        }
        public override void iconButtonLogOutButton_Click(object sender, EventArgs e)
        {
            SchoolManagement.ShowWindow(new SchoolManagementSetings(), this);           
        }
        public override void iconButtonLogOutIcon_Click(object sender, EventArgs e)
        {
            SchoolManagement.ShowWindow(new SchoolManagementSetings(), this);
        }
    }
}
