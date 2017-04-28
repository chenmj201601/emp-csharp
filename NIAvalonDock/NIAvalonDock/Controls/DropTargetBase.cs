//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    c059f06d-2b30-4f3c-9e8e-03b4950734d0
//        CLR Version:              4.0.30319.18444
//        Name:                     DropTargetBase
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DropTargetBase
//
//        created by Charley at 2014/7/22 10:00:57
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    abstract class DropTargetBase : DependencyObject
    {
        #region IsDraggingOver

        /// <summary>
        /// IsDraggingOver Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty IsDraggingOverProperty =
            DependencyProperty.RegisterAttached("IsDraggingOver", typeof(bool), typeof(DropTargetBase),
                new FrameworkPropertyMetadata((bool)false));

        /// <summary>
        /// Gets the IsDraggingOver property.  This dependency property 
        /// indicates if user is dragging a window over the target element.
        /// </summary>
        public static bool GetIsDraggingOver(DependencyObject d)
        {
            return (bool)d.GetValue(IsDraggingOverProperty);
        }

        /// <summary>
        /// Sets the IsDraggingOver property.  This dependency property 
        /// indicates if user is dragging away a window from the target element.
        /// </summary>
        public static void SetIsDraggingOver(DependencyObject d, bool value)
        {
            d.SetValue(IsDraggingOverProperty, value);
        }

        #endregion
    }
}
