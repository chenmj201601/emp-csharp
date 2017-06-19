//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    aef68976-be0a-477f-b2e7-833a0ace6964
//        CLR Version:              4.0.30319.42000
//        Name:                     UCObjectPropertyEditor
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCObjectPropertyEditor
//
//        Created by Charley at 2017/4/28 15:16:49
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using NetInfo.Wpf.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCObjectPropertyEditor.xaml 的交互逻辑
    /// </summary>
    public partial class UCObjectPropertyEditor
    {

        #region Members

        private bool mIsInited;
        private ObjectPropertyInfo mPropertyInfo;
        private object mObjectInstance;
        private ReportDesignPanel mPanel;

        private ObservableCollection<PropertyValueEnumItem> mListEnumValueItems =
            new ObservableCollection<PropertyValueEnumItem>();

        #endregion


        static UCObjectPropertyEditor()
        {
            PropertyValueChangedEvent = EventManager.RegisterRoutedEvent("PropertyValueChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<PropertyValueChangedEventArgs>),
                typeof(UCObjectPropertyEditor));
        }

        public UCObjectPropertyEditor()
        {
            InitializeComponent();

            Loaded += UCObjectPropertyEditor_Loaded;
        }

        void UCObjectPropertyEditor_Loaded(object sender, RoutedEventArgs e)
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
            if (PropertyInfoItem == null) { return; }
            PropertyInfoItem.Editor = this;
            mPropertyInfo = PropertyInfoItem.Info;
            mObjectInstance = PropertyInfoItem.ObjectInstance;
            mPanel = PropertyInfoItem.Panel;
            if (mPropertyInfo != null)
            {
                EditFormat = mPropertyInfo.EditFormat;
            }
            Value = PropertyInfoItem.Value;
            BoolValue = Value == "1";
            InitEnumItems();
            InitValue();
        }

        private void InitEnumItems()
        {
            mListEnumValueItems.Clear();
            switch (EditFormat)
            {
                case PropertyEditFormat.SingleSelect:
                    InitEnumSingleSelectItems();
                    break;
            }
        }

        private void InitEnumSingleSelectItems()
        {
            if (mPropertyInfo == null) { return; }
            int id = mPropertyInfo.ID;
            switch (id)
            {
                case ReportPropertyFactory.PRO_FONTFAMILY:
                    InitEnumFontFamilies();
                    break;
                case ReportPropertyFactory.PRO_HALIGN:
                    InitEnumHAligns();
                    break;
                case ReportPropertyFactory.PRO_VALIGN:
                    InitEnumVAligns();
                    break;
                case ReportPropertyFactory.PRO_EXT_DIRECTION:
                    InitEnumExtDirections();
                    break;
                case ReportPropertyFactory.PRO_FORMAT_TYPE:
                    InitEnumFormatTypes();
                    break;
                case ReportPropertyFactory.PRO_SEQUENCE_DATASET:
                    InitEnumDataSets();
                    break;
                case ReportPropertyFactory.PRO_SEQUENCE_DATAFIELD:
                    InitEnumDataFields();
                    break;
                case ReportPropertyFactory.PRO_SEQUENCE_OPT_METHOD:
                    InitEnumDataOptMethods();
                    break;
                case ReportPropertyFactory.PRO_SEQUENCE_GROUP_MODE:
                    InitEnumGroupModes();
                    break;
                case ReportPropertyFactory.PRO_SEQUENCE_COLLECT_MODE:
                    InitEnumCollectModes();
                    break;
                case ReportPropertyFactory.PRO_IMAGE_STRETCH:
                    InitEnumStretchModes();
                    break;
            }
        }

        private void InitEnumFontFamilies()
        {
            var fonts = Fonts.SystemFontFamilies;
            foreach (var font in fonts)
            {
                PropertyValueEnumItem item = new PropertyValueEnumItem();
                item.Info = font;
                item.Display = font.ToString();
                item.Description = item.Display;
                item.Value = item.Display;
                mListEnumValueItems.Add(item);
            }
        }

        private void InitEnumHAligns()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ((int)HorizontalAlignment.Left).ToString();
            item.Display = "左对齐";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)HorizontalAlignment.Center).ToString();
            item.Display = "居中对齐";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)HorizontalAlignment.Right).ToString();
            item.Display = "右对齐";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)HorizontalAlignment.Stretch).ToString();
            item.Display = "拉伸";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumVAligns()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ((int)VerticalAlignment.Top).ToString();
            item.Display = "顶对齐";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)VerticalAlignment.Center).ToString();
            item.Display = "中间对齐";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)VerticalAlignment.Bottom).ToString();
            item.Display = "底对齐";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)VerticalAlignment.Stretch).ToString();
            item.Display = "拉伸";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumExtDirections()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ((int)CellExtDirection.None).ToString();
            item.Display = "不扩展";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)CellExtDirection.Vertical).ToString();
            item.Display = "纵向扩展";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)CellExtDirection.Horizontal).ToString();
            item.Display = "横向扩展";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumFormatTypes()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ((int)CellFormatType.None).ToString();
            item.Display = "常规";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)CellFormatType.Numeric).ToString();
            item.Display = "数值";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)CellFormatType.Text).ToString();
            item.Display = "文本";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumDataSets()
        {
            if (mPanel == null) { return; }
            var document = mPanel.Document;
            if (document == null) { return; }
            var dataSets = document.DataSets;
            for (int i = 0; i < dataSets.Count; i++)
            {
                var dataSet = dataSets[i];
                PropertyValueEnumItem item = new PropertyValueEnumItem();
                item.Info = dataSet;
                item.Value = dataSet.Name;
                item.Display = item.Value;
                item.Description = item.Display;
                mListEnumValueItems.Add(item);
            }
        }

        private void InitEnumDataFields()
        {
            if (mObjectInstance == null) { return; }
            var sequenceElement = mObjectInstance as SequenceElement;
            if (sequenceElement == null) { return; }
            var dataSet = sequenceElement.DataSet;
            if (dataSet == null) { return; }
            var fields = dataSet.Fields;
            for (int i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                PropertyValueEnumItem item = new PropertyValueEnumItem();
                item.Info = field;
                string strName = field.Name;
                item.Value = strName;
                item.Display = item.Value;
                item.Description = item.Display;
                mListEnumValueItems.Add(item);
            }
        }

        private void InitEnumDataOptMethods()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ((int)DataOperationMethod.Group).ToString();
            item.Display = "分组";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)DataOperationMethod.List).ToString();
            item.Display = "列表";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)DataOperationMethod.Collect).ToString();
            item.Display = "汇总";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumGroupModes()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ReportSequence.GROUP_MODE_TRADITIONAL.ToString();
            item.Display = "普通";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ReportSequence.GROUP_MODE_ADJACENT_CONTINUE.ToString();
            item.Display = "相邻连续";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumCollectModes()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ReportSequence.COLLECT_MODE_SUM.ToString();
            item.Display = "求和";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ReportSequence.COLLECT_MODE_AVG.ToString();
            item.Display = "平均值";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ReportSequence.COLLECT_MODE_MAX.ToString();
            item.Display = "最大值";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ReportSequence.COLLECT_MODE_MIN.ToString();
            item.Display = "最小值";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitEnumStretchModes()
        {
            PropertyValueEnumItem item = new PropertyValueEnumItem();
            item.Value = ((int)Stretch.None).ToString();
            item.Display = "无";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)Stretch.Fill).ToString();
            item.Display = "铺满";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)Stretch.Uniform).ToString();
            item.Display = "保持宽高比";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
            item = new PropertyValueEnumItem();
            item.Value = ((int)Stretch.UniformToFill).ToString();
            item.Display = "保持宽高比铺满";
            item.Description = item.Display;
            mListEnumValueItems.Add(item);
        }

        private void InitValue()
        {
            switch (EditFormat)
            {
                case PropertyEditFormat.SingleSelect:
                    var item = mListEnumValueItems.FirstOrDefault(i => i.Value == Value);
                    if (mItemsSelectControlValue != null)
                    {
                        mItemsSelectControlValue.Tag = true;//禁止触发SelectionChanged事件
                        mItemsSelectControlValue.SelectedItem = item;
                        mItemsSelectControlValue.Tag = false;
                    }
                    break;
                case PropertyEditFormat.ColorSelect:
                    string strValue = Value;
                    if (!string.IsNullOrEmpty(strValue))
                    {
                        var color = ColorConverter.ConvertFromString(strValue);
                        if (color != null)
                        {
                            ColorValue = (Color)color;
                        }
                    }
                    break;
            }
        }

        #endregion


        #region Templates

        private const string PART_Panel = "PART_Panel";
        private const string PART_TextBlock = "PART_TextBlock";
        private const string PART_TextBox = "PART_TextBox";
        private const string PART_IntTextBox = "PART_IntTextBox";
        private const string PART_ItemsSelectControl = "PART_ItemsSelectControl";
        private const string PART_ColorTextBox = "PART_ColorTextBox";
        private const string PART_CheckBox = "PART_CheckBox";

        private Border mBorderPanel;
        private TextBlock mTextBlockValue;
        private AutoSelectTextBox mTextBoxValue;
        private IntegerUpDown mIntTextBoxValue;
        private Selector mItemsSelectControlValue;
        private ColorPicker mColorTextBox;
        private CheckBox mCheckBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mBorderPanel = GetTemplateChild(PART_Panel) as Border;
            if (mBorderPanel != null)
            {
                //var editor = mBorderPanel.Child as IResourcePropertyEditor;
                //if (editor != null)
                //{
                //    editor.PropertyValueChanged += Editor_PropertyValueChanged;
                //}
            }
            mTextBlockValue = GetTemplateChild(PART_TextBlock) as TextBlock;
            if (mTextBlockValue != null)
            {

            }
            mTextBoxValue = GetTemplateChild(PART_TextBox) as AutoSelectTextBox;
            if (mTextBoxValue != null)
            {
                mTextBoxValue.TextChanged += TextBoxValue_TextChanged;
            }
            mIntTextBoxValue = GetTemplateChild(PART_IntTextBox) as IntegerUpDown;
            if (mIntTextBoxValue != null)
            {
                mIntTextBoxValue.ValueChanged += IntTextBoxValue_ValueChanged;
            }
            mItemsSelectControlValue = GetTemplateChild(PART_ItemsSelectControl) as Selector;
            if (mItemsSelectControlValue != null)
            {
                mItemsSelectControlValue.SelectionChanged += ItemsSelectControlValue_SelectionChanged;
                //var combo = mItemsSelectControlValue as ComboBox;
                //if (combo != null)
                //{
                //    combo.DropDownOpened += ComboBox_DropDownOpened;
                //    combo.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(ComboBox_TextChanged));
                //}
                mItemsSelectControlValue.ItemsSource = mListEnumValueItems;
                InitValue();
            }
            mColorTextBox = GetTemplateChild(PART_ColorTextBox) as ColorPicker;
            if (mColorTextBox != null)
            {
                mColorTextBox.SelectedColorChanged += ColorTextBox_SelectedColorChanged;
            }
            mCheckBox = GetTemplateChild(PART_CheckBox) as CheckBox;
            if (mCheckBox != null)
            {
                mCheckBox.Click += CheckBox_Click;
            }
        }

        #endregion


        #region Event Handlers

        void TextBoxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PropertyInfoItem == null) { return; }
            if (mTextBoxValue == null) { return; }
            string strValue = mTextBoxValue.Text;
            PropertyInfoItem.Value = strValue;
            PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs();
            args.PropertyItem = PropertyInfoItem;
            args.ObjectInstance = mObjectInstance;
            args.Value = strValue;
            OnPropertyValueChanged(this, args);
        }

        void IntTextBoxValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (PropertyInfoItem == null) { return; }
            if (mIntTextBoxValue == null) { return; }
            var value = mIntTextBoxValue.Value;
            if (value == null) { return; }
            int intValue = (int)value;
            string strValue = intValue.ToString();
            PropertyInfoItem.Value = strValue;
            PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs();
            args.PropertyItem = PropertyInfoItem;
            args.ObjectInstance = mObjectInstance;
            args.Value = strValue;
            OnPropertyValueChanged(this, args);
        }

        void ItemsSelectControlValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mItemsSelectControlValue == null) { return; }
            var tag = mItemsSelectControlValue.Tag;
            if (tag != null && (bool)tag) { return; }
            if (PropertyInfoItem == null) { return; }
            var item = mItemsSelectControlValue.SelectedItem as PropertyValueEnumItem;
            if (item == null) { return; }
            string strValue = item.Value;
            PropertyInfoItem.Value = strValue;
            PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs();
            args.PropertyItem = PropertyInfoItem;
            args.ObjectInstance = mObjectInstance;
            args.Value = strValue;
            args.ValueItem = item;
            OnPropertyValueChanged(this, args);
        }

        void ColorTextBox_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            var color = e.NewValue;
            if (PropertyInfoItem == null) { return; }
            string strValue = color.ToString();
            PropertyInfoItem.Value = strValue;
            PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs();
            args.PropertyItem = PropertyInfoItem;
            args.ObjectInstance = mObjectInstance;
            args.Value = strValue;
            OnPropertyValueChanged(this, args);
        }

        void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyInfoItem == null) { return; }
            if (mCheckBox == null) { return; }
            bool isChecked = mCheckBox.IsChecked == true;
            string strValue = isChecked ? "1" : "0";
            PropertyInfoItem.Value = strValue;
            PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs();
            args.PropertyItem = PropertyInfoItem;
            args.ObjectInstance = mObjectInstance;
            args.Value = strValue;
            OnPropertyValueChanged(this, args);
        }

        #endregion


        #region Reload

        public void Reload()
        {
            if (PropertyInfoItem == null) { return; }
            Value = PropertyInfoItem.Value;
            BoolValue = Value == "1";
            InitEnumItems();
            InitValue();
        }

        #endregion


        #region PropertyInfoItemProperty

        public static readonly DependencyProperty PropertyInfoItemProperty =
            DependencyProperty.Register("PropertyInfoItem", typeof(ObjectPropertyInfoItem), typeof(UCObjectPropertyEditor), new PropertyMetadata(default(ObjectPropertyInfoItem)));

        public ObjectPropertyInfoItem PropertyInfoItem
        {
            get { return (ObjectPropertyInfoItem)GetValue(PropertyInfoItemProperty); }
            set { SetValue(PropertyInfoItemProperty, value); }
        }

        #endregion


        #region ValueProperty

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(UCObjectPropertyEditor), new PropertyMetadata(default(string)));

        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        #endregion


        #region BoolValueProperty

        public static readonly DependencyProperty BoolValueProperty =
            DependencyProperty.Register("BoolValue", typeof(bool), typeof(UCObjectPropertyEditor), new PropertyMetadata(default(bool)));

        public bool BoolValue
        {
            get { return (bool)GetValue(BoolValueProperty); }
            set { SetValue(BoolValueProperty, value); }
        }

        #endregion


        #region ColorValueProperty

        public static readonly DependencyProperty ColorValueProperty =
            DependencyProperty.Register("ColorValue", typeof(Color), typeof(UCObjectPropertyEditor), new PropertyMetadata(default(Color)));

        public Color ColorValue
        {
            get { return (Color)GetValue(ColorValueProperty); }
            set { SetValue(ColorValueProperty, value); }
        }

        #endregion


        #region EditFormatProperty

        public static readonly DependencyProperty EditFormatProperty =
            DependencyProperty.Register("EditFormat", typeof(PropertyEditFormat), typeof(UCObjectPropertyEditor), new PropertyMetadata(default(PropertyEditFormat)));

        public PropertyEditFormat EditFormat
        {
            get { return (PropertyEditFormat)GetValue(EditFormatProperty); }
            set { SetValue(EditFormatProperty, value); }
        }

        #endregion


        #region PropertyValueChangedEvent

        public static readonly RoutedEvent PropertyValueChangedEvent;

        public event RoutedPropertyChangedEventHandler<PropertyValueChangedEventArgs> PropertyValueChanged
        {
            add { AddHandler(PropertyValueChangedEvent, value); }
            remove { RemoveHandler(PropertyValueChangedEvent, value); }
        }

        private void OnPropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            var editor = sender as UCObjectPropertyEditor;
            if (editor != null)
            {
                RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs> args =
                    new RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs>(null, e);
                args.RoutedEvent = PropertyValueChangedEvent;
                editor.RaiseEvent(args);
            }
        }

        #endregion

    }
}
