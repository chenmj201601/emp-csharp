//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b3c5d78b-bbea-4b79-ba65-895de482e8aa
//        CLR Version:              4.0.30319.42000
//        Name:                     GridColumnHeader
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridColumnHeader
//
//        Created by Charley at 2017/4/12 15:01:31
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls.Primitives;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 代表网格中的列头
    /// </summary>
    public class GridColumnHeader : GridHeader
    {
        static GridColumnHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridColumnHeader),
                new FrameworkPropertyMetadata(typeof(GridColumnHeader)));
        }

        private const string PART_Thumb = "PART_Thumb";
        private Thumb mThumb;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mThumb = GetTemplateChild(PART_Thumb) as Thumb;
            if (mThumb != null)
            {
                mThumb.DragDelta += Thumb_DragDelta;
            }
        }

        void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            //拖动调整列的宽度
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

            GridHeaderSizeChangedEventArgs args=new GridHeaderSizeChangedEventArgs();
            args.Type = GridHeaderSizeChangedEventArgs.TYPE_COLUMN_WIDTH;
            args.Header = this;
            args.Value = now;
            args.Data = e;
            OnGridHeaderSizeChanged(this, args);
        }

    }
}
