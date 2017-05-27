//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6cc895e4-2edf-4bfd-b033-20949c2f3f6a
//        CLR Version:              4.0.30319.42000
//        Name:                     UCCellStyleModify
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCCellStyleModify
//
//        Created by Charley at 2017/5/24 15:18:10
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Media;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCCellStyleModify.xaml 的交互逻辑
    /// </summary>
    public partial class UCCellStyleModify
    {

        public GridCell GridCell;
        public CellStyleViewModel Item;

        private bool mIsInited;


        public UCCellStyleModify()
        {
            InitializeComponent();

            Loaded += UCCellStyleModify_Loaded;
            BtnConfirm.Click += BtnConfirm_Click;
            BtnClose.Click += BtnClose_Click;
        }


        void UCCellStyleModify_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {
            var cell = GridCell;
            if (cell == null) { return; }
            string strName = "新样式";
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
            CellStyleInfo cellStyle = new CellStyleInfo();
            cellStyle.Name = strName;
            cellStyle.IsBuiltIn = false;
            cellStyle.Style = style;
            CellStyleViewModel item = new CellStyleViewModel();
            item.CellStyle = cellStyle;
            item.Name = cellStyle.Name;
            item.SetStyle();
            DataContext = item;
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
            var item = DataContext as CellStyleViewModel;
            if (item == null) { return; }
            string strName = item.Name;
            if (string.IsNullOrEmpty(strName))
            {
                ShowException(string.Format("样式名称不能为空！"));
                TxtCellStyleName.Focus();
                return;
            }
            var cellStyle = item.CellStyle;
            if (cellStyle != null)
            {
                cellStyle.Name = strName;
            }
            Item = item;
            var popup = Parent as PopupWindow;
            if (popup != null)
            {
                popup.DialogResult = true;
                popup.Close();
            }
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
