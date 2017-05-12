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
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using NetInfo.Wpf.AvalonDock.Layout;
using ReportDesigner.Models;

namespace ReportDesigner
{
    /// <summary>
    /// ReportDesignPanel.xaml 的交互逻辑
    /// </summary>
    public partial class ReportDesignPanel
    {

        #region Members

        private bool mIsInited;

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
            AddHandler(GridHeader.GridHeaderSizeChangedEvent,
                new RoutedPropertyChangedEventHandler<GridHeaderSizeChangedEventArgs>(GridHeader_SizeChanged));
            AddHandler(EditableElement.EditableTextChangedEvent,
                new RoutedPropertyChangedEventHandler<EditableTextChangedEventArgs>(
                    EditableElement_EditableTextChanged));
            AddHandler(KeyDownEvent, new KeyEventHandler(Panel_KeyDown));
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

        void EditableElement_EditableTextChanged(object sender,
            RoutedPropertyChangedEventArgs<EditableTextChangedEventArgs> e)
        {
            IsModified = true;
        }

        void Panel_KeyDown(object sender, KeyEventArgs e)
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
                    EditableElement textElement = new EditableElement();
                    cell.Content = textElement;
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
                    var textElement = cell.Content as EditableElement;
                    if (textElement != null)
                    {
                        textElement.IsInEditMode = true;
                        var textbox = textElement.TextBox;
                        if (textbox != null)
                        {
                            textbox.Focus();
                        }
                    }
                }
            }
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
                var layoutDocument = LayoutPanel;
                if (layoutDocument != null)
                {
                    layoutDocument.Title = document.Name;
                }
            }
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
                    var cell = cells[cellKey];
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
                    EditableElement textElement;
                    var sequenceElement = cell.Content as SequenceElement;
                    if (sequenceElement != null)
                    {
                        ReportSequence reportSequence = new ReportSequence();
                        var dataSet = sequenceElement.DataSet;
                        if (dataSet != null)
                        {
                            reportSequence.DataSetName = dataSet.Name;
                        }
                        var dataField = sequenceElement.DataField;
                        if (dataField != null)
                        {
                            reportSequence.DataFieldName = dataField.Name;
                            var dataTable = dataField.Table;
                            if (dataTable != null)
                            {
                                reportSequence.DataTableName = dataTable.Name;
                            }
                        }
                        reportSequence.Expression = sequenceElement.Text;
                        reportElement = reportSequence;
                    }
                    else
                    {
                        textElement = cell.Content as EditableElement;
                        if (textElement != null)
                        {
                            if (!string.IsNullOrEmpty(textElement.Text))
                            {
                                ReportText reportText = new ReportText();
                                reportText.Text = textElement.Text;
                                textElement.Tag = reportText;
                                reportElement = reportText;
                            }
                        }
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

                        textElement = cell.Content as EditableElement;
                        if (textElement != null)
                        {
                            var textBlock = textElement.TextBlock;
                            if (textBlock != null)
                            {
                                var textDecorations = textBlock.TextDecorations;
                                if (textDecorations != null && Equals(textDecorations, TextDecorations.Underline))
                                {
                                    style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Underlined;
                                }
                            }
                            style.HAlign = (int)textElement.HAlign;
                            style.VAlign = (int)textElement.VAlign;
                        }

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
