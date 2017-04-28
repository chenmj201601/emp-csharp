//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1a270f68-09eb-4fef-8b48-278cc01a3a8a
//        CLR Version:              4.0.30319.42000
//        Name:                     InverseBoolConverter
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls.Core.Converters
//        File Name:                InverseBoolConverter
//
//        Created by Charley at 2017/4/28 16:43:08
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Windows.Data;


namespace NetInfo.Wpf.Controls.Core.Converters
{
    public class InverseBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
