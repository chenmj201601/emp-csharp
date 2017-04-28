//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cf70a2c7-de66-42e0-8647-9e3f2b4db1e2
//        CLR Version:              4.0.30319.42000
//        Name:                     GridHeaderSizeChangedEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridHeaderSizeChangedEventArgs
//
//        Created by Charley at 2017/4/20 12:07:50
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    public class GridHeaderSizeChangedEventArgs
    {
        /// <summary>
        /// 类型，1：改变列宽；2：改变行高
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 行或列对象
        /// </summary>
        public GridHeader Header { get; set; }
        /// <summary>
        /// 当前值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        public const int TYPE_COLUMN_WIDTH = 1;
        public const int TYPE_ROW_HEIGHT = 2;
    }
}
