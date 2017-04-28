//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    16023a54-60ae-49a0-bde4-44e3fc3bb0dd
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutControl
//
//        created by Charley at 2014/7/22 9:39:24
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutControl
    {
        ILayoutElement Model { get; }
    }
}
