//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6c38d60c-fd6b-4158-b47b-ca242e7b0cd3
//        CLR Version:              4.0.30319.42000
//        Name:                     DBDataType
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                DBDataType
//
//        Created by Charley at 2017/4/27 15:01:18
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports
{
    public enum DBDataType
    {
        Unkown=0,
        /// <summary>
        /// 短整型
        /// </summary>
        Short = 1,
        /// <summary>
        /// 整形
        /// </summary>
        Int = 2,
        /// <summary>
        /// 长整形
        /// </summary>
        Long = 3,
        /// <summary>
        /// 数值
        /// </summary>
        Number = 4,
        /// <summary>
        /// 单个字符
        /// </summary>
        Char = 11,
        /// <summary>
        /// 单个字符
        /// </summary>
        NChar = 12,
        /// <summary>
        /// 可变长字符
        /// </summary>
        Varchar = 13,
        /// <summary>
        /// 可变长字符
        /// </summary>
        NVarchar = 14,
        /// <summary>
        /// 日期时间
        /// </summary>
        Datetime = 21
    }
}
