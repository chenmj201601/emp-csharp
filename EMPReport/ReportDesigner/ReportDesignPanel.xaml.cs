//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    44cd276c-3e4e-4add-a059-70be3c4c213f
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportDesignPanel
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner
//        File Name:                ReportDesignPanel
//
//        Created by Charley at 2017/4/17 15:11:54
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using NetInfo.Wpf.AvalonDock.Layout;
using ReportDesigner.Commands;
using ReportDesigner.Models;
using ReportDesigner.UserControls;

namespace ReportDesigner
{
    /// <summary>
    /// ReportDesignPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ReportDesignPanel
    {

        #region Members

        private bool mIsInited;

        public MainWindow PageParent;
        public DesignGrid Grid
        {
            get { return GridMain; }
        }

        public LayoutDocument LayoutPanel { get; set; }

        public ReportDocument Document { get; set; }

        public ReportFileObject ReportFile { get; set; }

        public DesignerConfig DesignerConfig { get; set; }

        #endregion


        public ReportDesignPanel()
        {
            InitializeComponent();

            Loaded += ReportDesignPanel_Loaded;
            MouseRightButtonDown += ReportDesignPanel_MouseRightButtonDown;
            AddHandler(GridHeader.GridHeaderSizeChangedEvent,
                new RoutedPropertyChangedEventHandler<GridHeaderSizeChangedEventArgs>(GridHeader_SizeChanged));
            AddHandler(EditableElement.EditableElementEventEvent,
                new RoutedPropertyChangedEventHandler<EditableElementEventArgs>(EditableElement_EditableElementEvent));
            AddHandler(PreviewKeyDownEvent, new KeyEventHandler(Panel_PreviewKeyDown));
            AddHandler(UCObjectPropertyEditor.PropertyValueChangedEvent,
                new RoutedPropertyChangedEventHandler<PropertyValueChangedEventArgs>(PropertyEditor_PropertyValueChanged));
        }

        void ReportDesignPanel_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }


        #region IsModified

        public static readonly DependencyProperty IsModifiedProperty =
           DependencyProperty.Register("IsModified", typeof(bool), typeof(ReportDesignPanel), new PropertyMetadata(default(bool)));

        public bool IsModified
        {
            get { return (bool)GetValue(IsModifiedProperty); }
            set { SetValue(IsModifiedProperty, value); }
        }

        #endregion


        #region Title

        public static readonly DependencyProperty TitleProperty =
          DependencyProperty.Register("Title", typeof(string), typeof(ReportDesignPanel), new PropertyMetadata(default(string)));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion


        #region Init and Load

        private void Init()
        {
            if (Document == null) { return; }
            GridMain.Init(Document);
        }

        #endregion


        #region Event Handers

        void GridHeader_SizeChanged(object sender, RoutedPropertyChangedEventArgs<GridHeaderSizeChangedEventArgs> e)
        {
            IsModified = true;
        }

        void EditableElement_EditableElementEvent(object sender,
            RoutedPropertyChangedEventArgs<EditableElementEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            int code = args.Code;
            if (code == EditableElementEventArgs.EVT_EDIT_END)
            {
                IsModified = true;
            }
        }

        void Panel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                //如果按下Delete键，删除单元格内容
                var grid = GridMain;
                var cells = grid.SelectedCells;
                for (int i = 0; i < cells.Count; i++)
                {
                    var cell = cells[i];
                    cell.Content = null;
                    //添加一个默认的可编辑文本框
                    EditableElement textElement = new EditableElement();
                    cell.Content = textElement;
                }
            }
            else if (e.Key == Key.Tab
                || e.Key == Key.Left
                || e.Key == Key.Right
                || e.Key == Key.Up
                || e.Key == Key.Down)
            {
                //移动选中的单元格
                e.Handled = true;   //跳过继续冒泡
                var grid = GridMain;
                var cells = grid.SelectedCells;
                if (cells.Count > 0)
                {
                    var cell = cells[0];
                    var rowIndex = cell.RowIndex;
                    var colIndex = cell.ColumnIndex;
                    var allCells = GridMain.GridCells;
                    GridCell fixCell = null;
                    if (e.Key == Key.Tab
                        || e.Key == Key.Right)
                    {
                        colIndex = colIndex + cell.ColSpan;
                    }
                    if (e.Key == Key.Down)
                    {
                        rowIndex = rowIndex + cell.RowSpan;
                    }
                    if (e.Key == Key.Left)
                    {
                        if (colIndex > 0)
                        {
                            colIndex--;
                        }
                    }
                    if (e.Key == Key.Up)
                    {
                        if (rowIndex > 0)
                        {
                            rowIndex--;
                        }
                    }
                    var keys = allCells.Keys;
                    foreach (var curKey in keys)
                    {
                        GridCell curCell = allCells[curKey] as GridCell;
                        if (curCell == null) { continue; }
                        if (curCell.RowIndex <= rowIndex
                            && curCell.RowIndex + curCell.RowSpan > rowIndex
                            && curCell.ColumnIndex <= colIndex
                            && curCell.ColumnIndex + curCell.ColSpan > colIndex)
                        {
                            fixCell = curCell;
                        }
                    }

                    if (fixCell != null)
                    {
                        if (allCells.ContainsValue(fixCell))
                        {
                            grid.SetSelection(fixCell, fixCell);
                        }
                    }
                }
            }
            else if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key >= Key.A && e.Key <= Key.Z)
                || (e.Key == Key.Add))
            {
                //如果按下普通字母或数字键，将单元格值为编辑模式
                var grid = GridMain;
                var cells = grid.SelectedCells;
                if (cells.Count > 0)
                {
                    var cell = cells[0];
                    var editableElement = cell.Content as EditableElement;
                    if (editableElement != null)
                    {
                        var textElement = editableElement as TextElement;
                        if (textElement == null)
                        {
                            textElement = new TextElement();
                            textElement.Cell = cell;
                            textElement.Text = editableElement.Text;
                            cell.Content = textElement;
                            editableElement = textElement;
                            if (PageParent != null)
                            {
                                PageParent.SetObjectProperty();
                            }
                        }
                        editableElement.IsInEditMode = true;
                        var textbox = editableElement.TextBox;
                        if (textbox != null)
                        {
                            textbox.Focus();
                        }
                    }
                }
            }
        }

        void PropertyEditor_PropertyValueChanged(object sender,
            RoutedPropertyChangedEventArgs<PropertyValueChangedEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            IsModified = true;
        }

        void ReportDesignPanel_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            CreateContextMenu();
        }

        #endregion


        #region Operations

        public void SaveReport()
        {
            var document = Document;
            if (document == null
                || string.IsNullOrEmpty(document.Path))
            {
                //尚未保存到文件，选择文件位置
                var dialog = new SaveFileDialog();
                dialog.Filter = "All support files(*.rpt)|*.rpt";
                var result = dialog.ShowDialog();
                if (result != true) { return; }
                if (document == null)
                {
                    document = new ReportDocument();
                }
                document.Path = dialog.FileName;
                string strFileName = dialog.SafeFileName;
                string strName = strFileName.Substring(0, strFileName.LastIndexOf("."));
                document.Name = strName;
                Document = document;
            }
            document.Title = Title;
            List<VisualStyle> listVisualStyles = new List<VisualStyle>();
            var grid = GridMain;
            if (grid != null)
            {
                ReportGrid reportGrid = new ReportGrid();
                reportGrid.RowCount = grid.RowDefinitions.Count - 1;
                reportGrid.ColCount = grid.ColumnDefinitions.Count - 1;
                reportGrid.CellWidth = (int)(grid.CellWidth * ReportDefine.ENLARGE);
                reportGrid.CellHeight = (int)(grid.CellHeight * ReportDefine.ENLARGE);
                string strWidth = string.Empty;
                string strHeight = string.Empty;
                for (int i = 1; i < grid.ColumnDefinitions.Count; i++)
                {
                    strWidth += (int)(grid.ColumnDefinitions[i].ActualWidth * ReportDefine.ENLARGE) + ",";
                }
                for (int i = 1; i < grid.RowDefinitions.Count; i++)
                {
                    strHeight += (int)(grid.RowDefinitions[i].ActualHeight * ReportDefine.ENLARGE) + ",";
                }
                if (!string.IsNullOrEmpty(strWidth))
                {
                    strWidth = strWidth.Substring(0, strWidth.Length - 1);
                }
                if (!string.IsNullOrEmpty(strHeight))
                {
                    strHeight = strHeight.Substring(0, strHeight.Length - 1);
                }
                reportGrid.Widths = strWidth;
                reportGrid.Heights = strHeight;
                document.Grid = reportGrid;
                document.Cells.Clear();
                var cells = grid.GridCells;
                var cellKeys = cells.Keys;
                foreach (var cellKey in cellKeys)
                {
                    var cell = cells[cellKey] as GridCell;
                    if (cell == null) { continue; }
                    ReportCell reportCell = null;
                    VisualStyle style = null;


                    #region 边框

                    if (cell.BorderThickness != new Thickness(0, 0, 0, 0))
                    {
                        reportCell = new ReportCell();
                        ReportBorder border = new ReportBorder();
                        border.Left = (int)cell.BorderThickness.Left;
                        border.Top = (int)cell.BorderThickness.Top;
                        border.Right = (int)cell.BorderThickness.Right;
                        border.Bottom = (int)cell.BorderThickness.Bottom;
                        style = new VisualStyle();
                        style.Border = border;
                    }

                    #endregion


                    #region 单元格内容

                    ReportElement reportElement = null;
                    var textElement = cell.Content as TextElement;
                    if (textElement != null)
                    {
                        ReportText reportText = textElement.ToReport();
                        reportElement = reportText;
                    }
                    var sequenceElement = cell.Content as SequenceElement;
                    if (sequenceElement != null)
                    {
                        ReportSequence reportSequence =sequenceElement.ToReport();
                        reportElement = reportSequence;
                    }
                    var imageElement = cell.Content as ImageElement;
                    if (imageElement != null)
                    {
                        ReportImage reportImage = imageElement.ToReport();
                        var fullName = imageElement.SourceFile;
                        imageElement.SetExt(fullName);
                        reportElement = reportImage;


                        #region 将图片文件拷贝到报表文件同级目录的images/reportname中

                        if (imageElement.IsSourceUpdated)
                        {
                            string reportName = document.Name;
                            string target = document.Path;
                            target = target.Substring(0, target.LastIndexOf('\\'));
                            target = Path.Combine(target, "resources");
                            target = Path.Combine(target, reportName);
                            if (!Directory.Exists(target))
                            {
                                Directory.CreateDirectory(target);
                            }
                            string ext = imageElement.GetExtName();
                            target = Path.Combine(target, string.Format("{0}{1}", reportImage.ID, ext));
                            string source = imageElement.SourceFile;
                            if (!source.Equals(target))
                            {
                                try
                                {
                                    File.Copy(source, target, true);
                                }
                                catch { }
                                imageElement.IsSourceUpdated = false;
                            }
                        }

                        #endregion

                    }
                    if (reportElement != null)
                    {
                        var cellElement = cell.Content as ICellElement;
                        reportElement.LinkUrl = cellElement.LinkUrl;
                    }

                    if (reportElement != null)
                    {
                        if (reportCell == null)
                        {
                            reportCell = new ReportCell();
                        }
                        reportCell.Element = reportElement;
                    }

                    #endregion


                    if (reportCell != null)
                    {
                        reportCell.RowIndex = cell.RowIndex - 1;
                        reportCell.ColIndex = cell.ColumnIndex - 1;
                        reportCell.RowSpan = cell.RowSpan;
                        reportCell.ColSpan = cell.ColSpan;


                        #region 单元格样式

                        if (style == null)
                        {
                            style = new VisualStyle();
                        }
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

                        var temp = listVisualStyles.FirstOrDefault(s => s.Key == style.Key);
                        if (temp == null)
                        {
                            listVisualStyles.Add(style);
                        }

                        var index = listVisualStyles.FindIndex(s => s.Key == style.Key);
                        if (index >= 0)
                        {
                            reportCell.Style = index;
                        }

                        #endregion


                        document.Cells.Add(reportCell);
                    }
                }
            }
            document.Styles.Clear();
            for (int i = 0; i < listVisualStyles.Count; i++)
            {
                document.Styles.Add(listVisualStyles[i]);
            }
            string file = document.Path;
            try
            {
                XmlUtil.SerializeFile(document, file);
                //ShowInfomation(string.Format("End.\t{0}", file));
                IsModified = false;
            }
            catch (Exception ex)
            {
                ShowException(ex.Message);
            }
        }

        #endregion


        #region Others

        private void CreateContextMenu()
        {
            var grid = GridMain;
            if (grid == null)
            {
                ContextMenu = null;
                return;
            }
            var cells = grid.SelectedCells;
            if (cells.Count <= 0)
            {
                ContextMenu = null;
                return;
            }
            ContextMenu contextMenu = new ContextMenu();
            MenuItem item = new MenuItem();
            item.Command = ReportDesignerCommands.SaveAsStyleCommand;
            item.CommandParameter = cells;
            item.Header = "存储为样式...";
            Image icon = new Image();
            icon.Source =
                new BitmapImage(new Uri("/ReportDesigner;component/Images/00054.png", UriKind.RelativeOrAbsolute));
            item.Icon = icon;
            contextMenu.Items.Add(item);
            item = new MenuItem();
            item.Command = ReportDesignerCommands.SaveAsComponentCommand;
            item.CommandParameter = cells;
            item.Header = "存储为元件...";
            icon = new Image();
            icon.Source =
                new BitmapImage(new Uri("/ReportDesigner;component/Images/00055.png", UriKind.RelativeOrAbsolute));
            item.Icon = icon;
            contextMenu.Items.Add(item);
            ContextMenu = contextMenu;
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

    }
}
