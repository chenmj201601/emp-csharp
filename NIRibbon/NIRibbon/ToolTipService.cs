//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f080edd0-28e3-40fb-9c87-5f2b3e59806f
//        CLR Version:              4.0.30319.42000
//        Name:                     ToolTipService
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                ToolTipService
//
//        Created by Charley at 2017/4/11 10:26:40
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Windows;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents additional toltip functionality
    /// </summary>
    public static class ToolTipService
    {
        /// <summary>
        /// Attach ooltip properties to control
        /// </summary>
        /// <param name="type">Control type</param>
        public static void Attach(Type type)
        {
            System.Windows.Controls.ToolTipService.ShowOnDisabledProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(true));
            System.Windows.Controls.ToolTipService.InitialShowDelayProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(900));
            System.Windows.Controls.ToolTipService.BetweenShowDelayProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(0));
            System.Windows.Controls.ToolTipService.ShowDurationProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(20000));
        }
    }
}
