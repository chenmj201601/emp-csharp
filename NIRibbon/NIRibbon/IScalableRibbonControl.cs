//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    04106366-7afd-4ed8-a487-ab85c4655926
//        CLR Version:              4.0.30319.42000
//        Name:                     IScalableRibbonControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                IScalableRibbonControl
//
//        Created by Charley at 2017/4/11 9:54:17
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Repesents scalable ribbon contol
    /// </summary>
    public interface IScalableRibbonControl
    {
        /// <summary>
        /// Enlarge control size
        /// </summary>
        void Enlarge();
        /// <summary>
        /// Reduce control size
        /// </summary>
        void Reduce();

        /// <summary>
        /// Occurs when contol is scaled
        /// </summary>
        event EventHandler Scaled;
    }
}
