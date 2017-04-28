//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    d9635f67-328a-4f5a-a0a7-ebedf1045c0c
//        CLR Version:              4.0.30319.18444
//        Name:                     DropArea
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DropArea
//
//        created by Charley at 2014/7/22 9:59:44
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public enum DropAreaType
    {
        DockingManager,

        DocumentPane,

        DocumentPaneGroup,

        AnchorablePane,

    }


    public interface IDropArea
    {
        Rect DetectionRect { get; }
        DropAreaType Type { get; }
    }

    public class DropArea<T> : IDropArea where T : FrameworkElement
    {
        internal DropArea(T areaElement, DropAreaType type)
        {
            _element = areaElement;
            _detectionRect = areaElement.GetScreenArea();
            _type = type;
        }

        Rect _detectionRect;

        public Rect DetectionRect
        {
            get { return _detectionRect; }
        }

        DropAreaType _type;

        public DropAreaType Type
        {
            get { return _type; }
        }

        T _element;
        public T AreaElement
        {
            get
            {
                return _element;
            }
        }

    }
}
