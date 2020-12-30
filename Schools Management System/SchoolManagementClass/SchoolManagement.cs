using Schools_Management_System.SchoolManagementForms.New_Windows_Form;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schools_Management_System.SchoolManagementClass
{
    public class SchoolManagement
    {
        SchoolHome child = new SchoolHome();
        private static string folder = Application.StartupPath + "\\Images";
        public static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static void ShowWindow(Form openWin, Form closeWin)
        {
            Panel p = closeWin.Parent as Panel;
            if (p != null)
            {
                openWin.FormBorderStyle = FormBorderStyle.None;
                openWin.TopLevel = false;
                openWin.AutoScroll = true;
                p.Controls.Add(openWin);
                openWin.Dock = DockStyle.Fill;
                openWin.BringToFront();
                p.Tag = openWin;
                openWin.TopMost = true;
                openWin.Show();
                closeWin.Close();
            }
        }

        static Form activeform = null;
        public static void OpenChildForms(Form childform, Panel pan)
        {
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = childform;
            childform.TopLevel = false;
            childform.FormBorderStyle = FormBorderStyle.None;
            childform.Dock = DockStyle.Fill;
            pan.Controls.Add(childform);
            pan.Tag = childform;
            childform.BringToFront();
            childform.Show();
        }
        
        public static IEnumerable<Control> GetAllControls(Control aControl)
        {
            Stack<Control> stack = new Stack<Control>();

            stack.Push(aControl);

            while (stack.Any())
            {
                var nextControl = stack.Pop();

                foreach (Control childControl in nextControl.Controls)
                {
                    stack.Push(childControl);
                }

                yield return nextControl;
            }
        }
        // Image Show in Datagridview

        public static void ImageShowInDataGridview(DataGridView ShowImage)
        {
            Bitmap bmpImage = null;
            //DataGridViewImageColumn ic = new DataGridViewImageColumn();
            //ic.HeaderText = "Img";
            //ic.Image = null;
            //ic.Name = "cImg";
            //ic.Width = 100;
            //ShowImage.Columns.Add(ic);
            foreach (DataGridViewRow rows in ShowImage.Rows)
            {
                string imageName = rows.Cells["ImagePathGV"].Value.ToString();
                string imagePath = System.IO.Path.Combine(folder, imageName);

                if (imagePath != null)
                {
                    bmpImage = (Bitmap)Image.FromFile(imagePath, true);
                    Bitmap imageRS = new Bitmap(bmpImage, new Size(40, 80));
                    DataGridViewImageCell cell = rows.Cells["StudentImageGV"] as DataGridViewImageCell;                    
                    cell.Value = imageRS;
                    //((DataGridViewImageCell)rows.Cells["StudentImageGV"]).Value = imageRS;
                }
                else
                {
                   ShowMessage("Image Not Available!!", "Try Again!", "Warning");
                }
            }
        }

        // Age Calculator Design

        public static void CalculateAge(DateTimePicker DOB, TextBox txtbox)
        {
            DateTime birthDate = DOB.Value;
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(birthDate).Ticks).Year - 1;
            DateTime PastYearDate = birthDate.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;
           var valuDate =  String.Format("{0} Year(s) {1} Month(s) {2} Day(s)",
                                Years, Months, Days);
            if(Years <= 5)
            {
                txtbox.Text = null;
            }
            else
            {
                txtbox.Text = valuDate;
            }
           
        }

        //Student Duration In That Institute

        public static void StudentDurationCal(DateTimePicker AdmissonDate, TextBox studentDur)
        {
            DateTime admitaDate = AdmissonDate.Value;
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(admitaDate).Ticks).Year - 1;
            DateTime PastYearDate = admitaDate.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;
            var valuDate = String.Format("{0} Year(s) {1} Month(s) {2} Day(s)",
                                 Years, Months, Days);
            studentDur.Text = valuDate;
        }


        public static void StudentDurationCals(DateTime admitaDate, Label studentDur)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(admitaDate).Ticks).Year - 1;
            DateTime PastYearDate = admitaDate.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;
            var valuDate = String.Format("{0} Year(s) {1} Month(s) {2} Day(s)",
                                 Years, Months, Days);
            studentDur.Text = valuDate;
        }

        public static DialogResult ShowMessage(string msg, string heading, string type)
        {
            if (type == "Success")
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }  

        public static void ShowControls(Panel showPanel)
        {
            if(showPanel.Visible == false)
            {
                showPanel.Visible = true;
            }
        }
       
        public static void ShowTableLayoutPan(Panel pn, TableLayoutPanel tablepan)
        {
           foreach(Control c in tablepan.Controls)
            {
                if(c is TableLayoutPanel)
                {
                    TableLayoutPanel tbp = (TableLayoutPanel)c;
                    tbp.Visible = false;
                }
            }
        }
        
        public static void Disable_Reset(Panel p, TableLayoutPanel t)
        {
            foreach (Control c in t.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    tb.Enabled = false;
                    tb.Text = "";
                }
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.Enabled = false;
                    cb.SelectedIndex = -1;
                }
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    rb.Enabled = false;
                    rb.Checked = false;
                }
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Enabled = false;
                    cb.Checked = false;
                }
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.Enabled = false;
                    dtp.Value = DateTime.Now;

                }
                if (c is Button)
                {
                    Button btn = (Button)c;
                    btn.Enabled = false;
                }
            }
        }

        internal void ShowMessage()
        {
            throw new NotImplementedException();
        }

        public static void Disable(Panel p, TableLayoutPanel t)
        {
            foreach (Control c in t.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    tb.Enabled = false;
                }
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.Enabled = false;
                }
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    rb.Enabled = false;
                }
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Enabled = false;
                }
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.Enabled = false;

                }
                if (c is Button)
                {
                    Button btn = (Button)c;
                    btn.Enabled = false;
                }
            }
        }
        public static void Enable_Reset(Panel p, TableLayoutPanel t)
        {
            foreach (Control c in t.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    tb.Enabled = true;
                    tb.Text = "";
                }
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.Enabled = true;
                    cb.SelectedIndex = 0;
                }
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    rb.Enabled = true;
                    rb.Checked = false;
                }
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Enabled = true;
                    cb.Checked = false;
                }
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.Enabled = true;
                    dtp.Value = DateTime.Now;

                }
                if (c is Button)
                {
                    Button btn = (Button)c;
                    btn.Enabled = true;
                }
            }
        }
        public static void Enable_Reset(GroupBox gb)
        {
            foreach (Control c in gb.Controls)
            {
                if (c is TextBox)
                {
                    TextBox t = (TextBox)c;
                    t.Enabled = true;
                    t.Text = "";
                }
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.Enabled = true;
                    cb.SelectedIndex = -1;
                }
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    rb.Enabled = true;
                    rb.Checked = false;
                }
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Enabled = true;
                    cb.Checked = false;
                }
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.Enabled = true;
                    dtp.Value = DateTime.Now;

                }
                if (c is Button)
                {
                    Button btn = (Button)c;
                    btn.Enabled = true;
                }
            }
        }
        public static void Enable(Panel p, TableLayoutPanel t)
        {
            foreach (Control c in t.Controls)
            {
                if (c is TextBox)
                {
                    TextBox tb = (TextBox)c;
                    tb.Enabled = true;
                }
                if (c is ComboBox)
                {
                    ComboBox cb = (ComboBox)c;
                    cb.Enabled = true;
                }
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    rb.Enabled = true;
                }
                if (c is CheckBox)
                {
                    CheckBox cb = (CheckBox)c;
                    cb.Enabled = true;
                }
                if (c is DateTimePicker)
                {
                    DateTimePicker dtp = (DateTimePicker)c;
                    dtp.Enabled = true;
                }
                if (c is Button)
                {
                    Button btn = (Button)c;
                    btn.Enabled = true;
                }
            }
        }
    }
}
