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
    public partial class StudentInformations : ChildParentsSub
    {
        ShowDataInGridView showdata = new ShowDataInGridView();        
        public StudentInformations()
        {
            InitializeComponent();
            Font theFont = new Font("Arial", 9.0F, FontStyle.Regular);

            foreach (Control theControl in (SchoolManagement.GetAllControls(this)))
            {
                theControl.Font = theFont;
            }
        }

        
        private void StudentInformations_Shown(object sender, EventArgs e)
        {
           showdata.ShowStudentInformationsDestils(dataGridViewStudentInformationDetails, textBoxStudentInformationSearch);
           SchoolManagement.ImageShowInDataGridview(dataGridViewStudentInformationDetails);
        }
        private void StudentInformations_Load(object sender, EventArgs e)
        {
            //showdata.ShowStudentInformationsDestils(dataGridViewStudentInformationDetails, textBoxStudentInformationSearch);
            //Bitmap bmpImage = null;
            //DataGridViewImageColumn ic = new DataGridViewImageColumn();
            //ic.HeaderText = "Img";
            //ic.Image = null;
            //ic.Name = "cImg";
            //ic.Width = 100;
            //dataGridViewStudentInformationDetails.Columns.Add(ic);
            //foreach (DataGridViewRow rows in dataGridViewStudentInformationDetails.Rows)
            //{
            //    string imageName = rows.Cells["ImagePathGV"].Value.ToString();
            //    string imagePath = System.IO.Path.Combine(folder, imageName);

            //    if (imagePath != null)
            //    {
            //        bmpImage = (Bitmap)Image.FromFile(imagePath, true);
            //        Bitmap imageRS = new Bitmap(bmpImage, new Size(40, 50));
            //        DataGridViewImageCell cell = rows.Cells["cImg"] as DataGridViewImageCell;
            //        cell.Value = imageRS;
            //        //((DataGridViewImageCell)rows.Cells["StudentImageGV"]).Value = imageRS;
            //    }
            //    else
            //    {
            //        SchoolManagement.ShowMessage("Image Not Available!!", "Try Again!", "Warning");
            //    }
            //}
        }

        private void dataGridViewStudentInformationDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewStudentInformationDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridViewStudentInformationDetails.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }        

        private void textBoxStudentInformationSearch_TextChanged(object sender, EventArgs e)
        {            

            DataView DV = new DataView();

            DV.RowFilter = string.Format("Name LIKE '%{0}%'", textBoxStudentInformationSearch.Text);

            showdata.ShowStudentInformationsDestils(dataGridViewStudentInformationDetails, textBoxStudentInformationSearch);
        }

        private void iconButtonStudentInformationSearch_Click(object sender, EventArgs e)
        {
            if (textBoxStudentInformationSearch.Text == "")
            {
                SchoolManagement.ShowMessage("Please Enter Class/Student Name!!", "Try Again!", "Warning");
            }
            else
            {
                showdata.ShowStudentInformationsDestilsByTextSearch(dataGridViewStudentInformationDetails, textBoxStudentInformationSearch);
                SchoolManagement.ImageShowInDataGridview(dataGridViewStudentInformationDetails);
            }
        }

        private void iconButtonStudentShowInforClose_Click(object sender, EventArgs e)
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
