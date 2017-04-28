//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5d108328-378e-474f-bbaa-a574049aa065
//        CLR Version:              4.0.30319.18444
//        Name:                     DropTarget
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DropTarget
//
//        created by Charley at 2014/7/22 10:00:41
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    internal abstract class DropTarget<T> : DropTargetBase, IDropTarget where T : FrameworkElement
    {
        protected DropTarget(T targetElement, Rect detectionRect, DropTargetType type)
        {
            _targetElement = targetElement;
            _detectionRect = new Rect[] { detectionRect };
            _type = type;
        }

        protected DropTarget(T targetElement, IEnumerable<Rect> detectionRects, DropTargetType type)
        {
            _targetElement = targetElement;
            _detectionRect = detectionRects.ToArray();
            _type = type;
        }

        Rect[] _detectionRect;

        public Rect[] DetectionRects
        {
            get { return _detectionRect; }
        }


        T _targetElement;
        public T TargetElement
        {
            get { return _targetElement; }
        }

        DropTargetType _type;

        public DropTargetType Type
        {
            get { return _type; }
        }

        protected virtual void Drop(LayoutAnchorableFloatingWindow floatingWindow)
        { }

        protected virtual void Drop(LayoutDocumentFloatingWindow floatingWindow)
        { }


        public void Drop(LayoutFloatingWindow floatingWindow)
        {
            var root = floatingWindow.Root;
            var currentActiveContent = floatingWindow.Root.ActiveContent;
            var fwAsAnchorable = floatingWindow as LayoutAnchorableFloatingWindow;

            if (fwAsAnchorable != null)
            {
                this.Drop(fwAsAnchorable);
            }
            else
            {
                var fwAsDocument = floatingWindow as LayoutDocumentFloatingWindow;
                this.Drop(fwAsDocument);
            }

            Dispatcher.BeginInvoke(new Action(() =>
            {
                currentActiveContent.IsSelected = false;
                currentActiveContent.IsActive = false;
                currentActiveContent.IsActive = true;
            }), DispatcherPriority.Background);
        }

        public virtual bool HitTest(Point dragPoint)
        {
            return _detectionRect.Any(dr => dr.Contains(dragPoint));
        }

        public abstract Geometry GetPreviewPath(OverlayWindow overlayWindow, LayoutFloatingWindow floatingWindow);



        public void DragEnter()
        {
            SetIsDraggingOver(TargetElement, true);
        }

        public void DragLeave()
        {
            SetIsDraggingOver(TargetElement, false);
        }
    }
}
