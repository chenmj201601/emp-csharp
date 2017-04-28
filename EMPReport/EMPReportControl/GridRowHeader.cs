//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    a0b8df8c-ff57-44bd-a415-c53c5a329809
//        CLR Version:              4.0.30319.42000
//        Name:                     GridRowHeader
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridRowHeader
//
//        Created by Charley at 2017/4/12 15:17:49
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls.Primitives;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 网格中的行头
    /// </summary>
    public class GridRowHeader : GridHeader
    {
        static GridRowHeader()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridRowHeader),
                new FrameworkPropertyMetadata(typeof(GridRowHeader)));
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
            //拖动调整行的高度
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

            GridHeaderSizeChangedEventArgs args = new GridHeaderSizeChangedEventArgs();
            args.Type = GridHeaderSizeChangedEventArgs.TYPE_ROW_HEIGHT;
            args.Header = this;
            args.Value = now;
            args.Data = e;
            OnGridHeaderSizeChanged(this, args);
        }
    }
}
