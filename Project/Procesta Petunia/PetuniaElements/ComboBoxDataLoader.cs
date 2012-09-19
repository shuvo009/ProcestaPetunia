//Title :           Petunia Class Combo box Data Loader.
//Version :         2.0.0.0
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

namespace PetuniaElements
{
    public class ComboBoxDataLoader
    {
        public void ComboBoxItemLoad(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string query)
        {
            devComboBox.Items.Clear();
            DataTable tempDataTable = new DataTable();
            tempDataTable = new MySqlNaceassaryElement().DataReader(query);
            foreach (DataRow comboItems in tempDataTable.Rows)
            {
                devComboBox.Items.Add(comboItems[0]);
            }
            tempDataTable.Dispose();
        }
        //Company Name ComboBox Item Loader
        public void CompanyNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", Variables.COLUMN_NAME[7], Variables.TABLE_NAME[11]));
        }
        //Product name ComboBox Item Loader
        public void ProductNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string companyName)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2}='{3}' ORDER BY {0}", Variables.COLUMN_NAME[10], Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], companyName));
        }
        //Model Number ComboBox Item Loader
        public void ModelNumberLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string companyName, string productName)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2}='{3}' AND {4}='{5}' ORDER BY {0}", Variables.COLUMN_NAME[11], Variables.TABLE_NAME[11], Variables.COLUMN_NAME[7], companyName, Variables.COLUMN_NAME[10], productName));
        }
        //Creditors UserName Loader
        public void AccTypeUserNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox, string AccType)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} WHERE {2}='{3}' ORDER BY {0}", Variables.COLUMN_NAME[3], Variables.TABLE_NAME[10],Variables.COLUMN_NAME[12], AccType));
        }
        //All UserName Loader
        public void AllUserNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT DISTINCT {0} FROM {1} ORDER BY {0}", Variables.COLUMN_NAME[3], Variables.TABLE_NAME[10]));

        }
        //Recharge Company Name Loader
        public void RechargeCompanyNameLoader(DevExpress.Xpf.Editors.ComboBoxEdit devComboBox)
        {
            devComboBox.Text = string.Empty;
            ComboBoxItemLoad(devComboBox, string.Format("SELECT {0} FROM {1}", Variables.COLUMN_NAME[7], Variables.TABLE_NAME[16]));
        }
    }
}
