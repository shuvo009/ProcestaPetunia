//Title :           Petunia Class all TextBlock Data Loader.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Data TextBlock Type Microsoft TextBlock ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using MysqlClass;
using System.Data;
using ProcestaVariables;

namespace Procesta_Petunia.Class
{
    class TextBlockDataLoader
    {
        //Sock info Quantity and Rate
        public void StockInfoLoad(TextBlock productID, TextBlock textBlockQuantity, TextBlock textBlockWaitAndAvarageRate, string ProductID)
        {
           DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1} ='{2}'", Variables.TABLE_NAME[4], Variables.COLUMN_NAME[4], ProductID));
            if (tempDataTable.Rows.Count > 0)
            {
                DataRow tempDataRow = tempDataTable.Rows[0];
                productID.Text = Convert.ToString(tempDataRow[0]);
                textBlockQuantity.Text =Convert.ToString(tempDataRow[1]);
                textBlockWaitAndAvarageRate.Text = Convert.ToString(tempDataRow[2]);
            }
            else
            {
                return;
            }
        }
        // Stock info All
        public void StockInfoLoad(TextBlock productID, TextBlock quantity, TextBlock WaitAndAvarageRate, TextBlock Dabit, string ProductIDInternal)
        {
            DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1} ='{2}'", Variables.TABLE_NAME[4], Variables.COLUMN_NAME[4], ProductIDInternal));
            if (tempDataTable.Rows.Count > 0)
            {
                DataRow tempDataRow = tempDataTable.Rows[0];
                productID.Text = (string)tempDataRow[0];
                quantity.Text = (string)(tempDataRow[1]).ToString();
                WaitAndAvarageRate.Text = (string)(tempDataRow[3]).ToString();
                Dabit.Text = (string)(tempDataRow[2]).ToString();
                tempDataTable.Dispose();
            }
            else
            {
                tempDataTable.Dispose();
                return;
            }
        }
        //Invoice Stock Info Load
        public void InvoiceStockInfoLoad(InvoiceList invoiceItems, TextBlock textboxproductID, TextBlock textBoxQuantity, TextBlock textBoxWaitAndAvarageRate, string ProductID)
        {
            StockInfoLoad(textboxproductID, textBoxQuantity, textBoxWaitAndAvarageRate, ProductID);
            IEnumerable<InvoiceItems> ProductChecker = from invoiceItem in invoiceItems where invoiceItem.ProductID.Equals(ProductID) select invoiceItem ;
            if (ProductChecker.Count() > 0)
            {
                InvoiceItems quantity = ProductChecker.First();
                textBoxQuantity.Text = (Convert.ToInt32(textBoxQuantity.Text) - quantity.Quantity).ToString();
            }   
        }
        //Debtors Creditors Account Info Viewer
        public void DebtorsCreditorsAccountInfoViewer(TextBlock contractPerson, TextBlock phone, TextBlock Email, TextBlock Address, TextBlock Amount, string userID)
        {
            DataTable temDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0},{1} WHERE {0}.{2}='{3}' AND {0}.{2}={1}.{2}", Variables.TABLE_NAME[10],Variables.TABLE_NAME[9], Variables.COLUMN_NAME[3], userID));
            if (temDataTable.Rows.Count > 0)
            {
                DataRow temDataRow = temDataTable.Rows[0];
                contractPerson.Text = temDataRow[4].ToString();
                phone.Text = temDataRow[2].ToString();
                Email.Text = temDataRow[3].ToString();
                Address.Text = temDataRow[5].ToString();
                Amount.Text = temDataRow[7].ToString();
            }
            temDataTable.Dispose();
        }
        //Debtor and Creditors Account Info Loader
        public bool DebtorAndCreditorAccountLoader(TextBlock amount, string userName)
        {
            DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[9], Variables.COLUMN_NAME[3], userName));
            if (tempDataTable.Rows.Count > 0)
            {
                DataRow temDataRow = tempDataTable.Rows[0];
                    amount.Text = temDataRow[1].ToString();
                    tempDataTable.Dispose();
                    return true;
            }
            else
            {
                tempDataTable.Dispose();
                return false;
            }

        }
    }
}
