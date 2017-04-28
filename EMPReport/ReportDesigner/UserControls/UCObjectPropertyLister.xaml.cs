//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6dd17312-0ba1-4e0b-8d78-aadc7bcd4423
//        CLR Version:              4.0.30319.42000
//        Name:                     UCObjectPropertyLister
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCObjectPropertyLister
//
//        Created by Charley at 2017/4/28 10:55:22
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using NetInfo.EMP.Reports.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCObjectPropertyLister.xaml 的交互逻辑
    /// </summary>
    public partial class UCObjectPropertyLister
    {

        #region Members

        private bool mIsInited;

        private ObservableCollection<ObjectPropertyInfoItem> mListPropertyItems =
            new ObservableCollection<ObjectPropertyInfoItem>();

        private ICellElement mCellElement;

        #endregion


        public UCObjectPropertyLister()
        {
            InitializeComponent();
            Loaded += UCObjectPropertyLister_Loaded;
            TxtElementText.TextChanged += TxtElementText_TextChanged;
        }

        void UCObjectPropertyLister_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }


        #region Init and Load

        private void Init()
        {
            ListBoxPropertyList.ItemsSource = mListPropertyItems;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListBoxPropertyList.ItemsSource);
            if (view != null && view.GroupDescriptions != null)
            {
                view.GroupDescriptions.Clear();
                view.GroupDescriptions.Add(new PropertyGroupDescription("GroupName"));
            }
            double width = ActualWidth;
            if (!double.IsNaN(width))
            {
                NameWidth = width * 2 / 5;
            }
            InitPropertyItems();
            InitInfo();
            InitValue();
        }

        private void InitPropertyItems()
        {
            mListPropertyItems.Clear();
            var textElement = DataContext as TextElement;
            if (textElement != null)
            {
                var properties = TextElementPropertyFactory.GetPropertyList();
                for (int i = 0; i < properties.Count; i++)
                {
                    var info = properties[i];
                    var item = new ObjectPropertyInfoItem();
                    item.Info = info;
                    item.ID = info.ID;
                    item.PropertyName = info.Name;
                    item.GroupName = GetGroupName(info.GroupID);
                    mListPropertyItems.Add(item);
                }
            }
        }

        private void InitInfo()
        {
            if (DataContext == null) { return; }
            mCellElement = DataContext as ICellElement;
            if (mCellElement == null) { return; }
            var textElement = mCellElement as TextElement;
            if (textElement != null)
            {
                StrElementType = "静态文本";
                StrElementText = textElement.Text;
            }
        }

        private void InitValue()
        {
            if (mCellElement == null) { return; }
            TextElement textElement = mCellElement as TextElement;
            if (textElement != null)
            {
                var property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FONTFAMILY);
                if (property != null)
                {
                    property.Value = textElement.FontFamily.ToString();
                }
                property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FONTSIZE);
                if (property != null)
                {
                    property.Value = textElement.FontSize.ToString();
                }
                property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FONTSTYLE);
                if (property != null)
                {
                    property.Value = textElement.FontStyle.ToString();
                }
            }
        }

        public void Refresh()
        {
            InitPropertyItems();
            InitInfo();
            InitValue();
        }

        #endregion


        #region Others

        private string GetGroupName(int groupID)
        {
            string str = string.Empty;
            switch (groupID)
            {
                case 1:
                    str = "外观";
                    break;
                case 2:
                    str = "布局";
                    break;
                case 3:
                    str = "边框";
                    break;
            }
            return str;
        }

        #endregion


        #region NameWidthProperty

        public static readonly DependencyProperty NameWidthProperty =
            DependencyProperty.Register("NameWidth", typeof(double), typeof(UCObjectPropertyLister), new PropertyMetadata(default(double)));

        public double NameWidth
        {
            get { return (double)GetValue(NameWidthProperty); }
            set { SetValue(NameWidthProperty, value); }
        }

        #endregion


        #region StrElementTypeProperty

        public static readonly DependencyProperty StrElementTypeProperty =
            DependencyProperty.Register("StrElementType", typeof(string), typeof(UCObjectPropertyLister), new PropertyMetadata(default(string)));

        public string StrElementType
        {
            get { return (string)GetValue(StrElementTypeProperty); }
            set { SetValue(StrElementTypeProperty, value); }
        }

        #endregion


        #region StrElementTextProperty

        public static readonly DependencyProperty StrElementTextProperty =
            DependencyProperty.Register("StrElementText", typeof(string), typeof(UCObjectPropertyLister), new PropertyMetadata(default(string)));

        public string StrElementText
        {
            get { return (string)GetValue(StrElementTextProperty); }
            set { SetValue(StrElementTextProperty, value); }
        }

        #endregion


        #region EventHandlers

        private void Thumb_OnDragDelta(object sender, DragDeltaEventArgs e)
        {
            try
            {
                NameWidth = NameWidth + e.HorizontalChange;
            }
            catch { }
        }

        private void TxtElementText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textElement = mCellElement as TextElement;
            if (textElement != null)
            {
                textElement.Text = TxtElementText.Text;
            }
        }

        #endregion

    }
}
