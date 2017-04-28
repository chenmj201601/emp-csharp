//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1828ae93-6ce5-4357-84d0-0eded675c8e5
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportDataTable
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportDataTable
//
//        Created by Charley at 2017/4/27 14:56:28
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportDataTable
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Display { get; set; }

        [XmlIgnore]
        public ReportDataSet DataSet { get; set; }
    }
}
