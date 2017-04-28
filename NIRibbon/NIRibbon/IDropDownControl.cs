//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    8f2d3290-0448-455a-b7d7-5fa80c58d761
//        CLR Version:              4.0.30319.42000
//        Name:                     IDropDownControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                IDropDownControl
//
//        Created by Charley at 2017/4/11 9:50:30
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Controls.Primitives;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents control that have drop down popup
    /// </summary>
    public interface IDropDownControl
    {
        /// <summary>
        /// Gets drop down popup
        /// </summary>
        Popup DropDownPopup { get; }

        /// <summary>
        /// Gets a value indicating whether control context menu is opened
        /// </summary>
        bool IsContextMenuOpened { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether drop down is opened
        /// </summary>
        bool IsDropDownOpen { get; set; }
    }
}
