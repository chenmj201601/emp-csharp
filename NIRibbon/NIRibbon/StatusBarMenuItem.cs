//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    23f7d433-d2a7-46c5-b2bc-03c4077338e3
//        CLR Version:              4.0.30319.42000
//        Name:                     StatusBarMenuItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                StatusBarMenuItem
//
//        Created by Charley at 2017/4/11 10:24:01
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents menu item in ribbon status bar menu
    /// </summary>
    public class StatusBarMenuItem : MenuItem
    {
        #region Properties

        /// <summary>
        /// Gets or sets Ribbon Status Bar menu item
        /// </summary>
        public StatusBarItem StatusBarItem
        {
            get { return (StatusBarItem)GetValue(StatusBarItemProperty); }
            set { SetValue(StatusBarItemProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for StatusBarItem.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty StatusBarItemProperty =
            DependencyProperty.Register("StatusBarItem", typeof(StatusBarItem), typeof(StatusBarMenuItem), new UIPropertyMetadata(null));


        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        static StatusBarMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatusBarMenuItem), new FrameworkPropertyMetadata(typeof(StatusBarMenuItem)));
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="item">Ribbon Status Bar menu item</param>
        public StatusBarMenuItem(StatusBarItem item)
        {
            StatusBarItem = item;
        }

        #endregion
    }
}
