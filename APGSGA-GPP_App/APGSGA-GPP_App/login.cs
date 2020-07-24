//Header
//Verison: 1.0
//Author: Jann Erhardt
//Discription: 
/* 
 * Der Haupt eintritts Punkt des Programms
 * 
 * Funktionen: 
 *      1. Init --> Initialisiert alle Komonenten (Form1 Designer -> Generierter Code)
 *      2. Login-Send --> Speichert die Felder und schickt sie Program.cs, um das zu überprüfen
 *      3. reset --> Wenn das Login Falsch war werden die Text-Felder geleert
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APGSGA_GPP_App
{
    public partial class login : Form
    {
        //Initialisiert das Windows-Form Fenster
        #region Init

        //Initalise the Components
        public login()
        {
            InitializeComponent();
        }

        #endregion

        //Login betätigt Infos weiterleiten
        #region Login-Send

        private void button1_Click(object sender, EventArgs e)
        {
            string password = tB_Pass.Text;
            string username = tB_User.Text;

            Program.validateLogin(password, username);
        }

        private void login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string password = tB_Pass.Text;
                string username = tB_User.Text;

                Program.validateLogin(password, username);
            }
        }

        #endregion

        //Die Felder leeren
        #region reset

        public void resetLogin()
        {
            tB_User.Text = "";
            tB_Pass.Text = "";
            tB_User.Refresh();
            tB_Pass.Refresh();
        }

        #endregion
    }
}
