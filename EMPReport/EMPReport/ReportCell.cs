﻿//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f89553ae-260d-4c9a-818a-a2ed97af7f8d
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportCell
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportCell
//
//        Created by Charley at 2017/4/18 14:42:34
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportCell
    {
        /// <summary>
        /// 单元格的标识，由行，列号组成，格式为{0:D03}{1:D03}
        /// </summary>
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public int RowIndex { get; set; }
        [XmlAttribute]
        public int ColIndex { get; set; }
        [XmlAttribute]
        public int RowSpan { get; set; }
        [XmlAttribute]
        public int ColSpan { get; set; }
        [XmlAttribute]
        public int Style { get; set; }
        [XmlAttribute]
        public string LinkUrl { get; set; }
        [XmlAttribute]
        public int ExtDirection { get; set; }
        [XmlAttribute]
        public string LeftParent { get; set; }
        [XmlAttribute]
        public string TopParent { get; set; }
        [XmlAttribute]
        public int FormatType { get; set; }
        [XmlAttribute]
        public string FormatString { get; set; }
        [XmlElement]
        public ReportElement Element { get; set; }
    }
}
