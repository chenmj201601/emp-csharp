//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    bf3d3ec8-627e-450b-be24-da68b50cd315
//        CLR Version:              4.0.30319.18444
//        Name:                     DropDownButton
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                DropDownButton
//
//        created by Charley at 2014/7/22 10:00:04
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class DropDownButton : ToggleButton
    {
        public DropDownButton()
        {
            this.Unloaded += new RoutedEventHandler(DropDownButton_Unloaded);
        }


        #region DropDownContextMenu

        /// <summary>
        /// DropDownContextMenu Dependency Property
        /// </summary>
        public static readonly DependencyProperty DropDownContextMenuProperty =
            DependencyProperty.Register("DropDownContextMenu", typeof(ContextMenu), typeof(DropDownButton),
                new FrameworkPropertyMetadata((ContextMenu)null,
                    new PropertyChangedCallback(OnDropDownContextMenuChanged)));

        /// <summary>
        /// Gets or sets the DropDownContextMenu property.  This dependency property 
        /// indicates drop down menu to show up when user click on an anchorable menu pin.
        /// </summary>
        public ContextMenu DropDownContextMenu
        {
            get { return (ContextMenu)GetValue(DropDownContextMenuProperty); }
            set { SetValue(DropDownContextMenuProperty, value); }
        }

        /// <summary>
        /// Handles changes to the DropDownContextMenu property.
        /// </summary>
        private static void OnDropDownContextMenuChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DropDownButton)d).OnDropDownContextMenuChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the DropDownContextMenu property.
        /// </summary>
        protected virtual void OnDropDownContextMenuChanged(DependencyPropertyChangedEventArgs e)
        {
            var oldContextMenu = e.OldValue as ContextMenu;
            if (oldContextMenu != null && IsChecked.GetValueOrDefault())
                oldContextMenu.Closed -= new RoutedEventHandler(OnContextMenuClosed);
        }

        #endregion

        #region DropDownContextMenuDataContext

        /// <summary>
        /// DropDownContextMenuDataContext Dependency Property
        /// </summary>
        public static readonly DependencyProperty DropDownContextMenuDataContextProperty =
            DependencyProperty.Register("DropDownContextMenuDataContext", typeof(object), typeof(DropDownButton),
                new FrameworkPropertyMetadata((object)null));

        /// <summary>
        /// Gets or sets the DropDownContextMenuDataContext property.  This dependency property 
        /// indicates data context to set for drop down context menu.
        /// </summary>
        public object DropDownContextMenuDataContext
        {
            get { return (object)GetValue(DropDownContextMenuDataContextProperty); }
            set { SetValue(DropDownContextMenuDataContextProperty, value); }
        }

        #endregion

        protected override void OnClick()
        {
            if (DropDownContextMenu != null)
            {
                //IsChecked = true;
                DropDownContextMenu.PlacementTarget = this;
                DropDownContextMenu.Placement = PlacementMode.Bottom;
                DropDownContextMenu.DataContext = DropDownContextMenuDataContext;
                DropDownContextMenu.Closed += new RoutedEventHandler(OnContextMenuClosed);
                DropDownContextMenu.IsOpen = true;
            }

            base.OnClick();
        }

        void OnContextMenuClosed(object sender, RoutedEventArgs e)
        {
            //Debug.Assert(IsChecked.GetValueOrDefault());
            var ctxMenu = sender as ContextMenu;
            ctxMenu.Closed -= new RoutedEventHandler(OnContextMenuClosed);
            IsChecked = false;
        }

        void DropDownButton_Unloaded(object sender, RoutedEventArgs e)
        {
            DropDownContextMenu = null;
        }


    }
}
