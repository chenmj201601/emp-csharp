//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9ab6a35e-231f-4b16-9957-ce3b107ba125
//        CLR Version:              4.0.30319.42000
//        Name:                     VisualStyle
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                VisualStyle
//
//        Created by Charley at 2017/4/10 15:11:29
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class VisualStyle
    {
        [XmlAttribute]
        public string FontFamily { get; set; }
        [XmlAttribute]
        public int FontSize { get; set; }
        /// <summary>
        /// 字体样式
        /// </summary>
        [XmlAttribute]
        public int FontStyle { get; set; }
        [XmlAttribute]
        public string ForeColor { get; set; }
        [XmlAttribute]
        public string BackColor { get; set; }
        [XmlAttribute]
        public int Width { get; set; }
        [XmlAttribute]
        public int Height { get; set; }
        [XmlAttribute]
        public int HAlign { get; set; }
        [XmlAttribute]
        public int VAlign { get; set; }
        [XmlElement]
        public ReportBorder Border { get; set; }
        [XmlElement]
        public ReportPadding Padding { get; set; }

        [XmlAttribute]
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}_{8}_{9}_{10}",
                    FontFamily,
                    FontSize,
                    FontStyle,
                    ForeColor,
                    BackColor,
                    Width,
                    Height,
                    HAlign,
                    VAlign,
                    Border == null ? "" : Border.Key,
                    Padding == null ? "" : Padding.Key);
            }
        }
    }

    /// <summary>
    /// 字体类型
    /// </summary>
    [Flags]
    public enum FontStyle
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 加粗
        /// </summary>
        Bold = 1,
        /// <summary>
        /// 倾斜
        /// </summary>
        Italic = 2,
        /// <summary>
        /// 带下划线
        /// </summary>
        Underlined = 4
    }
}
