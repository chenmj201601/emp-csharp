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
        /// <summary>
        /// 查询语句，可以自定义查询语句
        /// 如果Sql不为空，报表引擎将直接使用此sql语句查询数据
        /// </summary>
        [XmlElement]
        public string Sql { get; set; }
        /// <summary>
        /// 是否自定义Sql查询语句
        /// </summary>
        [XmlAttribute]
        public int IsSqlCustomized { get; set; }

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

        private readonly List<ReportCondition> mConditions = new List<ReportCondition>();

        [XmlArray(ElementName = "Conditions")]
        [XmlArrayItem(ElementName = "Condition")]
        public List<ReportCondition> Conditions
        {
            get { return mConditions; }
        }

        private readonly List<ReportOrder> mOrders = new List<ReportOrder>();

        [XmlArray(ElementName = "Orders")]
        [XmlArrayItem(ElementName = "Order")]
        public List<ReportOrder> Orders
        {
            get { return mOrders; }
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
            for (int i = 0; i < Conditions.Count; i++)
            {
                var condition = Conditions[i];
                condition.DataSet = this;
            }
        }
    }
}
