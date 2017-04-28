//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9a7f1046-35f2-4827-9321-1ed67f94d1c9
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportGrid
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportGrid
//
//        Created by Charley at 2017/4/18 14:33:47
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportGrid
    {
        [XmlAttribute]
        public int RowCount { get; set; }
        [XmlAttribute]
        public int ColCount { get; set; }
        [XmlAttribute]
        public int CellWidth { get; set; }
        [XmlAttribute]
        public int CellHeight { get; set; }
        [XmlElement]
        public string Widths { get; set; }
        [XmlElement]
        public string Heights { get; set; }
        
    }
}
