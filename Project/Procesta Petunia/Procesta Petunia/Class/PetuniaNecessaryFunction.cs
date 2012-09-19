//Title :           Petunia Necessary Database Element.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Petunia Own Function Class.  
using System;
using System.Data;
using MysqlClass;
using System.Windows.Controls;
using ProcestaVariables;
using ProcestaNecessaryFunction;
using PetunicaControls;
namespace Procesta_Petunia.Class
{
    class PetuniaNecessaryFunction
    {
        //ProductID Finder
        public string ProductIDFinder(string companyName, string productName, string modelNumber)
        {
            DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1} WHERE {2}='{3}' AND {4}='{5}' AND {6}='{7}'", Variables.COLUMN_NAME[4], Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], companyName, Variables.COLUMN_NAME[10], productName, Variables.COLUMN_NAME[11], modelNumber));
            if (tempDataTable.Rows.Count > 0)
            {
                DataRow productIDDataRow = tempDataTable.Rows[0];
                return (string)productIDDataRow[0];
            }
            else
            {
                return "";
            }
        }
        //Invoice Total Amount
        public string TotalAmount(DataTable InvoiceTable)
        {
            double totalAmountIs = 0;
            foreach  (DataRow totalAmountDataRow in InvoiceTable.Rows)
            {
                totalAmountIs+=Convert.ToDouble(totalAmountDataRow[InvoiceTable.Columns[3].ToString()]);
            }
            return totalAmountIs.ToString(".00");
        }
        //Stock info Viewer
        public bool StockInfo(WrapPanel panelStockinfo,string query)
        {
            try
            {
                panelStockinfo.Children.Clear();
                DataTable tempDataTabale=new MySqlNaceassaryElement().DataReader(query);
                foreach (DataRow tempDataRow in tempDataTabale.Rows)
                {
                    StockInfoViewer tempStockInfoViewer = new StockInfoViewer();
                    tempStockInfoViewer.StockInfoDataContext(tempDataRow);
                    panelStockinfo.Children.Add(tempStockInfoViewer);
                }
                tempDataTabale.Dispose();
                return true;
            }
            catch
            {

                return false;
            }
        }
        //Stock Info By Group
        public bool StockInfoByGroup(WrapPanel panelStockInfo, string query)
        {
            try
            {
                panelStockInfo.Children.Clear();
                DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(query);
                foreach (DataRow tempDatRow in tempDataTable.Rows)
                {
                    StockInfoByGroup tempStockInfoGroup = new StockInfoByGroup();
                    tempStockInfoGroup.StockInfoGroupDataContext(tempDatRow);
                    panelStockInfo.Children.Add(tempStockInfoGroup);
                }
                tempDataTable.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }
        //Invoice Number
        public string InvoiceNumbers()
        {
            return new MySqlNaceassaryElement().InvoiceNumber(Variables.TABLE_NAME[14], Variables.COLUMN_NAME[15]);
        }
        //Date Creator 
        public string DateFromRadDatePicker(Telerik.Windows.Controls.RadDatePicker radDatePicker)
        {
            return new NecessaryFunction().MysqlDateFormate(radDatePicker.SelectedDate.ToString());
        }
        //Three month Back Time 
        public string ThreeMonthBack(Telerik.Windows.Controls.RadDatePicker radDatePicker)
        {
            return new NecessaryFunction().MysqlDateFormate(((DateTime)radDatePicker.SelectedDate).AddDays(-3).ToString());
        }
    }
}
