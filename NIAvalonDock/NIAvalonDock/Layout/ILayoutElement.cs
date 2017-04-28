//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    702fadb0-79ce-4b04-9fbf-5ec82d3bf133
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutElement
//
//        created by Charley at 2014/7/22 9:40:01
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutElement : INotifyPropertyChanged, INotifyPropertyChanging
    {
        ILayoutContainer Parent { get; }
        ILayoutRoot Root { get; }
    }
}
