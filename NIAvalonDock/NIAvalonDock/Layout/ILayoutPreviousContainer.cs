//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    63ae80d4-f3f1-4ff8-b2fd-ea55d214d602
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutPreviousContainer
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutPreviousContainer
//
//        created by Charley at 2014/7/22 9:43:14
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    interface ILayoutPreviousContainer
    {
        ILayoutContainer PreviousContainer { get; set; }

        string PreviousContainerId { get; set; }
    }
}
