//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    ef3471ea-99bb-45fc-b78b-80c77c44c6fa
//        CLR Version:              4.0.30319.18444
//        Name:                     IOverlayWindowDropTarget
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                IOverlayWindowDropTarget
//
//        created by Charley at 2014/7/22 10:03:21
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    interface IOverlayWindowDropTarget
    {
        Rect ScreenDetectionArea { get; }

        OverlayWindowDropTargetType Type { get; }
    }
}
