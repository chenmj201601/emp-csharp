//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    967bddb0-d2d9-491e-a60c-ae17a15db501
//        CLR Version:              4.0.30319.42000
//        Name:                     SeparatorTabItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                SeparatorTabItem
//
//        Created by Charley at 2017/4/11 10:21:20
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents separator to use in the TabControl
    /// </summary>
    public class SeparatorTabItem : TabItem
    {
        #region Constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1810")]
        static SeparatorTabItem()
        {
            Type type = typeof(SeparatorTabItem);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
            IsEnabledProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(false, null, CoerceIsEnabledAndTabStop));
            IsTabStopProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(false, null, CoerceIsEnabledAndTabStop));
            IsSelectedProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(false, OnIsSelectedChanged));
            StyleProperty.OverrideMetadata(typeof(SeparatorTabItem), new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceStyle)));
        }

        // Coerce object style
        static object OnCoerceStyle(DependencyObject d, object basevalue)
        {
            if (basevalue == null)
            {
                basevalue = (d as FrameworkElement).TryFindResource(typeof(SeparatorTabItem));
            }

            return basevalue;
        }

        static void OnIsSelectedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue) return;
            SeparatorTabItem separatorTabItem = (SeparatorTabItem)d;
            TabControl tabControl = separatorTabItem.Parent as TabControl;
            if (tabControl == null || tabControl.Items.Count <= 1) return;
            tabControl.SelectedIndex = tabControl.SelectedIndex == tabControl.Items.Count - 1
                ? tabControl.SelectedIndex - 1 :
                  tabControl.SelectedIndex + 1;
        }

        static object CoerceIsEnabledAndTabStop(DependencyObject d, object basevalue)
        {
            return false;
        }

        #endregion
    }
}
