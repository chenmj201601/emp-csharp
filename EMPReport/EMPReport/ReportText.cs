//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    e7327580-7f17-4dce-af5f-0fb9f6adde18
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportText
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportText
//
//        Created by Charley at 2017/4/10 15:19:48
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportText : ReportElement
    {
        [XmlElement]
        public string Text { get; set; }
    }
}
