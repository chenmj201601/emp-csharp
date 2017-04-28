//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    937c0b8a-e002-4a0a-b391-ceefc314e15f
//        CLR Version:              4.0.30319.42000
//        Name:                     GridSelectionAdorner
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridSelectionAdorner
//
//        Created by Charley at 2017/4/12 17:11:43
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 网格选择的遮罩层
    /// </summary>
    public class GridSelectionAdorner : Adorner
    {
        private FrameworkElement mElement;
        private GridSelection mSelection;
        public GridSelectionAdorner(FrameworkElement element)
            : base(element)
        {
            mElement = element;
            //屏蔽命中测试
            IsHitTestVisible = false;
        }

        public GridSelection Selection
        {
            get { return mSelection; }
            set { mSelection = value; }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = new Pen(Brushes.RoyalBlue, 2);
            pen.DashStyle = new DashStyle();
            Rect rect = new Rect(mSelection.Left, mSelection.Top, mSelection.Width, mSelection.Height);
            Brush brush = new SolidColorBrush(Color.FromArgb(20, 0, 0, 255));
            drawingContext.DrawRectangle(brush, pen, rect);
            //drawingContext.DrawRectangle(null, pen, rect);
            base.OnRender(drawingContext);
        }

    }
}
