//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    512fefab-30a5-4317-a40c-d9d7cce72497
//        CLR Version:              4.0.30319.42000
//        Name:                     DataOperationMethod
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                DataOperationMethod
//
//        Created by Charley at 2017/6/2 17:16:16
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports
{
    /// <summary>
    /// 数据操作方式
    /// </summary>
    public enum DataOperationMethod
    {
        /// <summary>
        /// 分组
        /// </summary>
        Group = 0,
        /// <summary>
        /// 列表
        /// </summary>
        List = 1,
        /// <summary>
        /// 汇总
        /// </summary>
        Collect = 2,
    }
}
