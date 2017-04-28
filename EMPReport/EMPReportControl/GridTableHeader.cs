//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    527408f3-af3f-4c47-b96c-ee313b6537a0
//        CLR Version:              4.0.30319.42000
//        Name:                     GridTableHeader
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridTableHeader
//
//        Created by Charley at 2017/4/13 18:13:55
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls.Primitives;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 网格中的表头，第一行第一列
    /// </summary>
    public class GridTableHeader : GridHeader
    {
        static GridTableHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridTableHeader),
                new FrameworkPropertyMetadata(typeof(GridTableHeader)));
        }

        private const string PART_HThumb = "PART_HThumb";
        private const string PART_VThumb = "PART_VThumb";
        private Thumb mHThumb;
        private Thumb mVThumb;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mHThumb = GetTemplateChild(PART_HThumb) as Thumb;
            if (mHThumb != null)
            {
                mHThumb.DragDelta += HThumb_DragDelta;
            }
            mVThumb = GetTemplateChild(PART_VThumb) as Thumb;
            if (mVThumb != null)
            {
                mVThumb.DragDelta += VThumb_DragDelta;
            }
        }

        void HThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Grid == null) { return; }
            DesignGrid grid = Grid;
            int columnIndex = ColumnIndex;
            if (grid.ColumnDefinitions.Count < columnIndex + 1) { return; }
            var column = grid.ColumnDefinitions[columnIndex];
            var change = e.HorizontalChange;
            var old = column.ActualWidth;
            var now = old + change;
            if (now > 0)
            {
                column.Width = new GridLength(now);
            }
        }

        void VThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Grid == null) { return; }
            DesignGrid grid = Grid;
            var rowIndex = RowIndex;
            if (grid.RowDefinitions.Count < rowIndex + 1) { return; }
            var row = grid.RowDefinitions[rowIndex];
            var old = row.ActualHeight;
            var change = e.VerticalChange;
            var now = old + change;
            if (now > 0)
            {
                row.Height = new GridLength(now);
            }
        }
    }
}
