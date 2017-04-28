//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    83578c96-5f98-4965-bc3b-f3d9a1decf43
//        CLR Version:              4.0.30319.42000
//        Name:                     DesignerConfig
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DesignerConfig
//
//        Created by Charley at 2017/4/19 15:02:36
//        http://www.netinfo.com 
//
//======================================================================

using System.Xml.Serialization;


namespace ReportDesigner.Models
{
    [XmlRoot]
    public class DesignerConfig
    {
        public const string FILE_NAME = "config.xml";

        public string DataDir { get; set; }
        public string PublishDir { get; set; }
        public string PreviewServer { get; set; }
        public int PreviewPort { get; set; }
    }
}
