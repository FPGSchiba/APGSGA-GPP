﻿//Header
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
 *      6. Show Users --> Zeigt die Form2, also die erstellten Benutzer
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
            DateTime dateBis = dTP_Bis.Value;
            DateTime dateVon = dTP_Von.Value;

            //Format querry
            string show = "local-userdb-guest add username " + username + " password " + password + " start-time " + dateVon.ToString(@"MM\/dd\/yyyy") + " 01:00 " + " expiry time " + dateBis.ToString(@"MM\/dd\/yyyy") + " 23:59";

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

            DateTime time = dTP_Von.Value;
            end = time.ToString(@"dd/MM/yyyy");

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
            string von = $"Gültig von: {dTP_Von.Value.ToString("dd.MM.yyyy ")} 01:00";
            string bis = $"Gültig bis: {dTP_Bis.Value.ToString("dd.MM.yyyy")} 23:59";
            string creationTime = $"Erstellungszeit: {DateTime.Now.ToString("dd.MM.yyyy HH:mm")}";

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

            //Formatting Footer Text
            Formatting textFooterParagraphFormat = new Formatting();
            textFooterParagraphFormat.FontFamily = new Font("Seoge UI");
            textFooterParagraphFormat.Size = 10;

            //Insert text 
            doc.AddFooters();
            doc.InsertParagraph(wlanName, false, textParagraphFormat);
            doc.InsertParagraph(user, false, textParagraphFormat);
            doc.InsertParagraph(passwd, false, textParagraphFormat);
            doc.Footers.Odd.InsertParagraph(von, false, textFooterParagraphFormat).Alignment = Alignment.right;
            doc.Footers.Odd.InsertParagraph(bis, false, textFooterParagraphFormat).Alignment = Alignment.right;
            doc.Footers.Odd.InsertParagraph(creationTime, false, textFooterParagraphFormat).Alignment = Alignment.right;
            doc.AddProtection(EditRestrictions.readOnly);
            doc.Save();

            using (PrintDialog pd = new PrintDialog())
            {
                if(pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Microsoft.Office.Interop.Word.Application app = (Microsoft.Office.Interop.Word.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application");

                    app.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;

                    object filename = fileName;
                    object missingValue = Type.Missing;

                    Microsoft.Office.Interop.Word.Document document = app.Documents.Open(ref filename);

                    app.ActivePrinter = pd.PrinterSettings.PrinterName;

                    object myTrue = true;
                    object myFalse = false;

                    app.ActiveDocument.PrintOut(ref myTrue, ref myFalse, pd.PrinterSettings.PrintRange, ref missingValue, pd.PrinterSettings.FromPage.ToString(), pd.PrinterSettings.ToPage.ToString(), ref missingValue, pd.PrinterSettings.Copies, ref missingValue, ref missingValue, ref myFalse, ref missingValue, ref missingValue, ref missingValue);

                    document.Close(ref missingValue, ref missingValue, ref missingValue);

                    while (app.BackgroundPrintingStatus > 0)
                    {
                        System.Threading.Thread.Sleep(250);
                    }

                    app.Quit();

                    MessageBox.Show("Fertig!");
                }
            }

        }

        #endregion

        //Angefangener Code
        #region Show Users

        private void button1_Click(object sender, EventArgs e)
        {
            //Show the Data from the XML
            Thread thread = new Thread(Program.showLoop);
            thread.Start();
        }

        #endregion
    }
}