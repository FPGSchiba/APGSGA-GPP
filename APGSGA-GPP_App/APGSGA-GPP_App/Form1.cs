using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

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
            this.User_TB.Text = Username;
            this.User_TB.Refresh();
        }

        public void WritePW(string Password)
        {
            this.PW_TB.Text = Password;
            this.PW_TB.Refresh();
        }

        private void User_Gene_Click(object sender, EventArgs e)
        {
            Program.GenerateUser();
        }

        private void PW_Gene_Click(object sender, EventArgs e)
        {
            Program.GeneratePW();
        }

        private void Create_B_Click(object sender, EventArgs e)
        {
            string username = this.User_TB.Text;
            string password = this.PW_TB.Text;
            string dateBis = this.dTP_Bis.Text;
            string dateVon = this.dTP_Von.Text;
            string show = username + " " + password + " " + formatTime(dateBis, true) + " " + formatTime(dateVon, false);
            MessageBox.Show(show);
            
        }

        public static string formatTime(string Time, bool isBis)
        {
            string end = "";

            string tag = Regex.Replace(Time, @"[A-Z]{1}[a-z]*,\s", "");
            tag = Regex.Replace(tag, @"\.\s[A-Z]{1}[a-z]*\s\d{4}", "");

            string monat = Regex.Replace(Time, @"[A-Z]{1}[a-z]*,\s\d{1,2}\.\s", "");
            monat = Regex.Replace(monat, @"\s\d{4}", "");
            monat = formatMonth(monat).ToString();

            string jahr = Regex.Replace(Time, @"[A-Z]{1}[a-z¨]*,\s\d{1,2}\.\s[A-Z]{1}[a-z]*\s", "");

            end = monat + "/" + tag + "/" + jahr + " ";

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
    }
}
