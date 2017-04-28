//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    132d1ff0-1ba8-43cf-a995-546d1de83e36
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutElementEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutElementEventArgs
//
//        created by Charley at 2014/7/22 9:48:29
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public class LayoutElementEventArgs : EventArgs
    {
        public LayoutElementEventArgs(LayoutElement element)
        {
            Element = element;
        }


        public LayoutElement Element
        {
            get;
            private set;
        }
    }
}
