//Title :           Petunia Control.
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Product Info With DevExpress ComboBox
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProcestaVariables;
using PetuniaElements;
namespace PetunicaControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ProductInfo : UserControl
    {
        public ProductInfo()
        {
            InitializeComponent();
        }
        #region public mathords
        #region Methords
        //Only Product Name Work
        public bool ProductNameLoad()
        {
            try
            {
                this.AllDisable();
                new ComboBoxDataLoader().ComboBoxItemLoad(this.comboBoxProductName, string.Format("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", Variables.COLUMN_NAME[10], Variables.TABLE_NAME[11]));
                this.comboBoxProductName.IsEnabled = true;
                return true;
            }
            catch
            {

                return false;
            }
        }
        //Only Model Number Work
        public bool ModelNumberLoad()
        {
            try
            {
                this.AllDisable();
                new ComboBoxDataLoader().ComboBoxItemLoad(this.comboBoxModelNumber, string.Format("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", Variables.COLUMN_NAME[11], Variables.TABLE_NAME[11]));
                this.comboBoxModelNumber.IsEnabled = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        //comboBox Company Name load
        public void CompanyNameLoader()
        {
            new ComboBoxDataLoader().CompanyNameLoader(this.comboBoxCompanyName);
        }
        #endregion

        #region Functions
        //Diable All comboBox
        public void AllDisable()
        {
            this.comboBoxCompanyName.IsEnabled = false;
            this.comboBoxProductName.IsEnabled = false;
            this.comboBoxModelNumber.IsEnabled = false;
        }
        //Enable All ComboBox
        public void AllEnable()
        {
            this.comboBoxCompanyName.IsEnabled =true;
            this.comboBoxProductName.IsEnabled = true;
            this.comboBoxModelNumber.IsEnabled = true;
        }
        //Clear All ComboBox Text
        public void ClearAllText()
        {
            this.comboBoxCompanyName.Text = string.Empty;
            this.comboBoxProductName.Text = string.Empty;
            this.comboBoxModelNumber.Text = string.Empty;
        }
        #endregion

        #region Propites
        //Get OR Set Company Name
        public string CompanyNameIs
        {
            get
            {
                return this.comboBoxCompanyName.Text;
            }
            set
            {
                this.comboBoxCompanyName.Text = value;
            }
        }
        //Get OR Set Product name
        public string ProductNameIs
        {
            get
            {
                return this.comboBoxProductName.Text;
            }
            set
            {
                this.comboBoxProductName.Text = value;
            }
        }
        //Get OR Set Model Number
        public string ModelNumberIs
        {
            get 
            {
                return this.comboBoxModelNumber.Text;
            }
            set
            {
                this.comboBoxModelNumber.Text = value;
            }
        }
        //comboBox Company Name IsEnable
        public bool CompanyNameIsEnable
        {
            set
            {
                    this.comboBoxCompanyName.IsEnabled = value;
            }
        }
        //comboBox Product Name IsEnable
        public bool ProductNameIsEnable
        {
            set
            {
                this.comboBoxProductName.IsEnabled = value;
            }
        }
        //comboBox ModelNumber IsEbale
        public bool ModelNumberIsEnable
        {
            set
            {
                this.comboBoxModelNumber.IsEnabled = value;
            }
        }
        //return Description
        public string DescriptionIs
        {
            get
            {
                return Description();
            }
        }
        #endregion
        #endregion

        #region private methord
        #region Function
        //show Description 
        private string Description()
        {
            return string.Format("{0}/{1}/{2}",this.comboBoxCompanyName.Text,this.comboBoxProductName.Text,this.comboBoxModelNumber.Text);
        }
        #endregion

        #region Event Handerlar
        //ComboBox CompanyName Select Index Change
        private void comboBoxCompanyNameSelectIndexChange(object sender, RoutedEventArgs e)
        {
            this.comboBoxModelNumber.Text = string.Empty;
            new ComboBoxDataLoader().ProductNameLoader(this.comboBoxProductName, this.comboBoxCompanyName.Text);
        }
        //ComboBox Product Name Select Index Change
        private void comboBoxProductNameIndexChange(object sender, RoutedEventArgs e)
        {
            new ComboBoxDataLoader().ModelNumberLoader(this.comboBoxModelNumber, this.comboBoxCompanyName.Text, this.comboBoxProductName.Text);
        }
        //ComboBox Model Number IndexChange
        private void comboBoxModelNumberSelectIndexChange(object sender, RoutedEventArgs e)
        {
            if (this.comboBoxModelNumber.Text!=string.Empty)
            {
                OnModelNumberIndexChanged(EventArgs.Empty);
            }
        }
        //ComboBox Company Name Key Down
        private void comboBoxCompanyNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                OnCompanyNameKeyDown(EventArgs.Empty);
            }
        }
        //ComboBox Product Name Key Down
        private void comboBoxProductNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                OnProductNameKeyDown(EventArgs.Empty);
            }
        }
        //ComboBox Model Number Key Down
        private void comboBoxModelNumberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                OnModelNumberKeyDown(EventArgs.Empty);
            }
        }
        #endregion
        #endregion

        #region Control EventHandlar
        //Model Number Index Change
        public event EventHandler ModelNumberIndexChanged;
        //Company Name Key Down
        public event EventHandler CompanyNameEnterKeyDown;
        //Product Name Key Down
        public event EventHandler ProductNameEnterKeyDown;
        //Model Name Key Down
        public event EventHandler ModelNumberEnterKeyDown;

        #endregion

        #region Protected Methords
        //Model Number Index Change
        protected virtual void OnModelNumberIndexChanged(EventArgs e)
        {
            if (ModelNumberIndexChanged !=null)
            {
                ModelNumberIndexChanged(this,e);
            }
        }
        // Company Name Key Down
        protected virtual void OnCompanyNameKeyDown(EventArgs e)
        {
            if (CompanyNameEnterKeyDown!=null)
            {
                CompanyNameEnterKeyDown(this, e);
            }
        }
        //Product Name Key Down
        protected virtual void OnProductNameKeyDown(EventArgs e)
        {
            if (ProductNameEnterKeyDown!=null)
            {
                ProductNameEnterKeyDown(this,e);
            }
        }
        //Model Number Key Down
        protected virtual void OnModelNumberKeyDown(EventArgs e)
        {
            if (ModelNumberEnterKeyDown!=null)
            {
                ModelNumberEnterKeyDown(this,e);
            }
        }

        #endregion

    }
}
