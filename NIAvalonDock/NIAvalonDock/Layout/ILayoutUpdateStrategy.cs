//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    55278cd5-1c1e-42be-abfb-e59da4218b88
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutUpdateStrategy
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutUpdateStrategy
//
//        created by Charley at 2014/7/22 9:43:55
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutUpdateStrategy
    {
        bool BeforeInsertAnchorable(
            LayoutRoot layout,
            LayoutAnchorable anchorableToShow,
            ILayoutContainer destinationContainer);

        void AfterInsertAnchorable(
            LayoutRoot layout,
            LayoutAnchorable anchorableShown);


        bool BeforeInsertDocument(
            LayoutRoot layout,
            LayoutDocument anchorableToShow,
            ILayoutContainer destinationContainer);

        void AfterInsertDocument(
            LayoutRoot layout,
            LayoutDocument anchorableShown);
    }
}
