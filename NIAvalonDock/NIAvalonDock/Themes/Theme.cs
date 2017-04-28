//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1a84c898-e56f-415a-99db-8aa0fae79df4
//        CLR Version:              4.0.30319.42000
//        Name:                     Theme
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.AvalonDock.Themes
//        File Name:                Theme
//
//        Created by Charley at 2017/4/11 12:01:06
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Windows;


namespace NetInfo.Wpf.AvalonDock.Themes
{
    public abstract class Theme : DependencyObject
    {
        public Theme()
        {

        }

        public abstract Uri GetResourceUri();


    }
}
