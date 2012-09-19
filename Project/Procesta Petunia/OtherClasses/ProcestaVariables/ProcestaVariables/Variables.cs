//Title :           Procesta Variable
//Version :         2.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All kinds global variable are here 
using System;
using ProcestaAccountingFunction;
namespace ProcestaVariables
{
    public class Variables
    {
        public enum OperationTrypes { Purchase, Sales, Debtors, Creditors, PurchaseReturn, SalesReturn, CashReceive, CashPayment, InvPurDebCre}

        public static bool LOGIN_IS = true;

        public static String[,] ERROR_MESSAGES = new String[3, 15]
        {
            {"Petunia 1.0","Database Connection Failed","Error","Writing Error","Reading Error","Fields Are Empty","Login fail Password incorrect","Password Not Mash","Database Connection Ok","Product ID Already Exit","Product Info Already Exit","Update Successfully","Invalid Number","Please Select a Item","No Item In your Cart"},
            //   00                 01                      02          03          04              05                  06                                  07                  08                  09                                  010                 011                 012                 013                 014
            {"No More Product In your Stock","Are You Want To Exit ?","Password Mash","No product Found !","Please Select a Option","User ID Already Exit","Are you want to Remove ?","This Product already in your cart. Are you want to update ?","User ID Doesn`t Exit","Please Select a Product","No Item Found !","Are you want to update ?","Are you want to delete ?","Delete successfully ","Are You Want to Save ?"},
            //      10                              11                      12              13                      14                       15                      16                                      17                                            18                    19                          110                 111                     112                         113                     114
            {"No Enough Balance","Unable to Find","Company Name Already Exit !","","","","","","","","","","","",""}
            //     20                   21              22
        };
        public static String[] OTHERS_VARIALES = new String[5]
        {
            "Product Return","Sales Return","Purchase","Invoices"," History"
        };
        public static String[] ACCOUNT_TYPE = new String[3]
        {
            "Creditor","Debtor","Employ"
        };
        public static String[] TABLE_NAME = new String[19]
        {
            "pet_bank_account","pet_bank_ledger","pet_cash_ledger","pet_credit_debtor_ledger","pet_current_stock","pet_debtor_leger","pet_expance_ledger","pet_ofice_instrument_ledger","pet_order_delivery","pet_personal_account","pet_personal_information","pet_product_information","pet_purchase_ledger","pet_salary_ledger","pet_sales_ledger","pet_cash_receive_pament","pet_recharge_company_info","pet_recharge_amount","pet_recharge_histiroy"
                    // 0               1               2                      3                           4                 5               6                     7                           8                       9                  10                         11                           12                    13                14                  15                         16                          17                      18
        };
        public static String[] COLUMN_NAME = new String[17]
        {
            "EntryDate","Amount","IssueName","UserID","ProductID","Rate","Quantity","CompanyName","DeliveryDate","Complete","ProductName","ModelName","AccountType","Transaction","Description","InvoiceNumber","AvgRate"
            // 0           1           2         3         4        5         6           7            8              9  .       10              11          12         13              14          15              16
        };
        public static String[] CompanyInfo = new String[3]{
            "SIKDER TELECOM","A Total Solution Of Telecommunication","Room # 108, Nizam`s Shankar Plaza,72 Satmosij Road, Dhanmondi,Dhaka-1209 Mob: 01712038344,01191194755,E-mail: sikdersbd@yahoo.com"
            //       0                              1                                                                           2
        };
        public void InitializeLoaclData()
        {
            foreach (string tableName in TABLE_NAME)
            {
                AccountClass.thisDataSet.ThisTables.Rows.Add(tableName);
            }
            foreach (string columnName in COLUMN_NAME)
            {
                AccountClass.thisDataSet.ThisColumn.Rows.Add(columnName);
            }
        }
    }
}
