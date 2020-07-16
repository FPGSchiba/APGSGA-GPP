/* 
Version: 1.0
Build: 1.0
Author: Jann Erhardt
*/

using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace APGSGA_GPP_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        //Safe Password and Username
        public static string password = "";
        public static string username = "";
        public static string host = "192.168.5.11";
        public static string user = "admin";
        public static string pwd = "passw0rd";
        public static string localXML = @"C:\temp\Users.xml";

        static void Main()
        {
            //Make a Thread for the MainLoop
            Thread thread = new Thread(MainLoop);
            thread.Start();

            //wait for GUI before try to write a username and a Password
            Thread.Sleep(5000);

            //Initiate Username and Password
            GenerateUser();
            GeneratePW();
        }

        public static void setMinDate()
        {
            //Make a cross-thread action for set the minDate
            if(Application.OpenForms["Form1"] != null)
            {
                Application.OpenForms["Form1"].Invoke(new Action(() => {

                    (Application.OpenForms["Form1"] as Form1).setMinDate_dTP_Bis();

                }));
            }
        }

        public static void GenerateUser()
        {
            //Make a cross-thread action for write a username
            username = CreateUsername();
            if (Application.OpenForms["Form1"] != null)
            {
                Application.OpenForms["Form1"].Invoke(new Action(() => {
                
                    (Application.OpenForms["Form1"] as Form1).WriteUser(username);

                }));
            }
        }

        public static void GeneratePW()
        {
            //Make a cross-thread action for write a password
            password = CreatePW();
            if (Application.OpenForms["Form1"] != null)
            {
                Application.OpenForms["Form1"].Invoke(new Action(() => {

                    (Application.OpenForms["Form1"] as Form1).WritePW(password);

                }));
            }
        }

        //A Test function to test Username and Password output
        static void TestOutputs()
        {
            MessageBox.Show(CreateUsername());
            MessageBox.Show(CreatePW());
            MessageBox.Show(getXMLcontent());
        }

        static void MainLoop()
        {
            //The MainLoop of the Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void showLoop()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }

        //A String for the Password
        static string CreatePW(int length = 8)
        {
            string end = "";
            length = length / 2;
            System.Random random = new System.Random();

            //generates a Password with the Strings underneath
            const string numchars = "0123456789BCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            List<string> randStr = new List<string>();
            for (int i = 0; i <= 2000; i++)
            {
                string AlphaRandom = new string(Enumerable.Repeat(chars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());

                string NumberRandom = new string(Enumerable.Repeat(numchars, length)
                  .Select(s => s[random.Next(s.Length)]).ToArray());

                if (randStr.Contains(AlphaRandom + NumberRandom))
                {
                    i--;
                }
                else
                {
                    randStr.Add(AlphaRandom + NumberRandom);
                    end = randStr[i];
                }
            }
            return end;
        }

        //A String for the username
        public static string CreateUsername()
        {
            string end = "";
            string[] vs = new string[3];
            string temp = "";

            //Makes a cross-thread action to get the Date
            if (Application.OpenForms["Form1"] != null)
            {
                Application.OpenForms["Form1"].Invoke(new Action(() => {

                    temp = (Application.OpenForms["Form1"] as Form1).dTP_Von_GetTime();

                }));
            }

            //Parse the Date
            vs = temp.Split('.');
            string year = vs[2];
            char[] temps = year.ToCharArray();
            year = temps[2].ToString() + temps[3].ToString();
            vs[2] = year;
            foreach(string i in vs)
            {
                end += Int32.Parse(i).ToString("D2");
            }

            //Format String
            end = "Gast" + end;
            return end;
        }

        //Function for the Querry
        public static void sendData(string Data)
        {
            try
            {
                //connect to the server with SSH
                using(SshClient client = new SshClient(host, user, pwd))
                {
                    client.Connect();
                    //create a command in the connection and execute it
                    SshCommand sc = client.CreateCommand(Data);
                    sc.Execute();
                    string antewort = sc.Result;
                    client.Disconnect();
                }
            }
            catch
            {
                MessageBox.Show("Error while connecting to the Server, please check your network");
            }
        }

        //clean up code
        static void deleteXML()
        {
            if (File.Exists(localXML))
            {
                File.Delete(localXML);
            }
        }

        //get the information about the current users
        public static string getXMLcontent()
        {
            string ret = "";

            if (!File.Exists(localXML))
            {
                newXML();
            }

            ret = File.ReadAllText(localXML);

            return ret;
        }

        public static void addAccess(string usern, string passwd, string startDate, string endDate)
        {
            checkXML();

            //Get the content of the current XML
            string completeXML = getXMLcontent();

            //Make the XMl ready to Write
            string temp = Regex.Replace(completeXML, @"<\/guestwlan>", "");

            //Check if there is a base Structure
            if (completeXML == "")
            {
                //No base structure --> Create new one
                newXML();
            }
            else
            {
                //It has a base structure --> Write the string temp on it
                File.WriteAllText(localXML, temp);
            }

            //Get current Time and create a string from it
            string month = DateTime.Now.Month.ToString("D2");
            string day = DateTime.Now.Day.ToString("D2");
            string year = DateTime.Now.Year.ToString("D4");
            string hrs = DateTime.Now.Hour.ToString("D2");
            string mins = DateTime.Now.Minute.ToString("D2");
            string secs = DateTime.Now.Second.ToString("D2");
            string creation = month + "/" + day + "/" + year + " " + hrs + ":" + mins + ":" + secs;

            //write the File with a new row
            using(var sw =  File.AppendText(localXML))
            {
                sw.WriteLine("    <row>");
                sw.WriteLine($"        <guest_username>{usern}</guest_username>");
                sw.WriteLine($"        <guest_password>{passwd}</guest_password>");
                sw.WriteLine($"        <creation_date>{creation}</creation_date>");
                sw.WriteLine($"        <end_date>{endDate}</end_date>");
                sw.WriteLine($"        <start_date>{startDate}</start_date>");
                sw.WriteLine("    </row>");
                sw.Write("</guestwlan>");
                sw.Close();
            }
        }

        //If the File is Empty create a base structure
        static void newXML()
        {
            File.WriteAllText(localXML, "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<guestwlan>\n</guestwlan>\n");
        }

        //deleting Old Users
        public static void deleteOldUser()
        {
            try
            {
                //Open the Document
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(localXML);

                //Search for the Dates where the User ends
                XmlNodeList creations = xDoc.GetElementsByTagName("end_date");
                List<string> dates = new List<string>();

                //Addd to a list all times
                foreach (XmlNode creation in creations)
                {
                    dates.Add(creation.InnerText);
                }

                foreach (string date in dates)
                {
                    //Parse the Time to a DateTime
                    DateTime temp = DateTime.Parse(formatDateTime(date));

                    //Test if the User is outdated
                    if (temp < DateTime.Now)
                    {
                        try
                        {
                            //Using the XML-Library to Parse out the date and delete the Row where its in
                            var xml = File.ReadAllText(localXML);
                            XDocument doc = XDocument.Parse(xml);
                            doc.Descendants().Elements("row").Where(x => x.Element("end_date")?.Value == date).Remove();
                            var result = doc.ToString();

                            //Generate the Varables out of the Date
                            string[] vs;
                            string end = "";
                            string userDate = formatDateTime(date);
                            string[] tempp = userDate.Split(' ');

                            //Parse the Date
                            vs = tempp[0].Split('.');
                            string year = vs[2];
                            char[] temps = year.ToCharArray();
                            year = temps[2].ToString() + temps[3].ToString();
                            vs[2] = year;
                            foreach (string i in vs)
                            {
                                end += Int32.Parse(i).ToString("D2");
                            }

                            //Format String
                            end = "Gast" + end;

                            //delete The User on the Server
                            delUser(end);

                            //Write the new File
                            File.WriteAllText(localXML, result);
                        }
                        catch (Exception e)
                        {
                            //handling errors while Parsing
                            MessageBox.Show("XML ist doof: " + e.Message);
                        }
                    }
                }
            }
            catch
            {
                //nothing
            }
        }

        //Format back to DateTime
        static string formatDateTime(string Date)
        {
            //create a return String
            string end = "";

            //Split up the Date and Time
            string[] temps = Date.Split(' ');
            string time = temps[1];
            string date = temps[0];

            //Split up the date and reorder it
            temps = date.Split('/');
            string day = temps[1];
            string month = temps[0];
            string year = temps[2];

            //Write it in the right order
            end = day + "." + month + "." + year + " " + time;

            //return the String
            return end;
        }

        //delete User on DB
        static void delUser(string usern)
        {
            try
            {
                //connect to the server with SSH
                using (SshClient client = new SshClient(host, user, pwd))
                {
                    client.Connect();
                    //create a command in the connection and execute it
                    SshCommand sc = client.CreateCommand($"ocal-userdb-guest del username \"{usern}\"");
                    sc.Execute();
                    string antewort = sc.Result;
                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                //Error Handling
                MessageBox.Show(e.Message);
            }
        }

        public static void deleteUser(string usern)
        {
            //Using the XML-Library to Parse out the date and delete the Row where its in
            var xml = File.ReadAllText(localXML);
            XDocument doc = XDocument.Parse(xml);
            doc.Descendants().Elements("row").Where(x => x.Element("guest_username")?.Value == usern).Remove();
            var result = doc.ToString();

            //delete The User on the Server
            delUser(usern);

            //Write the new File
            File.WriteAllText(localXML, result);
        }

        //Checking for prblems in the XML
        public static void checkXML()
        {
            //Get the content
            string content = getXMLcontent();
            
            //If everything is deleted make a new structure
            if(content == "<guestwlan />")
            {
                File.WriteAllText(localXML, Regex.Replace("<?xml version =\"1.0\" encoding=\"UTF-8\"?>\n<guestwlan>\n" + content, "<guestwlan />", "</guestwlan>"));
                content = Regex.Replace("<?xml version =\"1.0\" encoding=\"UTF-8\"?>\n<guestwlan>\n" + content, "<guestwlan />", "</guestwlan>");
            }

            //Check if we should better delete it and make a new File
            if(content == "")
            {
                File.Delete(localXML);
                newXML();
            }
        }
    }
}