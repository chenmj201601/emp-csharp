﻿//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    7c76770a-6991-4dc4-b71f-a3491315221d
//        CLR Version:              4.0.30319.42000
//        Name:                     EditableTextChangedEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                EditableTextChangedEventArgs
//
//        Created by Charley at 2017/4/21 15:01:25
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Controls;


namespace NetInfo.Wpf.Controls
{
    /// <summary>
    /// 可编辑文本框文本变化事件参数
    /// </summary>
    public class EditableTextChangedEventArgs
    {
        /// <summary>
        /// 可编辑的文本框
        /// </summary>
        public EditableTextBlock EditableTextBlock { get; set; }
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
