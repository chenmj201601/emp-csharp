//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    72357ab9-9b40-4dc3-b3b1-47544d4583db
//        CLR Version:              4.0.30319.18444
//        Name:                     WeakDictionary
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                WeakDictionary
//
//        created by Charley at 2014/7/22 10:13:11
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    class WeakDictionary<K, V> where K : class
    {
        public WeakDictionary()
        { }

        List<WeakReference> _keys = new List<WeakReference>();
        List<V> _values = new List<V>();

        public V this[K key]
        {
            get
            {
                V valueToReturn;
                if (!GetValue(key, out valueToReturn))
                    throw new ArgumentException();
                return valueToReturn;
            }
            set
            {
                SetValue(key, value);
            }
        }

        public bool ContainsKey(K key)
        {
            CollectGarbage();
            return -1 != _keys.FindIndex(k => k.GetValueOrDefault<K>() == key);
        }

        public void SetValue(K key, V value)
        {
            CollectGarbage();
            int vIndex = _keys.FindIndex(k => k.GetValueOrDefault<K>() == key);
            if (vIndex > -1)
                _values[vIndex] = value;
            else
            {
                _values.Add(value);
                _keys.Add(new WeakReference(key));
            }
        }

        public bool GetValue(K key, out V value)
        {
            CollectGarbage();
            int vIndex = _keys.FindIndex(k => k.GetValueOrDefault<K>() == key);
            value = default(V);
            if (vIndex == -1)
                return false;
            value = _values[vIndex];
            return true;
        }


        void CollectGarbage()
        {
            int vIndex = 0;

            do
            {
                vIndex = _keys.FindIndex(vIndex, k => !k.IsAlive);
                if (vIndex >= 0)
                {
                    _keys.RemoveAt(vIndex);
                    _values.RemoveAt(vIndex);
                }
            }
            while (vIndex >= 0);
        }
    }
}
