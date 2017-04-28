//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    c65835c1-6dd1-4703-8f81-12af567a027a
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutAnchorablePaneControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutAnchorablePaneControl
//
//        created by Charley at 2014/7/22 10:04:50
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class LayoutAnchorablePaneControl : TabControl, ILayoutControl//, ILogicalChildrenContainer
    {
        static LayoutAnchorablePaneControl()
        {
            FocusableProperty.OverrideMetadata(typeof(LayoutAnchorablePaneControl), new FrameworkPropertyMetadata(false));
        }

        public LayoutAnchorablePaneControl(LayoutAnchorablePane model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            _model = model;

            SetBinding(ItemsSourceProperty, new Binding("Model.Children") { Source = this });
            SetBinding(FlowDirectionProperty, new Binding("Model.Root.Manager.FlowDirection") { Source = this });

            this.LayoutUpdated += new EventHandler(OnLayoutUpdated);
        }

        void OnLayoutUpdated(object sender, EventArgs e)
        {
            var modelWithAtcualSize = _model as ILayoutPositionableElementWithActualSize;
            modelWithAtcualSize.ActualWidth = ActualWidth;
            modelWithAtcualSize.ActualHeight = ActualHeight;
        }

        LayoutAnchorablePane _model;

        public ILayoutElement Model
        {
            get { return _model; }
        }

        protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            _model.SelectedContent.IsActive = true;

            base.OnGotKeyboardFocus(e);
        }

        protected override void OnMouseLeftButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!e.Handled && _model.SelectedContent != null)
                _model.SelectedContent.IsActive = true;
        }

        protected override void OnMouseRightButtonDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);

            if (!e.Handled && _model.SelectedContent != null)
                _model.SelectedContent.IsActive = true;

        }

    }
}
