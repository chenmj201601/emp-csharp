//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1ec649eb-e638-460a-bef3-1d40d9b55ef0
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutDocumentPaneControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutDocumentPaneControl
//
//        created by Charley at 2014/7/22 10:08:05
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
    public class LayoutDocumentPaneControl : TabControl, ILayoutControl//, ILogicalChildrenContainer
    {
        static LayoutDocumentPaneControl()
        {
            FocusableProperty.OverrideMetadata(typeof(LayoutDocumentPaneControl), new FrameworkPropertyMetadata(false));
        }


        internal LayoutDocumentPaneControl(LayoutDocumentPane model)
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

        protected override void OnGotKeyboardFocus(System.Windows.Input.KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            System.Diagnostics.Trace.WriteLine(string.Format("OnGotKeyboardFocus({0}, {1})", e.Source, e.NewFocus));


            //if (_model.SelectedContent != null)
            //    _model.SelectedContent.IsActive = true;

        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            base.OnSelectionChanged(e);

            if (_model.SelectedContent != null)
                _model.SelectedContent.IsActive = true;
        }

        List<object> _logicalChildren = new List<object>();

        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                return _logicalChildren.GetEnumerator();
            }
        }

        LayoutDocumentPane _model;

        public ILayoutElement Model
        {
            get { return _model; }
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
