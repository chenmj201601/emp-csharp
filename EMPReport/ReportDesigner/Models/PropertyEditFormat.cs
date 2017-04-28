//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    16060bc6-b1e3-4216-9261-34cb30f11e30
//        CLR Version:              4.0.30319.42000
//        Name:                     PropertyEditFormat
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                PropertyEditFormat
//
//        Created by Charley at 2017/4/28 13:04:33
//        http://www.netinfo.com 
//
//======================================================================

namespace ReportDesigner.Models
{
    /// <summary>
    /// 属性编辑格式
    /// </summary>
    public enum PropertyEditFormat
    {
        /// <summary>
        /// 未知，直接文本显示
        /// </summary>
        Unkown = 0,
        /// <summary>
        /// 普通文本
        /// </summary>
        String = 100,
        /// <summary>
        /// 整型数值
        /// </summary>
        Int = 101,
        /// <summary>
        /// 带小数的数值
        /// </summary>
        Numeric = 102,
        /// <summary>
        /// 是否选择
        /// </summary>
        YesNo = 200,
        /// <summary>
        /// 启用禁用
        /// </summary>
        EnableDisable = 201,
        /// <summary>
        /// 下拉单选
        /// </summary>
        SingleSelect = 300,
        /// <summary>
        /// 选择颜色
        /// </summary>
        ColorSelect = 301,
    }
}
