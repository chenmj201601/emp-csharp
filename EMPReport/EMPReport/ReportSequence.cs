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
        /// <summary>
        /// 数据操作方式，分组，列表，汇总等
        /// </summary>
        [XmlAttribute]
        public int DataOptMethod { get; set; }
        /// <summary>
        /// 分组模式，只有 DataOptMethod 为 Group 时才有效，普通模式，相邻连续等
        /// </summary>
        [XmlAttribute]
        public int GroupMode { get; set; }
        /// <summary>
        /// 汇总模式，只有 DataOptMethod 为 Collect 是才有效，求和，求平均值，求最大，最小值等
        /// </summary>
        [XmlAttribute]
        public int CollectMode { get; set; }
        [XmlElement]
        public string Expression { get; set; }

        public const int GROUP_MODE_TRADITIONAL = 0;
        public const int GROUP_MODE_ADJACENT_CONTINUE = 1;

        public const int COLLECT_MODE_SUM = 0;
        public const int COLLECT_MODE_AVG = 1;
        public const int COLLECT_MODE_MAX = 2;
        public const int COLLECT_MODE_MIN = 3;
    }
}
