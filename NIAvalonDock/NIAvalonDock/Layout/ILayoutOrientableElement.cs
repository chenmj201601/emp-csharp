//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f92236fd-06c9-4a23-85f1-d6978d209c9b
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutOrientableElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutOrientableElement
//
//        created by Charley at 2014/7/22 9:41:08
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutOrientableGroup : ILayoutGroup
    {
        Orientation Orientation { get; set; }
    }
}
