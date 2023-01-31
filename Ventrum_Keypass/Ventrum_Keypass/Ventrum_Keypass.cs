using KeePassLib;
using KeePassLib.Interfaces;
using KeePassLib.Keys;
using KeePassLib.Serialization;
using System.Windows.Forms;

namespace Ventrum_Keypass
{
    public partial class Ventrum_Keypass : Form
    {
        private string databaseFilePath = string.Empty;
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
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "KeyPass Database (*.kdbx)|*.kdbx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string password = PromptForPassword();
                    if (string.IsNullOrEmpty(password))
                    {
                        return;
                    }

                    try
                    {
                        databaseFilePath = saveFileDialog.FileName;
                        PwDatabase database = new PwDatabase();
                        IOConnectionInfo ioc = new IOConnectionInfo
                        {
                            Path = databaseFilePath
                        };
                        CompositeKey compositeKey = new CompositeKey();
                        compositeKey.AddUserKey(new KcpPassword(password));
                        database.New(ioc, compositeKey);
                        IStatusLogger logger = new NullStatusLogger();
                        database.Save(logger);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to create KeyPass database file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
                    databaseFilePath = openFileDialog.FileName;
                    string password = PromptForPassword(); // Implement a method to prompt the user for the password
                    PwDatabase database = Open(databaseFilePath, password);
                    // Continue with the code to manage the opened KeyPass database
                    if (database != null)
                    {
                        var kpdata = from entry in database.RootGroup.GetEntries(true)
                                     select new
                                     {
                                         Title = entry.Strings.ReadSafe("Title"),
                                         UserName = entry.Strings.ReadSafe("UserName"),
                                         Password = entry.Strings.ReadSafe("Password"),
                                         URL = entry.Strings.ReadSafe("URL"),
                                         Notes = entry.Strings.ReadSafe("Notes")
                                     };
                        dataGridView1.DataSource = kpdata.ToList();
                        database.Close();
                    }
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            try
            {
                databaseFilePath = saveFileDialog.FileName;
                PwDatabase database = new PwDatabase();
                IOConnectionInfo ioc = new IOConnectionInfo
                {
                    Path = databaseFilePath
                };
                IStatusLogger logger = new NullStatusLogger();
                database.Save(logger);

            }
            catch(Exception ex)
            {
                MessageBox.Show("Failed to save KeyPass database file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveFileAsHandler(object sender, EventArgs e)
        {
            string password = string.Empty;
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "KeePass Database (*.kdbx)|*.kdbx|All Files (*.*)|*.*",
                    FilterIndex = 1,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string databaseFilePath = saveFileDialog.FileName;
                    using (PasswordForm pwdform = new PasswordForm())
                    {
                        password = pwdform.Password;
                    }
                    PwDatabase database = Open(databaseFilePath, password);
                    if(database != null)
                    {
                        IOConnectionInfo ioc = new IOConnectionInfo();
                        ioc.Path = databaseFilePath;
                        IStatusLogger logger = new NullStatusLogger();
                        database.SaveAs(ioc, true, logger);
                        MessageBox.Show("KeyPass database file saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Failed to save KeyPass database file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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