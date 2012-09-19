//Title :           Accounting Calculation.
//Version :         2.0.0.3
//Copyright :       Copyright (c) 2010
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All Kind Accounting.  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
 
namespace ProcestaAccountingFunction
{
    public class AccountingFunctionsClass
    {
        public static AccountingDataSet thisDataSet = new AccountingDataSet(); // DataSet for Accounting Class.

        public delegate bool DBQueres(string query); //insert into DataBase 
        public delegate DataTable DatabaseRead(string query); // Read data from database and return dataTable
        public delegate decimal MysqlRowCounter(string tableName); // return total row of a table
       
        public enum TransactionType {Cash,check,Credit,debt,bank} //
        private enum ProductOperationType {Purchase,Sales,PurchaseReturn,SalesReturn}
        public enum ExpanceType {Salary,Rent,OfficeExpance,Stationary,Others}
        
        //Startup checker
        public static bool StartupDataBaseChecker(DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                DataTable errorCodeDataTable = new DataTable();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}='Profit'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[0][0],AccountingVariables.PETUNIA_DATE, thisDataSet.ThisColumn[1][0]));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INRO {0} VALUES('{1}','Profit',0,0,0,0", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE));
                }
                errorCodeDataTable.Clear();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}='Salary'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[0][0], AccountingVariables.PETUNIA_DATE, thisDataSet.ThisColumn[1][0]));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INRO {0} VALUES('{1}','Salary',0,0,0,0", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE));
                }
                errorCodeDataTable.Clear();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}='Rent'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[0][0], AccountingVariables.PETUNIA_DATE, thisDataSet.ThisColumn[1][0]));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INRO {0} VALUES('{1}','Rent',0,0,0,0", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE));
                }
                errorCodeDataTable.Clear();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}='OfficeExpance'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[0][0], AccountingVariables.PETUNIA_DATE, thisDataSet.ThisColumn[1][0]));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INRO {0} VALUES('{1}','OfficeExpance',0,0,0,0", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE));
                }
                errorCodeDataTable.Clear();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='Stationary'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[0][0], AccountingVariables.PETUNIA_DATE, thisDataSet.ThisColumn[1][0]));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INRO {0} VALUES('{1}','Stationary',0,0,0,0", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE));
                }
                errorCodeDataTable.Clear();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}={2} AND {3}='Others'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[0][0], AccountingVariables.PETUNIA_DATE, thisDataSet.ThisColumn[1][0]));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INRO {0} VALUES('{1}','Others',0,0,0,0", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE));
                }
                errorCodeDataTable.Clear();
                errorCodeDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", thisDataSet.ThisTables[6][0], thisDataSet.ThisColumn[0][0], AccountingVariables.PETUNIA_DATE));
                if (errorCodeDataTable.Rows.Count.Equals(0))
                {
                    DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}',0,0,0,0,0)", thisDataSet.ThisTables[6][0], AccountingVariables.PETUNIA_DATE));
                }

                return true;
            }
            catch (Exception)
            {
                
                return false;
            }
        }
        //Return Last Rows Value
        private double LastRowVaue(string columnName, string tableName,DatabaseRead DataReader,MysqlRowCounter Tablerowcount)
        {
            DataTable getDataTable = new DataTable();
            decimal rownumbers = Tablerowcount(tableName);
            if (rownumbers.Equals(0))
            {
                rownumbers = 1;
            }
            getDataTable= DataReader(string.Format("SELECT {0} FROM {1} LIMIT {2},{3}",columnName,tableName,rownumbers-1,rownumbers));
            if (getDataTable.Rows.Count > 0)
            {
                return Convert.ToDouble(getDataTable.Rows[0][getDataTable.Columns[0].ToString()]);
            }
            else
            {
                return 0.00;
            }
        }
        //Product Rate Calculate
        private double ProductRateIs(string Amount, string quantity, string productID, DatabaseRead DataReader)
        {
            DataTable getDataTable = new DataTable();
            getDataTable = DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", thisDataSet.ThisTables[22][0].ToString(), thisDataSet.ThisColumn[10][0].ToString(), productID));
            DataRow getRowData = getDataTable.Rows[0];
            return Convert.ToDouble(((Convert.ToDouble(getRowData[1])+Convert.ToDouble(Amount))/(Convert.ToDouble(getRowData[0])+Convert.ToDouble(quantity))).ToString(".00"));
        }
        //Insert & Update Cash debt or credits
        private bool CashOperation(DataTable transfarData, TransactionType transactionType, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)                                                                                                                                                                                                                                                                  //Column : 0 Petunia_Date                
        {                                                                                                                                                                                                                                                                                                                                                                                                                                   //Column : 1 Description                 
            try                                                                                                                                                                                                                                                                                                                                                                                                                             //Column : 2 Credit Taka or Debt Taka    
            {                                                                                                                                                                                                                                                                                                                                                                                                                               //Column :                
                DataRow transfarDataRow = transfarData.Rows[0];                                                                                                                                                                                                                                                                                                                                                                                             
                if (transactionType.Equals(TransactionType.debt))                                                                                                                                                                                                                                                                                                                                                                                                  
                {                                                                                                                                                                                                                                                                                                                                                                                                                         
                    DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}',{3},0,{4}+{3},0)", thisDataSet.ThisTables[2][0].ToString(), transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[2][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2}, {3}={3}+{2} WHERE {4}='{5}'", thisDataSet.ThisTables[6][0].ToString(), thisDataSet.ThisColumn[6][0], transfarDataRow[2], thisDataSet.ThisColumn[8][0], thisDataSet.ThisColumn[0][0].ToString(), transfarDataRow[0]));                                                                                                                                   
                }                                                                                                                                                                                                                                                                                                                                                                                                                         
                else if(transactionType.Equals(TransactionType.Credit))
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                    DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}',0,{3},{4}-{3},0)", thisDataSet.ThisTables[2][0].ToString(), transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[2][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}-{2}, {3}={3}-{2} WHERE {4}='{5}'", thisDataSet.ThisTables[6][0].ToString(), thisDataSet.ThisColumn[7][0], transfarDataRow[2], thisDataSet.ThisColumn[8][0], thisDataSet.ThisColumn[0][0].ToString(), transfarDataRow[0]));
                }
                else
                {
                    return false;
                }
            }                                                                                                                                                                                                                                                                                                                                                                                                                                                             
            catch                                                                                                                                                                                                                                                                                                                                                                                                                                                         
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                return false;                                                                                                                                                                                                                                                                                                                                                                                                                                             
            }
        }
        //Update Stock 
        private bool StockOperation(DataTable transfarData, ProductOperationType operationType, DBQueres DataWriter,DatabaseRead DataReder,MysqlRowCounter TableRowCounter)
        {
            try
            {
                DataRow tranfarDataRow=transfarData.Rows[0];                                                                             
                if (operationType.Equals(ProductOperationType.Purchase))
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                      //Column :0 Petunia_Date
                    DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}',{5},{6},'{7}',{8},{9},0,{10}+{9},0)", thisDataSet.ThisTables[23][0], tranfarDataRow[0],tranfarDataRow[8], tranfarDataRow[1], tranfarDataRow[2], tranfarDataRow[3], tranfarDataRow[4], tranfarDataRow[5], tranfarDataRow[6],tranfarDataRow[7], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[23][0].ToString(), new DatabaseRead(DataReder), new MysqlRowCounter(TableRowCounter))));//Column :1 Description
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2}, {3}={3}+{4},{5}={3}/{1} WHERE {6}='{7}'", thisDataSet.ThisTables[22][0], thisDataSet.ThisColumn[2][0], tranfarDataRow[3], thisDataSet.ThisColumn[3][0].ToString(), tranfarDataRow[4], thisDataSet.ThisColumn[4][0].ToString(), thisDataSet.ThisColumn[10][0].ToString(),tranfarDataRow[8]));                                                                                                          //Column :2 Particulars
                }                                                                                                                                                                                                                                                                                                                                                                                                                                                      //Column :3 Quantity
                else if (operationType.Equals(ProductOperationType.Sales))                                                                                                                                                                                                                                                                                                                                                                                             //Column :4 Rate//Amount
                {                                                                                                                                                                                                                                                                                                                                                                                                                                                      //Column :5 AVERAGE_RATE
                    DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}',{5},{6},{7},{8},0,{9},{10}-{9},0)", thisDataSet.ThisTables[23][0], tranfarDataRow[0], tranfarDataRow[8], tranfarDataRow[1], tranfarDataRow[2], tranfarDataRow[3], tranfarDataRow[4], "0.00", tranfarDataRow[6], tranfarDataRow[7], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[23][0].ToString(), new DatabaseRead(DataReder), new MysqlRowCounter(TableRowCounter))));//Column :6 INVOICE_NUMBER
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}-{2}, {3}={3}-{4},{5}={3}/{1} WHERE {6}='{7}'", thisDataSet.ThisTables[22][0], thisDataSet.ThisColumn[2][0], tranfarDataRow[3], thisDataSet.ThisColumn[3][0].ToString(), tranfarDataRow[4], thisDataSet.ThisColumn[4][0].ToString(), thisDataSet.ThisColumn[10][0].ToString(), tranfarDataRow[8]));                                                                                       //Column :7 Debt_Tk or Credit_tk
                }                                                                                                                                                                                                                                                                                                                                                                                                                                                    //Column :8 Product_ID
                else                                                                                                                                                                                                                                                                                                                                                                                                                                                   
                {
                    return false;
                }
            }
            catch 
            {

                return false;
            }
        }
        //Insert into Bank
        private bool BankAccount(DataTable transfarData,TransactionType transactionType,DatabaseRead DataReder, DBQueres DataWriter, MysqlRowCounter TableRowCounter)
        {
            try
            {
                DataRow transfarDataRow = transfarData.Rows[0];
                if (transactionType.Equals(TransactionType.debt))                                                                                                                                                                                                                                                                                                                                       //Max Column 3
                {                                                                                                                                                                                                                                                                                                                                                                                       //Column : PETUNIA_DATE
                    return DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}',0,{3},{4}-{3},0)", thisDataSet.ThisTables[1][0].ToString(), transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], LastRowVaue(thisDataSet.ThisColumn[9][0].ToString(), thisDataSet.ThisTables[1][0].ToString(), new DatabaseRead(DataReder), new MysqlRowCounter(TableRowCounter))));                     //Column : DESCRIPTION
                }                                                                                                                                                                                                                                                                                                                                                                                       //Column : DEBIT_TK or CREDIT_TK
                else if (transactionType.Equals(TransactionType.Credit))                                                                                                                                                                                                                                                                                                                                
                {
                    return DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}',{3},0,{4}+{3},0)", thisDataSet.ThisTables[1][0].ToString(), transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], LastRowVaue(thisDataSet.ThisColumn[9][0].ToString(), thisDataSet.ThisTables[1][0].ToString(), new DatabaseRead(DataReder), new MysqlRowCounter(TableRowCounter))));                         
                }                                                                                                                                                                                                                                                                                                                                                                                       

                else
                {
                    return false;
                }
               
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Insert Creditors Account
        public bool CreditorsAndDebitorsAccount(DataTable transfarData,TransactionType transactionType,DatabaseRead DataReder,DBQueres DataWriter,MysqlRowCounter TableRowCounter)
        {
            try
            {
                DataRow transfarDataRow = transfarData.Rows[0];                                                                                                                                                                                                                                                                                       // column :0 PETUNIA_DATE
                if (transactionType.Equals(TransactionType.Credit))                                                                                                                                                                                                                                                                                   // column :1 DESCRIPTION
                {                                                                                                                                                                                                                                                                                                                                     // column :2 CREDIT_TK or DEBIT_TK
                  DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}',0,{5},0,{6}+{5})", thisDataSet.ThisTables[5][0], transfarDataRow[0], transfarDataRow[3], transfarDataRow[4], transfarDataRow[1], transfarDataRow[2], LastRowVaue(thisDataSet.ThisColumn[9][0].ToString(), thisDataSet.ThisTables[5][0].ToString(), new DatabaseRead(DataReder), new MysqlRowCounter(TableRowCounter))));
                  DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}'",thisDataSet.ThisTables[3][0],thisDataSet.ThisColumn[6][0],transfarDataRow[2],thisDataSet.ThisColumn[12][0],transfarDataRow[3]));                                                                                                                                // column :3 User_ID  
                  return DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}',0,{5},0,0)",thisDataSet.ThisTables[24][0],transfarDataRow[0],transfarDataRow[3],transfarDataRow[4],transfarDataRow[1],transfarDataRow[2]));//need update Credit_Balance_Tk                                                                                                    //Column : 4 Product ID      
                }                                                                                                                                                                                                                                                                                                                                     
                else if (transactionType.Equals(TransactionType.debt))
                {
                  DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}',{5},0,{6}-{5},0)", thisDataSet.ThisTables[7][0], transfarDataRow[0], transfarDataRow[3], transfarDataRow[4], transfarDataRow[1], transfarDataRow[2], LastRowVaue(thisDataSet.ThisColumn[9][0].ToString(), thisDataSet.ThisTables[7][0].ToString(), new DatabaseRead(DataReder), new MysqlRowCounter(TableRowCounter))));
                  DataWriter(string.Format("UPDATE {0} SET {1}={1}-{2} WHERE {3}='{4}'", thisDataSet.ThisTables[3][0], thisDataSet.ThisColumn[7][0], transfarDataRow[2], thisDataSet.ThisColumn[12][0], transfarDataRow[3]));
                  return DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}',{5},0,0,0)", thisDataSet.ThisTables[24][0], transfarDataRow[0],transfarDataRow[3], transfarDataRow[4], transfarDataRow[1], transfarDataRow[2]));//need update Credit_Balance_Tk
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Trading Account//Need To Call
        private bool TradingAccount(DataTable transfarData, TransactionType transactionType, DatabaseRead DataReder, DBQueres DataWriter, MysqlRowCounter TableRowCounter)
        {
            try
            {
                DataRow transfarDataRow = transfarData.Rows[0];
                if (Convert.ToDouble(transfarDataRow[2])>Convert.ToDouble(transfarDataRow[3]))
                {
                    double profit = Convert.ToDouble(transfarDataRow[2]) - Convert.ToDouble(transfarDataRow[3]);
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}',{3},{4},0,{5},0,0)", thisDataSet.ThisTables[25][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], transfarDataRow[3], profit));                                                  	//Column : 0 Petunia_Date 
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='Profit'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[7][0], profit, thisDataSet.ThisColumn[0][0], transfarDataRow[0], thisDataSet.ThisColumn[1][0]));          	//Column : 1 Description
                }                                                                                                                                                                                                                                                           	//Column : 2 Purchase_Rate
                else if (Convert.ToDouble(transfarDataRow[2]) < Convert.ToDouble(transfarDataRow[3]))                                                                                                                                                                         	//Column : 3 Sales_Rate
                {
                    double profit = Convert.ToDouble(transfarDataRow[3]) - Convert.ToDouble(transfarDataRow[2]);
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}',{3},{4},{5},0,0,0)", thisDataSet.ThisTables[25][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], transfarDataRow[3], profit));
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='Profit'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[6][0], profit, thisDataSet.ThisColumn[0][0], transfarDataRow[0], thisDataSet.ThisColumn[1][0]));
                }
                else if (Convert.ToDouble(transfarDataRow[2]).Equals(Convert.ToDouble(transfarDataRow[3])))
                {
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}',{3},{4},0,0,0,0)", thisDataSet.ThisTables[25][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], transfarDataRow[3]));
                    return DataWriter(string.Format("UPDATE {0} SET {1}={1}+0 WHERE {3}='{4}' AND {5}='Profit'", thisDataSet.ThisTables[16][0], thisDataSet.ThisColumn[6][0], thisDataSet.ThisColumn[0][0], transfarDataRow[0], thisDataSet.ThisColumn[1][0]));
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Purchase Ledger
        public bool PurchaseLedger(DataTable transfarData, TransactionType transactionType, DBQueres DataWrite, DatabaseRead DataRader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSat = new AccountingDataSet();
                DataRow transfarDataRow = transfarData.Rows[0];
                //string tableStockLedger = thisDataSet.ThisTables.Rows[0][thisDataSet.ThisTables.Columns[0].ToString()].ToString();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
                DataWrite(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}',{5},{6},'{7}','{8}',{9},0,{10}+{9},0)", thisDataSet.ThisTables[17][0], transfarDataRow[0],transfarDataRow[8], transfarDataRow[1], transactionType, transfarDataRow[2], transfarDataRow[3], transfarDataRow[4], transfarDataRow[5], transfarDataRow[6], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[17][0].ToString(), new DatabaseRead(DataRader), new MysqlRowCounter(TableRowCounter)))); //PurchaseLedger                                                                                                                          
                commonDataSat.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1],ProductOperationType.Purchase, transfarDataRow[2],transfarDataRow[6],transfarDataRow[4],transfarDataRow[5],transfarDataRow[6],transfarDataRow[8]);
                StockOperation(commonDataSat.ThisCommonUse, ProductOperationType.Purchase, new DBQueres(DataWrite),new DatabaseRead(DataRader),new MysqlRowCounter(TableRowCounter));
                if (transactionType.Equals(TransactionType.Cash))                                                                                                                                      //Column : 0 Petunia_Date                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
                {                                                                                                                                                                                      //Column : 1 Description
                    commonDataSat.ThisCommonUse.Clear();                                                                                                                                               //Column : 2 Transaction="";\\Should be Remove
                    commonDataSat.ThisCommonUse.Dispose();                                                                                                                                             //Column : 2 Quantity
                    commonDataSat.ThisCommonUse.Rows.Add(transfarDataRow[0],transfarDataRow[1],transfarDataRow[6]);                                                                                    //Column : 3 Rate
                    return CashOperation(commonDataSat.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWrite), new DatabaseRead(DataRader), new MysqlRowCounter(TableRowCounter));             //Column : 4 Average_Rate                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                }                                                                                                                                                                                      //Column : 5 Invoice_Number                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                else if (transactionType.Equals(TransactionType.check))                                                                                                                                //Column : 6 Debt_Tk or Credit_Tk                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
                {                                                                                                                                                                                      //Column : 7 User_ID                                                                                                                                          
                    commonDataSat.ThisCommonUse.Clear();                                                                                                                                               //Column : 8 ProductID
                    commonDataSat.ThisCommonUse.Dispose();                                                                                                                                             //Column : 9 Credit Balance
                    commonDataSat.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[6]);                                                                                  //Column :10 Payment Balance
                    return BankAccount(commonDataSat.ThisCommonUse, TransactionType.Credit, new DatabaseRead(DataRader), new DBQueres(DataWrite), new MysqlRowCounter(TableRowCounter));                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
                }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
                else if (transactionType.Equals(TransactionType.Credit))                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
                {
                    commonDataSat.ThisCommonUse.Clear();
                    commonDataSat.ThisCommonUse.Dispose();
                    commonDataSat.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[10]);
                    CashOperation(commonDataSat.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWrite), new DatabaseRead(DataRader), new MysqlRowCounter(TableRowCounter)); 
                    commonDataSat.ThisCommonUse.Clear();
                    commonDataSat.ThisCommonUse.Dispose();
                    commonDataSat.ThisCommonUse.Rows.Add(transfarDataRow[0],transactionType,transfarDataRow[9],transfarDataRow[7],transfarDataRow[8]);
                    return CreditorsAndDebitorsAccount(commonDataSat.ThisCommonUse, TransactionType.Credit, new DatabaseRead(DataRader), new DBQueres(DataWrite), new MysqlRowCounter(TableRowCounter));
                }
                else
                {
                    return false;
                }
                
            }
            catch
            {
                return false;
            }
        }
        //Sales Ledger
        public bool SalesLedger(DataTable transfarData, TransactionType transactionType, DBQueres DataWriter, DatabaseRead DataReadr, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSet = new AccountingDataSet();
                DataRow transfarDataRow=transfarData.Rows[0];
                DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}','{5}','{6}',{7},0,{8},0,{9}+{8})", thisDataSet.ThisTables[20][0], transfarDataRow[0],transfarDataRow[6],transfarDataRow[1], transactionType, transfarDataRow[2], transfarDataRow[3], transfarDataRow[4], transfarDataRow[5],LastRowVaue(thisDataSet.ThisColumn[9][0].ToString(),thisDataSet.ThisTables[20][0].ToString(),new DatabaseRead(DataReadr),new MysqlRowCounter(TableRowCounter))));
                commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], ProductOperationType.Sales, transfarDataRow[2],Convert.ToDouble(transfarDataRow[2]) * Convert.ToDouble(transfarDataRow[8]),transfarDataRow[8], transfarDataRow[4], transfarDataRow[5], transfarDataRow[6]);
                StockOperation(commonDataSet.ThisCommonUse, ProductOperationType.Sales, new DBQueres(DataWriter), new DatabaseRead(DataReadr), new MysqlRowCounter(TableRowCounter));
                if (transactionType.Equals(TransactionType.Cash))                                                                                                                              
                {
                    commonDataSet.ThisCommonUse.Clear();                                                                                                                                                                                        //Column :0 Petunia_Date
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                                                                      //Column :1 Description
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1],transfarDataRow[5]);                                                                                                                            //Column :2 Transaction //should be remove
                    return CashOperation(commonDataSet.ThisCommonUse, TransactionType.debt, new DBQueres(DataWriter), new DatabaseRead(DataReadr), new MysqlRowCounter(TableRowCounter));                                                       //Column :2 Quantity
                }                                                                                                                                                                                                                               //Column :3 Rate
                else if (transactionType.Equals(TransactionType.Credit))                                                                                                                                                                        //Column :4 Invoice_Number
                {                                                                                                                                                                                                                               //Column :5 Debt_Tk or Credit_tk 
                    commonDataSet.ThisCommonUse.Clear();                                                                                                                                                                                        //Column :6 Product_ID                                                                                                                                                                                
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                                                                      //Column :7 User_ID
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[10]);                                                                                                                          //Column :8 Average_Rate   
                    CashOperation(commonDataSet.ThisCommonUse, TransactionType.debt, new DBQueres(DataWriter), new DatabaseRead(DataReadr), new MysqlRowCounter(TableRowCounter));                                                              //Column :9 Credit Balance
                    commonDataSet.ThisCommonUse.Clear();                                                                                                                                                                                        //Column :10 Payment Balance
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                                                                      
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[9], transfarDataRow[7], transfarDataRow[6]);                                                                                                   
                    return CreditorsAndDebitorsAccount(commonDataSet.ThisCommonUse, TransactionType.debt, new DatabaseRead(DataReadr), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));                                         
                }
                else if (transactionType.Equals(TransactionType.check))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[5]);
                    return BankAccount(commonDataSet.ThisCommonUse, TransactionType.Credit, new DatabaseRead(DataReadr), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));
                 }                                                                                                                                                                                                           
                else                                                                                                                                                                                                                            
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        // Purchase Return
        public bool PurchaseReturn(DataTable transfarData, TransactionType transactionType, DBQueres DataWriter,DatabaseRead DataReader,MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSet=new AccountingDataSet();
                DataRow transfarDataRow = transfarData.Rows[0];
                DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}',{5},{6},{7},'{8}',0,{9},{10}-{9},0)", thisDataSet.ThisTables[17][0],transfarDataRow[0], transfarDataRow[2], transfarDataRow[1], transactionType, transfarDataRow[3], transfarDataRow[4], transfarDataRow[5], transfarDataRow[6],transfarDataRow[7], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[17][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0],transfarDataRow[1],transfarDataRow[1],transfarDataRow[3],transfarDataRow[4],transfarDataRow[5],transfarDataRow[6],transfarDataRow[7]);
                StockOperation(commonDataSet.ThisCommonUse, ProductOperationType.Sales, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));
                if (transactionType.Equals(TransactionType.Credit))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[7], transfarDataRow[8],transfarDataRow[2]);                                                         //Column : 0 Petunia_Date  
                    return CreditorsAndDebitorsAccount(commonDataSet.ThisCommonUse, TransactionType.debt, new DatabaseRead(DataReader), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));             //Column : 1 Description
                }                                                                                                                                                                                                    //Column : 2 Product_ID
                else if (transactionType.Equals(TransactionType.debt))                                                                                                                                               //Column : 3 Quantity
                {                                                                                                                                                                                                    //Column : 4 Rate
                    commonDataSet.ThisCommonUse.Clear();                                                                                                                                                             //Column : 5 Average_Rate  
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                                           //Column : 6 Invoice_Number
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[7]);                                                                                                //Column : 7 Debt_Tk Or credit_Tk  
                    return CashOperation(commonDataSet.ThisCommonUse, TransactionType.debt, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));                           //Column : 8 User_ID
                }                                                                                                                                                                                                    //Column : 9    
                else                                                                                                                                                                                                 
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //Sales Return
        public bool SalesReturn(DataTable transfarData, TransactionType transactionType, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                DataRow transfarDataRow = transfarData.Rows[0];
                AccountingDataSet commonDataSet = new AccountingDataSet();
                DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}',{5},{6},{7},{8},0,0,{9}-{8})",thisDataSet.ThisTables[20][0],transfarDataRow[0],transfarDataRow[2],transfarDataRow[1],transactionType.Equals(TransactionType.Cash) ? TransactionType.Cash : TransactionType.Credit, transfarDataRow[3],transfarDataRow[4],transfarDataRow[5],transfarDataRow[6],LastRowVaue(thisDataSet.ThisColumn[9][0].ToString(),thisDataSet.ThisTables[20][0].ToString(),new DatabaseRead(DataReader),new MysqlRowCounter(TableRowCounter))));
                commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0],transfarDataRow[1],transfarDataRow[2],transfarDataRow[3],transfarDataRow[4],transfarDataRow[5],transfarDataRow[6],transfarDataRow[7]);
                StockOperation(commonDataSet.ThisCommonUse, ProductOperationType.Purchase, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));
                if (transactionType.Equals(TransactionType.Cash))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                                                                  //Column : 0 Petunia_Date  
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[7]);                                                                                                                       //Column : 1 Description
                    return CashOperation(commonDataSet.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));                                                //Column : 2 Transaction /\ Product_ID
                }                                                                                                                                                                                                                           //Column : 3 Quantity
                else if (transactionType.Equals(TransactionType.debt))                                                                                                                                                                      //Column : 4 Rate
                {                                                                                                                                                                                                                           //Column : 5 Average_Rate  
                    commonDataSet.ThisCommonUse.Clear();                                                                                                                                                                                    //Column : 6 Invoice_Number
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                                                                  //Column : 7 Debt_Tk Or credit_Tk
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[7], transfarDataRow[8],transfarDataRow[9]);                                                                                //Column : 8 User_ID
                    return CreditorsAndDebitorsAccount(commonDataSet.ThisCommonUse, TransactionType.debt, new DatabaseRead(DataReader), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));                                    //Column :9 Product_ID
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //Cash Received
        public bool CashReceived(DataTable transfarData, DBQueres DataWriter,DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSet = new AccountingDataSet();
                DataRow transfarDataRow = transfarData.Rows[0];
                commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0],"Cash Receive",transfarDataRow[1]);
                return CashOperation(commonDataSet.ThisCommonUse, TransactionType.debt, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));
            }
            catch                                                                                                          //Column : Petunia_Date
            {                                                                                                              //Column : Debt_Tk Or credit_Tk
                return false;                                                                                             
            }                                                                                                             
        }
        //Cash Payment
        public bool CashPayment(DataTable transfarData, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSet = new AccountingDataSet();
                DataRow transfarDataRow = transfarData.Rows[0];
                commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0],"Cash Payment",transfarDataRow[1]);
                return CashOperation(commonDataSet.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));
            }
            catch                                                                                        //Column : Petunia_Date
            {                                                                                            //Column : Debt_Tk Or credit_Tk
                return false;
            }
        }
        //Income And Expanse
        public bool ExpanseS(DataTable transfarData, ExpanceType expanceType, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {                                                                                    
            try
            {
                DataRow transfarDataRow = transfarData.Rows[0];
                if (expanceType.Equals(ExpanceType.Salary))
                {
                    AccountingDataSet commonDataSet = new AccountingDataSet();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0],ExpanceType.OfficeExpance,transfarDataRow[3]);
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}','{4}',{5},0,{6}+{5},0)",thisDataSet.ThisTables[19][0],transfarDataRow[0],transfarDataRow[4],transfarDataRow[1],transfarDataRow[2],transfarDataRow[3],LastRowVaue(thisDataSet.ThisColumn[6][0].ToString(),thisDataSet.ThisTables[19][0].ToString(),new DatabaseRead(DataReader),new MysqlRowCounter(TableRowCounter))));
                    DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='Salary'",thisDataSet.ThisTables[16][0],thisDataSet.ThisColumn[6][0],transfarDataRow[3],thisDataSet.ThisColumn[0][0],transfarDataRow[0],thisDataSet.ThisColumn[1][0]));
                    CashOperation(commonDataSet.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));
                    DataWriter(string.Format("UPDATE {0} SET {1}={2} WHERE {3}='{4}'", thisDataSet.ThisTables[3][0], thisDataSet.ThisColumn[6][0], transfarDataRow[3],thisDataSet.ThisColumn[12][0],transfarDataRow[4]));                                                                                                                                                                     //Column : 0 Petunia_Date 
                    return DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','Salary','{2}','{3}',{4},0,0,0)", thisDataSet.ThisTables[24][0], transfarDataRow[0], transfarDataRow[4], transfarDataRow[1], transfarDataRow[3]));                                                                                                                                                              //Column : 1 Description
                }                                                                                                                                                                                                                                                                                                                                                                        //Column : 2 Transaction
                else if (expanceType.Equals(ExpanceType.Rent))                                                                                                                                                                                                                                                                                                                           //Column : 3 Debt_Tk Or credit_Tk
                {                                                                                                                                                                                                                                                                                                                                                                        //Column : 4 User_ID
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}',{4},0,{5}+{4},0)", thisDataSet.ThisTables[18][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], transfarDataRow[3], LastRowVaue(thisDataSet.ThisColumn[6][0].ToString(),thisDataSet.ThisTables[19][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                     return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='Rent'",thisDataSet.ThisTables[16][0],thisDataSet.ThisColumn[6][0],transfarDataRow[3],thisDataSet.ThisColumn[0][0],transfarDataRow[0],thisDataSet.ThisColumn[1][0]));
                }
                else if (expanceType.Equals(ExpanceType.OfficeExpance))
                {
                     DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}',{4},0,{5}+{4},0)",thisDataSet.ThisTables[13][0],transfarDataRow[0],transfarDataRow[1],transfarDataRow[2],transfarDataRow[3],LastRowVaue(thisDataSet.ThisColumn[6][0].ToString(),thisDataSet.ThisTables[19][0].ToString(),new DatabaseRead(DataReader),new MysqlRowCounter(TableRowCounter))));
                     return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='OfficeExpance'",thisDataSet.ThisTables[16][0],thisDataSet.ThisColumn[6][0],transfarDataRow[3],thisDataSet.ThisColumn[0][0],transfarDataRow[0],thisDataSet.ThisColumn[1][0]));
                }
                else if (expanceType.Equals(ExpanceType.Stationary))
                {
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}',{4},0,{5}+{4},0)", thisDataSet.ThisTables[21][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], transfarDataRow[3], LastRowVaue(thisDataSet.ThisColumn[6][0].ToString(), thisDataSet.ThisTables[19][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                     return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='Stationary'",thisDataSet.ThisTables[16][0],thisDataSet.ThisColumn[6][0],transfarDataRow[3],thisDataSet.ThisColumn[0][0],transfarDataRow[0],thisDataSet.ThisColumn[1][0]));
                }
                else if (expanceType.Equals(ExpanceType.Others))
                {
                    DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','{2}','{3}',{4},0,{5}+{4},0)", thisDataSet.ThisTables[9][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[2], transfarDataRow[3], LastRowVaue(thisDataSet.ThisColumn[6][0].ToString(), thisDataSet.ThisTables[19][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                     return DataWriter(string.Format("UPDATE {0} SET {1}={1}+{2} WHERE {3}='{4}' AND {5}='Others'",thisDataSet.ThisTables[16][0],thisDataSet.ThisColumn[6][0],transfarDataRow[3],thisDataSet.ThisColumn[0][0],transfarDataRow[0],thisDataSet.ThisColumn[1][0]));
                }
                else
                {
                    return false;
                }
              
            }
            catch
            {
                return false;
            }
        }
        //Furniture Ledger
        public bool FurnitureLedger(DataTable transfarData,TransactionType transactionType, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSet = new AccountingDataSet();
                DataRow transfarDataRow= transfarData.Rows[0];
                DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}',{3},0,{4}+{3},0)",thisDataSet.ThisTables[10][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[3], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[10][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                if (transactionType.Equals(TransactionType.Cash))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                             //Column : 0 Petunia_Date
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[3]);                                                                                  //Column : 1 Description
                    return CashOperation(commonDataSet.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));           //Column : 2 Debt_Tk Or credit_Tk
                }
                else if (transactionType.Equals(TransactionType.check))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[3]);
                    return BankAccount(commonDataSet.ThisCommonUse, TransactionType.debt, new DatabaseRead(DataReader), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));
                }
                else if (transactionType.Equals(TransactionType.Credit))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[3], transfarDataRow[4]);
                    return CreditorsAndDebitorsAccount(commonDataSet.ThisCommonUse, TransactionType.Credit, new DatabaseRead(DataReader), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //Office Instrument
        public bool OfficeInstrumentLedger(DataTable transfarData, TransactionType transactionType, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountingDataSet commonDataSet = new AccountingDataSet();
                DataRow transfarDataRow = transfarData.Rows[0];
                DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','{2}',{3},0,{4}+{3},0)",thisDataSet.ThisTables[14][0], transfarDataRow[0], transfarDataRow[1], transfarDataRow[3], LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[10][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                if (transactionType.Equals(TransactionType.Cash))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();                                                                                                                                             //Column : 0 Petunia_Date
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[3]);                                                                                  //Column : 1 Description
                    return CashOperation(commonDataSet.ThisCommonUse, TransactionType.Credit, new DBQueres(DataWriter), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));           //Column : 2 Debt_Tk Or credit_Tk
                }
                else if (transactionType.Equals(TransactionType.check))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[3]);
                    return BankAccount(commonDataSet.ThisCommonUse, TransactionType.debt, new DatabaseRead(DataReader), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));
                }
                else if (transactionType.Equals(TransactionType.Credit))
                {
                    commonDataSet.ThisCommonUse.Clear();
                    commonDataSet.ThisCommonUse.Dispose();
                    commonDataSet.ThisCommonUse.Rows.Add(transfarDataRow[0], transfarDataRow[1], transfarDataRow[3], transfarDataRow[4]);
                    return CreditorsAndDebitorsAccount(commonDataSet.ThisCommonUse, TransactionType.Credit, new DatabaseRead(DataReader), new DBQueres(DataWriter), new MysqlRowCounter(TableRowCounter));
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        //Depreciation Furniture And Office Instrument
        public bool DepreciationFurnitureAndOfficeInstrument(DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                AccountOtherFunctions otherAccountingFunctions = new AccountOtherFunctions();
                double Depreciation= LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(),thisDataSet.ThisTables[10][0].ToString(),new DatabaseRead(DataReader),new MysqlRowCounter(TableRowCounter));
                DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','Depreciation (15%)',0,{2},{3}-{2},0)", thisDataSet.ThisTables[10][0], AccountingVariables.PETUNIA_DATE,otherAccountingFunctions.PersentIs(Depreciation),Depreciation));
                DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','Furniture Depreciation (15%)',{2},0,0,0)", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE, otherAccountingFunctions.PersentIs(Depreciation)));
                Depreciation = LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[14][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter));
                DataWriter(string.Format("INSERT INTO {0} VALUES('{1}','Depreciation (15%)',0,{2},{3}-{2},0)", thisDataSet.ThisTables[14][0], AccountingVariables.PETUNIA_DATE, otherAccountingFunctions.PersentIs(Depreciation), Depreciation));
                return DataWriter(string.Format("INSERT INTO {0} VALUES ('{1}','Office Instrutement Depreciation (15%)',{2},0,0,0)", thisDataSet.ThisTables[16][0], AccountingVariables.PETUNIA_DATE, otherAccountingFunctions.PersentIs(Depreciation)));
            }
            catch (Exception)
            {

                return false;
            }
        }
        //Initial Balance Insert
        public bool InitialBalanceInsert(string amount ,TransactionType transactionType, DBQueres DataWriter, DatabaseRead DataReader, MysqlRowCounter TableRowCounter)
        {
            try
            {
                if (transactionType.Equals(TransactionType.Cash))
                {
                    DataWriter(string.Format("INSERT INFO {0} VALUES('{1}','0',{2},0,{3}+{2},0)",thisDataSet.ThisTables[2][0],AccountingVariables.PETUNIA_DATE,amount,LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(),thisDataSet.ThisTables[2][0].ToString(),new DatabaseRead(DataReader),new MysqlRowCounter(TableRowCounter))));
                    return true;
                }
                else if (transactionType.Equals(TransactionType.bank))
                {
                    DataWriter(string.Format("INSERT INFO {0} VALUES('{1}','{2}',0,{3}+{2},0,0)", thisDataSet.ThisTables[1][0], AccountingVariables.PETUNIA_DATE, amount, LastRowVaue(thisDataSet.ThisColumn[8][0].ToString(), thisDataSet.ThisTables[1][0].ToString(), new DatabaseRead(DataReader), new MysqlRowCounter(TableRowCounter))));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {

                return false;
            }
        }



        //Profit And Lose Account
        //public bool ProfitAndLoseAccountByMonth(DataTable transfarData,string ProfitDate, DatabaseRead DataReader)
        //{
        //    try
        //    {
        //        double creditAmount = 0, debtAmount = 0;
        //        string[] months = ProfitDate.Split('-');
        //        for (int i = 0; i < 32; i++)
        //        {
        //            DataTable profitAndLoseDataTable = new DataTable();
        //            profitAndLoseDataTable = DataReader(string.Format("SELECT {0},{1} FROM {2} WHERE {3}={4}",thisDataSet.ThisColumn[6][0],thisDataSet.ThisColumn[7][0], thisDataSet.ThisTables[16][0],thisDataSet.ThisColumn[0][0],string.Format("{0}-{1}-{2}",months[0],months[1],AccountOtherFunctions.NumberCoupleIs(i))));
        //            foreach (DataRow profitLoseDataRow in profitAndLoseDataTable.Rows)
        //            {
                        
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
                
        //        return false;
        //    }
        //}
       
        ////Balance DB
        //public bool BlanceBD(DBReader DBDataReader, DBQueres DBOperation)
        //{
        //    try
        //    {
        //        AccountingDataSet blancedata = new AccountingDataSet();
        //        DBDataReader(string.Format("SELECT * FROM {0} WHERE {1} = {2}", AccountingVariables.ACCOUNTING_TABLE_NAME[2], AccountingVariables.ACCOUNTING_COLUMN_NAME[5], AccountingVariables.PETUNIA_DATE), blancedata.Blance);
        //        DataRow blanceDataRow = blancedata.Blance.Rows[0];
        //        if ((double)blanceDataRow[blancedata.Blance.Columns[1].ToString()] > (double)blanceDataRow[blancedata.Blance.Columns[2].ToString()])
        //        {
        //            return DBOperation(string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4}", AccountingVariables.ACCOUNTING_TABLE_NAME[2], AccountingVariables.ACCOUNTING_COLUMN_NAME[11], blanceDataRow[blancedata.Blance.Columns[1].ToString()], AccountingVariables.ACCOUNTING_COLUMN_NAME[5], AccountingVariables.PETUNIA_DATE));
        //        }
        //        else if ((double)blanceDataRow[blancedata.Blance.Columns[1].ToString()] < (double)blanceDataRow[blancedata.Blance.Columns[2].ToString()])
        //        {
        //            return DBOperation(string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4}", AccountingVariables.ACCOUNTING_TABLE_NAME[2], AccountingVariables.ACCOUNTING_COLUMN_NAME[11], blanceDataRow[blancedata.Blance.Columns[2].ToString()], AccountingVariables.ACCOUNTING_COLUMN_NAME[5], AccountingVariables.PETUNIA_DATE));
        //        }
        //        else if (((double)blanceDataRow[blancedata.Blance.Columns[1].ToString()]).Equals((double)blanceDataRow[blancedata.Blance.Columns[2].ToString()]))
        //        {
        //            return DBOperation(string.Format("UPDATE {0} SET {1}={2} WHERE {3}={4}", AccountingVariables.ACCOUNTING_TABLE_NAME[2], AccountingVariables.ACCOUNTING_COLUMN_NAME[11], "0", AccountingVariables.ACCOUNTING_COLUMN_NAME[5], AccountingVariables.PETUNIA_DATE));
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
