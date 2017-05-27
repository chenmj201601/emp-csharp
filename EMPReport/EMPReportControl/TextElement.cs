//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    fda326fe-813e-4709-950a-37b7385fe6da
//        CLR Version:              4.0.30319.42000
//        Name:                     TextElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                TextElement
//
//        Created by Charley at 2017/5/15 17:13:55
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    public class TextElement : EditableElement, ICellElement
    {

        #region CellElement

        public string LinkUrl { get; set; }

        public GridCell Cell { get; set; }

        #endregion


        #region 创建一个TextElement对象

        public static TextElement FromReport(ReportText reportText)
        {
            TextElement textElement = new TextElement();
            textElement.Text = reportText.Text;
            return textElement;
        }

        #endregion


        #region 生成一个报表对象

        public ReportText ToReport()
        {
            ReportText reportText = new ReportText();
            reportText.Text = Text;
            return reportText;
        }

        #endregion

    }
}
