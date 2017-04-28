//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    561fee51-d71d-4473-be49-cd108123ea45
//        CLR Version:              4.0.30319.42000
//        Name:                     IKeyTipedControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                IKeyTipedControl
//
//        Created by Charley at 2017/4/11 9:50:59
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Ribbon
{
    /// <summary>
    /// Base interface for controls supports key tips
    /// </summary>
    public interface IKeyTipedControl
    {
        /// <summary>
        /// Handles key tip pressed
        /// </summary>
        void OnKeyTipPressed();

        /// <summary>
        /// Handles back navigation with KeyTips
        /// </summary>
        void OnKeyTipBack();
    }
}
