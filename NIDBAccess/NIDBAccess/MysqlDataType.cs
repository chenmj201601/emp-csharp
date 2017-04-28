//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3cef25ff-9840-4b41-9184-9670793355f0
//        CLR Version:              4.0.30319.42000
//        Name:                     MysqlDataType
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.DBAccesses
//        File Name:                MysqlDataType
//
//        Created by Charley at 2017/4/25 11:07:32
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.DBAccesses
{
    /// <summary>
    /// MySql数据类型
    /// </summary>
    public enum MysqlDataType
    {
        /// <summary>
        /// 可变长字符型
        /// </summary>
        Varchar,
        /// <summary>
        /// 可变长字符型
        /// </summary>
        NVarchar,
        /// <summary>
        /// 固定大小字符型
        /// </summary>
        Char,
        /// <summary>
        /// 整型
        /// </summary>
        Int,
        /// <summary>
        /// 长整型
        /// </summary>
        Bigint
    }
}
