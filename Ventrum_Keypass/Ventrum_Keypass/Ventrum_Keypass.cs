using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Dynamic;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using KeePassLib;
using KeePassLib.Cryptography.PasswordGenerator;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using Microsoft.VisualBasic.ApplicationServices;

namespace Ventrum_Keypass
{
    public partial class Ventrum_Keypass : Form
    {
        private string _currentSaveFilePath = string.Empty;
        //private Dictionary<string, List<>
        public Ventrum_Keypass()
        {
            InitializeComponent();
            InitializeMainView();
        }

        public static PwDatabase Open(string dbFilePath, string pwd)
        {
            var ioconnection = new IOConnectionInfo
            {
                Path = dbFilePath
            };
            var compositeKey = new CompositeKey();
            compositeKey.AddUserKey(new KcpPassword(pwd));

            var database = new PwDatabase();
            database.Open(ioconnection, compositeKey, null);

            return database;
        }

        private void InitializeMainView()
        {
            var menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("File");
            var groupMenu = new ToolStripMenuItem("Group");
            var entryMenu = new ToolStripMenuItem("Entry");
            var findMenu = new ToolStripMenuItem("Find");

            fileMenu.DropDownItems.Add("New", null, NewFileHandler);
            fileMenu.DropDownItems.Add("Open", null, OpenFileHandler);
            fileMenu.DropDownItems.Add("Save", null, SaveFileHandler);
            fileMenu.DropDownItems.Add("Save As", null, SaveFileAsHandler);
            fileMenu.DropDownItems.Add("Close", null, CloseFileHandler);
            fileMenu.DropDownItems.Add("Exit", null, ExitHandler);

            groupMenu.DropDownItems.Add("Add Group", null, AddGroupHandler);
            groupMenu.DropDownItems.Add("Edit Group", null, EditGroupHandler);
            groupMenu.DropDownItems.Add("Delete Group", null, DeleteGroupHandler);

            entryMenu.DropDownItems.Add("Add Entry", null, AddEntryHandler);
            entryMenu.DropDownItems.Add("Edit Entry", null, EditEntryHandler);
            entryMenu.DropDownItems.Add("Delete Entry", null, DeleteEntryHandler);

            findMenu.DropDownItems.Add("Find", null, FindHandler);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(groupMenu);
            menuStrip.Items.Add(entryMenu);
            menuStrip.Items.Add(findMenu);

            Controls.Add(menuStrip);


        }

        private void NewFileHandler(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "KeyPass Database (*.kdbx)|*.kdbx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string databaseFilePath = saveFileDialog.FileName;
                    string password = PromptForPassword(); // Prompt the user for the password
                    PwDatabase database = new PwDatabase();
                    IOConnectionInfo ioc = new IOConnectionInfo();
                    ioc.Path = databaseFilePath;
                    database.New(ioc, new CompositeKey(new KcpPassword(password)));
                    database.Save(ioc);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create KeyPass database file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    private void OpenFileHandler(object sender, EventArgs e)
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "KeyPass Database (*.kdbx)|*.kdbx";
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                string databaseFilePath = openFileDialog.FileName;
                string password = PromptForPassword(); // Implement a method to prompt the user for the password
                PwDatabase database = Open(databaseFilePath, password);
                // Continue with the code to manage the opened KeyPass database
                }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open KeyPass database file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }
        }

        private string PromptForPassword()
        {
            using (PasswordForm passwordPrompt = new PasswordForm())
            {
                if (passwordPrompt.ShowDialog() == DialogResult.OK)
                {
                    return passwordPrompt.Password;
                }
            }
            return null;
        }

        private void SaveFileHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Save File");
        }

        private void SaveFileAsHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Save File As");
        }

        private void CloseFileHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Close File");
        }

        private void ExitHandler(object sender, EventArgs e)
        {
            Close();
        }

        private void AddGroupHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Add Group");
        }

        private void EditGroupHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Edit Group");
        }

        private void DeleteGroupHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Delete Group");
        }

        private void AddEntryHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Add Entry");
        }

        private void EditEntryHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Edit Entry");
        }

        private void DeleteEntryHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Delete Entry");
        }

        private void FindHandler(object sender, EventArgs e)
        {
            MessageBox.Show("Find");
        }
    }
}