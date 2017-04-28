//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    98b334eb-76c5-4248-aeb4-4563bfa85aa7
//        CLR Version:              4.0.30319.42000
//        Name:                     GridBorder
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridBorder
//
//        Created by Charley at 2017/4/12 14:58:03
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 单元格的边框
    /// </summary>
    public class GridBorder : Border
    {
        static GridBorder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GridBorder),
                new FrameworkPropertyMetadata(typeof(GridBorder)));
        }

        public GridBorder()
        {
            //屏蔽边框的命中测试
            IsHitTestVisible = false;
        }
    }
}
