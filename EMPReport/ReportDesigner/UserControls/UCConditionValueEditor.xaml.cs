//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    21dafde4-08c6-4987-ad03-f473328a9019
//        CLR Version:              4.0.30319.42000
//        Name:                     UCConditionValueEditor
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCConditionValueEditor
//
//        Created by Charley at 2017/5/31 17:25:06
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCConditionValueEditor.xaml 的交互逻辑
    /// </summary>
    public partial class UCConditionValueEditor
    {

        public ReportConditionItem ConditionItem;
        public DataSetItem DataSetItem;
        public IList<DataFieldItem> ListAllFieldItems;

        private bool mIsInited;
        private readonly ObservableCollection<DataFieldItem> mListFieldItems = new ObservableCollection<DataFieldItem>();


        public UCConditionValueEditor()
        {
            InitializeComponent();

            Loaded += UCConditionValueEditor_Loaded;
            BtnClose.Click += BtnClose_Click;
            BtnConfirm.Click += BtnConfirm_Click;
            RadioText.Click += (s, e) => SetEnableState();
            RadioField.Click += (s, e) => SetEnableState();
            RadioParam.Click += (s, e) => SetEnableState();
        }

        void UCConditionValueEditor_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {
            ComboFields.ItemsSource = mListFieldItems;
            InitFieldItems();
            InitInfo();
        }

        private void InitFieldItems()
        {
            mListFieldItems.Clear();
            if (ListAllFieldItems == null) { return; }
            for (int i = 0; i < ListAllFieldItems.Count; i++)
            {
                mListFieldItems.Add(ListAllFieldItems[i]);
            }
        }

        private void InitInfo()
        {
            var conditionItem = ConditionItem;
            if (conditionItem == null) { return; }
            string strValue = conditionItem.Value;
            RadioText.IsChecked = string.IsNullOrEmpty(strValue) || (!strValue.StartsWith("{") && !strValue.StartsWith("["));
            RadioField.IsChecked = !string.IsNullOrEmpty(strValue) && strValue.StartsWith("{");
            RadioParam.IsChecked = !string.IsNullOrEmpty(strValue) && strValue.StartsWith("[");
            SetEnableState();
            TxtConditionValue.Text = strValue;
            if (!string.IsNullOrEmpty(strValue))
            {
                if (strValue.StartsWith("{"))
                {
                    strValue = strValue.TrimStart('{');
                    strValue = strValue.TrimEnd('}');
                    var item = mListFieldItems.FirstOrDefault(f => f.Name == strValue);
                    ComboFields.SelectedItem = item;
                }
            }
        }

        private void SetEnableState()
        {
            TxtConditionValue.IsEnabled = RadioText.IsChecked == true;
            ComboFields.IsEnabled = RadioField.IsChecked == true;
            ComboParams.IsEnabled = RadioParam.IsChecked == true;
        }

        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var conditionItem = ConditionItem;
            if (conditionItem == null) { return; }
            string strValue = string.Empty;
            if (RadioText.IsChecked == true)
            {
                strValue = TxtConditionValue.Text;
            }
            if (RadioField.IsChecked == true)
            {
                var item = ComboFields.SelectedItem as DataFieldItem;
                if (item == null)
                {
                    ShowException(string.Format("请选择一个字段！"));
                    return;
                }
                strValue = string.Format("{{{0}}}", item.Name);
            }
            conditionItem.Value = strValue;
            var parent = Parent as PopupWindow;
            if (parent != null)
            {
                parent.DialogResult = true;
                parent.Close();
            }
        }

        void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var parent = Parent as PopupWindow;
            if (parent != null)
            {
                parent.Close();
            }
        }


        #region Others

        private void ShowException(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInformation(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion
    }
}
