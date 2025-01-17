﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyQrCodeScanner
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window, INotifyPropertyChanged
    {
        private bool cont;
        private bool isdailog;
        public ResultWindow(string res,string type,bool isdialog1,bool cancontinue)
        {
            InitializeComponent();
            this.DataContext = this;
            isdailog = isdialog1;
            Data = res;
            if (!CheckURI(res))
                grid1.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
            CodeType = type;
            if (!cancontinue)
                grid1.ColumnDefinitions[2].Width = new GridLength(0, GridUnitType.Star);
        }

        #region 属性
        private string codetype;

        public string CodeType
        {
            get { return codetype; }
            set
            {
                codetype = value;
                this.OnPropertyChanged("CodeType");
            }
        }

        private string data;

        public string Data
        {
            get { return data; }
            set
            {
                data = value;
                this.OnPropertyChanged("Data");
            }
        }

        private bool CheckURI(string s)
        {
            try
            {
                var t = new Uri(s);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(text1.Text);
        }

        private void ButtonCopy_Click(object sender, RoutedEventArgs e)
        {
            text1.SelectAll();
            text1.Copy();
            ButtonCopy.Style = (Style)Application.Current.Resources["MaterialDesignRaisedButton"];
            ButtonCopyText.Text = "已复制";
        }

        private void ButtonContinue_Click(object sender, RoutedEventArgs e)
        {
            cont = true;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isdailog)
                this.DialogResult = cont;
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(text1.Text);
            Application.Current.Shutdown();
        }

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion
    }
}
