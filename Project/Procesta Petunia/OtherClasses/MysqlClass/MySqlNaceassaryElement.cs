using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using ProcestaVariables;
namespace MysqlClass
{
    public class MySqlNaceassaryElement
    {
        public MySqlConnection MysqlDBConnection;
        //connect to mysql Database with parameter
        public bool ConnectToMysql(string databaseName,string serverIP,string portNumber, string userId,string Password)
        {
            try
            {
                MysqlDBConnection = new MySqlConnection(string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};",serverIP,portNumber, databaseName, userId, Password));
                MysqlDBConnection.Open();
                if (MysqlDBConnection.State.Equals(ConnectionState.Open))
                {
                    Variables.DATABASE_LOGIN_INFO.SetValue(serverIP, 0);
                    Variables.DATABASE_LOGIN_INFO.SetValue(portNumber, 1);
                    Variables.DATABASE_LOGIN_INFO.SetValue(databaseName, 2);
                    Variables.DATABASE_LOGIN_INFO.SetValue(userId, 3);
                    Variables.DATABASE_LOGIN_INFO.SetValue(Password, 4);
                }
                MysqlDBConnection.Close();
                MysqlDBConnection.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //connect to mysql Database without parameter
        public bool ConnectToMysql()
        {
            try
            {
                MysqlDBConnection = new MySqlConnection(string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4};", Variables.DATABASE_LOGIN_INFO[0], Variables.DATABASE_LOGIN_INFO[1], Variables.DATABASE_LOGIN_INFO[2], Variables.DATABASE_LOGIN_INFO[3], Variables.DATABASE_LOGIN_INFO[4]));
                MysqlDBConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Database Operation
        public bool DatabaseOperation(string query)
        {
            try
            {
                ConnectToMysql();
                MySqlCommand operationCommand = new MySqlCommand(query, MysqlDBConnection);
                operationCommand.ExecuteNonQuery();
                operationCommand.Dispose();
                MysqlDBConnection.Close();
                MysqlDBConnection.Dispose();
                return true;
            }
            catch
            {
                return false;
            } 
        }
        // DataReader
        public DataTable DataReader(string query)
        {
            DataTable mysqlDataTable = new DataTable();
            try
            {
                ConnectToMysql();
                MySqlDataAdapter readerAdapter = new MySqlDataAdapter(query, MysqlDBConnection);
                readerAdapter.Fill(mysqlDataTable);
                readerAdapter.Dispose();
                MysqlDBConnection.Close();
                MysqlDBConnection.Dispose();
                return mysqlDataTable;
            }
            catch
            {
                return mysqlDataTable;
            }
        }
        //Mysql Row Counter
        public decimal MysqlRowsIs(string tableName)
        {
            try
            {
                ConnectToMysql();
                MySqlCommand dataCommand = new MySqlCommand(string.Format("SELECT COUNT(*) FROM {0}",tableName), MysqlDBConnection);
                decimal rowsNumber = Convert.ToDecimal(dataCommand.ExecuteScalar());
                dataCommand.Dispose();
                MysqlDBConnection.Close();
                MysqlDBConnection.Dispose();
                return rowsNumber;
            }
            catch (Exception)
            {

                return -1;
            }
        }
        //Login Checker
        public bool LoginChecker(string query)
        {
            if (MysqlValueChecker(query))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Product ID
        public string ProductIDIs(string tableName,string colunmName)
        {
            decimal prductID=1;
            prductID+=MysqlRowsIs(tableName);
            for (; ; )
            {
                if (MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}={2}", tableName, colunmName, prductID)))
                {
                    char[] idContains = Convert.ToString(prductID).ToCharArray();
                    if (idContains.Length <= 6)
                    {
                        return prductID.ToString("000000");
                    }
                    else
                    {
                        return prductID.ToString();
                    }
                }
                else
                {
                    prductID++;
                }
            }
           
            
        }
        //Check a Value Exit or not
        public bool MysqlValueChecker(string query)
        {
            DataTable mysqlDataTable = new DataTable();
            mysqlDataTable = DataReader(query);
            if (mysqlDataTable.Rows.Count.Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Last Row Checker
        private decimal LastRowValue(string tableName, string columnName)
        {
            decimal tableRowsIs = MysqlRowsIs(tableName);
            if (tableRowsIs.Equals(0))
            {
                tableRowsIs = 1;
            }
            DataTable tempDataTable = new DataTable();
            tempDataTable = DataReader(string.Format("SELECT {0} FROM {1} LIMIT {2},{3}",columnName,tableName,tableRowsIs-1,tableRowsIs));
            if (tempDataTable.Rows.Count > 0)
            {
                DataRow tempDataRow = tempDataTable.Rows[0];
                return Convert.ToDecimal(tempDataRow[0]);
            }
            else
            {
                return 1;
            }
        }
        //Invoice Number
        public string InvoiceNumber(string tableName,string columnName)
        {
            decimal lastInvoiceNumber=1;
            lastInvoiceNumber += LastRowValue(tableName, columnName);
            for (; ;)
            {
                if (MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}={2}", tableName, columnName, lastInvoiceNumber)))
                {
                    char[] invoiceContain = Convert.ToString(lastInvoiceNumber).ToCharArray();
                    if (invoiceContain.Length <= 6)
                    {
                        return lastInvoiceNumber.ToString("000000");
                    }
                    else
                    {
                        return lastInvoiceNumber.ToString();
                    }
                }
                else
                {
                    lastInvoiceNumber++;
                }
            }
        }
        //Database Checker
        public bool DatabaseChecker()
        {
            try
            {
                foreach (string mysqlTableStatement in Variables.MYSQL_TABLE_SQL_STATEMENT)
                {
                    DatabaseOperation(mysqlTableStatement);
                    Variables.DATABASE_LOGIN_INFO[2] = "petunia ";
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
