
//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    2a9ed4d4-8b01-4f11-9e5c-fa5bed7174b6
//        CLR Version:              4.0.30319.42000
//        Name:                     DesignGrid
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                DesignGrid
//
//        Created by Charley at 2017/4/18 14:52:47
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;


namespace NetInfo.EMP.Reports.Controls
{
    public class DesignGrid : Grid
    {
        static DesignGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignGrid),
                new FrameworkPropertyMetadata(typeof(DesignGrid)));

            GridSelectedEvent = EventManager.RegisterRoutedEvent("GridSelected", RoutingStrategy.Bubble,
               typeof(RoutedPropertyChangedEventHandler<GridSelectedEventArgs>), typeof(DesignGrid));
        }

        public DesignGrid()
        {
            mSelection = new GridSelection();

            Loaded += DesignGrid_Loaded;
            AddHandler(GridCell.CellMouseEvent,
                new RoutedPropertyChangedEventHandler<GridCellMouseEventArgs>(GridCell_Mouse));
            AddHandler(GridHeader.GridHeaderSizeChangedEvent,
                new RoutedPropertyChangedEventHandler<GridHeaderSizeChangedEventArgs>(GridHeader_GridHeaderSizeChanged));
            MouseLeftButtonDown += ReportGrid_MouseLeftButtonDown;
            MouseMove += ReportGrid_MouseMove;
            MouseLeftButtonUp += ReportGrid_MouseLeftButtonUp;

            BindCommands();
        }

        void DesignGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }


        #region Cells

        /*
         * GridCells字典存放所有单元格，方便快速获取指定的单元格。
         * Key的规则是 {rowIndex:D3}{ColIndex:D3}
         * 注意：GridCells中的元素应该与ReportGrid的Children对应，新增或删除的时候要注意同步
         */

        private readonly Dictionary<string, CellBase> mGridCells = new Dictionary<string, CellBase>();

        public Dictionary<string, CellBase> GridCells
        {
            get { return mGridCells; }
        }

        #endregion


        #region Commands

        private void BindCommands()
        {
            CommandBindings.Add(new CommandBinding(GridHeader.HeadClickCommand, HeadClickCommand_Executed,
                (s, e) => e.CanExecute = true));
        }

        #endregion


        #region ShowBorder

        public static readonly DependencyProperty ShowBorderProperty =
           DependencyProperty.Register("ShowBorder", typeof(bool), typeof(DesignGrid), new PropertyMetadata(true, OnShowBorderChanged));

        public bool ShowBorder
        {
            get { return (bool)GetValue(ShowBorderProperty); }
            set { SetValue(ShowBorderProperty, value); }
        }

        private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = d as DesignGrid;
            if (obj != null)
            {
                obj.OnShowBorderChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }

        public void OnShowBorderChanged(bool oldValue, bool newValue)
        {

            if (newValue)
            {
                //创建边框
                InitGridBorder();
            }
            else
            {
                //清除边框
                for (int i = Children.Count - 1; i >= 0; i--)
                {
                    var reportCellBorder = Children[i] as GridBorder;
                    if (reportCellBorder != null)
                    {
                        Children.Remove(reportCellBorder);
                    }
                }
            }
        }

        #endregion


        #region CellWidth

        public static readonly DependencyProperty CellWidthProperty =
            DependencyProperty.Register("CellWidth", typeof(double), typeof(DesignGrid), new PropertyMetadata(50.0));

        public double CellWidth
        {
            get { return (double)GetValue(CellWidthProperty); }
            set { SetValue(CellWidthProperty, value); }
        }

        #endregion


        #region CellHeight

        public static readonly DependencyProperty CellHeightProperty =
            DependencyProperty.Register("CellHeight", typeof(double), typeof(DesignGrid), new PropertyMetadata(20.0));

        public double CellHeight
        {
            get { return (double)GetValue(CellHeightProperty); }
            set { SetValue(CellHeightProperty, value); }
        }

        #endregion


        #region RowCount

        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(DesignGrid), new PropertyMetadata(50));

        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        #endregion


        #region ColCount

        public static readonly DependencyProperty ColCountProperty =
            DependencyProperty.Register("ColCount", typeof(int), typeof(DesignGrid), new PropertyMetadata(20));

        public int ColCount
        {
            get { return (int)GetValue(ColCountProperty); }
            set { SetValue(ColCountProperty, value); }
        }

        #endregion


        #region Widths

        public static readonly DependencyProperty WidthsProperty =
            DependencyProperty.Register("Widths", typeof(string), typeof(DesignGrid), new PropertyMetadata(default(string)));

        public string Widths
        {
            get { return (string)GetValue(WidthsProperty); }
            set { SetValue(WidthsProperty, value); }
        }

        #endregion


        #region Heights

        public static readonly DependencyProperty HeightsProperty =
            DependencyProperty.Register("Heights", typeof(string), typeof(DesignGrid), new PropertyMetadata(default(string)));

        public string Heights
        {
            get { return (string)GetValue(HeightsProperty); }
            set { SetValue(HeightsProperty, value); }
        }

        #endregion


        #region ReportDocument

        public ReportDocument Document { get; set; }

        #endregion


        #region Init

        /*
         * 网格的列头默认用拉丁字母表示（行头是数字表示）
         * 这里有个问题没有处理，就是如果列大于26个的情况，这个时候就需要用多个拉丁字母了，如：AA
         * 目前没有代码实现
         */

        private bool mIsInited;

        private readonly char[] mColumnTitles =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        private readonly List<string> mListSkipCells = new List<string>();

        /// <summary>
        /// 初始化一个 50 * 20 的网格，考虑行和列的标题，实际是 51 * 21
        /// </summary>
        public void Init()
        {
            int rCount = RowCount;
            int cCount = ColCount;
            Init(rCount, cCount);
        }

        public void Init(int rowCount, int columnCount)
        {
            //考虑行和列的标题，应该行数和列数各加 1
            rowCount++;
            columnCount++;
            double defaultWidth = CellWidth;
            double defaultHeight = CellHeight;
            //初始化之前首先清楚行列已经单元格
            Children.Clear();
            mGridCells.Clear();
            ColumnDefinitions.Clear();
            RowDefinitions.Clear();
            List<int> listHeights = new List<int>();
            if (!string.IsNullOrEmpty(Heights))
            {
                string[] heights = Heights.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < heights.Length; i++)
                {
                    int height;
                    int.TryParse(heights[i], out height);
                    height = height / ReportDefine.ENLARGE;
                    listHeights.Add(height);
                }
            }
            List<int> listWidths = new List<int>();
            if (!string.IsNullOrEmpty(Widths))
            {
                string[] widths = Widths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < widths.Length; i++)
                {
                    int width;
                    int.TryParse(widths[i], out width);
                    width = width / ReportDefine.ENLARGE;
                    listWidths.Add(width);
                }
            }
            //初始化行定义
            for (int i = 0; i < rowCount; i++)
            {
                RowDefinition row = new RowDefinition();
                row.MinHeight = 2;
                if (i == 0)
                {
                    row.Height = new GridLength(20);
                }
                else
                {
                    if (i - 1 < listHeights.Count)
                    {
                        row.Height = new GridLength(listHeights[i - 1]);
                    }
                    else
                    {
                        row.Height = new GridLength(defaultHeight);
                    }
                }
                RowDefinitions.Add(row);
            }
            //初始化列定义
            for (int i = 0; i < columnCount; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.MinWidth = 2;
                if (i == 0)
                {
                    column.Width = new GridLength(30);
                }
                else
                {
                    if (i - 1 < listWidths.Count)
                    {
                        column.Width = new GridLength(listWidths[i - 1]);
                    }
                    else
                    {
                        column.Width = new GridLength(defaultWidth);
                    }
                }
                ColumnDefinitions.Add(column);
            }

            InitTableHeader();
            InitColumnHeader();
            InitRowHeader();
            InitCell();
            InitGridBorder();

            //以下初始化可能没有作用，因为单元格还没有生成
            SetSelection();
            SetCellRect();
            SetHeaderSelection();
        }

        public void Init(ReportDocument document)
        {
            if (document == null)
            {
                Init();
                return;
            }
            Document = document;
            var grid = document.Grid;
            if (grid == null)
            {
                Init();
                return;
            }
            int rowCount = grid.RowCount;
            int colCount = grid.ColCount;
            int cellWidth = grid.CellWidth / ReportDefine.ENLARGE;
            int cellHeight = grid.CellHeight / ReportDefine.ENLARGE;
            RowCount = rowCount;
            ColCount = colCount;
            CellWidth = cellWidth;
            CellHeight = cellHeight;
            Widths = grid.Widths;
            Heights = grid.Heights;
            Init();
        }

        private void InitGridBorder()
        {
            /*
             * 网格的边框是根据单元格添加进去的
             * 需要考虑单元格的合并情况
             * 这里要注意不要有重复边框
             * 表头的单元格应该四面边框
             * 列头和行头的单元格应该三面边框
             * 内容单元格则两面边框
             */

            //先清楚所有边框
            for (int i = Children.Count - 1; i >= 0; i--)
            {
                GridBorder border = Children[i] as GridBorder;
                if (border != null)
                {
                    Children.Remove(border);
                }
            }
            if (ShowBorder)
            {
                for (int i = 0; i < Children.Count; i++)
                {
                    CellBase cell = Children[i] as CellBase;
                    if (cell == null) { continue; }
                    int rowIndex = cell.RowIndex;
                    int colIndex = cell.ColumnIndex;
                    int rowSpan = cell.RowSpan;
                    int colSpan = cell.ColSpan;
                    GridBorder border = new GridBorder();
                    if (rowIndex == 0 && colIndex == 0)
                    {
                        border.BorderThickness = new Thickness(1, 1, 1, 1);
                    }
                    else if (rowIndex == 0)
                    {
                        border.BorderThickness = new Thickness(0, 1, 1, 1);
                    }
                    else if (colIndex == 0)
                    {
                        border.BorderThickness = new Thickness(1, 0, 1, 1);
                    }
                    else
                    {
                        border.BorderThickness = new Thickness(0, 0, 1, 1);
                    }
                    border.SetValue(ZIndexProperty, -1);
                    border.SetValue(RowProperty, rowIndex);
                    border.SetValue(ColumnProperty, colIndex);
                    border.SetValue(RowSpanProperty, rowSpan);
                    border.SetValue(ColumnSpanProperty, colSpan);
                    Children.Add(border);
                }
            }
        }

        private void InitTableHeader()
        {
            GridTableHeader header = new GridTableHeader();
            header.Content = "";
            header.RowIndex = 0;
            header.ColumnIndex = 0;
            header.RowSpan = 1;
            header.ColSpan = 1;
            header.Grid = this;
            header.DataContext = header;
            header.SetBinding(ToolTipProperty, new Binding("Rect"));
            Children.Add(header);
            mGridCells.Add(
                string.Format("{0}{1}", header.RowIndex.ToString("000"), header.ColumnIndex.ToString("000")), header);
        }

        private void InitColumnHeader()
        {
            int columnCount = ColumnDefinitions.Count;
            for (int i = 1; i < columnCount; i++)
            {
                GridColumnHeader header = new GridColumnHeader();
                header.Content = mColumnTitles[i - 1];
                header.RowIndex = 0;
                header.ColumnIndex = i;
                header.RowSpan = 1;
                header.ColSpan = 1;
                header.Grid = this;
                header.DataContext = header;
                header.SetBinding(ToolTipProperty, new Binding("Rect"));
                Children.Add(header);
                mGridCells.Add(
                    string.Format("{0}{1}", header.RowIndex.ToString("000"), header.ColumnIndex.ToString("000")), header);
            }
        }

        private void InitRowHeader()
        {
            int rowCount = RowDefinitions.Count;
            for (int i = 1; i < rowCount; i++)
            {
                GridRowHeader header = new GridRowHeader();
                header.Content = i;
                header.RowIndex = i;
                header.ColumnIndex = 0;
                header.RowSpan = 1;
                header.ColSpan = 1;
                header.Grid = this;
                header.DataContext = header;
                header.SetBinding(ToolTipProperty, new Binding("Rect"));
                Children.Add(header);
                mGridCells.Add(
                    string.Format("{0}{1}", header.RowIndex.ToString("000"), header.ColumnIndex.ToString("000")), header);
            }
        }

        private void InitCell()
        {
            mListSkipCells.Clear();
            InitDefinedCell();
            int rowCount = RowDefinitions.Count;
            int colCount = ColumnDefinitions.Count;
            for (int i = 1; i < rowCount; i++)
            {
                for (int j = 1; j < colCount; j++)
                {
                    string strKey = string.Format("{0:D3}{1:D3}", i, j);
                    if (mListSkipCells.Contains(strKey)) { continue; }

                    GridCell cell = new GridCell();
                    cell.RowIndex = i;
                    cell.ColumnIndex = j;
                    cell.RowSpan = 1;
                    cell.ColSpan = 1;
                    cell.Grid = this;
                    cell.DataContext = cell;
                    //cell.SetBinding(ToolTipProperty, new Binding("Rect"));

                    if (cell.Content == null)
                    {
                        //如果单元格没有内容，添加一个可编辑的文本控件
                        EditableElement textElement = new EditableElement();
                        textElement.Text = string.Empty;
                        cell.Content = textElement;
                    }

                    Children.Add(cell);
                    mGridCells.Add(
                        string.Format("{0}{1}", cell.RowIndex.ToString("000"), cell.ColumnIndex.ToString("000")), cell);
                }
            }
        }

        private void InitDefinedCell()
        {
            if (Document == null) { return; }
            var dataSets = Document.DataSets;
            var reportCells = Document.Cells;
            for (int i = 0; i < reportCells.Count; i++)
            {
                var reportCell = reportCells[i];
                int rowIndex = reportCell.RowIndex + 1;
                int colIndex = reportCell.ColIndex + 1;
                int rowSpan = reportCell.RowSpan;
                int colSpan = reportCell.ColSpan;
                for (int k = 0; k < rowSpan; k++)
                {
                    for (int l = 0; l < colSpan; l++)
                    {
                        string strKey = string.Format("{0:D3}{1:D3}", k + rowIndex, l + colIndex);
                        mListSkipCells.Add(strKey);
                    }
                }

                GridCell cell = new GridCell();
                cell.RowIndex = rowIndex;
                cell.ColumnIndex = colIndex;
                cell.RowSpan = rowSpan;
                cell.ColSpan = colSpan;
                cell.Grid = this;
                cell.DataContext = cell;
                cell.Tag = reportCell;
                //cell.SetBinding(ToolTipProperty, new Binding("Rect"));


                var reportText = reportCell.Element as ReportText;
                if (reportText != null)
                {
                    EditableElement textElement = new EditableElement();
                    textElement.Text = reportText.Text;
                    textElement.Tag = reportText;
                    cell.Content = textElement;
                }
                var reportSequence = reportCell.Element as ReportSequence;
                if (reportSequence != null)
                {
                    SequenceElement sequenceElement = new SequenceElement();
                    var dataSet = dataSets.FirstOrDefault(d => d.Name == reportSequence.DataSetName);
                    sequenceElement.DataSet = dataSet;
                    if (dataSet != null)
                    {
                        var field =
                            dataSet.Fields.FirstOrDefault(
                                f =>
                                    f.DataSet == dataSet && f.TableName == reportSequence.DataTableName &&
                                    f.Name == reportSequence.DataFieldName);
                        sequenceElement.DataField = field;
                    }
                    sequenceElement.Text = reportSequence.Expression;
                    sequenceElement.Tag = reportSequence;
                    cell.Content = sequenceElement;
                }

                if (cell.Content == null)
                {
                    //如果单元格没有内容，添加一个可编辑的文本控件
                    EditableElement element = new EditableElement();
                    element.Text = string.Empty;
                    cell.Content = element;
                }

                InitCellStyle(cell);

                Children.Add(cell);
                mGridCells.Add(
                    string.Format("{0}{1}", cell.RowIndex.ToString("000"), cell.ColumnIndex.ToString("000")), cell);
            }
        }

        private void InitCellStyle(GridCell cell)
        {
            var document = Document;
            if (document == null) { return; }
            var reportCell = cell.Tag as ReportCell;
            if (reportCell == null) { return; }
            int styleIndex = reportCell.Style;
            if (styleIndex < 0 || styleIndex >= document.Styles.Count) { return; }
            var style = document.Styles[styleIndex];
            var border = style.Border;
            if (border != null)
            {
                cell.BorderBrush = Brushes.Gray;
                cell.BorderThickness = new Thickness(border.Left, border.Top, border.Right, border.Bottom);
            }
            cell.FontFamily = new FontFamily(style.FontFamily);
            cell.FontSize = style.FontSize;
            cell.FontWeight = (style.FontStyle & (int)FontStyle.Bold) > 0
                ? FontWeights.Bold
                : FontWeights.Normal;
            cell.FontStyle = (style.FontStyle & (int)FontStyle.Italic) > 0
                ? FontStyles.Italic
                : FontStyles.Normal;
            string fontColor = style.ForeColor;
            if (!string.IsNullOrEmpty(fontColor))
            {
                var color = ColorConverter.ConvertFromString(fontColor);
                if (color != null)
                {
                    cell.Foreground = new SolidColorBrush((Color)color);
                }
            }
            string fillColor = style.BackColor;
            if (!string.IsNullOrEmpty(fillColor))
            {
                var color = ColorConverter.ConvertFromString(fillColor);
                if (color != null)
                {
                    cell.Background = new SolidColorBrush((Color)color);
                }
            }
            var reportElement = reportCell.Element;
            if (reportElement == null) { return; }
            var textElement = cell.Content as EditableElement;
            if (textElement != null)
            {
                var textBlock = textElement.TextBlock;
                if (textBlock != null)
                {
                    textBlock.TextDecorations = (style.FontStyle & (int)FontStyle.Underlined) > 0
                        ? TextDecorations.Underline
                        : null;
                }
                textElement.HAlign = (HorizontalAlignment)style.HAlign;
                textElement.VAlign = (VerticalAlignment)style.HAlign;
            }
        }

        #endregion


        #region GridSelection

        /*
         * 单元格选择中有一个问题现在还没有处理
         * 当选择的时候跨越了合并单元格，选择会有问题
         */

        private GridCell mOriginalCell;     //选择的起始单元格，即鼠标按下时所在的单元格
        private GridCell mCurrentCell;      //选择的终止单元格，即鼠标松开时所在的单元格
        private readonly GridSelection mSelection;  //选择的坐标信息（左，上，宽，高）
        private GridSelectionAdorner mSelectionAdorner;     //选择的遮罩层
        private List<GridCell> mSelectedCells = new List<GridCell>();  //选中的单元格

        public GridSelection Selection
        {
            get { return mSelection; }
        }

        void ReportGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //如果鼠标是按下状态，绘制遮罩层
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SetSelection();
                SetCellRect();
                SetHeaderSelection();
            }
        }

        void ReportGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //鼠标按下时记下当前单元格为起始单元格
            if (mCurrentCell != null)
            {
                mOriginalCell = mCurrentCell;
            }
            SetSelection();
            SetCellRect();
            SetHeaderSelection();
        }

        void ReportGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GridSelectedEventArgs args = new GridSelectedEventArgs();
            args.Grid = this;
            args.Selection = mSelection;
            for (int i = 0; i < SelectedCells.Count; i++)
            {
                args.SelectedCells.Add(SelectedCells[i]);
            }
            OnGridSelected(this, args);
        }

        private void SetSelection()
        {
            /*
             * 设置选择矩形区域
             * 根据起始单元格和终止单元格
             * 考虑跨越合并单元格的情况
             * 
             * 分两种情况，一是起始单元格与终止单元格是同一单元格，即单元格的 RowIndex 和 ColIndex 相同
             * 二是起始单元格与终止单元格不是同一单元格
             * 
             * 对于第一种情况
             * 根据ColumnIndex计算出X坐标，从第0列到ColumnIndex列的实际宽度累加即可
             * 根据RowIndex计算出Y坐标，从第0行到RowIndex行的实际高度累加即可
             * 宽度由列的宽度决定，需要考虑合并单元格的情况（考虑ColSpan）
             * 高度由行的高度决定，需要考试合并单元格的情况（考虑RowSpan）
             * 
             * 对于第二种情况
             * 可以利用单元格的坐标信息计算
             * X坐标是起始和终止两个单元格的X坐标的较小值
             * Y坐标是起始和终止两个单元格的Y坐标的较小值
             * 计算宽度和高度时首先确定两个单元格的右下角坐标（单元格的X，Y坐标加上宽度，高度即可）
             * 取右下角坐标的较大值
             * 较大值减去X，Y坐标即可得到选择区域的宽度和高度
             * 但是要考虑跨越合并单元格的情况
             */

            if (mOriginalCell == null
                    || mCurrentCell == null) { return; }
            int oriRowIndex = mOriginalCell.RowIndex;
            int oriColIndex = mOriginalCell.ColumnIndex;
            int curRowIndex = mCurrentCell.RowIndex;
            int curColIndex = mCurrentCell.ColumnIndex;
            double left = 0;
            double top = 0;
            double width = 0;
            double height = 0;
            if (oriRowIndex == curRowIndex
                && oriColIndex == curColIndex)
            {
                //通过累加得到X坐标
                for (int i = 0; i < oriColIndex; i++)
                {
                    var column = ColumnDefinitions[i];
                    left += column.ActualWidth;
                }
                //通过累加得到Y坐标
                for (int i = 0; i < oriRowIndex; i++)
                {
                    var row = RowDefinitions[i];
                    top += row.ActualHeight;
                }
                //考虑合并单元格的情况
                int rowSpan = mOriginalCell.RowSpan;
                int colSpan = mOriginalCell.ColSpan;
                for (int i = 0; i < colSpan; i++)
                {
                    width += ColumnDefinitions[oriColIndex + i].ActualWidth;
                }
                for (int i = 0; i < rowSpan; i++)
                {
                    height += RowDefinitions[oriRowIndex + i].ActualHeight;
                }
            }
            else
            {
                Rect oriRect = mOriginalCell.Rect;
                Rect curRect = mCurrentCell.Rect;
                left = Math.Min(oriRect.Left, curRect.Left);
                top = Math.Min(oriRect.Top, curRect.Top);
                Point oriPoint = new Point(oriRect.Left + oriRect.Width, oriRect.Top + oriRect.Height);
                Point curPoint = new Point(curRect.Left + curRect.Width, curRect.Top + curRect.Height);
                double maxLeft = Math.Max(oriPoint.X, curPoint.X);
                double maxTop = Math.Max(oriPoint.Y, curPoint.Y);
                width = maxLeft - left;
                height = maxTop - top;

                /*
                 * 需要考虑跨越合并单元格的情况
                 * 遍历所有单元格得到单元格的坐标信息Rect
                 * 判断Rect是否有相交，如果相交，取两个Rect的并集
                 */
                Rect rect1 = new Rect(left, top, width, height);
                for (int i = 0; i < Children.Count; i++)
                {
                    GridCell cell = Children[i] as GridCell;
                    if (cell == null) { continue; }
                    Rect rect2 = cell.Rect;
                    if (RectUtil.IntersectRect(rect1, rect2))
                    {
                        //如果相交，取并集矩形，作为最终选择的矩形区域
                        Rect merge = RectUtil.MergeRect(rect1, rect2);
                        left = merge.Left;
                        top = merge.Top;
                        width = merge.Width;
                        height = merge.Height;
                    }
                }
            }
            //选择区域的坐标信息，根据选择区域的坐标信息绘制遮罩层
            mSelection.Left = left;
            mSelection.Top = top;
            mSelection.Width = width;
            mSelection.Height = height;
            if (mSelectionAdorner == null)
            {
                mSelectionAdorner = new GridSelectionAdorner(this);
                mSelectionAdorner.Selection = mSelection;
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                adornerLayer.Add(mSelectionAdorner);
            }
            mSelectionAdorner.InvalidateVisual();       //重新绘制遮罩层
        }

        private void SetCellRect()
        {
            /*
             * 计算单元格以及行头，列头的坐标信息
             */
            int colCount = ColumnDefinitions.Count;
            int rowCount = RowDefinitions.Count;
            double left = 0.0;
            double top = 0.0;
            double width;
            double height;
            /*
             * 计算列头的坐标信息
             */
            for (int i = 0; i < colCount; i++)
            {
                var column = ColumnDefinitions[i];
                width = column.ActualWidth;
                var colHeader = mGridCells[string.Format("000{0}", i.ToString("000"))] as GridColumnHeader;
                if (colHeader != null)
                {
                    colHeader.Rect = new Rect(left, 0, width, RowDefinitions[0].ActualHeight);
                }
                left += width;
            }
            /*
             * 计算行头的坐标信息
             */
            for (int i = 0; i < rowCount; i++)
            {
                var row = RowDefinitions[i];
                height = row.ActualHeight;
                var rowHeader = mGridCells[string.Format("{0}000", i.ToString("000"))] as GridRowHeader;
                if (rowHeader != null)
                {
                    rowHeader.Rect = new Rect(0, top, ColumnDefinitions[0].ActualWidth, height);
                }
                top += height;
            }
            /*
             * 计算单元格的坐标信息
             * 单元格的坐标依赖与行头，列头的坐标，另外还要考虑合并单元格的情况
             * 首先X坐标等于对应的列头的X坐标
             * Y坐标等于对应的行头的Y坐标
             * 如果存在合并单元格，宽度和高度等于对应的所有列，行的宽度，高度之和
             */
            for (int i = 0; i < Children.Count; i++)
            {
                left = 0.0;
                top = 0.0;
                width = 0.0;
                height = 0.0;
                var cell = Children[i] as GridCell;
                if (cell != null)
                {
                    int rowIndex = cell.RowIndex;
                    int colIndex = cell.ColumnIndex;
                    int rowSpan = cell.RowSpan;
                    int colSpan = cell.ColSpan;
                    for (int k = 0; k < colSpan; k++)
                    {
                        var colHeader = mGridCells[string.Format("000{0}", (colIndex + k).ToString("000"))] as GridColumnHeader;
                        if (colHeader != null)
                        {
                            if (k == 0)
                            {
                                left = colHeader.Rect.Left;
                            }
                            width += colHeader.Rect.Width;
                        }
                    }
                    for (int k = 0; k < rowSpan; k++)
                    {
                        var rowHeader = mGridCells[string.Format("{0}000", (rowIndex + k).ToString("000"))] as GridRowHeader;
                        if (rowHeader != null)
                        {
                            if (k == 0)
                            {
                                top = rowHeader.Rect.Top;
                            }
                            height += rowHeader.Rect.Height;
                        }
                    }
                    cell.Rect = new Rect(left, top, width, height);
                }
            }
        }

        private void SetHeaderSelection()
        {
            /*
             * 通过选择区域的坐标信息确定行头，列头是否选中
             */
            int colCount = ColumnDefinitions.Count;
            int rowCount = RowDefinitions.Count;
            for (int i = 0; i < colCount; i++)
            {
                var colHeader = mGridCells[string.Format("000{0}", i.ToString("000"))] as GridColumnHeader;
                if (colHeader != null)
                {
                    //X坐标位于选择区域的Left及Width之间视为被选中，否则未被选中
                    if (mSelection.Left > 0
                        && colHeader.Rect.Left >= mSelection.Left
                        && colHeader.Rect.Left + colHeader.Rect.Width <= mSelection.Left + mSelection.Width)
                    {
                        colHeader.IsSelected = true;
                    }
                    else
                    {
                        colHeader.IsSelected = false;
                    }
                }
            }
            for (int i = 0; i < rowCount; i++)
            {
                var rowHeader = mGridCells[string.Format("{0}000", i.ToString("000"))] as GridRowHeader;
                if (rowHeader != null)
                {
                    //Y坐标位于选择区域Top和Height之间，视为选中，否则未被选中
                    if (mSelection.Top > 0
                        && rowHeader.Rect.Top >= mSelection.Top
                        && rowHeader.Rect.Top + rowHeader.Rect.Height <= mSelection.Top + mSelection.Height)
                    {
                        rowHeader.IsSelected = true;
                    }
                    else
                    {
                        rowHeader.IsSelected = false;
                    }
                }
            }
        }

        public List<GridCell> SelectedCells
        {
            get
            {
                mSelectedCells.Clear();
                if (mSelection.Left <= 0
                    && mSelection.Top <= 0
                    && mSelection.Width <= 0
                    && mSelection.Height <= 0)
                {
                    return mSelectedCells;
                }
                //获取被选中的单元格
                for (int i = 0; i < Children.Count; i++)
                {
                    var cell = Children[i] as GridCell;
                    if (cell == null) { continue; }
                    if (cell.Rect.Left >= mSelection.Left
                        && cell.Rect.Left + cell.Rect.Width <= mSelection.Left + mSelection.Width
                        && cell.Rect.Top >= mSelection.Top
                        && cell.Rect.Top + cell.Rect.Height <= mSelection.Top + mSelection.Height)
                    {
                        mSelectedCells.Add(cell);
                    }
                }
                //排序
                mSelectedCells = mSelectedCells.OrderBy(c => c.RowIndex).ThenBy(c => c.ColumnIndex).ToList();
                return mSelectedCells;
            }
        }

        private void GridCell_Mouse(object sender, RoutedPropertyChangedEventArgs<GridCellMouseEventArgs> e)
        {
            var args = e.NewValue;
            if (args == null) { return; }
            var cell = args.Source;
            if (cell == null) { return; }
            int code = args.Code;
            if (code == GridCellMouseEventArgs.EVT_MOUSE_LEFT_BUTTON_DOWN)
            {
                mCurrentCell = cell;
            }
            if (code == GridCellMouseEventArgs.EVT_MOUSE_MOVE)
            {
                var data = args.Data as MouseEventArgs;
                if (data != null)
                {
                    if (data.LeftButton == MouseButtonState.Pressed)
                    {
                        mCurrentCell = cell;
                    }
                }
            }
        }

        private void GridHeader_GridHeaderSizeChanged(object sender,
            RoutedPropertyChangedEventArgs<GridHeaderSizeChangedEventArgs> e)
        {
            SetSelection();
            SetCellRect();
            SetHeaderSelection();
        }

        private void HeadClickCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            /*
             * 通过点击行头，列头选择单元格，实际就是设置起始单元格和终止单元格，跟手动选择类似
             */
            var header = e.Parameter as GridHeader;
            if (header == null) { return; }
            GridCell originalCell = null;
            GridCell currentCell = null;
            //点击表头，所有单元格被选中
            var tableHeader = header as GridTableHeader;
            if (tableHeader != null)
            {
                int colCount = ColumnDefinitions.Count;
                int rowCount = RowDefinitions.Count;
                originalCell = mGridCells["001001"] as GridCell;
                currentCell = mGridCells[string.Format("{0:D3}{1:D3}", rowCount - 1, colCount - 1)] as GridCell;
            }
            //点击列头，当前列的所有单元格被选中
            var columnHeader = header as GridColumnHeader;
            if (columnHeader != null)
            {
                int colIndex = columnHeader.ColumnIndex;
                int rowCount = RowDefinitions.Count;
                originalCell = mGridCells[string.Format("001{0:D3}", colIndex)] as GridCell;
                currentCell = mGridCells[string.Format("{0:D3}{1:D3}", rowCount - 1, colIndex)] as GridCell;
            }
            //点击行头，当前行的所有单元格被选中
            var rowHeader = header as GridRowHeader;
            if (rowHeader != null)
            {
                int rowIndex = rowHeader.RowIndex;
                int colCount = ColumnDefinitions.Count;
                originalCell = mGridCells[string.Format("{0:D3}001", rowIndex)] as GridCell;
                currentCell = mGridCells[string.Format("{0:D3}{1:D3}", rowIndex, colCount - 1)] as GridCell;
            }
            if (originalCell != null)
            {
                mOriginalCell = originalCell;
            }
            if (currentCell != null)
            {
                mCurrentCell = currentCell;
            }
            //设置选择区域，计算坐标，并且设置行头列头选择状态
            SetSelection();
            SetCellRect();
            SetHeaderSelection();
        }

        #endregion


        #region Merge Cells

        /// <summary>
        /// 合并已选择的单元格
        /// </summary>
        public void MergeCells()
        {
            var cells = SelectedCells;
            MergeCells(cells);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="cells"></param>
        public void MergeCells(List<GridCell> cells)
        {
            int oriRowIndex = 0;
            int oriColIndex = 0;
            int rowIndex = 0;
            int colIndex = 0;
            int rowBottom = 0;//行索引与跨行的和，即单元格延伸的行索引
            int colRight = 0;//列索引与跨列的和，即单元格延伸的列索引
            object cellContent = null;
            if (cells.Count > 0)
            {
                rowIndex = cells[0].RowIndex;
                colIndex = cells[0].ColumnIndex;
                rowBottom = rowIndex + cells[0].RowSpan;
                colRight = colIndex + cells[0].ColSpan;
                //记录初始行列索引以及单元格内容
                oriRowIndex = rowIndex;
                oriColIndex = colIndex;
                cellContent = cells[0].Content;
            }
            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                if (cell.RowIndex > rowIndex)
                {
                    rowIndex = cell.RowIndex;
                    rowBottom = rowIndex + cell.RowSpan;
                }
                if (cell.ColumnIndex > colIndex)
                {
                    colIndex = cell.ColumnIndex;
                    colRight = colIndex + cell.ColSpan;
                }

                //清除原单元格
                Children.Remove(cell);
                mGridCells.Remove(string.Format("{0:D3}{1:D3}", cell.RowIndex, cell.ColumnIndex));
            }
            int rowSpan = rowBottom - oriRowIndex;
            int colSpan = colRight - oriColIndex;
            //创建新单元格
            GridCell newCell = new GridCell();
            newCell.Content = cellContent;
            newCell.RowIndex = oriRowIndex;
            newCell.ColumnIndex = oriColIndex;
            newCell.RowSpan = rowSpan;
            newCell.ColSpan = colSpan;
            newCell.Grid = this;
            newCell.DataContext = newCell;
            //newCell.SetBinding(ToolTipProperty, new Binding("Rect"));
            Children.Add(newCell);
            mGridCells.Add(string.Format("{0:D3}{1:D3}", oriRowIndex, oriColIndex), newCell);
            //重绘边框
            InitGridBorder();
        }

        #endregion


        #region Unmerge Cells

        /// <summary>
        /// 取消合并已选择的单元格
        /// </summary>
        public void UnmergeCells()
        {
            var cells = SelectedCells;
            UnmergeCells(cells);
        }

        /// <summary>
        /// 取消合并单元格
        /// </summary>
        /// <param name="cells"></param>
        public void UnmergeCells(List<GridCell> cells)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                GridCell cell = cells[i];
                UnmerageCell(cell);
            }
        }

        /// <summary>
        /// 取消合并但与昂个
        /// </summary>
        /// <param name="cell"></param>
        public void UnmerageCell(GridCell cell)
        {
            int colIndex = cell.ColumnIndex;
            int rowIndex = cell.RowIndex;
            int colSpan = cell.ColSpan;
            int rowSpan = cell.RowSpan;
            for (int i = 0; i < rowSpan; i++)
            {
                for (int j = 0; j < colSpan; j++)
                {
                    //初始单元格跳过
                    if (i == 0 && j == 0) { continue; }

                    //创建新的单元格
                    GridCell newCell = new GridCell();
                    newCell.RowIndex = rowIndex + i;
                    newCell.ColumnIndex = colIndex + j;
                    newCell.RowSpan = 1;
                    newCell.ColSpan = 1;
                    newCell.Grid = this;
                    newCell.DataContext = newCell;
                    //newCell.SetBinding(ToolTipProperty, new Binding("Rect"));

                    //添加默认可输入单元格内容
                    EditableElement textElement = new EditableElement();
                    textElement.Text = string.Empty;
                    newCell.Content = textElement;

                    Children.Add(newCell);
                    mGridCells.Add(string.Format("{0:D3}{1:D3}", newCell.RowIndex, newCell.ColumnIndex), newCell);
                }
            }
            //跨行，跨列恢复为1
            cell.RowSpan = 1;
            cell.ColSpan = 1;
            //重绘边框
            InitGridBorder();
        }

        #endregion


        #region Operations



        #endregion


        #region Others



        #endregion


        #region Debug

        public event Action<string> Debug;

        private void OnDebug(string msg)
        {
            if (Debug != null)
            {
                Debug(msg);
            }
        }

        #endregion


        #region GridSelectedEvent

        public static readonly RoutedEvent GridSelectedEvent;

        public event RoutedPropertyChangedEventHandler<GridSelectedEventArgs> GridSelected
        {
            add { AddHandler(GridSelectedEvent, value); }
            remove { RemoveHandler(GridSelectedEvent, value); }
        }

        protected void OnGridSelected(object sender, GridSelectedEventArgs e)
        {
            var grid = sender as DesignGrid;
            if (grid != null)
            {
                RoutedPropertyChangedEventArgs<GridSelectedEventArgs> args =
                    new RoutedPropertyChangedEventArgs<GridSelectedEventArgs>(default(GridSelectedEventArgs), e);
                args.RoutedEvent = GridSelectedEvent;
                grid.RaiseEvent(args);
            }
        }

        #endregion

    }
}
