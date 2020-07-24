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
        public login()
        {
            InitializeComponent();
        }

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

        public void resetLogin()
        {
            tB_User.Text = "";
            tB_Pass.Text = "";
            tB_User.Refresh();
            tB_Pass.Refresh();
        }
    }
}
