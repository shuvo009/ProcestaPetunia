//Title :           Petunia Database Setting.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     DataTabe contains are input here
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using ProcestaNecessaryFunction;
using MysqlClass;
using ProcestaVariables;
namespace Procesta_Petunia
{
    public partial class WindowDataBaseSetting : Window
    {
        public WindowDataBaseSetting()
        {
            InitializeComponent();
          //  this.databaseSettingTitel.CurrentWindow=App.Current.Windows[1];
            this.DatabaseSettingtextboxUsername.Text = MysqlClass.Properties.Settings.Default.username;
            this.DatabaseSettingtextboxHostIP.Text = MysqlClass.Properties.Settings.Default.serverip;
            this.DatabaseSettingtextboxPortNumber.Text = MysqlClass.Properties.Settings.Default.port.ToString() ;
            this.DatabaseSettingtextboxDatabaseName.Text = MysqlClass.Properties.Settings.Default.defultDatabase;

        }

        #region Button Click
        //check database string
        private void DatabaseSettingConnectButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
            NecessaryFunction necessaryElement = new NecessaryFunction();
            this.DatabaseSettingConnectButton.IsEnabled = false;
            if (DatabaseSettingtextboxUsername.Text != string.Empty && DatabaseSettingtextboxPassword.Password != string.Empty && DatabaseSettingtextboxConformPassword.Password != string.Empty && DatabaseSettingtextboxHostIP.Text != string.Empty && DatabaseSettingtextboxPortNumber.Text != string.Empty && DatabaseSettingtextboxPortNumber.Text != string.Empty && DatabaseSettingtextboxDatabaseName.Text != string.Empty && necessaryElement.PasswordIs(DatabaseSettingtextboxPassword.Password, DatabaseSettingtextboxConformPassword.Password))
            {
                if (mysqlNecessaryFunction.ConnectToMysql(this.DatabaseSettingtextboxDatabaseName.Text,this.DatabaseSettingtextboxHostIP.Text,this.DatabaseSettingtextboxPortNumber.Text,this.DatabaseSettingtextboxUsername.Text,this.DatabaseSettingtextboxConformPassword.Password))
                {
                    Properties.Settings.Default.DatabaseServerip = this.DatabaseSettingtextboxHostIP.Text;
                    Properties.Settings.Default.DatabasePortNumber = this.DatabaseSettingtextboxPortNumber.Text;
                    Properties.Settings.Default.DarabaseDefultDatabase = this.DatabaseSettingtextboxDatabaseName.Text;
                    Properties.Settings.Default.DatabaseUserName = this.DatabaseSettingtextboxUsername.Text;
                    Properties.Settings.Default.DatabasePassword = DatabaseSettingtextboxConformPassword.Password;
                    Properties.Settings.Default.Save();
                    Microsoft.Windows.Controls.MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0, 8], ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
                    System.Windows.Forms.Application.Restart();
                    Application.Current.Shutdown();

                }
                else
                {
                    Microsoft.Windows.Controls.MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0, 1], ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
                    this.DatabaseSettingConnectButton.IsEnabled = true;
                }

            }
            else
            {
                Microsoft.Windows.Controls.MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0, 1] + Environment.NewLine + ProcestaVariables.Variables.ERROR_MESSAGES[0, 7], ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
                this.DatabaseSettingConnectButton.IsEnabled = true;
            }
        }
        //Button Exit Click
        private void DatabaseSettingButtonExitClick(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult messBoxResult = new MessageBoxResult();
            messBoxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 1], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messBoxResult.Equals(MessageBoxResult.Yes))
            {
                Application.Current.Shutdown();
            }
        }
        #endregion

        #region TextBox
        //Conform Password Change
        private void conformPasswordChange(object sender, System.Windows.RoutedEventArgs e)
        {
            NecessaryFunction necessaryElement = new NecessaryFunction();
            if (DatabaseSettingtextboxPassword.Password.Length.Equals(DatabaseSettingtextboxConformPassword.Password.Length))
            {
                if (necessaryElement.PasswordIs(DatabaseSettingtextboxPassword.Password, DatabaseSettingtextboxConformPassword.Password))
                {
                    DatabaseSettingPasswordError.Foreground=Brushes.ForestGreen;
                    DatabaseSettingPasswordError.Text=Variables.ERROR_MESSAGES[1,2];
                }
                else
                {
                    DatabaseSettingPasswordError.Foreground = Brushes.Red;
                    DatabaseSettingPasswordError.Text = Variables.ERROR_MESSAGES[0, 6];
                }
            }
            else
            {
                DatabaseSettingPasswordError.Text = string.Empty;
            }
        }
        //Password Password Change
        private void passwordPasswordChage(object sender, System.Windows.RoutedEventArgs e)
        {
            DatabaseSettingtextboxConformPassword.Password = string.Empty;
            DatabaseSettingPasswordError.Text = string.Empty;
        }
        //Conform Password KetDown
        private void DatabaseSettingtextboxConformPasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                DatabaseSettingConnectButtonClick(null,null);
            }
        }
        //Database setting Database Name
        private void DatabaseSettingtextboxDatabaseNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                DatabaseSettingConnectButtonClick(null,null);
            }
        }
        #endregion

        #region Protected Class
        //Window On closing
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messBoxResult = new MessageBoxResult();
            messBoxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 1], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messBoxResult.Equals(MessageBoxResult.Yes))
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion
    }
}
