//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    a9f7eda6-13ed-4bd6-8bad-8721dba3746c
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportPropertyFactory
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ReportPropertyFactory
//
//        Created by Charley at 2017/5/16 9:51:44
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Linq;
using NetInfo.EMP.Reports.Controls;


namespace ReportDesigner.Models
{
    public class ReportPropertyFactory
    {
        public static IList<ObjectPropertyInfo> GetProperties(ICellElement cellElement)
        {
            IList<ObjectPropertyInfo> listProperties = new List<ObjectPropertyInfo>();
            ObjectPropertyInfo info = new ObjectPropertyInfo();
            info.ID = PRO_FONTFAMILY;
            info.Name = "字体";
            info.GroupID = GP_FONT;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "SimSun";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FONTSIZE;
            info.Name = "字号";
            info.GroupID = GP_FONT;
            info.SortID = 3;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "11";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FONTSTYLE_BOLD;
            info.Name = "加粗";
            info.GroupID = GP_FONT;
            info.SortID = 4;
            info.EditFormat = PropertyEditFormat.YesNo;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FONTSTYLE_ITALIC;
            info.Name = "倾斜";
            info.GroupID = GP_FONT;
            info.SortID = 5;
            info.EditFormat = PropertyEditFormat.YesNo;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FONTSTYLE_UNDERLINE;
            info.Name = "下划线";
            info.GroupID = GP_FONT;
            info.SortID = 6;
            info.EditFormat = PropertyEditFormat.YesNo;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FORECOLOR;
            info.Name = "文字颜色";
            info.GroupID = GP_FONT;
            info.SortID = 7;
            info.EditFormat = PropertyEditFormat.ColorSelect;
            info.DefaultValue = "";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BACKCOLOR;
            info.Name = "背景颜色";
            info.GroupID = GP_FONT;
            info.SortID = 8;
            info.EditFormat = PropertyEditFormat.ColorSelect;
            info.DefaultValue = "";
            listProperties.Add(info);

            info = new ObjectPropertyInfo();
            info.ID = PRO_HALIGN;
            info.Name = "水平对齐方式";
            info.GroupID = GP_LAYOUT;
            info.SortID = 1;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_VALIGN;
            info.Name = "垂直对齐方式";
            info.GroupID = GP_LAYOUT;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "0";
            listProperties.Add(info);

            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_LEFT;
            info.Name = "左";
            info.GroupID = GP_BORDER;
            info.SortID = 1;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_TOP;
            info.Name = "上";
            info.GroupID = GP_BORDER;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_RIGHT;
            info.Name = "右";
            info.GroupID = GP_BORDER;
            info.SortID = 3;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_BOTTOM;
            info.Name = "下";
            info.GroupID = GP_BORDER;
            info.SortID = 4;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);

            info = new ObjectPropertyInfo();
            info.ID = PRO_LINK_URL;
            info.Name = "链接地址";
            info.GroupID = GP_LINK;
            info.SortID = 1;
            info.EditFormat = PropertyEditFormat.String;
            info.DefaultValue = "";
            listProperties.Add(info);

            var sequenceElement = cellElement as SequenceElement;
            if (sequenceElement != null)
            {
                info = new ObjectPropertyInfo();
                info.ID = PRO_DATASET;
                info.Name = "数据集";
                info.GroupID = GP_BASIC;
                info.SortID = 1;
                info.EditFormat = PropertyEditFormat.SingleSelect;
                info.DefaultValue = "";
                listProperties.Add(info);
                info = new ObjectPropertyInfo();
                info.ID = PRO_DATAFIELD;
                info.Name = "字段";
                info.GroupID = GP_BASIC;
                info.SortID = 2;
                info.EditFormat = PropertyEditFormat.SingleSelect;
                info.DefaultValue = "";
                listProperties.Add(info);

                info = new ObjectPropertyInfo();
                info.ID = PRO_SEQUENCE_EXT_METHOHD;
                info.Name = "扩展方式";
                info.GroupID = GP_SEQUENCE;
                info.SortID = 1;
                info.EditFormat = PropertyEditFormat.SingleSelect;
                info.DefaultValue = "1";
                listProperties.Add(info);
                info = new ObjectPropertyInfo();
                info.ID = PRO_SEQUENCE_MERGE;
                info.Name = "合并相同单元格";
                info.GroupID = GP_SEQUENCE;
                info.SortID = 2;
                info.EditFormat = PropertyEditFormat.YesNo;
                info.DefaultValue = "1";
                listProperties.Add(info);
            }

            var imageElement = cellElement as ImageElement;
            if (imageElement != null)
            {
                info = new ObjectPropertyInfo();
                info.ID = PRO_IMAGE_SOURCE;
                info.Name = "图片源";
                info.GroupID = GP_BASIC;
                info.SortID = 1;
                info.EditFormat = PropertyEditFormat.ImageSelect;
                info.DefaultValue = "";
                listProperties.Add(info);

                info = new ObjectPropertyInfo();
                info.ID = PRO_IMAGE_WIDTH;
                info.Name = "宽";
                info.GroupID = GP_IMAGE;
                info.SortID = 1;
                info.EditFormat = PropertyEditFormat.Int;
                info.DefaultValue = "0";
                listProperties.Add(info);
                info = new ObjectPropertyInfo();
                info.ID = PRO_IMAGE_HEIGHT;
                info.Name = "高";
                info.GroupID = GP_IMAGE;
                info.SortID = 2;
                info.EditFormat = PropertyEditFormat.Int;
                info.DefaultValue = "0";
                listProperties.Add(info);
                info = new ObjectPropertyInfo();
                info.ID = PRO_IMAGE_STRETCH;
                info.Name = "拉伸";
                info.GroupID = GP_IMAGE;
                info.SortID = 3;
                info.EditFormat = PropertyEditFormat.SingleSelect;
                info.DefaultValue = "1";
                listProperties.Add(info);
            }

            return listProperties.OrderBy(p => p.GroupID).ThenBy(p => p.ID).ToList();
        }

        //基本
        //数据列
        public const int PRO_DATASET = 101;
        public const int PRO_DATAFIELD = 102;
        public const int PRO_IMAGE_SOURCE = 111;

        //外观
        public const int PRO_TEXT = 1;
        public const int PRO_FONTFAMILY = 2;
        public const int PRO_FONTSIZE = 3;
        public const int PRO_FONTSTYLE_BOLD = 4;
        public const int PRO_FONTSTYLE_ITALIC = 5;
        public const int PRO_FONTSTYLE_UNDERLINE = 6;
        public const int PRO_FORECOLOR = 7;
        public const int PRO_BACKCOLOR = 8;

        //布局
        public const int PRO_HALIGN = 11;
        public const int PRO_VALIGN = 12;

        //边框
        public const int PRO_BORDER_LEFT = 21;
        public const int PRO_BORDER_TOP = 22;
        public const int PRO_BORDER_RIGHT = 23;
        public const int PRO_BORDER_BOTTOM = 24;


        //超链接
        public const int PRO_LINK_URL = 201;

        //数据列
        public const int PRO_SEQUENCE_EXT_METHOHD = 301;
        public const int PRO_SEQUENCE_MERGE = 302;

        //图片
        public const int PRO_IMAGE_WIDTH = 311;
        public const int PRO_IMAGE_HEIGHT = 312;
        public const int PRO_IMAGE_STRETCH = 313;

        //组编号
        public const int GP_BASIC = 0;
        public const int GP_FONT = 1;
        public const int GP_LAYOUT = 2;
        public const int GP_BORDER = 3;
        public const int GP_LINK = 10;
        public const int GP_SEQUENCE = 101;
        public const int GP_IMAGE = 102;

    }
}
