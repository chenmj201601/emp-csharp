//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3fdc3c74-6cdb-4e23-aa96-8a9e6b09c8ab
//        CLR Version:              4.0.30319.42000
//        Name:                     ShortUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ShortUpDown
//
//        Created by Charley at 2017/4/28 16:40:13
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class ShortUpDown : CommonNumericUpDown<short>
    {
        #region Constructors

        static ShortUpDown()
        {
            UpdateMetadata(typeof(ShortUpDown), (short)1, short.MinValue, short.MaxValue);
        }

        public ShortUpDown()
            : base(Int16.Parse, Decimal.ToInt16, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override short IncrementValue(short value, short increment)
        {
            return (short)(value + increment);
        }

        protected override short DecrementValue(short value, short increment)
        {
            return (short)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
