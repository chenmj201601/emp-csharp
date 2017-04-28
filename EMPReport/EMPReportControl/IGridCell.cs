//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    755c211d-3a1d-4de5-9d3b-89b2a48f7599
//        CLR Version:              4.0.30319.42000
//        Name:                     IGridCell
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                IGridCell
//
//        Created by Charley at 2017/4/12 14:57:16
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;

namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 网格中的单元格（公共参数定义）
    /// </summary>
    public interface IGridCell
    {
        int RowIndex { get; set; }
        int ColumnIndex { get; set; }
        int RowSpan { get; set; }
        int ColSpan { get; set; }
        Rect Rect { get; set; }
        DesignGrid Grid { get; set; }
    }
}
