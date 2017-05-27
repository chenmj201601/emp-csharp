//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b833631e-3017-4241-a8fa-0aede2074ce8
//        CLR Version:              4.0.30319.42000
//        Name:                     CellStyleConfig
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                CellStyleConfig
//
//        Created by Charley at 2017/5/27 11:28:52
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace ReportDesigner.Models
{
    [XmlRoot]
    public class CellStyleConfig
    {
        public const string FILE_NAME = "cellstyles.xml";

        private readonly List<CellStyleInfo> mCellStyles = new List<CellStyleInfo>();

        [XmlArray(ElementName = "CellStyles")]
        [XmlArrayItem(ElementName = "CellStyle")]
        public List<CellStyleInfo> CellStyles
        {
            get { return mCellStyles; }
        }
    }
}
