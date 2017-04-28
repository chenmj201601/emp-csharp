//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    99e86521-94d7-44b9-bfbe-e56100fe7b07
//        CLR Version:              4.0.30319.18444
//        Name:                     WindowActivateEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                WindowActivateEventArgs
//
//        created by Charley at 2014/7/22 10:13:29
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class WindowActivateEventArgs : EventArgs
    {
        public WindowActivateEventArgs(IntPtr hwndActivating)
        {
            HwndActivating = hwndActivating;
        }

        public IntPtr HwndActivating
        {
            get;
            private set;
        }
    }
}
