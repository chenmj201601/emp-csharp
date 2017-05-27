//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    d216dd60-2430-46a5-bab3-ebf051c76f97
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportImage
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportImage
//
//        Created by Charley at 2017/4/18 15:31:15
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.IO;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportImage : ReportElement
    {
        /// <summary>
        /// ID，GUID串
        /// </summary>
        [XmlAttribute]
        public string ID { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        [XmlAttribute]
        public int Width { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [XmlAttribute]
        public int Height { get; set; }
        /// <summary>
        /// 拉伸模式
        /// </summary>
        [XmlAttribute]
        public int Stretch { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        [XmlAttribute]
        public int Extension { get; set; }
        /// <summary>
        /// 替换文本
        /// </summary>
        [XmlElement]
        public string Alt { get; set; }


        #region 扩展名

        public const int EXT_PNG = 1;
        public const int EXT_BMP = 2;
        public const int EXT_JPG = 3;
        public const int EXT_JPEG = 4;

        #endregion

    }
}
