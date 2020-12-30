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
using System.Windows.Forms;

namespace Schools_Management_System.SchoolManagementForms.New_Windows_Form
{
    public partial class SchoolHome : MaterialSkin.Controls.MaterialForm
    {        
        public SchoolHome()
        {
            InitializeComponent();
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }
       
        private void SchoolHome_Load(object sender, EventArgs e)
        {            
            SchoolManagementSetings st = new SchoolManagementSetings();
            st.MdiParent = SchoolHome.ActiveForm;
            st.TopLevel = false;
            this.panelChildFormContainer.Controls.Add(st);
            st.FormBorderStyle = FormBorderStyle.None;
            st.Dock = DockStyle.Fill;
            st.BringToFront();
            this.panelChildFormContainer.Tag = st;
            st.TopMost = true;
            st.Show();
        }        
    }
}
