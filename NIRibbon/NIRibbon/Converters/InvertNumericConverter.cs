//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    16bb9244-2d14-468e-a84a-e59ec721e019
//        CLR Version:              4.0.30319.42000
//        Name:                     InvertNumericConverter
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon.Converters
//        File Name:                InvertNumericConverter
//
//        Created by Charley at 2017/4/10 18:56:59
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Globalization;
using System.Windows.Data;


namespace NetInfo.Ribbon.Converters
{
    public class InvertNumericConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value produced by the binding source.</param><param name="targetType">The type of the binding target property.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            {
                var numericValue = value as float?;

                if (numericValue != null)
                {
                    return numericValue * -1;
                }
            }

            {
                var numericValue = value as double?;

                if (numericValue != null)
                {
                    return numericValue * -1;
                }
            }

            {
                var numericValue = value as int?;

                if (numericValue != null)
                {
                    return numericValue * -1;
                }
            }

            {
                var numericValue = value as long?;

                if (numericValue != null)
                {
                    return numericValue * -1;
                }
            }

            return value;
        }

        /// <summary>
        /// Converts a value. 
        /// </summary>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        /// <param name="value">The value that is produced by the binding target.</param><param name="targetType">The type to convert to.</param><param name="parameter">The converter parameter to use.</param><param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}
