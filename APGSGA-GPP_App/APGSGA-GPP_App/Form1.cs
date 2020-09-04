//Header
//Verison: 1.1
//Author: Jann Erhardt
//Discription: 
/* 
 * Der Main-Loop des Programms
 * 
 * Funktionen: 
 *      1. Init --> Initialisiert alle Komonenten (Form1 Designer -> Generierter Code)
 *      2. User --> Erstellt neuen Benutzer oder verändert ihn
 *      3. Create User --> Initialisiert die Erstellung des Benutzers (Formatiert das Datume und erstellt den Querry-String)
 *      4. Handling Time --> Alle Funktionen zu den TimePicker
 *      5. Print Handling --> Macht das Word Dokument und Druckt es beim Standard Drucker des Gerätes
 *      6. Not Implemented --> Noch nicht fertige Code Stücke / entfernte Code stücke
 */


using System;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Xceed.Words.NET;
using Xceed.Document.NET;
using System.IO;
using System.Diagnostics;

namespace APGSGA_GPP_App
{
    public partial class Form1 : Form
    {

        //Alle Variablen, die Benötigt werden
        #region Variables

        //Define The Word Path
        private string fileName = @"C:\temp\Gast_WLAN.docx";

        //Define the Document and Open the Document with invisible Word
        private Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application { Visible = false };
        private Microsoft.Office.Interop.Word.Document doc;

        #endregion

        //Initialisiert das Windows-Form Fenster
        #region Init

        //Initalise the Components
        public Form1()
        {
            InitializeComponent();
        }

        #endregion

        //verändert und erstellt Benutzer (Benutzer = Benutzernamen & Passwort)
        #region Add and Change User

        public void Adduser()
        {
            User user = new User();
            this.PW_TB.Text = user.Password;
            this.PW_TB.Refresh();
            this.User_TB.Text = user.Username;
            this.User_TB.Refresh();
        }

        private void User_Gene_Click(object sender, EventArgs e)
        {
            User user = new User();
            this.User_TB.Text = user.Username;
            this.User_TB.Refresh();
        }

        private void PW_Gene_Click(object sender, EventArgs e)
        {
            User user = new User();
            this.PW_TB.Text = user.Password;
            this.PW_TB.Refresh();
        }

        #endregion

        //Initialisert den Querry-String und das Drucken des Benutzers
        #region Create and Format the User

        [Obsolete]
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
            Thread tprint = new Thread(() => print(username, password));

            //Send the querry String
            Thread tsend = new Thread(() => Program.sendData(show));

            //Start both Threads
            tprint.Start();
            tsend.Start();

            //Update XML to show the created users
            //Thread thread = new Thread(() => Program.addAccess(username, password, formatTime(dateVon, true), formatTime(dateBis, false)));
            //thread.Start();
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
                end += "01:00";
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

        #endregion

        //Alle Funktionen zu den TimePicker
        #region Handling Time

        private void dTP_Von_ValueChanged(object sender, EventArgs e)
        {
            //Add A User
            Adduser();

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

        public void debugMinDate()
        {
            //Set the Mindate of Bis-Date to Von-Date
            dTP_Bis.MinDate = dTP_Von.Value.Date;
        }

        #endregion

        //Druckt den Benutzer
        #region Print Handling

        [Obsolete]
        public void print(string username, string password)
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

            //Creating DIrectory
            if (!Directory.Exists(@"C:\temp\"))
            {
                Directory.CreateDirectory(@"C:\temp\");
            }

            //Deleting Old Files
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            //Create Variables
            var doc = DocX.Create(fileName);
            string title = "Gast Zugang APGSGA";
            string wlanName = "WLAN-Name: APGSGAguest" + Environment.NewLine;
            string user = $"Benutzernamen: {username}" + Environment.NewLine;
            string passwd = $"Passwort: {password}";

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

            Microsoft.Office.Interop.Word.Application appWord = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document wordDocument = appWord.Documents.Open(fileName);
            wordDocument.ExportAsFixedFormat(@"C:\temp\out.pdf", Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF);
            wordDocument.Close();
            appWord.Quit();

            //Delete Word
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            //Real Print
            printDocument();
        }

        [Obsolete]
        private void printDocument()
        {
            try
            {
                Process p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    Verb = "print",
                    FileName = @"C:\temp\out.pdf" //put the correct path here
                };
                p.Start();
            }
            catch (Exception e)
            {
                //Handling Printer Errors
                MessageBox.Show($"Error while Printing: {e.Message}");
            }

            /*Close everything Just in Case
            Microsoft.Office.Interop.Word.Application wordRun = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");
            foreach (Microsoft.Office.Interop.Word.Document dok in wordRun.Documents)
            {
                if (dok.Name == "Gast_WLAN.docx")
                {
                    dok.Close(SaveChanges: false);
                }
            }
            */

            //Document Printed
            MessageBox.Show("Benutzer erfolgreich erstellt");
        }

        #endregion

        //Angefangener Code
        #region Not Implemented

        private void button1_Click(object sender, EventArgs e)
        {
            //Show the Data from the XML
            Thread thread = new Thread(Program.showLoop);
            thread.Start();
        }

        #endregion
    }
}