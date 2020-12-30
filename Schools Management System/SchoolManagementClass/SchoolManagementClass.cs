using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Schools_Management_System.SchoolManagementClass;
using Schools_Management_System.SchoolManagementForms;
using Schools_Management_System.SchoolManagementForms.New_Windows_Form;

namespace Schools_Management_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SchoolHome());
        }
    }
}
