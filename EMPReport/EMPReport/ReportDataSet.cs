//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9fa4c7ab-c6e6-46e7-996d-365a6de7f38b
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportDataSet
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportDataSet
//
//        Created by Charley at 2017/4/27 14:55:59
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportDataSet
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string DataSourceName { get; set; }

        private readonly List<ReportDataTable> mTables = new List<ReportDataTable>();

        [XmlArray(ElementName = "Tables")]
        [XmlArrayItem(ElementName = "Table")]
        public List<ReportDataTable> Tables
        {
            get { return mTables; }
        }

        private readonly List<ReportDataField> mFields = new List<ReportDataField>();

        [XmlArray(ElementName = "Fields")]
        [XmlArrayItem(ElementName = "Field")]
        public List<ReportDataField> Fields
        {
            get { return mFields; }
        }

        public void Init()
        {
            for (int i = 0; i < Tables.Count; i++)
            {
                var table = Tables[i];
                table.DataSet = this;
            }
            for (int i = 0; i < Fields.Count; i++)
            {
                var field = Fields[i];
                string strTable = field.TableName;
                var table = Tables.FirstOrDefault(t => t.Name == strTable);
                field.DataSet = this;
                field.Table = table;
            }
        }
    }
}
