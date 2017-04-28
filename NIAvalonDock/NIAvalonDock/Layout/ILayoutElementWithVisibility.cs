//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6fea4914-c04a-4db6-ae7b-c9667315406e
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutElementWithVisibility
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutElementWithVisibility
//
//        created by Charley at 2014/7/22 9:40:24
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutElementWithVisibility
    {
        void ComputeVisibility();
    }
}
