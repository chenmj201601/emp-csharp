//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    51b5cda8-95a7-4409-9c0d-22fa136fde22
//        CLR Version:              4.0.30319.42000
//        Name:                     AllowedSpecialValues
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                AllowedSpecialValues
//
//        Created by Charley at 2017/4/28 16:01:43
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    [Flags]
    public enum AllowedSpecialValues
    {
        None = 0,
        NaN = 1,
        PositiveInfinity = 2,
        NegativeInfinity = 4,
        AnyInfinity = PositiveInfinity | NegativeInfinity,
        Any = NaN | AnyInfinity
    }
}
