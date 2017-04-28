//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    4cd4ecbb-f4dc-4226-93ea-dac33f3e96d1
//        CLR Version:              4.0.30319.42000
//        Name:                     ObjectPropertyInfo
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ObjectPropertyInfo
//
//        Created by Charley at 2017/4/28 12:52:55
//        http://www.netinfo.com 
//
//======================================================================

namespace ReportDesigner.Models
{
    public class ObjectPropertyInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int GroupID { get; set; }
        public int SortID { get; set; }
        public PropertyEditFormat EditFormat { get; set; }
        public string DefaultValue { get; set; }
    }
}
