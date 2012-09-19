//Title :           Petunia Main Window.
//Version :         2.0.0.1
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All Contain are Call Here  
using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MysqlClass;
using Procesta_Petunia.Class;
using ProcestaVariables;
using ProcestaNecessaryFunction;
using ProcestaAccountingFunction;
using Procesta_Petunia.DataSet;
using PetuniaElements;
using Telerik.Windows.Controls;
namespace Procesta_Petunia
{
    public partial class MainWindow : Window
    {
        #region Local Varibale
        private Variables.OperationTrypes operationType = new Variables.OperationTrypes();
        private string invoiceNumber = string.Empty;
        public static  DispatcherTimer timerLoginCheck = new DispatcherTimer();
        public static DataSetPetunia petuniaDataSet = new DataSetPetunia();
        #endregion
        public MainWindow()
        {
            timerLoginCheck.Tick += new EventHandler(WindowPropartyChecker);
            timerLoginCheck.Interval = new TimeSpan(0,0,0,0,5);
            timerLoginCheck.Start();
            GridViewInvoice = new InvoiceList();
            GridViewLoadProductHistory = new LoadProductHistory();
            TotalSummary = new Summary();
            InitializeComponent();
            this.mainwindowTitleBar.CurrentWindow = App.Current.Windows[1];
        }
        //WindowPropartyChecker Trick (Timer)
        private void WindowPropartyChecker(object sender,EventArgs e)
        {
            if (Properties.Settings.Default.LoginEnable && Variables.LOGIN_IS)
            {
                timerLoginCheck.IsEnabled = false;
                this.Opacity = .2;
                new WindowLogin().ShowDialog();
            }
            else
            {
                this.Opacity = 1;
            }
        }

        #region Properties
        public InvoiceList GridViewInvoice { get; set; }
        public LoadProductHistory GridViewLoadProductHistory { get; set; }
        public Summary TotalSummary { get; set; }
        #endregion

        #region Tree view
        private void SettingTreeViewSelect(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadTreeViewItem Item = SettinsTreeView.SelectedContainer;
            if (Item.Name.Equals(SettingTreeViewItemGeneral.Name))
            {
                this.SettingPanelHide();
                this.GeneralItemClear();
                this.SettingPanelGeneral.Visibility = Visibility.Visible;
            }
            else if (Item.Name.Equals(SettingTreeViewItemPassword.Name))
            {
                this.SettingPanelHide();
                this.PasswordChangeItemClear();
                this.SettingPanelPasswordChange.Visibility = Visibility.Visible;
            }
            else if (Item.Name.Equals(SettingTreeViewItemDatabase.Name))
            {
                this.SettingPanelHide();
                this.DatabaseSettingItemLoadClear();
                this.SettingPanelDatabaseSetting.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region Menu Event

        #region Ribbon Transport & Home
        //Ribbon-->Home & Transport-->AddStock
        private void RibbonAddStock(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.AddStockItemClear();
            this.PanelStockAdd.Visibility = Visibility.Visible;
            this.StockAddProductInfo.CompanyNameLoader();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.AddStockComboBoxCreditorsName, Variables.ACCOUNT_TYPE[0]);
        }
        //Ribbon-->Home & Transport-->Invoice
        private void RibbonInvoice(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.InvoiceItemClear();
            this.PaneInvoice.Visibility = Visibility.Visible;
            this.InvoiceProductInfo.CompanyNameLoader();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.InvoiceComboBoxDebtorName, Variables.ACCOUNT_TYPE[1]);
        }
        // Ribbon Home & Transport --> Debtors Payment
        private void RibbonDebtorsPayment(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.DebtorsCredtorsPaymentItemClear();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.DebtorsCredtorsPaymentComboBoxName, Variables.ACCOUNT_TYPE[1]);
            this.DebtorCredtorsPaymentLabelHeader.Text = Variables.ACCOUNT_TYPE[1];
            operationType = Variables.OperationTrypes.Debtors;
            this.PanelDebtorsPayment.Visibility = Visibility.Visible;
        }
        //Ribbon Home & Transport -->Creditors Payment
        private void RibbonCreditorsPayment(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.DebtorsCredtorsPaymentItemClear();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.DebtorsCredtorsPaymentComboBoxName, Variables.ACCOUNT_TYPE[0]);
            this.DebtorCredtorsPaymentLabelHeader.Text = Variables.ACCOUNT_TYPE[0];
            operationType = Variables.OperationTrypes.Creditors;
            this.PanelDebtorsPayment.Visibility = Visibility.Visible;
        }
        //Ribbon Home & TransPort --> Product Return
        private void RinnonProductReturn(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.ReturnProductSalesItemClear();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.ReturnProductSalesComboBoxName, Variables.ACCOUNT_TYPE[1]);
            this.ReturnProductSalesProductInfo.CompanyNameLoader();
            this.ReturnProductSalesHeader.Text = Variables.OTHERS_VARIALES[0];
            this.ReturnProductSalesRadioButtonCreditOrDebit.Header = Variables.ACCOUNT_TYPE[0];
            operationType = Variables.OperationTrypes.PurchaseReturn;
            this.PanelReturnProductSales.Visibility = Visibility.Visible;
        }
        //Ribbon Home & TranPort -->Sales Return
        private void RibbonSalesReturn(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.ReturnProductSalesItemClear();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.ReturnProductSalesComboBoxName, Variables.ACCOUNT_TYPE[0]);
            this.ReturnProductSalesProductInfo.CompanyNameLoader();
            this.ReturnProductSalesHeader.Text = Variables.OTHERS_VARIALES[1];
            this.ReturnProductSalesRadioButtonCreditOrDebit.Header = Variables.ACCOUNT_TYPE[1];
            operationType = Variables.OperationTrypes.SalesReturn;
            this.PanelReturnProductSales.Visibility = Visibility.Visible;
        }
        //Ribbon Transport -->Cash Payment
        private void RibbonCashPayment(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.CashRecivePaymentItemClear();
            operationType = Variables.OperationTrypes.CashPayment;
            this.CashReceivePaymentLabelHeader.Text = Variables.OperationTrypes.CashPayment.ToString();
            this.PanelCashReceivePayment.Visibility = Visibility.Visible;
        }
        //Ribbon Transport -->Cash Receive
        private void RibbonCashReceive(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.CashRecivePaymentItemClear();
            operationType = Variables.OperationTrypes.CashReceive;
            this.CashReceivePaymentLabelHeader.Text = Variables.OperationTrypes.CashReceive.ToString();
            this.PanelCashReceivePayment.Visibility = Visibility.Visible;
        }
        //Ribbon Transport -->Expanses
        private void RibbonExpanses(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.ExpansesitemClear();
            this.PanelExpanses.Visibility = Visibility.Visible;
        }
        //Ribbon Transport --> Salary 
        private void RibbonSalary(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.SalaryItemClear();
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.SalaryComboBoxName, Variables.ACCOUNT_TYPE[2]);
            this.PanelSalary.Visibility = Visibility.Visible;
        }
        //Ribbon Home --> Summary 
        private void RibbonSummary(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.PanelSummary.Visibility = Visibility.Visible;
        }
        //Recharge
        private void RibbonHomeButtonRechargeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            new ComboBoxDataLoader().RechargeCompanyNameLoader(this.RechargeAmountComBoxBoxCompayName);
            this.RecharesClear();
            this.PanelRechagreAmount.Visibility = Visibility.Visible;
        }
        #endregion

        #region Ribbon View
        //Ribbon-->View-->Stocks
        private void RibbonStockView(object sender, System.Windows.RoutedEventArgs e)//need clear
        {
            this.HideAllPanel();
            this.StockInfoItemClear();
            this.StockinfoProductInfo.CompanyNameLoader();
            this.PanelStockInfo.Visibility = Visibility.Visible;
        }
        // Ribbon-->View-->Invoice
        private void RibbonInvoiceView(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.PurchaseSalesViewUIDisable();
            this. PurchaseSalesView();
            this.PurchaseSalesViewLabelHeader.Text = Variables.OTHERS_VARIALES[3];
            operationType = Variables.OperationTrypes.Sales;
            this.PurchaseSalesViewProductInfo.CompanyNameLoader();
            this.PanelPurchaseSalesView.Visibility = Visibility.Visible;
        }
        //Ribbon-->View-->Purchase
        private void RibbonPurchaseView(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.PurchaseSalesViewUIDisable();
            this.PurchaseSalesView();
            this.PurchaseSalesViewLabelHeader.Text = Variables.OTHERS_VARIALES[2];
            operationType = Variables.OperationTrypes.Purchase;
            this.PurchaseSalesViewProductInfo.CompanyNameLoader();
            this.PanelPurchaseSalesView.Visibility = Visibility.Visible;
        }
        //Ribbon-->View-->Product Return View
        private void RibbonProductReturnView(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.PurchaseSalesViewUIDisable();
            this.PurchaseSalesView();
            this.PurchaseSalesViewLabelHeader.Text = Variables.OTHERS_VARIALES[0];
            this.PurchaseSalesViewProductInfo.CompanyNameLoader();
            operationType = Variables.OperationTrypes.SalesReturn;
            this.PanelPurchaseSalesView.Visibility = Visibility.Visible;
        }
        //Ribbon-->View-->Seals Return
        private void RibbonSelasReturnView(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.PurchaseSalesViewUIDisable();
            this.PurchaseSalesView();
            this.PurchaseSalesViewLabelHeader.Text = Variables.OTHERS_VARIALES[1];
            this.PurchaseSalesViewProductInfo.CompanyNameLoader();
            operationType = Variables.OperationTrypes.PurchaseReturn;
            this.PanelPurchaseSalesView.Visibility = Visibility.Visible;
        }
        //Ribbon View --> Debtors Payment History
        private void RibbonDebtorsPaymentHistory(object sender, System.Windows.RoutedEventArgs e)
        {
            HideAllPanel();
            this.DebtorsCredtorsViewUIDisable();
            DebtorsCreditorsViewItemClear();
            this.DebtorsCreditorsViewLabelHader.Text = Variables.ACCOUNT_TYPE[1];
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.DebtorsCreditorsViewComboBoxName, Variables.ACCOUNT_TYPE[1]);
            operationType = Variables.OperationTrypes.Debtors;
            this.PanelDebtorsCreditorsView.Visibility = Visibility.Visible;
        }
        //Ribbon View --> Creditor Payment History
        private void RibbonCreditorsPaymentHistory(object sender, System.Windows.RoutedEventArgs e)
        {
            HideAllPanel();
            this.DebtorsCredtorsViewUIDisable();
            this.DebtorsCreditorsViewItemClear();
            this.DebtorsCreditorsViewLabelHader.Text = Variables.ACCOUNT_TYPE[0];
            new ComboBoxDataLoader().AccTypeUserNameLoader(this.DebtorsCreditorsViewComboBoxName, Variables.ACCOUNT_TYPE[0]);
            operationType = Variables.OperationTrypes.Creditors;
            this.PanelDebtorsCreditorsView.Visibility = Visibility.Visible;
        }
        //Ribbon View -->Expanses View
        private void RibbonExpansesView(object sender, System.Windows.RoutedEventArgs e)
        {
            HideAllPanel();
            ExpansesViewItemClear();
            this.PanelExpansesViewUpdate.Visibility = Visibility.Visible;
        }
        //Recharge View
        private void RibbonViewButtonRechargeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            new ComboBoxDataLoader().RechargeCompanyNameLoader(this.RechargeHistoryComboBoxCompanyName);
            this.ReechareHistoryClear();
            this.PanelRechargeHistory.Visibility = Visibility.Visible;
        }
        #endregion

        #region Ribbon Edit
        //Ribbon-->Edit-->Edit Product Info
        private void RibbonProductInfoEdit(object sender, System.Windows.RoutedEventArgs e)
        {
            HideAllPanel();
            EditNewProductItemClear();
            this.EditNewProductProductInfo.CompanyNameLoader();
            this.PanelEditNewProduct.Visibility = Visibility.Visible;
        }
        //Ribbon-->Edit-->Edit Party Info
        private void RibbonPartyinfoEdit(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.EditPartyInfoItemClear();
            new ComboBoxDataLoader().AllUserNameLoader(this.EditPartyInfoComboBoxUserID);
            this.PanelEditPartyInfo.Visibility = Visibility.Visible;
        }
        //Ribbon Edit -->Stock Edit
        private void RibbonStockEdit(object sender, System.Windows.RoutedEventArgs e)
        {
            HideAllPanel();
            StockEditItemClear();
            this.StockEditProductInfo.CompanyNameLoader();
            this.PanelStockEdit.Visibility = Visibility.Visible;
        }
        //Recharge 
        private void RibbonEditButtonRechargesClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            new ComboBoxDataLoader().RechargeCompanyNameLoader(this.EditRechagreComboBoxCompanyName);
            this.EditRechargeClear();
            this.PanelRechargeEditCompanyInfo.Visibility = Visibility.Visible;
        }
        #endregion

        #region Ribbon New
        //New Recharges
        private void RibbonTransportButtonNewRechargesClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.NewRechagreClear();
            this.PanelNewRecharge.Visibility = Visibility.Visible;
        }
        //New Party 
        private void RibbonNewParty(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.NewPartyCreateItemClear();
            this.PanelNewParty.Visibility = Visibility.Visible;
        }
        //NewProduct
        private void RibbonNewProduct(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
            this.NewProductItemClear();
            this.PanelNewProduct.Visibility = Visibility.Visible;
        }
        #endregion

        #region Ribbon Home
        //Ribbon Home--> Home
        private void RibbonButtonHomeClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.HideAllPanel();
			this.HomeImage.Visibility= Visibility.Visible;
        }
        #endregion

        #region BackTag Loan
        // All BackTag Info Load Here
        private void BackTagInitialBalanceLoad(object sender, System.Windows.RoutedEventArgs e)
        {
            this.BackTagInitialBalanceItemClear();
        }
        #endregion

        #endregion

        #region Button Events

        #region Panel Main 
        //Logout Button Click
        private void LogoutButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            timerLoginCheck.IsEnabled = true;
            Variables.LOGIN_IS = true;
        }
        //Menu Exit
        private void MenuExitClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Panel New product
        //New Product Button Click
        private void NewProductCreateButton_Click(object sender, RoutedEventArgs e)
        {
            this.NewProductCreateButton.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
            try
            {
                if (this.NewProductTextBoxCompanyName.Text != string.Empty && this.NewProductTextBoxProductName.Text != string.Empty && this.NewProductTextBoxModelNumber.Text != string.Empty && (this.NewProductTextBoxProductID.Text != string.Empty || this.NewProductCheckBoxAutoGenerate.IsChecked.Equals(true)))
                {

                    if (!this.NewProductCheckBoxAutoGenerate.IsChecked.Equals(true))
                    {
                        if (this.NewProductTextBoxProductID.Text != string.Empty)
                        {
                            if (!mysqlNecessaryFunction.MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[4], this.NewProductTextBoxProductID.Text)))
                            {
                               throw new PetuniaException(Variables.ERROR_MESSAGES[0, 9]);
                            }
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else
                    {
                        this.NewProductTextBoxProductID.Text = mysqlNecessaryFunction.ProductIDIs(Variables.TABLE_NAME[11], Variables.COLUMN_NAME[4]);
                    }
                    this.NewProductTextBoxProductID.Text = new NecessaryFunction().SixDigitNumber(this.NewProductTextBoxProductID.Text);
                    if (!mysqlNecessaryFunction.MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}='{6}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], this.NewProductTextBoxCompanyName.Text, Variables.COLUMN_NAME[10], this.NewProductTextBoxProductName.Text, Variables.COLUMN_NAME[11], this.NewProductTextBoxModelNumber.Text)) | !mysqlNecessaryFunction.DatabaseOperation(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[4], Variables.COLUMN_NAME[4], this.NewProductTextBoxProductID.Text)))
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 10]);
                    }
                    else
                    {
                        if (mysqlNecessaryFunction.DatabaseOperation(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}','{5}','{6}')", Variables.TABLE_NAME[11], this.NewProductTextBoxProductID.Text, DateTime.Now.ToString("yyyy-MM-dd"), this.NewProductTextBoxCompanyName.Text, this.NewProductTextBoxProductName.Text, this.NewProductTextBoxModelNumber.Text, this.NewProductTextBoxDescription.Text)))
                        {
                            mysqlNecessaryFunction.DatabaseOperation(string.Format("INSERT INTO {0} VALUES ('{1}',0,0,0,NULL)", Variables.TABLE_NAME[4], this.NewProductTextBoxProductID.Text));
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                        }
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, ProcestaVariables.Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.NewProductCreateButton.IsEnabled = true;
                mysqlNecessaryFunction.Dispose();
            }        
        }
        //New Product Click Button Click
        private void NewProductClearButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.NewProductClearButton.IsEnabled = false;
            this.NewProductItemClear();
            Mouse.OverrideCursor =null;
            this.NewProductClearButton.IsEnabled =true;
        }
        #endregion

        #region Panel Add Stock
        // Add stock Update Button
        private void AddStockUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AddStockButtonAdd.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
            DataSetPetunia commonUseDataSet = new DataSetPetunia();
            try
            {
                if (this.StockAddProductInfo.CompanyNameIs != string.Empty && this.StockAddProductInfo.ProductNameIs != string.Empty && this.StockAddProductInfo.ModelNumberIs != string.Empty && AddStockLabelProductIDInfo.Text != string.Empty && AddStockNumericUpDown.Value > 0 && AddStockTextBoxRate.Text != string.Empty && AddStockTextBoxInvoiceNUmber.Text != string.Empty && Convert.ToDouble(this.AddStockTextBoxRate.Text) > 0)
                {
                    commonUseDataSet.PetuniaCommonUse.Rows.Add(this.AddStockLabelProductIDInfo.Text, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.AddStockDatePicker), this.AddStockNumericUpDown.Value.ToString(), this.AddStockLabelRateInfo.Text, new NecessaryFunction().ComaSpriter(this.AddStockTextBoxRate.Text), this.AddStockLabelAmountNewInfo.Text, this.AddStockTextBoxInvoiceNote.Text, this.AddStockComboBoxCreditorsName.Text, this.AddStockTextBoxIssueName.Text, this.AddStockTextBoxInvoiceNUmber.Text, this.AddStockLabelCreditAmount.Text, new NecessaryFunction().ComaSpriter(this.AddStockTextBoxPaymentAmount.Text));
                    if (this.AddStockRadioButtonCash.IsChecked.Equals(true))
                    {
                        if (new AccountClass().Purchase(commonUseDataSet.PetuniaCommonUse, TransactionType.Cash, new DBQueres(mysqlNecessaryFunction.DatabaseOperation), new DatabaseRead(mysqlNecessaryFunction.DataReader)))
                        {
                            this.AddStockTextBoxInvoiceNUmber.Text = string.Empty;
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                        }
                    }
                    else if (this.AddStockRadioButtonCheeck.IsChecked.Equals(true))
                    {
                        if (new AccountClass().Purchase(commonUseDataSet.PetuniaCommonUse, TransactionType.check, new DBQueres(mysqlNecessaryFunction.DatabaseOperation), new DatabaseRead(mysqlNecessaryFunction.DataReader)))
                        {
                            this.AddStockTextBoxInvoiceNUmber.Text = string.Empty;
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                        }
                    }
                    else if (this.AddStockReadioButtonCredit.IsChecked.Equals(true))
                    {
                        if (this.AddStockComboBoxCreditorsName.Text != string.Empty && this.AddStockTextBoxPaymentAmount.Text != string.Empty && this.AddStockLabelCreditAmount.Text != string.Empty && Convert.ToDouble(this.AddStockLabelCreditAmount.Text) >= 0)
                        {
                            if (new AccountClass().Purchase(commonUseDataSet.PetuniaCommonUse, TransactionType.Credit, new DBQueres(mysqlNecessaryFunction.DatabaseOperation), new DatabaseRead(mysqlNecessaryFunction.DataReader)))
                            {
                                this.AddStockTextBoxInvoiceNUmber.Text = string.Empty;
                                throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                            }
                            else
                            {
                                throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                            }
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[1, 4]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.AddStockButtonAdd.IsEnabled = true;
                mysqlNecessaryFunction.Dispose();
                commonUseDataSet.Dispose();
            }
        }
        //Add Stock New 
        private void AddStockButtonNewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AddStockButtonNew.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            this.RibbonAddStock(null, null);
            this.AddStockButtonNew.IsEnabled = true;
            Mouse.OverrideCursor = null;
        }
        #endregion

        #region Panel Invoice
        //Add item Invoice
        private void invoiceGridAddClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoiceButtonAdd.IsEnabled = false;
            Mouse.OverrideCursor=Cursors.Wait;
            try
            {
                if (this.InvoiceProductInfo.CompanyNameIs != string.Empty && this.InvoiceProductInfo.ProductNameIs != string.Empty && this.InvoiceProductInfo.ModelNumberIs != string.Empty && this.InvoiceNumberUpDownQuantity.Value > 0 && this.InvoiceNumberUpDownQuantity.Value.ToString() != string.Empty && Convert.ToDouble(this.InvoiceNumberBoxRate.Text) > 0 && this.InvoiceNumberBoxRate.Text != string.Empty)
                {
                    new GridViewModify().ChartItemAdd(GridViewInvoice, this.InvoiceLabelProductIDInfo.Text, this.InvoiceProductInfo.DescriptionIs, this.InvoiceLabelQuantityInfo.Text, this.InvoiceNumberUpDownQuantity.Value.ToString(), this.InvoiceLabelSaleAmount.Text, this.InvoiceNumberBoxRate.Value.ToString(), this.InvoiceLabelQuantityInfo);
                }
                else
                {
                    new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                this.InvoiceButtonAdd.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        //Invoice Save
        private void InvoiceSpritButtonSaveClick(object sender, System.Windows.RoutedEventArgs e) //Should Be error
        {
            this.InvoiceButtonSaveAndPrint.IsEnabled = false;
            DataSetPetunia commonDataUse = new DataSetPetunia();
            try
            {
                if (GridViewInvoice.Count > 0)
                {
                    double paymentAmount = new NecessaryFunction().ComaSpriter(this.InvoiceNumberBoxPaidAmount.Text);
                    string creditAmount = this.InvoieLabelCreditAmount.Text;
                    MessageBoxResult messageBoxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 14], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (messageBoxResult.Equals(MessageBoxResult.Yes))
                    {
                            foreach (InvoiceItems tempInvoiceItem in GridViewInvoice)
                            {
                                commonDataUse.PetuniaCommonUse.Rows.Add(tempInvoiceItem.ProductID, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.InvoiceDateTimePicker), tempInvoiceItem.Quantity, tempInvoiceItem.Rate, tempInvoiceItem.Amount, "NONE", this.InvoiceTextBoxIssueName.Text, invoiceNumber, paymentAmount, this.InvoiceComboBoxDebtorName.Text, creditAmount);
                                paymentAmount = 0;
                                creditAmount = "0";
                                if (this.InvoiceRadioButtonCash.IsChecked.Equals(true))
                                {
                                    if (new AccountClass().Sales(commonDataUse.PetuniaCommonUse, TransactionType.Cash, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                                    {
                                       throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                                    }
                                    else
                                    {
                                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                                    }
                                }
                                else if (this.InvoiceRadioButtonDebtor.IsChecked.Equals(true))
                                {
                                    if (this.InvoiceComboBoxDebtorName.Text != string.Empty && this.InvoiceNumberBoxPaidAmount.Text != string.Empty && this.InvoieLabelCreditAmount.Text != string.Empty && Convert.ToDouble(this.InvoieLabelCreditAmount.Text) >= 0)
                                    {
                                        if (new AccountClass().Sales(commonDataUse.PetuniaCommonUse, TransactionType.Credit, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                                        {
                                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                                        }
                                        else
                                        {
                                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                                        }
                                    }
                                    else
                                    {
                                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                                    }
                                }
                                else if (this.InvoiceRadioButtonCheck.IsChecked.Equals(true))
                                {
                                    if (new AccountClass().Sales(commonDataUse.PetuniaCommonUse, TransactionType.check, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                                    {
                                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                                    }
                                    else
                                    {
                                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                                    }
                                }
                                else
                                {
                                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 13]);
                                }
                            }
                    }                   
                }

                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 14]);
                }
            }
            catch (SuccessfullException success)
            {
                GridViewInvoice.Clear();
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.InvoiceButtonSaveAndPrint.IsEnabled = true;
                commonDataUse.Dispose();
            }
        }
        //Invoice Save and print
        private void InvoiceSplitButtonSaveAndPreviewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoiceButtonSaveAndPrint.IsEnabled = true;
            Mouse.OverrideCursor = Cursors.Wait;
            this.InvoiceSpritButtonSaveClick(null, null);
            Mouse.OverrideCursor = null;
            this.InvoiceSplitButtonPreviewClick(null, null);
            this.InvoiceButtonSaveAndPrint.IsEnabled = true;
        }
        //Invoice Sprit Button Preview
        private void InvoiceSplitButtonPreviewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoiceButtonSaveAndPrint.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.InvoicegridControl.Items.Count > 0)
                {
                    string amountIs = this.InvoicegridControl.AggregateResults["invoiceAmountTotal"].FormattedValue.ToString();
                    if (this.InvoiceRadioButtonCash.IsChecked.Equals(true))
                    {
                        new GridViewModify().InvoicePreview(this.InvoicegridControl, invoiceNumber, this.InvoiceTextBoxCustomerName.Text, this.InvoiceTextBoxBillTo.Text, amountIs.ToString(), this.InvoiceComboBoxDebtorName.Text, amountIs.ToString(), "0");
                    }
                    else if (this.InvoiceRadioButtonDebtor.IsChecked.Equals(true))
                    {
                        new GridViewModify().InvoicePreview(this.InvoicegridControl, invoiceNumber, this.InvoiceTextBoxCustomerName.Text, this.InvoiceTextBoxBillTo.Text, amountIs.ToString(), this.InvoiceComboBoxDebtorName.Text, new NecessaryFunction().ComaSpriter(this.InvoiceNumberBoxPaidAmount.Text).ToString(), this.InvoieLabelCreditAmount.Text);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 13]);
                    }
                }
                else
                {
                   throw new PetuniaException(Variables.ERROR_MESSAGES[0, 14]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.InvoiceButtonSaveAndPrint.IsEnabled = true;
            }
            
        }
        //Invoice New click
        private void InvoiceButtonClearClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoiceButtonClear.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            this.InvoiceItemClear();
            this.InvoiceButtonClear.IsEnabled = true;
            Mouse.OverrideCursor = null;
        }
        #endregion

        #region Panel Edit New Product
        //Edit NewProduct Search Button
        private void EditNewProductSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            string mysqlQuery = string.Empty;
            DataTable tempDataTable = new DataTable();
            try
            {
                if (EditNewProductRadioButtonByProductInfo.IsChecked.Equals(true))
                {
                    if (this.EditNewProductProductInfo.CompanyNameIs != string.Empty && this.EditNewProductProductInfo.ProductNameIs != string.Empty && this.EditNewProductProductInfo.ModelNumberIs != string.Empty)
                    {
                        mysqlQuery = string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}='{6}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], this.EditNewProductProductInfo.CompanyNameIs, Variables.COLUMN_NAME[10], this.EditNewProductProductInfo.ProductNameIs, Variables.COLUMN_NAME[11], this.EditNewProductProductInfo.ModelNumberIs);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                if (EditNewProductRadioButtonByProductID.IsChecked.Equals(true))
                {
                    if (EditNewProductTextBoxProductID.Text != string.Empty)
                    {
                        mysqlQuery = string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[4], new NecessaryFunction().SixDigitNumber(this.EditNewProductTextBoxProductID.Text));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                tempDataTable = new MySqlNaceassaryElement().DataReader(mysqlQuery);
                if (tempDataTable.Rows.Count > (0))
                {
                    DataRow tempTableRow = tempDataTable.Rows[0];
                    this.EditNewProductTextBoxCompanyName.Text = tempTableRow[2].ToString();
                    this.EditNewProductTextBoxProductName.Text = tempTableRow[3].ToString();
                    this.EditNewProductTextBoxModelNumber.Text = tempTableRow[4].ToString();
                    this.EditNewProductTextBoxDescription.Text = tempTableRow[5].ToString();
                    this.EditNewProductLabelProductID.Text = tempTableRow[0].ToString();
                    this.EditNewProductButtonSearch.IsEnabled = true;
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[1, 10]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                tempDataTable.Dispose();
                mysqlQuery = null;
                this.EditNewProductButtonSearch.IsEnabled = true;
            }

        }
        //Edit NewProduct Clear Button
        private void EditNewProductClearClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductTextBoxCompanyName.Text = string.Empty;
            this.EditNewProductTextBoxProductName.Text = string.Empty;
            this.EditNewProductTextBoxModelNumber.Text = string.Empty;
            this.EditNewProductTextBoxDescription.Text = string.Empty;
        }
        //Edit NewProduct Update Button
        private void EditNewProductUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductButtonUpdate.IsEnabled = false;
            MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
            try
            {
                MessageBoxResult tempDiglogBoxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 11], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
                Mouse.OverrideCursor = Cursors.Wait;
                if (tempDiglogBoxResult.Equals(MessageBoxResult.Yes))
                {
                    if (this.EditNewProductTextBoxCompanyName.Text != string.Empty && this.EditNewProductTextBoxProductName.Text != string.Empty && this.EditNewProductTextBoxModelNumber.Text != string.Empty && this.EditNewProductProductInfo.ModelNumberIs != string.Empty)
                    {
                        if (mysqlNecessaryFunction.MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}='{6}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], this.EditNewProductTextBoxCompanyName.Text, Variables.COLUMN_NAME[10], this.EditNewProductTextBoxProductName.Text, Variables.COLUMN_NAME[11], this.EditNewProductTextBoxModelNumber.Text)))
                        {
                            mysqlNecessaryFunction.DatabaseOperation(string.Format("UPDATE {0} SET {1}='{2}',{3}='{4}',{5}='{6}' WHERE {7}='{8}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], this.EditNewProductTextBoxCompanyName.Text, Variables.COLUMN_NAME[10], this.EditNewProductTextBoxProductName.Text, Variables.COLUMN_NAME[11], this.EditNewProductTextBoxModelNumber.Text, Variables.COLUMN_NAME[4], this.EditNewProductLabelProductID.Text));
                            this.EditNewProductProductInfo.CompanyNameLoader();
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 10]);
                        }
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                mysqlNecessaryFunction.Dispose();
                this.EditNewProductButtonUpdate.IsEnabled = true;
            }
        }
        //Edit New Product Info Delete Click
        private void EditNewProductButtonDeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductButtonDelete.IsEnabled = false;
            try
            {
                MessageBoxResult tempMessResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 12], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
                Mouse.OverrideCursor = Cursors.Wait;
                if (tempMessResult.Equals(MessageBoxResult.Yes))
                {
                    if (this.EditNewProductRdioButtonDeleteByProductInfo.IsChecked.Equals(true))
                    {
                        if (this.EditNewProductProductInfo.CompanyNameIs != string.Empty && this.EditNewProductProductInfo.ProductNameIs != string.Empty && this.EditNewProductProductInfo.ModelNumberIs != string.Empty)
                        {
                            new MySqlNaceassaryElement().DatabaseOperation(string.Format("DELETE FROM {0} WHERE {1}='{2}' AND {3}='{4}' AND {5}='{6}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], this.EditNewProductProductInfo.CompanyNameIs, Variables.COLUMN_NAME[10], this.EditNewProductProductInfo.ProductNameIs, Variables.COLUMN_NAME[11], this.EditNewProductProductInfo.ModelNumberIs));
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else if (this.EditNewProductRdioButtonDeleteByCompanyName.IsChecked.Equals(true))
                    {
                        if (this.EditNewProductProductInfo.CompanyNameIs != string.Empty)
                        {
                           new MySqlNaceassaryElement().DatabaseOperation(string.Format("DELETE FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], this.EditNewProductProductInfo.CompanyNameIs));
                           throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else if (this.EditNewProductRdioButtonDeleteByProductName.IsChecked.Equals(true))
                    {
                        if (this.EditNewProductProductInfo.ProductNameIs != string.Empty)
                        {
                          new MySqlNaceassaryElement().DatabaseOperation(string.Format("DELETE FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[10], this.EditNewProductProductInfo.ProductNameIs));
                          throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else if (this.EditNewProductRdioButtonDeleteByModelNumber.IsChecked.Equals(true))
                    {
                        if (this.EditNewProductProductInfo.ModelNumberIs != string.Empty)
                        {
                            new MySqlNaceassaryElement().DatabaseOperation(string.Format("DELETE FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[11], Variables.COLUMN_NAME[11], this.EditNewProductProductInfo.ModelNumberIs));
                           throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.EditNewProductButtonDelete.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region Panel New Party
        //New Party Create Button
        private void NewPartyCreateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NewParyButtonCreate.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
            try
            {
                if (this.NewParyTextBoxUserID.Text != string.Empty && this.NewParyComboBoxAccountType.Text != string.Empty)
                {

                    if (mysqlNecessaryFunction.MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[10], Variables.COLUMN_NAME[3], this.NewParyTextBoxUserID.Text)))
                    {
                        mysqlNecessaryFunction.DatabaseOperation(string.Format("INSERT {0} VALUES ('{1}','{2}','{3}','{4}','{5}','{6}')", Variables.TABLE_NAME[10], this.NewParyTextBoxUserID.Text, this.NewParyComboBoxAccountType.Text, this.NewPartyTextBoxPhoneNumber.Text, this.NewPartyTextBoxEmail.Text, this.NewPartyTextBoxContractPerson.Text, this.NewpartyTextBoxAddress.Text));
                        mysqlNecessaryFunction.DatabaseOperation(string.Format("INSERT INTO {0} VALUES ('{1}',0)", Variables.TABLE_NAME[9], this.NewParyTextBoxUserID.Text));
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[1, 5]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                mysqlNecessaryFunction.Dispose();
                this.NewParyButtonCreate.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        // New party Create Clear Click
        private void NewParyButtonClearClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NewParyButtonClear.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            this.NewPartyCreateItemClear();
            Mouse.OverrideCursor = null;
            this.NewParyButtonClear.IsEnabled = true;
        }
        #endregion

        #region Panel Party Info Edit
        //Edit Party Info Update Button
        private void EditPartyInfoUpdateClickk(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditPartyInfoButtonUpdate.IsEnabled = false;
            try
            {
                if (this.EditPartyInfoComboBoxUserID.Text != string.Empty && this.EditPartyInfoComboBoxAccountType.Text != string.Empty)
                {
                    MessageBoxResult tempMessageResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 11], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
                    Mouse.OverrideCursor = Cursors.Wait;
                    if (tempMessageResult.Equals(MessageBoxResult.Yes))
                    {
                        new MySqlNaceassaryElement().DatabaseOperation(string.Format("UPDATE {0} SET AccountType='{1}',Phone='{2}',Email='{3}',ContractPerson='{4}',Address='{5}' WHERE {6}='{7}'", Variables.TABLE_NAME[10], this.EditPartyInfoComboBoxAccountType.Text, this.EditParyInfoTextBoxPhoneNumber.Text, this.EditPartyInfoTextBoxEmail.Text, this.EditPartyInfoTextBoxControctPerson.Text, this.EditPartyInfoTextBoxAddress.Text, Variables.COLUMN_NAME[3], this.EditPartyInfoComboBoxUserID.Text));
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                this.EditPartyInfoButtonUpdate.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        //Edit party Info Delete click
        private void EditPartyInfoButtonDeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditPartyInfoButtonDelete.IsEnabled = false;
            try
            {
                if (this.EditPartyInfoComboBoxUserID.Text != string.Empty && this.EditPartyInfoComboBoxAccountType.Text != string.Empty)
                {
                    MessageBoxResult tempMessboxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 12], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (tempMessboxResult.Equals(MessageBoxResult.Yes))
                    {
                        Mouse.OverrideCursor = Cursors.Wait;
                        new MySqlNaceassaryElement().DatabaseOperation(string.Format("DELETE FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[10], Variables.COLUMN_NAME[3], this.EditPartyInfoComboBoxUserID.Text));
                        new ComboBoxDataLoader().AllUserNameLoader(this.EditPartyInfoComboBoxUserID);
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[1, 13]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                this.EditPartyInfoButtonDelete.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region Panel Stock Info View
        //Stock Info Search button
        private void StockInfoButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                string fieldNams = string.Format("{7}.{0},{7}.{1},{7}.{2},{7}.{3},{8}.{4},{8}.{5},{8}.{6}", Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[7], Variables.COLUMN_NAME[10], Variables.COLUMN_NAME[11], Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[5], Variables.TABLE_NAME[11], Variables.TABLE_NAME[4]);
                if (this.StockInfoRadioButtonByProductInfo.IsChecked.Equals(true))
                {
                    if (this.StockinfoProductInfo.CompanyNameIs != string.Empty && this.StockinfoProductInfo.ProductNameIs != string.Empty && this.StockinfoProductInfo.ModelNumberIs != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{5}='{6}' AND {1}.{7}='{8}' AND {1}.{9}={2}.{9} ORDER BY {9}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[7], this.StockinfoProductInfo.CompanyNameIs, Variables.COLUMN_NAME[10], this.StockinfoProductInfo.ProductNameIs, Variables.COLUMN_NAME[11], this.StockinfoProductInfo.ModelNumberIs, Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonByCompanyName.IsChecked.Equals(true))
                {

                    if (this.StockinfoProductInfo.ProductNameIs != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{5}={2}.{5} AND {1}.{3}='{4}' ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[10], this.StockinfoProductInfo.ProductNameIs, Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonByProductName.IsChecked.Equals(true))
                {
                    if (this.StockinfoProductInfo.CompanyNameIs != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{5}={2}.{5} AND {1}.{3}='{4}' ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[7], this.StockinfoProductInfo.CompanyNameIs, Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonByModelNumber.IsChecked.Equals(true))
                {
                    if (this.StockinfoProductInfo.ModelNumberIs != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{5}={2}.{5} ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[11], this.StockinfoProductInfo.ModelNumberIs, Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonByProductID.IsChecked.Equals(true))
                {
                    if (this.StockInfotextBoxProductID.Text != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{3}={2}.{3} ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[4], new NecessaryFunction().SixDigitNumber(this.StockInfotextBoxProductID.Text)));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonByAmount.IsChecked.Equals(true))
                {
                    if (this.StockInfoTextBoxAmount.Text != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{5}={2}.{5} AND {2}.{3}<='{4}' ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[1], new NecessaryFunction().ComaSpriter(this.StockInfoTextBoxAmount.Text), Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonByRate.IsChecked.Equals(true))
                {
                    if (this.StockInfoTextBoxRate.Text != string.Empty)
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{5}={2}.{5} AND {2}.{3}<='{4}' ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[5], this.StockInfoTextBoxRate.Text, Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.StockInfoRadioButtonAll.IsChecked.Equals(true))
                {
                    if (this.StockInfoCheckBoxAll.IsChecked.Equals(true))
                    {
                        new PetuniaNecessaryFunction().StockInfoByGroup(this.StockinfoWapPanel, string.Format("SELECT {4}.{0},COUNT({4}.{1}),SUM({5}.{2}) AS TotalQuantity,SUM({5}.{3}) FROM {4},{5} WHERE {4}.{6}={5}.{6} GROUP BY {0} ORDER BY TotalQuantity DESC", Variables.COLUMN_NAME[7], Variables.COLUMN_NAME[11], Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[4]));
                    }
                    else
                    {
                        new PetuniaNecessaryFunction().StockInfo(this.StockinfoWapPanel, string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}={2}.{3} ORDER BY {3}", fieldNams, Variables.TABLE_NAME[11], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[4]));
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 13]);
                }
            }
            catch (PetuniaException error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.StockinfoButtonSearch.IsEnabled = true;
            }
        }
        #endregion

        #region Panel Summary
        private void SummaryButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SummaryButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataTable tempdateTable = new DataTable();
            MySqlNaceassaryElement mysqElement = new MySqlNaceassaryElement();
            try
            {
                if (this.SummaryDate.SelectedDate.ToString() != string.Empty)
                {
                    string tempDate = new PetuniaNecessaryFunction().DateFromRadDatePicker(this.SummaryDate);
                    tempdateTable = mysqElement.DataReader(string.Format("SELECT {0} FROM {1} WHERE {2} BETWEEN '{3}' AND '{4}'", Variables.COLUMN_NAME[1], Variables.TABLE_NAME[2], Variables.COLUMN_NAME[0], new NecessaryFunction().MysqlDateFormate(((DateTime)this.SummaryDate.SelectedDate).AddDays(-1).ToString()), tempDate));
                    if (tempdateTable.Rows.Count > 0)
                    {
                        DataRow tempDataRow = tempdateTable.Rows[0];
                        this.TotalSummary.EndingCash = tempDataRow[0].ToString();
                        try
                        {
                            tempDataRow = tempdateTable.Rows[1];
                            this.TotalSummary.StartingCash = tempDataRow[0].ToString();
                        }
                        catch
                        {
                            this.TotalSummary.StartingCash = Variables.ERROR_MESSAGES[2, 1];
                        }
                    }
                    tempdateTable = mysqElement.DataReader(string.Format("SELECT SUM({0}),SUM({1}) FROM {2} WHERE {3}='{4}'", Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[12], Variables.COLUMN_NAME[0], tempDate));
                    if (tempdateTable.Rows.Count > 0)
                    {
                        DataRow tempDataRow = tempdateTable.Rows[0];
                        this.TotalSummary.PurchasQuantity = tempDataRow[0].ToString();
                        this.TotalSummary.PurchaseAmount = tempDataRow[1].ToString();
                    }
                    tempdateTable = mysqElement.DataReader(string.Format("SELECT SUM({0}),SUM({1}) FROM {2} WHERE {3}='{4}'", Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[14], Variables.COLUMN_NAME[0], tempDate));
                    if (tempdateTable.Rows.Count > 0)
                    {
                        DataRow tempDataRow = tempdateTable.Rows[0];
                        this.TotalSummary.SalesQuantity = tempDataRow[0].ToString();
                        this.TotalSummary.SalesAmount = tempDataRow[1].ToString();
                    }
                    tempdateTable = mysqElement.DataReader(string.Format("SELECT SUM({0}) FROM {1} WHERE {2}='{3}'", Variables.COLUMN_NAME[1], Variables.TABLE_NAME[6], Variables.COLUMN_NAME[0], tempDate));
                    if (tempdateTable.Rows.Count > 0)
                    {
                        DataRow tempDataRow = tempdateTable.Rows[0];
                        this.TotalSummary.Expanses = tempDataRow[0].ToString();
                    }
                    tempdateTable = mysqElement.DataReader(string.Format("SELECT SUM({0}),SUM({1}) FROM {2} WHERE {3}='{4}' AND {5}='Return'", Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[12], Variables.COLUMN_NAME[0], tempDate, Variables.COLUMN_NAME[14]));
                    if (tempdateTable.Rows.Count > 0)
                    {
                        DataRow tempDataRow = tempdateTable.Rows[0];
                        this.TotalSummary.PuchaseReturnQuantity = tempDataRow[0].ToString();
                        this.TotalSummary.PuchaseReturnAmount = tempDataRow[1].ToString();
                    }
                    tempdateTable = mysqElement.DataReader(string.Format("SELECT SUM({0}),SUM({1}) FROM {2} WHERE {3}='{4}' AND {5}='Return'", Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[14], Variables.COLUMN_NAME[0], tempDate, Variables.COLUMN_NAME[14]));
                    if (tempdateTable.Rows.Count > 0)
                    {
                        DataRow tempDataRow = tempdateTable.Rows[0];
                        this.TotalSummary.SalesReturnQuantity = tempDataRow[0].ToString();
                        this.TotalSummary.SalesReurnAmount = tempDataRow[1].ToString();
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);    
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            finally
            {
                tempdateTable.Dispose();
                mysqElement.Dispose();
                Mouse.OverrideCursor = null;
                this.SummaryButtonSearch.IsEnabled = true;
            }
        }
        #endregion

        #region Panel Debitor and Creditor Payment
        //Debtor Payment Search Button Click
        private void DebtorPaymentButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCredtorsPaymentButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.DebtorsCredtorsPaymentTextBoxName.Text != string.Empty)
                {
                    DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}' AND {3}='{4}'", Variables.TABLE_NAME[10], Variables.COLUMN_NAME[3], this.DebtorsCredtorsPaymentTextBoxName.Text, Variables.COLUMN_NAME[12], this.DebtorCredtorsPaymentLabelHeader.Text));
                    if (tempDataTable.Rows.Count > 0)
                    {
                        this.DebtorsCredtorsPaymentComboBoxName.Text = this.DebtorsCredtorsPaymentTextBoxName.Text;
                        tempDataTable.Dispose();
                    }
                    else
                    {
                        tempDataTable.Dispose();
                        throw new PetuniaException(Variables.ERROR_MESSAGES[1, 8]);    
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);    
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.DebtorsCredtorsPaymentButtonSearch.IsEnabled = true;
            }
        }
        //Debtor Payment Payment Click
        private void DebtorPaymentButtonPamentClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCredtorsPaymentButtonPayment.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSetPetunia commonDataUse = new DataSetPetunia();
            bool debtors = false;
            try
            {
                if (this.DebtorsCredtorsPaymentComboBoxName.Text != string.Empty && this.DebtorsCredtorsPaymentNumberBoxPaymentAmount.Text != string.Empty)
                {
                        if (operationType.Equals(Variables.OperationTrypes.Debtors))
                        {
                            debtors = true;
                        }
                        DataTable tempadataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1} WHERE {2}='{3}' ORDER BY {4}", Variables.COLUMN_NAME[4], Variables.TABLE_NAME[3], Variables.COLUMN_NAME[3], this.DebtorsCredtorsPaymentComboBoxName.Text, Variables.COLUMN_NAME[0]));
                        if (tempadataTable.Rows.Count > 0)
                        {
                            DataRow tempDataRow = tempadataTable.Rows[0];
                            commonDataUse.PetuniaCommonUse.Rows.Add(this.DebtorsCredtorsPaymentComboBoxName.Text, tempDataRow[0], new PetuniaNecessaryFunction().DateFromRadDatePicker(this.DebtorsCredtorsPaymentDateAndTimePicker), new NecessaryFunction().ComaSpriter(this.DebtorsCredtorsPaymentNumberBoxPaymentAmount.Text));
                            if (debtors ? new AccountClass().CreditorsAndDebtorsPayment(commonDataUse.PetuniaCommonUse, TransactionType.debt, new DatabaseRead(new MySqlNaceassaryElement().DataReader), new DBQueres(new MySqlNaceassaryElement().DatabaseOperation)) : new AccountClass().CreditorsAndDebtorsPayment(commonDataUse.PetuniaCommonUse, TransactionType.Credit, new DatabaseRead(new MySqlNaceassaryElement().DataReader), new DBQueres(new MySqlNaceassaryElement().DatabaseOperation)))
                            {
                                throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                            }
                            else
                            {
                                throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                            }
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                        }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                commonDataUse.Dispose();
                this.DebtorsCredtorsPaymentButtonPayment.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region Panel Product and Sales Return 
        //Return Product Sales Search Click
        private void ReturnProductSalesButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataTable tempDataTable=new DataTable();
            string tableName;
            try
            {
                if (this.ReturnProductSalesLabelProductID.Text != string.Empty)
                {
                    if (operationType.Equals(Variables.OperationTrypes.PurchaseReturn))
                    {
                        tableName = Variables.TABLE_NAME[12];
                    }
                    else if (operationType.Equals(Variables.OperationTrypes.SalesReturn))
                    {
                        tableName = Variables.TABLE_NAME[14];
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 2]);
                    }
                    if (this.ReturnProductSalesRadioButtonbyAll.IsChecked.Equals(true))
                    {
                        tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {11}.{0},{10}.{1},{10}.{2},{10}.{3},{11}.{4},{11}.{5},{11}.{6},{11}.{7},{11}.{8},{11}.{9} FROM {10},{11} WHERE {11}.{4}='{12}' AND {10}.{4}={11}.{4} ORDER BY {0}", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[7], Variables.COLUMN_NAME[10], Variables.COLUMN_NAME[11], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[5], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[15], Variables.COLUMN_NAME[13], Variables.TABLE_NAME[11], tableName, this.ReturnProductSalesLabelProductID.Text));
                        GridViewLoadProductHistory.Clear();
                        foreach (DataRow tempDataRow in tempDataTable.Rows)
                        {
                            GridViewLoadProductHistory.Add(new ProductHistory(tempDataRow[0].ToString().Substring(0, 9), tempDataRow[2].ToString(), tempDataRow[1].ToString(), tempDataRow[3].ToString(), tempDataRow[4].ToString(), tempDataRow[5].ToString(), tempDataRow[6].ToString(), tempDataRow[7].ToString(), tempDataRow[8].ToString(), tempDataRow[9].ToString()));
                        }
                    }
                    else if (this.ReturnProductSalesRadioButtonByDate.IsChecked.Equals(true))
                    {
                        tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {11}.{0},{10}.{1},{10}.{2},{10}.{3},{11}.{4},{11}.{5},{11}.{6},{11}.{7},{11}.{8},{11}.{9} FROM {10},{11} WHERE {11}.{0}='{12}' AND {10}.{4}={11}.{4} ORDER BY {0}", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[7], Variables.COLUMN_NAME[10], Variables.COLUMN_NAME[11], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[5], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[15], Variables.COLUMN_NAME[13], Variables.TABLE_NAME[11], tableName, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.ReturnProductSalesDateAndTime)));
                        GridViewLoadProductHistory.Clear();
                        foreach (DataRow tempDataRow in tempDataTable.Rows)
                        {
                            GridViewLoadProductHistory.Add(new ProductHistory(tempDataRow[0].ToString().Substring(0, 9), tempDataRow[2].ToString(), tempDataRow[1].ToString(), tempDataRow[3].ToString(), tempDataRow[4].ToString(), tempDataRow[5].ToString(), tempDataRow[6].ToString(), tempDataRow[7].ToString(), tempDataRow[8].ToString(), tempDataRow[9].ToString()));
                        }
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[1, 5]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                tempDataTable.Dispose();
                tableName = null;
                Mouse.OverrideCursor = null;
                this.ReturnProductSalesButtonSearch.IsEnabled = true;
            }
        }
        //Return Product Sales Chart Add button Click
        private void ReturnProductSalesButtonAddClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesButtonAdd.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.GridViewInvoice.Count > 0)
                {
                    if (this.ReturnProductSalesProductInfo.CompanyNameIs != string.Empty && this.ReturnProductSalesProductInfo.ProductNameIs != string.Empty && this.ReturnProductSalesProductInfo.ModelNumberIs != string.Empty && this.ReturnProductSalesNumericReturnQuantity.Value > 0 && this.ReturnProductSalesNumericReturnQuantity.Value.ToString() != string.Empty && Convert.ToDouble(this.ReturnProductSalesLabelQuantity.Text) > 0)
                    {
                        string productRate = this.ReturnProductSalesLabelRateHistory.Text != string.Empty ? ReturnProductSalesLabelRateHistory.Text : ReturnProductSalesLabelRate.Text;
                        new GridViewModify().ChartItemAdd(GridViewInvoice, this.ReturnProductSalesLabelProductID.Text, this.ReturnProductSalesProductInfo.DescriptionIs, this.ReturnProductSalesLabelQuantity.Text, this.ReturnProductSalesNumericReturnQuantity.Value.ToString(), new NecessaryFunction().AmountIs(productRate, this.ReturnProductSalesNumericReturnQuantity.Value.ToString()), this.ReturnProductSalesLabelRateHistory.Text, this.ReturnProductSalesLabelQuantity);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 14]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.ReturnProductSalesButtonAdd.IsEnabled = true;
            }
        }
        //Return Product Sales Save Button Click
        private void ReturnProductSalesButtonSaveClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesButtonSave.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSetPetunia tempDataSet = new DataSetPetunia();
            try
            {
                if (GridViewInvoice.Count > 0)
                {
                    double paymentAmountIs = new NecessaryFunction().ComaSpriter(this.ReturnProductSalesNumberBoxPaymentAmountCreditOrDebit.Text);
                    string creditAmountIs = this.ReturnProductSalesLabelCreditAmount.Text;
                    foreach (InvoiceItems returnproductItems in GridViewInvoice)
                    {
                        tempDataSet.PetuniaCommonUse.Rows.Add(returnproductItems.ProductID, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.ReturnProductSalesDateAndTime), returnproductItems.Quantity, returnproductItems.Rate, returnproductItems.Amount, this.ReturnProductSalesComboBoxName.Text, this.ReturnProductSalesLabelInvoiceNumber.Text, creditAmountIs, paymentAmountIs);
                        paymentAmountIs = 0;
                        creditAmountIs = "0";
                        if (this.ReturnProductSalesRadioButtonCash.IsChecked.Equals(true))
                        {
                            if (operationType.Equals(Variables.OperationTrypes.PurchaseReturn) ? new AccountClass().PurchaseReturn(tempDataSet.PetuniaCommonUse, TransactionType.Cash, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)) : new AccountClass().SalesReturn(tempDataSet.PetuniaCommonUse, TransactionType.Cash, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                            {
                                throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                            }
                            else
                            {
                                throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                            }
                        }
                        else if (this.ReturnProductSalesRadioButtonCreditOrDebit.IsChecked.Equals(true))
                        {
                            if (this.ReturnProductSalesComboBoxName.Text != string.Empty && this.ReturnProductSalesNumberBoxPaymentAmountCreditOrDebit.Text != string.Empty)
                            {
                                if (operationType.Equals(Variables.OperationTrypes.PurchaseReturn) ? new AccountClass().PurchaseReturn(tempDataSet.PetuniaCommonUse, TransactionType.Credit, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)) : new AccountClass().SalesReturn(tempDataSet.PetuniaCommonUse, TransactionType.debt, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                                {
                                    throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                                }
                                else
                                {
                                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                                }
                            }
                            else
                            {
                                throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                            }
                        }
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 14]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                tempDataSet.Dispose();
                Mouse.OverrideCursor = null;
                this.ReturnProductSalesButtonSave.IsEnabled = true;
            }
        }
        //Return Product Sales Clear button Click
        private void ReturnProductSalesButtonClearClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ReturnProductSalesItemClear();
        }
        #endregion

        #region Panel Cash receive Payment 
        //Cash Receive Payment Update Click
        private void CashReceivePaymentButtonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.CashReceivePaymentButtonUpdate.IsEnabled=false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSetPetunia tempDataSet = new DataSetPetunia();
            try
            {
                if (this.CashReceivePaymentDateTime.DisplayDate.ToString() != string.Empty && this.CashReceivePaymentTextBoxDescription.Text != string.Empty && this.CashReceivePaymentNumberBoxAmount.Text != string.Empty)
                {
                    tempDataSet.PetuniaCommonUse.Rows.Add(new PetuniaNecessaryFunction().DateFromRadDatePicker(this.CashReceivePaymentDateTime), new NecessaryFunction().ComaSpriter(this.CashReceivePaymentNumberBoxAmount.Text), this.CashReceivePaymentTextBoxDescription.Text);
                    if (operationType.Equals(Variables.OperationTrypes.CashPayment))
                    {
                        if (new AccountClass().CashPayment(tempDataSet.PetuniaCommonUse, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                        {
                            this.CashReceivePaymentNumberBoxAmount.Text = string.Empty;
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                        }
                    }
                    else if (operationType.Equals(Variables.OperationTrypes.CashReceive))
                    {
                        if (new AccountClass().CashReceived(tempDataSet.PetuniaCommonUse, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                        {
                            this.CashReceivePaymentNumberBoxAmount.Text = string.Empty;
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                        }
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                tempDataSet.Dispose();
                Mouse.OverrideCursor = null;
                this.CashReceivePaymentButtonUpdate.IsEnabled = true;
            }
        }
        #endregion

        #region Panel Expance
        //Expanses Update Click
        private void ExpansesButtonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ExpansesButtonUpdate.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSetPetunia tempDataSet = new DataSetPetunia();
            try
            {
                if (this.ExpansesDataAndTime.DisplayDate.ToString() != string.Empty && this.ExpansesComboBoxExpansesType.Text != string.Empty && this.ExpansesNumberBoxAmount.Text != string.Empty)
                {
                    tempDataSet.PetuniaCommonUse.Rows.Add(new PetuniaNecessaryFunction().DateFromRadDatePicker(this.ExpansesDataAndTime), this.ExpansesComboBoxExpansesType.Text, new NecessaryFunction().ComaSpriter(this.ExpansesNumberBoxAmount.Text));
                    if (new AccountClass().Expanses(tempDataSet.PetuniaCommonUse, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                    {
                        this.ExpansesNumberBoxAmount.Text = string.Empty;
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        this.ExpansesNumberBoxAmount.Text = string.Empty;
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                tempDataSet.Dispose();
                this.ExpansesButtonUpdate.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region Panel Stock Edit
        // Stock Edit Update Click
        private void StockEditButtonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockEditButtonUpdate.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.StockEditProductInfo.CompanyNameIs != string.Empty && this.StockEditProductInfo.ProductNameIs != string.Empty && this.StockEditProductInfo.ModelNumberIs != string.Empty && this.StockEditNumberBoxNewRate.Text != string.Empty && this.StockEditNumberUpDownNewQuantity.Value > 0)
                {
                    if (new MySqlNaceassaryElement().DatabaseOperation(string.Format("UPDATE {0} SET {1}={2}, {3}={4}, {5}={6} WHERE {7}='{8}'", Variables.TABLE_NAME[4], Variables.COLUMN_NAME[5], new NecessaryFunction().ComaSpriter(this.StockEditNumberBoxNewRate.Text), Variables.COLUMN_NAME[6], this.StockEditNumberUpDownNewQuantity.Value, Variables.COLUMN_NAME[1], this.StockEditLanelNewAmount.Text, Variables.COLUMN_NAME[4], this.StockEditLabelProductID.Text)))
                    {
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null; 
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.StockEditButtonUpdate.IsEnabled = true;
            }
        }
        #endregion

        #region Panel Salary
        //Salary View Click
        private void SalaryButtonViewClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SalaryButtonView.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.SalaryComboBoxName.Text != string.Empty)
                {
                    if (this.SalaryRadioButtonAll.IsChecked.Equals(true))
                    {
                        this.SalaryGridViewHistory.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1} = '{2}'", Variables.TABLE_NAME[13], Variables.COLUMN_NAME[3], this.SalaryComboBoxName.Text));
                    }
                    else if (this.SalaryRadioButtonLast3Month.IsChecked.Equals(true))
                    {
                        this.SalaryGridViewHistory.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1} = '{2}' AND {3} BETWEEN '{4}' AND '{5}'", Variables.TABLE_NAME[13], Variables.COLUMN_NAME[3], this.SalaryComboBoxName.Text, Variables.COLUMN_NAME[0], new PetuniaNecessaryFunction().ThreeMonthBack(this.SalaryDateAndTime), new PetuniaNecessaryFunction().DateFromRadDatePicker(this.SalaryDateAndTime)));
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.SalaryButtonView.IsEnabled = true;
            }
        }
        //Salary Update Click
        private void SalaryButtonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.SalaryButtonUpdate.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSetPetunia temDataSet = new DataSetPetunia();
            try
            {
                if (this.SalaryComboBoxName.Text != string.Empty && this.SalaryDateAndTime.DisplayDate.ToString() != string.Empty && this.SalaryNumberBoxAmount.Text != string.Empty && this.SalaryTextBoxDescription.Text != string.Empty)
                {
                    temDataSet.PetuniaCommonUse.Rows.Add(this.SalaryComboBoxName.Text, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.SalaryDateAndTime), new NecessaryFunction().ComaSpriter(this.SalaryNumberBoxAmount.Text), this.SalaryTextBoxDescription.Text);
                    if (new AccountClass().Salary(temDataSet.PetuniaCommonUse, new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                    {
                        this.SalaryNumberBoxAmount.Text = string.Empty;
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        this.SalaryNumberBoxAmount.Text = string.Empty;
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                temDataSet.Dispose();
                Mouse.OverrideCursor = null;
                this.SalaryButtonUpdate.IsEnabled = true;
            }
        }
        #endregion

        #region Panel Debtors and Creditors View
        //Debtors Creditors View Button Search History Click
        private void DebtorsCreditorsViewButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                string halfSqlStatmentIs = string.Format("SELECT {5}.{0},{5}.{1},{5}.{2},{6}.{3},{6}.{5} FROM {5},{6}", Variables.COLUMN_NAME[7], Variables.COLUMN_NAME[10], Variables.COLUMN_NAME[11], Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[11], Variables.TABLE_NAME[3]);
                if (this.DebtorsCreditorsViewComboBoxName.Text != string.Empty)
                {
                    if (this.DebtorsCreditorsViewRadioButtonByDate.IsChecked.Equals(true))
                    {
                        if (this.DebtorsCreditorsViewDateAndTime.DisplayDate.ToString() != string.Empty)
                        {
                            this.DebtorsCreditorsViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {4}='{5}' AND {0}='{6}'", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[3], Variables.COLUMN_NAME[3], this.DebtorsCreditorsViewComboBoxName.Text, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.DebtorsCreditorsViewDateAndTime)));
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else if (this.DebtorsCreditorsViewRadioButtonByProductID.IsChecked.Equals(true))
                    {
                        if (this.DebtorsCreditorsViewTextBoxProductID.Text != string.Empty)
                        {
                            this.DebtorsCreditorsViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {4}='{5}' AND {1}='{6}'", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[3], Variables.COLUMN_NAME[3], this.DebtorsCreditorsViewComboBoxName.Text, new NecessaryFunction().SixDigitNumber(this.DebtorsCreditorsViewTextBoxProductID.Text)));
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else if (this.DebtorsCreditorsViewRadioButtonByAmount.IsChecked.Equals(true))
                    {
                        if (this.DebtorsCreditorsViewNumberBoxAmount.Text != string.Empty)
                        {
                            this.DebtorsCreditorsViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {4}='{5}' AND {2}='{6}'", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[3], Variables.COLUMN_NAME[3], this.DebtorsCreditorsViewComboBoxName.Text, new NecessaryFunction().ComaSpriter(this.DebtorsCreditorsViewNumberBoxAmount.Text)));
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                        }
                    }
                    else if (this.DebtorsCreditorsViewRadioButtonByAll.IsChecked.Equals(true))
                    {
                        this.DebtorsCreditorsViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {4}='{5}'", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[1], Variables.TABLE_NAME[3], Variables.COLUMN_NAME[3], this.DebtorsCreditorsViewComboBoxName.Text));
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
            }
            finally
            {
                 this.DebtorsCreditorsViewButtonSearch.IsEnabled = true;
                 Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region Panel Expance View
        //Expanses View Search Click
        private void ExpansesViewbuttonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ExpansesViewbuttonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.ExpansesViewRadioButtonByDate.IsChecked.Equals(true))
                {
                    if (this.ExpansesViewDateAndTime.DisplayDate.ToString() != string.Empty)
                    {
                        this.ExpansesViewGiewView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {0}='{4}'", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[14], Variables.TABLE_NAME[6], new PetuniaNecessaryFunction().DateFromRadDatePicker(this.ExpansesViewDateAndTime)));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.ExpansesViewRadioButtonByType.IsChecked.Equals(true))
                {
                    if (this.ExpansesViewComboBoxName.Text != string.Empty)
                    {
                        this.ExpansesViewGiewView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {4}='{5}'", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[14], Variables.TABLE_NAME[6], Variables.COLUMN_NAME[3], ExpansesViewComboBoxName.Text));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.ExpansesViewRadioButtonByBoth.IsChecked.Equals(true))
                {
                    if (this.ExpansesViewComboBoxName.Text != string.Empty && this.ExpansesViewDateAndTime.DisplayDate.ToString() != string.Empty)
                    {
                        this.ExpansesViewGiewView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0},{1},{2} FROM {3} WHERE {4}='{5}' AND {0}={6}", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[14], Variables.TABLE_NAME[6], Variables.COLUMN_NAME[3], this.ExpansesViewComboBoxName.Text, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.ExpansesViewDateAndTime)));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            { 
                 this.ExpansesViewbuttonSearch.IsEnabled = true;
                 Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region BackTag
        //Initial Balance Update Click
        private void InitalBalanceButtonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.InitalBalanceButtonUpdate.IsEnabled = false;
            try
            {
                if (this.InitalBalanceNumberBoxAmount.Text != string.Empty && (this.InitalBalanceRadioButtonBank.IsChecked.Equals(true) || this.InitalBalanceRadioButtonCash.IsChecked.Equals(true)))
                {
                    if (this.InitalBalanceRadioButtonCash.IsChecked.Equals(true) ? new AccountClass().InitialCashBalance(new NecessaryFunction().ComaSpriter(this.InitalBalanceNumberBoxAmount.Text).ToString(), new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)) : new AccountClass().InitialBankBalance(new NecessaryFunction().ComaSpriter(this.InitalBalanceNumberBoxAmount.Text).ToString(), new DBQueres(new MySqlNaceassaryElement().DatabaseOperation), new DatabaseRead(new MySqlNaceassaryElement().DataReader)))
                    {
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.InitalBalanceButtonUpdate.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        #endregion

        #region PanelPurchaseSalesView
        //Purchase Sales View Search button click
        private void PurchaseSalesViewButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                string tableName = operationType.Equals(Variables.OperationTrypes.Purchase) || operationType.Equals(Variables.OperationTrypes.PurchaseReturn) ? Variables.TABLE_NAME[12] : Variables.TABLE_NAME[14];
                string ResturnOrSeals = operationType.Equals(Variables.OperationTrypes.Purchase) || operationType.Equals(Variables.OperationTrypes.Sales) ? string.Format("{0}.{1} NOT IN ('Return')", tableName, Variables.COLUMN_NAME[14], Variables.OTHERS_VARIALES[0]) : string.Format("{0}.{1} IN ('Return')", tableName, Variables.COLUMN_NAME[14], Variables.OTHERS_VARIALES[0]);
                string fieldsName = string.Format("{11}.{0},{11}.{1},{11}.{2},{11}.{3},{11}.{4},{12}.{5},{12}.{6},{12}.{7},{12}.{8},{12}.{9},{12}.{10}", Variables.COLUMN_NAME[0], Variables.COLUMN_NAME[4], Variables.COLUMN_NAME[7], Variables.COLUMN_NAME[10], Variables.COLUMN_NAME[11], Variables.COLUMN_NAME[6], Variables.COLUMN_NAME[5], Variables.COLUMN_NAME[1], Variables.COLUMN_NAME[13], Variables.COLUMN_NAME[15], Variables.COLUMN_NAME[14], Variables.TABLE_NAME[11], tableName);
                if (this.PurchaseSalesViewRadioButtonByProductInfo.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewProductInfo.CompanyNameIs != string.Empty && this.PurchaseSalesViewProductInfo.ProductNameIs != string.Empty && this.PurchaseSalesViewProductInfo.ModelNumberIs != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{5}='{6}' AND {1}.{7}='{8}' AND {1}.{9}={2}.{9} AND {10} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[7], this.PurchaseSalesViewProductInfo.CompanyNameIs, Variables.COLUMN_NAME[10], this.PurchaseSalesViewProductInfo.ProductNameIs, Variables.COLUMN_NAME[11], this.PurchaseSalesViewProductInfo.ModelNumberIs, Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonByCompanyName.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewProductInfo.ProductNameIs != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{5}={2}.{5} AND {6} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[10], this.PurchaseSalesViewProductInfo.ProductNameIs, Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonByProductName.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewProductInfo.CompanyNameIs != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{5}={2}.{5} AND {6} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[7], this.PurchaseSalesViewProductInfo.CompanyNameIs, Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonByModelNumber.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewProductInfo.ModelNumberIs != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}='{4}' AND {1}.{5}={2}.{5} AND {6} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[11], this.PurchaseSalesViewProductInfo.ModelNumberIs, Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonByDate.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewDateAndTime.DisplayDate.ToString() != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {2}.{3}='{4}' AND {1}.{5}={2}.{5} AND {6} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[0], new PetuniaNecessaryFunction().DateFromRadDatePicker(this.PurchaseSalesViewDateAndTime), Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonByAmount.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewNumberBoxAmount.Text != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {2}.{3}<='{4}' AND {1}.{5}={2}.{5} AND {6} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[1], new NecessaryFunction().ComaSpriter(this.PurchaseSalesViewNumberBoxAmount.Text), Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonByQuantity.IsChecked.Equals(true))
                {
                    if (this.PurchaseSalesViewNumberBoxQuantity.Text != string.Empty)
                    {
                        this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {2}.{3}='{4}' AND {1}.{5}={2}.{5} AND {6} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[6], new NecessaryFunction().ComaSpriter(this.PurchaseSalesViewNumberBoxQuantity.Text), Variables.COLUMN_NAME[4], ResturnOrSeals));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.PurchaseSalesViewRadioButtonUpByAll.IsChecked.Equals(true))
                {
                    this.PerchaseSalesViewGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1},{2} WHERE {1}.{3}={2}.{3} AND {4} ORDER BY {3}", fieldsName, Variables.TABLE_NAME[11], tableName, Variables.COLUMN_NAME[4], ResturnOrSeals));
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[1, 4]);
                }
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.PurchaseSalesViewButtonSearch.IsEnabled = true;
                Mouse.OverrideCursor = null;
            }
        }
        //Purchase Sales View Print Button Click
        private void PurchaseSalesViewButtonPrintClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewButtonPrint.IsEnabled = false;
            new GridViewModify().InvPurDebCre(this.PerchaseSalesViewGridView);
            this.PurchaseSalesViewButtonPrint.IsEnabled =true;
        }
        #endregion

        #region Panel New Rechagre
        //Insert Button Click
        private void NewRecgaresButtonInsertClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RibbonTransportButtonNewRecharges.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (this.NewRechargeDate.DisplayDate.ToString() != string.Empty && this.NewRecgaresTextBoxCompnayName.Text != string.Empty)
                {
                    if (new MySqlNaceassaryElement().MysqlValueChecker(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[16], Variables.COLUMN_NAME[7], this.NewRecgaresTextBoxCompnayName.Text)))
                    {
                        if (new MySqlNaceassaryElement().DatabaseOperation(string.Format("INSERT INTO {0} VALUES ('{1}','{2}','{3}','{4}')", Variables.TABLE_NAME[16], this.NewRecgaresTextBoxCompnayName.Text, new PetuniaNecessaryFunction().DateFromRadDatePicker(this.NewRechargeDate), this.NewRecgaresTextBoxContractPerson.Text, this.NewRecgaresTextBoxPhoneNumber.Text)) && new MySqlNaceassaryElement().DatabaseOperation(string.Format("INSERT INTO {0} VALUES ('{1}',0)", Variables.TABLE_NAME[17], this.NewRecgaresTextBoxCompnayName.Text)))
                        {
                            throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                        }
                        else
                        {
                            throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                        }
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[2, 2]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.RibbonTransportButtonNewRecharges.IsEnabled = true;
            }
        }
        //Clear Button Click
        private void NewRecgaresButtonClearClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NewRechagreClear();
        }

        #endregion

        #region Panel Edit Recharge
        //Update Click
        private void EditRechagreButtonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditRechagreButtonUpdate.IsEnabled=false;
            Mouse.OverrideCursor=Cursors.Wait;
            try
            {
                if (this.EditRechagreComboBoxCompanyName.Text != string.Empty)
                {
                    if (new MySqlNaceassaryElement().DatabaseOperation(string.Format("UPDATE {0} SET ContractPerson='{1}' , PhoneNumber='{2}' WHERE {3}='{4}'", Variables.TABLE_NAME[16], this.EditRechagreTextBoxContractPerson.Text, this.EditRechagreTextBoxPhoneNumber.Text, Variables.COLUMN_NAME[7], this.EditRechagreComboBoxCompanyName.Text)))
                    {
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.EditRechagreButtonUpdate.IsEnabled = true;
            }
        }
        //Delete Click
        private void EditRechagreButtonDeleteClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditRechagreButtonDelete.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            try
            {
                if (new MySqlNaceassaryElement().DatabaseOperation(string.Format("DELETE FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[16], Variables.COLUMN_NAME[7], this.EditRechagreComboBoxCompanyName.Text)))
                {
                    new ComboBoxDataLoader().RechargeCompanyNameLoader(this.EditRechagreComboBoxCompanyName);
                    throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.EditRechagreButtonDelete.IsEnabled = true;
            }
        }
        #endregion

        #region Panel Recharge
        private void RechargeAmountButonUpdateClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RechargeAmountButonUpdate.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            DataSetPetunia commonDataUse = new DataSetPetunia();
            try
            {
                OperationType tempOperationType;
                if (this.RechargeAmountRedioButtonPurchase.IsChecked.Equals(true))
                {
                    tempOperationType = OperationType.Purchase;
                }
                else if (this.RechargeAmountRedioButtonSales.IsChecked.Equals(true))
                {
                    tempOperationType = OperationType.Sales;
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[1, 4]);
                }
                if (this.RechargeAmountNumicBoxAmount.Text != string.Empty)
                {
                    commonDataUse.PetuniaCommonUse.Rows.Add(new PetuniaNecessaryFunction().DateFromRadDatePicker(this.RechargeAmountDate), this.RechargeAmountComBoxBoxCompayName.Text, new NecessaryFunction().ComaSpriter(this.RechargeAmountNumicBoxAmount.Text));
                    if (new AccountClass().Rechares(commonDataUse.PetuniaCommonUse, tempOperationType, new MySqlNaceassaryElement().DatabaseOperation, new MySqlNaceassaryElement().DataReader))
                    {
                        throw new SuccessfullException(Variables.ERROR_MESSAGES[0, 11]);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 3]);
                    }
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0, 5], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            catch (SuccessfullException success)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(success.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                Mouse.OverrideCursor = null;
                this.RechargeAmountButonUpdate.IsEnabled = true;
                commonDataUse.Dispose();
            }
        }
        #endregion

        #region Panel Recharege History
        private void RechargeHistoryButtonSearchClick(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RechargeHistoryButtonSearch.IsEnabled = false;
            Mouse.OverrideCursor = Cursors.Wait;
            string searchCondition = string.Empty;
            try
            {
                if (this.RechargeHistoryRadioButtonCompanyName.IsChecked.Equals(true))
                {
                    if (this.RechargeHistoryComboBoxCompanyName.Text != string.Empty)
                    {
                        searchCondition = string.Format("{0}='{1}'", Variables.COLUMN_NAME[7], this.RechargeHistoryComboBoxCompanyName.Text);
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else if (this.RechargeHistoryRadioButtonDate.IsChecked.Equals(true))
                {
                    if (this.RechargeHistoryDate.DisplayDate.ToString()!=string.Empty)
                    {
                        searchCondition = string.Format("{0}='{1}'", Variables.COLUMN_NAME[0], new PetuniaNecessaryFunction().DateFromRadDatePicker(this.RechargeHistoryDate));
                    }
                    else
                    {
                        throw new PetuniaException(Variables.ERROR_MESSAGES[0, 5]);
                    }
                }
                else
                {
                    throw new PetuniaException(Variables.ERROR_MESSAGES[1,4]);
                }
                this.RechargeHistoryGridView.ItemsSource = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1}",Variables.TABLE_NAME[18],searchCondition));
            }
            catch (Exception error)
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(error.Message, Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                searchCondition = null;
                Mouse.OverrideCursor = null;
                this.RechargeHistoryButtonSearch.IsEnabled = true;
            }
        }
        #endregion
        #endregion

        #region ComboBox Evens

        #region Panel Invoice
        //Invoice Debtors Name Select Index Change
        private void InvoiceComboBoxDebtorNameSelectIndexChange(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                this.InvoiceNumberBoxPaidAmount.Text = "0";
                this.InvoieLabelCreditAmount.Text = this.InvoicegridControl.AggregateResults["invoiceAmountTotal"].FormattedValue.ToString();
            }
            catch
            {
                this.InvoieLabelCreditAmount.Text = "0";
            }
        }
        #endregion

        #region Panel Edit Party Info
        //Edit Party Info User ID Select Index Change
        private void EditPartyInfoUserIDIndexChaged(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            DataTable tempDatatable = new DataTable();
            tempDatatable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}'",Variables.TABLE_NAME[10],Variables.COLUMN_NAME[3],this.EditPartyInfoComboBoxUserID.Text));
            if (tempDatatable.Rows.Count > 0)
            {
                DataRow tempDataRow = tempDatatable.Rows[0];
                this.EditPartyInfoComboBoxAccountType.Text = tempDataRow[1].ToString();
                this.EditPartyInfoTextBoxControctPerson.Text = tempDataRow[4].ToString();
                this.EditParyInfoTextBoxPhoneNumber.Text = tempDataRow[2].ToString();
                this.EditPartyInfoTextBoxEmail.Text = tempDataRow[3].ToString();
                this.EditPartyInfoTextBoxAddress.Text = tempDataRow[5].ToString();
                tempDatatable.Dispose();
                Mouse.OverrideCursor = null;
                return;
            }
            else
            {
                tempDatatable.Dispose();
                Mouse.OverrideCursor = null;
                return;
            }
        }
        #endregion

        #region Panel Debtors Credtors payment
        //Debtors payment Debtor Name Select Index Change
        private void DebtorsPaymentComboboxDebtornameIndexChange(object sender, System.Windows.RoutedEventArgs e)
        {
            new TextBlockDataLoader().DebtorAndCreditorAccountLoader(this.DebtorsCredtorsPaymentLabelAmount, this.DebtorsCredtorsPaymentComboBoxName.Text);
        }
        #endregion

        #region Panel Debtor Creditor View
        //Debtors Creditors View Name Select Index Change
        private void DebtorsCreditorsViewComboBoxNameIndexChange(object sender, System.Windows.RoutedEventArgs e)
        {
           new TextBlockDataLoader().DebtorsCreditorsAccountInfoViewer(this.DebtorsCreditorsViewLabelPerInfoContractPerson, this.DebtorsCreditorsViewLabelPerPhone, this.DebtorsCreditorsViewLabelPerInfoEmail, this.DebtorsCreditorsViewLabelPerInfoAddress, this.DebtorsCreditorsViewLabelAccInfoAmount, this.DebtorsCreditorsViewComboBoxName.Text);
        }
        #endregion

        #region Panel Return Purchase and Sales
        private void ReturnProductSalesComboBoxNameSelectIndexChange(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                ReturnProductSalesLabelCreditAmount.Text = this.ReturnProductSalesGridViewChart.AggregateResults["returnProductTotalAmount"].FormattedValue.ToString();
            }
            catch
            {
                ReturnProductSalesLabelCreditAmount.Text = "0";
            }
        }
        #endregion

        #region Panel edit Recharge
        private void EditRechagreComboBoxCompanyNameSelectIndexChange(object sender, System.Windows.RoutedEventArgs e)
        {
            DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT * FROM {0} WHERE {1}='{2}'", Variables.TABLE_NAME[16], Variables.COLUMN_NAME[7], this.EditRechagreComboBoxCompanyName.Text));
            if (tempDataTable.Rows.Count>0)
            {
                DataRow tempDataRow=tempDataTable.Rows[0];
                this.EditRechagreTextBoxContractPerson.Text = tempDataRow[2].ToString();
                this.EditRechagreTextBoxPhoneNumber.Text = tempDataRow[3].ToString();
            }
            tempDataTable.Dispose();
        }
        #endregion

        #region Panel Recharege
        private void RechargeAmountComBoxBoxCompayNameSelectIndexChange(object sender, System.Windows.RoutedEventArgs e)
        {
            DataTable tempDataTable = new MySqlNaceassaryElement().DataReader(string.Format("SELECT {0} FROM {1} WHERE {2}='{3}'", Variables.COLUMN_NAME[1], Variables.TABLE_NAME[17], Variables.COLUMN_NAME[7], this.RechargeAmountComBoxBoxCompayName.Text));
            if (tempDataTable.Rows.Count>0)
            {
                this.RechargeAmountTextBlockStockAmount.Text = tempDataTable.Rows[0][0].ToString();
            }
        }
        #endregion
        #endregion

        #region ProductInfo Event

        #region Panel Add Stock
        //Add Stock Model Number Index Change
        private void StockAddProductInfoModelNumberIndexChange(object sender, System.EventArgs e)
        {
            if (!this.StockAddProductInfo.ModelNumberIs.Equals(string.Empty))
            {
                new TextBlockDataLoader().StockInfoLoad(this.AddStockLabelProductIDInfo, this.AddStockLabelQuantityInfo, this.AddStockLabelRateInfo, new PetuniaNecessaryFunction().ProductIDFinder(this.StockAddProductInfo.CompanyNameIs, this.StockAddProductInfo.ProductNameIs,this.StockAddProductInfo.ModelNumberIs));
            }
        }
        #endregion

        #region Invoice
        //Invoice Model Number Index Change
        private void InvoiceProductInfoModelNumberIndexChange(object sender, System.EventArgs e)
        {
            if (! this.InvoiceProductInfo.ProductNameIs.Equals(string.Empty))
            {
                new TextBlockDataLoader().InvoiceStockInfoLoad(GridViewInvoice, this.InvoiceLabelProductIDInfo, this.InvoiceLabelQuantityInfo, this.InvoiceLabelRateinfo, new PetuniaNecessaryFunction().ProductIDFinder(this.InvoiceProductInfo.CompanyNameIs, this.InvoiceProductInfo.ProductNameIs, this.InvoiceProductInfo.ModelNumberIs));
            }
        }
        #endregion

        #region Panel Return Product Sales
        private void ReturnProductSalesProductInfoModelNumberIndexChange(object sender, System.EventArgs e)
        {
            new TextBlockDataLoader().StockInfoLoad(this.ReturnProductSalesLabelProductID, this.ReturnProductSalesLabelQuantity, this.ReturnProductSalesLabelRate, this.ReturnProductSalesLabelAmount, new PetuniaNecessaryFunction().ProductIDFinder(this.ReturnProductSalesProductInfo.CompanyNameIs, this.ReturnProductSalesProductInfo.ProductNameIs, this.ReturnProductSalesProductInfo.ModelNumberIs));
        }
        #endregion

        #region Panel Stock Edit
        private void StockEditProductInfoModelNumberIndexChange(object sender, System.EventArgs e)
        {
            new TextBlockDataLoader().StockInfoLoad(this.StockEditLabelProductID, this.StockEditLabelQuantity, this.StockEditLabelRate, this.StockEditLabelAmount, new PetuniaNecessaryFunction().ProductIDFinder(this.StockEditProductInfo.CompanyNameIs, this.StockEditProductInfo.ProductNameIs, this.StockEditProductInfo.ModelNumberIs));
        }
        #endregion
        #endregion

        #region Text Box Event

        #region Panel New Product
        //Model Number Key Down
        private void NewProductTextBoxModelNumberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                NewProductCreateButton_Click(null,null);
            }
        }
        //Product ID Key Down
        private void NewProductTextBoxProductIDKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                NewProductCreateButton_Click(null, null);
            }
        }
        #endregion

        #region Panel Add Stock
        //Invoice Number Key Down
        private void AddStockTextBoxInvoiceNUmberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                AddStockUpdateClick(null,null);
            }
        }
        //Payment Amount Key Down
        private void AddStockTextBoxPaymentAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                AddStockUpdateClick(null, null);
            }
        }

        #endregion

        #region BackTag
        //InitalBalance keyDown
        private void InitalBalanceNumberBoxAmountKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.InitalBalanceButtonUpdateClick(null, null);
            }
        }
        #endregion

        #region Panel Invoice
        // Invoice Paid Amount Text Change
        private void InvoiceTextBoxAmountPaidTextChange(object sender, System.Windows.RoutedPropertyChangedEventArgs<string> e)
        {
            try
            {
                this.InvoieLabelCreditAmount.Text = new NecessaryFunction().CreditAmountIs(this.InvoicegridControl.AggregateResults["invoiceAmountTotal"].FormattedValue.ToString(),new NecessaryFunction().ComaSpriter(this.InvoiceNumberBoxPaidAmount.Text).ToString()).ToString();
            }
            catch
            {
                return;
            }
        }
        //Number Box Rate Key Down Event
        private void InvoiceNumberBoxRateKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.invoiceGridAddClick(null,null);
            }
        }
        //Number Box Paid Amount Key Down
        private void InvoiceNumberBoxPaidAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.InvoiceSpritButtonSaveClick(null, null);
            }
        }
        #endregion

        #region Panel Stock Edit
        //Quantity Value Changed
        private void StockEditNumberUpDownNewQuantityValueChanged(object sender, Telerik.Windows.Controls.RadRangeBaseValueChangedEventArgs e)
        {
            if (this.StockEditNumberBoxNewRate.Text!=string.Empty)
            {
                this.StockEditLanelNewAmount.Text = new NecessaryFunction().AmountIs(new NecessaryFunction().ComaSpriter(this.StockEditNumberBoxNewRate.Text).ToString(), this.StockEditNumberUpDownNewQuantity.Value.ToString());
            }
        }
        #endregion

        #region Panel Purchase And Selas View
        //Date and Time Key Down
        private void PurchaseSalesViewDateAndTimeKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                PurchaseSalesViewButtonSearchClick(null, null);
            }
        }
        //Number Box Amount Key Down
        private void PurchaseSalesViewNumberBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                PurchaseSalesViewButtonSearchClick(null, null);
            }
        }
        //Number Box Quantity Key Down
        private void PurchaseSalesViewNumberBoxQuantityKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                PurchaseSalesViewButtonSearchClick(null, null);
            }
        }
        #endregion

        #region Panel Edit New Product Info
        //Product ID Key Down
        private void EditNewProductTextBoxProductIDKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                EditNewProductClearClick(null,null);
            }
        }
        //Model Number Key Down
        private void EditNewProductTextBoxModelNumberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                EditNewProductUpdateClick(null, null);
            }
        }
        #endregion

        #region Panel New Party
        //Address Key Down
        private void NewpartyTextBoxAddressKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.NewPartyCreateClick(null, null);
            }
        }
        #endregion

        #region Panel Edit Party Info
        //Address Key Down
        private void EditPartyInfoTextBoxAddressKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.EditPartyInfoUpdateClickk(null, null);
            }
        }
        //Phone Number Key Down
        private void EditParyInfoTextBoxPhoneNumberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.EditPartyInfoUpdateClickk(null, null);
            }
        }
        #endregion

        #region Panel Stock Info
        //Product ID Key Dowd
        private void StockInfotextBoxProductIDKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.StockInfoButtonSearchClick(null, null);
            }
        }
        //Amount Key Down
        private void StockInfoTextBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.StockInfoButtonSearchClick(null, null);
            }
        }
        //Rate Key Down
        private void StockInfoTextBoxRateKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.StockInfoButtonSearchClick(null, null);
            }
        }
        #endregion

        #region Panel Debtors Payment
        //Name key down
        private void DebtorsCredtorsPaymentTextBoxNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.DebtorPaymentButtonSearchClick(null, null);
            }
        }
        //Amount Key Down
        private void DebtorsCredtorsPaymentNumberBoxPaymentAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.DebtorPaymentButtonPamentClick(null, null);
            }
        }
        #endregion

        #region Panel Return Product Seals
        //Date And Time Key Down
        private void ReturnProductSalesDateAndTimeKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.ReturnProductSalesButtonSearchClick(null, null);
            }
        }
        //Return Quantity Key Down
        private void ReturnProductSalesNumericReturnQuantityKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.ReturnProductSalesButtonAddClick(null, null);
            }
        }
        //Return Amount Credit Or Debit Key Down
        private void ReturnProductSalesNumberBoxPaymentAmountCreditOrDebitKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.ReturnProductSalesButtonSaveClick(null, null);
            }
        }
        //Return Amount Credit Or Debit Text Change
        private void ReturnProductSalesNumberBoxPaymentAmountCreditOrDebitTextChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<string> e)
        {
            try
            {
                this.ReturnProductSalesLabelCreditAmount.Text = new NecessaryFunction().CreditAmountIs(this.ReturnProductSalesGridViewChart.AggregateResults["returnProductTotalAmount"].FormattedValue.ToString(), new NecessaryFunction().ComaSpriter(this.ReturnProductSalesNumberBoxPaymentAmountCreditOrDebit.Text).ToString());
            }
            catch
            {
                return;
            }
        }
        #endregion

        #region Panel Cash Payment and Receive
       //Amount Key Down
        private void CashReceivePaymentNumberBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.CashReceivePaymentButtonUpdateClick(null, null);
            }
        }

        #endregion

        #region Panel Expances 
        //Amount Key Down
        private void ExpansesNumberBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.ExpansesButtonUpdateClick(null, null);
            }
        }
        #endregion

        #region Panel Salary
        //Amount Key Down
        private void SalaryNumberBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.SalaryButtonUpdateClick(null, null);
            }
        }
        #endregion

        #region Panel Debtors Creditors View
        //Date And Time Key Down
        private void DebtorsCreditorsViewDateAndTimeKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.DebtorsCreditorsViewButtonSearchClick(null, null);
            }
        }
        //Product ID Key Down
        private void DebtorsCreditorsViewTextBoxProductIDKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.DebtorsCreditorsViewButtonSearchClick(null, null);
            }
        }
        //Amount Key Down
        private void DebtorsCreditorsViewNumberBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.DebtorsCreditorsViewButtonSearchClick(null, null);
            }
        }
        #endregion

        #region Panel Edit Recharge
        private void EditRechagreTextBoxPhoneNumberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.EditRechagreButtonUpdateClick(null, null);
            }
        }
        #endregion

        #region Panel Rechage
        //Amount Key Down
        private void RechargeAmountNumicBoxAmountKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.RechargeAmountButonUpdateClick(null, null);
            }
        }
        #endregion
        #endregion

        #region Cheecked Box
        //NewProductCheckBoxAutoGenerate Checked 
        private void AutoGenerateChcked(object sender, System.Windows.RoutedEventArgs e)
        {
            NewProductTextBoxProductID.IsEnabled = false;
        }
        //NewProductCheckBoxAutoGenerate Unchecked
        private void AutoGenerateunchecked(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (NewProductCheckBoxAutoGenerate.IsChecked.Equals(false))
            {
                NewProductTextBoxProductID.IsEnabled = true;
            }
        }
        #endregion

        #region Radio Button Events

        #region Panel Stock Info View
        //By Product Info Checked
        private void StockInfoRadioButtonProductInfoChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.AllEnable();
        }
        //By Product Info Unchecked
        private void StockInfoRadioButtonByProductInfoUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.AllDisable();
        }
        //By Company Name Checked
        private void StockInfoRadioButtonByCompanyNameChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.ProductNameLoad();
            this.StockinfoProductInfo.ProductNameIsEnable = true;
        }
        //By Company Name Unchecked
        private void StockInfoRadioButtonByCompanyNameUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.ProductNameIsEnable = false;
        }
        //By Product Name Checked
        private void StockInfoRadioButtonByProductNameCheeked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.CompanyNameLoader();
            this.StockinfoProductInfo.CompanyNameIsEnable = true;
        }
        //By Product Name UnChecked
        private void StockInfoRadioButtonByProductNameUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.CompanyNameIsEnable = false;
        }
        //By Model Number Checked
        private void StockInfoRadioButtonByModelNumberChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.ModelNumberLoad();
            this.StockinfoProductInfo.ModelNumberIsEnable = true;
        }
        //By Model Number Un Checked
        private void StockInfoRadioButtonByModelNumberUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockinfoProductInfo.ModelNumberIsEnable = false;
        }
        //By Product ID Checked
        private void StockInfoRadioButtonByProductIDChecked(object sender, System.Windows.RoutedEventArgs e)
        {
          //  StockInfoUIDisable();
            this.StockInfotextBoxProductID.IsEnabled = true;
        }
        //By Product ID UnChecked
        private void StockInfoRadioButtonByProductIDUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockInfotextBoxProductID.IsEnabled = false;
        }
        //By Amount Checked
        private void StockInfoRadioButtonByAmountChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockInfoTextBoxAmount.IsEnabled = true;
        }
        //By Amount UnChecked
        private void StockInfoRadioButtonByAmountUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockInfoTextBoxAmount.IsEnabled = false;
        }
        //By Rate Checked
        private void StockInfoRadioButtonByRateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockInfoTextBoxRate.IsEnabled = true;
        }
        //By Rate UnChecked
        private void StockInfoRadioButtonByRateUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.StockInfoTextBoxRate.IsEnabled = false;
        }
        //By All Checked
        private void StockInfoRadioButtonAllChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            StockInfoUIDisable();
        }
        #endregion

        #region Panel Return Product Sales
        //Return Product Sales RadioButton By All Checked
        private void ReturnProductSalesRadioButtonbyAllChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesDateAndTime.IsEnabled = false;
        }
        //Return Product Sales RadioButton By Date Checked
        private void ReturnProductSalesRadioButtonByDateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesDateAndTime.IsEnabled = true;
        }
        //Return Product Credit Checked
        private void ReturnProductSalesRadioButtonCreditOrDebitChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesGridCreditOrDebit.Visibility = Visibility.Visible;
        }
        //Return Product Credit Unchecked
        private void ReturnProductSalesRadioButtonCreditOrDebitUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ReturnProductSalesGridCreditOrDebit.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Panel Purchase Sales View
        //By Product Info Checked
        private void PurchaseSalesViewRadioButtonByProductInfoChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewProductInfo.AllEnable();
        }
        //By Product info UnChecekd
        private void PurchaseSalesViewRadioButtonByProductInfoUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewProductInfo.AllDisable();
        }
        //By Company Name Checked
        private void PurchaseSalesViewRadioButtonByCompanyNameChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewProductInfo.ProductNameLoad();
            this.PurchaseSalesViewProductInfo.ProductNameIsEnable = true;
        }
        //By Product Name UnChecked
        private void PurchaseSalesViewRadioButtonByCompanyNameUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewProductInfo.ProductNameIsEnable = false;
        }
        //By Product Name Checked
        private void PurchaseSalesViewRadioButtonByProductNameChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.PurchaseSalesViewProductInfo.CompanyNameLoader();
            this.PurchaseSalesViewProductInfo.CompanyNameIsEnable = true;
            Mouse.OverrideCursor = null;
        }
        //By Product name Un Checked
        private void PurchaseSalesViewRadioButtonByProductNameUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewProductInfo.CompanyNameIsEnable = false;
        }
        //By Model Number Checked
        private void PurchaseSalesViewRadioButtonByModelNumberChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.PurchaseSalesViewProductInfo.ModelNumberLoad();
            this.PurchaseSalesViewProductInfo.ModelNumberIsEnable = true;
            Mouse.OverrideCursor = null;
        }
        //By Model Number unChecked
        private void PurchaseSalesViewRadioButtonByModelNumberUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewProductInfo.ModelNumberIsEnable =false;
        }
        //By Date Checked
        private void PurchaseSalesViewRadioButtonByDateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewDateAndTime.IsEnabled = true;
        }
        //By Date Unchecked
        private void PurchaseSalesViewRadioButtonByDateUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewDateAndTime.IsEnabled = false;
        }
        //By Amount Checked
        private void PurchaseSalesViewRadioButtonByAmountChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewNumberBoxAmount.IsEnabled = true;
        }
        //By Amount Unchecked
        private void PurchaseSalesViewRadioButtonByAmountUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewNumberBoxAmount.IsEnabled = false;
        }
        //By Quantity Checked
        private void PurchaseSalesViewRadioButtonByQuantityChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewNumberBoxQuantity.IsEnabled = true;
        }
        //By Quantity UnChecked
        private void PurchaseSalesViewRadioButtonByQuantityUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.PurchaseSalesViewNumberBoxQuantity.IsEnabled = false;
        }
        //Purchase Sales View By All Checked
        private void PurchaseSalesViewRadioButtonByAll(object sender, System.Windows.RoutedEventArgs e)
        {
            PurchaseSalesViewUIDisable();
        }
        #endregion

        #region Panel Debtors Creditors View
        //By Date Checked
        private void DebtorsCreditorsViewRadioButtonByDateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewDateAndTime.IsEnabled = true;
        }
        //By Date UnChecked
        private void DebtorsCreditorsViewRadioButtonByDateUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewDateAndTime.IsEnabled =false;
        }
        //Debtors Creditors View  By Product ID Checked
        private void DebtorsCreditorsViewRadioButtonByProductIDChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewTextBoxProductID.IsEnabled = true;
        }
        private void DebtorsCreditorsViewRadioButtonByProductIDUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewTextBoxProductID.IsEnabled = false;
        }
        //Debtors Creditors View  By Amount Checked
        private void DebtorsCreditorsViewRadioButtonByAmountChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewNumberBoxAmount.IsEnabled = true;
        }

        private void DebtorsCreditorsViewRadioButtonByAmountUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.DebtorsCreditorsViewNumberBoxAmount.IsEnabled = false;
        }
        #endregion

        #region Panel Expance View
        //Expanses View By Date Checked
        private void ExpansesViewRadioButtonByDateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ExpansesViewUIDisable();
            this.ExpansesViewDateAndTime.IsEnabled = true;
        }
        //Expanses View By Type Checked
        private void ExpansesViewRadioButtonByTypeChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ExpansesViewUIDisable();
            this.ExpansesViewComboBoxName.IsEnabled = true;
        }
        //Expanses View by Both Checked
        private void ExpansesViewRadioButtonByBothChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ExpansesViewDateAndTime.IsEnabled = true;
            this.ExpansesViewComboBoxName.IsEnabled = true;
        }
        #endregion

        #region Panel Add Stock
        //Add stock Creditors Checked
        private void AddStockReadioButtonCreditChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AddStockPanelCredit.Visibility = Visibility.Visible;
        }
        //Add Stock Creditors Unchecked
        private void AddStockReadioButtonCreditUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AddStockPanelCredit.Visibility = Visibility.Hidden;
        }
        //Check Checked
        private void AddStockRadioButtonCheeckChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AddstockPanelCheck.Visibility = Visibility.Visible;
        }
        //Check unchecked
        private void AddStockRadioButtonCheeckUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.AddstockPanelCheck.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Panel Invoice
        //Invoice Credit Checked
        private void InvoiceRadioButtonCreditChecked(object sender, System.Windows.RoutedEventArgs e)
        {
             this.InvoicePanelCredit.Visibility = Visibility.Visible;
        }
        //Invoice Credit Unchecked
        private void InvoiceRadioButtonCreditUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoicePanelCredit.Visibility = Visibility.Hidden;
        }
        //Check Checked
        private void InvoiceRadioButtonCheckChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoicePanelCheck.Visibility = Visibility.Visible;
        }
        //Check UnChecked
        private void InvoiceRadioButtonCheckUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.InvoicePanelCheck.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Panel Edit New Product
        //By Product Info
        private void EditNewProductRadioButtonByProductInfoChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductProductInfo.IsEnabled = true;
            this.EditNewProductProductInfo.AllEnable();
        }
       //By Product Info UnChecked
        private void EditNewProductRadioButtonByProductInfoUnChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductProductInfo.IsEnabled = false;
        }
        //Product By Product ID
        private void EditNewProductRadioButtonByProductIDChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductTextBoxProductID.IsEnabled = true;
        }
        //By Product ID UnChecked
        private void EditNewProductRadioButtonByProductIDUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductTextBoxProductID.IsEnabled =false;
        }
        //Delete Checked
        private void EditNewProductRadioButtonDeleteChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductGroupBoxDelete.IsEnabled = true;
            this.EditNewProductRadioButtonByProductInfo.IsChecked = true;
            this.EditNewProductButtonSearch.IsEnabled = false;
        }
        //Update Checked
        private void EditNewProductRadioButtonUpdateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductgroupBoxUpdate.IsEnabled = true;
        }
        //Delete UnChecked
        private void EditNewProductRadioButtonDeleteUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductGroupBoxDelete.IsEnabled = false;
            this.EditNewProductButtonSearch.IsEnabled = true;
            this.EditNewProductRadioButtonByProductID.IsEnabled =true;
        }
        //Update UnChecked
        private void EditNewProductRadioButtonUpdateUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductgroupBoxUpdate.IsEnabled = false;
        }
        //Delete By Product Info Checked
        private void EditNewProductRdioButtonDeleteByProductInfoChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductProductInfo.AllEnable();
        }
        //Delete By Company Name
        private void EditNewProductRdioButtonDeleteByCompanyNameChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductProductInfo.AllDisable();
            this.EditNewProductProductInfo.ProductNameIsEnable = true;
            this.EditNewProductProductInfo.ProductNameLoad();
        }
        //Delete By Product Name
        private void EditNewProductRdioButtonDeleteByProductNameChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductProductInfo.AllDisable();
            this.EditNewProductProductInfo.CompanyNameIsEnable = true;
            this.EditNewProductProductInfo.CompanyNameLoader();
        }
        //Delete By Model Number
        private void EditNewProductRdioButtonDeleteByModelNumberChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.EditNewProductProductInfo.AllDisable();
            this.EditNewProductProductInfo.ModelNumberIsEnable = true;
            this.EditNewProductProductInfo.ModelNumberLoad();
        }
        #endregion

        #region Panel Recharge View
        //Company Name Checked
        private void RechargeHistoryRadioButtonCompanyNameChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RechargeHistoryComboBoxCompanyName.IsEnabled = true;
        }
        //Company Name UnChecked
        private void RechargeHistoryRadioButtonCompanyNameUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RechargeHistoryComboBoxCompanyName.IsEnabled = false;
        }
        //Date Checked
        private void RechargeHistoryRadioButtonDateChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RechargeHistoryDate.IsEnabled = true;
        }
        //Date UnChecked
        private void RechargeHistoryRadioButtonDateUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.RechargeHistoryDate.IsEnabled = false;
        }
        #endregion

        #endregion

        #region Setting

        #region Database Setting
        //Button Click
        private void SettingButtonSaveClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.SettingButtonSave.IsEnabled = false;
            if (this.SettingTextboxDatabaseUserName.Text!=string.Empty && this.SettingPasswordBoxDatabasePassword.Password!=string.Empty && this.SettingTextBoxDatabaseHostIp.Text!=string.Empty && this.SettingTextBoxDatabasePortnumber.Text!=string.Empty && this.SettingTextboxDatabaseDatabaseName.Text!=string.Empty)
            {
                if (new MySqlNaceassaryElement().ConnectToMysql(this.SettingTextboxDatabaseDatabaseName.Text,this.SettingTextBoxDatabaseHostIp.Text,this.SettingTextBoxDatabasePortnumber.Text,this.SettingTextboxDatabaseUserName.Text,this.SettingPasswordBoxDatabasePassword.Password))
                {
                    Mouse.OverrideCursor = null;
                    Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0,8],Variables.ERROR_MESSAGES[0,0],MessageBoxButton.OK,MessageBoxImage.Information);
                    this.SettingButtonSave.IsEnabled =true;
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0,1],Variables.ERROR_MESSAGES[0,0],MessageBoxButton.OK,MessageBoxImage.Error);
                    this.SettingButtonSave.IsEnabled = true;
                }
            }
            else
            {
                Mouse.OverrideCursor = null;
                Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0,5],Variables.ERROR_MESSAGES[0,0],MessageBoxButton.OK,MessageBoxImage.Stop);
                this.SettingButtonSave.IsEnabled = true;
            }
        }
        private void SettingButtonDatabaseClearClick(object sender, System.Windows.RoutedEventArgs e)
        {
           this.SettingButtonDatabaseClear.IsEnabled =false;
           Mouse.OverrideCursor=Cursors.Wait;
           this.SettingTextboxDatabaseUserName.Text = string.Empty;
           this.SettingPasswordBoxDatabasePassword.Password = string.Empty;
           this.SettingTextBoxDatabaseHostIp.Text = string.Empty;
           this.SettingTextboxDatabaseDatabaseName.Text = string.Empty;
           this.SettingTextBoxDatabasePortnumber.Text = string.Empty;
           this.SettingButtonDatabaseClear.IsEnabled =true;
           Mouse.OverrideCursor= null;

        }
        //Database Name Key Down
        private void SettingTextboxDatabaseDatabaseNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.SettingButtonSaveClick(null,null);
            }
        }
        //password Key Down
        private void SettingPasswordBoxDatabasePasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.SettingButtonSaveClick(null, null);
            }
        }
        //User Name Key Down
        private void SettingTextboxDatabaseUserNameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.SettingButtonSaveClick(null, null);
            }
        }
        #endregion

        #region Password Change
        //Password Save Button Click
        private void PasswordButtonSaveClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            this.PasswordButtonSave.IsEnabled = false;
            if (this.PasswordBoxOldPassword.Password!=string.Empty && this.PasswordBoxNewPassword.Password!=string.Empty && this.PasswordConformPassword.Password!=string.Empty)
            {
                if (Properties.Settings.Default.LoginPassword.Equals(PasswordBoxOldPassword.Password) && this.PasswordBoxNewPassword.Password.Equals(this.PasswordConformPassword.Password))
                {
                    Properties.Settings.Default.LoginPassword = this.PasswordConformPassword.Password;
                    Properties.Settings.Default.Save();
                    Mouse.OverrideCursor =null;
                    Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0, 11], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Information);
                    this.PasswordButtonSave.IsEnabled =true;
                }
                else
                {
                    Mouse.OverrideCursor = null;
                    Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0, 7], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.OK, MessageBoxImage.Error);
                    this.PasswordButtonSave.IsEnabled = true;
                }
            }
            else
            {
                Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[0,5],Variables.ERROR_MESSAGES[0,0],MessageBoxButton.OK,MessageBoxImage.Error);
                Mouse.OverrideCursor = null;
                this.PasswordButtonSave.IsEnabled = true;
            }
        }
        //Conform Password key Down
        private void PasswordConformPasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                this.PasswordButtonSaveClick(null, null);
            }
        }
        #endregion

        #region General
        //Login Enable Checked
        private void GeneralCheckBoxLoginEnableCheckeed(object sender, System.Windows.RoutedEventArgs e)
        {
            Properties.Settings.Default.LoginEnable = true;
            Properties.Settings.Default.Save();
        }
        //Login Enable Unchecked
        private void GeneralCheckBoxLoginEnableUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            Properties.Settings.Default.LoginEnable = false;
            Properties.Settings.Default.Save();
        }
        //Show Order and Delivery Checked
        private void GeneralCheckboxShowOrderChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowOrderDelivery = true;
            Properties.Settings.Default.Save();
        }
        //Show Order and Delivery UnChecked
        private void GeneralCheckboxShowOrderUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            Properties.Settings.Default.ShowOrderDelivery = false;
            Properties.Settings.Default.Save();
        }
        #endregion
        #endregion

        #region Private Functions
        //Hide All Panel
        private void HideAllPanel()
        {
            this.PanelNewProduct.Visibility = Visibility.Hidden;
            this.PanelStockAdd.Visibility = Visibility.Hidden;
            this.PaneInvoice.Visibility = Visibility.Hidden;
            this.PanelPurchaseSalesView.Visibility = Visibility.Hidden;
            this.PanelStockInfo.Visibility = Visibility.Hidden;
            this.PanelEditNewProduct.Visibility = Visibility.Hidden;
            this.PanelNewParty.Visibility = Visibility.Hidden;
            this.PanelEditPartyInfo.Visibility = Visibility.Hidden;
            this.PanelDebtorsPayment.Visibility = Visibility.Hidden;
            this.PanelReturnProductSales.Visibility = Visibility.Hidden;
            this.PanelCashReceivePayment.Visibility = Visibility.Hidden;
            this.PanelExpanses.Visibility = Visibility.Hidden;
            this.PanelStockEdit.Visibility = Visibility.Hidden;
            this.PanelSalary.Visibility = Visibility.Hidden;
            this.PanelDebtorsCreditorsView.Visibility = Visibility.Hidden;
            this.PanelExpansesViewUpdate.Visibility = Visibility.Hidden;
            this.PanelSummary.Visibility = Visibility.Hidden;
			this.HomeImage.Visibility= Visibility.Hidden;
            this.PanelNewRecharge.Visibility = Visibility.Hidden;
            this.PanelRechargeEditCompanyInfo.Visibility = Visibility.Hidden;
            this.PanelRechagreAmount.Visibility = Visibility.Hidden;
            this.PanelRechargeHistory.Visibility = Visibility.Hidden;
        }
        //Setting Panel Hide
        private void SettingPanelHide()
        {
            this.SettingPanelDatabaseSetting.Visibility = Visibility.Hidden;
            this.SettingPanelPasswordChange.Visibility = Visibility.Hidden;
            this.SettingPanelGeneral.Visibility = Visibility.Hidden;
        }
        //Item Clear
        #region Item Clear
        //New Product Item Clear
        private void NewProductItemClear()
        {
            this.NewProductTextBoxCompanyName.Text = string.Empty;
            this.NewProductTextBoxProductID.Text = string.Empty;
            this.NewProductTextBoxProductID.IsEnabled = false;
            this.NewProductCheckBoxAutoGenerate.IsChecked = true;
            this.NewProductTextBoxProductName.Text = string.Empty;
            this.NewProductTextBoxModelNumber.Text = string.Empty;
            this.NewProductTextBoxDescription.Text = string.Empty;
        }
        //Add Stock Item Clear
        private void AddStockItemClear()
        {
            this.StockAddProductInfo.ClearAllText();
            this.AddStockLabelProductIDInfo.Text = string.Empty;
            this.AddStockLabelQuantityInfo.Text = string.Empty;
            this.AddStockLabelRateInfo.Text = string.Empty;
            this.AddStockNumericUpDown.Value = 0;
            this.AddStockTextBoxRate.Text = string.Empty;
            this.AddStockRadioButtonCash.IsChecked = true;
            this.AddStockTextBoxPaymentAmount.Text = string.Empty;
            this.AddStockPanelCredit.Visibility = Visibility.Hidden;
            this.AddStockTextBoxIssueName.Text = string.Empty;
            this.AddStockTextBoxInvoiceNote.Text = string.Empty;
        }
        //Invoice item Clear
        private void InvoiceItemClear()
        {
           this.InvoiceTextBoxCustomerName.Text = string.Empty;
           this.InvoiceTextBoxBillTo.Text = string.Empty;
           this.InvoiceProductInfo.ClearAllText();
           this.InvoiceLabelProductIDInfo.Text = string.Empty;
           this.InvoiceLabelQuantityInfo.Text = string.Empty;
           this.InvoiceRadioButtonCash.IsChecked = true;
           this.InvoiceComboBoxDebtorName.Text = string.Empty;
           this.GridViewInvoice.Clear();
           invoiceNumber = new PetuniaNecessaryFunction().InvoiceNumbers();
           this.InvoiceTextBoxIssueName.Text = string.Empty;
           this.InvoiceNumberBoxRate.Text = "0";
           this.InvoieLabelCreditAmount.Text = string.Empty;
           this.InvoiceNumberBoxPaidAmount.Text = "0";
           this.InvoiceLabelRateinfo.Text = string.Empty;
        }
        // Edit NewProduct Item Clear
        private void EditNewProductItemClear()
        {
            this.EditNewProductProductInfo.ClearAllText();
            this.EditNewProductLabelProductID.Text = string.Empty;
            this.EditNewProductRadioButtonByProductInfo.IsChecked = true;
            this.EditNewProductTextBoxCompanyName.Text = string.Empty;
            this.EditNewProductTextBoxModelNumber.Text = string.Empty;
            this.EditNewProductTextBoxProductID.Text = string.Empty;
            this.EditNewProductTextBoxProductName.Text = string.Empty;
            this.EditNewProductRadioButtonUpdate.IsChecked = true;
            this.EditNewProductRdioButtonDeleteByProductInfo.IsChecked = true;
            this.EditNewProductGroupBoxDelete.IsEnabled = false;
        }
        //New Party Create Item Clear
        private void NewPartyCreateItemClear()
        {
            this.NewParyTextBoxUserID.Text = string.Empty;
            this.NewParyComboBoxAccountType.Text = string.Empty;
            this.NewPartyTextBoxContractPerson.Text = string.Empty;
            this.NewPartyTextBoxPhoneNumber.Text = string.Empty;
            this.NewPartyTextBoxEmail.Text = string.Empty;
            this.NewpartyTextBoxAddress.Text = string.Empty;
        }
        //Edit Party Info Item Clear
        private void EditPartyInfoItemClear()
        {
            this.EditPartyInfoComboBoxUserID.Text = string.Empty;
            this.EditPartyInfoComboBoxAccountType.Text = string.Empty;
            this.EditPartyInfoTextBoxAddress.Text = string.Empty;
            this.EditPartyInfoTextBoxControctPerson.Text = string.Empty;
            this.EditPartyInfoTextBoxEmail.Text = string.Empty;
            this.EditParyInfoTextBoxPhoneNumber.Text = string.Empty;
        }
        //Stock Info Item Clear
        private void StockInfoItemClear()
        {
            this.StockinfoProductInfo.ClearAllText();
            this.StockInfotextBoxProductID.Text = string.Empty;
            this.StockInfoTextBoxAmount.Text = string.Empty;
            this.StockInfoTextBoxRate.Text = string.Empty;
            this.StockinfoWapPanel.Children.Clear();
            this.StockInfoRadioButtonByProductInfo.IsChecked = true;
        }
        //Debtors Payment Item Clear
        private void DebtorsCredtorsPaymentItemClear()
        {
            this.DebtorsCredtorsPaymentComboBoxName.Text = string.Empty;
            this.DebtorsCredtorsPaymentLabelAmount.Text = string.Empty;
            this.DebtorsCredtorsPaymentNumberBoxPaymentAmount.Text = string.Empty;
            this.DebtorCredtorsPaymentLabelHeader.Text = string.Empty;
            this.DebtorsCredtorsPaymentTextBoxName.Text = string.Empty;
        }
        //Return Product Sales Item Clear
        private void ReturnProductSalesItemClear()
        {
            this.ReturnProductSalesProductInfo.ClearAllText();
            this.ReturnProductSalesLabelAmount.Text = string.Empty;
            this.ReturnProductSalesLabelProductID.Text = string.Empty;
            this.ReturnProductSalesLabelQuantity.Text = string.Empty;
            this.ReturnProductSalesLabelRate.Text = string.Empty;
            this.ReturnProductSalesRadioButtonByDate.IsChecked = true;
            this.ReturnProductSalesRadioButtonCash.IsChecked = true;
            this.ReturnProductSalesComboBoxName.Text = string.Empty;
            this.ReturnProductSalesNumericReturnQuantity.Value = 0;
            this.ReturnProductSalesHeader.Text = string.Empty;
            this.ReturnProductSalesLabelCreditAmount.Text = string.Empty;
            this.GridViewInvoice.Clear();
            this.GridViewLoadProductHistory.Clear();
            this.ReturnProductSalesNumberBoxPaymentAmountCreditOrDebit.Text = "0";
        }
        // Purchase Sales View Item Clear
        private void PurchaseSalesView()
        {
            //this support 1)Purchase view
            //             2)Sales View
            //             3)Product Return view
            //             4)Sales Return View
            this.PurchaseSalesViewProductInfo.ClearAllText();
            this.PurchaseSalesViewNumberBoxAmount.Text = string.Empty;
            this.PurchaseSalesViewNumberBoxQuantity.Text = string.Empty;
            this.PurchaseSalesViewRadioButtonByProductInfo.IsChecked = true;
            this.PurchaseSalesViewLabelHeader.Text = string.Empty;
            this.PerchaseSalesViewGridView.ItemsSource = null;
        }
        //Cash Receive and Payment Item Clear
        private void CashRecivePaymentItemClear()
        {
            //This Support 1) Cash Receive
            //             2) Cash Payment
            this.CashReceivePaymentLabelHeader.Text = string.Empty;
            this.CashReceivePaymentTextBoxDescription.Text = string.Empty;
            this.CashReceivePaymentNumberBoxAmount.Text = string.Empty;
        }
        //Expanses Item Clear
        private void ExpansesitemClear()
        {
            this.ExpansesNumberBoxAmount.Text = string.Empty;
            this.ExpansesComboBoxExpansesType.Text = string.Empty;
        }
        //Stock Edit Item Clear
        private void StockEditItemClear()
        {
            this.StockEditProductInfo.ClearAllText();
            this.StockEditLabelProductID.Text = string.Empty;
            this.StockEditLabelQuantity.Text = string.Empty;
            this.StockEditLabelRate.Text = string.Empty;
            this.StockEditLabelAmount.Text = string.Empty;
            this.StockEditNumberUpDownNewQuantity.Value = 0;
            this.StockEditNumberBoxNewRate.Text = string.Empty;
        }
        //Salary Item Clear
        private void SalaryItemClear()
        {
            this.SalaryComboBoxName.Text = string.Empty;
            this.SalaryRadioButtonNone.IsChecked = true;
            this.SalaryTextBoxDescription.Text = string.Empty;
            this.SalaryNumberBoxAmount.Text = string.Empty;
            this.SalaryGridViewHistory.ItemsSource = null;
        }
        //Debtors Creditors view item Clear
        private void DebtorsCreditorsViewItemClear()
        {
            //This Support 1)Creditor view
            //             2)Debtors View
            this.DebtorsCreditorsViewComboBoxName.Text = string.Empty;
            this.DebtorsCreditorsViewLabelHader.Text = string.Empty;
            this.DebtorsCreditorsViewLabelPerInfoContractPerson.Text = string.Empty;
            this.DebtorsCreditorsViewLabelPerPhone.Text = string.Empty;
            this.DebtorsCreditorsViewLabelPerInfoEmail.Text = string.Empty;
            this.DebtorsCreditorsViewLabelPerInfoAddress.Text = string.Empty;
            this.DebtorsCreditorsViewTextBoxProductID.Text = string.Empty;
            this.DebtorsCreditorsViewNumberBoxAmount.Text = string.Empty;
            this.DebtorsCreditorsViewGridView.ItemsSource = null;
            this.DebtorsCreditorsViewRadioButtonByDate.IsChecked = true;
        }
        //Expanses View Item Clear
        private void ExpansesViewItemClear()
        {
            this.ExpansesViewComboBoxName.Text = string.Empty;
            this.ExpansesViewRadioButtonByDate.IsChecked = true;
        }
        //BackTag InitialBalance Item Clear
        private void BackTagInitialBalanceItemClear()
        {
            this.InitalBalanceRadioButtonCash.IsChecked = true;
            this.InitalBalanceNumberBoxAmount.Text = string.Empty;
        }
        //Database Setting Item Clear
        private void DatabaseSettingItemLoadClear()
        {
            this.SettingTextboxDatabaseDatabaseName.Text = MysqlClass.Properties.Settings.Default.defultDatabase;
            this.SettingTextboxDatabaseUserName.Text = MysqlClass.Properties.Settings.Default.username;
            this.SettingPasswordBoxDatabasePassword.Password = string.Empty;
            this.SettingTextBoxDatabaseHostIp.Text = MysqlClass.Properties.Settings.Default.serverip;
            this.SettingTextBoxDatabasePortnumber.Text = MysqlClass.Properties.Settings.Default.port.ToString();
        }
        //Password Change ItemClear
        private void PasswordChangeItemClear()
        {
            this.PasswordBoxOldPassword.Password = string.Empty;
            this.PasswordBoxNewPassword.Password = string.Empty;
            this.PasswordConformPassword.Password = string.Empty;
        }
        //General Item Clear
        private void GeneralItemClear()
        {
            this.GeneralCheckBoxLoginEnable.IsChecked = Properties.Settings.Default.LoginEnable;
            this.GeneralCheckboxShowOrder.IsChecked = Properties.Settings.Default.ShowOrderDelivery;
        }
        //Clear New Recharge 
        private void NewRechagreClear()
        {
            this.NewRecgaresTextBoxCompnayName.Text = string.Empty;
            this.NewRecgaresTextBoxContractPerson.Text = string.Empty;
            this.NewRecgaresTextBoxPhoneNumber.Text = string.Empty;
        }
        //Clear Edit Recharge
        private void EditRechargeClear()
        {
            this.EditRechagreTextBoxContractPerson.Text = string.Empty;
            this.EditRechagreTextBoxPhoneNumber.Text = string.Empty;
        }
        //Clear Recharge
        private void RecharesClear()
        {
            this.RechargeAmountNumicBoxAmount.Text = string.Empty;
            this.RechargeAmountTextBlockStockAmount.Text = string.Empty;
            this.RechargeAmountTextBlockStockAmount.Text = string.Empty;
            this.RechargeAmountRedioButtonSales.IsChecked = true;
        }
        //Clear Recharge History
        private void ReechareHistoryClear()
        {
            this.RechargeHistoryRadioButtonCompanyName.IsChecked = true;
            this.RechargeHistoryDate.IsEnabled = true;
            this.RechargeHistoryGridView.ItemsSource = null;
        }
        #endregion
        // UI Control Disable
        #region UI Control Disable
        //Stock Info UI Disable
        private void StockInfoUIDisable()
        {
            this.StockinfoProductInfo.AllDisable();
            this.StockInfotextBoxProductID.IsEnabled = false;
            this.StockInfoTextBoxRate.IsEnabled = false;
            this.StockInfoTextBoxAmount.IsEnabled = false;
        }
        //Purchase Sales view UI Disable
        private void PurchaseSalesViewUIDisable()
        {
            this.PurchaseSalesViewDateAndTime.IsEnabled = false;
            this.PurchaseSalesViewNumberBoxQuantity.IsEnabled = false;
            this.PurchaseSalesViewNumberBoxAmount.IsEnabled = false;
        }
        //Debtors Creditors View UI Disable
        private void DebtorsCredtorsViewUIDisable()
        {
            this.DebtorsCreditorsViewDateAndTime.IsEnabled = false;
            this.DebtorsCreditorsViewTextBoxProductID.IsEnabled = false;
            this.DebtorsCreditorsViewNumberBoxAmount.IsEnabled = false;
        }
        //Expanses view UI Disable
        private void ExpansesViewUIDisable()
        {
            this.ExpansesViewComboBoxName.IsEnabled = false;
            this.ExpansesViewDateAndTime.IsEnabled = false;
        }
        #endregion
        //On Closing
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messBoxResult = new MessageBoxResult();
            messBoxResult = Microsoft.Windows.Controls.MessageBox.Show(Variables.ERROR_MESSAGES[1, 1], Variables.ERROR_MESSAGES[0, 0], MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messBoxResult.Equals(MessageBoxResult.Yes))
            {
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

     
        #endregion
    }
}
