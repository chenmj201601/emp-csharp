//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    05a2d43b-be24-47d7-b6ba-02526e1cc821
//        CLR Version:              4.0.30319.42000
//        Name:                     UCReportImageModify
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCReportImageModify
//
//        Created by Charley at 2017/5/25 10:07:15
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NetInfo.EMP.Reports.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCReportImageModify.xaml 的交互逻辑
    /// </summary>
    public partial class UCReportImageModify
    {

        public GridCell GridCell;
        public ImageElement ImageElement;
        public bool IsModify;

        private bool mIsInited;
        private ObservableCollection<ListItem> mListStretchModes = new ObservableCollection<ListItem>();


        public UCReportImageModify()
        {
            InitializeComponent();

            Loaded += UCReportImageModify_Loaded;
            BtnImage.Click += BtnImage_Click;
            BtnConfirm.Click += BtnConfirm_Click;
            BtnClose.Click += BtnClose_Click;
        }

        void UCReportImageModify_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {
            ComboStretch.ItemsSource = mListStretchModes;
            InitStretchModes();
            if (GridCell == null) { return; }
            if (IsModify)
            {
                ImageElement imageElement = ImageElement;
                if (imageElement == null) { return; }
                TxtWidth.Text = imageElement.ImageWidth.ToString();
                TxtHeight.Text = imageElement.ImageHeight.ToString();
                var item = mListStretchModes.FirstOrDefault(i => i.IntValue == (int)imageElement.Stretch);
                ComboStretch.SelectedItem = item;
                DataContext = imageElement;
            }
            else
            {
                ImageElement imageElement = new ImageElement();
                imageElement.ID = Guid.NewGuid().ToString();
                imageElement.Cell = GridCell;
                imageElement.Text = string.Empty;
                imageElement.Stretch = Stretch.Uniform;
                imageElement.SourceFile = string.Empty;
                imageElement.IsSourceUpdated = true;
                TxtWidth.Text = imageElement.ImageWidth.ToString();
                TxtHeight.Text = imageElement.ImageHeight.ToString();
                var item = mListStretchModes.FirstOrDefault(i => i.IntValue == (int)imageElement.Stretch);
                ComboStretch.SelectedItem = item;
                DataContext = imageElement;
            }
        }

        private void InitStretchModes()
        {
            mListStretchModes.Clear();
            ListItem item = new ListItem();
            item.Name = "None";
            item.Display = "无";
            item.Tip = item.Display;
            item.IntValue = 0;
            mListStretchModes.Add(item);
            item = new ListItem();
            item.Name = "Fill";
            item.Display = "铺满";
            item.Tip = item.Display;
            item.IntValue = 1;
            mListStretchModes.Add(item);
            item = new ListItem();
            item.Name = "Uniform";
            item.Display = "保留长宽比";
            item.Tip = item.Display;
            item.IntValue = 2;
            mListStretchModes.Add(item);
            item = new ListItem();
            item.Name = "UniformToFill";
            item.Display = "保留长宽比铺满";
            item.Tip = item.Display;
            item.IntValue = 3;
            mListStretchModes.Add(item);
        }

        void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var parent = Parent as PopupWindow;
            if (parent != null)
            {
                parent.Close();
            }
        }

        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var imageElement = DataContext as ImageElement;
            if (imageElement == null) { return; }
            string text = imageElement.Text;
            if (string.IsNullOrEmpty(text))
            {
                ShowException(string.Format("替换文本不能为空！"));
                return;
            }
            var stretchItem = ComboStretch.SelectedItem as ListItem;
            if (stretchItem == null)
            {
                ShowException(string.Format("请选择图片拉伸模式！"));
                return;
            }
            imageElement.Stretch = (Stretch)stretchItem.IntValue;
            int intValue;
            if (int.TryParse(TxtWidth.Text, out intValue)
                && intValue > 0 && intValue < 1000)
            {
                imageElement.ImageWidth = intValue;
            }
            if (int.TryParse(TxtHeight.Text, out intValue)
               && intValue > 0 && intValue < 1000)
            {
                imageElement.ImageHeight = intValue;
            }
            ImageElement = imageElement;
            var popup = Parent as PopupWindow;
            if (popup != null)
            {
                popup.DialogResult = true;
                popup.Close();
            }
        }

        void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            var imageElement = DataContext as ImageElement;
            if (imageElement == null) { return; }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择图片";
            dialog.Filter = "Png 图片|*.png|Bmp 图片|*.bmp|Jpg 图片|*.jpg";
            var result = dialog.ShowDialog();
            if (result != true) { return; }
            string fullName = dialog.FileName;
            BitmapImage image = new BitmapImage(new Uri(fullName, UriKind.RelativeOrAbsolute));
            imageElement.ImageSource = image.Clone();
            imageElement.SourceFile = fullName;
            imageElement.IsSourceUpdated = true;
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
