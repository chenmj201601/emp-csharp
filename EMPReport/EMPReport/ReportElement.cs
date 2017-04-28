//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    287325e7-11b6-44e1-a8cf-3f391749f838
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportElement
//
//        Created by Charley at 2017/4/10 15:10:11
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    [XmlInclude(typeof(ReportText))]
    [XmlInclude(typeof(ReportChart))]
    [XmlInclude(typeof(ReportSequence))]
    [XmlInclude(typeof(ReportImage))]
    public class ReportElement
    {
        [XmlIgnore]
        public ReportDocument Document { get; set; }
        [XmlAttribute]
        public int Style { get; set; }
    }
}
