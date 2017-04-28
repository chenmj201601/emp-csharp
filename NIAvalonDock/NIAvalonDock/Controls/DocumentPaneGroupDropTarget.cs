//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    4009e166-503a-4e08-80b9-12970abeeee0
//        CLR Version:              4.0.30319.18444
//        Name:                     DocumentPaneGroupDropTarget
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DocumentPaneGroupDropTarget
//
//        created by Charley at 2014/7/22 9:58:52
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    internal class DocumentPaneGroupDropTarget : DropTarget<LayoutDocumentPaneGroupControl>
    {
        internal DocumentPaneGroupDropTarget(LayoutDocumentPaneGroupControl paneControl, Rect detectionRect, DropTargetType type)
            : base(paneControl, detectionRect, type)
        {
            _targetPane = paneControl;
        }

        LayoutDocumentPaneGroupControl _targetPane;

        protected override void Drop(LayoutDocumentFloatingWindow floatingWindow)
        {
            ILayoutPane targetModel = _targetPane.Model as ILayoutPane;

            switch (Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:
                    #region DropTargetType.DocumentPaneGroupDockInside
                    {
                        var paneGroupModel = targetModel as LayoutDocumentPaneGroup;
                        var paneModel = paneGroupModel.Children[0] as LayoutDocumentPane;
                        var sourceModel = floatingWindow.RootDocument;

                        paneModel.Children.Insert(0, sourceModel);
                    }
                    break;
                    #endregion
            }
            base.Drop(floatingWindow);
        }

        protected override void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        {
            ILayoutPane targetModel = _targetPane.Model as ILayoutPane;

            switch (Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:
                    #region DropTargetType.DocumentPaneGroupDockInside
                    {
                        var paneGroupModel = targetModel as LayoutDocumentPaneGroup;
                        var paneModel = paneGroupModel.Children[0] as LayoutDocumentPane;
                        var layoutAnchorablePaneGroup = floatingWindow.RootPanel as LayoutAnchorablePaneGroup;

                        int i = 0;
                        foreach (var anchorableToImport in layoutAnchorablePaneGroup.Descendents().OfType<LayoutAnchorable>().ToArray())
                        {
                            paneModel.Children.Insert(i, anchorableToImport);
                            i++;
                        }
                    }
                    break;
                    #endregion
            }

            base.Drop(floatingWindow);
        }

        public override System.Windows.Media.Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindowModel)
        {
            switch (Type)
            {
                case DropTargetType.DocumentPaneGroupDockInside:
                    #region DropTargetType.DocumentPaneGroupDockInside
                    {
                        var targetScreenRect = TargetElement.GetScreenArea();
                        targetScreenRect.Offset(-overlayWindow.Left, -overlayWindow.Top);

                        return new RectangleGeometry(targetScreenRect);
                    }
                    #endregion
            }

            return null;
        }

    }
}
