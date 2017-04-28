//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b7e80931-8d1d-409b-b99f-8aeec2c7e370
//        CLR Version:              4.0.30319.42000
//        Name:                     SpinDirection
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                SpinDirection
//
//        Created by Charley at 2017/4/28 15:57:58
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Wpf.Controls
{
    /// <summary>
    /// Represents spin directions that could be initiated by the end-user.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    public enum SpinDirection
    {
        /// <summary>
        /// Represents a spin initiated by the end-user in order to Increase a value.
        /// </summary>
        Increase = 0,

        /// <summary>
        /// Represents a spin initiated by the end-user in order to Decrease a value.
        /// </summary>
        Decrease = 1
    }
}
