//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5b7e86b1-0a14-41dc-95fe-ecd4b1ff4f9a
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutAnchorableFloatingWindow
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutAnchorableFloatingWindow
//
//        created by Charley at 2014/7/22 9:44:41
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Markup;
using System.Xml.Serialization;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [Serializable]
    [ContentProperty("RootPanel")]
    public class LayoutAnchorableFloatingWindow : LayoutFloatingWindow, ILayoutElementWithVisibility
    {
        public LayoutAnchorableFloatingWindow()
        {

        }

        #region RootPanel

        private LayoutAnchorablePaneGroup _rootPanel = null;
        public LayoutAnchorablePaneGroup RootPanel
        {
            get { return _rootPanel; }
            set
            {
                if (_rootPanel != value)
                {
                    RaisePropertyChanging("RootPanel");

                    if (_rootPanel != null)
                        _rootPanel.ChildrenTreeChanged -= new EventHandler<ChildrenTreeChangedEventArgs>(_rootPanel_ChildrenTreeChanged);

                    _rootPanel = value;
                    if (_rootPanel != null)
                        _rootPanel.Parent = this;

                    if (_rootPanel != null)
                        _rootPanel.ChildrenTreeChanged += new EventHandler<ChildrenTreeChangedEventArgs>(_rootPanel_ChildrenTreeChanged);

                    RaisePropertyChanged("RootPanel");
                    RaisePropertyChanged("IsSinglePane");
                    RaisePropertyChanged("SinglePane");
                    RaisePropertyChanged("Children");
                    RaisePropertyChanged("ChildrenCount");
                    ((ILayoutElementWithVisibility)this).ComputeVisibility();
                }
            }
        }

        void _rootPanel_ChildrenTreeChanged(object sender, ChildrenTreeChangedEventArgs e)
        {
            RaisePropertyChanged("IsSinglePane");
            RaisePropertyChanged("SinglePane");

        }

        public bool IsSinglePane
        {
            get
            {
                return RootPanel != null && RootPanel.Descendents().OfType<ILayoutAnchorablePane>().Where(p => p.IsVisible).Count() == 1;
            }
        }

        public ILayoutAnchorablePane SinglePane
        {
            get
            {
                if (!IsSinglePane)
                    return null;

                var singlePane = RootPanel.Descendents().OfType<LayoutAnchorablePane>().Single(p => p.IsVisible);
                singlePane.UpdateIsDirectlyHostedInFloatingWindow();
                return singlePane;
            }
        }

        #endregion

        public override IEnumerable<ILayoutElement> Children
        {
            get
            {
                if (ChildrenCount == 1)
                    yield return RootPanel;

                yield break;
            }
        }

        public override void RemoveChild(ILayoutElement element)
        {
            Debug.Assert(element == RootPanel && element != null);
            RootPanel = null;
        }

        public override void ReplaceChild(ILayoutElement oldElement, ILayoutElement newElement)
        {
            Debug.Assert(oldElement == RootPanel && oldElement != null);
            RootPanel = newElement as LayoutAnchorablePaneGroup;
        }

        public override int ChildrenCount
        {
            get
            {
                if (RootPanel == null)
                    return 0;
                return 1;
            }
        }

        #region IsVisible
        [NonSerialized]
        private bool _isVisible = true;

        [XmlIgnore]
        public bool IsVisible
        {
            get { return _isVisible; }
            private set
            {
                if (_isVisible != value)
                {
                    RaisePropertyChanging("IsVisible");
                    _isVisible = value;
                    RaisePropertyChanged("IsVisible");
                    if (IsVisibleChanged != null)
                        IsVisibleChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler IsVisibleChanged;

        #endregion


        void ILayoutElementWithVisibility.ComputeVisibility()
        {
            if (RootPanel != null)
                IsVisible = RootPanel.IsVisible;
            else
                IsVisible = false;

        }

        public override bool IsValid
        {
            get { return RootPanel != null; }
        }

#if TRACE
        public override void ConsoleDump(int tab)
        {
            System.Diagnostics.Trace.Write(new string(' ', tab * 4));
            System.Diagnostics.Trace.WriteLine("FloatingAnchorableWindow()");

            RootPanel.ConsoleDump(tab + 1);
        }
#endif
    }
}
