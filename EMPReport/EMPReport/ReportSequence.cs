//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b90d2500-8205-440d-b480-b9847b8aaa91
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportSequence
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportSequence
//
//        Created by Charley at 2017/4/18 15:32:27
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportSequence : ReportElement
    {
        [XmlAttribute]
        public string DataSetName { get; set; }
        [XmlAttribute]
        public string DataTableName { get; set; }
        [XmlAttribute]
        public string DataFieldName { get; set; }
        [XmlAttribute]
        public int ExtMethod { get; set; }
        [XmlAttribute]
        public int IsMerge { get; set; }
        [XmlElement]
        public string Expression { get; set; }
    }
}
