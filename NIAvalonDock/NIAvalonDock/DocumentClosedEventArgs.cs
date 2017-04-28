//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    c6a103d3-2385-4aed-8a94-ebc44bea43f0
//        CLR Version:              4.0.30319.18444
//        Name:                     DocumentClosedEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock
//        File Name:                DocumentClosedEventArgs
//
//        created by Charley at 2014/7/22 10:15:15
//        http://www.netinfo.com 
//
//======================================================================
using System;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock
{
    public class DocumentClosedEventArgs : EventArgs
    {
        public DocumentClosedEventArgs(LayoutDocument document)
        {
            Document = document;
        }

        public LayoutDocument Document
        {
            get;
            private set;
        }
    }
}
