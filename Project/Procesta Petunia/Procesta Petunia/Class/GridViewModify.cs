//Title :           Petunia Class GridView Data Loader.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Data GridView Type Telerik RadGridView ;
using System;
using System.Collections.Generic;
using System.Linq;
using ProcestaVariables;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Procesta_Petunia.Class;
using ProcestaNecessaryFunction;
using Procesta_Petunia.Reports;
using Telerik.Windows.Controls;
using System.IO;
namespace Procesta_Petunia.Class
{
    class GridViewModify
    {
        //Grid view Chart Item Add
        public void ChartItemAdd(InvoiceList invoiceItems, string productID,string Description, string stockQuantity, string saleQuanty, string SaleAmount, string saleRate, TextBlock quantity)
        {
            if (Convert.ToInt32(saleQuanty) <= Convert.ToInt32(stockQuantity))
            {
                IEnumerable<InvoiceItems> invoiceitem = from tempInvoiceItem in invoiceItems where tempInvoiceItem.ProductID.Equals(productID) select tempInvoiceItem;
                if (invoiceitem.Count() > 0)
                    {
                        MessageBoxResult errorCodeMessBoxResult = new MessageBoxResult();
                        errorCodeMessBoxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 7], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                        
                        switch (errorCodeMessBoxResult)
                        {
                            case MessageBoxResult.Yes:
                                invoiceitem.First().Quantity += Convert.ToInt32(saleQuanty);
                                invoiceitem.First().Amount += Convert.ToDouble(SaleAmount);
                                break;
                            case MessageBoxResult.No:
                                invoiceitem.First().Quantity = Convert.ToInt32(saleQuanty);
                                invoiceitem.First().Amount = Convert.ToDouble(SaleAmount);
                                break;
                            case MessageBoxResult.Cancel:
                                return;
                            default:
                                return;
                        }
                    }
                    else
                    {
                        invoiceItems.Add(new InvoiceItems(productID, Description, Convert.ToInt32(saleQuanty), Convert.ToDouble(saleRate), Convert.ToDouble(SaleAmount)));
                    }
                    quantity.Text = Convert.ToString(Convert.ToDouble(stockQuantity) - Convert.ToDouble(saleQuanty));
                    return;
                }
            else
            {
                Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 0], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            
        }
        //Invoice Purchase Debtors and creditors View
        public bool InvPurDebCre(Telerik.Windows.Controls.RadGridView radGridView)
        {
            try
            {
                CristalReportDataSourch(radGridView, MainWindow.petuniaDataSet.InvPurDebCrePaymentView, Variables.OperationTrypes.InvPurDebCre);
                InvPurDebCreCrystalReport invPurdebtCristalReport = new InvPurDebCreCrystalReport();
                invPurdebtCristalReport.SetDataSource(MainWindow.petuniaDataSet);
                invPurdebtCristalReport.SetParameterValue("CompanyName",Variables.CompanyInfo[0]);
                invPurdebtCristalReport.SetParameterValue("CompanyTitle",Variables.CompanyInfo[1]);
                invPurdebtCristalReport.SetParameterValue("CompanyAddress",Variables.CompanyInfo[2]);
                ReportViewer InvPurDebCreReportViewer = new ReportViewer();
                InvPurDebCreReportViewer.ReportViewerView.ViewerCore.ReportSource = invPurdebtCristalReport;
                InvPurDebCreReportViewer.ShowDialog();
                InvPurDebCreReportViewer.ReportViewerView.ViewerCore.CloseView(invPurdebtCristalReport);
                invPurdebtCristalReport.Close();
                invPurdebtCristalReport.Dispose();
                MainWindow.petuniaDataSet.InvPurDebCrePaymentView.Clear();
                MainWindow.petuniaDataSet.InvPurDebCrePaymentView.Dispose();
                return true;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                return false;
            }
        }
        //Crystal View DataSourch
        public DataTable CristalReportDataSourch(Telerik.Windows.Controls.RadGridView radgridview, DataTable tempDataTable, Variables.OperationTrypes oprationType)
        {
            string txtline = string.Empty;
            NecessaryFunction necessaryElement = new NecessaryFunction();
            GridViewExportOptions exportOption = new GridViewExportOptions();
            exportOption.Format = ExportFormat.Text;
            string fileNameWithPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DATA_EXPORT.txt");
            necessaryElement.CreateTextFile(fileNameWithPath);
            FileStream fileStream = new FileStream(fileNameWithPath, FileMode.OpenOrCreate, FileAccess.Write);
            radgridview.Export((Stream)fileStream, exportOption);
            fileStream.Close();
            fileStream.Dispose();
            StreamReader txtReader = new StreamReader(fileNameWithPath);
            while ((txtline=txtReader.ReadLine())!=null)
            {
                char[] delimiterChars = { ' ', '"', '\t' };
                String[] columnData = txtline.Split(delimiterChars,StringSplitOptions.RemoveEmptyEntries);
                if (oprationType.Equals(Variables.OperationTrypes.Sales))
                {
                  tempDataTable.Rows.Add(columnData[0].Trim(), columnData[1].Trim(), columnData[2].Trim(), columnData[3].Trim());
                }
                else if (oprationType.Equals(Variables.OperationTrypes.InvPurDebCre))
                {
                    tempDataTable.Rows.Add(columnData[0].Trim(), columnData[3].Trim(), columnData[4].Trim(), columnData[5].Trim(), columnData[6].Trim(), columnData[7].Trim(), columnData[8].Trim(), columnData[9].Trim(), columnData[11].Trim(), columnData[10].Trim());
                }
            }
            txtReader.Close();
            txtReader.Dispose();
            necessaryElement.DeleteFile(fileNameWithPath);
            return tempDataTable;
        }
        // Invoice Preview
        public bool InvoicePreview(Telerik.Windows.Controls.RadGridView radgridview, string invoiceNumbers, string customerName, string customerAddress, string total, string creditorsID, string paidAmount, string creaditAmount)
        {
            try
            {
                Procesta_Petunia.DataSet.DataSetPetunia crystalReportDataSet = new DataSet.DataSetPetunia();
                CristalReportDataSourch(radgridview, crystalReportDataSet.InvoiceMaker, Variables.OperationTrypes.Sales);
                InvoiceMakerCrystalReport invoicereport = new InvoiceMakerCrystalReport();
                invoicereport.SetDataSource(crystalReportDataSet);
                invoicereport.SetParameterValue("CompanyName", Variables.CompanyInfo[0]);
                invoicereport.SetParameterValue("CompanyTitle", Variables.CompanyInfo[1]);
                invoicereport.SetParameterValue("CompanyAddress", Variables.CompanyInfo[2]);
                invoicereport.SetParameterValue("InvoiceNumber", invoiceNumbers);
                invoicereport.SetParameterValue("CustomerName", customerName);
                invoicereport.SetParameterValue("CustomerAddress", customerAddress);
                invoicereport.SetParameterValue("CustomerId", creditorsID);
                invoicereport.SetParameterValue("TotalAmount", total);
                invoicereport.SetParameterValue("PayedAmount", paidAmount);
                invoicereport.SetParameterValue("CreditAmount", creaditAmount);
                invoicereport.SetParameterValue("AmountInText", new NumberToEnglish().changeNumericToWords(total));

                ReportViewer crystalReportViewer = new ReportViewer();
                crystalReportViewer.ReportViewerView.ViewerCore.ReportSource = invoicereport;
                crystalReportViewer.ShowDialog();
                crystalReportViewer.ReportViewerView.ViewerCore.CloseView(invoicereport);
                crystalReportViewer.ReportViewerView.ViewerCore.Dispose();
                invoicereport.Close();
                invoicereport.Dispose();
                crystalReportDataSet.Dispose();
                return true;
            }
            catch 
            {

                return false;
            }
        }
    }
}
