//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    436c0c11-5a2b-463b-88d9-77765e835493
//        CLR Version:              4.0.30319.18444
//        Name:                     MathHelper
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock
//        File Name:                MathHelper
//
//        created by Charley at 2014/7/22 10:30:51
//        http://www.netinfo.com 
//
//======================================================================
using System;

namespace NetInfo.Wpf.AvalonDock
{
    internal static class MathHelper
    {

        public static double MinMax(double value, double min, double max)
        {
            if (min > max)
                throw new ArgumentException("min>max");

            if (value < min)
                return min;
            if (value > max)
                return max;

            return value;
        }

        public static void AssertIsPositiveOrZero(double value)
        {
            if (value < 0.0)
                throw new ArgumentException("Invalid value, must be a positive number or equal to zero");
        }
    }
}
