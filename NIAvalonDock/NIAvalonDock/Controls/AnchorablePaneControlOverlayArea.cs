//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9405a2db-6dc7-45f3-89c2-88b07890546b
//        CLR Version:              4.0.30319.18444
//        Name:                     AnchorablePaneControlOverlayArea
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                AnchorablePaneControlOverlayArea
//
//        created by Charley at 2014/7/22 9:54:55
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class AnchorablePaneControlOverlayArea : OverlayArea
    {
        internal AnchorablePaneControlOverlayArea(
            IOverlayWindow overlayWindow,
            LayoutAnchorablePaneControl anchorablePaneControl)
            : base(overlayWindow)
        {

            _anchorablePaneControl = anchorablePaneControl;
            base.SetScreenDetectionArea(new Rect(
                _anchorablePaneControl.PointToScreenDPI(new Point()),
                _anchorablePaneControl.TransformActualSizeToAncestor()));

        }

        LayoutAnchorablePaneControl _anchorablePaneControl;
    }
}
