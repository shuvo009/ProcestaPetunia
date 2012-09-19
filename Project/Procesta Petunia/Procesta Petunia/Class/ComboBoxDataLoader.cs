//Title :           Petunia Class Combo box Data Loader.
//Version :         1.0.0.1
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Combo box type is DevExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProcestaVariables;
using MysqlClass;
using System.Data;

namespace Procesta_Petunia.Class
{
    class ComboBoxDataLoader
    {
        public void ComboBoxItemLoad(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string query)
        {
            devComboBox.Items.Clear();
            DataTable tempDataTable = new DataTable();
            MySqlNaceassaryElement mysqlNecessaryFunction = new MySqlNaceassaryElement();
            tempDataTable = mysqlNecessaryFunction.DataReader(query);
            foreach (DataRow comboItems in tempDataTable.Rows)
            {
                devComboBox.Items.Add(comboItems[0]);
            }
        }
        //Company Name ComboBox Item Loader
        public void CompanyNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", Variables.COLUMN_NAME[13], Variables.TABLE_NAME[15]));
        }
        //Product name ComboBox Item Loader
        public void ProductNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string companyName)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2}='{3}' ORDER BY {0}", Variables.COLUMN_NAME[14], Variables.TABLE_NAME[15], Variables.COLUMN_NAME[13], companyName));
        }
        //Model Number ComboBox Item Loader
        public void ModelNumberLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string companyName, string productName)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2}='{3}' AND {4}='{5}' ORDER BY {0}", Variables.COLUMN_NAME[15], Variables.TABLE_NAME[15], Variables.COLUMN_NAME[13], companyName, Variables.COLUMN_NAME[14], productName));
        }
        //Creditors UserName Loader
        public void AccTypeUserNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string AccType)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2}='{3}' ORDER BY {0}", Variables.COLUMN_NAME[12], Variables.TABLE_NAME[4], Variables.COLUMN_NAME[16], AccType));
        }
        //All UserName Loader
        public void AllUserNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", Variables.COLUMN_NAME[12], Variables.TABLE_NAME[4]));

        }
        //Account Type Loader
        public void AccountTypeLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Items.Clear();
            devComboBox.Text = string.Empty;
            foreach (string accType in Variables.ACCOUNT_TYPE)
            {
                devComboBox.Items.Add(accType);
            }
        }
        //Expanses Type Loader
        public void ExpansesTypeLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Items.Clear();
            devComboBox.Text = string.Empty;
            foreach (string ExpType in Variables.EXPASES_TYPE)
            {
                devComboBox.Items.Add(ExpType);
            }
        }
    }
}
