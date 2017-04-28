//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    358ff049-64b4-42d3-a817-a6aee01a1650
//        CLR Version:              4.0.30319.42000
//        Name:                     UIntegerUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                UIntegerUpDown
//
//        Created by Charley at 2017/4/28 16:41:07
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class UIntegerUpDown : CommonNumericUpDown<uint>
    {
        #region Constructors

        static UIntegerUpDown()
        {
            UpdateMetadata(typeof(UIntegerUpDown), (uint)1, uint.MinValue, uint.MaxValue);
        }

        public UIntegerUpDown()
            : base(uint.Parse, Decimal.ToUInt32, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override uint IncrementValue(uint value, uint increment)
        {
            return (uint)(value + increment);
        }

        protected override uint DecrementValue(uint value, uint increment)
        {
            return (uint)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
