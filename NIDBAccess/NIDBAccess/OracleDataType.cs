//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    8364166b-c4a1-4aff-9433-3b954282995f
//        CLR Version:              4.0.30319.42000
//        Name:                     OracleDataType
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.DBAccesses
//        File Name:                OracleDataType
//
//        Created by Charley at 2017/4/25 11:09:16
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.DBAccesses
{
    /// <summary>
    /// Oracle数据库的数据类型
    /// </summary>
    public enum OracleDataType
    {
        /// <summary>
        /// 可变长字符型
        /// </summary>
        Varchar2,
        /// <summary>
        /// 可变长字符型
        /// </summary>
        Nvarchar2,
        /// <summary>
        /// 整型
        /// </summary>
        Int32,
        /// <summary>
        /// 固定大小字符型
        /// </summary>
        Char,
        /// <summary>
        /// 日期型
        /// </summary>
        Date,
        /// <summary>
        /// 游标指针
        /// </summary>
        RefCursor
    }
}
