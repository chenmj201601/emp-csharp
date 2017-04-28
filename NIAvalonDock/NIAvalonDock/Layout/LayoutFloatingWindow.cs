//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    ab256af4-b15a-4df4-b7ab-24740abbfa22
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutFloatingWindow
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutFloatingWindow
//
//        created by Charley at 2014/7/22 9:48:47
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
    [XmlInclude(typeof(LayoutAnchorableFloatingWindow))]
    [XmlInclude(typeof(LayoutDocumentFloatingWindow))]
    public abstract class LayoutFloatingWindow : LayoutElement, ILayoutContainer
    {
        public LayoutFloatingWindow()
        {

        }


        public abstract IEnumerable<ILayoutElement> Children { get; }

        public abstract void RemoveChild(ILayoutElement element);

        public abstract void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement);

        public abstract int ChildrenCount { get; }

        public abstract bool IsValid { get; }




    }
}
