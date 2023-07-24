﻿using pwdvault.Modeles;
using pwdvault.Services;
using pwdvault.Services.Exceptions;
using pwdvault.Controllers;
using Serilog;

namespace pwdvault.Forms
{
    public partial class Password : UserControl
    {
        private string _appName = "";
        private string _username = "";
        private string _iconName = "";

        // Define the event to raise after editing or deleting a password.
        public event EventHandler? PasswordEditedOrDeleted;

        public string AppName
        {
            get { return _appName; }
            set { _appName = value; lbApp.Text = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; lbUser.Text = value; }
        }
        public string IconName
        {
            get { return _iconName; }
            set { _iconName = value; pictureBoxApp.Image = (Image?)Properties.Resources.ResourceManager.GetObject(value); }
        }


        public Password(string appName, string username, string iconName)
        {
            InitializeComponent();
            AppName = appName;
            Username = username;
            IconName = iconName;
        }

        private void Password_MouseEnter(object sender, EventArgs e)
        {
            BackColor = SystemColors.InactiveBorder;
        }

        private void Password_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                new EditPassword(AppName, Username).ShowDialog();
                OnPasswordEditedOrDeleted();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occured. Please try again later or contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Logger.Error("\nSource : " + ex.Source + "\nMessage : " + ex.Message);
            }

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Are you sure to delete {AppName} password?", "Delete confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using var context = new PasswordVaultContext();
                    var passwordController = new PasswordController(context);
                    var password = passwordController.GetPassword(AppName, Username);
                    passwordController.DeletePassword(password.Id);

                    var vaultController = VaultController.GetInstance();
                    vaultController.DeleteEncryptionKey(AppName);

                    OnPasswordEditedOrDeleted();
                }
                catch (PasswordNotFoundException ex)
                {
                    MessageBox.Show("An unexpected error occured. Please try again later or contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Log.Logger.Error("\nSource : " + ex.Source + "\nMessage : " + ex.Message);
                }
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                using var context = new PasswordVaultContext();
                var passwordController = new PasswordController(context);
                var password = passwordController.GetPassword(AppName, Username);

                var vaultController = VaultController.GetInstance();
                Clipboard.SetText(EncryptionService.DecryptPassword(password.Password, vaultController.GetEncryptionKey(AppName)));
                ClearClipboardDelayed();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occured. Please try again later or contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Logger.Error("\nSource : " + ex.Source + "\nMessage : " + ex.Message);
            }
        }

        /// <summary>
        /// <para>
        /// This method creates a new thread that waits for 10 seconds and then clear the clipboard.
        /// The apartment state of the thread is set to STA (single-threaded apartment) as to interact with only one thread, the clipboard one.
        /// </para>
        /// </summary>
        private static void ClearClipboardDelayed()
        {
            try
            {
                var thread = new Thread(() =>
                {
                    // Wait for 10 seconds
                    Thread.Sleep(10000);

                    // Clear the clipboard
                    Clipboard.Clear();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occured. Please try again later or contact the administrator.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Logger.Error("\nSource : " + ex.Source + "\nMessage : " + ex.Message);
            }
        }

        /// <summary>
        /// Function responsible for raising an event after editing or deleting a password.
        /// </summary>
        protected virtual void OnPasswordEditedOrDeleted()
        {
            PasswordEditedOrDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
