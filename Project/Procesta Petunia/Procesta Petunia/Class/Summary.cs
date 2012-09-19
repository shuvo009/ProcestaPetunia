//Title :           Petunia.
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Hold Summary of a day and Binding with summary Panel
using System;                                                                                                                                                                                                                    
using System.ComponentModel;                                                                                                                                                                                                                 
namespace ProcestaAccountingFunction                                                                                       
{                                                                                                                          
    public class Summary : INotifyPropertyChanged                                                                          
    {                                                                                                                      
                                                                                                                           
        #region Property                                                                                                       
        public string PurchasQuantity 
        {
            get { return this._PurchaseQuantity; }
            set
            {
                this._PurchaseQuantity = value;
                this.OnPropertyChanges("purchasQuantity");
            }
        }                                                                                       
        public string PurchaseAmount
        {
            get { return this._PurchaseAmount; }
            set
            {
                this._PurchaseAmount = value;
                this.OnPropertyChanges("PurchaseAmount");
            }
        }
        public string SalesQuantity
        {
            get { return this._SalesQuantaty; }
            set 
            {
                this._SalesQuantaty = value;
                this.OnPropertyChanges("SalesQuantity");
            }
        }
        public string SalesAmount
        {
            get { return this._SalesAmount; }
            set
            {
                this._SalesAmount = value;
                this.OnPropertyChanges("SalesAmount");
            }
        }
        public string StartingCash
        {
            get { return this._StartingCash; }
            set 
            {
                this._StartingCash = value;
                this.OnPropertyChanges("StartingCash");
            }
        }
        public string EndingCash
        {
            get { return this._EndingCash;}
            set
            {
                this._EndingCash = value;
                this.OnPropertyChanges("EndingCash");
            }
        }
        public string Expanses
        {
            get { return this._Expanses; }
            set
            {
                this._Expanses = value;
                this.OnPropertyChanges("Expanses");
            }
        }
        public string PuchaseReturnQuantity
        {
            get { return this._PurchaseReturnQuantity; }
            set
            {
                this._PurchaseReturnQuantity = value;
                this.OnPropertyChanges("PuchaseReturnQuantity");
            }
        }
        public string PuchaseReturnAmount
        {
            get { return this._PurchaseReturnAmount; }
            set 
            {
                this._PurchaseReturnAmount = value;
                this.OnPropertyChanges("PuchaseReturnAmount");
            }
        }
        public string SalesReturnQuantity
        {
            get { return this._SalesReturnQuantity; }
            set
            {
                this._SalesReturnQuantity = value;
                this.OnPropertyChanges("SalesReturnQuantity");
            }
        }
        public string SalesReurnAmount
        {
            get { return this._SalesReturnnAmount; }
            set
            {
                this._SalesReturnnAmount = value;
                this.OnPropertyChanges("SalesReurnAmount");
            }
        }

        #endregion
        #region Property Changeed
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanges(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Private Varibale
        private string _PurchaseQuantity = "0";
        private string _PurchaseAmount = "0";
        private string _SalesQuantaty = "0";
        private string _SalesAmount = "0";
        private string _StartingCash = "0";
        private string _EndingCash = "0";
        private string _Expanses = "0";
        private string _PurchaseReturnQuantity = "0";
        private string _PurchaseReturnAmount = "0";
        private string _SalesReturnQuantity = "0";
        private string _SalesReturnnAmount = "0";
        #endregion
    }
}
