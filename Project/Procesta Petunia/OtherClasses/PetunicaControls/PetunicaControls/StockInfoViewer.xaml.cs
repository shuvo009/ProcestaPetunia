//Title :           User Control.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Show Product Details at Stock View.  
using System;
using System.Windows.Controls;
using System.Data;

namespace PetunicaControls
{
	public partial class StockInfoViewer : UserControl
	{
		public StockInfoViewer()
		{
			this.InitializeComponent();
        }

        public void StockInfoDataContext(DataRow dataRow)
        {
            this.DataContext = new ProductInformation() { ProductID=dataRow[0].ToString(),CompanyName=dataRow[1].ToString(),ProductName=dataRow[2].ToString(),ModelNumber=dataRow[3].ToString(),Quantity=dataRow[4].ToString(),Amount=dataRow[5].ToString(),Rate=dataRow[6].ToString()};
        }
    }

    public class ProductInformation
    {
        public string ProductID { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string ModelNumber { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }
}