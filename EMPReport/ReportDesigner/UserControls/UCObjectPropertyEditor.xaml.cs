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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NetInfo.Wpf.Controls;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCObjectPropertyEditor.xaml 的交互逻辑
    /// </summary>
    public partial class UCObjectPropertyEditor
    {

        private bool mIsInited;
        private ObjectPropertyInfo mPropertyInfo;

        private ObservableCollection<PropertyValueEnumItem> mListEnumValueItems =
            new ObservableCollection<PropertyValueEnumItem>();


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
            mPropertyInfo = PropertyInfoItem.Info;
            if (mPropertyInfo != null)
            {
                EditFormat = mPropertyInfo.EditFormat;
            }
            Value = PropertyInfoItem.Value;
            InitEnumItems();
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
                case TextElementPropertyFactory.PRO_FONTFAMILY:
                    InitEnumFontFamilies();
                    break;
                case TextElementPropertyFactory.PRO_FONTSTYLE:
                    InitEnumFontStyles();
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

        private void InitEnumFontStyles()
        {
            var styles = Enum.GetValues(typeof (NetInfo.EMP.Reports.FontStyle));
            foreach (var style in styles)
            {
                PropertyValueEnumItem item = new PropertyValueEnumItem();
                item.Info = style;
                item.Display = style.ToString();
                item.Description = item.Display;
                item.Value = item.Display;
                mListEnumValueItems.Add(item);
            }
        }

        #endregion


        #region Templates

        private const string PART_Panel = "PART_Panel";
        private const string PART_TextBlock = "PART_TextBlock";
        private const string PART_TextBox = "PART_TextBox";
        private const string PART_IntTextBox = "PART_IntTextBox";
        private const string PART_ItemsSelectControl = "PART_ItemsSelectControl";

        private Border mBorderPanel;
        private TextBlock mTextBlockValue;
        private AutoSelectTextBox mTextBoxValue;
        private IntegerUpDown mIntTextBoxValue;
        private Selector mItemsSelectControlValue;

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
                mTextBoxValue.TextChanged += mTextBoxValue_TextChanged;
            }
            mIntTextBoxValue = GetTemplateChild(PART_IntTextBox) as IntegerUpDown;
            if (mIntTextBoxValue != null)
            {
                mIntTextBoxValue.ValueChanged += mIntTextBoxValue_ValueChanged;
            }
            mItemsSelectControlValue = GetTemplateChild(PART_ItemsSelectControl) as Selector;
            if (mItemsSelectControlValue != null)
            {
                mItemsSelectControlValue.SelectionChanged += mItemsSelectControlValue_SelectionChanged;
                //var combo = mItemsSelectControlValue as ComboBox;
                //if (combo != null)
                //{
                //    combo.DropDownOpened += ComboBox_DropDownOpened;
                //    combo.AddHandler(TextBox.TextChangedEvent, new TextChangedEventHandler(ComboBox_TextChanged));
                //}
                mItemsSelectControlValue.ItemsSource = mListEnumValueItems;
            }
        }

        #endregion


        #region Event Handlers

        void mTextBoxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (mPropertyValue != null
            //    && mTextBoxValue != null)
            //{
            //    mPropertyValue.Value = mTextBoxValue.Text;

            //    RaisePropertyValueChangedEvent();
            //}
        }

        void mIntTextBoxValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //if (mPropertyValue != null
            //    && mIntTextBoxValue != null)
            //{
            //    string value = mIntTextBoxValue.Value == null ? string.Empty : mIntTextBoxValue.Value.ToString();
            //    switch (mPropertyValue.ObjType)
            //    {
            //        case S1110Consts.RESOURCE_ALARMSERVERPARAM:
            //            switch (mPropertyValue.PropertyID)
            //            {
            //                case 23:
            //                case 33:
            //                    int intValue;
            //                    if (int.TryParse(value, out intValue))
            //                    {
            //                        value = (intValue * 3600).ToString(); ;
            //                    }
            //                    break;
            //            }
            //            break;
            //    }
            //    mPropertyValue.Value = value;

            //    RaisePropertyValueChangedEvent();
            //}
        }
        void mItemsSelectControlValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bool isInit = e.RemovedItems.Count == 0;        //此处判断是否第一次触发事件不太合理
            //if (mItemsSelectControlValue != null)
            //{
            //    var item = mItemsSelectControlValue.SelectedItem as PropertyValueEnumItem;
            //    if (item != null)
            //    {
            //        mPropertyValue.Value = item.Value;
            //    }
            //    else
            //    {
            //        mPropertyValue.Value = string.Empty;
            //    }
            //    if (mConfigObject != null)
            //    {
            //        mConfigObject.SetPropertyValue(mPropertyValue.PropertyID, mPropertyValue.Value);
            //    }
            //    PropertyValueChangedEventArgs args = new PropertyValueChangedEventArgs();
            //    args.ConfigObject = mConfigObject;
            //    args.PropertyItem = PropertyInfoItem;
            //    args.PropertyInfo = mPropertyInfo;
            //    args.PropetyValue = mPropertyValue;
            //    args.Value = mPropertyValue.Value;
            //    args.ValueItem = item;
            //    args.IsInit = isInit;
            //    OnPropertyValueChanged(args);
            //}
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


        #region EditFormatProperty

        public static readonly DependencyProperty EditFormatProperty =
            DependencyProperty.Register("EditFormat", typeof(PropertyEditFormat), typeof(UCObjectPropertyEditor), new PropertyMetadata(default(PropertyEditFormat)));

        public PropertyEditFormat EditFormat
        {
            get { return (PropertyEditFormat)GetValue(EditFormatProperty); }
            set { SetValue(EditFormatProperty, value); }
        }

        #endregion

    }
}
