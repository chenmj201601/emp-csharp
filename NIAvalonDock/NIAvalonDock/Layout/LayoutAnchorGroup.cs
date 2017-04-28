//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    37f249ed-77b7-4688-9736-460e69d28f61
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutAnchorGroup
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutAnchorGroup
//
//        created by Charley at 2014/7/22 9:45:47
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [ContentProperty("Children")]
    [Serializable]
    public class LayoutAnchorGroup : LayoutGroup<LayoutAnchorable>, ILayoutPreviousContainer, ILayoutPaneSerializable
    {
        public LayoutAnchorGroup()
        {
        }

        protected override bool GetVisibility()
        {
            return Children.Count > 0;
        }


        #region PreviousContainer

        [field: NonSerialized]
        private ILayoutContainer _previousContainer = null;
        [XmlIgnore]
        ILayoutContainer ILayoutPreviousContainer.PreviousContainer
        {
            get { return _previousContainer; }
            set
            {
                if (_previousContainer != value)
                {
                    _previousContainer = value;
                    RaisePropertyChanged("PreviousContainer");
                    var paneSerializable = _previousContainer as ILayoutPaneSerializable;
                    if (paneSerializable != null &&
                        paneSerializable.Id == null)
                        paneSerializable.Id = Guid.NewGuid().ToString();
                }
            }
        }

        #endregion

        string _id;
        string ILayoutPaneSerializable.Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        string ILayoutPreviousContainer.PreviousContainerId
        {
            get;
            set;
        }

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (_id != null)
                writer.WriteAttributeString("Id", _id);
            if (_previousContainer != null)
            {
                var paneSerializable = _previousContainer as ILayoutPaneSerializable;
                if (paneSerializable != null)
                {
                    writer.WriteAttributeString("PreviousContainerId", paneSerializable.Id);
                }
            }

            base.WriteXml(writer);
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute("Id"))
                _id = reader.Value;
            if (reader.MoveToAttribute("PreviousContainerId"))
                ((ILayoutPreviousContainer)this).PreviousContainerId = reader.Value;


            base.ReadXml(reader);
        }
    }
}
