//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    547ef973-d41a-4f32-a2b9-36031b7b6be6
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutPanelElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutPanelElement
//
//        created by Charley at 2014/7/22 9:41:52
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutPanelElement : ILayoutElement
    {
        bool IsVisible { get; }
    }
}
