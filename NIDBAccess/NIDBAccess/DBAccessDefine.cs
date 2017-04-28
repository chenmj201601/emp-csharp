//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    41f0a964-598a-48b5-9d20-d1d9d48f6cc7
//        CLR Version:              4.0.30319.42000
//        Name:                     DBAccessDefine
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.DBAccesses
//        File Name:                DBAccessDefine
//
//        Created by Charley at 2017/4/25 11:03:04
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.DBAccesses
{
    /// <summary>
    /// 相关常量定义
    /// </summary>
    public class DBAccessDefine
    {

        #region 错误号定义

        /// <summary>
        /// 表不存在
        /// </summary>
        public const int ERR_TABLE_NOT_EXIST = 1001;

        #endregion


        #region MySQL错误号

        internal const int MYSQL_ERR_OBJECT_NOT_EXIST = 208;

        #endregion


        #region MSSQL 错误号

        internal const int MSSQL_ERR_OBJECT_NOT_EXIST = 208;  //表不存在

        #endregion


        #region ORCL 错误号

        internal const int ORCL_ERR_OBJECT_NOT_EXIST = 942;     //表不存在

        #endregion
    }
}
