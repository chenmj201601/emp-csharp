//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3e2a713d-2659-43d1-9b8f-c20ebc9190c7
//        CLR Version:              4.0.30319.18444
//        Name:                     AnchorableShowStrategy
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                AnchorableShowStrategy
//
//        created by Charley at 2014/7/22 9:36:44
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [Flags]
    public enum AnchorableShowStrategy : byte
    {
        Most = 0x0001,
        Left = 0x0002,
        Right = 0x0004,
        Top = 0x0010,
        Bottom = 0x0020,
    }
}
