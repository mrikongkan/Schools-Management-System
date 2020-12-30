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

namespace Schools_Management_System.School_Board
{
    public partial class ChildParentsSub : Form
    {
        public ChildParentsSub()
        {
            InitializeComponent();
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }

        private void iconButtonClose_Click(object sender, EventArgs e)
        {
           
        }
    }
}
