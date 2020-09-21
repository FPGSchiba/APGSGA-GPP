//Header
//Verison: 1.0
//Author: Jann Erhardt
//Discription: 
/* 
 * Darstellung von Daten --> Darestellung von einem XML
 * 
 * Funktionen: 
 *      1. Init --> Initialisiert alle Komonenten (Form1 Designer -> Generierter Code)
 *      2. load XML --> Ladet das XML ins DataSet1 und dann ins DataGrid und Formatiert die Columns
 *        a. Web requests --> Ladet das XML vom Internet herunter uns speichert dieses
 */

using System;
using System.Windows.Forms;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;
using System.Xml;
using System.Data;
using System.Globalization;

namespace APGSGA_GPP_App
{
    public partial class Form2 : Form
    {
        //Initialisiert das Windows-Form Fenster
        #region Init

        public Form2()
        {
            InitializeComponent();
        }

        #endregion

        //XML öffnen und in ins DataGrid einlesen / Buttons erstellen
        #region load XML

        private static string session;
        private static string xmlpath = @"C:\temp\data.xml";
        private bool isWritten = false;

        [Obsolete]
        private async void dataSet1_InitializedAsync(object sender, EventArgs e)
        {
            //Start get the XML content
            await Task.Run(() => getCookie());

            //Wait for the File to be written
            while (!isWritten){    }

            //Create xml reader
            XmlReader xmlFile = XmlReader.Create(xmlpath, new XmlReaderSettings());

            //Read xml to dataset
            dataSet1.ReadXml(xmlFile);

            //Hide all not used Columns
            dataSet1.Tables["row"].Columns["guest_company"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["guest_phone"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["guest_email"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["guest_status"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["carrier"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["wlan"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["visitor_hours"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["sponsor_username"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["sponsor_fullname"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["sponsor_department"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["sponsor_email"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["optional_field_1"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["optional_field_2"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["optional_field_3"].ColumnMapping = MappingType.Hidden;
            dataSet1.Tables["row"].Columns["optional_field_4"].ColumnMapping = MappingType.Hidden;

            //Add all Time Columns to a List to easly access them
            List<string> vs = new List<string>();
            vs.Add("creation_date");
            vs.Add("start_date");
            vs.Add("end_date");

            //For every Time in the Table
            foreach(string col in vs)
            {
                //Create a List of all Rows from the Column
                List<string> ids = new List<string>(dataSet1.Tables["row"].Rows.Count);
                foreach (DataRow row in dataSet1.Tables["row"].Rows)
                    ids.Add((string)row[col]);
                IFormatProvider enUsDateFormat = new CultureInfo("en-US").DateTimeFormat;

                //For every Row in the Column
                foreach (string val in ids)
                {
                    //Format the string to a Date
                    DateTime time = DateTime.Parse(val, enUsDateFormat);

                    //Get the Right Column and Row
                    for (int i = 0; i < dataSet1.Tables["row"].Rows.Count; i++)
                    {
                        for (int j = 0; j < dataSet1.Tables["row"].Columns.Count; j++)
                        {
                            //Check if its the right data
                            if (dataSet1.Tables["row"].Rows[i][j].ToString() == val.ToString())
                            {
                                //Change the Date-Format
                                dataSet1.Tables["row"].Rows[i][j] = time.ToString(@"dd/MM/yyyy HH:mm");
                            }
                        }
                    }
                }
            }

            //Chang the names to User readable names
            dataSet1.Tables["row"].Columns["guest_username"].ColumnName = "Benutzernamen";
            dataSet1.Tables["row"].Columns["guest_password"].ColumnName = "Passwort";
            dataSet1.Tables["row"].Columns["guest_fullname"].ColumnName = "Name";
            dataSet1.Tables["row"].Columns["comments"].ColumnName = "Kommentare";
            dataSet1.Tables["row"].Columns["creation_date"].ColumnName = "Erstellungsdatum";
            dataSet1.Tables["row"].Columns["start_date"].ColumnName = "von Datum";
            dataSet1.Tables["row"].Columns["end_date"].ColumnName = "bis Datum";
            dataSet1.Tables["row"].Columns["grantor"].ColumnName = "Ersteller";
            dataSet1.Tables["row"].Columns["grantor_role"].ColumnName = "Rolle";

            //show the Formatted row
            dataGridView1.DataSource = dataSet1.Tables["row"];

            //Close xml reader
            xmlFile.Close();
        }

        #region WebRequests

        //To Format the Cookies to reuse them
        private string formatSession(string sess)
        {
            string ret = "";

            var temp = sess.Split(';');
            ret = temp[0];

            return ret;
        }

        private async void getCookie()
        {
            //Create the Http client
            HttpClient client = new HttpClient();

            //Login Data to get the Session
            var values = new Dictionary<string, string>
            {
                { "opcode", "login" },
                { "url", "/logout.html" },
                { "needxml", "0" },
                { "uid", Program.user },
                { "passwd", Program.pwd }
            };

            //Add the Login Data
            var content = new FormUrlEncodedContent(values);

            //Ignore unsafe Webpages
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            //Make a POST request to get a Session key
            using (var response = await client.PostAsync("https://aruba01.apgsga.ch:4343/screens/wms/wms.login", content))
            {
                foreach (var header in response.Headers)
                {
                    if(header.Key == "Set-Cookie")
                    {
                        //Safe session key
                        session = formatSession(header.Value.ToArray()[0]);
                    }
                }
            }

            //Use the session key to download the XML
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Cookie", session);

            using (var response = await client.GetAsync("https://aruba01.apgsga.ch:4343/screens/cmnutil/gc_data.xml"))
            {
                string xmlcontent = await response.Content.ReadAsStringAsync();
                using (StreamWriter sw = new StreamWriter(xmlpath))
                {
                    //Safe the XML
                    sw.Write(xmlcontent);
                    sw.Close();
                }
            }

            //Give the OK to read the XML
            isWritten = true;
        }

        #endregion

        //Delete the File for safety when Form2 is closing
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(xmlpath))
            {
                File.Delete(xmlpath);
            }
        }

        #endregion

    }
}
