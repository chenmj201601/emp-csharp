//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    02acb77b-5ebc-43d6-a802-999add064dcf
//        CLR Version:              4.0.30319.42000
//        Name:                     UCComponentBox
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCComponentBox
//
//        Created by Charley at 2017/5/12 12:04:44
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NetInfo.Common;
using NetInfo.Wpf.Controls;
using ReportDesigner.Commands;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCComponentBox.xaml 的交互逻辑
    /// </summary>
    public partial class UCComponentBox
    {

        #region Members

        public MainWindow PageParent;
        public DesignerConfig DesignerConfig;

        private bool mIsInited;
        private ObservableCollection<ComponentItem> mListBasicComponentItems = new ObservableCollection<ComponentItem>();
        private ObservableCollection<ComponentItem> mListShapComponentItems = new ObservableCollection<ComponentItem>();
        private ObservableCollection<ComponentItem> mListChartComponentItems = new ObservableCollection<ComponentItem>();
        private ObservableCollection<ComponentItem> mListOtherComponentItems = new ObservableCollection<ComponentItem>();

        #endregion


        public UCComponentBox()
        {
            InitializeComponent();

            Loaded += UCComponentBox_Loaded;
            AddHandler(ListItemPanel.ListItemEventEvent, new RoutedPropertyChangedEventHandler<ListItemEventArgs>(ListItemPanel_ListItemEvent));
        }

        void UCComponentBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        public IList<ComponentItem> GetAllComponentItems()
        {
            IList<ComponentItem> items = new List<ComponentItem>();
            foreach (var item in mListBasicComponentItems)
            {
                items.Add(item);
            }
            foreach (var item in mListShapComponentItems)
            {
                items.Add(item);
            }
            foreach (var item in mListChartComponentItems)
            {
                items.Add(item);
            }
            return items;
        }

        public void AddComponent(ComponentItem item)
        {
            if (item == null) { return; }
            var info = item.Info;
            if (info == null) { return; }
            int groupID = info.GroupID;
            if (groupID == ComponentInfo.GP_BASIC)
            {
                mListBasicComponentItems.Add(item);
            }
            if (groupID == ComponentInfo.GP_SHAP)
            {
                mListShapComponentItems.Add(item);
            }
            if (groupID == ComponentInfo.GP_CHART)
            {
                mListChartComponentItems.Add(item);
            }
            if (groupID == ComponentInfo.GP_OTHER)
            {
                mListOtherComponentItems.Add(item);
            }

            SaveComponentConfig();
        }


        #region Init and Load

        private void Init()
        {
            ListBoxBasicComponent.ItemsSource = mListBasicComponentItems;
            ListBoxShapComponent.ItemsSource = mListShapComponentItems;
            ListBoxChartComponent.ItemsSource = mListChartComponentItems;
            ListBoxOtherComponent.ItemsSource = mListOtherComponentItems;
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.ComponentDeleteCommand,
                ComponentDelete_Executed, ComponentDelete_CanExecute));
            InitComponentItems();
        }

        private void InitComponentItems()
        {
            mListBasicComponentItems.Clear();
            mListShapComponentItems.Clear();
            mListChartComponentItems.Clear();

            try
            {
                IList<ComponentInfo> allComponentInfos = ComponentInfo.InitComponentInfos();
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ComponentConfig.FILE_NAME);
                if (File.Exists(path))
                {
                    OperationReturn optReturn = XMLHelper.DeserializeFile<ComponentConfig>(path);
                    if (optReturn.Result)
                    {
                        var config = optReturn.Data as ComponentConfig;
                        if (config != null)
                        {
                            for (int i = 0; i < config.ListComponents.Count; i++)
                            {
                                allComponentInfos.Add(config.ListComponents[i]);
                            }
                        }
                    }
                }
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "components");
                var basicComponents = allComponentInfos.Where(c => c.GroupID == ComponentInfo.GP_BASIC).ToList();
                for (int i = 0; i < basicComponents.Count; i++)
                {
                    var info = basicComponents[i];
                    ComponentItem item = new ComponentItem();
                    item.Info = info;
                    item.Name = info.Name;
                    item.Display = info.Name;
                    item.Description = string.IsNullOrEmpty(info.Description) ? info.Name : info.Description;
                    item.GroupName = info.GroupID.ToString();
                    string strIcon = Path.Combine(path, string.Format("{0}.png", info.ID));
                    if (!File.Exists(strIcon))
                    {
                        strIcon = string.Format("/ReportDesigner;component/Images/00060{0}.png", info.ID);
                        item.Icon = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                        item.Icon = bitmap.Clone(); //调用Clone，防止占用文件
                    }
                    mListBasicComponentItems.Add(item);
                }
                var shapComponents = allComponentInfos.Where(c => c.GroupID == ComponentInfo.GP_SHAP).ToList();
                for (int i = 0; i < shapComponents.Count; i++)
                {
                    var info = shapComponents[i];
                    ComponentItem item = new ComponentItem();
                    item.Info = info;
                    item.Name = info.Name;
                    item.Display = info.Name;
                    item.Description = string.IsNullOrEmpty(info.Description) ? info.Name : info.Description;
                    item.GroupName = info.GroupID.ToString();
                    string strIcon = Path.Combine(path, string.Format("{0}.png", info.ID));
                    if (!File.Exists(strIcon))
                    {
                        strIcon = string.Format("/ReportDesigner;component/Images/00060{0}.png", info.ID);
                        item.Icon = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                        item.Icon = bitmap.Clone(); 
                    }
                    mListShapComponentItems.Add(item);
                }
                var chartComponents = allComponentInfos.Where(c => c.GroupID == ComponentInfo.GP_CHART).ToList();
                for (int i = 0; i < chartComponents.Count; i++)
                {
                    var info = chartComponents[i];
                    ComponentItem item = new ComponentItem();
                    item.Info = info;
                    item.Name = info.Name;
                    item.Display = info.Name;
                    item.Description = string.IsNullOrEmpty(info.Description) ? info.Name : info.Description;
                    item.GroupName = info.GroupID.ToString();
                    string strIcon = Path.Combine(path, string.Format("{0}.png", info.ID));
                    if (!File.Exists(strIcon))
                    {
                        strIcon = string.Format("/ReportDesigner;component/Images/00060{0}.png", info.ID);
                        item.Icon = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                        item.Icon = bitmap.Clone();
                    }
                    mListChartComponentItems.Add(item);
                }
                var otherComponents = allComponentInfos.Where(c => c.GroupID == ComponentInfo.GP_OTHER).ToList();
                for (int i = 0; i < otherComponents.Count; i++)
                {
                    var info = otherComponents[i];
                    ComponentItem item = new ComponentItem();
                    item.Info = info;
                    item.Name = info.Name;
                    item.Display = info.Name;
                    item.Description = string.IsNullOrEmpty(info.Description) ? info.Name : info.Description;
                    item.GroupName = info.GroupID.ToString();
                    string strIcon = Path.Combine(path, string.Format("{0}.png", info.ID));
                    if (!File.Exists(strIcon))
                    {
                        strIcon = string.Format("/ReportDesigner;component/Images/00060{0}.png", info.ID);
                        item.Icon = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        BitmapImage bitmap = new BitmapImage(new Uri(strIcon, UriKind.RelativeOrAbsolute));
                        item.Icon = bitmap.Clone();
                    }
                    mListChartComponentItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }


        #endregion


        private void ComponentDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.Parameter as ComponentItem;
            if (item == null) { return; }
            string strName = item.Name;
            var result = MessageBox.Show(string.Format("确定删除元件 {0} ?", strName), App.AppTitle,
                MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }
            var info = item.Info;
            if (info == null) { return; }
            int groupID = info.GroupID;
            if (groupID == 1)
            {
                mListBasicComponentItems.Remove(item);
            }
            if (groupID == 2)
            {
                mListShapComponentItems.Remove(item);
            }
            if (groupID == 3)
            {
                mListChartComponentItems.Remove(item);
            }
            SaveComponentConfig();
        }

        private void ComponentDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveComponentConfig()
        {
            ComponentConfig config = new ComponentConfig();
            for (int i = 0; i < mListBasicComponentItems.Count; i++)
            {
                var item = mListBasicComponentItems[i];
                var info = item.Info;
                if (info != null)
                {
                    if (!info.IsBuildIn)
                    {
                        config.ListComponents.Add(info);
                    }
                }
            }
            for (int i = 0; i < mListShapComponentItems.Count; i++)
            {
                var item = mListShapComponentItems[i];
                var info = item.Info;
                if (info != null)
                {
                    if (!info.IsBuildIn)
                    {
                        config.ListComponents.Add(info);
                    }
                }
            }
            for (int i = 0; i < mListChartComponentItems.Count; i++)
            {
                var item = mListChartComponentItems[i];
                var info = item.Info;
                if (info != null)
                {
                    if (!info.IsBuildIn)
                    {
                        config.ListComponents.Add(info);
                    }
                }
            }
            for (int i = 0; i < mListOtherComponentItems.Count; i++)
            {
                var item = mListOtherComponentItems[i];
                var info = item.Info;
                if (info != null)
                {
                    if (!info.IsBuildIn)
                    {
                        config.ListComponents.Add(info);
                    }
                }
            }
            if (config.ListComponents.Count > 0)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ComponentConfig.FILE_NAME);
                OperationReturn optReturn = XMLHelper.SerializeFile(config, path);
                if (!optReturn.Result)
                {
                    ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                    return;
                }
                ShowInfomation(string.Format("存储元件成功！"));
            }
        }


        #region Event Handlers

        void ListItemPanel_ListItemEvent(object sender, RoutedPropertyChangedEventArgs<ListItemEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            var item = args.Item as ComponentItem;
            if (item == null) { return; }
            int code = args.Code;
            if (code == ListItemEventArgs.EVT_MOUSEDOUBLECLICK)
            {
                if (PageParent != null)
                {
                    PageParent.InsertComponent(item);
                }
            }
        }

        #endregion


        #region Others

        private void ShowException(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInfomation(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

    }
}
