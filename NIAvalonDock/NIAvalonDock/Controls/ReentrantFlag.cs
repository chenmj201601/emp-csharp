//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    dea0f996-9a94-4640-9328-e291e9bf3e68
//        CLR Version:              4.0.30319.18444
//        Name:                     ReentrantFlag
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                ReentrantFlag
//
//        created by Charley at 2014/7/22 10:12:39
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class ReentrantFlag
    {
        public class _ReentrantFlagHandler : IDisposable
        {
            ReentrantFlag _owner;
            public _ReentrantFlagHandler(ReentrantFlag owner)
            {
                _owner = owner;
                _owner._flag = true;
            }

            public void Dispose()
            {
                _owner._flag = false;
            }
        }

        bool _flag = false;

        public _ReentrantFlagHandler Enter()
        {
            if (_flag)
                throw new InvalidOperationException();
            return new _ReentrantFlagHandler(this);
        }

        public bool CanEnter
        {
            get { return !_flag; }
        }

    }
}
