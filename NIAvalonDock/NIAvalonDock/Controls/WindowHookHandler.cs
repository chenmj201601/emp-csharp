//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cda4906d-db1f-4600-b493-57e4c0b5ee4b
//        CLR Version:              4.0.30319.18444
//        Name:                     WindowHookHandler
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                WindowHookHandler
//
//        created by Charley at 2014/7/22 10:13:47
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class FocusChangeEventArgs : EventArgs
    {
        public FocusChangeEventArgs(IntPtr gotFocusWinHandle, IntPtr lostFocusWinHandle)
        {
            GotFocusWinHandle = gotFocusWinHandle;
            LostFocusWinHandle = lostFocusWinHandle;
        }

        public IntPtr GotFocusWinHandle
        {
            get;
            private set;
        }
        public IntPtr LostFocusWinHandle
        {
            get;
            private set;
        }
    }

    class WindowHookHandler
    {
        public WindowHookHandler()
        {

        }

        IntPtr _windowHook;
        Win32Helper.HookProc _hookProc;
        public void Attach()
        {
            _hookProc = new Win32Helper.HookProc(this.HookProc);
            _windowHook = Win32Helper.SetWindowsHookEx(
                Win32Helper.HookType.WH_CBT,
                _hookProc,
                IntPtr.Zero,
                (int)Win32Helper.GetCurrentThreadId());
        }


        public void Detach()
        {
            Win32Helper.UnhookWindowsHookEx(_windowHook);
        }

        public int HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == Win32Helper.HCBT_SETFOCUS)
            {
                if (FocusChanged != null)
                    FocusChanged(this, new FocusChangeEventArgs(wParam, lParam));
            }
            else if (code == Win32Helper.HCBT_ACTIVATE)
            {
                if (_insideActivateEvent.CanEnter)
                {
                    using (_insideActivateEvent.Enter())
                    {
                        //if (Activate != null)
                        //    Activate(this, new WindowActivateEventArgs(wParam));
                    }
                }
            }


            return Win32Helper.CallNextHookEx(_windowHook, code, wParam, lParam);
        }

        public event EventHandler<FocusChangeEventArgs> FocusChanged;

        //public event EventHandler<WindowActivateEventArgs> Activate;

        ReentrantFlag _insideActivateEvent = new ReentrantFlag();
    }
}
