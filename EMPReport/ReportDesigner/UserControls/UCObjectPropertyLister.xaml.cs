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
using System.Windows.Media;
using NetInfo.EMP.Reports;
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

        public ReportDesignPanel Panel;

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
            AddHandler(UCObjectPropertyEditor.PropertyValueChangedEvent,
                new RoutedPropertyChangedEventHandler<PropertyValueChangedEventArgs>(Editor_PropertyValueChanged));
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
            InitInfo();
            InitPropertyItems();
            InitValue();
        }

        private void InitPropertyItems()
        {
            mListPropertyItems.Clear();
            var sequenceElement = DataContext as SequenceElement;
            if (sequenceElement != null)
            {
                var properties = SequenceElementPropertyFactory.GetPropertyList();
                for (int i = 0; i < properties.Count; i++)
                {
                    var info = properties[i];
                    var item = new ObjectPropertyInfoItem();
                    item.Info = info;
                    item.ID = info.ID;
                    item.PropertyName = info.Name;
                    item.GroupName = GetGroupName(info.GroupID);
                    item.ObjectInstance = mCellElement;
                    item.Panel = Panel;
                    mListPropertyItems.Add(item);
                }
                return;
            }
            var textElement = DataContext as EditableElement;
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
                    item.ObjectInstance = mCellElement;
                    item.Panel = Panel;
                    mListPropertyItems.Add(item);
                }
            }
        }

        private void InitInfo()
        {
            if (DataContext == null) { return; }
            mCellElement = DataContext as ICellElement;
            if (mCellElement == null) { return; }
            var sequenceElement = mCellElement as SequenceElement;
            if (sequenceElement != null)
            {
                StrElementType = "数据列";
                StrElementText = sequenceElement.Text;
                ElementTextReadOnly = true;
                return;
            }
            var textElement = mCellElement as EditableElement;
            if (textElement != null)
            {
                StrElementType = "静态文本";
                StrElementText = textElement.Text;
                ElementTextReadOnly = false;
            }
        }

        private void InitValue()
        {
            if (mCellElement == null) { return; }


            #region TextElement

            EditableElement textElement = mCellElement as EditableElement;
            if (textElement != null)
            {
                ObjectPropertyInfoItem property;
                var cell = textElement.Parent as GridCell;
                if (cell != null)
                {
                    property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FONTFAMILY);
                    if (property != null)
                    {
                        property.Value = cell.FontFamily.ToString();
                    }
                    property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FONTSIZE);
                    if (property != null)
                    {
                        property.Value = cell.FontSize.ToString();
                    }
                    property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FONTSTYLE);
                    if (property != null)
                    {
                        property.Value = cell.FontStyle.ToString();
                    }
                    property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_FORECOLOR);
                    if (property != null)
                    {
                        var brush = cell.Foreground as SolidColorBrush;
                        if (brush != null)
                        {
                            property.Value = brush.Color.ToString();
                        }
                    }
                    property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_BACKCOLOR);
                    if (property != null)
                    {
                        var brush = cell.Background as SolidColorBrush;
                        if (brush != null)
                        {
                            property.Value = brush.Color.ToString();
                        }
                    }
                }
                property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_HALIGN);
                if (property != null)
                {
                    property.Value = ((int)textElement.HAlign).ToString();
                }
                property = mListPropertyItems.FirstOrDefault(p => p.ID == TextElementPropertyFactory.PRO_VALIGN);
                if (property != null)
                {
                    property.Value = ((int)textElement.VAlign).ToString();
                }
            }

            #endregion


            #region SequenceElement

            var sequenceElement = mCellElement as SequenceElement;
            if (sequenceElement != null)
            {
                var property = mListPropertyItems.FirstOrDefault(p => p.ID == SequenceElementPropertyFactory.PRO_DATASET);
                if (property != null)
                {
                    var dataSet = sequenceElement.DataSet;
                    if (dataSet != null)
                    {
                        property.Value = dataSet.Name;
                    }
                    else
                    {
                        property.Value = "";
                    }
                }
                property = mListPropertyItems.FirstOrDefault(p => p.ID == SequenceElementPropertyFactory.PRO_DATAFIELD);
                if (property != null)
                {
                    var dataField = sequenceElement.DataField;
                    if (dataField != null)
                    {
                        property.Value = dataField.Name;
                    }
                    else
                    {
                        property.Value = "";
                    }
                }
            }

            #endregion

        }

        public void Refresh()
        {
            InitInfo();
            InitPropertyItems();
            InitValue();
        }

        #endregion


        #region Others

        private string GetGroupName(int groupID)
        {
            string str = string.Empty;
            switch (groupID)
            {
                case 0:
                    str = "基本";
                    break;
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


        #region ElementTextReadOnlyProperty

        public static readonly DependencyProperty ElementTextReadOnlyProperty =
            DependencyProperty.Register("ElementTextReadOnly", typeof(bool), typeof(UCObjectPropertyLister), new PropertyMetadata(default(bool)));

        public bool ElementTextReadOnly
        {
            get { return (bool)GetValue(ElementTextReadOnlyProperty); }
            set { SetValue(ElementTextReadOnlyProperty, value); }
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
            var textElement = mCellElement as EditableElement;
            if (textElement != null)
            {
                textElement.Text = TxtElementText.Text;
            }
        }

        private void Editor_PropertyValueChanged(object sender,
            RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            var item = args.PropertyItem;
            if (item == null) { return; }
            string strValue = args.Value;
            int id = item.ID;


            #region TextElement

            var textElement = mCellElement as EditableElement;
            if (textElement != null)
            {
                var cell = textElement.Parent as GridCell;
                if (cell != null)
                {
                    if (id == TextElementPropertyFactory.PRO_FONTFAMILY)
                    {
                        var valueItem = args.ValueItem;
                        if (valueItem != null)
                        {
                            var fontFamily = valueItem.Info as FontFamily;
                            if (fontFamily != null)
                            {
                                cell.FontFamily = fontFamily;
                            }
                        }
                    }
                    if (id == TextElementPropertyFactory.PRO_FONTSIZE)
                    {
                        int fontSize;
                        if (int.TryParse(strValue, out fontSize)
                            && fontSize > 0)
                        {
                            cell.FontSize = fontSize;
                        }
                    }
                    if (id == TextElementPropertyFactory.PRO_FONTSTYLE)
                    {

                    }
                    if (id == TextElementPropertyFactory.PRO_FORECOLOR)
                    {
                        var color = ColorConverter.ConvertFromString(strValue);
                        if (color != null)
                        {
                            cell.Foreground = new SolidColorBrush((Color)color);
                        }
                    }
                    if (id == TextElementPropertyFactory.PRO_BACKCOLOR)
                    {
                        var color = ColorConverter.ConvertFromString(strValue);
                        if (color != null)
                        {
                            cell.Background = new SolidColorBrush((Color)color);
                        }
                    }
                }
                if (id == TextElementPropertyFactory.PRO_HALIGN)
                {
                    var valueItem = args.ValueItem;
                    string value = valueItem.Value;
                    int intValue;
                    if (int.TryParse(value, out intValue))
                    {
                        textElement.HAlign = (HorizontalAlignment)intValue;
                    }
                }
                if (id == TextElementPropertyFactory.PRO_VALIGN)
                {
                    var valueItem = args.ValueItem;
                    string value = valueItem.Value;
                    int intValue;
                    if (int.TryParse(value, out intValue))
                    {
                        textElement.VAlign = (VerticalAlignment)intValue;
                    }
                }
            }

            #endregion


            #region SequenceElement

            var sequenceElement = mCellElement as SequenceElement;
            if (sequenceElement != null)
            {
                if (id == SequenceElementPropertyFactory.PRO_DATASET)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        var dataSet = valueItem.Info as ReportDataSet;
                        if (dataSet != null)
                        {
                            sequenceElement.DataSet = dataSet;
                            //关联属性编辑框重新加载
                            var relativeProperty =
                                mListPropertyItems.FirstOrDefault(
                                    p => p.ID == SequenceElementPropertyFactory.PRO_DATAFIELD);
                            if (relativeProperty != null)
                            {
                                var editor = relativeProperty.Editor;
                                if (editor != null)
                                {
                                    editor.Reload();
                                }
                            }
                        }
                    }
                }
                if (id == SequenceElementPropertyFactory.PRO_DATAFIELD)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        var dataField = valueItem.Info as ReportDataField;
                        if (dataField != null)
                        {
                            sequenceElement.DataField = dataField;
                            //修改Text属性
                            var dataSet = dataField.DataSet;
                            if (dataSet != null)
                            {
                                sequenceElement.Text = string.Format("={{{0}}}.{{{1}}}", dataSet.Name, dataField.Name);
                            }
                        }
                    }
                }
            }

            #endregion

        }

        #endregion

    }
}
