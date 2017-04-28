//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    7475a8b0-bfb4-4c97-a2e2-3bbbfb201e70
//        CLR Version:              4.0.30319.42000
//        Name:                     DataSourceInfo
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DataSourceInfo
//
//        Created by Charley at 2017/4/24 18:25:25
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace ReportDesigner.Models
{
    [XmlRoot]
    public class DataSourceInfo
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlElement]
        public DatabaseInfo DBInfo { get; set; }
    }
}
