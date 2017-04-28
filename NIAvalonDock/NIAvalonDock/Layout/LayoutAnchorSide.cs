//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    4a8db7af-ae5e-4228-a9f5-ed57afcf418c
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutAnchorSide
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Layout
//        File Name:                LayoutAnchorSide
//
//        created by Charley at 2014/7/22 9:46:08
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace NetInfo.Wpf.AvalonDock.Layout
{
    [ContentProperty("Children")]
    [Serializable]
    public class LayoutAnchorSide : LayoutGroup<LayoutAnchorGroup>
    {
        public LayoutAnchorSide()
        {
        }

        protected override bool GetVisibility()
        {
            return Children.Count > 0;
        }


        protected override void OnParentChanged(ILayoutContainer oldValue, ILayoutContainer newValue)
        {
            base.OnParentChanged(oldValue, newValue);

            UpdateSide();
        }

        private void UpdateSide()
        {
            if (Root.LeftSide == this)
                Side = AnchorSide.Left;
            else if (Root.TopSide == this)
                Side = AnchorSide.Top;
            else if (Root.RightSide == this)
                Side = AnchorSide.Right;
            else if (Root.BottomSide == this)
                Side = AnchorSide.Bottom;
        }


        #region Side

        private AnchorSide _side;
        public AnchorSide Side
        {
            get { return _side; }
            private set
            {
                if (_side != value)
                {
                    RaisePropertyChanging("Side");
                    _side = value;
                    RaisePropertyChanged("Side");
                }
            }
        }

        #endregion



    }
}
