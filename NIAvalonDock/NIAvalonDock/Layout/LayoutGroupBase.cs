//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    7b11c6c9-cf0c-4ce4-a0ce-e146d2b1e786
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutGroupBase
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutGroupBase
//
//        created by Charley at 2014/7/22 9:49:13
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [Serializable]
    public abstract class LayoutGroupBase : LayoutElement
    {
        [field: NonSerialized]
        [field: XmlIgnore]
        public event EventHandler ChildrenCollectionChanged;

        protected virtual void OnChildrenCollectionChanged()
        {
            if (ChildrenCollectionChanged != null)
                ChildrenCollectionChanged(this, EventArgs.Empty);
        }

        protected void NotifyChildrenTreeChanged(ChildrenTreeChange change)
        {
            OnChildrenTreeChanged(change);
            var parentGroup = Parent as LayoutGroupBase;
            if (parentGroup != null)
                parentGroup.NotifyChildrenTreeChanged(ChildrenTreeChange.TreeChanged);
        }

        [field: NonSerialized]
        [field: XmlIgnore]
        public event EventHandler<ChildrenTreeChangedEventArgs> ChildrenTreeChanged;

        protected virtual void OnChildrenTreeChanged(ChildrenTreeChange change)
        {
            if (ChildrenTreeChanged != null)
                ChildrenTreeChanged(this, new ChildrenTreeChangedEventArgs(change));
        }


    }
}
