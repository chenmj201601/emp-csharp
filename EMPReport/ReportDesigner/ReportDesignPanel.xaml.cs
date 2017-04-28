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
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
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
                    TextElement textElement = new TextElement();
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
                    var textElement = cell.Content as TextElement;
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


                    #region 边框

                    if (cell.BorderThickness != new Thickness(0, 0, 0, 0))
                    {
                        reportCell = new ReportCell();
                        ReportBorder border = new ReportBorder();
                        border.Left = (int)cell.BorderThickness.Left;
                        border.Top = (int)cell.BorderThickness.Top;
                        border.Right = (int)cell.BorderThickness.Right;
                        border.Bottom = (int)cell.BorderThickness.Bottom;
                        reportCell.Border = border;
                    }

                    #endregion


                    #region 单元格内容

                    ReportElement reportElement = null;
                    var textElement = cell.Content as TextElement;
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
                    if (reportElement != null)
                    {
                        if (reportCell == null)
                        {
                            reportCell = new ReportCell();
                        }
                        reportCell.Element = reportElement;

                    }

                    #endregion


                    #region 单元格样式

                    textElement = cell.Content as TextElement;
                    if (textElement != null)
                    {
                        VisualStyle style = new VisualStyle();
                        var fontFamily = textElement.FontFamily;
                        if (fontFamily != null)
                        {
                            style.FontFamily = fontFamily.ToString();
                        }
                        var fontSize = textElement.FontSize;
                        style.FontSize = (int)fontSize;
                        var fontWeight = textElement.FontWeight;
                        if (fontWeight == FontWeights.Bold)
                        {
                            style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Bold;
                        }
                        var fontStyle = textElement.FontStyle;
                        if (fontStyle == FontStyles.Italic)
                        {
                            style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Italic;
                        }
                        var textBlock = textElement.TextBlock;
                        if (textBlock != null)
                        {
                            var textDecorations = textBlock.TextDecorations;
                            if (textDecorations != null && Equals(textDecorations, TextDecorations.Underline))
                            {
                                style.FontStyle = style.FontStyle | (int)NetInfo.EMP.Reports.FontStyle.Underlined;
                            }
                        }
                        var hAlign = textElement.HorizontalAlignment;
                        style.HorizontalAlignment = (int)hAlign;
                        var vAlign = textElement.VerticalAlignment;
                        style.VerticalAlignment = (int)vAlign;

                        var fontBrush = textElement.Foreground as SolidColorBrush;
                        if (fontBrush != null)
                        {
                            style.Foreground = fontBrush.Color.ToString();
                        }
                        var fillBrush = textElement.Background as SolidColorBrush;
                        if (fillBrush != null)
                        {
                            style.Background = fontBrush.Color.ToString();
                        }

                        var temp = listVisualStyles.FirstOrDefault(s => s.Key == style.Key);
                        if (temp == null)
                        {
                            listVisualStyles.Add(style);
                        }

                        var index = listVisualStyles.FindIndex(s => s.Key == style.Key);
                        if (index >= 0
                            && reportElement != null)
                        {
                            reportElement.Style = index;
                        }
                    }

                    #endregion


                    if (reportCell != null)
                    {
                        reportCell.RowIndex = cell.RowIndex - 1;
                        reportCell.ColIndex = cell.ColumnIndex - 1;
                        reportCell.RowSpan = cell.RowSpan;
                        reportCell.ColSpan = cell.ColSpan;
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

        public void SaveReportHtml()
        {
            try
            {
                var document = Document;
                if (document == null) { return; }
                if (DesignerConfig == null) { return; }
                string strName = document.Name;
                string strPath = DesignerConfig.PublishDir;
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                strPath = string.Format("{0}/{1}.html", strPath, strName);

                ReportGrid grid = document.Grid;
                if (grid == null) { return; }


                #region 网格行列信息

                int rowCount = grid.RowCount;
                int colCount = grid.ColCount;
                int cellWidth = grid.CellWidth / ReportDefine.ENLARGE;
                int cellHeight = grid.CellHeight / ReportDefine.ENLARGE;
                string strWidths = grid.Widths;
                string strHeights = grid.Heights;

                #endregion


                #region 单元格宽度，高度列表

                List<int> listWidths = new List<int>();
                List<int> listHeights = new List<int>();
                string[] temps = strWidths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < colCount; i++)
                {
                    if (i < temps.Length)
                    {
                        string temp = temps[i];
                        listWidths.Add(int.Parse(temp) / ReportDefine.ENLARGE);
                    }
                    else
                    {
                        listWidths.Add(0);
                    }
                }
                temps = strHeights.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < rowCount; i++)
                {
                    if (i < temps.Length)
                    {
                        string temp = temps[i];
                        listHeights.Add(int.Parse(temp) / ReportDefine.ENLARGE);
                    }
                    else
                    {
                        listHeights.Add(0);
                    }
                }

                #endregion


                #region 单元格字典集合

                List<ReportCell> cells = document.Cells;
                Dictionary<string, ReportCell> cellDictionary = new Dictionary<string, ReportCell>();
                for (int i = 0; i < cells.Count; i++)
                {
                    var cell = cells[i];
                    string key = string.Format("{0:D3}{1:D3}", cell.RowIndex, cell.ColIndex);
                    cellDictionary.Add(key, cell);
                }

                #endregion


                #region 输出表格架构

                FileStream fs = File.Open(strPath, FileMode.OpenOrCreate, FileAccess.Write);
                XmlTextWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 4;
                writer.WriteStartElement("table");
                for (int i = 0; i < colCount; i++)
                {
                    //设定列的宽度
                    int width = cellWidth;
                    if (i < listWidths.Count)
                    {
                        width = listWidths[i];
                    }
                    writer.WriteStartElement("col");
                    writer.WriteAttributeString("width", string.Format("{0}", width));
                    writer.WriteEndElement();
                }
                List<string> listSkipCells = new List<string>();
                for (int i = 0; i < document.Cells.Count; i++)
                {
                    //考虑合并单元格的情况，如果有单元格跨了多行或多列，
                    //将被跨的行或列的索引记录下来，后面生成每个单元格标签的时候需要跳过
                    var cell = document.Cells[i];
                    int rowIndex = cell.RowIndex;
                    int colIndex = cell.ColIndex;
                    int rowSpan = cell.RowSpan;
                    int colSpan = cell.ColSpan;
                    for (int k = 0; k < rowSpan; k++)
                    {
                        for (int l = 0; l < colSpan; l++)
                        {
                            if (k == 0 && l == 0)
                            {
                                //本身索引跳过
                                continue;
                            }
                            string key = string.Format("{0:D3}{1:D3}", k + rowIndex, l + colIndex);
                            listSkipCells.Add(key);
                        }
                    }
                }
                //按行，按列依次生成单元格，注意，listSkipCells 中的需要跳过
                for (int i = 0; i < rowCount; i++)
                {
                    int height = cellHeight;
                    if (i < listHeights.Count)
                    {
                        height = listHeights[i];
                    }
                    writer.WriteStartElement("tr");
                    writer.WriteAttributeString("style", string.Format("height:{0}px", height));
                    for (int j = 0; j < colCount; j++)
                    {
                        string key = string.Format("{0:D3}{1:D3}", i, j);
                        if (listSkipCells.Contains(key))
                        {
                            continue;
                        }
                        var cell = document.Cells.FirstOrDefault(c => c.RowIndex == i && c.ColIndex == j);
                        if (cell == null)
                        {
                            writer.WriteStartElement("td");
                            writer.WriteEndElement();
                        }
                        else
                        {

                            #region 单元格样式

                            writer.WriteStartElement("td");
                            writer.WriteAttributeString("rowspan", string.Format("{0}", cell.RowSpan));
                            writer.WriteAttributeString("colspan", string.Format("{0}", cell.ColSpan));
                            string strStyle = string.Empty;
                            var border = cell.Border;
                            if (border != null)
                            {
                                strStyle += string.Format("border-style:solid;border-width:0px;");
                                if (border.Left > 0)
                                {
                                    strStyle += string.Format("border-left-width:{0}px;", border.Left);
                                }
                                if (border.Top > 0)
                                {
                                    strStyle += string.Format("border-top-width:{0}px;", border.Top);
                                }
                                if (border.Right > 0)
                                {
                                    strStyle += string.Format("border-right-width:{0}px;", border.Right);
                                }
                                if (border.Bottom > 0)
                                {
                                    strStyle += string.Format("border-bottom-width:{0}px;", border.Bottom);
                                }
                            }
                            if (!string.IsNullOrEmpty(strStyle))
                            {
                                writer.WriteAttributeString("style", strStyle);
                            }
                            var reportText = cell.Element as ReportText;
                            if (reportText != null)
                            {
                                writer.WriteStartElement("div");
                                var styleIndex = reportText.Style;
                                if (styleIndex >= 0 && styleIndex < document.Styles.Count)
                                {
                                    var style = document.Styles[styleIndex];
                                    if (style != null)
                                    {
                                        strStyle = string.Empty;
                                        strStyle += string.Format("font-family:{0};", style.FontFamily);
                                        strStyle += string.Format("font-size:{0}px;", style.FontSize);
                                        if (style.FontStyle > 0)
                                        {
                                            if ((style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Bold) > 0)
                                            {
                                                strStyle += string.Format("font-weight:bold;");
                                            }
                                            if ((style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Italic) > 0)
                                            {
                                                strStyle += string.Format("font-style:italic;");
                                            }
                                            if ((style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Underlined) > 0)
                                            {
                                                strStyle += string.Format("text-decoration:underline;");
                                            }
                                        }
                                        if (style.HorizontalAlignment >= 0 && style.HorizontalAlignment < 3)
                                        {
                                            if (style.HorizontalAlignment == 0)
                                            {
                                                strStyle += string.Format("text-align:left;");
                                            }
                                            if (style.HorizontalAlignment == 1)
                                            {
                                                strStyle += string.Format("text-align:center;");
                                            }
                                            if (style.HorizontalAlignment == 2)
                                            {
                                                strStyle += string.Format("text-align:right;");
                                            }
                                        }
                                        var fontColor = style.Foreground;
                                        if (!string.IsNullOrEmpty(fontColor) && fontColor.Length > 3)
                                        {
                                            strStyle += string.Format("color:#{0};", fontColor.Substring(3));
                                        }
                                        var fillColor = style.Background;
                                        if (!string.IsNullOrEmpty(fillColor) && fillColor.Length > 3)
                                        {
                                            strStyle += string.Format("background-color:#{0};", fontColor.Substring(3));
                                        }
                                        strStyle = strStyle.TrimEnd(';');
                                        writer.WriteAttributeString("style", strStyle);
                                    }
                                }
                                writer.WriteString(string.Format("{0}", reportText.Text));
                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();

                            #endregion

                        }
                    }
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();

                #endregion

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
