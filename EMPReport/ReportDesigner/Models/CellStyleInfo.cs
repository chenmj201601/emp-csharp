//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3d389cd6-701a-4c2b-845d-4e45281be15b
//        CLR Version:              4.0.30319.42000
//        Name:                     CellStyleInfo
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                CellStyleInfo
//
//        Created by Charley at 2017/5/24 14:39:05
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Xml.Serialization;
using NetInfo.EMP.Reports;


namespace ReportDesigner.Models
{
    [XmlRoot]
    public class CellStyleInfo
    {
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 是否内置的
        /// </summary>
        [XmlAttribute]
        public bool IsBuiltIn { get; set; }
        [XmlElement]
        public VisualStyle Style { get; set; }

        public static IList<CellStyleInfo> InitCellStyles()
        {
            List<CellStyleInfo> cellStyles = new List<CellStyleInfo>();
            VisualStyle style = new VisualStyle();
            style.FontFamily = "SimSun";
            style.FontSize = 20;
            style.FontStyle = 1;
            style.ForeColor = "#FFC0504D";
            style.HAlign = 1;
            style.VAlign = 1;
            CellStyleInfo cellStyle = new CellStyleInfo();
            cellStyle.Name = "标题 1";
            cellStyle.IsBuiltIn = true;
            cellStyle.Style = style;
            cellStyles.Add(cellStyle);
            style = new VisualStyle();
            style.FontFamily = "SimSun";
            style.FontSize = 15;
            style.FontStyle = 1;
            style.ForeColor = "#FF000000";
            style.HAlign = 1;
            style.VAlign = 1;
            cellStyle = new CellStyleInfo();
            cellStyle.Name = "列头 1";
            cellStyle.IsBuiltIn = true;
            cellStyle.Style = style;
            cellStyles.Add(cellStyle);
            style = new VisualStyle();
            style.FontFamily = "SimSun";
            style.FontSize = 12;
            style.FontStyle = 0;
            style.ForeColor = "#FF000000";
            style.HAlign = 0;
            style.VAlign = 1;
            cellStyle = new CellStyleInfo();
            cellStyle.Name = "正文 1";
            cellStyle.IsBuiltIn = true;
            cellStyle.Style = style;
            cellStyles.Add(cellStyle);
            return cellStyles;
        }
    }
}
