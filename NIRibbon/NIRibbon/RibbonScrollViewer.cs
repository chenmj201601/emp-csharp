//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    d88568b9-8ea6-4f06-b787-42601992c9c0
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonScrollViewer
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonScrollViewer
//
//        Created by Charley at 2017/4/11 10:13:23
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Controls;
using System.Windows.Media;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents ScrollViewer with modified hit test
    /// </summary>
    public class RibbonScrollViewer : ScrollViewer
    {
        #region Overrides

        /// <summary>
        /// Performs a hit test to determine whether the specified 
        /// points are within the bounds of this ScrollViewer
        /// </summary>
        /// <returns>The result of the hit test</returns>
        /// <param name="hitTestParameters">The parameters for hit testing within a visual object</param>
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            if (VisualChildrenCount > 0)
            {
                return VisualTreeHelper.HitTest(GetVisualChild(0), hitTestParameters.HitPoint);
            }
            return base.HitTestCore(hitTestParameters);
        }

        #endregion
    }
}
