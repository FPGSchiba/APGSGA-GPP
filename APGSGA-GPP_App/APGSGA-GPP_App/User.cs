//Header
//Verison: 1.0
//Author: Jann Erhardt
//Discription: 
/* 
 * Sammlung von Daten, um alles zu Strukturieren
 * 
 * Funktionen: 
 *      1. Variable generation --> Erstellt den Benutzer (Benutzernamen & Passwort)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APGSGA_GPP_App
{
    class User
    {

        //Alle Variablen
        #region Variablen

        public string Username = CreateUsername();
        public string Password = CreatePW();

        #endregion

        //Die Erstellung der Variablen
        #region Variable generation

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
        static string CreateUsername()
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
            foreach (string i in vs)
            {
                end += Int32.Parse(i).ToString("D2");
            }

            //Format String
            end = Program.Gast + end;
            return end;
        }

        #endregion

    }
}
