using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ventrum_Keypass
{
    public partial class PasswordForm : Form
    {
        public string Password { get; set; }
        public PasswordForm()
        {
            InitializeComponent();
        }

        private void okbtn_Click(object sender, EventArgs e)
        {
            if (pwdtextbox.Text != string.Empty)
            {
                Password = pwdtextbox.Text;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Please fill in a password to continue");
                DialogResult = DialogResult.TryAgain;
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
