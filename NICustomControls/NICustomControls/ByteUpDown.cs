//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    08866282-a241-4f0a-83ac-57f35dd37156
//        CLR Version:              4.0.30319.42000
//        Name:                     ByteUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ByteUpDown
//
//        Created by Charley at 2017/4/28 16:37:23
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Wpf.Controls
{
    public class ByteUpDown : CommonNumericUpDown<byte>
    {
        #region Constructors

        static ByteUpDown()
        {
            UpdateMetadata(typeof(ByteUpDown), (byte)1, byte.MinValue, byte.MaxValue);
        }

        public ByteUpDown()
            : base(Byte.Parse, Decimal.ToByte, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Base Class Overrides

        protected override byte IncrementValue(byte value, byte increment)
        {
            return (byte)(value + increment);
        }

        protected override byte DecrementValue(byte value, byte increment)
        {
            return (byte)(value - increment);
        }

        #endregion //Base Class Overrides
    }
}
