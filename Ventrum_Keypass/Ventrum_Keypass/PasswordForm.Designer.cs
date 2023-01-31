namespace Ventrum_Keypass
{
    partial class PasswordForm
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
            this.okbtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.pwdtextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // okbtn
            // 
            this.okbtn.Location = new System.Drawing.Point(194, 449);
            this.okbtn.Name = "okbtn";
            this.okbtn.Size = new System.Drawing.Size(188, 58);
            this.okbtn.TabIndex = 0;
            this.okbtn.Text = "OK";
            this.okbtn.UseVisualStyleBackColor = true;
            this.okbtn.Click += new System.EventHandler(this.okbtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(607, 449);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(188, 58);
            this.cancelBtn.TabIndex = 1;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // pwdtextbox
            // 
            this.pwdtextbox.Location = new System.Drawing.Point(378, 372);
            this.pwdtextbox.Name = "pwdtextbox";
            this.pwdtextbox.PasswordChar = '*';
            this.pwdtextbox.Size = new System.Drawing.Size(250, 47);
            this.pwdtextbox.TabIndex = 2;
            this.pwdtextbox.Text = "Password";
            this.pwdtextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 41F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 630);
            this.Controls.Add(this.pwdtextbox);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.okbtn);
            this.Name = "PasswordForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button okbtn;
        private Button cancelBtn;
        private TextBox pwdtextbox;
    }
}