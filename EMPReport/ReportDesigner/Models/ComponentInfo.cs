//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b963eebb-331a-4eb1-bcb5-e17d738a035d
//        CLR Version:              4.0.30319.42000
//        Name:                     ComponentInfo
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ComponentInfo
//
//        Created by Charley at 2017/5/12 14:32:55
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Windows.Media;
using System.Xml.Serialization;
using NetInfo.EMP.Reports;

namespace ReportDesigner.Models
{
    [XmlRoot]
    public class ComponentInfo
    {
        [XmlAttribute]
        public int ID { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// 是否内置的
        /// </summary>
        [XmlAttribute]
        public bool IsBuildIn { get; set; }

        [XmlElement]
        public string Description { get; set; }

        [XmlAttribute]
        public int GroupID { get; set; }

        [XmlElement]
        public VisualStyle Style { get; set; }

        [XmlElement]
        public ReportElement Element { get; set; }


        #region Component ID

        public const int CID_REPORT_TITLE = 101;
        public const int CID_COLUMN_TITLE = 102;
        public const int CID_HYPER_LINK = 103;
        public const int CID_REPORT_IMAGE = 104;

        public const int CID_SHAP_LINE = 201;
        public const int CID_SHAP_RECTANCEL = 202;
        public const int CID_SHAP_ELLIPSE = 203;
        public const int CID_SHAP_PATH = 204;

        public const int CID_CHAR_LINE = 301;
        public const int CID_CHAR_BAR = 302;
        public const int CID_CHAR_PIE = 303;

        #endregion


        #region Group ID

        public const int GP_BASIC = 1;
        public const int GP_SHAP = 2;
        public const int GP_CHART = 3;
        public const int GP_OTHER = 100;

        #endregion


        public static IList<ComponentInfo> InitComponentInfos()
        {
            IList<ComponentInfo> infos = new List<ComponentInfo>();
            VisualStyle style;
            ReportText reportText;
            ReportImage reportImage;

            ComponentInfo info = new ComponentInfo();
            info.ID = CID_REPORT_TITLE;
            info.Name = "报表标题";
            info.IsBuildIn = true;
            info.GroupID = GP_BASIC;
            style = new VisualStyle();
            style.FontFamily = "SimSun";
            style.FontSize = 20;
            style.FontStyle = 1;
            style.ForeColor = "#FFC0504D";
            style.HAlign = 1;
            style.VAlign = 1;
            info.Style = style;
            reportText = new ReportText();
            reportText.Text = "标题";
            info.Element = reportText;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_COLUMN_TITLE;
            info.Name = "列标题";
            info.IsBuildIn = true;
            info.GroupID = GP_BASIC;
            style = new VisualStyle();
            style.FontFamily = "SimSun";
            style.FontSize = 15;
            style.FontStyle = 1;
            style.ForeColor = "#FFFFFFFF";
            style.BackColor = "#FF00B050";
            style.HAlign = 1;
            style.VAlign = 1;
            info.Style = style;
            reportText = new ReportText();
            reportText.Text = "标题";
            info.Element = reportText;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_HYPER_LINK;
            info.Name = "超链接";
            info.IsBuildIn = true;
            info.GroupID = GP_BASIC;
            style = new VisualStyle();
            style.FontStyle = (int)FontStyle.Underlined;
            info.Style = style;
            reportText = new ReportText();
            reportText.Text = "链接";
            info.Element = reportText;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_REPORT_IMAGE;
            info.Name = "图片";
            info.IsBuildIn = true;
            info.GroupID = GP_BASIC;
            reportImage = new ReportImage();
            reportImage.Alt = "图片";
            reportImage.Stretch = (int)Stretch.Uniform;
            info.Element = reportImage;
            infos.Add(info);

            info = new ComponentInfo();
            info.ID = CID_SHAP_LINE;
            info.Name = "直线";
            info.IsBuildIn = true;
            info.GroupID = GP_SHAP;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_SHAP_RECTANCEL;
            info.Name = "矩形";
            info.IsBuildIn = true;
            info.GroupID = GP_SHAP;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_SHAP_ELLIPSE;
            info.Name = "椭圆";
            info.IsBuildIn = true;
            info.GroupID = GP_SHAP;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_SHAP_PATH;
            info.Name = "路径";
            info.IsBuildIn = true;
            info.GroupID = GP_SHAP;
            infos.Add(info);

            info = new ComponentInfo();
            info.ID = CID_CHAR_LINE;
            info.Name = "折现图";
            info.IsBuildIn = true;
            info.GroupID = GP_CHART;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_CHAR_BAR;
            info.Name = "柱状图";
            info.IsBuildIn = true;
            info.GroupID = GP_CHART;
            infos.Add(info);
            info = new ComponentInfo();
            info.ID = CID_CHAR_PIE;
            info.Name = "饼图";
            info.IsBuildIn = true;
            info.GroupID = GP_CHART;
            infos.Add(info);

            return infos;
        }

    }
}
