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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportSequence : ReportElement
    {
        [XmlElement]
        public string Expression { get; set; }
    }
}
