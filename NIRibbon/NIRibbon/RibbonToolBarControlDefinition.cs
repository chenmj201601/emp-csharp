//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    ce456170-2ea1-4aec-8376-fa6d60f9e673
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonToolBarControlDefinition
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonToolBarControlDefinition
//
//        Created by Charley at 2017/4/11 10:17:02
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.ComponentModel;
using System.Windows;
using NetInfo.Ribbon.Extensibility;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represent logical definition for a control in toolbar
    /// </summary>
    public class RibbonToolBarControlDefinition : DependencyObject, INotifyPropertyChanged, IRibbonSizeChangedSink
    {
        public RibbonToolBarControlDefinition()
        {
            RibbonAttachedProperties.SetRibbonSize(this, RibbonControlSize.Small);
        }

        #region Target Property

        /// <summary>
        /// Gets or sets name of the target control
        /// </summary>
        public string Target
        {
            get { return (string)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ControlName.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(string),
            typeof(RibbonToolBarControlDefinition), new UIPropertyMetadata(null, OnTargetPropertyChanged));

        static void OnTargetPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RibbonToolBarControlDefinition definition = (RibbonToolBarControlDefinition)d;
            definition.Invalidate("Target");
        }

        #endregion

        #region Width Property

        /// <summary>
        /// Gets or sets width of the target control
        /// </summary>
        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Width. 
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.Register("Width", typeof(double), typeof(RibbonToolBarControlDefinition), new UIPropertyMetadata(Double.NaN, OnWidthPropertyChanged));

        static void OnWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RibbonToolBarControlDefinition definition = (RibbonToolBarControlDefinition)d;
            definition.Invalidate("Width");
        }

        #endregion

        #region Invalidating

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        void Invalidate(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Implementation of IRibbonSizeChangedSink

        public void OnSizePropertyChanged(RibbonControlSize previous, RibbonControlSize current)
        {
            this.Invalidate("Size");
        }

        #endregion
    }
}
