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

        private void InitInfo()
        {
            StrElementType = string.Empty;
            if (DataContext == null) { return; }
            mCellElement = DataContext as ICellElement;
            if (mCellElement == null) { return; }
            var textElement = mCellElement as TextElement;
            if (textElement != null)
            {
                StrElementType = "静态文本";
                StrElementText = textElement.Text;
                ElementTextReadOnly = false;
            }
            var sequenceElement = mCellElement as SequenceElement;
            if (sequenceElement != null)
            {
                StrElementType = "数据列";
                StrElementText = sequenceElement.Text;
                ElementTextReadOnly = true;
            }
            var imageElement = mCellElement as ImageElement;
            if (imageElement != null)
            {
                StrElementType = "图片";
                StrElementText = imageElement.Text;
                ElementTextReadOnly = false;
            }
        }

        private void InitPropertyItems()
        {
            mListPropertyItems.Clear();
            if (mCellElement == null) { return; }
            var properties = ReportPropertyFactory.GetElementProperties(mCellElement);
            for (int i = 0; i < properties.Count; i++)
            {
                var info = properties[i];
                ObjectPropertyInfoItem item = new ObjectPropertyInfoItem();
                item.Info = info;
                item.ID = info.ID;
                item.PropertyName = info.Name;
                item.GroupName = GetGroupName(info.GroupID);
                item.Value = info.DefaultValue;
                item.ObjectInstance = mCellElement;
                item.Panel = Panel;
                mListPropertyItems.Add(item);
            }
        }

        private void InitValue()
        {
            var element = mCellElement;
            if (element == null) { return; }
            ObjectPropertyInfoItem item;


            #region Sequence

            var sequenceElement = element as SequenceElement;
            if (sequenceElement != null)
            {
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_SEQUENCE_DATASET);
                if (item != null)
                {
                    var dataSet = sequenceElement.DataSet;
                    if (dataSet != null)
                    {
                        item.Value = dataSet.Name;
                    }
                }
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_SEQUENCE_DATAFIELD);
                if (item != null)
                {
                    var field = sequenceElement.DataField;
                    if (field != null)
                    {
                        item.Value = field.Name;
                    }
                }
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_SEQUENCE_OPT_METHOD);
                if (item != null)
                {
                    item.Value = sequenceElement.DataOptMethod.ToString();
                }
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_SEQUENCE_GROUP_MODE);
                if (item != null)
                {
                    item.Value = sequenceElement.GroupMode.ToString();
                }
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_SEQUENCE_COLLECT_MODE);
                if (item != null)
                {
                    item.Value = sequenceElement.CollectMode.ToString();
                }
            }

            #endregion


            #region Image

            var imageElement = element as ImageElement;
            if (imageElement != null)
            {
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_IMAGE_WIDTH);
                if (item != null)
                {
                    item.Value = imageElement.ImageWidth.ToString();
                }
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_IMAGE_HEIGHT);
                if (item != null)
                {
                    item.Value = imageElement.ImageHeight.ToString();
                }
                item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_IMAGE_STRETCH);
                if (item != null)
                {
                    item.Value = ((int)imageElement.Stretch).ToString();
                }
            }

            #endregion

        }

        #endregion


        #region Others

        private string GetGroupName(int groupID)
        {
            string str = string.Empty;
            switch (groupID)
            {
                case ReportPropertyFactory.GP_BASIC:
                    str = "基本";
                    break;
                case ReportPropertyFactory.GP_FONT:
                    str = "外观";
                    break;
                case ReportPropertyFactory.GP_LAYOUT:
                    str = "布局";
                    break;
                case ReportPropertyFactory.GP_BORDER:
                    str = "边框";
                    break;
                case ReportPropertyFactory.GP_LINK:
                    str = "超链接";
                    break;
                case ReportPropertyFactory.GP_EXTENSION:
                    str = "扩展性";
                    break;
                case ReportPropertyFactory.GP_CELL_PARENT:
                    str = "父格";
                    break;
                case ReportPropertyFactory.GP_FORMAT:
                    str = "格式";
                    break;
                case ReportPropertyFactory.GP_SEQUENCE_DATA_OPERATION:
                    str = "数据操作";
                    break;
                case ReportPropertyFactory.GP_IMAGE_STRETCH:
                    str = "图片拉伸";
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
            var textElement = mCellElement as TextElement;
            if (textElement != null)
            {
                textElement.Text = TxtElementText.Text;
            }
            var imageElement = mCellElement as ImageElement;
            if (imageElement != null)
            {
                imageElement.Text = TxtElementText.Text;
            }
            if (Panel != null)
            {
                Panel.IsModified = true;
            }
        }

        private void Editor_PropertyValueChanged(object sender,
            RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            var item = args.PropertyItem;
            if (item == null) { return; }
            if (mCellElement == null) { return; }
            int id = item.ID;
            string strValue = args.Value;


            #region Sequence

            var sequenceElement = mCellElement as SequenceElement;
            if (sequenceElement != null)
            {
                if (id == ReportPropertyFactory.PRO_SEQUENCE_DATASET)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        var dataSet = valueItem.Info as ReportDataSet;
                        sequenceElement.DataSet = dataSet;
                        if (dataSet != null)
                        {
                            //刷新数据列
                            var fieldItem =
                                mListPropertyItems.FirstOrDefault(i => i.ID == ReportPropertyFactory.PRO_SEQUENCE_DATAFIELD);
                            if (fieldItem != null)
                            {
                                var editor = fieldItem.Editor;
                                if (editor != null)
                                {
                                    editor.Reload();
                                }
                            }
                        }
                    }
                }
                if (id == ReportPropertyFactory.PRO_SEQUENCE_DATAFIELD)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        var dataField = valueItem.Info as ReportDataField;
                        sequenceElement.DataField = dataField;
                        if (dataField != null)
                        {
                            var dataSet = sequenceElement.DataSet;
                            if (dataSet != null)
                            {
                                StrElementText = sequenceElement.Text = string.Format("={{{0}.{1}}}", dataSet.Name, dataField.Name);
                            }
                        }
                    }
                }
                if (id == ReportPropertyFactory.PRO_SEQUENCE_OPT_METHOD)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        int intValue;
                        if (int.TryParse(valueItem.Value, out intValue))
                        {
                            sequenceElement.DataOptMethod = intValue;
                        }
                    }
                }
                if (id == ReportPropertyFactory.PRO_SEQUENCE_GROUP_MODE)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        int intValue;
                        if (int.TryParse(valueItem.Value, out intValue))
                        {
                            sequenceElement.GroupMode = intValue;
                        }
                    }
                }
                if (id == ReportPropertyFactory.PRO_SEQUENCE_COLLECT_MODE)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        int intValue;
                        if (int.TryParse(valueItem.Value, out intValue))
                        {
                            sequenceElement.CollectMode = intValue;
                        }
                    }
                }
            }

            #endregion


            #region Image

            var imageElement = mCellElement as ImageElement;
            if (imageElement != null)
            {
                if (id == ReportPropertyFactory.PRO_IMAGE_WIDTH)
                {
                    int intValue;
                    if (int.TryParse(strValue, out intValue)
                        && intValue > 0
                        && intValue < 1000)
                    {
                        imageElement.ImageWidth = intValue;
                    }
                }
                if (id == ReportPropertyFactory.PRO_IMAGE_HEIGHT)
                {
                    int intValue;
                    if (int.TryParse(strValue, out intValue)
                        && intValue > 0
                        && intValue < 1000)
                    {
                        imageElement.ImageHeight = intValue;
                    }
                }
                if (id == ReportPropertyFactory.PRO_IMAGE_STRETCH)
                {
                    var valueItem = args.ValueItem;
                    if (valueItem != null)
                    {
                        int intValue;
                        if (int.TryParse(valueItem.Value, out intValue)
                            && intValue >= 0
                            && intValue <= 3)
                        {
                            imageElement.Stretch = (Stretch)intValue;
                        }
                    }
                }
            }

            #endregion

        }

        #endregion

    }
}
