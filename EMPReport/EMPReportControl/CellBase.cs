//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    467977a6-074b-492c-b28f-33bbc9a2d481
//        CLR Version:              4.0.30319.42000
//        Name:                     CellBase
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                CellBase
//
//        Created by Charley at 2017/4/14 9:26:36
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 代表网格中的单元格，也可能是行头或列头
    /// </summary>
    public class CellBase : ContentControl, IGridCell
    {
        public static readonly DependencyProperty CellNameProperty =
            DependencyProperty.Register("CellName", typeof(string), typeof(CellBase), new PropertyMetadata(default(string)));

        public string CellName
        {
            get { return (string)GetValue(CellNameProperty); }
            set { SetValue(CellNameProperty, value); }
        }

        public static readonly DependencyProperty CellKeyProperty =
            DependencyProperty.Register("CellKey", typeof(string), typeof(CellBase), new PropertyMetadata(default(string)));

        public string CellKey
        {
            get { return (string)GetValue(CellKeyProperty); }
            set { SetValue(CellKeyProperty, value); }
        }

        public static readonly DependencyProperty RowIndexProperty =
            DependencyProperty.Register("RowIndex", typeof(int), typeof(CellBase), new PropertyMetadata(default(int)));

        public int RowIndex
        {
            get { return (int)GetValue(RowIndexProperty); }
            set { SetValue(RowIndexProperty, value); }
        }

        public static readonly DependencyProperty ColumnIndexProperty =
            DependencyProperty.Register("ColumnIndex", typeof(int), typeof(CellBase), new PropertyMetadata(default(int)));

        public int ColumnIndex
        {
            get { return (int)GetValue(ColumnIndexProperty); }
            set { SetValue(ColumnIndexProperty, value); }
        }

        public static readonly DependencyProperty ColSpanProperty =
            DependencyProperty.Register("ColSpan", typeof(int), typeof(CellBase), new PropertyMetadata(default(int)));

        public int ColSpan
        {
            get { return (int)GetValue(ColSpanProperty); }
            set { SetValue(ColSpanProperty, value); }
        }

        public static readonly DependencyProperty RowSpanProperty =
            DependencyProperty.Register("RowSpan", typeof(int), typeof(CellBase), new PropertyMetadata(default(int)));

        public int RowSpan
        {
            get { return (int)GetValue(RowSpanProperty); }
            set { SetValue(RowSpanProperty, value); }
        }

        public static readonly DependencyProperty RectProperty =
            DependencyProperty.Register("Rect", typeof(Rect), typeof(CellBase), new PropertyMetadata(default(Rect)));

        public Rect Rect
        {
            get { return (Rect)GetValue(RectProperty); }
            set { SetValue(RectProperty, value); }
        }

        public DesignGrid Grid { get; set; }
    }
}
