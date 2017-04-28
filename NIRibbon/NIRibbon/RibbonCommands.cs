//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    2980b7e4-a99a-4ef8-bde9-d299a94a9a42
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonCommands
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonCommands
//
//        Created by Charley at 2017/4/11 10:06:21
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Input;


namespace NetInfo.Ribbon
{
    public static class RibbonCommands
    {
        private static readonly RoutedUICommand openBackstage = new RoutedUICommand("Open backstage", "OpenBackstage", typeof(RibbonCommands));

        /// <summary>
        /// Gets the value that represents the Open Backstage command
        /// </summary>
        public static RoutedCommand OpenBackstage
        {
            get { return openBackstage; }
        }
    }
}
