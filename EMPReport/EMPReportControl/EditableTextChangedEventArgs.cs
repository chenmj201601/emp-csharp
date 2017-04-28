//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    e2f76224-e864-4b7d-8e43-adeac9bf41b1
//        CLR Version:              4.0.30319.42000
//        Name:                     EditableTextChangedEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                EditableTextChangedEventArgs
//
//        Created by Charley at 2017/4/25 15:44:14
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Controls;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 可编辑文本框文本变化事件参数
    /// </summary>
    public class EditableTextChangedEventArgs
    {
        /// <summary>
        /// 可编辑的文本框
        /// </summary>
        public EditableElement EditableTextBlock { get; set; }
        /// <summary>
        /// 实际编辑文本框
        /// </summary>
        public TextBox EditableTextBox { get; set; }
        /// <summary>
        /// 当前文本值
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 事件原始数据
        /// </summary>
        public object Data { get; set; }
    }
}
