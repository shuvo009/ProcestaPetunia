//Title :           Petunia.
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     Provide Software Information From here.
using System;

namespace Procesta_Petunia.Class
{
    public class ProductInformation
    {
        public string _ProductName = "Petunia";
        private string _ProductVersion="2.0.0.0";
        public string _ReleaseDate = "10/10/2011";
        private string _ComapnyName = "Procesta";

        public string ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value; }
        }
        
        public string ProductVersion
        {
            get { return _ProductVersion; }
            set
            {
                _ProductVersion = value;
            }
        }

        public string CompanyName
        {
            get { return _ComapnyName; }
            set { _ComapnyName = value; }
        }

        public string ReleaseDate
        {
            get { return _ReleaseDate; }
            set { _ReleaseDate = value; }
        }
                
        
    }
}
