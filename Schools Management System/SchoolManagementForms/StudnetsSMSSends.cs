using Schools_Management_System.SchoolManagementClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schools_Management_System.SchoolManagementForms
{
    public partial class StudnetsSMSSends : Form
    {
        public StudnetsSMSSends()
        {
            InitializeComponent();
        }

        private void iconButtonClose_Click(object sender, EventArgs e)
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

        private void StudnetsSMSSends_Load(object sender, EventArgs e)
        {
            comboBoxSelectPersonContainer.SelectedIndex = 0;
            comboBoxSelectStudentShift.SelectedIndex = 0;
            comboBoxSelectStudentStandard.SelectedIndex = 0;
            //comboBoxSelectPersonPositions.SelectedIndex = 0;
            //comboBoxSelectPersonTypes.SelectedIndex = 0;
        }
        private void comboBoxSelectPersonContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBoxSelectPersonContainer.Text == "Stusdents")
            {
                comboBoxSelectStudentShift.Visible = true;
                comboBoxSelectPersonPositions.Visible = false;
                comboBoxSelectPersonTypes.Visible = false;               
            }
            else
            {
                comboBoxSelectStudentShift.Visible = false;
                comboBoxSelectPersonPositions.Visible = true;
                comboBoxSelectPersonTypes.Visible = true;
                comboBoxSelectStudentStandard.Visible = false;
            }
        }
        private void textBoxStudentSMSSender_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStudentSMSSender.Text == "")
            {
                labelSendSMSSenderErr.Visible = true;
            }
            else
            {
                labelSendSMSSenderErr.Visible = false;
            }
        }
        private void textBoxStudentSMSPassword_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStudentSMSPassword.Text == "")
            {
                labelStudentSMSPasswordErr.Visible = true;
            }
            else
            {
                labelStudentSMSPasswordErr.Visible = false;
            }
        }

        private void textBoxStudentSMSNumber_TextChanged(object sender, EventArgs e)
        {
            Regex expr = new Regex(@"^((\+){0,1}91(\s){0,1}(\-){0,1}(\s){0,1}){0,1}9[0-9](\s){0,1}(\-){0,1}(\s){0,1}[1-9]{1}[0-9]{7}$");
            string phoneNumbers = textBoxStudentSMSNumber.Text;
            if (textBoxStudentSMSNumber.Text == "")
            {
                labelSendSMSNumberErr.Visible = true;
            }
            else
            {                         
                if (phoneNumbers.Length > 11)
                {
                    if (expr.IsMatch(phoneNumbers))
                    {
                        textBoxStudentSMSNumber.Multiline = true;
                        tableLayoutPanelStudentSMSSenderWhole.SetRowSpan(textBoxStudentSMSNumber, 2);
                        labelSendSMSNumberErr.Visible = false;
                    }
                    else
                    {
                        SchoolManagement.ShowMessage("Only Valid Phone Number Will Be Entered Here!", "Try Again!", "Warning");
                        textBoxStudentSMSNumber.Text = "";
                        labelSendSMSNumberErr.Visible = true;
                    }
                }
                else
                {
                    textBoxStudentSMSNumber.Multiline = false;
                    tableLayoutPanelStudentSMSSenderWhole.SetRowSpan(textBoxStudentSMSNumber, 1);
                    labelSendSMSNumberErr.Visible = false;                    
                }



                //string regexPattern = @"^+880[0-9]{9}$";
                //Regex r = new Regex(regexPattern);
                //List<string> numbers = new List<string>();
                
                //foreach (string s in numbers)
                //{
                //    if (r.Match(s).Success)
                //    {
                //        labelSendSMSNumberErr.Visible = false;
                //    }
                //    else
                //    {
                //        SchoolManagement.ShowMessage("Only Valid Phone Number Will Be Entered Here!", "Try Again!", "Warning");
                //        textBoxStudentSMSNumber.Text = "";
                //        labelSendSMSNumberErr.Visible = true;
                //    }
                //}             
               
            }
        }

        private void textBoxStudentSMSMessages_TextChanged(object sender, EventArgs e)
        {
            if (textBoxStudentSMSMessages.Text == "")
            {
                labelSendSMSMessageErr.Visible = true;
            }
            else
            {
                labelSendSMSMessageErr.Visible = false;
            }
        }

        private void iconButtonStudentSendSMS_Click(object sender, EventArgs e)
        {
            if (textBoxStudentSMSNumber.Text != "" || textBoxStudentSMSPassword.Text != "" || textBoxStudentSMSNumber.Text != "" || textBoxStudentSMSMessages.Text != "")
            {
                //string result = "";
                //WebRequest request = null;
                //HttpWebResponse response = null;
                //try
                //{
                //    String to = textBoxStudentSMSNumber.Text; //Recipient Phone Number multiple number must be  separated by comma
                //    String username = textBoxStudentSMSSender.Text; //Your Username
                //    String hash_token = "e23f24976667329e8b356d7718493163"; //generate token from the control panel
                //    String message = System.Uri.EscapeUriString(textBoxStudentSMSMessages.Text); //do not use single quotation   (') in the message to avoid forbidden result
                //    String url = "http://alphasms.biz/index.php?app=ws&op=pv&u=" + username + "&h=" + hash_token + "&to=" + to + "&msg=" + message;
                //    request = WebRequest.Create(url);

                //    // Send the 'HttpWebRequest' and wait for response.
                //    response = (HttpWebResponse)request.GetResponse();
                //    Stream stream = response.GetResponseStream();
                //    Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                //    StreamReader reader = new
                //    System.IO.StreamReader(stream, ec);
                //    result = reader.ReadToEnd();
                //    Console.WriteLine(result);
                //    reader.Close();
                //    stream.Close();
                //    SchoolManagement.ShowMessage("Your Message is Send Successfully!","Thank You!","Success");
                //    textBoxStudentSMSNumber.Text = "";
                //    textBoxStudentSMSMessages.Text = " ";

                //}
                //catch (Exception exp)
                //{
                //    MessageBox.Show(null,"Please Input Number Carefully"+ exp.Message,MessageBoxButtons.OK,MessageBoxIcon.Information);
                //}
                //finally
                //{
                //    if (response != null)
                //        response.Close();
                //}

                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    try
                    {
                        string url = "http://smsc.vianett.no/v3/send.ashx?" +
                            "src=" + textBoxStudentSMSNumber.Text + "&" +
                            "dst=" + textBoxStudentSMSNumber.Text + "&" +
                            "msg=" + System.Web.HttpUtility.UrlEncode(textBoxStudentSMSMessages.Text, System.Text.Encoding.GetEncoding("ISO-8859-1")) + "&" +
                            "username=" + System.Web.HttpUtility.UrlEncode(textBoxStudentSMSSender.Text) + "&" +
                            "password=" + System.Web.HttpUtility.UrlEncode(textBoxStudentSMSPassword.Text);
                        //Call web api to send sms messages
                        string result = client.DownloadString(url);
                        if (result.Contains("OK"))
                            MessageBox.Show("Your message has been successfully sent.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Message send failure.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else 
            {
                SchoolManagement.ShowMessage("Please Fill Up Those Fields Carefully", "Try Again!", "Warning");
            }
        }

        private void comboBoxSelectStudentShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSelectStudentShift.Visible == true && comboBoxSelectStudentShift.SelectedIndex > 0)
            {
                comboBoxSelectStudentStandard.Visible = true;
            }
            else
            {
                comboBoxSelectStudentStandard.Visible = false;
            }
        }        
    }
}
