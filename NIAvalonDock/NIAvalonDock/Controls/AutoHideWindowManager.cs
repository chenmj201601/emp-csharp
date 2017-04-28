//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    c22d0db7-072e-49e1-9ed7-f81c4a0620ad
//        CLR Version:              4.0.30319.18444
//        Name:                     AutoHideWindowManager
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                AutoHideWindowManager
//
//        created by Charley at 2014/7/22 9:56:15
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Threading;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class AutoHideWindowManager
    {
        DockingManager _manager;

        internal AutoHideWindowManager(DockingManager manager)
        {
            _manager = manager;
            SetupCloseTimer();
        }


        WeakReference _currentAutohiddenAnchor = null;

        public void ShowAutoHideWindow(LayoutAnchorControl anchor)
        {
            if (_currentAutohiddenAnchor.GetValueOrDefault<LayoutAnchorControl>() != anchor)
            {
                StopCloseTimer();
                _currentAutohiddenAnchor = new WeakReference(anchor);
                _manager.AutoHideWindow.Show(anchor);
                StartCloseTimer();
            }
        }

        public void HideAutoWindow(LayoutAnchorControl anchor = null)
        {
            if (anchor == null ||
                anchor == _currentAutohiddenAnchor.GetValueOrDefault<LayoutAnchorControl>())
            {
                StopCloseTimer();
            }
            else
                System.Diagnostics.Debug.Assert(false);
        }

        DispatcherTimer _closeTimer = null;
        void SetupCloseTimer()
        {
            _closeTimer = new DispatcherTimer(DispatcherPriority.Background);
            _closeTimer.Interval = TimeSpan.FromMilliseconds(1500);
            _closeTimer.Tick += (s, e) =>
            {
                if (_manager.AutoHideWindow.IsWin32MouseOver ||
                    ((LayoutAnchorable)_manager.AutoHideWindow.Model).IsActive ||
                    _manager.AutoHideWindow.IsResizing)
                    return;

                StopCloseTimer();
            };
        }

        void StartCloseTimer()
        {
            _closeTimer.Start();

        }

        void StopCloseTimer()
        {
            _closeTimer.Stop();
            _manager.AutoHideWindow.Hide();
            _currentAutohiddenAnchor = null;
        }
    }
}
