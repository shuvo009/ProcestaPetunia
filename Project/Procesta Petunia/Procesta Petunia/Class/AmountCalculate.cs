//Title :           Petunia.
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     DataBinding With Function to calculate amount and due amount.
using System;
using System.ComponentModel;
namespace Procesta_Petunia.Class
{
    public class AmountCalculate : INotifyPropertyChanged
    {
        #region Constructor
        public AmountCalculate()
        { }
        public AmountCalculate(string quantity, string rate)
        {
            AmountCalculator(quantity,new ProcestaNecessaryFunction.NecessaryFunction().ComaSpriter(rate));
        }
        #endregion

        #region Properties
        private string _AmountIs;
        private string _PaymentAmountIs="0";
        private string _DueAmountIs;

        public string PaymentAmountIs
        { 
            get{return _PaymentAmountIs;}
            set 
            {
                _PaymentAmountIs = value;
                this.DueAmountCalculator(new ProcestaNecessaryFunction.NecessaryFunction().ComaSpriter(value).ToString());
                this.OnPropertyChanged("PaymentAmountIs");
            }
        }
        public string AmountIs
        {
            get { return _AmountIs; }
            set
            {
                _AmountIs = value;
                OnPropertyChanged("AmountIs");
                this.DueAmountCalculator(PaymentAmountIs.ToString());
            }
        }
        public string DueAmountIs
        {
            get { return _DueAmountIs; }
            set 
            {
                _DueAmountIs = value;
                this.OnPropertyChanged("DueAmountIs");
            }
        }
        #endregion

        #region Method
        private void AmountCalculator(string quantity, double rate)
        {
            try
            {
                 string totalAmountIs=(Convert.ToInt32(quantity) * rate).ToString();
                 AmountIs = totalAmountIs;
            }
            catch
            {
                AmountIs = "0";
            }
        }

        private void DueAmountCalculator(string paymentAmount)
        {
            try
            {
                double dueamountIs = Convert.ToDouble(AmountIs) - Convert.ToDouble(paymentAmount);
                if (dueamountIs>=0)
                {
                    DueAmountIs= dueamountIs.ToString();
                }
                else
                {
                    DueAmountIs= "0";
                }
            }
            catch
            {

                DueAmountIs = "0";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                // Raise the PropertyChanged event
                this.PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

    }
}
