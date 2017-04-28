//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    039da173-e72c-4644-8b1a-41a628870858
//        CLR Version:              4.0.30319.42000
//        Name:                     RectUtil
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                RectUtil
//
//        Created by Charley at 2017/4/14 16:38:55
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Windows;


namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 矩形操作帮助类
    /// </summary>
    public class RectUtil
    {
        /// <summary>
        /// 返回两个矩形的并集
        /// </summary>
        /// <param name="rect1">第一个矩形</param>
        /// <param name="rect2">第二个矩形</param>
        /// <returns>两矩形的并集</returns>
        public static Rect MergeRect(Rect rect1, Rect rect2)
        {
            double x1 = Math.Min(rect1.Left, rect2.Left);
            double y1 = Math.Min(rect1.Top, rect2.Top);
            double x2 = Math.Max(rect1.Right, rect2.Right);
            double y2 = Math.Max(rect1.Bottom, rect2.Bottom);
            double width = x2 - x1;
            double height = y2 - y1;
            return new Rect(x1, y1, width, height);
        }

        /// <summary>
        /// 判断两个矩形是否相交，这个方法与Rect的IntersectsWith方法类似，但是这个方法考虑了两矩形的外接的情况，外接不认为相交
        /// </summary>
        /// <param name="rect1">第一个矩形</param>
        /// <param name="rect2">第二个矩形</param>
        /// <returns></returns>
        public static bool IntersectRect(Rect rect1, Rect rect2)
        {
            Rect merge = MergeRect(rect1, rect2);
            double x1 = rect1.Left.Equals(merge.Left) ? rect2.Left : rect1.Left;
            double x2 = rect1.Right.Equals(merge.Right) ? rect2.Right : rect1.Right;
            double y1 = rect1.Top.Equals(merge.Top) ? rect2.Top : rect1.Top;
            double y2 = rect1.Bottom.Equals(merge.Bottom) ? rect2.Bottom : rect1.Bottom;
            return !(x1 >= x2 || y1 >= y2);
        }
    }
}
