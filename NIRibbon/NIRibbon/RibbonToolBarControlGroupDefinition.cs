//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b260956f-1543-4c93-8dcb-6d4bebb8223f
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonToolBarControlGroupDefinition
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonToolBarControlGroupDefinition
//
//        Created by Charley at 2017/4/11 10:18:09
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represent logical container for toolbar items
    /// </summary>
    [ContentProperty("Children")]
    public class RibbonToolBarControlGroupDefinition : DependencyObject
    {
        #region Events

        /// <summary>
        /// Occures when children has been changed
        /// </summary>
        public event NotifyCollectionChangedEventHandler ChildrenChanged;

        #endregion

        #region Fields

        // User defined rows
        readonly ObservableCollection<RibbonToolBarControlDefinition> children = new ObservableCollection<RibbonToolBarControlDefinition>();

        #endregion

        #region Children Property

        /// <summary>
        /// Gets rows
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<RibbonToolBarControlDefinition> Children
        {
            get { return children; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Default constructor
        /// </summary>
        public RibbonToolBarControlGroupDefinition()
        {
            children.CollectionChanged += OnChildrenCollectionChanged;
        }

        void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (ChildrenChanged != null) ChildrenChanged(sender, e);
        }

        #endregion
    }
}
