//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    287ef95b-be84-4034-b0d4-4fbb9903329c
//        CLR Version:              4.0.30319.42000
//        Name:                     SingleUpDown
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                SingleUpDown
//
//        Created by Charley at 2017/4/28 16:40:35
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Windows;


namespace NetInfo.Wpf.Controls
{
    public class SingleUpDown : CommonNumericUpDown<float>
    {
        #region Constructors

        static SingleUpDown()
        {
            UpdateMetadata(typeof(SingleUpDown), 1f, float.NegativeInfinity, float.PositiveInfinity);
        }

        public SingleUpDown()
            : base(Single.Parse, Decimal.ToSingle, (v1, v2) => v1 < v2, (v1, v2) => v1 > v2)
        {

        }

        #endregion //Constructors

        #region Properties

        #region AllowInputSpecialValues

        public static readonly DependencyProperty AllowInputSpecialValuesProperty =
            DependencyProperty.Register("AllowInputSpecialValues", typeof(AllowedSpecialValues), typeof(SingleUpDown), new UIPropertyMetadata(AllowedSpecialValues.None));

        public AllowedSpecialValues AllowInputSpecialValues
        {
            get { return (AllowedSpecialValues)GetValue(AllowInputSpecialValuesProperty); }
            set { SetValue(AllowInputSpecialValuesProperty, value); }
        }

        #endregion //AllowInputSpecialValues

        #endregion

        #region Base Class Overrides

        protected override float? OnCoerceIncrement(float? baseValue)
        {
            if (baseValue.HasValue && float.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Increment.");

            return base.OnCoerceIncrement(baseValue);
        }

        protected override float? OnCoerceMaximum(float? baseValue)
        {
            if (baseValue.HasValue && float.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Maximum.");

            return base.OnCoerceMaximum(baseValue);
        }

        protected override float? OnCoerceMinimum(float? baseValue)
        {
            if (baseValue.HasValue && float.IsNaN(baseValue.Value))
                throw new ArgumentException("NaN is invalid for Minimum.");

            return base.OnCoerceMinimum(baseValue);
        }

        protected override float IncrementValue(float value, float increment)
        {
            return value + increment;
        }

        protected override float DecrementValue(float value, float increment)
        {
            return value - increment;
        }

        protected override void SetValidSpinDirection()
        {
            if (Value.HasValue && float.IsInfinity(Value.Value) && (Spinner != null))
            {
                Spinner.ValidSpinDirection = ValidSpinDirections.None;
            }
            else
            {
                base.SetValidSpinDirection();
            }
        }

        protected override float? ConvertTextToValue(string text)
        {
            float? result = base.ConvertTextToValue(text);

            if (result != null)
            {
                if (float.IsNaN(result.Value))
                    TestInputSpecialValue(this.AllowInputSpecialValues, AllowedSpecialValues.NaN);
                else if (float.IsPositiveInfinity(result.Value))
                    TestInputSpecialValue(this.AllowInputSpecialValues, AllowedSpecialValues.PositiveInfinity);
                else if (float.IsNegativeInfinity(result.Value))
                    TestInputSpecialValue(this.AllowInputSpecialValues, AllowedSpecialValues.NegativeInfinity);
            }

            return result;
        }

        #endregion
    }
}
