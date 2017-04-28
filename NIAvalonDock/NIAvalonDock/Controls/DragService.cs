//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    6fc9765c-86cb-4f49-aeb9-4fd9433e52ff
//        CLR Version:              4.0.30319.18444
//        Name:                     DragService
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DragService
//
//        created by Charley at 2014/7/22 9:59:28
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class DragService
    {
        DockingManager _manager;
        LayoutFloatingWindowControl _floatingWindow;

        public DragService(LayoutFloatingWindowControl floatingWindow)
        {
            _floatingWindow = floatingWindow;
            _manager = floatingWindow.Model.Root.Manager;


            GetOverlayWindowHosts();
        }

        List<IOverlayWindowHost> _overlayWindowHosts = new List<IOverlayWindowHost>();
        void GetOverlayWindowHosts()
        {
            _overlayWindowHosts.AddRange(_manager.GetFloatingWindowsByZOrder().OfType<LayoutAnchorableFloatingWindowControl>().Where(fw => fw != _floatingWindow && fw.IsVisible));
            _overlayWindowHosts.Add(_manager);
        }

        IOverlayWindowHost _currentHost;
        IOverlayWindow _currentWindow;
        List<IDropArea> _currentWindowAreas = new List<IDropArea>();
        IDropTarget _currentDropTarget;

        public void UpdateMouseLocation(Point dragPosition)
        {
            var floatingWindowModel = _floatingWindow.Model as LayoutFloatingWindow;

            var newHost = _overlayWindowHosts.FirstOrDefault(oh => oh.HitTest(dragPosition));

            if (_currentHost != null || _currentHost != newHost)
            {
                //is mouse still inside current overlay window host?
                if ((_currentHost != null && !_currentHost.HitTest(dragPosition)) ||
                    _currentHost != newHost)
                {
                    //esit drop target
                    if (_currentDropTarget != null)
                        _currentWindow.DragLeave(_currentDropTarget);
                    _currentDropTarget = null;

                    //exit area
                    _currentWindowAreas.ForEach(a =>
                        _currentWindow.DragLeave(a));
                    _currentWindowAreas.Clear();

                    //hide current overlay window
                    if (_currentWindow != null)
                        _currentWindow.DragLeave(_floatingWindow);
                    if (_currentHost != null)
                        _currentHost.HideOverlayWindow();
                    _currentHost = null;
                }

                if (_currentHost != newHost)
                {
                    _currentHost = newHost;
                    _currentWindow = _currentHost.ShowOverlayWindow(_floatingWindow);
                    _currentWindow.DragEnter(_floatingWindow);
                }
            }

            if (_currentHost == null)
                return;

            if (_currentDropTarget != null &&
                !_currentDropTarget.HitTest(dragPosition))
            {
                _currentWindow.DragLeave(_currentDropTarget);
                _currentDropTarget = null;
            }

            List<IDropArea> areasToRemove = new List<IDropArea>();
            _currentWindowAreas.ForEach(a =>
            {
                //is mouse still inside this area?
                if (!a.DetectionRect.Contains(dragPosition))
                {
                    _currentWindow.DragLeave(a);
                    areasToRemove.Add(a);
                }
            });

            areasToRemove.ForEach(a =>
                _currentWindowAreas.Remove(a));


            var areasToAdd =
                _currentHost.GetDropAreas(_floatingWindow).Where(cw => !_currentWindowAreas.Contains(cw) && cw.DetectionRect.Contains(dragPosition)).ToList();

            _currentWindowAreas.AddRange(areasToAdd);

            areasToAdd.ForEach(a =>
                _currentWindow.DragEnter(a));

            if (_currentDropTarget == null)
            {
                _currentWindowAreas.ForEach(wa =>
                {
                    if (_currentDropTarget != null)
                        return;

                    _currentDropTarget = _currentWindow.GetTargets().FirstOrDefault(dt => dt.HitTest(dragPosition));
                    if (_currentDropTarget != null)
                    {
                        _currentWindow.DragEnter(_currentDropTarget);
                        return;
                    }
                });
            }

        }

        public void Drop(Point dropLocation, out bool dropHandled)
        {
            dropHandled = false;

            UpdateMouseLocation(dropLocation);

            var floatingWindowModel = _floatingWindow.Model as LayoutFloatingWindow;
            var root = floatingWindowModel.Root;

            if (_currentHost != null)
                _currentHost.HideOverlayWindow();

            if (_currentDropTarget != null)
            {
                _currentWindow.DragDrop(_currentDropTarget);
                root.CollectGarbage();
                dropHandled = true;
            }


            _currentWindowAreas.ForEach(a => _currentWindow.DragLeave(a));

            if (_currentDropTarget != null)
                _currentWindow.DragLeave(_currentDropTarget);
            if (_currentWindow != null)
                _currentWindow.DragLeave(_floatingWindow);
            _currentWindow = null;

            _currentHost = null;
        }

        internal void Abort()
        {
            var floatingWindowModel = _floatingWindow.Model as LayoutFloatingWindow;

            _currentWindowAreas.ForEach(a => _currentWindow.DragLeave(a));

            if (_currentDropTarget != null)
                _currentWindow.DragLeave(_currentDropTarget);
            if (_currentWindow != null)
                _currentWindow.DragLeave(_floatingWindow);
            _currentWindow = null;
            if (_currentHost != null)
                _currentHost.HideOverlayWindow();
            _currentHost = null;
        }
    }
}
