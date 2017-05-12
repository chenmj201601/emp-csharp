//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b01c7d3a-e74d-4bf5-83cd-cdd8358a63e6
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportPadding
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportPadding
//
//        Created by Charley at 2017/5/11 15:00:52
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
    public class ReportPadding
    {
        [XmlAttribute]
        public int Left { get; set; }
        [XmlAttribute]
        public int Top { get; set; }
        [XmlAttribute]
        public int Right { get; set; }
        [XmlAttribute]
        public int Bottom { get; set; }

        [XmlAttribute]
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}_{2}_{3}",
                    Left,
                    Top,
                    Right,
                    Bottom);
            }
        }
    }
}
