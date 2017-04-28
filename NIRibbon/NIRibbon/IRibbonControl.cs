//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    d96da485-f53a-41e5-9807-93f32a5fd05c
//        CLR Version:              4.0.30319.42000
//        Name:                     IRibbonControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                IRibbonControl
//
//        Created by Charley at 2017/4/11 9:53:47
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents logical sizes of a ribbon control 
    /// </summary>
    public enum RibbonControlSize
    {
        /// <summary>
        /// Large size of a control
        /// </summary>
        Large = 0,
        /// <summary>
        /// Middle size of a control
        /// </summary>
        Middle,
        /// <summary>
        /// Small size of a control
        /// </summary>
        Small
    }

    /// <summary>
    /// Base interface for Fluent controls
    /// </summary>
    public interface IRibbonControl : IKeyTipedControl
    {
        /// <summary>
        /// Gets or sets element Text
        /// </summary>
        object Header { get; set; }

        /// <summary>
        /// Gets or sets Icon for the element
        /// </summary>
        object Icon { get; set; }

    }
}
