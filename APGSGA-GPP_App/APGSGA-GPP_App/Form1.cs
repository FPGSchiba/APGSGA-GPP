using System;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.IO;

namespace APGSGA_GPP_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void WriteUser(string Username)
        {
            //Set an extern Username
            this.User_TB.Text = Username;
            this.User_TB.Refresh();
        }

        public void WritePW(string Password)
        {
            //Set an extern Password
            this.PW_TB.Text = Password;
            this.PW_TB.Refresh();
        }

        private void User_Gene_Click(object sender, EventArgs e)
        {
            //Generate Username
            Program.GenerateUser();
        }

        private void PW_Gene_Click(object sender, EventArgs e)
        {
            //Generate Password
            Program.GeneratePW();
        }

        private void Create_B_Click(object sender, EventArgs e)
        {
            //Get all needed Variables
            string username = this.User_TB.Text;
            string password = this.PW_TB.Text;
            string dateBis = this.dTP_Bis.Text;
            string dateVon = this.dTP_Von.Text;

            //Format querry
            string show = "local-userdb-guest add username " + username + " password " + password + " start-time " + formatTime(dateBis, true) + " expiry time " + formatTime(dateVon, false);

            //Print the Document
            print(username, password);

            //Send the querry String
            Program.sendData(show);

            //Update XML to show the created users
            Thread thread = new Thread(() => Program.addAccess(username, password, formatTime(dateVon, true), formatTime(dateBis, false)));
            thread.Start();
        }

        public static string formatTime(string Time, bool isBis)
        {
            string end = "";

            //Parse Date
            string tag = Regex.Replace(Time, @"[A-Z]{1}[a-z]*,\s", "");
            tag = Regex.Replace(tag, @"\.\s[A-Z]{1}[a-z]*\s\d{4}", "");
            tag = Int32.Parse(tag).ToString("D2");

            //Parse Month
            string monat = Regex.Replace(Time, @"[A-Z]{1}[a-z]*,\s\d{1,2}\.\s", "");
            monat = Regex.Replace(monat, @"\s\d{4}", "");
            monat = formatMonth(monat).ToString("D2");

            //Parse year
            string jahr = Regex.Replace(Time, @"[A-Z]{1}[a-z¨]*,\s\d{1,2}\.\s[A-Z]{1}[a-z]*\s", "");
            jahr = Int32.Parse(jahr).ToString("D4");

            //format date
            end = monat + "/" + tag + "/" + jahr + " ";

            //adding right time
            if (isBis)
            {
                end += "00:00";
            }
            else
            {
                end += "23:59";
            }

            return end;
        }

        public static int formatMonth(string month)
        {
            //string to int
            switch (month)
            {
                case "Januar":
                    return 1;
                case "Februar":
                    return 2;
                case "März":
                    return 3;
                case "April":
                    return 4;
                case "Mai":
                    return 5;
                case "Juni":
                    return 6;
                case "Juli":
                    return 7;
                case "August":
                    return 8;
                case "September":
                    return 9;
                case "November":
                    return 10;
                case "Oktober":
                    return 11;
                case "Dezember":
                    return 12;
                default:
                    return 0;
            }
        }

        private void dTP_Von_ValueChanged(object sender, EventArgs e)
        {
            //Generate a new User
            Program.GenerateUser();
            Program.GeneratePW();

            //Debug minDate
            dTP_Bis.MinDate = dTP_Von.Value.Date;

            //Set bis Date equals von Date
            try
            {
                dTP_Bis.Text = dTP_Von.Text;
                dTP_Von.Refresh();
            }
            catch
            {
                MessageBox.Show("Error while changing Date");
            }
        }

        public string dTP_Von_GetTime()
        {
            //Get The Text from vonDate
            string end = "";
            end = this.dTP_Von.Text;

            //Parse the Day
            string tag = Regex.Replace(end, @"[A-Z]{1}[a-z]*,\s", "");
            tag = Regex.Replace(tag, @"\.\s[A-Z]{1}[a-z]*\s\d{4}", "");

            //Parse the Month
            string monat = Regex.Replace(end, @"[A-Z]{1}[a-z]*,\s\d{1,2}\.\s", "");
            monat = Regex.Replace(monat, @"\s\d{4}", "");
            monat = formatMonth(monat).ToString();

            //Parse the year
            string jahr = Regex.Replace(end, @"[A-Z]{1}[a-z¨]*,\s\d{1,2}\.\s[A-Z]{1}[a-z]*\s", "");

            //Format the output
            end = tag + "." + monat + "." + jahr;

            //Return String
            return end;
        }

        private void dTP_Bis_ValueChanged(object sender, EventArgs e)
        {
            //Function call to set bis MinDate
            setMinDate_dTP_Bis();
        }

        public void setMinDate_dTP_Bis()
        {
            //Set bis MinDate equals to von Date
            dTP_Bis.MinDate = dTP_Von.Value.Date;
        }

        private string fileName = @"C:\temp\Gast_WLAN.docx";

        public void print(string username, string password)
        {
            Thread thread = new Thread(() => printMethod(username, password));
            thread.Start();
        }

        void printMethod(string username, string password)
        {
            //Closing Open Files just in case
            Microsoft.Office.Interop.Word.Application word = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            foreach (Microsoft.Office.Interop.Word.Document dok in word.Documents)
            {
                if (dok.Name == "Gast_WLAN.docx")
                {
                    dok.Close(SaveChanges: false);
                }
            }

            //Deleting Old Files
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            //Create Variables
            var doc = DocX.Create(fileName);
            string title = "Gast Zugang APGSGA";
            string wlanName = "WLAN-Name: APG-Guest" + Environment.NewLine;
            string user = $"Benutzernamen: {username}" + Environment.NewLine;
            string passwd = $"Passwort: {password}" + Environment.NewLine;

            //Formatting Title  
            Formatting titleFormat = new Formatting(); 
            titleFormat.FontFamily = new Font("Seoge UI");
            titleFormat.Size = 26;
            titleFormat.Position = 40;

            //Insert Title
            Paragraph paragraphTitle = doc.InsertParagraph(title, false, titleFormat);
            paragraphTitle.Alignment = Alignment.center;

            //Formatting Anderer Text
            Formatting textParagraphFormat = new Formatting();
            textParagraphFormat.FontFamily = new Font("Seoge UI"); 
            textParagraphFormat.Size = 14;

            //Insert text  
            doc.InsertParagraph(wlanName, false, textParagraphFormat);
            doc.InsertParagraph(user, false, textParagraphFormat);
            doc.InsertParagraph(passwd, false, textParagraphFormat);
            doc.AddProtection(EditRestrictions.readOnly);
            doc.Save();

            //Hide Word
            FileInfo f = new FileInfo(fileName);
            f.Attributes = FileAttributes.Hidden;

            //Real Print
            printDocument();
        }

        //Define the Document and Open the Document with invisible Word
        private Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application { Visible = false };
        private Microsoft.Office.Interop.Word.Document doc;

        private void printDocument()
        {
            //If Doc is not defined Open it first.
            if (doc == null)
            {
                //Open the File
                doc = word.Documents.Open(fileName, ReadOnly: true, Visible: true);
            }

            try
            {
                //Print the Document
                doc.PrintOut();
            }
            catch (Exception e)
            {
                //Handling Printer Errors
                MessageBox.Show($"Error while Printing: {e.Message}");
            }

            //Close doc
            doc.Close();

            //Close everything Just in Case
            Microsoft.Office.Interop.Word.Application wordRun = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            foreach (Microsoft.Office.Interop.Word.Document dok in wordRun.Documents)
            {
                if (dok.Name == "Gast_WLAN.docx")
                {
                    dok.Close(SaveChanges: false);
                }
            }

            //Delete the File to clean up
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            //Document Printed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Show the Data from the XML
            Thread thread = new Thread(Program.showLoop);
            thread.Start();
        }
    }
}