//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    01907595-df52-4b45-984d-49cea30569e6
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutContentSelector
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutContentSelector
//
//        created by Charley at 2014/7/22 9:39:05
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutContentSelector
    {
        int SelectedContentIndex { get; set; }

        int IndexOf(LayoutContent content);

        LayoutContent SelectedContent { get; }
    }
}
