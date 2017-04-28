﻿//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    57b29d15-0861-4e6a-9914-3b5b4683b60b
//        CLR Version:              4.0.30319.42000
//        Name:                     StatusBarItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                StatusBarItem
//
//        Created by Charley at 2017/4/11 10:23:25
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents ribbon status bar item
    /// </summary>
    public class StatusBarItem : System.Windows.Controls.Primitives.StatusBarItem
    {
        #region Properties

        #region Title

        /// <summary>
        /// Gets or sets ribbon status bar item
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(StatusBarItem), new UIPropertyMetadata(null));

        #endregion

        #region Value

        /// <summary>
        /// Gets or sets ribbon status bar value
        /// </summary>
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Value.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(StatusBarItem),
            new UIPropertyMetadata(null, OnValueChanged));

        static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatusBarItem item = (StatusBarItem)d;
            item.CoerceValue(ContentProperty);
        }


        #endregion

        #region isChecked

        /// <summary>
        /// Gets or sets whether status bar item is checked in menu
        /// </summary>
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register("IsChecked", typeof(bool), typeof(StatusBarItem), new UIPropertyMetadata(true, OnIsCheckedChanged));

        // Handles IsChecked changed
        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatusBarItem item = d as StatusBarItem;
            item.CoerceValue(VisibilityProperty);
            if ((bool)e.NewValue) item.RaiseChecked();
            else item.RaiseUnchecked();
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Occurs when status bar item checks
        /// </summary>
        public event RoutedEventHandler Checked;
        /// <summary>
        /// Occurs when status bar item unchecks
        /// </summary>
        public event RoutedEventHandler Unchecked;

        // Raises checked event
        private void RaiseChecked()
        {
            if (Checked != null) Checked(this, new RoutedEventArgs());
        }

        // Raises unchecked event
        private void RaiseUnchecked()
        {
            if (Unchecked != null) Unchecked(this, new RoutedEventArgs());
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor
        /// </summary>
        static StatusBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StatusBarItem), new FrameworkPropertyMetadata(typeof(StatusBarItem)));
            VisibilityProperty.AddOwner(typeof(StatusBarItem), new FrameworkPropertyMetadata(null, CoerceVisibility));
            ContentProperty.AddOwner(typeof(StatusBarItem), new FrameworkPropertyMetadata(null, OnContentChanged, CoerceContent));
        }

        // Content changing handler
        static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            StatusBarItem item = (StatusBarItem)d;
            item.CoerceValue(ValueProperty);
        }

        // Coerce content
        static object CoerceContent(DependencyObject d, object basevalue)
        {
            StatusBarItem item = (StatusBarItem)d;
            // if content is null returns value
            if ((basevalue == null) && (item.Value != null)) return item.Value;
            return basevalue;
        }

        // Coerce visibility
        static object CoerceVisibility(DependencyObject d, object basevalue)
        {
            // If unchecked when not visible in status bar
            if (!(d as StatusBarItem).IsChecked) return Visibility.Collapsed;
            return basevalue;
        }

        #endregion
    }
}
