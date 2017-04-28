//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    375015df-26be-4546-b01b-6d8b764bddf4
//        CLR Version:              4.0.30319.18444
//        Name:                     DocumentPaneControlOverlayArea
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DocumentPaneControlOverlayArea
//
//        created by Charley at 2014/7/22 9:57:54
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class DocumentPaneControlOverlayArea : OverlayArea
    {


        internal DocumentPaneControlOverlayArea(
            IOverlayWindow overlayWindow,
            LayoutDocumentPaneControl documentPaneControl)
            : base(overlayWindow)
        {
            _documentPaneControl = documentPaneControl;
            base.SetScreenDetectionArea(new Rect(
                _documentPaneControl.PointToScreenDPI(new Point()),
                _documentPaneControl.TransformActualSizeToAncestor()));
        }

        LayoutDocumentPaneControl _documentPaneControl;


    }
}
