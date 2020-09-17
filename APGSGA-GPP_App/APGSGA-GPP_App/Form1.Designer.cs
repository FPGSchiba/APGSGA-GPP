namespace APGSGA_GPP_App
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.User_TB = new System.Windows.Forms.TextBox();
            this.PW_TB = new System.Windows.Forms.TextBox();
            this.User_Gene = new System.Windows.Forms.Button();
            this.PW_Gene = new System.Windows.Forms.Button();
            this.dTP_Von = new System.Windows.Forms.DateTimePicker();
            this.dTP_Bis = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Create_B = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // User_TB
            // 
            this.User_TB.Location = new System.Drawing.Point(74, 85);
            this.User_TB.Name = "User_TB";
            this.User_TB.Size = new System.Drawing.Size(200, 20);
            this.User_TB.TabIndex = 0;
            // 
            // PW_TB
            // 
            this.PW_TB.Location = new System.Drawing.Point(74, 124);
            this.PW_TB.Name = "PW_TB";
            this.PW_TB.Size = new System.Drawing.Size(200, 20);
            this.PW_TB.TabIndex = 1;
            // 
            // User_Gene
            // 
            this.User_Gene.Location = new System.Drawing.Point(280, 85);
            this.User_Gene.Name = "User_Gene";
            this.User_Gene.Size = new System.Drawing.Size(75, 23);
            this.User_Gene.TabIndex = 4;
            this.User_Gene.Text = "Generate";
            this.User_Gene.UseVisualStyleBackColor = true;
            this.User_Gene.Click += new System.EventHandler(this.User_Gene_Click);
            // 
            // PW_Gene
            // 
            this.PW_Gene.Location = new System.Drawing.Point(280, 122);
            this.PW_Gene.Name = "PW_Gene";
            this.PW_Gene.Size = new System.Drawing.Size(75, 23);
            this.PW_Gene.TabIndex = 5;
            this.PW_Gene.Text = "Generate";
            this.PW_Gene.UseVisualStyleBackColor = true;
            this.PW_Gene.Click += new System.EventHandler(this.PW_Gene_Click);
            // 
            // dTP_Von
            // 
            this.dTP_Von.Location = new System.Drawing.Point(74, 190);
            this.dTP_Von.Name = "dTP_Von";
            this.dTP_Von.Size = new System.Drawing.Size(200, 20);
            this.dTP_Von.TabIndex = 8;
            this.dTP_Von.ValueChanged += new System.EventHandler(this.dTP_Von_ValueChanged);
            // 
            // dTP_Bis
            // 
            this.dTP_Bis.Location = new System.Drawing.Point(74, 229);
            this.dTP_Bis.Name = "dTP_Bis";
            this.dTP_Bis.Size = new System.Drawing.Size(200, 20);
            this.dTP_Bis.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Gast WLAN zugang erstellen";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Von";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 229);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Bis";
            // 
            // Create_B
            // 
            this.Create_B.Location = new System.Drawing.Point(257, 264);
            this.Create_B.Name = "Create_B";
            this.Create_B.Size = new System.Drawing.Size(98, 23);
            this.Create_B.TabIndex = 15;
            this.Create_B.Text = "Create and Print";
            this.Create_B.UseVisualStyleBackColor = true;
            this.Create_B.Click += new System.EventHandler(this.Create_B_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 299);
            this.Controls.Add(this.Create_B);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dTP_Bis);
            this.Controls.Add(this.dTP_Von);
            this.Controls.Add(this.PW_Gene);
            this.Controls.Add(this.User_Gene);
            this.Controls.Add(this.PW_TB);
            this.Controls.Add(this.User_TB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Gast Zugang";
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox User_TB;
        private System.Windows.Forms.TextBox PW_TB;
        private System.Windows.Forms.Button User_Gene;
        private System.Windows.Forms.Button PW_Gene;
        private System.Windows.Forms.DateTimePicker dTP_Von;
        private System.Windows.Forms.DateTimePicker dTP_Bis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Create_B;
        private System.Diagnostics.EventLog eventLog1;
    }
}

