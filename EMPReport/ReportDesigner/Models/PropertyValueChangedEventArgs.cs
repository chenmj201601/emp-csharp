//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    4b636850-ca62-4e08-b445-71a4c8184f73
//        CLR Version:              4.0.30319.42000
//        Name:                     PropertyValueChangedEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                PropertyValueChangedEventArgs
//
//        Created by Charley at 2017/5/3 16:26:48
//        http://www.netinfo.com 
//
//======================================================================

namespace ReportDesigner.Models
{
    /// <summary>
    /// 属性改变事件参数
    /// </summary>
    public class PropertyValueChangedEventArgs
    {
        public object ObjectInstance { get; set; }
        /// <summary>
        /// 属性项
        /// </summary>
        public ObjectPropertyInfoItem PropertyItem { get; set; }
        /// <summary>
        /// 改变后的新值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 下拉选择的项
        /// </summary>
        public PropertyValueEnumItem ValueItem { get; set; }
    }
}
