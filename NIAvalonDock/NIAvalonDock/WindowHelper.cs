//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9b044adf-d162-4aac-9cab-eefbf3815cf9
//        CLR Version:              4.0.30319.18444
//        Name:                     WindowHelper
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock
//        File Name:                WindowHelper
//
//        created by Charley at 2014/7/22 11:20:17
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace NetInfo.Wpf.AvalonDock
{
    static class WindowHelper
    {
        public static bool IsAttachedToPresentationSource(this Visual element)
        {
            return PresentationSource.FromVisual(element as Visual) != null;
        }

        public static void SetParentToMainWindowOf(this Window window, Visual element)
        {
            var wndParent = Window.GetWindow(element);
            if (wndParent != null)
                window.Owner = wndParent;
            else
            {
                IntPtr parentHwnd;
                if (GetParentWindowHandle(element, out parentHwnd))
                    Win32Helper.SetOwner(new WindowInteropHelper(window).Handle, parentHwnd);
            }
        }

        public static IntPtr GetParentWindowHandle(this Window window)
        {
            if (window.Owner != null)
                return new WindowInteropHelper(window.Owner).Handle;
            else
                return Win32Helper.GetOwner(new WindowInteropHelper(window).Handle);
        }


        public static bool GetParentWindowHandle(this Visual element, out IntPtr hwnd)
        {
            hwnd = IntPtr.Zero;
            HwndSource wpfHandle = PresentationSource.FromVisual(element) as HwndSource;

            if (wpfHandle == null)
                return false;

            hwnd = Win32Helper.GetParent(wpfHandle.Handle);
            if (hwnd == IntPtr.Zero)
                hwnd = wpfHandle.Handle;
            return true;
        }

        public static void SetParentWindowToNull(this Window window)
        {
            if (window.Owner != null)
                window.Owner = null;
            else
            {
                Win32Helper.SetOwner(new WindowInteropHelper(window).Handle, IntPtr.Zero);
            }
        }
    }
}
