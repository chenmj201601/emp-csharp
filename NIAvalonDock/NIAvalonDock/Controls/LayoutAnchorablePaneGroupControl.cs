//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    73c7c680-8830-4e05-8c40-d722b8d472d0
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutAnchorablePaneGroupControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutAnchorablePaneGroupControl
//
//        created by Charley at 2014/7/22 10:05:07
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
    public class LayoutAnchorablePaneGroupControl : LayoutGridControl<ILayoutAnchorablePane>, ILayoutControl
    {
        internal LayoutAnchorablePaneGroupControl(LayoutAnchorablePaneGroup model)
            : base(model, model.Orientation)
        {
            _model = model;
        }

        LayoutAnchorablePaneGroup _model;

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
