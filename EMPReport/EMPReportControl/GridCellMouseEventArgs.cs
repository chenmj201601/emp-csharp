//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6aff93f3-2d78-4be7-9d1c-fe9e20466d12
//        CLR Version:              4.0.30319.42000
//        Name:                     GridCellMouseEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridCellMouseEventArgs
//
//        Created by Charley at 2017/4/13 9:24:37
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 单元格内鼠标事件参数
    /// </summary>
    public class GridCellMouseEventArgs
    {
        /// <summary>
        /// 事件代码
        /// 101：鼠标左键按下      MouseButtonEventArgs
        /// 102：鼠标左键松开      MouseButtonEventArgs
        /// 111：鼠标移动      MouseEventArgs  
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 所在单元格
        /// </summary>
        public GridCell Source { get; set; }
        /// <summary>
        /// 事件数据，事件代码不同，数据类型也不同
        /// </summary>
        public object Data { get; set; }

        public const int EVT_MOUSE_LEFT_BUTTON_DOWN = 101;
        public const int EVT_MOUSE_LEFT_BUTTON_UP = 102;
        public const int EVT_MOUSE_MOVE = 111;
    }
}
