//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    093eca6d-e495-4b85-a98a-1841af6f63c6
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportParameter
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportParameter
//
//        Created by Charley at 2017/4/10 18:17:10
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace NetInfo.EMP.Reports
{
    public class ReportParameter
    {
        public string Name { get; set; }
        public int DataType { get; set; }
        public string Value { get; set; }
    }
}
