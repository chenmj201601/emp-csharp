//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    c4007229-d2e8-401d-8042-57de02f1b88e
//        CLR Version:              4.0.30319.18444
//        Name:                     Extentions
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock
//        File Name:                Extentions
//
//        created by Charley at 2014/7/22 10:15:57
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections;
using System.Collections.Generic;

namespace NetInfo.Wpf.AvalonDock
{
    internal static class Extensions
    {
        public static bool Contains(this IEnumerable collection, object item)
        {
            foreach (var o in collection)
                if (o == item)
                    return true;

            return false;
        }


        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (T v in collection)
                action(v);
        }


        public static int IndexOf<T>(this T[] array, T value) where T : class
        {
            for (int i = 0; i < array.Length; i++)
                if (array[i] == value)
                    return i;

            return -1;
        }

        public static V GetValueOrDefault<V>(this WeakReference wr)
        {
            if (wr == null || !wr.IsAlive)
                return default(V);
            return (V)wr.Target;
        }
    }
}
