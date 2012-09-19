//Title :           Petunia Startup Window.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All startup activates are here with animations But this service is not available this time.  
using System;
using System.Windows;
using ProcestaVariables;
using MysqlClass;
using ProcestaAccountingFunction;
using MysqlDataBackup;
using System.Threading;
using System.ComponentModel;
namespace Procesta_Petunia
{
    public partial class WindowStartUp : Window
    {
        #region Variables
        private BackgroundWorker stratupBackgroundWorker = new BackgroundWorker();
        private bool flagDatabaseError = false;
        #endregion

        public WindowStartUp()
        {
            InitializeComponent();
            stratupBackgroundWorker.WorkerReportsProgress = true;
            stratupBackgroundWorker.DoWork += new DoWorkEventHandler(startupWorker);
            stratupBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(strtaupProcess);
            stratupBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(strtaCompleate);
            stratupBackgroundWorker.RunWorkerAsync();
        }

        private void startupWorker(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(800);
            stratupWindowLogingMessages.AnimationText = "Connecting....";
            stratupBackgroundWorker.ReportProgress(1);
            Thread.Sleep(1000);
            MySqlNaceassaryElement.DatabaseConnectionInfo[0] = Properties.Settings.Default.DatabaseServerip;
            MySqlNaceassaryElement.DatabaseConnectionInfo[1] = Properties.Settings.Default.DatabasePortNumber;
            MySqlNaceassaryElement.DatabaseConnectionInfo[2] = "petunia2.0";
            MySqlNaceassaryElement.DatabaseConnectionInfo[3] = Properties.Settings.Default.DatabaseUserName;
            MySqlNaceassaryElement.DatabaseConnectionInfo[4] = Properties.Settings.Default.DatabasePassword;
           bool databaseConnectionTest;
            try
            {
                 databaseConnectionTest = new MySqlNaceassaryElement().ConnectToMysql(Properties.Settings.Default.DarabaseDefultDatabase, Properties.Settings.Default.DatabaseServerip, Properties.Settings.Default.DatabasePortNumber, Properties.Settings.Default.DatabaseUserName, Properties.Settings.Default.DatabasePassword);
            }
            catch
            {
                databaseConnectionTest = false;
            }
            if (databaseConnectionTest)
            {
                stratupBackgroundWorker.ReportProgress(2);
                Thread.Sleep(1000);
                stratupWindowLogingMessages.AnimationText = "Loading....";
                stratupBackgroundWorker.ReportProgress(3);
                Thread.Sleep(2000);
                stratupBackgroundWorker.ReportProgress(4);
                Thread.Sleep(500);
                stratupBackgroundWorker.ReportProgress(5);
                Thread.Sleep(600);
            }
            else
            {
                stratupBackgroundWorker.ReportProgress(6);
            }
        }
        private void strtaupProcess(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage.Equals(1))
            {
                stratupWindowLogingMessages.AnimationArrivalBegin = true;
            }
            else if (e.ProgressPercentage.Equals(2))
            {
                stratupWindowLogingMessages.AnimationLeaveBegin = true;
            }
            else if (e.ProgressPercentage.Equals(3))
            {
                stratupWindowLogingMessages.AnimationArrivalBegin = true;
            }
            else if (e.ProgressPercentage.Equals(4))
            {
                MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
                MysqlBackupVariablesClass.MYSQL_CONNECTION_STRING[0] = Properties.Settings.Default.DatabaseServerip;
                MysqlBackupVariablesClass.MYSQL_CONNECTION_STRING[1]=Properties.Settings.Default.DatabasePortNumber;
                MysqlBackupVariablesClass.MYSQL_CONNECTION_STRING[2] = Properties.Settings.Default.DarabaseDefultDatabase;
                MysqlBackupVariablesClass.MYSQL_CONNECTION_STRING[3]=Properties.Settings.Default.DatabaseUserName;
                MysqlBackupVariablesClass.MYSQL_CONNECTION_STRING[4]=Properties.Settings.Default.DatabasePassword;
                new MysqlDataBackupRestoreClass().DataBaseRestore("Petunia2.0");
                new Variables().InitializeLoaclData();
                new AccountClass().StartupDataBaseChecker(new DBQueres(mysqlNecessaryFunction.DatabaseOperation), new DatabaseRead(mysqlNecessaryFunction.DataReader), new MysqlRowCounter(mysqlNecessaryFunction.MysqlRowsIs));
            }
            else if (e.ProgressPercentage.Equals(5))
            {
                stratupWindowLogingMessages.AnimationLeaveBegin = true;
            }
            else if (e.ProgressPercentage.Equals(6))
            {
                flagDatabaseError = true;
                Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0, 1], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
                new WindowDataBaseSetting().ShowDialog();
                this.Close();
            }
        }
        private void strtaCompleate(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!flagDatabaseError)
            {
                new MainWindow().Show();
                this.Close();
            }
        }
    }
}
