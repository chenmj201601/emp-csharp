//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cced5f99-97d6-4c65-b905-8bdb87477d2b
//        CLR Version:              4.0.30319.42000
//        Name:                     DecimalUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                DecimalUpDown
//
//        Created by Charley at 2017/4/28 16:37:52
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class DecimalUpDown : CommonNumericUpDown<decimal>
    {
        #region Constructors

        static DecimalUpDown()
        {
            UpdateMetadata(typeof(DecimalUpDown), 1m, decimal.MinValue, decimal.MaxValue);
        }

        public DecimalUpDown()
            : base(Decimal.Parse, (d) => d, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override decimal IncrementValue(decimal value, decimal increment)
        {
            return value + increment;
        }

        protected override decimal DecrementValue(decimal value, decimal increment)
        {
            return value - increment;
        }

        #endregion //Base Class Overrides
    }
}
