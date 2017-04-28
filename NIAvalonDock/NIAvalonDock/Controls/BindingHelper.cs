//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    769344ff-ed45-464a-b070-b20eec25d5a5
//        CLR Version:              4.0.30319.18444
//        Name:                     BindingHelper
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                BindingHelper
//
//        created by Charley at 2014/7/22 9:56:32
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class BindingHelper
    {
        public static void RebindInactiveBindings(DependencyObject dependencyObject)
        {
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(dependencyObject.GetType()))
            {
                var dpd = DependencyPropertyDescriptor.FromProperty(property);
                if (dpd != null)
                {
                    BindingExpressionBase binding = BindingOperations.GetBindingExpressionBase(dependencyObject, dpd.DependencyProperty);
                    if (binding != null)
                    {
                        //if (property.Name == "DataContext" || binding.HasError || binding.Status != BindingStatus.Active)
                        {
                            // Ensure that no pending calls are in the dispatcher queue
                            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.SystemIdle, (Action)delegate
                            {
                                // Remove and add the binding to re-trigger the binding error
                                dependencyObject.ClearValue(dpd.DependencyProperty);
                                BindingOperations.SetBinding(dependencyObject, dpd.DependencyProperty, binding.ParentBindingBase);
                            });
                        }
                    }
                }
            }
        }
    }
}
