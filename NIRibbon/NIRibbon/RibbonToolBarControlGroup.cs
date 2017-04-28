//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    41bf9ff3-6ce0-41a3-bc43-7bbc34cadc28
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonToolBarControlGroup
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonToolBarControlGroup
//
//        Created by Charley at 2017/4/11 10:17:36
//        http://www.netinfo.com 
//
//======================================================================

using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represent logical container for toolbar items
    /// </summary>
    [ContentProperty("Children")]
    public class RibbonToolBarControlGroup : ItemsControl
    {
        #region Properties

        /// <summary>
        /// Gets whether the group is the fisrt control in the row
        /// </summary>
        public bool IsFirstInRow
        {
            get { return (bool)GetValue(IsFirstInRowProperty); }
            set { SetValue(IsFirstInRowProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for IsFirstInRow.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsFirstInRowProperty =
            DependencyProperty.Register("IsFirstInRow", typeof(bool), typeof(RibbonToolBarControlGroup), new UIPropertyMetadata(true));

        /// <summary>
        /// Gets whether the group is the last control in the row
        /// </summary>
        public bool IsLastInRow
        {
            get { return (bool)GetValue(IsLastInRowProperty); }
            set { SetValue(IsLastInRowProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for IsFirstInRow.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsLastInRowProperty =
            DependencyProperty.Register("IsLastInRow", typeof(bool), typeof(RibbonToolBarControlGroup), new UIPropertyMetadata(true));

        #endregion

        #region Initialization

        [SuppressMessage("Microsoft.Performance", "CA1810")]
        static RibbonToolBarControlGroup()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RibbonToolBarControlGroup), new FrameworkPropertyMetadata(typeof(RibbonToolBarControlGroup)));
            StyleProperty.OverrideMetadata(typeof(RibbonToolBarControlGroup), new FrameworkPropertyMetadata(null, new CoerceValueCallback(OnCoerceStyle)));
        }

        // Coerce object style
        static object OnCoerceStyle(DependencyObject d, object basevalue)
        {
            if (basevalue == null)
            {
                basevalue = (d as FrameworkElement).TryFindResource(typeof(RibbonToolBarControlGroup));
            }

            return basevalue;
        }

        #endregion
    }
}
