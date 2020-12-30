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
    public partial class StudentFacility : ChildParentsSub
    {
        public StudentFacility()
        {
            InitializeComponent();
        }

        private void iconButtonStudentFacilityforClose_Click(object sender, EventArgs e)
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
}
