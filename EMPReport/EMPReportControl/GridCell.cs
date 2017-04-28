//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cca40041-6b77-402d-8598-cda5d5e07913
//        CLR Version:              4.0.30319.42000
//        Name:                     GridCell
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridCell
//
//        Created by Charley at 2017/4/12 14:57:43
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Input;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 代表网格中的单元格，不包括行头和列头
    /// </summary>
    public class GridCell : CellBase
    {
        static GridCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridCell), new FrameworkPropertyMetadata(typeof(GridCell)));

            CellMouseEvent = EventManager.RegisterRoutedEvent("CellMouse", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<GridCellMouseEventArgs>), typeof(GridCell));
        }

        public GridCell()
        {
            MouseLeftButtonDown += GridCell_MouseLeftButtonDown;
            MouseMove += GridCell_MouseMove;
        }

        void GridCell_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridCellMouseEventArgs args = new GridCellMouseEventArgs();
            args.Code = GridCellMouseEventArgs.EVT_MOUSE_LEFT_BUTTON_DOWN;
            args.Source = this;
            args.Data = e;
            OnCellMouse(this, args);
        }

        void GridCell_MouseMove(object sender, MouseEventArgs e)
        {
            GridCellMouseEventArgs args = new GridCellMouseEventArgs();
            args.Code = GridCellMouseEventArgs.EVT_MOUSE_MOVE;
            args.Source = this;
            args.Data = e;
            OnCellMouse(this, args);
        }

        public static readonly DependencyProperty SelectedProperty =
            DependencyProperty.Register("Selected", typeof(bool), typeof(GridCell), new PropertyMetadata(default(bool)));

        public bool Selected
        {
            get { return (bool)GetValue(SelectedProperty); }
            set { SetValue(SelectedProperty, value); }
        }

        /// <summary>
        /// 单元格的鼠标事件，可以将鼠标事件报告给网格，其中附带当前单元格信息
        /// </summary>
        public static readonly RoutedEvent CellMouseEvent;

        public event RoutedPropertyChangedEventHandler<GridCellMouseEventArgs> CellMouse
        {
            add { AddHandler(CellMouseEvent, value); }
            remove { RemoveHandler(CellMouseEvent, value); }
        }

        protected void OnCellMouse(object sender, GridCellMouseEventArgs e)
        {
            var cell = sender as GridCell;
            if (cell != null)
            {
                RoutedPropertyChangedEventArgs<GridCellMouseEventArgs> args =
                    new RoutedPropertyChangedEventArgs<GridCellMouseEventArgs>(default(GridCellMouseEventArgs), e);
                args.RoutedEvent = CellMouseEvent;
                cell.RaiseEvent(args);
            }
        }

    }
}
