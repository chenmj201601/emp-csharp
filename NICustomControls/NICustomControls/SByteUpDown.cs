//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    ef534408-06fe-4a32-8645-beacaead369c
//        CLR Version:              4.0.30319.42000
//        Name:                     SByteUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                SByteUpDown
//
//        Created by Charley at 2017/4/28 16:39:46
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class SByteUpDown : CommonNumericUpDown<sbyte>
    {
        #region Constructors

        static SByteUpDown()
        {
            UpdateMetadata(typeof(SByteUpDown), (sbyte)1, sbyte.MinValue, sbyte.MaxValue);
        }

        public SByteUpDown()
            : base(sbyte.Parse, Decimal.ToSByte, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override sbyte IncrementValue(sbyte value, sbyte increment)
        {
            return (sbyte)(value + increment);
        }

        protected override sbyte DecrementValue(sbyte value, sbyte increment)
        {
            return (sbyte)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
