//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    a783b174-50ce-4ce9-967c-3cdf2b74938a
//        CLR Version:              4.0.30319.42000
//        Name:                     CellFormatType
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                CellFormatType
//
//        Created by Charley at 2017/6/2 17:10:02
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports
{
    /// <summary>
    /// 单元格数据类型
    /// </summary>
    public enum CellFormatType
    {
        /// <summary>
        /// 默认
        /// </summary>
        None = 0,
        /// <summary>
        /// 数值
        /// </summary>
        Numeric = 1,
        /// <summary>
        /// 文本
        /// </summary>
        Text = 10,
    }
}
