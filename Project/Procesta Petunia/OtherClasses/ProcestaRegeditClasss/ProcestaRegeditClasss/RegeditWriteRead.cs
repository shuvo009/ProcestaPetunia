//Title :           Registry Read And Write.
//Version :         1.0.0.5
//Copyright :       Copyright (c) 2010
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Supports all kind Registry value type.  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using ProcestaVariables;
namespace ProcestaRegeditClasss
{
    public class RegeditWriteRead
    {
        //Write Registry Value
        public bool RegistryWriter(string subKey, string keyName,string keyValue, RegistryValueKind valueType)
        {
            try
            {
                RegistryPermission regeditPermission = new RegistryPermission(RegistryPermissionAccess.AllAccess,System.IO.Path.Combine(Registry.CurrentUser.ToString(),System.IO.Path.Combine(ProcestaVariables.Variables.REGISTRY_PATH,subKey)));
                regeditPermission.Demand();
                RegistryKey regeditWrite=Registry.CurrentUser;
                //regeditWrite.SetAccessControl(regeditUserRuls);
                regeditWrite = regeditWrite.OpenSubKey(System.IO.Path.Combine(ProcestaVariables.Variables.REGISTRY_PATH, subKey), true);
                if(regeditWrite!=null)
                {
                    if(valueType.Equals(RegistryValueKind.Binary))
                    {
                        regeditWrite.SetValue(keyName,RegeditNecessaryFunction.ToByteArray(keyValue),RegistryValueKind.Binary);
                        regeditWrite.Close();
                        return true;
                    }
                    else if(valueType.Equals(RegistryValueKind.DWord))
                    {
                        regeditWrite.SetValue(keyName,Convert.ToInt32(keyValue),RegistryValueKind.DWord);
                        regeditWrite.Close();
                        return true;
                    }
                    else if( valueType.Equals(RegistryValueKind.ExpandString))
                    {
                        regeditWrite.SetValue(keyName,keyValue,RegistryValueKind.ExpandString);
                        regeditWrite.Close();
                        return true;
                    }
                    else if(valueType.Equals(RegistryValueKind.MultiString))
                    {
                        regeditWrite.SetValue(keyName,RegeditNecessaryFunction.ToStringArray(keyValue),RegistryValueKind.MultiString);
                        regeditWrite.Close();
                        return true;
                    }
                    else if(valueType.Equals(RegistryValueKind.QWord))
                    {
                        regeditWrite.SetValue(keyName,Convert.ToInt32(keyValue),RegistryValueKind.QWord);
                        regeditWrite.Close();
                        return true;
                    }
                    else
                    {
                        regeditWrite.SetValue(keyName,keyValue,RegistryValueKind.String);
                        regeditWrite.Close();
                        return true;
                    }
                }
                else
                {
                    RegistryKey regKeyCrator=Registry.CurrentUser;
                    regKeyCrator = regKeyCrator.CreateSubKey(System.IO.Path.Combine(ProcestaVariables.Variables.REGISTRY_PATH, subKey), RegistryKeyPermissionCheck.Default);
                    
                    regKeyCrator.Close();
                    RegistryWriter(subKey,keyName,keyValue,valueType);
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0,3] + Environment.NewLine + e, ProcestaVariables.Variables.ERROR_MESSAGES[0,0], MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
        }
        //Read Registry
        public string RegistryReadr(string subKey,string keyName)
        {
            try
            {
                RegistryPermission regeditPermission = new RegistryPermission(RegistryPermissionAccess.Read, System.IO.Path.Combine(Registry.CurrentUser.ToString(), System.IO.Path.Combine(ProcestaVariables.Variables.REGISTRY_PATH, subKey)));
                regeditPermission.Demand();
                RegistryKey regeditRead = Registry.CurrentUser;
                regeditRead = regeditRead.OpenSubKey(System.IO.Path.Combine(ProcestaVariables.Variables.REGISTRY_PATH, subKey));
                if (regeditRead != null)
                {
                    if ((regeditRead.GetValue(keyName)).GetType().Equals(typeof(Int32)))
                    {
                        return regeditRead.GetValue(keyName).ToString();
                    }
                    else
                    {
                        return (string)regeditRead.GetValue(keyName);
                    }
                }
                else
                {
                    RegistryWriter(subKey,keyName,string.Empty,RegistryValueKind.String);
                    RegistryReadr(subKey, keyName);
                    return null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(ProcestaVariables.Variables.ERROR_MESSAGES[0,4], ProcestaVariables.Variables.ERROR_MESSAGES[0,0], MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
