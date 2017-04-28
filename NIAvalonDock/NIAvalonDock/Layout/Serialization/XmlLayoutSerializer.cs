//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f96598a8-175d-4210-8a23-de84f465a7ea
//        CLR Version:              4.0.30319.18444
//        Name:                     XmlLayoutSerializer
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout.Serialization
//        File Name:                XmlLayoutSerializer
//
//        created by Charley at 2014/7/22 9:36:04
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NetInfo.Wpf.AvalonDock.Layout.Serialization
{
    public class XmlLayoutSerializer : LayoutSerializer
    {
        public XmlLayoutSerializer(DockingManager manager)
            : base(manager)
        {

        }

        public void Serialize(System.Xml.XmlWriter writer)
        {
            var serializer = new XmlSerializer(typeof(LayoutRoot));
            serializer.Serialize(writer, Manager.Layout);
        }
        public void Serialize(System.IO.TextWriter writer)
        {
            var serializer = new XmlSerializer(typeof(LayoutRoot));
            serializer.Serialize(writer, Manager.Layout);
        }
        public void Serialize(System.IO.Stream stream)
        {
            var serializer = new XmlSerializer(typeof(LayoutRoot));
            serializer.Serialize(stream, Manager.Layout);
        }

        public void Serialize(string filepath)
        {
            using (var stream = new StreamWriter(filepath))
                Serialize(stream);
        }

        public void Deserialize(System.IO.Stream stream)
        {
            try
            {
                StartDeserialization();
                var serializer = new XmlSerializer(typeof(LayoutRoot));
                var layout = serializer.Deserialize(stream) as LayoutRoot;
                FixupLayout(layout);
                Manager.Layout = layout;
            }
            finally
            {
                EndDeserialization();
            }
        }

        public void Deserialize(System.IO.TextReader reader)
        {
            try
            {
                StartDeserialization();
                var serializer = new XmlSerializer(typeof(LayoutRoot));
                var layout = serializer.Deserialize(reader) as LayoutRoot;
                FixupLayout(layout);
                Manager.Layout = layout;
            }
            finally
            {
                EndDeserialization();
            }
        }

        public void Deserialize(System.Xml.XmlReader reader)
        {
            try
            {
                StartDeserialization();
                var serializer = new XmlSerializer(typeof(LayoutRoot));
                var layout = serializer.Deserialize(reader) as LayoutRoot;
                FixupLayout(layout);
                Manager.Layout = layout;
            }
            finally
            {
                EndDeserialization();
            }
        }

        public void Deserialize(string filepath)
        {
            using (var stream = new StreamReader(filepath))
                Deserialize(stream);
        }
    }
}
