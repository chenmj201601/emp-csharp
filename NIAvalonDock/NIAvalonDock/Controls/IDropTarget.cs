//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    0172d982-f93a-41db-806b-4b7745f9cb2a
//        CLR Version:              4.0.30319.18444
//        Name:                     IDropTarget
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                IDropTarget
//
//        created by Charley at 2014/7/22 10:02:29
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    internal interface IDropTarget
    {
        Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindow);

        bool HitTest(Point dragPoint);

        DropTargetType Type { get; }

        void Drop(LayoutFloatingWindow floatingWindow);

        void DragEnter();

        void DragLeave();
    }
}
