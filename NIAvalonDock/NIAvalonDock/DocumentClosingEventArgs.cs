//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    70bf7076-4de1-4c18-9693-7d7923c07100
//        CLR Version:              4.0.30319.18444
//        Name:                     DocumentClosingEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock
//        File Name:                DocumentClosingEventArgs
//
//        created by Charley at 2014/7/22 10:15:34
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock
{
    public class DocumentClosingEventArgs : CancelEventArgs
    {
        public DocumentClosingEventArgs(LayoutDocument document)
        {
            Document = document;
        }

        public LayoutDocument Document
        {
            get;
            private set;
        }
    }
}
