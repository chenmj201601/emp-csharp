//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5d3f34b3-10c5-418a-a8b7-b8e601ebe0fc
//        CLR Version:              4.0.30319.42000
//        Name:                     TextElementPropertyFactory
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                TextElementPropertyFactory
//
//        Created by Charley at 2017/4/28 13:01:11
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;


namespace ReportDesigner.Models
{
    public class TextElementPropertyFactory
    {
        public static IList<ObjectPropertyInfo> GetPropertyList()
        {
            List<ObjectPropertyInfo> listProperties = new List<ObjectPropertyInfo>();
            ObjectPropertyInfo info = new ObjectPropertyInfo();
            info.ID = PRO_FONTFAMILY;
            info.Name = "字体";
            info.GroupID = 1;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "SimSun";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FONTSIZE;
            info.Name = "字号";
            info.GroupID = 1;
            info.SortID = 3;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "11";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FONTSTYLE;
            info.Name = "文字样式";
            info.GroupID = 1;
            info.SortID = 4;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_FORECOLOR;
            info.Name = "文字颜色";
            info.GroupID = 1;
            info.SortID = 5;
            info.EditFormat = PropertyEditFormat.ColorSelect;
            info.DefaultValue = "";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BACKCOLOR;
            info.Name = "背景颜色";
            info.GroupID = 1;
            info.SortID = 6;
            info.EditFormat = PropertyEditFormat.ColorSelect;
            info.DefaultValue = "";
            listProperties.Add(info);

            info = new ObjectPropertyInfo();
            info.ID = PRO_HALIGN;
            info.Name = "水平对齐方式";
            info.GroupID = 2;
            info.SortID = 1;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_VALIGN;
            info.Name = "垂直对齐方式";
            info.GroupID = 2;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "0";
            listProperties.Add(info);

            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_LEFT;
            info.Name = "左";
            info.GroupID = 3;
            info.SortID = 1;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_TOP;
            info.Name = "上";
            info.GroupID = 3;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_RIGHT;
            info.Name = "右";
            info.GroupID = 3;
            info.SortID = 3;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_BORDER_BOTTOM;
            info.Name = "下";
            info.GroupID = 3;
            info.SortID = 4;
            info.EditFormat = PropertyEditFormat.Int;
            info.DefaultValue = "0";
            listProperties.Add(info);

            return listProperties;
        }

        public const int PRO_TEXT = 1;
        public const int PRO_FONTFAMILY = 2;
        public const int PRO_FONTSIZE = 3;
        public const int PRO_FONTSTYLE = 4;
        public const int PRO_FORECOLOR = 5;
        public const int PRO_BACKCOLOR = 6;

        public const int PRO_HALIGN = 11;
        public const int PRO_VALIGN = 12;

        public const int PRO_BORDER_LEFT = 21;
        public const int PRO_BORDER_TOP = 22;
        public const int PRO_BORDER_RIGHT = 23;
        public const int PRO_BORDER_BOTTOM = 24;

    }
}
