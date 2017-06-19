﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NetInfo.Common;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using NetInfo.Wpf.AvalonDock.Layout;
using NetInfo.Wpf.AvalonDock.Layout.Serialization;
using ReportDesigner.Commands;
using ReportDesigner.Models;
using ReportDesigner.UserControls;

namespace ReportDesigner
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {

        #region Members

        private bool mIsInited;
        private ObservableCollection<ComboItem> mListFontFamilies = new ObservableCollection<ComboItem>();
        private ObservableCollection<ComboItem> mListFontSizes = new ObservableCollection<ComboItem>();
        private ObservableCollection<ComboItem> mListBorderWidths = new ObservableCollection<ComboItem>();

        private ObservableCollection<DataSourceItem> mListDataSourceItems = new ObservableCollection<DataSourceItem>();
        private ObservableCollection<DataSetItem> mListDataSetItems = new ObservableCollection<DataSetItem>();

        private ObservableCollection<BorderStyleViewModel> mListBorderStyles =
            new ObservableCollection<BorderStyleViewModel>();

        private ObservableCollection<CellStyleViewModel> mListCellStyles =
            new ObservableCollection<CellStyleViewModel>();
        private List<ReportDesignPanel> mListReportPanels = new List<ReportDesignPanel>();
        private DesignerConfig mDesignerConfig;
        private ReportFileObject mRootFileObject;

        #endregion


        #region ColorViewModel

        public ColorViewModel ColorViewModel { get; set; }

        #endregion


        public MainWindow()
        {
            InitializeComponent();

            ColorViewModel = new ColorViewModel();
            mRootFileObject = new ReportFileObject();

            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;

            AddHandler(DesignGrid.GridSelectedEvent,
                new RoutedPropertyChangedEventHandler<GridSelectedEventArgs>(DesignGrid_GridSelected));
            AddHandler(EditableElement.EditableElementEventEvent,
                new RoutedPropertyChangedEventHandler<EditableElementEventArgs>(EditableElement_EditableElementEvent));
            AddHandler(UCObjectPropertyEditor.PropertyValueChangedEvent,
                new RoutedPropertyChangedEventHandler<PropertyValueChangedEventArgs>(PropertyEditor_PropertyValueChanged));

            BtnBorderStyle.Click += BtnBorderStyle_Click;
            BtnFontBold.Click += BtnFontBold_Click;
            BtnFontItalic.Click += BtnFontItalic_Click;
            BtnFontUnderlined.Click += BtnFontUnderlined_Click;
            BtnFontLeft.Click += BtnFontLeft_Click;
            BtnFontCenter.Click += BtnFontCenter_Click;
            BtnFontRight.Click += BtnFontRight_Click;
            BtnFontTop.Click += BtnFontTop_Click;
            BtnFontMiddle.Click += BtnFontMiddle_Click;
            BtnFontBottom.Click += BtnFontBottom_Click;
            BtnFontColor.Click += BtnFontColor_Click;
            BtnFillColor.Click += BtnFillColor_Click;
            BtnBorderColor.Click += BtnBorderColor_Click;

            ComboFontFamily.SelectionChanged += ComboFontFamily_SelectionChanged;
            ComboFontSize.SelectionChanged += ComboFontSize_SelectionChanged;
            GalleryBorderStyle.SelectionChanged += GalleryBroderStyle_SelectionChanged;
            GalleryStyles.SelectionChanged += GalleryStyles_SelectionChanged;

            GalleryFontColor.SelectedColorChanged += GalleryFontColor_SelectedColorChanged;
            GalleryFillColor.SelectedColorChanged += GalleryFillColor_SelectedColorChanged;
            GalleryBorderColor.SelectedColorChanged += GalleryBorderColor_SelectedColorChanged;

            TvFiles.MouseDoubleClick += TvFiles_MouseDoubleClick;
            ListBoxDataSources.MouseDoubleClick += ListBoxDataSources_MouseDoubleClick;

            PanelFileExplorer.IsVisibleChanged += (s, e) => SetViewPanelCheck();
            PanelDataSource.IsVisibleChanged += (s, e) => SetViewPanelCheck();
            PanelDataSet.IsVisibleChanged += (s, e) => SetViewPanelCheck();
            PanelCellPropertyBox.IsVisibleChanged += (s, e) => SetViewPanelCheck();
            PanelElementPropertyBox.IsVisibleChanged += (s, e) => SetViewPanelCheck();
            PanelComponentBox.IsVisibleChanged += (s, e) => SetViewPanelCheck();
        }


        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < DocumentList.ChildrenCount; i++)
            {
                var layout = DocumentList.Children[i] as LayoutDocument;
                if (layout == null) { continue; }
                var panel = layout.Content as ReportDesignPanel;
                if (panel == null) { continue; }
                if (!panel.IsModified) { continue; }
                string strTitle = panel.Title;
                var result = MessageBox.Show(string.Format("{0} 已经修改，是否保存?", strTitle),
                    App.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    SaveReport(panel);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }


        #region Init and Load

        private void Init()
        {
            ComboFontFamily.ItemsSource = mListFontFamilies;
            ComboFontSize.ItemsSource = mListFontSizes;
            ComboBorderWidth.ItemsSource = mListBorderWidths;
            GalleryBorderStyle.ItemsSource = mListBorderStyles;
            TvFiles.ItemsSource = mRootFileObject.Children;
            ListBoxDataSources.ItemsSource = mListDataSourceItems;
            ListBoxDataSets.ItemsSource = mListDataSetItems;
            GalleryStyles.ItemsSource = mListCellStyles;
            BindCommands();
            InitDesignerConfig();
            InitFontFamilies();
            InitFontSizes();
            InitBorderWidths();
            InitBorderStyles();
            LoadDesignerConfig();
            InitCellStyles();
            CreateComponentBox();
            InitReportFiles();
            LoadDataSources();
            //InitReportPanels();
            SetViewPanelCheck();
            InitLayout();
            WindowState = WindowState.Maximized;
        }

        private void InitDesignerConfig()
        {
            mDesignerConfig = new DesignerConfig();
            string server = "localhost";
            int port = 8060;
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "emp-report\\emp-report-server");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            mDesignerConfig.PreviewDir = path;
            mDesignerConfig.PreviewServer = server;
            mDesignerConfig.PreviewPort = port;
        }

        private void LoadDesignerConfig()
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DesignerConfig.FILE_NAME);
            if (!File.Exists(strPath)) { return; }
            try
            {
                OperationReturn optReturn = XMLHelper.DeserializeFile<DesignerConfig>(strPath);
                if (!optReturn.Result)
                {
                    WriteLog(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                    return;
                }
                DesignerConfig config = optReturn.Data as DesignerConfig;
                if (config == null)
                {
                    WriteLog(string.Format("Fail.\t DesignerConfig is null."));
                    return;
                }
                mDesignerConfig = config;
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }

        private void InitFontFamilies()
        {
            mListFontFamilies.Clear();
            var fontFamilies = Fonts.SystemFontFamilies;
            foreach (var fontFamily in fontFamilies)
            {
                ComboItem item = new ComboItem();
                item.Name = fontFamily.ToString();
                item.Display = item.Name;
                item.Tip = item.Display;
                item.Data = fontFamily;
                mListFontFamilies.Add(item);
            }
        }

        private void InitFontSizes()
        {
            mListFontSizes.Clear();
            for (int i = 1; i < 100; i++)
            {
                ComboItem item = new ComboItem();
                item.Name = i.ToString();
                item.Display = item.Name;
                item.Tip = item.Display;
                item.Data = i.ToString();
                mListFontSizes.Add(item);
            }
        }

        private void InitBorderWidths()
        {
            mListBorderWidths.Clear();
            for (int i = 1; i < 100; i++)
            {
                ComboItem item = new ComboItem();
                item.Name = i.ToString();
                item.Display = item.Name;
                item.Tip = item.Display;
                item.Data = i.ToString();
                mListBorderWidths.Add(item);
            }
        }

        private void InitBorderStyles()
        {
            mListBorderStyles.Clear();
            for (int i = 1; i <= 3; i++)
            {
                BorderStyleViewModel item = new BorderStyleViewModel();
                item.Name = string.Format("Border[{0}]", i);
                item.Display = item.Name;
                item.Tip = item.Display;
                item.Type = i;
                item.Icon = new BitmapImage(new Uri(string.Format("/ReportDesigner;component/Images/00019{0:D3}.png", i), UriKind.RelativeOrAbsolute));
                mListBorderStyles.Add(item);
            }
        }

        private void InitReportFiles()
        {
            try
            {
                mRootFileObject.Children.Clear();
                if (mDesignerConfig == null) { return; }
                string serverDir = mDesignerConfig.PreviewDir;
                if (string.IsNullOrEmpty(serverDir)) { return; }
                string fileDir = Path.Combine(serverDir, "reports");
                if (!Directory.Exists(fileDir))
                {
                    Directory.CreateDirectory(fileDir);
                }
                DirectoryInfo rootDir = new DirectoryInfo(fileDir);
                ReportFileObject rootObj = new ReportFileObject();
                rootObj.Name = rootDir.Name;
                rootObj.Type = 0;
                rootObj.Icon = "/ReportDesigner;component/Images/00026.png";
                rootObj.Parent = mRootFileObject;
                rootObj.Data = rootDir;
                InitReportFiles(rootObj);
                mRootFileObject.Children.Add(rootObj);
                rootObj.IsExpanded = true;
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }

        private void InitReportFiles(ReportFileObject parentObj)
        {
            DirectoryInfo parentDir = parentObj.Data as DirectoryInfo;
            if (parentDir == null) { return; }
            DirectoryInfo[] subDirs = parentDir.GetDirectories();
            for (int i = 0; i < subDirs.Length; i++)
            {
                DirectoryInfo dir = subDirs[i];
                ReportFileObject obj = new ReportFileObject();
                obj.Name = dir.Name;
                obj.Type = 1;
                obj.Data = dir;
                obj.Icon = "/ReportDesigner;component/Images/00026.png";
                obj.Parent = parentObj;
                parentObj.Children.Add(obj);
                InitReportFiles(obj);
            }
            FileInfo[] files = parentDir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo file = files[i];
                if (file.Extension.ToLower().Equals(".rpt"))
                {
                    ReportFileObject obj = new ReportFileObject();
                    obj.Name = file.Name;
                    obj.Type = 2;
                    obj.Data = file;
                    obj.Icon = "/ReportDesigner;component/Images/00027.png";
                    obj.Parent = parentObj;
                    parentObj.Children.Add(obj);
                }
            }
        }

        private void LoadDataSources()
        {
            try
            {
                mListDataSourceItems.Clear();
                string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataSourceConfig.FILE_NAME);
                if (!File.Exists(strPath)) { return; }
                OperationReturn optReturn = XMLHelper.DeserializeFile<DataSourceConfig>(strPath);
                if (!optReturn.Result)
                {
                    ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                    return;
                }
                DataSourceConfig config = optReturn.Data as DataSourceConfig;
                if (config == null)
                {
                    ShowException(string.Format("DataSourceConfig is null."));
                    return;
                }
                for (int i = 0; i < config.DataSources.Count; i++)
                {
                    var dataSource = config.DataSources[i];
                    DataSourceItem item = new DataSourceItem();
                    item.Name = dataSource.Name;
                    item.Data = dataSource;
                    var dbInfo = dataSource.DBInfo;
                    if (dbInfo != null)
                    {
                        dbInfo.RealPassword = dbInfo.Password;
                        if (dbInfo.TypeID == 1)
                        {
                            item.Icon = new BitmapImage(new Uri("/Images/00037.png", UriKind.RelativeOrAbsolute));
                        }
                        if (dbInfo.TypeID == 2)
                        {
                            item.Icon = new BitmapImage(new Uri("/Images/00038.png", UriKind.RelativeOrAbsolute));
                        }
                        if (dbInfo.TypeID == 3)
                        {
                            item.Icon = new BitmapImage(new Uri("/Images/00039.png", UriKind.RelativeOrAbsolute));
                        }
                        item.Detail = dbInfo.ToString();
                    }
                    mListDataSourceItems.Add(item);
                }
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }

        private void InitDataSets()
        {
            mListDataSetItems.Clear();
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var document = panel.Document;
            if (document == null) { return; }
            var dss = document.DataSets;
            for (int i = 0; i < dss.Count; i++)
            {
                var ds = dss[i];
                string strName = ds.Name;
                string strDataSource = ds.DataSourceName;
                DataSetItem item = new DataSetItem();
                item.Name = strName;
                item.Tip = string.Format("[{0}][{1}]", strName, strDataSource);
                item.Icon = new BitmapImage(new Uri("/Images/00043.png", UriKind.RelativeOrAbsolute));
                item.Data = ds;
                mListDataSetItems.Add(item);
            }
        }

        private void InitLayout()
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            if (!File.Exists(strPath)) { return; }
            var serializer = new XmlLayoutSerializer(PanelManager);
            using (var stream = new StreamReader(strPath))
            {
                serializer.Deserialize(stream);
            }
            SetViewPanelCheck();
        }

        private void InitCellStyles()
        {
            mListCellStyles.Clear();
            //内置样式
            IList<CellStyleInfo> cellStyles = CellStyleInfo.InitCellStyles();
            //加载外部的自定义样式
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CellStyleConfig.FILE_NAME);
            if (File.Exists(path))
            {
                OperationReturn optReturn = XMLHelper.DeserializeFile<CellStyleConfig>(path);
                if (optReturn.Result)
                {
                    var config = optReturn.Data as CellStyleConfig;
                    if (config != null)
                    {
                        for (int i = 0; i < config.CellStyles.Count; i++)
                        {
                            cellStyles.Add(config.CellStyles[i]);
                        }
                    }
                }
            }
            for (int i = 0; i < cellStyles.Count; i++)
            {
                var cellStyle = cellStyles[i];
                CellStyleViewModel item = new CellStyleViewModel();
                item.CellStyle = cellStyle;
                item.Name = cellStyle.Name;
                item.SetStyle();
                mListCellStyles.Add(item);
            }

        }

        #endregion


        #region EventHandlers

        void LayoutReport_IsSelectedChanged(object sender, EventArgs e)
        {
            var layout = sender as LayoutDocument;
            if (layout == null) { return; }
            if (!layout.IsSelected) { return; }
            InitDataSets();
            SetToolBarStatus();
            SetStatus();
        }

        void LayoutReport_Closing(object sender, CancelEventArgs e)
        {
            var layout = sender as LayoutDocument;
            if (layout == null) { return; }
            var panel = layout.Content as ReportDesignPanel;
            if (panel == null) { return; }
            if (panel.IsModified)
            {
                var result = MessageBox.Show(string.Format("报表已经修改，是否保存?"), App.AppTitle,
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    layout.IsSelected = true;
                    SaveReport();
                    mListReportPanels.Remove(panel);
                }
                else if (result == MessageBoxResult.No)
                {
                    mListReportPanels.Remove(panel);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                mListReportPanels.Remove(panel);
            }
        }

        void DesignGrid_GridSelected(object sender, RoutedPropertyChangedEventArgs<GridSelectedEventArgs> e)
        {
            SetToolBarStatus();
            CreateCellPropertyBox();
            CreateElementPropertyBox();
            SetStatus();
        }

        void EditableElement_EditableElementEvent(object sender,
           RoutedPropertyChangedEventArgs<EditableElementEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            int code = args.Code;
            if (code == EditableElementEventArgs.EVT_EDIT_END)
            {
                var panel = GetCurrentDesignPanel();
                if (panel != null)
                {
                    var layout = panel.LayoutPanel;
                    if (layout != null)
                    {
                        panel.SetValue(ReportDesignPanel.TitleProperty, layout.Title);
                    }
                    panel.IsModified = true;
                }
            }
        }

        void PropertyEditor_PropertyValueChanged(object sender,
            RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs> e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel != null)
            {
                panel.IsModified = true;
            }
            SetToolBarStatus();
        }

        void BtnFontUnderlined_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontUnderlined.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.TextDecration = ischecked ? TextDecorations.Underline : null;
            }
            panel.IsModified = true;
        }

        void BtnFontItalic_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontItalic.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.FontStyle = ischecked ? FontStyles.Italic : FontStyles.Normal;
            }
            panel.IsModified = true;
        }

        void BtnFontBold_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontBold.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.FontWeight = ischecked ? FontWeights.Bold : FontWeights.Normal;
            }
            panel.IsModified = true;
        }

        void BtnFontBottom_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontBottom.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (ischecked)
                {
                    cell.VAlign = VerticalAlignment.Bottom;
                }
                else
                {
                    cell.VAlign = VerticalAlignment.Stretch;
                }
            }
            if (ischecked)
            {
                BtnFontTop.IsChecked = false;
                BtnFontMiddle.IsChecked = false;
            }
            panel.IsModified = true;
        }

        void BtnFontMiddle_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontMiddle.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (ischecked)
                {
                    cell.VAlign = VerticalAlignment.Center;
                }
                else
                {
                    cell.VAlign = VerticalAlignment.Stretch;
                }
            }
            if (ischecked)
            {
                BtnFontTop.IsChecked = false;
                BtnFontBottom.IsChecked = false;
            }
            panel.IsModified = true;
        }

        void BtnFontTop_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontTop.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (ischecked)
                {
                    cell.VAlign = VerticalAlignment.Top;
                }
                else
                {
                    cell.VAlign = VerticalAlignment.Stretch;
                }
            }
            if (ischecked)
            {
                BtnFontMiddle.IsChecked = false;
                BtnFontBottom.IsChecked = false;
            }
            panel.IsModified = true;
        }

        void BtnFontRight_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontRight.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (ischecked)
                {
                    cell.HAlign = HorizontalAlignment.Right;
                }
                else
                {
                    cell.HAlign = HorizontalAlignment.Stretch;
                }
            }
            if (ischecked)
            {
                BtnFontLeft.IsChecked = false;
                BtnFontCenter.IsChecked = false;
            }
            panel.IsModified = true;
        }

        void BtnFontCenter_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontCenter.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (ischecked)
                {
                    cell.HAlign = HorizontalAlignment.Center;
                }
                else
                {
                    cell.HAlign = HorizontalAlignment.Stretch;
                }
            }
            if (ischecked)
            {
                BtnFontLeft.IsChecked = false;
                BtnFontRight.IsChecked = false;
            }
            panel.IsModified = true;
        }

        void BtnFontLeft_Click(object sender, RoutedEventArgs e)
        {
            var ischecked = BtnFontLeft.IsChecked == true;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (ischecked)
                {
                    cell.HAlign = HorizontalAlignment.Left;
                }
                else
                {
                    cell.HAlign = HorizontalAlignment.Stretch;
                }
            }
            if (ischecked)
            {
                BtnFontCenter.IsChecked = false;
                BtnFontRight.IsChecked = false;
            }
            panel.IsModified = true;
        }

        void BtnBorderColor_Click(object sender, RoutedEventArgs e)
        {

        }

        void BtnFillColor_Click(object sender, RoutedEventArgs e)
        {
            var color = ColorViewModel.FillColor;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.Background = new SolidColorBrush(color);
            }
            panel.IsModified = true;
        }

        void BtnFontColor_Click(object sender, RoutedEventArgs e)
        {
            var color = ColorViewModel.FontColor;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.Foreground = new SolidColorBrush(color);
            }
            panel.IsModified = true;
        }

        void BtnBorderStyle_Click(object sender, RoutedEventArgs e)
        {
            SetBorderStyle();
        }

        void ComboFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tag = ComboFontSize.Tag;
            if (tag != null && (bool)tag) { return; }
            var fontSizeItem = ComboFontSize.SelectedItem as ComboItem;
            if (fontSizeItem == null) { return; }
            int size = int.Parse(fontSizeItem.Name);
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.FontSize = size;
            }
            panel.IsModified = true;
        }

        void ComboFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tag = ComboFontFamily.Tag;
            if (tag != null && (bool)tag) { return; }
            var fontFamilyItem = ComboFontFamily.SelectedItem as ComboItem;
            if (fontFamilyItem == null) { return; }
            string strName = fontFamilyItem.Name;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                var textElement = cell.Content as EditableElement;
                if (textElement == null) { continue; }
                try
                {
                    textElement.FontFamily = new FontFamily(strName);
                }
                catch { }
            }
            panel.IsModified = true;
        }

        void GalleryBorderColor_SelectedColorChanged(object sender, RoutedEventArgs e)
        {

        }

        void GalleryFillColor_SelectedColorChanged(object sender, RoutedEventArgs e)
        {
            var color = ColorViewModel.FillColor;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.Background = new SolidColorBrush(color);
            }
            panel.IsModified = true;
        }

        void GalleryFontColor_SelectedColorChanged(object sender, RoutedEventArgs e)
        {
            var color = ColorViewModel.FontColor;
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.Foreground = new SolidColorBrush(color);
            }
            panel.IsModified = true;
        }

        void GalleryBroderStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = GalleryBorderStyle.SelectedItem as BorderStyleViewModel;
            if (item != null)
            {
                BtnBorderStyle.Tag = item;
                BtnBorderStyle.Icon = item.Icon;
                SetBorderStyle();
            }
            var panel = GetCurrentDesignPanel();
            if (panel != null)
            {
                panel.IsModified = true;
            }
        }

        void GalleryStyles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tag = GalleryStyles.Tag;
            if (tag != null && (bool)tag) { return; }
            var item = GalleryStyles.SelectedItem as CellStyleViewModel;
            if (item != null)
            {
                SetCellStyle();
                var panel = GetCurrentDesignPanel();
                if (panel != null)
                {
                    panel.IsModified = true;
                }
            }
        }

        void TvFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = TvFiles.SelectedItem as ReportFileObject;
            if (item == null) { return; }
            OpenReport();
        }

        void ListBoxDataSources_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ListBoxDataSources.SelectedItem as DataSourceItem;
            if (item == null) { return; }
            ModifyDataSource();
        }

        #endregion


        #region Operations

        private void SaveReport()
        {
            var panel = GetCurrentDesignPanel();
            SaveReport(panel);
        }

        private void SaveReport(ReportDesignPanel panel)
        {
            if (panel == null) { return; }
            panel.SaveReport();
            InitReportFiles();
        }

        private void PreviewReport()
        {
            try
            {
                var panel = GetCurrentDesignPanel();
                if (panel == null) { return; }
                if (panel.IsModified)
                {
                    SaveReport();
                }
                var document = panel.Document;
                if (document == null) { return; }
                string report = document.Name;
                System.Diagnostics.Process.Start("explorer.exe",
                    string.Format("\"http://{0}:{1}/report_server?report_name={2}\"",
                    mDesignerConfig.PreviewServer,
                    mDesignerConfig.PreviewPort,
                    report));
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }

        private void NewReport()
        {
            string strTitle = string.Format("NewReport");
            ReportDesignPanel panel = new ReportDesignPanel();
            panel.PageParent = this;
            panel.DesignerConfig = mDesignerConfig;
            LayoutDocument layout = new LayoutDocument();
            layout.Closing += LayoutReport_Closing;
            layout.IsSelectedChanged += LayoutReport_IsSelectedChanged;
            layout.CanFloat = false;
            layout.Title = strTitle;
            layout.Content = panel;
            panel.LayoutPanel = layout;
            panel.SetValue(ReportDesignPanel.TitleProperty, layout.Title);
            DocumentList.Children.Add(layout);
            mListReportPanels.Add(panel);
            layout.IsSelected = true;
        }

        private void OpenReport()
        {
            var reportFile = TvFiles.SelectedItem as ReportFileObject;
            if (reportFile == null) { return; }
            if (reportFile.Type != 2) { return; }
            FileInfo file = reportFile.Data as FileInfo;
            if (file == null) { return; }
            string path = file.FullName;
            OperationReturn optReturn = XMLHelper.DeserializeFile<ReportDocument>(path);
            if (!optReturn.Result)
            {
                ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                return;
            }
            var reportDocument = optReturn.Data as ReportDocument;
            if (reportDocument == null) { return; }
            reportDocument.Path = path;
            reportDocument.Init();
            ReportDesignPanel reportPanel = null;
            for (int i = 0; i < mListReportPanels.Count; i++)
            {
                var panel = mListReportPanels[i];
                var document = panel.Document;
                if (document == null) { continue; }
                if (!string.IsNullOrEmpty(document.Path)
                    && document.Path.Equals(path))
                {
                    reportPanel = panel;
                    break;
                }
            }
            if (reportPanel == null)
            {
                reportPanel = new ReportDesignPanel();
                reportPanel.PageParent = this;
                reportPanel.DesignerConfig = mDesignerConfig;
                reportPanel.Title = reportDocument.Title;
                if (string.IsNullOrEmpty(reportPanel.Title))
                {
                    reportPanel.Title = reportDocument.Name;
                }
                reportPanel.Document = reportDocument;
                reportPanel.ReportFile = reportFile;
                LayoutDocument layout = new LayoutDocument();
                layout.Closing += LayoutReport_Closing;
                layout.IsSelectedChanged += LayoutReport_IsSelectedChanged;
                layout.CanFloat = false;
                layout.Content = reportPanel;
                layout.SetValue(LayoutDocument.TitleProperty, reportPanel.Title);
                reportPanel.LayoutPanel = layout;
                DocumentList.Children.Add(layout);
                mListReportPanels.Add(reportPanel);
                layout.IsSelected = true;
            }
            else
            {
                var layout = reportPanel.LayoutPanel;
                if (layout != null)
                {
                    layout.IsSelected = true;
                }
            }
        }

        private void DeleteReport()
        {
            var fileObj = TvFiles.SelectedItem as ReportFileObject;
            if (fileObj == null) { return; }
            if (fileObj.Type == 1)
            {
                var dirInfo = fileObj.Data as DirectoryInfo;
                if (dirInfo == null) { return; }
                var result = MessageBox.Show(string.Format("确定删除目录 {0} ?", dirInfo.Name), App.AppTitle,
                    MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) { return; }
                dirInfo.Delete(true);
            }
            else if (fileObj.Type == 2)
            {
                var fileInfo = fileObj.Data as FileInfo;
                if (fileInfo == null) { return; }
                var result = MessageBox.Show(string.Format("确定删除报表文件 {0} ?", fileInfo.Name), App.AppTitle,
                   MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes) { return; }
                fileInfo.Delete();
            }
            var parent = fileObj.Parent;
            if (parent != null)
            {
                for (int i = 0; i < mListReportPanels.Count; i++)
                {
                    var panel = mListReportPanels[i];
                    var temp = panel.ReportFile;
                    if (temp != null && temp == fileObj)
                    {
                        var layout = panel.LayoutPanel;
                        if (layout != null)
                        {
                            layout.Close();
                        }
                    }
                }
                parent.Children.Remove(fileObj);
            }
        }

        private void CreateDir()
        {

        }

        private void SetBorderStyle()
        {
            try
            {
                var borderStyle = BtnBorderStyle.Tag as BorderStyleViewModel;
                if (borderStyle == null) { return; }
                int type = borderStyle.Type;
                var panel = GetCurrentDesignPanel();
                if (panel == null) { return; }
                var grid = panel.Grid;
                if (grid == null) { return; }
                var cells = grid.SelectedCells;
                if (type == 1)
                {
                    //清除边框
                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i];
                        cell.BorderBrush = Brushes.Transparent;
                        cell.BorderThickness = new Thickness(0);
                    }
                }
                if (type == 2)
                {
                    //全边框
                    int startRowIndex = int.MaxValue;
                    int startColIndex = int.MaxValue;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i];
                        startRowIndex = Math.Min(startRowIndex, cell.RowIndex);
                        startColIndex = Math.Min(startColIndex, cell.ColumnIndex);
                    }
                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i];
                        cell.BorderBrush = Brushes.Gray;
                        if (cell.RowIndex == startRowIndex
                            && cell.ColumnIndex == startColIndex)
                        {
                            cell.BorderThickness = new Thickness(1, 1, 1, 1);
                        }
                        else if (cell.RowIndex == startRowIndex)
                        {
                            cell.BorderThickness = new Thickness(0, 1, 1, 1);
                        }
                        else if (cell.ColumnIndex == startColIndex)
                        {
                            cell.BorderThickness = new Thickness(1, 0, 1, 1);
                        }
                        else
                        {
                            cell.BorderThickness = new Thickness(0, 0, 1, 1);
                        }
                    }
                }
                if (type == 3)
                {
                    //外侧边框
                    int startRowIndex = int.MaxValue;
                    int startColIndex = int.MaxValue;
                    int endRowIndex = 0;
                    int endColIndex = 0;
                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i];
                        startRowIndex = Math.Min(startRowIndex, cell.RowIndex);
                        startColIndex = Math.Min(startColIndex, cell.ColumnIndex);
                        endRowIndex = Math.Max(endRowIndex, cell.RowIndex + cell.RowSpan);
                        endColIndex = Math.Max(endColIndex, cell.ColumnIndex + cell.ColSpan);
                    }
                    for (int i = 0; i < cells.Count; i++)
                    {
                        var cell = cells[i];
                        cell.BorderBrush = Brushes.Gray;
                        int left = 0;
                        int top = 0;
                        int right = 0;
                        int bottom = 0;
                        if (cell.ColumnIndex == startColIndex)
                        {
                            left = 1;
                        }
                        if (cell.RowIndex == startRowIndex)
                        {
                            top = 1;
                        }
                        if (cell.ColumnIndex + cell.ColSpan == endColIndex)
                        {
                            right = 1;
                        }
                        if (cell.RowIndex + cell.RowSpan == endRowIndex)
                        {
                            bottom = 1;
                        }
                        cell.BorderThickness = new Thickness(left, top, right, bottom);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }

        private void SetCellStyle()
        {
            var item = GalleryStyles.SelectedItem as CellStyleViewModel;
            if (item == null) { return; }
            var cellStyle = item.CellStyle;
            if (cellStyle == null) { return; }
            var style = cellStyle.Style;
            if (style == null) { return; }
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                SetCellStyle(cell, style);
                cell.AddedData1 = item;
            }
        }

        private void SetCellStyle(GridCell cell, VisualStyle style)
        {
            if (!string.IsNullOrEmpty(style.FontFamily))
            {
                cell.FontFamily = new FontFamily(style.FontFamily);
            }
            if (style.FontSize > 0)
            {
                cell.FontSize = style.FontSize;
            }
            cell.FontWeight = (style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Bold) > 0
                ? FontWeights.Bold
                : FontWeights.Normal;
            cell.FontStyle = (style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Italic) > 0
                ? FontStyles.Italic
                : FontStyles.Normal;
            cell.TextDecration = (style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Underlined) > 0
                ? TextDecorations.Underline
                : null;
            cell.HAlign = (HorizontalAlignment)style.HAlign;
            cell.VAlign = (VerticalAlignment)style.VAlign;
            if (!string.IsNullOrEmpty(style.ForeColor))
            {
                var color = ColorConverter.ConvertFromString(style.ForeColor);
                if (color != null)
                {
                    cell.Foreground = new SolidColorBrush((Color)color);
                }
            }
            if (!string.IsNullOrEmpty(style.BackColor))
            {
                var color = ColorConverter.ConvertFromString(style.BackColor);
                if (color != null)
                {
                    cell.Background = new SolidColorBrush((Color)color);
                }
            }
        }

        private void MergeCells()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            grid.MergeCells();
            panel.IsModified = true;
        }

        private void UnmergeCell()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            grid.UnmergeCells();
            panel.IsModified = true;
        }

        private void AppClose()
        {
            Close();
        }

        private void SaveDataSources()
        {
            DataSourceConfig config = new DataSourceConfig();
            for (int i = 0; i < mListDataSourceItems.Count; i++)
            {
                var item = mListDataSourceItems[i];
                var info = item.Data as DataSourceInfo;
                if (info == null) { continue; }
                config.DataSources.Add(info);
            }
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataSourceConfig.FILE_NAME);
            OperationReturn optReturn = XMLHelper.SerializeFile(config, strPath);
            if (!optReturn.Result)
            {
                ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
            }
        }

        private void AddDataSource()
        {
            UCDataSourceModify uc = new UCDataSourceModify();
            uc.IsModify = false;
            uc.ListAllDataSourceItems = mListDataSourceItems;
            PopupWindow popup = new PopupWindow();
            popup.Title = "添加数据源";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result == true)
            {
                var item = uc.DataSourceItem;
                if (item == null) { return; }
                mListDataSourceItems.Add(item);
                SaveDataSources();
            }
        }

        private void ModifyDataSource()
        {
            var item = ListBoxDataSources.SelectedItem as DataSourceItem;
            if (item == null) { return; }
            UCDataSourceModify uc = new UCDataSourceModify();
            uc.IsModify = true;
            uc.ListAllDataSourceItems = mListDataSourceItems;
            uc.DataSourceItem = item;
            PopupWindow popup = new PopupWindow();
            popup.Title = "编辑数据源";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result == true)
            {
                SaveDataSources();
            }
        }

        private void DeleteDataSource()
        {
            var item = ListBoxDataSources.SelectedItem as DataSourceItem;
            if (item == null) { return; }
            var result = MessageBox.Show(string.Format("确定删除数据源 {0} ？", item.Name), App.AppTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }
            mListDataSourceItems.Remove(item);
            SaveDataSources();
        }

        private void AddDataSet()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var document = panel.Document;
            UCDataSetWizard uc = new UCDataSetWizard();
            uc.IsModify = false;
            uc.ListAllDataSources = mListDataSourceItems;
            uc.ListAllDataSets = mListDataSetItems;
            PopupWindow popup = new PopupWindow();
            popup.Title = "添加数据集向导";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result == true)
            {
                var dataSetItem = uc.DataSetItem;
                if (dataSetItem == null) { return; }
                mListDataSetItems.Add(dataSetItem);
                if (document == null)
                {
                    document = new ReportDocument();
                    panel.Document = document;
                }
                var dataSet = dataSetItem.Data as ReportDataSet;
                if (dataSet == null) { return; }
                document.DataSets.Add(dataSet);
                panel.IsModified = true;
            }
        }

        private void ModifyDataSet()
        {
            var dataSetItem = ListBoxDataSets.SelectedItem as DataSetItem;
            if (dataSetItem == null) { return; }
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var document = panel.Document;
            UCDataSetWizard uc = new UCDataSetWizard();
            uc.IsModify = true;
            uc.ListAllDataSources = mListDataSourceItems;
            uc.ListAllDataSets = mListDataSetItems;
            uc.DataSetItem = dataSetItem;
            PopupWindow popup = new PopupWindow();
            popup.Title = "修改数据集";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result == true)
            {
                dataSetItem = uc.DataSetItem;
                if (dataSetItem == null) { return; }
                var dataSet = dataSetItem.Data as ReportDataSet;
                if (dataSet == null) { return; }
                if (document == null) { return; }
                var temp = document.DataSets.FirstOrDefault(d => d.Name == dataSet.Name);
                if (temp != null)
                {
                    document.DataSets.Remove(temp);
                }
                document.DataSets.Add(dataSet);
                panel.IsModified = true;
            }
        }

        private void DeleteDataSet()
        {
            var dataSetItem = ListBoxDataSets.SelectedItem as DataSetItem;
            if (dataSetItem == null) { return; }
            var result = MessageBox.Show(string.Format("确定删除数据集 {0} ?", dataSetItem.Name), App.AppTitle,
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var document = panel.Document;
            if (document != null)
            {
                var dataSet = dataSetItem.Data as ReportDataSet;
                if (dataSet != null)
                {
                    document.DataSets.Remove(dataSet);
                }
            }
            mListDataSetItems.Remove(dataSetItem);
        }

        private void SetViewPanelCheck()
        {
            var panel = GetPanlByContentID("PanelFileExplorer");
            if (panel != null)
            {
                CheckBoxFileExplorer.IsChecked = panel.IsVisible;
            }
            panel = GetPanlByContentID("PanelDataSource");
            if (panel != null)
            {
                CheckBoxDataSource.IsChecked = panel.IsVisible;
            }
            panel = GetPanlByContentID("PanelDataSet");
            if (panel != null)
            {
                CheckBoxDataSet.IsChecked = panel.IsVisible;
            }
            panel = GetPanlByContentID("PanelComponentBox");
            if (panel != null)
            {
                CheckBoxComponentBox.IsChecked = panel.IsVisible;
            }
            panel = GetPanlByContentID("PanelCellPropertyBox");
            if (panel != null)
            {
                CheckBoxCellPropertyBox.IsChecked = panel.IsVisible;
            }
            panel = GetPanlByContentID("PanelElementPropertyBox");
            if (panel != null)
            {
                CheckBoxElementPropertyBox.IsChecked = panel.IsVisible;
            }
        }

        private void SaveLayout()
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            var serializer = new XmlLayoutSerializer(PanelManager);
            using (var stream = new StreamWriter(strPath))
            {
                serializer.Serialize(stream);
            }
            ShowInfomation(string.Format("保存布局成功！"));
        }

        private void RestoreLayout()
        {
            string strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "layout.xml");
            if (!File.Exists(strPath)) { return; }
            var serializer = new XmlLayoutSerializer(PanelManager);
            using (var stream = new StreamReader(strPath))
            {
                serializer.Deserialize(stream);
            }
            SetViewPanelCheck();
        }

        public void SaveConfig()
        {
            if (mDesignerConfig == null) { return; }
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DesignerConfig.FILE_NAME);
            OperationReturn optReturn = XMLHelper.SerializeFile(mDesignerConfig, path);
            if (!optReturn.Result)
            {
                ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                return;
            }
            ShowInfomation(string.Format("保存配置文件成功！"));
        }

        private void SaveCellStyleConfig()
        {
            CellStyleConfig config = new CellStyleConfig();
            for (int i = 0; i < mListCellStyles.Count; i++)
            {
                var item = mListCellStyles[i];
                var cellStyle = item.CellStyle;
                if (cellStyle != null)
                {
                    if (!cellStyle.IsBuiltIn)
                    {
                        config.CellStyles.Add(cellStyle);
                    }
                }
            }
            if (config.CellStyles.Count > 0)
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CellStyleConfig.FILE_NAME);
                OperationReturn optReturn = XMLHelper.SerializeFile(config, path);
                if (!optReturn.Result)
                {
                    ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                    return;
                }
                ShowInfomation(string.Format("存储样式成功！"));
            }
        }


        #region Insert Report Element

        private void InsertText()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            TextElement textElement = new TextElement();
            textElement.Text = string.Empty;
            textElement.Cell = cell;
            cell.Content = textElement;
            CreateCellPropertyBox();
            CreateElementPropertyBox();
            var textBox = textElement.TextBox;
            if (textBox != null)
            {
                textBox.Focus();
            }
            textElement.IsInEditMode = true;
            panel.IsModified = true;
        }

        private void InsertSequence()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            SequenceElement sequenceElement = new SequenceElement();
            sequenceElement.Text = string.Empty;
            sequenceElement.Cell = cell;
            cell.Content = sequenceElement;
            CreateCellPropertyBox();
            CreateElementPropertyBox();
            panel.IsModified = true;
        }

        private void InsertImage()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            UCReportImageModify uc = new UCReportImageModify();
            uc.IsModify = false;
            uc.GridCell = cell;
            PopupWindow popup = new PopupWindow();
            popup.Title = "插入图片";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result != true) { return; }
            var imageElement = uc.ImageElement;
            if (imageElement != null)
            {
                cell.Content = imageElement;
            }
            panel.IsModified = true;
        }

        public void InsertComponent(ComponentItem componentItem)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            if (componentItem == null) { return; }
            var componentInfo = componentItem.Info;
            if (componentInfo == null) { return; }
            var style = componentInfo.Style;
            if (style != null)
            {
                SetCellStyle(cell, style);
            }
            var reportElement = componentItem.CreateCellElement();
            if (reportElement != null)
            {
                reportElement.Cell = cell;
            }
            cell.Content = reportElement;
            CreateCellPropertyBox();
            CreateElementPropertyBox();
            panel.IsModified = true;
        }

        #endregion


        #endregion


        #region Commands

        private void BindCommands()
        {
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Close, ApplicationClose_Executed,
                ApplicationClose_CanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Help, ApplicationHelp_Executed,
               ApplicationHelp_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.AppAboutCommand, ApplicationAbout_Executed,
               ApplicationAbout_CanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.New, ApplicationNew_Executed,
                ApplicationNew_CanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Open, ApplicationOpen_Executed,
                ApplicationOpen_CanExecute));
            CommandBindings.Add(new CommandBinding(ApplicationCommands.Save, ApplicationSave_Executed,
                ApplicationSave_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.ReportFileDeleteCommand, ReportFileDelete_Executed,
                ReportFileDelete_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.PreviewCommand, ReportPreview_Executed,
                ReportPreview_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DirCreateCommand, DirCreate_Executed,
                DirCreate_CanExecute));

            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.CellMergeCommand, CellMerge_Executed,
                CellMerge_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.CellUnmergeCommand, CellUnmerge_Executed,
               CellUnmerge_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.InsertTextCommand, InsertText_Executed,
                InsertText_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.InsertSequenceCommand, InsertSequence_Executed,
               InsertSequence_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.InsertImageCommand, InsertImage_Executed,
               InsertImage_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.InsertChartCommand, InsertChart_Executed,
               InsertChart_CanExecute));

            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DataSourceAddCommand, DataSourceAdd_Executed,
                DataSourceAdd_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DataSourceModifyCommand, DataSourceModify_Executed,
                DataSourceModify_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DataSourceDeleteCommand, DataSourceDelete_Executed,
                DataSourceDelete_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DataSetAddCommand, DataSetAdd_Executed,
                DataSetAdd_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DataSetModifyCommand, DataSetModify_Executed,
                DataSetModify_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.DataSetDeleteCommand, DataSetDelete_Executed,
               DataSetDelete_CanExecute));

            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.ViewPanelCheckCommand, ViewPanelCheck_Executed,
                ViewPanelCheck_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.LayoutSaveCommand, LayoutSave_Executed,
                LayoutSave_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.LayoutRestoreCommand, LayoutRestore_Executed,
                LayoutRestore_CanExecute));

            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.SaveAsStyleCommand, SaveAsStyle_Executed,
                SaveAsStyle_CanExecute));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.SaveAsComponentCommand, SaveAsComponent_Executed,
                SaveAsComponent_CanExecute));

            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.CellStyleDeleteCommand, CellStyleDeleteComponent_Executed,
               CellStyleDeleteComponent_CanExecute));
        }


        #region Command Executed

        private void ApplicationClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AppClose();
        }

        private void ApplicationHelp_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void ApplicationAbout_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //if (mDesignerConfig == null) { return; }
            //string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DesignerConfig.FILE_NAME);
            //OperationReturn optReturn = XMLHelper.SerializeFile(mDesignerConfig, path);
            //if (!optReturn.Result)
            //{
            //    ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
            //    return;
            //}
            //ShowInfomation(string.Format("End.\t{0}", path));
        }

        private void ApplicationNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewReport();
        }

        private void ApplicationOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenReport();
        }

        private void ApplicationSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveReport();
        }

        private void ReportFileDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteReport();
        }

        private void ReportPreview_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            PreviewReport();
        }

        private void DirCreate_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            CreateDir();
        }

        private void CellMerge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MergeCells();
        }

        private void CellUnmerge_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            UnmergeCell();
        }

        private void InsertText_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertText();
        }

        private void InsertSequence_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertSequence();
        }

        private void InsertImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            InsertImage();
        }

        private void InsertChart_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void DataSourceAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddDataSource();
        }

        private void DataSourceModify_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ModifyDataSource();
        }

        private void DataSourceDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteDataSource();
        }

        private void DataSetAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddDataSet();
        }

        private void DataSetModify_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ModifyDataSet();
        }

        private void DataSetDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DeleteDataSet();
        }

        private void ViewPanelCheck_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var checkBox = e.Source as CheckBox;
            if (checkBox == null) { return; }
            string panelName = e.Parameter.ToString();
            var panel = GetPanlByContentID(panelName);
            if (panel == null) { return; }
            panel.IsVisible = checkBox.IsChecked == true;
        }

        private void LayoutSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveLayout();
        }

        private void LayoutRestore_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            RestoreLayout();
        }

        private void SaveAsStyle_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var cells = e.Parameter as List<GridCell>;
            if (cells == null) { return; }
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            UCCellStyleModify uc = new UCCellStyleModify();
            uc.GridCell = cell;
            PopupWindow popup = new PopupWindow();
            popup.Title = "存储样式";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result != true) { return; }
            var item = uc.Item;
            if (item == null) { return; }
            mListCellStyles.Add(item);
            var cellStyle = item.CellStyle;
            if (cellStyle != null)
            {
                SaveCellStyleConfig();
            }
        }

        private void SaveAsComponent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var cells = e.Parameter as List<GridCell>;
            if (cells == null) { return; }
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            UCComponentModify uc = new UCComponentModify();
            uc.GridCell = cell;
            var box = PanelComponentBox.Content as UCComponentBox;
            if (box == null) { return; }
            uc.ListComponentItems = box.GetAllComponentItems();
            PopupWindow popup = new PopupWindow();
            popup.Title = "存储元件";
            popup.Content = uc;
            var result = popup.ShowDialog();
            if (result != true) { return; }
            var item = uc.Item;
            if (item == null) { return; }
            box.AddComponent(item);
        }

        private void CellStyleDeleteComponent_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.Parameter as CellStyleViewModel;
            if (item == null) { return; }
            string strName = item.Name;
            var result = MessageBox.Show(string.Format("确定删除样式 {0}？", strName), App.AppTitle, MessageBoxButton.YesNoCancel,
                MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) { return; }
            mListCellStyles.Remove(item);
            var cellStyle = item.CellStyle;
            if (cellStyle != null)
            {
                SaveCellStyleConfig();
            }
        }

        #endregion


        #region Command CanExecute

        private void ApplicationClose_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ApplicationHelp_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ApplicationAbout_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ApplicationNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ApplicationOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = TvFiles.SelectedItem as ReportFileObject;
            if (item == null) { return; }
            if (item.Type == 2)
            {
                e.CanExecute = true;
            }
        }

        private void ApplicationSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            e.CanExecute = panel.IsModified;
        }

        private void ReportFileDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = TvFiles.SelectedItem as ReportFileObject;
            if (item == null) { return; }
            if (item.Type == 1
                || item.Type == 2)
            {
                e.CanExecute = true;
            }
        }

        private void ReportPreview_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            e.CanExecute = true;
        }

        private void DirCreate_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = TvFiles.SelectedItem as ReportFileObject;
            if (item == null) { return; }
            if (item.Type == 1)
            {
                e.CanExecute = true;
            }
        }

        private void CellMerge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count > 1)
            {
                e.CanExecute = true;
            }
        }

        private void CellUnmerge_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count > 0)
            {
                for (int i = 0; i < cells.Count; i++)
                {
                    var cell = cells[i];
                    if (cell.RowSpan > 1
                        || cell.ColSpan > 1)
                    {
                        e.CanExecute = true;
                        return;
                    }
                }
            }
        }

        private void InsertText_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count == 1)
            {
                e.CanExecute = true;
            }
        }

        private void InsertSequence_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count == 1)
            {
                e.CanExecute = true;
            }
        }

        private void InsertImage_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count == 1)
            {
                e.CanExecute = true;
            }
        }

        private void InsertChart_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //var cells = GetSelectedCells();
            //if (cells.Count == 1)
            //{
            //    e.CanExecute = true;
            //}
        }

        private void DataSourceAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DataSourceModify_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = ListBoxDataSources.SelectedItem as DataSourceItem;
            if (item == null) { return; }
            e.CanExecute = true;
        }

        private void DataSourceDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = ListBoxDataSources.SelectedItem as DataSourceItem;
            if (item == null) { return; }
            e.CanExecute = true;
        }

        private void DataSetAdd_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            e.CanExecute = true;
        }

        private void DataSetModify_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = ListBoxDataSets.SelectedItem as DataSetItem;
            if (item == null) { return; }
            e.CanExecute = true;
        }

        private void DataSetDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var item = ListBoxDataSets.SelectedItem as DataSetItem;
            if (item == null) { return; }
            e.CanExecute = true;
        }

        private void ViewPanelCheck_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LayoutSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void LayoutRestore_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveAsStyle_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            e.CanExecute = true;
        }

        private void SaveAsComponent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            e.CanExecute = true;
        }

        private void CellStyleDeleteComponent_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        #endregion


        #endregion


        #region Others

        public ReportDesignPanel GetCurrentDesignPanel()
        {
            var document = DocumentList.SelectedContent as LayoutDocument;
            if (document == null) { return null; }
            var panel = document.Content as ReportDesignPanel;
            return panel;
        }

        private LayoutAnchorable GetPanlByContentID(string contentID)
        {
            var panel =
                PanelManager.Layout.Descendents()
                    .OfType<LayoutAnchorable>()
                    .FirstOrDefault(p => p.ContentId == contentID);
            return panel;
        }

        public void SetToolBarStatus()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0) { return; }
            var cell = cells[0];
            ComboItem fontFamilyItem = null;
            var fontFamily = cell.FontFamily;
            if (fontFamily != null)
            {
                fontFamilyItem = mListFontFamilies.FirstOrDefault(ff => ff.Name.Equals(fontFamily.ToString()));
            }
            ComboFontFamily.Tag = true;     //禁止触发SelectionChanged事件
            ComboFontFamily.SelectedItem = fontFamilyItem;
            ComboFontFamily.Tag = false;
            var fontSize = cell.FontSize;
            var fontSizeItem = mListFontSizes.FirstOrDefault(fs => fs.Name == ((int)fontSize).ToString());
            ComboFontSize.Tag = true;
            ComboFontSize.SelectedItem = fontSizeItem;
            ComboFontSize.Tag = false;
            var fontWeight = cell.FontWeight;
            BtnFontBold.IsChecked = fontWeight == FontWeights.Bold;
            var fontStyle = cell.FontStyle;
            BtnFontItalic.IsChecked = fontStyle == FontStyles.Italic;
            var textDecration = cell.TextDecration;
            BtnFontUnderlined.IsChecked = textDecration != null && Equals(textDecration, TextDecorations.Underline);
            BtnFontLeft.IsChecked = cell.HAlign == HorizontalAlignment.Left;
            BtnFontCenter.IsChecked = cell.HAlign == HorizontalAlignment.Center;
            BtnFontRight.IsChecked = cell.HAlign == HorizontalAlignment.Right;
            BtnFontTop.IsChecked = cell.VAlign == VerticalAlignment.Top;
            BtnFontMiddle.IsChecked = cell.VAlign == VerticalAlignment.Center;
            BtnFontBottom.IsChecked = cell.VAlign == VerticalAlignment.Bottom;

            CellStyleViewModel preStyle = cell.AddedData1 as CellStyleViewModel;
            GalleryStyles.Tag = true;       //切换样式的时候禁止触发SelectionChanged事件
            GalleryStyles.SelectedItem = preStyle;
            GalleryStyles.Tag = false;
        }

        public void SetStatus()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var selection = grid.Selection;
            if (selection != null)
            {
                TxtSelection.Text = string.Format("{0}", selection);
            }
            var cells = grid.SelectedCells;
            if (cells.Count > 0)
            {
                var cell = cells[0];
                TxtCell.Text = string.Format("R:{0}, C:{1}, RS:{2}, CS:{3}",
                    cell.RowIndex,
                    cell.ColumnIndex,
                    cell.RowSpan,
                    cell.ColSpan);
            }
        }

        public void CreateCellPropertyBox()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count > 0)
            {
                var cell = cells[0];
                UCCellPropertyLister uc = new UCCellPropertyLister();
                uc.Panel = panel;
                uc.DataContext = cell;
                PanelCellPropertyBox.Content = uc;
            }
        }

        public void CreateElementPropertyBox()
        {
            var panel = GetCurrentDesignPanel();
            if (panel == null) { return; }
            var grid = panel.Grid;
            if (grid == null) { return; }
            var cells = grid.SelectedCells;
            if (cells.Count > 0)
            {
                var cell = cells[0];
                var element = cell.Content as ICellElement;
                if (element == null) { return;}
                UCObjectPropertyLister uc = new UCObjectPropertyLister();
                uc.Panel = panel;
                uc.DataContext = element;
                PanelElementPropertyBox.Content = uc;
            }
        }

        public void CreateComponentBox()
        {
            UCComponentBox uc = new UCComponentBox();
            uc.PageParent = this;
            uc.DesignerConfig = mDesignerConfig;
            PanelComponentBox.Content = uc;
        }

        private void ShowException(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInfomation(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion


        #region Log

        private void WriteLog(string category, string msg)
        {
            var app = App.Current as App;
            if (app != null)
            {
                app.WriteLog(category, msg);
            }
        }

        private void WriteLog(string msg)
        {
            WriteLog("ReportDesigner", msg);
        }

        #endregion

    }
}
