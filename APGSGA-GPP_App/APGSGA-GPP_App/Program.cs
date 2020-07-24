//Header
//Verison: 1.0
//Author: Jann Erhardt
//Discription: 
/* 
 * Der Haupt eintritts Punkt des Programms
 * 
 * Funktionen: 
 *      1. Main --> Der Eintritts Punkt des Programms
 *      2. Die Loops --> Die Windows-Form Loops 
 *      3. Querry --> Die Querry Funktion, mit der cmd Commands auf dem Server ausgeführt werden
 *      4. Login Validation --> Die Überprüfung des Logins
 *      5. Init Main-Loop --> Die Initialisierung des Hauptprogramms
 *      6. Not Implemented --> Noch nicht fertige Code Stücke / entfernte Code stücke
 */
using Renci.SshNet;
using System;
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
        //Alle Variablen, die Benötigt werden
        #region Variablen
        //Safe Password and Username
        public static string password = "";
        public static string username = "";

        //The Information to connect to the Server
        public static string host = "192.168.5.11";
        public static string user { get; private set; }
        public static string pwd { get; private set; }
        public static string Gast;
        public static LoginData login = new LoginData();

        //File location
        public static string localXML = @"C:\temp\Users.xml";

        #endregion

        //Die void Main Funktion von c#
        #region Programm-Start
        static void Main(string[] args)
        {
            //Make the Login-Loop
            Thread thread = new Thread(loginLoop);
            thread.Start();
        }

        #endregion

        //Alle Loops von den Windwos-Form Klassen
        #region Loops

        public static void MainLoop()
        {
            //The MainLoop of the Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static void showLoop()
        {
            //The Showloop of the Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form2());
        }

        public static void loginLoop()
        {
            //The Loginloop of the Application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
        }

        #endregion

        //Die Querry Funktion
        #region Querry

        //Function for the Querry
        public static void sendData(string Data)
        {
            try
            {
                //connect to the server with SSH
                using (SshClient client = new SshClient(host, user, pwd))
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
                //Error Message, when the Command couldn't be executed
                MessageBox.Show("Error while connecting to the Server, please check your network");
            }
        }

        #endregion

        //Die Validierung des Logins
        #region Login Validation

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

        //Validate the Login Information
        public static void validateLogin(string password, string username)
        {
            bool validate;

            //Try to use the username to get the Password from the Dictionary
            try
            {
                //Validate must be true
                validate = login.dic_login[username] == MD5Hash(password);
            }
            catch
            {
                //Validate is False
                validate = false;

                //Makes a cross-thread action to reset the Login Information
                if (Application.OpenForms["login"] != null)
                {
                    Application.OpenForms["login"].Invoke(new Action(() => {

                        (Application.OpenForms["login"] as login).resetLogin();

                    }));
                }
            }

            //Check if the user is Authorised to access the Application
            if (validate)
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
                while (temp == ".0.")
                {
                    if (Application.OpenForms["Form1"] != null)
                    {
                        Application.OpenForms["Form1"].Invoke(new Action(() => {

                            temp = (Application.OpenForms["Form1"] as Form1).dTP_Von_GetTime();

                        }));
                    }
                }

                //Init the Username and Password
                startMainLoop();
            }
            else
            {
                MessageBox.Show("Falsches oder nicht bekanntes Login.");
            }
        }

        #endregion

        //Die initialiserung des Main-Loops
        #region Init Main-Loop

        //Making an Other Username per Login
        private static void startMainLoop()
        {
            //Get the Computername
            string komplett = Environment.MachineName;

            //Try to get the right Pattern for APG Computers
            if (Regex.IsMatch(komplett, @"[CL][LA][A-Z]{3}\d{9}"))
            {
                //delete everything around the Location în the Computername
                komplett = Regex.Replace(komplett, "^[CL][LA]", "");
                komplett = Regex.Replace(komplett, @"\d{9}$", "");

                //Set Gast to the Parsed location
                Gast = komplett;
            }
            else
            {
                //Most likely UMB so ask witch location they want to make a User
                string response = Interaction.InputBox("Für welchen Standort benötigsts du einen Benutzer?", "Standort", "ZHG");

                //Set Gast to the asked location
                Gast = response;
            }

            //Add a user to The Main Application
            if (Application.OpenForms["Form1"] != null)
            {
                Application.OpenForms["Form1"].Invoke(new Action(() => {

                    (Application.OpenForms["Form1"] as Form1).Adduser();

                }));
            }
        }

        #endregion

        //Angefangener Code
        #region Not Implemented

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
            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            MessageBox.Show(errors);
            MessageBox.Show(results);
        }

        //Not Implemented
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
                    if (antewort == "")
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
    
        #endregion

    }
}