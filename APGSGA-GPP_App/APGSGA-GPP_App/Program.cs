/* 
Version: 1.0
Build: 1.0
Author: Jann Erhardt
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace APGSGA_GPP_App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static string password = "";
        public static string username = "";

        [STAThread]
        static void Main()
        {
            Thread thread = new Thread(MainLoop);
            thread.Start();
            Thread.Sleep(1000);
            GenerateUser();
            GeneratePW();
        }

        public static void GenerateUser()
        {
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
            password = CreatePW();
            if (Application.OpenForms["Form1"] != null)
            {
                Application.OpenForms["Form1"].Invoke(new Action(() => {

                    (Application.OpenForms["Form1"] as Form1).WritePW(password);

                }));
            }
        }

        static void TestOutputs()
        {
            MessageBox.Show(CreateUsername());
            MessageBox.Show(CreatePW());
        }

        static void MainLoop()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static string CreatePW(int length = 8)
        {
            string end = "";
            length = length / 2;
            Random random = new Random();
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

        static string CreateUsername()
        {
            string end = "";
            string[] vs = new string[3];
            string temp = DateTime.Now.ToShortDateString();
            vs = temp.Split('.');
            string year = vs[2];
            char[] temps = year.ToCharArray();
            year = temps[2].ToString() + temps[3].ToString();
            vs[2] = year;
            foreach(string i in vs)
            {
                end += i;
            }
            end = "Gast" + end;
            return end;
        }
    }
}
