//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    78d3bed8-5232-4b4d-8547-b10cf5f8b68d
//        CLR Version:              4.0.30319.42000
//        Name:                     ValidSpinDirections
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ValidSpinDirections
//
//        Created by Charley at 2017/4/28 15:56:42
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    /// <summary>
    /// Represents spin directions that are valid.
    /// </summary>
    [Flags]
    public enum ValidSpinDirections
    {
        /// <summary>
        /// Can not increase nor decrease.
        /// </summary>
        None = 0,

        /// <summary>
        /// Can increase.
        /// </summary>
        Increase = 1,

        /// <summary>
        /// Can decrease.
        /// </summary>
        Decrease = 2
    }
}
