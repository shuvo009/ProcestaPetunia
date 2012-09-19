//Title :           Petunia.
//Version :         1.0.0.0
//Copyright :       Copyright (c) 2011
//Author :          Md.Hasanuzzaman (shuvo009@live.com)
//Company :         procesta (http://www.procesta.com/)
//Description :     View Crystal Report From Here 
using System;
using System.Windows;

namespace Procesta_Petunia
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        public ReportViewer()
        {
            InitializeComponent();
            ReportViewerView.Owner = this;
        }
    }
}
