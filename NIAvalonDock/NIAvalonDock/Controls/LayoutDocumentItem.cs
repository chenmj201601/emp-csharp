//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    a4d3c2ad-2235-4bfc-a8aa-fa5be39004d7
//        CLR Version:              4.0.30319.18444
//        Name:                     LayoutDocumentItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.AvalonDock.Controls
//        File Name:                LayoutDocumentItem
//
//        created by Charley at 2014/7/22 10:07:31
//        http://www.netinfo.com 
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using NetInfo.Wpf.AvalonDock.Layout;

namespace NetInfo.Wpf.AvalonDock.Controls
{
    public class LayoutDocumentItem : LayoutItem
    {
        LayoutDocument _document;
        internal LayoutDocumentItem()
        {

        }

        internal override void Attach(LayoutContent model)
        {
            _document = model as LayoutDocument;
            base.Attach(model);
        }

        protected override void Close()
        {
            var dockingManager = _document.Root.Manager;
            dockingManager._ExecuteCloseCommand(_document);
        }

        #region Description

        /// <summary>
        /// Description Dependency Property
        /// </summary>
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(LayoutDocumentItem),
                new FrameworkPropertyMetadata((string)null,
                    new PropertyChangedCallback(OnDescriptionChanged)));

        /// <summary>
        /// Gets or sets the Description property.  This dependency property 
        /// indicates the description to display for the document item.
        /// </summary>
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Description property.
        /// </summary>
        private static void OnDescriptionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LayoutDocumentItem)d).OnDescriptionChanged(e);
        }

        /// <summary>
        /// Provides derived classes an opportunity to handle changes to the Description property.
        /// </summary>
        protected virtual void OnDescriptionChanged(DependencyPropertyChangedEventArgs e)
        {
            _document.Description = (string)e.NewValue;
        }

        #endregion

        internal override void Detach()
        {
            _document = null;
            base.Detach();
        }
    }
}
