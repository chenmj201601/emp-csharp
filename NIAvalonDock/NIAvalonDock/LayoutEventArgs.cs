//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f848bb9b-3b28-43b2-9670-f9a9152896d9
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock
//        File Name:                LayoutEventArgs
//
//        created by Charley at 2014/7/22 10:16:14
//        http://www.netinfo.com 
//
//======================================================================
using System;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock
{
    class LayoutEventArgs : EventArgs
    {
        public LayoutEventArgs(LayoutRoot layoutRoot)
        {
            LayoutRoot = layoutRoot;
        }

        public LayoutRoot LayoutRoot
        {
            get;
            private set;
        }
    }
}
