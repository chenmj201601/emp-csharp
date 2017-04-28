//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f192c6f9-5bff-4590-9cd3-2c168e7cc80e
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutDocumentPaneGroupControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutDocumentPaneGroupControl
//
//        created by Charley at 2014/7/22 10:08:21
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class LayoutDocumentPaneGroupControl : LayoutGridControl<ILayoutDocumentPane>, ILayoutControl
    {
        internal LayoutDocumentPaneGroupControl(LayoutDocumentPaneGroup model)
            : base(model, model.Orientation)
        {
            _model = model;
        }

        LayoutDocumentPaneGroup _model;

        protected override void OnFixChildrenDockLengths()
        {
            #region Setup DockWidth/Height for children
            if (_model.Orientation == Orientation.Horizontal)
            {
                for (int i = 0; i < _model.Children.Count; i++)
                {
                    var childModel = _model.Children[i] as ILayoutPositionableElement;
                    if (!childModel.DockWidth.IsStar)
                    {
                        childModel.DockWidth = new GridLength(1.0, GridUnitType.Star);
                    }
                }
            }
            else
            {
                for (int i = 0; i < _model.Children.Count; i++)
                {
                    var childModel = _model.Children[i] as ILayoutPositionableElement;
                    if (!childModel.DockHeight.IsStar)
                    {
                        childModel.DockHeight = new GridLength(1.0, GridUnitType.Star);
                    }
                }
            }
            #endregion
        }

    }
}
