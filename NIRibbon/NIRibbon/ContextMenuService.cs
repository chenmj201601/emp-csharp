//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    da28024c-2049-4894-9c99-c3a8a21a23bb
//        CLR Version:              4.0.30319.42000
//        Name:                     ContextMenuService
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                ContextMenuService
//
//        Created by Charley at 2017/4/10 19:15:26
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Windows;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents additional context menu service
    /// </summary>
    public static class ContextMenuService
    {
        /// <summary>
        /// Attach needed parameters to control
        /// </summary>
        /// <param name="type"></param>
        public static void Attach(Type type)
        {
            System.Windows.Controls.ContextMenuService.ShowOnDisabledProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(true));
            FrameworkElement.ContextMenuProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(null, OnContextMenuChanged, CoerceContextMenu));
        }

        private static void OnContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(FrameworkElement.ContextMenuProperty);
        }

        private static object CoerceContextMenu(DependencyObject d, object basevalue)
        {
            IQuickAccessItemProvider control = d as IQuickAccessItemProvider;
            if ((basevalue == null) && ((control == null) || control.CanAddToQuickAccessToolBar)) return Ribbon.RibbonContextMenu;
            return basevalue;
        }

        /// <summary>
        /// Coerce control context menu
        /// </summary>
        /// <param name="o">Control</param>
        public static void Coerce(DependencyObject o)
        {
            o.CoerceValue(FrameworkElement.ContextMenuProperty);
        }
    }
}
