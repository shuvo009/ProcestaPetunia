//Title :           Petunia User Login.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     User Can Login here but this service is not available this time.  
using System;
using System.Windows;
using System.Windows.Input;
using ProcestaVariables;
namespace Procesta_Petunia.Class
{ 
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();
			this.TitleBarLoginWindow.CurrentWindow=App.Current.Windows[1];
        }
        #region Button Event
        //LoginButton Click
        private void LiginButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.loginButton.IsEnabled = false;
            if (!this.loginPasswordBox.Password.Equals(string.Empty))
            {
                if (this.loginPasswordBox.Password.Equals(Properties.Settings.Default.LoginPassword))
                {
                    MainWindow.timerLoginCheck.IsEnabled = true;
                    Variables.LOGIN_IS = false;
                    this.Close();
                }
                else
                {
                    Microsoft.Windows.Controls.MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0, 6], ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
                    this.loginPasswordBox.Password = string.Empty;
                    this.loginButton.IsEnabled = true;
                }

            }
            else
            {
                Microsoft.Windows.Controls.MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0, 5], ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
                this.loginButton.IsEnabled = true;
            }
        }
        #endregion

        #region TextBox Event
        private void loginPasswordBoxKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                LiginButtonClick(null, null);
            }
        }
        #endregion
    }
}
