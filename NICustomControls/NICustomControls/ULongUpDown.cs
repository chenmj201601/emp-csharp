//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    18d65b78-5a7a-43cc-b0ab-cc32d73a1859
//        CLR Version:              4.0.30319.42000
//        Name:                     ULongUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ULongUpDown
//
//        Created by Charley at 2017/4/28 16:41:30
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class ULongUpDown : CommonNumericUpDown<ulong>
    {
        #region Constructors

        static ULongUpDown()
        {
            UpdateMetadata(typeof(ULongUpDown), (ulong)1, ulong.MinValue, ulong.MaxValue);
        }

        public ULongUpDown()
            : base(ulong.Parse, Decimal.ToUInt64, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override ulong IncrementValue(ulong value, ulong increment)
        {
            return (ulong)(value + increment);
        }

        protected override ulong DecrementValue(ulong value, ulong increment)
        {
            return (ulong)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
