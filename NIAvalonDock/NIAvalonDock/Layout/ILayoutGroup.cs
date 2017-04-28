//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    7ce8abab-21de-4be9-99ad-be9b9a08b2fd
//        CLR Version:              4.0.30319.18444
//        Name:                     ILayoutGroup
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                ILayoutGroup
//
//        created by Charley at 2014/7/22 9:40:43
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    public interface ILayoutGroup : ILayoutContainer
    {
        int IndexOfChild(ILayoutElement element);
        void InsertChildAt(int index, ILayoutElement element);
        void RemoveChildAt(int index);
        void ReplaceChildAt(int index, ILayoutElement element);
        event EventHandler ChildrenCollectionChanged;
    }
}
