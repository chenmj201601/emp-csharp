//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    396a5dc6-9b1a-4cf7-ae58-b1b0ccb3e6ca
//        CLR Version:              4.0.30319.42000
//        Name:                     DataSourceConfig
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DataSourceConfig
//
//        Created by Charley at 2017/4/19 15:04:46
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Xml.Serialization;


namespace ReportDesigner.Models
{
    [XmlRoot]
    public class DataSourceConfig
    {
        public const string FILE_NAME = "datasource.xml";

        private List<DataSourceInfo> mDataSources = new List<DataSourceInfo>();

        [XmlArray(ElementName = "DataSources")]
        [XmlArrayItem(ElementName = "DataSource")]
        public List<DataSourceInfo> DataSources
        {
            get { return mDataSources; }
        }
    }
}
