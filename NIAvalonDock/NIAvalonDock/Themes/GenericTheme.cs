//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    546ee108-871e-4426-be1c-fc3413c6abce
//        CLR Version:              4.0.30319.42000
//        Name:                     GenericTheme
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.AvalonDock.Themes
//        File Name:                GenericTheme
//
//        Created by Charley at 2017/4/11 12:00:40
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.AvalonDock.Themes
{
    public class GenericTheme : Theme
    {
        public override Uri GetResourceUri()
        {
            return new Uri(
                "/NIAvalonDock;component/Themes/generic.xaml",
                UriKind.Relative);
        }
    }
}
