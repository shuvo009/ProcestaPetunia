//Title :           User Control.
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Show Product by Group at Stock View.  
using System;
using System.Windows.Controls;

namespace PetunicaControls
{
	/// <summary>
	/// Interaction logic for StockInfoByGroup.xaml
    /// All Property`s are Read only
	/// </summary>
	public partial class StockInfoByGroup : UserControl
	{
		public StockInfoByGroup()
		{
			this.InitializeComponent();
		}

        public void StockInfoGroupDataContext(System.Data.DataRow dataRow)
        {
            this.DataContext = new StockInfoGroup() { Title=dataRow[0].ToString(),TotalModel=dataRow[1].ToString(),TotalQuantity=dataRow[2].ToString(),TotalAmount=dataRow[3].ToString()};
        }
    }

    public class StockInfoGroup
    {
        public string Title { get; set; }
        public string  TotalModel { get; set; }
        public string TotalQuantity { get; set; }
        public string  TotalAmount { get; set; }
    }
}