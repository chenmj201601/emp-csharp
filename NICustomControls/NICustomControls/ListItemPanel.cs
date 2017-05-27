//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    43464f6c-3d4a-4c7f-a5bc-1ed73dd46555
//        CLR Version:              4.0.30319.42000
//        Name:                     ListItemPanel
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ListItemPanel
//
//        Created by Charley at 2017/5/15 10:29:19
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace NetInfo.Wpf.Controls
{
    public class ListItemPanel : ContentControl
    {
        static ListItemPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ListItemPanel),
                new FrameworkPropertyMetadata(typeof(ListItemPanel)));

            ListItemEventEvent = EventManager.RegisterRoutedEvent("ListItemEvent", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<ListItemEventArgs>), typeof(ListItemPanel));
        }

        public ListItemPanel()
        {
            MouseDoubleClick += ListItemPanel_MouseDoubleClick;
        }

        void ListItemPanel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListItemEventArgs args = new ListItemEventArgs();
            var item = DataContext as IListItemObject;
            args.Item = item;
            args.Code = ListItemEventArgs.EVT_MOUSEDOUBLECLICK;
            args.Data = e;
            OnListItemEvent(this, args);
        }

        public static readonly RoutedEvent ListItemEventEvent;

        public event RoutedPropertyChangedEventHandler<ListItemEventArgs> ListItemEvent
        {
            add { AddHandler(ListItemEventEvent, value); }
            remove { RemoveHandler(ListItemEventEvent, value); }
        }

        protected void OnListItemEvent(object sender, ListItemEventArgs e)
        {
            var panel = sender as ListItemPanel;
            if (panel != null)
            {
                RoutedPropertyChangedEventArgs<ListItemEventArgs> args =
                    new RoutedPropertyChangedEventArgs<ListItemEventArgs>(default(ListItemEventArgs), e);
                args.RoutedEvent = ListItemEventEvent;
                panel.RaiseEvent(args);
            }
        }

    }
}
