//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cef1ebe7-34bf-4c57-ae7a-25fedd1a29cc
//        CLR Version:              4.0.30319.42000
//        Name:                     ComponentConfig
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ComponentConfig
//
//        Created by Charley at 2017/5/17 15:50:16
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Xml.Serialization;


namespace ReportDesigner.Models
{
    [XmlRoot]
    public class ComponentConfig
    {
        public const string FILE_NAME = "components.xml";

        private List<ComponentInfo> mListComponents = new List<ComponentInfo>();

        [XmlArray(ElementName = "Components")]
        [XmlArrayItem(ElementName = "Component")]
        public List<ComponentInfo> ListComponents
        {
            get { return mListComponents; }
        }
    }
}
