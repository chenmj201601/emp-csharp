//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    dc49619c-cd04-42e7-bc1d-21f23a85e305
//        CLR Version:              4.0.30319.18444
//        Name:                     AutoHideCommandLayoutItemFromLayoutModelConverter
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Converters
//        File Name:                AutoHideCommandLayoutItemFromLayoutModelConverter
//
//        created by Charley at 2014/7/22 9:30:28
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using NetInfo.Wpf.AvalonDock.Controls;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Converters
{
    public class AutoHideCommandLayoutItemFromLayoutModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //when this converter is called layout could be constructing so many properties here are potentially not valid
            var layoutModel = value as LayoutContent;
            if (layoutModel == null)
                return null;
            if (layoutModel.Root == null)
                return null;
            if (layoutModel.Root.Manager == null)
                return null;

            var layoutItemModel = layoutModel.Root.Manager.GetLayoutItemFromModel(layoutModel) as LayoutAnchorableItem;
            if (layoutItemModel == null)
                return Binding.DoNothing;

            return layoutItemModel.AutoHideCommand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
