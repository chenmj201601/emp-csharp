//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    87229e91-672c-453a-90f6-c9f2dfde51a9
//        CLR Version:              4.0.30319.42000
//        Name:                     ListItemEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ListItemEventArgs
//
//        Created by Charley at 2017/5/15 10:28:07
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Wpf.Controls
{
    public class ListItemEventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IListItemObject Item { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public const int EVT_MOUSEDOUBLECLICK = 101;
    }
}
