//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    65d4a836-3ee8-42eb-bf57-45ead27d1929
//        CLR Version:              4.0.30319.42000
//        Name:                     LongUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                LongUpDown
//
//        Created by Charley at 2017/4/28 16:39:06
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class LongUpDown : CommonNumericUpDown<long>
    {
        #region Constructors

        static LongUpDown()
        {
            UpdateMetadata(typeof(LongUpDown), 1L, long.MinValue, long.MaxValue);
        }

        public LongUpDown()
            : base(Int64.Parse, Decimal.ToInt64, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override long IncrementValue(long value, long increment)
        {
            return value + increment;
        }

        protected override long DecrementValue(long value, long increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}
