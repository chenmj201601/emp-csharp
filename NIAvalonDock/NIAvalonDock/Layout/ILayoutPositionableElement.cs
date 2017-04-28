//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b98ad58f-e71f-41d6-a7b7-9506a3c1ab73
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutPositionableElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutPositionableElement
//
//        created by Charley at 2014/7/22 9:42:31
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    internal interface ILayoutPositionableElement : ILayoutElement, ILayoutElementForFloatingWindow
    {
        GridLength DockWidth
        {
            get;
            set;
        }

        GridLength DockHeight
        {
            get;
            set;
        }

        double DockMinWidth { get; set; }
        double DockMinHeight { get; set; }
        bool IsVisible { get; }
    }

    internal interface ILayoutPositionableElementWithActualSize
    {
        double ActualWidth { get; set; }
        double ActualHeight { get; set; }
    }

    internal interface ILayoutElementForFloatingWindow
    {
        double FloatingWidth { get; set; }
        double FloatingHeight { get; set; }
        double FloatingLeft { get; set; }
        double FloatingTop { get; set; }
        bool IsMaximized { get; set; }
    }
}
