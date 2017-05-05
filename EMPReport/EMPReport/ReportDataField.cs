//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5d291b2e-c446-4dd4-856e-f3e06b33149a
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportDataField
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportDataField
//
//        Created by Charley at 2017/4/27 14:56:51
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportDataField
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Display { get; set; }
        [XmlAttribute]
        public int DataType { get; set; }
        [XmlAttribute]
        public string TableName { get; set; }

        [XmlIgnore]
        public ReportDataTable Table { get; set; }
        [XmlIgnore]
        public ReportDataSet DataSet { get; set; }
    }
}
