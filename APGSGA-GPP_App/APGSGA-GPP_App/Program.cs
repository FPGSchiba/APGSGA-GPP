using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

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

        //The Information to connect to the Server
        public static string host = "192.168.5.11";
        public static string user { get; private set; }
        public static string pwd { get; private set; }
        private static string Gast;
        public static Dictionary<string, string> dic_login = new Dictionary<string, string>();

        //File location
        public static string localXML = @"C:\temp\Users.xml";

        static void Main(string[] args)
        {
            //Add the usernames and Passwords
            dic_login.Add("zhg", "c47a2624a4d01589f9696f3bb45cff8a");
            dic_login.Add("admin", "bed128365216c019988915ed3add75fb");
            dic_login.Add("hotline", "b0700b31ce0c29f134059ebda6516ad8");

            //Make the Login-Loop
            Thread thread = new Thread(loginLoop);
            thread.Start();
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

        public static void loginLoop()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
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
            end = Gast + end;
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

        //get the information about the current users
        public static string getXMLcontent()
        {
            string ret = "";

            return ret;
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
                    if(antewort == "")
                    {

                    }
                    else
                    {
                        MessageBox.Show(antewort);
                    }
                    client.Disconnect();
                }
            }
            catch (Exception e)
            {
                //Error Handling
                MessageBox.Show(e.Message);
            }
        }

        //Get the MD5-Hash code from a string
        public static string MD5Hash(string input)
        {
            //Translate string to bytes
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            //Write the Hex for the bytes
            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


        public static void validateLogin(string password, string username)
        {
            bool validate;
            try
            {
                validate = dic_login[username] == MD5Hash(password);
            }
            catch
            {
                validate = false;

                //Makes a cross-thread action to reset the Login Information
                if (Application.OpenForms["login"] != null)
                {
                    Application.OpenForms["login"].Invoke(new Action(() => {

                        (Application.OpenForms["login"] as login).resetLogin();

                    }));
                }
            }

            if(validate)
            {
                //Set username and Password for connecting to the server
                user = username;
                pwd = password;

                //Close Login
                Form.ActiveForm.Close();

                //Make a Thread for the MainLoop
                Thread thread = new Thread(MainLoop);
                thread.Start();

                //Wait til the Program has started
                string temp = ".0.";
                while(temp == ".0.")
                {
                    if (Application.OpenForms["Form1"] != null)
                    {
                        Application.OpenForms["Form1"].Invoke(new Action(() => {

                            temp = (Application.OpenForms["Form1"] as Form1).dTP_Von_GetTime();

                        }));
                    }
                }

                //Init the Username and Password
                wrigthGuest();
                GenerateUser();
                GeneratePW();
            }
            else
            {
                MessageBox.Show("Falsches oder nicht bekanntes Login.");
            }
        }

        //Making an Other Username per Login
        private static void wrigthGuest()
        {
            string komplett = Environment.MachineName;

            if(Regex.IsMatch(komplett, @"[CL][LA][A-Z]{3}\d{9}"))
            {
                komplett = Regex.Replace(komplett, "^[CL][LA]", "");
                komplett = Regex.Replace(komplett, @"\d{9}$", "");
                Gast = komplett;
            }
            else
            {
                string response = Interaction.InputBox("Für welchen Standort benötigsts du einen Benutzer?", "Standort", "ZHG");
                Gast = response;
            }
        }

        //Optional (not implemented yet
        public static void writeXML()
        {
            var psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\python.exe";
            var script = @"C:\temp\GetXML.py";
            psi.Arguments = $"\"{script}\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            var errors = "";
            var results = "";
            using(var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            MessageBox.Show(errors);
            MessageBox.Show(results);
        }
    }
}