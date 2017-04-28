//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6babf47f-8a51-458b-8a9b-6ee96ede0364
//        CLR Version:              4.0.30319.42000
//        Name:                     MssqlDataType
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.DBAccesses
//        File Name:                MssqlDataType
//
//        Created by Charley at 2017/4/25 11:04:21
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.DBAccesses
{
    /// <summary>
    /// Sql Server数据库的数据类型
    /// </summary>
    public enum MssqlDataType
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
