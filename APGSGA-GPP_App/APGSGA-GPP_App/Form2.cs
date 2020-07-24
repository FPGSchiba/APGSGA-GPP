using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace APGSGA_GPP_App
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        private void dataSet1_Initialized(object sender, EventArgs e)
        {
            try
            {
                //Checking the XML for known Errors
                //Program.checkXML();
                //Program.deleteOldUser();
            }
            catch(Exception E)
            {
                //Error handling while Xml-Error handling
                MessageBox.Show(E.Message);
            }

            //Get the XML-Content and Format and read it correctly
            string temp = Program.getXMLcontent();
            temp = formatXML(temp);
            System.IO.StringReader xml = new System.IO.StringReader(temp);

            try
            {
                //Read the XML inside 
                dataSet1.ReadXml(xml);
            }
            catch(Exception E)
            {
                //Error handling while creating data
                MessageBox.Show(E.Message);
            }
            try
            {
                //Filling the Table
                dataGridView1.DataSource = dataSet1.Tables[0];
            }
            catch
            {
                //Handling Errors with no XML-Data
                MessageBox.Show("Noch keine Zugänge auf diesem PC erstellt");
            }

            try
            {
                //Creating the Delete button
                int columnIndex = 5;
                if (dataGridView1.Columns["Löschen"] == null)
                {
                    dataGridView1.Columns.Insert(columnIndex, Löschen);
                }

                //Creating the Print button
                columnIndex = 6;
                if (dataGridView1.Columns["Print"] == null)
                {

                }
                dataGridView1.Columns.Insert(columnIndex, Print);
            }
            catch
            {

            }
        }

        //Make the Visuals for the User
        static string formatXML(string content)
        {
            string end = "";

            end = Regex.Replace(content, "guest_username", "Benutzernamen");
            end = Regex.Replace(end, "guest_password", "Passwort");
            end = Regex.Replace(end, "creation_date", "Erstellungsdatum");
            end = Regex.Replace(end, "end_date", "Endet_am");
            end = Regex.Replace(end, "start_date", "Beginnt_am");

            return end;
        }

        //Button Handling
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Check if its the right Column
            if (e.ColumnIndex == dataGridView1.Columns["Löschen"].Index)
            {
                //Dobble Check if the User really wants to delete the user
                DialogResult dialogResult = MessageBox.Show("Willst du diesen Benutzer Wirklich Löschen?", "Lösch bestätigung", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //If Yes the right username will be taken from the table
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    string usern = dataGridView1.Rows[e.RowIndex].Cells["Benutzernamen"].Value.ToString();

                    //Then the user will be deleted
                    //Program.deleteUser(usern);

                    //The Form gets closed
                    Form.ActiveForm.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    //Nothing 
                }
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Print"].Index)
            {
                DialogResult dialogResult = MessageBox.Show("Willst du diesen Benutzer wirklich nochmal Drucken?", "Druck bestätigung", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //If Yes the wright username and Password will be taken from the table
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    string usern = dataGridView1.Rows[e.RowIndex].Cells["Benutzernamen"].Value.ToString();
                    string passwd = dataGridView1.Rows[e.RowIndex].Cells["Passwort"].Value.ToString();

                    //Makes a cross-thread action to Print the Document
                    if (Application.OpenForms["Form1"] != null)
                    {
                        Application.OpenForms["Form1"].Invoke(new Action(() => {

                            (Application.OpenForms["Form1"] as Form1).print(usern, passwd);

                        }));
                    }

                }
                else if (dialogResult == DialogResult.No)
                {
                    //Nothing 
                }
            }
        }
    }
}
