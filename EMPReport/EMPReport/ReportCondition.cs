//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b44ee50a-f296-489b-8dc5-76c416657e7f
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportCondition
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportCondition
//
//        Created by Charley at 2017/5/31 10:47:17
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportCondition
    {
        /// <summary>
        /// 字段，如果各个表中有相同字段名，用 DataTableName_DataFieldName 表示
        /// </summary>
        [XmlAttribute]
        public string Field { get; set; }
        /// <summary>
        /// 比较类型，等于，大于，小于等
        /// </summary>
        [XmlAttribute]
        public int Judge { get; set; }
        /// <summary>
        /// 关系类型，与，或 等
        /// </summary>
        [XmlAttribute]
        public int Relation { get; set; }
        /// <summary>
        /// 值，如果是字段值，用花括号表示法，如 {FieldName}
        /// </summary>
        [XmlElement]
        public string Value { get; set; }

        [XmlIgnore]
        public ReportDataSet DataSet { get; set; }
    }
}
