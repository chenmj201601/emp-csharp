//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    47f47a03-095d-44d2-ab71-c4188b1c1eee
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutPanelControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutPanelControl
//
//        created by Charley at 2014/7/22 10:10:09
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
    public class LayoutPanelControl : LayoutGridControl<ILayoutPanelElement>, ILayoutControl
    {
        internal LayoutPanelControl(LayoutPanel model)
            : base(model, model.Orientation)
        {
            _model = model;

        }

        LayoutPanel _model;

        protected override void OnFixChildrenDockLengths()
        {
            if (ActualWidth == 0.0 ||
                ActualHeight == 0.0)
                return;

            var modelAsPositionableElement = _model as ILayoutPositionableElementWithActualSize;
            #region Setup DockWidth/Height for children
            if (_model.Orientation == Orientation.Horizontal)
            {
                if (_model.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>())
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        var childContainerModel = _model.Children[i] as ILayoutContainer;
                        var childPositionableModel = _model.Children[i] as ILayoutPositionableElement;

                        if (childContainerModel != null &&
                            (childContainerModel.IsOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>() ||
                             childContainerModel.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>()))
                        {
                            childPositionableModel.DockWidth = new GridLength(1.0, GridUnitType.Star);
                        }
                        else if (childPositionableModel != null && childPositionableModel.DockWidth.IsStar)
                        {
                            var childPositionableModelWidthActualSize = childPositionableModel as ILayoutPositionableElementWithActualSize;

                            var widthToSet = Math.Max(childPositionableModelWidthActualSize.ActualWidth, childPositionableModel.DockMinWidth);

                            widthToSet = Math.Min(widthToSet, ActualWidth / 2.0);
                            widthToSet = Math.Max(widthToSet, childPositionableModel.DockMinWidth);

                            childPositionableModel.DockWidth = new GridLength(
                                widthToSet,
                                GridUnitType.Pixel);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        var childPositionableModel = _model.Children[i] as ILayoutPositionableElement;
                        if (!childPositionableModel.DockWidth.IsStar)
                        {
                            childPositionableModel.DockWidth = new GridLength(1.0, GridUnitType.Star);
                        }
                    }
                }
            }
            else
            {
                if (_model.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>())
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        var childContainerModel = _model.Children[i] as ILayoutContainer;
                        var childPositionableModel = _model.Children[i] as ILayoutPositionableElement;

                        if (childContainerModel != null &&
                            (childContainerModel.IsOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>() ||
                             childContainerModel.ContainsChildOfType<LayoutDocumentPane, LayoutDocumentPaneGroup>()))
                        {
                            childPositionableModel.DockHeight = new GridLength(1.0, GridUnitType.Star);
                        }
                        else if (childPositionableModel != null && childPositionableModel.DockHeight.IsStar)
                        {
                            var childPositionableModelWidthActualSize = childPositionableModel as ILayoutPositionableElementWithActualSize;

                            var heightToSet = Math.Max(childPositionableModelWidthActualSize.ActualHeight, childPositionableModel.DockMinHeight);
                            heightToSet = Math.Min(heightToSet, ActualHeight / 2.0);
                            heightToSet = Math.Max(heightToSet, childPositionableModel.DockMinHeight);

                            childPositionableModel.DockHeight = new GridLength(heightToSet, GridUnitType.Pixel);
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _model.Children.Count; i++)
                    {
                        var childPositionableModel = _model.Children[i] as ILayoutPositionableElement;
                        if (!childPositionableModel.DockHeight.IsStar)
                        {
                            childPositionableModel.DockHeight = new GridLength(1.0, GridUnitType.Star);
                        }
                    }
                }
            }
            #endregion
        }

    }
}
