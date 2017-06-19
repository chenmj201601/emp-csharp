//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    945278bf-e9d9-44a4-9e5e-f2ca7d2fca5e
//        CLR Version:              4.0.30319.42000
//        Name:                     UCCellPropertyLister
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCCellPropertyLister
//
//        Created by Charley at 2017/6/2 14:58:54
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using NetInfo.EMP.Reports.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCCellPropertyLister.xaml 的交互逻辑
    /// </summary>
    public partial class UCCellPropertyLister
    {

        #region Members

        public ReportDesignPanel Panel;

        private bool mIsInited;
        private GridCell mCell;
        private ObservableCollection<ObjectPropertyInfoItem> mListPropertyItems =
          new ObservableCollection<ObjectPropertyInfoItem>();

        #endregion


        public UCCellPropertyLister()
        {
            InitializeComponent();

            Loaded += UCCellPropertyLister_Loaded;
            AddHandler(UCObjectPropertyEditor.PropertyValueChangedEvent,
               new RoutedPropertyChangedEventHandler<PropertyValueChangedEventArgs>(Editor_PropertyValueChanged));
        }

        void UCCellPropertyLister_Loaded(object sender, RoutedEventArgs e)
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
            mCell = DataContext as GridCell;
            if (mCell == null) { return; }
            CellName = mCell.CellName;
        }

        private void InitPropertyItems()
        {
            mListPropertyItems.Clear();
            if (mCell == null) { return; }
            var properties = ReportPropertyFactory.GetCellProperties();
            for (int i = 0; i < properties.Count; i++)
            {
                var info = properties[i];
                ObjectPropertyInfoItem item = new ObjectPropertyInfoItem();
                item.Info = info;
                item.ID = info.ID;
                item.PropertyName = info.Name;
                item.GroupName = GetGroupName(info.GroupID);
                item.Value = info.DefaultValue;
                item.ObjectInstance = mCell;
                item.Panel = Panel;
                mListPropertyItems.Add(item);
            }
        }

        private void InitValue()
        {
            var cell = mCell;
            if (cell == null) { return; }
            ObjectPropertyInfoItem item;
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FONTFAMILY);
            if (item != null)
            {
                item.Value = cell.FontFamily.ToString();
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FONTSIZE);
            if (item != null)
            {
                item.Value = ((int)cell.FontSize).ToString();
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FONTSTYLE_BOLD);
            if (item != null)
            {
                item.Value = cell.FontWeight == FontWeights.Bold ? "1" : "0";
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FONTSTYLE_ITALIC);
            if (item != null)
            {
                item.Value = cell.FontStyle == FontStyles.Italic ? "1" : "0";
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FONTSTYLE_UNDERLINE);
            if (item != null)
            {
                item.Value = Equals(cell.TextDecration, TextDecorations.Underline) ? "1" : "0";
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FORECOLOR);
            if (item != null)
            {
                var brush = cell.Foreground as SolidColorBrush;
                if (brush != null)
                {
                    item.Value = brush.Color.ToString();
                }
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_BACKCOLOR);
            if (item != null)
            {
                var brush = cell.Background as SolidColorBrush;
                if (brush != null)
                {
                    item.Value = brush.Color.ToString();
                }
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_HALIGN);
            if (item != null)
            {
                item.Value = ((int)cell.HAlign).ToString();
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_VALIGN);
            if (item != null)
            {
                item.Value = ((int)cell.VAlign).ToString();
            }

            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_LINK_URL);
            if (item != null)
            {
                item.Value = cell.LinkUrl;
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_EXT_DIRECTION);
            if (item != null)
            {
                item.Value = cell.ExtDirection.ToString();
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_CELL_PARENT_LEFT);
            if (item != null)
            {
                item.Value = cell.LeftParent;
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_CELL_PARENT_TOP);
            if (item != null)
            {
                item.Value = cell.TopParent;
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FORMAT_TYPE);
            if (item != null)
            {
                item.Value = cell.FormatType.ToString();
            }
            item = mListPropertyItems.FirstOrDefault(p => p.ID == ReportPropertyFactory.PRO_FORMAT_EXPRESSION);
            if (item != null)
            {
                item.Value = cell.FormatString;
            }
        }

        #endregion


        #region NameWidthProperty

        public static readonly DependencyProperty NameWidthProperty =
            DependencyProperty.Register("NameWidth", typeof(double), typeof(UCCellPropertyLister), new PropertyMetadata(default(double)));

        public double NameWidth
        {
            get { return (double)GetValue(NameWidthProperty); }
            set { SetValue(NameWidthProperty, value); }
        }

        #endregion


        #region CellNameProperty

        public static readonly DependencyProperty CellNameProperty =
            DependencyProperty.Register("CellName", typeof(string), typeof(UCCellPropertyLister), new PropertyMetadata(default(string)));

        public string CellName
        {
            get { return (string)GetValue(CellNameProperty); }
            set { SetValue(CellNameProperty, value); }
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

        private void Editor_PropertyValueChanged(object sender,
         RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            var item = args.PropertyItem;
            if (item == null) { return; }
            var cell = mCell;
            if (cell == null) { return; }
            int id = item.ID;
            string strValue = args.Value;
            if (id == ReportPropertyFactory.PRO_FONTFAMILY)
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
            if (id == ReportPropertyFactory.PRO_FONTSIZE)
            {
                int intValue;
                if (int.TryParse(strValue, out intValue))
                {
                    if (intValue > 0)
                    {
                        cell.FontSize = intValue;
                    }
                }
            }
            if (id == ReportPropertyFactory.PRO_FONTSTYLE_BOLD)
            {
                cell.FontWeight = strValue == "1" ? FontWeights.Bold : FontWeights.Normal;
            }
            if (id == ReportPropertyFactory.PRO_FONTSTYLE_ITALIC)
            {
                cell.FontStyle = strValue == "1" ? FontStyles.Italic : FontStyles.Normal;
            }
            if (id == ReportPropertyFactory.PRO_FONTSTYLE_UNDERLINE)
            {
                cell.TextDecration = strValue == "1" ? TextDecorations.Underline : null;
            }
            if (id == ReportPropertyFactory.PRO_FORECOLOR)
            {
                var color = ColorConverter.ConvertFromString(strValue);
                if (color != null)
                {
                    cell.Foreground = new SolidColorBrush((Color)color);
                }
            }
            if (id == ReportPropertyFactory.PRO_BACKCOLOR)
            {
                var color = ColorConverter.ConvertFromString(strValue);
                if (color != null)
                {
                    cell.Background = new SolidColorBrush((Color)color);
                }
            }
            if (id == ReportPropertyFactory.PRO_HALIGN)
            {
                var valueItem = args.ValueItem;
                if (valueItem != null)
                {
                    string str = valueItem.Value;
                    int intValue;
                    if (int.TryParse(str, out intValue)
                        && intValue >= 0
                        && intValue <= 3)
                    {
                        cell.HAlign = (HorizontalAlignment)intValue;
                    }
                }
            }
            if (id == ReportPropertyFactory.PRO_VALIGN)
            {
                var valueItem = args.ValueItem;
                if (valueItem != null)
                {
                    string str = valueItem.Value;
                    int intValue;
                    if (int.TryParse(str, out intValue)
                        && intValue >= 0
                        && intValue <= 3)
                    {
                        cell.VAlign = (VerticalAlignment)intValue;
                    }
                }
            }
            if (id == ReportPropertyFactory.PRO_LINK_URL)
            {
                cell.LinkUrl = strValue;
            }
            if (id == ReportPropertyFactory.PRO_EXT_DIRECTION)
            {
                var valueItem = args.ValueItem;
                if (valueItem != null)
                {
                    string str = valueItem.Value;
                    int intValue;
                    if (int.TryParse(str, out intValue))
                    {
                        cell.ExtDirection = intValue;
                    }
                }
            }
            if (id == ReportPropertyFactory.PRO_CELL_PARENT_LEFT)
            {
                cell.LeftParent = strValue;
            }
            if (id == ReportPropertyFactory.PRO_CELL_PARENT_TOP)
            {
                cell.TopParent = strValue;
            }
            if (id == ReportPropertyFactory.PRO_FORMAT_TYPE)
            {
                var valueItem = args.ValueItem;
                if (valueItem != null)
                {
                    string str = valueItem.Value;
                    int intValue;
                    if (int.TryParse(str, out intValue))
                    {
                        cell.FormatType = intValue;
                    }
                }
            }
            if (id == ReportPropertyFactory.PRO_FORMAT_EXPRESSION)
            {
                cell.FormatString = strValue;
            }
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

    }
}
