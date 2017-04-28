//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    931f4c55-737e-427b-8395-88f85b89afff
//        CLR Version:              4.0.30319.42000
//        Name:                     StaticConverters
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon.Converters
//        File Name:                StaticConverters
//
//        Created by Charley at 2017/4/10 18:57:49
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;


namespace NetInfo.Ribbon.Converters
{
    public static class StaticConverters
    {
        public static readonly InvertNumericConverter InvertNumericConverter = new InvertNumericConverter();

        public static readonly ThicknessConverter ThicknessConverter = new ThicknessConverter();
    }
}
