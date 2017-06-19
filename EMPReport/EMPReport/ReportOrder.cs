//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    da977c74-0382-4a19-aa6a-0b3e826f0af0
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportOrder
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportOrder
//
//        Created by Charley at 2017/6/1 15:32:24
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportOrder
    {
        /// <summary>
        /// 字段，如果各个表中有相同字段名，用 DataTableName_DataFieldName 表示
        /// </summary>
        [XmlAttribute]
        public string Field { get; set; }
        [XmlAttribute]
        public int Direction { get; set; }

        [XmlIgnore]
        public ReportDataSet DataSet { get; set; }
    }
}
