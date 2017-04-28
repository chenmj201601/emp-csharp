//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5cb6fc44-5788-4f3d-8a37-db5c2e9ed699
//        CLR Version:              4.0.30319.18444
//        Name:                     OverlayWindowDropTargetType
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                OverlayWindowDropTargetType
//
//        created by Charley at 2014/7/22 10:12:19
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public enum OverlayWindowDropTargetType
    {
        DockingManagerDockLeft,
        DockingManagerDockTop,
        DockingManagerDockRight,
        DockingManagerDockBottom,

        DocumentPaneDockLeft,
        DocumentPaneDockTop,
        DocumentPaneDockRight,
        DocumentPaneDockBottom,
        DocumentPaneDockInside,

        AnchorablePaneDockLeft,
        AnchorablePaneDockTop,
        AnchorablePaneDockRight,
        AnchorablePaneDockBottom,
        AnchorablePaneDockInside,

    }
}
