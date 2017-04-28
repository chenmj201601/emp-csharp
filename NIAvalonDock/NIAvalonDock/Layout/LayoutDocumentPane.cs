//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f25d75f1-a530-4f52-9c32-f7c941e48e82
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutDocumentPane
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutDocumentPane
//
//        created by Charley at 2014/7/22 9:47:32
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Markup;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [ContentProperty("Children")]
    [Serializable]
    public class LayoutDocumentPane : LayoutPositionableGroup<LayoutContent>, ILayoutDocumentPane, ILayoutPositionableElement, ILayoutContentSelector, ILayoutPaneSerializable
    {
        public LayoutDocumentPane()
        {
        }
        public LayoutDocumentPane(LayoutContent firstChild)
        {
            Children.Add(firstChild);
        }

        protected override bool GetVisibility()
        {
            if (Parent is LayoutDocumentPaneGroup)
                return ChildrenCount > 0;

            return true;
        }

        #region SelectedContentIndex

        private int _selectedIndex = -1;
        public int SelectedContentIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (value < 0 ||
                    value >= Children.Count)
                    value = -1;

                if (_selectedIndex != value)
                {
                    RaisePropertyChanging("SelectedContentIndex");
                    RaisePropertyChanging("SelectedContent");
                    if (_selectedIndex >= 0 &&
                        _selectedIndex < Children.Count)
                        Children[_selectedIndex].IsSelected = false;

                    _selectedIndex = value;

                    if (_selectedIndex >= 0 &&
                        _selectedIndex < Children.Count)
                        Children[_selectedIndex].IsSelected = true;

                    RaisePropertyChanged("SelectedContentIndex");
                    RaisePropertyChanged("SelectedContent");
                }
            }
        }

        protected override void ChildMoved(int oldIndex, int newIndex)
        {
            if (_selectedIndex == oldIndex)
            {
                RaisePropertyChanging("SelectedContentIndex");
                _selectedIndex = newIndex;
                RaisePropertyChanged("SelectedContentIndex");
            }


            base.ChildMoved(oldIndex, newIndex);
        }

        public LayoutContent SelectedContent
        {
            get { return _selectedIndex == -1 ? null : Children[_selectedIndex]; }
        }
        #endregion

        protected override void OnChildrenCollectionChanged()
        {
            if (SelectedContentIndex >= ChildrenCount)
                SelectedContentIndex = Children.Count - 1;
            if (SelectedContentIndex == -1 && ChildrenCount > 0)
            {
                if (Root == null)//if I'm not yet connected just switch to first document
                    SelectedContentIndex = 0;
                else
                {
                    var childrenToSelect = Children.OrderByDescending(c => c.LastActivationTimeStamp.GetValueOrDefault()).First();
                    SelectedContentIndex = Children.IndexOf(childrenToSelect);
                    childrenToSelect.IsActive = true;
                }
            }

            base.OnChildrenCollectionChanged();

            RaisePropertyChanged("ChildrenSorted");
        }

        public int IndexOf(LayoutContent content)
        {
            return Children.IndexOf(content);
        }

        protected override void OnIsVisibleChanged()
        {
            UpdateParentVisibility();
            base.OnIsVisibleChanged();
        }

        void UpdateParentVisibility()
        {
            var parentPane = Parent as ILayoutElementWithVisibility;
            if (parentPane != null)
                parentPane.ComputeVisibility();
        }

        public IEnumerable<LayoutContent> ChildrenSorted
        {
            get
            {
                var listSorted = Children.ToList();
                listSorted.Sort();
                return listSorted;
            }
        }

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

        public override void WriteXml(System.Xml.XmlWriter writer)
        {
            if (_id != null)
                writer.WriteAttributeString("Id", _id);

            base.WriteXml(writer);
        }

        public override void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToAttribute("Id"))
                _id = reader.Value;


            base.ReadXml(reader);
        }


#if TRACE
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("DocumentPane()");

            foreach (LayoutElement child in Children)
                child.ConsoleDump(tab + 1);
        }
#endif

    }
}
