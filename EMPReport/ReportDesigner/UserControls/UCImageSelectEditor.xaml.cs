//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    c2cd6b83-2866-4354-a5e8-c6c0a1cb7570
//        CLR Version:              4.0.30319.42000
//        Name:                     UCImageSelectEditor
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCImageSelectEditor
//
//        Created by Charley at 2017/5/26 11:34:07
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using NetInfo.EMP.Reports.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCImageSelectEditor.xaml 的交互逻辑
    /// </summary>
    public partial class UCImageSelectEditor
    {

        private bool mIsInited;
        private ImageElement mImageElement;


        public UCImageSelectEditor()
        {
            InitializeComponent();

            Loaded += UCImageSelectEditor_Loaded;
            BtnSelect.Click += BtnSelect_Click;
        }

        void UCImageSelectEditor_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {
            if (PropertyInfoItem == null) { return; }
            mImageElement = PropertyInfoItem.ObjectInstance as ImageElement;
            if (mImageElement == null) { return; }
        }


        #region PropertyInfoItem

        public static readonly DependencyProperty PropertyInfoItemProperty =
            DependencyProperty.Register("PropertyInfoItem", typeof(ObjectPropertyInfoItem), typeof(UCImageSelectEditor), new PropertyMetadata(default(ObjectPropertyInfoItem)));

        public ObjectPropertyInfoItem PropertyInfoItem
        {
            get { return (ObjectPropertyInfoItem)GetValue(PropertyInfoItemProperty); }
            set { SetValue(PropertyInfoItemProperty, value); }
        }

        #endregion


        void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (mImageElement == null) { return; }
            var cell = mImageElement.Cell;
            if (cell == null) { return; }
            UCReportImageModify uc = new UCReportImageModify();
            uc.IsModify = true;
            uc.GridCell = cell;
            uc.ImageElement = mImageElement;
            PopupWindow popup = new PopupWindow();
            popup.Title = string.Format("修改图片源");
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result != true) { return; }
            var panel = PropertyInfoItem.Panel;
            if (panel != null)
            {
                panel.IsModified = true;
            }
        }

    }
}
