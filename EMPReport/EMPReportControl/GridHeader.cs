//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    d898dab9-d32f-4f6f-8be6-a3fdb60cd2dd
//        CLR Version:              4.0.30319.42000
//        Name:                     GridHeader
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridHeader
//
//        Created by Charley at 2017/4/12 15:16:41
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Input;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 网格中的行头或列头或表头
    /// </summary>
    public class GridHeader : CellBase
    {
        static GridHeader()
        {
            GridHeaderSizeChangedEvent = EventManager.RegisterRoutedEvent("GridHeaderSizeChanged",
                RoutingStrategy.Bubble, typeof (RoutedPropertyChangedEventHandler<GridHeaderSizeChangedEventArgs>),
                typeof (GridHeader));
        }

        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register("IsSelected", typeof(bool), typeof(GridHeader), new PropertyMetadata(default(bool)));

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        private static readonly RoutedUICommand mHeadClickCommand = new RoutedUICommand();

        /// <summary>
        /// 表头（或行头，列头）的点击命令，用于选择整行，整列，整个表格
        /// </summary>
        public static RoutedUICommand HeadClickCommand
        {
            get { return mHeadClickCommand; }
        }

        public static readonly RoutedEvent GridHeaderSizeChangedEvent;

        public event RoutedPropertyChangedEventHandler<GridHeaderSizeChangedEventArgs> GridHeaderSizeChanged
        {
            add { AddHandler(GridHeaderSizeChangedEvent, value); }
            remove { RemoveHandler(GridHeaderSizeChangedEvent, value); }
        }

        protected void OnGridHeaderSizeChanged(object sender, GridHeaderSizeChangedEventArgs e)
        {
            var header = sender as GridHeader;
            if (header != null)
            {
                RoutedPropertyChangedEventArgs<GridHeaderSizeChangedEventArgs> args =
                    new RoutedPropertyChangedEventArgs<GridHeaderSizeChangedEventArgs>(
                        default(GridHeaderSizeChangedEventArgs), e);
                args.RoutedEvent = GridHeaderSizeChangedEvent;
                header.RaiseEvent(args);
            }
        }
    }
}
