//Title :           Necessary Function.
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2010
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     All Kind Functions.  

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcestaRegeditClasss
{
    class RegeditNecessaryFunction
    {
        //convert string to byte array
        public static byte[] ToByteArray(string mystring)
        {
            string[] bytestring = mystring.Split(',');
            byte[] byteOut = new byte[bytestring.Length];
            for (int i = 0; i < bytestring.Length; i++)
            {
                byteOut[i] = Convert.ToByte(bytestring[i]);
            }
            return byteOut;
        }
        //convert string to string array
        public static string[] ToStringArray(string myString)
        {
            string[] stringout=myString.Split(',');
            return stringout;
        }
    }
}
