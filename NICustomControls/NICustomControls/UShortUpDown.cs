//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1c0354f6-4ae0-4446-949c-c93c913aabde
//        CLR Version:              4.0.30319.42000
//        Name:                     UShortUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                UShortUpDown
//
//        Created by Charley at 2017/4/28 16:41:54
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class UShortUpDown : CommonNumericUpDown<ushort>
    {
        #region Constructors

        static UShortUpDown()
        {
            UpdateMetadata(typeof(UShortUpDown), (ushort)1, ushort.MinValue, ushort.MaxValue);
        }

        public UShortUpDown()
            : base(ushort.Parse, Decimal.ToUInt16, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override ushort IncrementValue(ushort value, ushort increment)
        {
            return (ushort)(value + increment);
        }

        protected override ushort DecrementValue(ushort value, ushort increment)
        {
            return (ushort)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
