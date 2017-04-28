//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    17719254-5dcd-48bc-ab41-c8684be7527c
//        CLR Version:              4.0.30319.42000
//        Name:                     IRibbonSizeChangedSink
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon.Extensibility
//        File Name:                IRibbonSizeChangedSink
//
//        Created by Charley at 2017/4/10 19:00:03
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Ribbon.Extensibility
{
    public interface IRibbonSizeChangedSink
    {
        void OnSizePropertyChanged(RibbonControlSize previous, RibbonControlSize current);
    }
}
