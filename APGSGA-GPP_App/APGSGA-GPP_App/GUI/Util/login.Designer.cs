namespace APGSGA_GPP_App
{
    partial class login
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
            this.tB_User = new System.Windows.Forms.TextBox();
            this.tB_Pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.B_Login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tB_User
            // 
            this.tB_User.Location = new System.Drawing.Point(73, 95);
            this.tB_User.Name = "tB_User";
            this.tB_User.Size = new System.Drawing.Size(183, 20);
            this.tB_User.TabIndex = 0;
            // 
            // tB_Pass
            // 
            this.tB_Pass.Location = new System.Drawing.Point(73, 121);
            this.tB_Pass.MaxLength = 14;
            this.tB_Pass.Name = "tB_Pass";
            this.tB_Pass.PasswordChar = '*';
            this.tB_Pass.Size = new System.Drawing.Size(183, 20);
            this.tB_Pass.TabIndex = 1;
            this.tB_Pass.UseSystemPasswordChar = true;
            this.tB_Pass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 102);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Aruba-Login";
            // 
            // B_Login
            // 
            this.B_Login.Location = new System.Drawing.Point(181, 196);
            this.B_Login.Name = "B_Login";
            this.B_Login.Size = new System.Drawing.Size(75, 23);
            this.B_Login.TabIndex = 5;
            this.B_Login.Text = "Log in";
            this.B_Login.UseVisualStyleBackColor = true;
            this.B_Login.Click += new System.EventHandler(this.button1_Click);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 231);
            this.Controls.Add(this.B_Login);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tB_Pass);
            this.Controls.Add(this.tB_User);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Name = "login";
            this.Text = "Log In";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.login_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tB_User;
        private System.Windows.Forms.TextBox tB_Pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button B_Login;
    }
}