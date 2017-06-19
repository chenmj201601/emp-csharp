//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    43c8a6e0-9e6f-4622-a0f9-f883254b4f19
//        CLR Version:              4.0.30319.42000
//        Name:                     UCComponentModify
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCComponentModify
//
//        Created by Charley at 2017/5/24 17:19:53
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCComponentModify.xaml 的交互逻辑
    /// </summary>
    public partial class UCComponentModify
    {

        public GridCell GridCell;
        public ComponentItem Item;
        public IList<ComponentItem> ListComponentItems;

        private bool mIsInited;
        private string mIconFile;
        private ObservableCollection<ListItem> mListGroupItems = new ObservableCollection<ListItem>();

        public UCComponentModify()
        {
            InitializeComponent();

            Loaded += UCComponentModify_Loaded;
            BtnConfirm.Click += BtnConfirm_Click;
            BtnClose.Click += BtnClose_Click;
            BtnIcon.Click += BtnIcon_Click;
        }


        void UCComponentModify_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {
            ComboGroups.ItemsSource = mListGroupItems;
            InitComponentGroups();
            var cell = GridCell;
            if (cell == null) { return; }


            #region ComponentInfo

            ComponentInfo component = new ComponentInfo();
            int id = 1000;
            if (ListComponentItems != null)
            {
                foreach (var componentItem in ListComponentItems)
                {
                    var info = componentItem.Info;
                    if (info != null)
                    {
                        id = Math.Max(id, info.ID);
                    }
                }
            }
            id++;
            component.ID = id;
            component.Name = "新元件";
            component.Description = string.Empty;
            component.IsBuildIn = false;

            #endregion


            ComponentItem item = new ComponentItem();
            item.Info = component;
            item.Name = component.Name;
            item.Display = component.Name;
            item.Description = component.Description;
            DataContext = item;
        }

        private void InitComponentGroups()
        {
            mListGroupItems.Clear();
            ListItem item = new ListItem();
            item.Name = "Basic";
            item.Display = "常用元件";
            item.IntValue = ComponentInfo.GP_BASIC;
            mListGroupItems.Add(item);
            item = new ListItem();
            item.Name = "Shap";
            item.Display = "形状";
            item.IntValue = ComponentInfo.GP_SHAP;
            mListGroupItems.Add(item);
            item = new ListItem();
            item.Name = "Chart";
            item.Display = "图表";
            item.IntValue = ComponentInfo.GP_CHART;
            mListGroupItems.Add(item);
            item = new ListItem();
            item.Name = "Other";
            item.Display = "其他";
            item.IntValue = ComponentInfo.GP_OTHER;
            mListGroupItems.Add(item);
        }

        void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var popup = Parent as PopupWindow;
            if (popup != null)
            {
                popup.Close();
            }
        }

        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var item = DataContext as ComponentItem;
            if (item == null) { return; }
            string strName = item.Name;
            if (string.IsNullOrEmpty(strName))
            {
                ShowException(string.Format("元件名称不能为空！"));
                return;
            }
            var groupItem = ComboGroups.SelectedItem as ListItem;
            if (groupItem == null)
            {
                ShowException(string.Format("请选择一个分组！"));
                return;
            }
            var info = item.Info;
            if (info == null) { return; }
            int id = info.ID;
            info.Name = strName;
            info.Description = item.Description;
            info.GroupID = groupItem.IntValue;
            if (!string.IsNullOrEmpty(mIconFile))
            {
                string strTarget = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "components");
                if (!Directory.Exists(strTarget))
                {
                    Directory.CreateDirectory(strTarget);
                }
                strTarget = Path.Combine(strTarget, string.Format("{0}.png", id));
                try
                {
                    File.Copy(mIconFile, strTarget, true);
                }
                catch { }
                BitmapImage bitmap = new BitmapImage(new Uri(strTarget, UriKind.RelativeOrAbsolute));
                item.Icon = bitmap.Clone();
            }

            var cell = GridCell;
            if (cell != null)
            {

                #region 样式

                VisualStyle style = new VisualStyle();
                style.FontFamily = cell.FontFamily.ToString();
                style.FontSize = (int)cell.FontSize;
                var fontWeight = cell.FontWeight;
                if (fontWeight == FontWeights.Bold)
                {
                    style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Bold;
                }
                var fontStyle = cell.FontStyle;
                if (fontStyle == FontStyles.Italic)
                {
                    style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Italic;
                }
                var textDecration = cell.TextDecration;
                if (textDecration != null
                    && Equals(textDecration, TextDecorations.Underline))
                {
                    style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Underlined;
                }
                var fontBrush = cell.Foreground as SolidColorBrush;
                if (fontBrush != null)
                {
                    style.ForeColor = fontBrush.Color.ToString();
                }
                var fillBrush = cell.Background as SolidColorBrush;
                if (fillBrush != null)
                {
                    style.BackColor = fillBrush.Color.ToString();
                }
                style.HAlign = (int)cell.HAlign;
                style.VAlign = (int)cell.VAlign;
                info.Style = style;

                #endregion


                #region 内容

                var element = cell.Content as ICellElement;
                ReportElement reportElement = null;
                if (element != null)
                {
                    var textElement = element as TextElement;
                    if (textElement != null)
                    {
                        ReportText reportText = textElement.ToReport();
                        reportElement = reportText;
                    }
                    var sequenceElement = element as SequenceElement;
                    if (sequenceElement != null)
                    {
                        ReportSequence reportSequence = sequenceElement.ToReport();
                        reportElement = reportSequence;
                    }
                    var imageElement = element as ImageElement;
                    if (imageElement != null)
                    {
                        ReportImage reportImage = imageElement.ToReport();
                        var fullName = imageElement.SourceFile;
                        imageElement.SetExt(fullName);


                        #region 保存资源文件

                        string target = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "components");
                        target = Path.Combine(target, "resources");
                        if (!Directory.Exists(target))
                        {
                            Directory.CreateDirectory(target);
                        }
                        string strExt = imageElement.GetExtName();
                        target = Path.Combine(target, string.Format("{0}{1}", reportImage.ID, strExt));
                        try
                        {
                            File.Copy(fullName, target, true);
                        }
                        catch { }

                        #endregion


                        reportElement = reportImage;
                    }
                }
                if (reportElement != null)
                {
                    info.Element = reportElement;
                }

                #endregion

            }


            Item = item;
            var popup = Parent as PopupWindow;
            if (popup != null)
            {
                popup.DialogResult = true;
                popup.Close();
            }
        }

        void BtnIcon_Click(object sender, RoutedEventArgs e)
        {
            var item = DataContext as ComponentItem;
            if (item == null) { return; }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = string.Format("选择元件图标");
            dialog.Filter = "PNG 图标|*.png";
            var result = dialog.ShowDialog();
            if (result != true) { return; }
            mIconFile = dialog.FileName;
            BitmapImage bitmap = new BitmapImage(new Uri(mIconFile, UriKind.RelativeOrAbsolute));
            item.Icon = bitmap.Clone();
        }

        private void ShowException(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInfomation(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

    }
}
