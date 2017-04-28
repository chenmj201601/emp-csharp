//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    384ea320-44f4-46e8-a29c-3b91f496ef52
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutAnchorGroupControl
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutAnchorGroupControl
//
//        created by Charley at 2014/7/22 10:06:05
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class LayoutAnchorGroupControl : Control, ILayoutControl
    {
        static LayoutAnchorGroupControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LayoutAnchorGroupControl), new FrameworkPropertyMetadata(typeof(LayoutAnchorGroupControl)));
        }


        internal LayoutAnchorGroupControl(LayoutAnchorGroup model)
        {
            _model = model;
            CreateChildrenViews();

            _model.Children.CollectionChanged += (s, e) => OnModelChildrenCollectionChanged(e);
        }

        private void CreateChildrenViews()
        {
            var manager = _model.Root.Manager;
            foreach (var childModel in _model.Children)
            {
                _childViews.Add(new LayoutAnchorControl(childModel) { Template = manager.AnchorTemplate });
            }
        }

        private void OnModelChildrenCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove ||
                e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                if (e.OldItems != null)
                {
                    {
                        foreach (var childModel in e.OldItems)
                            _childViews.Remove(_childViews.First(cv => cv.Model == childModel));
                    }
                }
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
                _childViews.Clear();

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add ||
                e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Replace)
            {
                if (e.NewItems != null)
                {
                    var manager = _model.Root.Manager;
                    int insertIndex = e.NewStartingIndex;
                    foreach (LayoutAnchorable childModel in e.NewItems)
                    {
                        _childViews.Insert(insertIndex++, new LayoutAnchorControl(childModel) { Template = manager.AnchorTemplate });
                    }
                }
            }
        }

        ObservableCollection<LayoutAnchorControl> _childViews = new ObservableCollection<LayoutAnchorControl>();

        public ObservableCollection<LayoutAnchorControl> Children
        {
            get { return _childViews; }
        }


        LayoutAnchorGroup _model;
        public ILayoutElement Model
        {
            get { return _model; }
        }
    }
}
