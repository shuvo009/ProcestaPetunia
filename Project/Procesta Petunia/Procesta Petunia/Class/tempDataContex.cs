//Title :           Petunia
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All data binding goes from Here 
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Procesta_Petunia.Class
{
   public class tempDataContex
    {
      
    }

   #region Invoice Items
   public class InvoiceList : ObservableCollection<InvoiceItems>
    {
        public InvoiceList(): base()
        {
        } 
    }

    //For make Invoice Variable Class
    public class InvoiceItems : INotifyPropertyChanged
   {
       #region Private Variables
       private double _Rate;
       private int _Quantity;
       private double _Amount;
       private string _Description;
       #endregion

       public InvoiceItems(string productID,string Description,  int quantity, double rate, double Amount)
       {
           this.ProductID = productID;
           this.Description=Description;
           this.Quantity = quantity;
           this.Rate = rate;
           this.Amount = Amount;
       }

       #region Properties
       public string ProductID { get; set; }

       public string Description
       {
           get { return this._Description; }
           set 
           { 
               this._Description = value;
               this.OnPropertyChanges("Description"); 
           }
       }
       public double Rate
       {
           get { return this._Rate; } 
           set 
           {
               this._Rate = value;
               this.OnPropertyChanges("Rate");
           }
       }

       public int Quantity
       {
           get { return this._Quantity; }
           set 
           {
               this._Quantity = value;
               this.OnPropertyChanges("Quantity");
           }
       }

       public double Amount
       {
           get { return this._Amount; }
           set
           {
               this._Amount = value;
               this.OnPropertyChanges("Amount");
           }
       }

       #endregion

       #region Property Changeed
       public event PropertyChangedEventHandler PropertyChanged;

       private void OnPropertyChanges(string propertyName)
       {
           if (this.PropertyChanged!=null)
           {
               this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
           }
       }
       #endregion
   }
    #endregion

    public class ProductHistory
    {
        public string EntryDate { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public string ModelName { get; set; }
        public string ProductID { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
        public string InvoiceNumber { get; set; }
        public string Transaction { get; set; }

        public ProductHistory(string EntryDate, string companyName, string ProductName, string ModelName, string ProductID, string Quantity, string Rate, string Amount, string InvoiceNumber, string Transaction)
        {
            this.EntryDate = EntryDate;
            this.CompanyName = companyName;
            this.ProductName = ProductName;
            this.ModelName = ModelName;
            this.ProductID = ProductID;
            this.Quantity = Quantity;
            this.Rate = Rate;
            this.Amount = Amount;
            this.Transaction = Transaction;
            this.InvoiceNumber = InvoiceNumber;
        }
    }


    public class LoadProductHistory : ObservableCollection<ProductHistory>
    {
        public LoadProductHistory() : base() { }
    }
}
