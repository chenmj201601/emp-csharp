//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    429e324c-327d-475f-9510-949970ea112e
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutDocument
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutDocument
//
//        created by Charley at 2014/7/22 9:46:34
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [Serializable]
    public class LayoutDocument : LayoutContent
    {
        public bool IsVisible
        {
            get { return true; }
        }

        #region Description

        private string _description = null;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }

        #endregion

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer);

            if (!string.IsNullOrWhiteSpace(Description))
                writer.WriteAttributeString("Description", Description);

        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute("Description"))
                Description = reader.Value;

            base.ReadXml(reader);
        }


#if TRACE
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("Document()");
        }
#endif


        protected override void InternalDock()
        {
            var root = Root as LayoutRoot;
            LayoutDocumentPane documentPane = null;
            if (root.LastFocusedDocument != null &&
                root.LastFocusedDocument != this)
            {
                documentPane = root.LastFocusedDocument.Parent as LayoutDocumentPane;
            }

            if (documentPane == null)
            {
                documentPane = root.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            }


            bool added = false;
            if (root.Manager.LayoutUpdateStrategy != null)
            {
                added = root.Manager.LayoutUpdateStrategy.BeforeInsertDocument(root, this, documentPane);
            }

            if (!added)
            {
                if (documentPane == null)
                    throw new InvalidOperationException("Layout must contains at least one LayoutDocumentPane in order to host documents");

                documentPane.Children.Add(this);
                added = true;
            }

            if (root.Manager.LayoutUpdateStrategy != null)
            {
                root.Manager.LayoutUpdateStrategy.AfterInsertDocument(root, this);
            }


            base.InternalDock();
        }
    }
}
