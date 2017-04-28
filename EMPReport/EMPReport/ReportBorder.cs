//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b16c12d8-c0ca-4286-b2f4-1c0df2aeea01
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportBorder
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportBorder
//
//        Created by Charley at 2017/4/21 15:46:11
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportBorder
    {
        [XmlAttribute]
        public int Left { get; set; }
        [XmlAttribute]
        public string LeftColor { get; set; }
        [XmlAttribute]
        public int Top { get; set; }
        [XmlAttribute]
        public string TopColor { get; set; }
        [XmlAttribute]
        public int Right { get; set; }
        [XmlAttribute]
        public string RightColor { get; set; }
        [XmlAttribute]
        public int Bottom { get; set; }
        [XmlAttribute]
        public string BottomColor { get; set; }

        [XmlAttribute]
        public string Key
        {
            get
            {
                return string.Format("{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}",
                    Left,
                    Top,
                    Right,
                    Bottom,
                    LeftColor,
                    TopColor,
                    RightColor,
                    BottomColor);
            }
        }
    }
}
