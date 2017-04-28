//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cbb640dd-ba7d-461a-a1bc-fd20de4088f0
//        CLR Version:              4.0.30319.42000
//        Name:                     GroupSeparatorMenuItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                GroupSeparatorMenuItem
//
//        Created by Charley at 2017/4/11 9:48:27
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents group separator menu item
    /// </summary>
    [ContentProperty("Header")]
    public class GroupSeparatorMenuItem : MenuItem
    {
        [SuppressMessage("Microsoft.Performance", "CA1810")]
        static GroupSeparatorMenuItem()
        {
            Type type = typeof(GroupSeparatorMenuItem);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
            StyleProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(null, OnCoerceStyle));
            IsEnabledProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(false, null, CoerceIsEnabledAndTabStop));
            IsTabStopProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(false, null, CoerceIsEnabledAndTabStop));
        }

        static object CoerceIsEnabledAndTabStop(DependencyObject d, object basevalue)
        {
            return false;
        }

        // Coerce object style
        static object OnCoerceStyle(DependencyObject d, object basevalue)
        {
            if (basevalue == null)
            {
                basevalue = (d as FrameworkElement).TryFindResource(typeof(GroupSeparatorMenuItem));
            }

            return basevalue;
        }
    }
}
