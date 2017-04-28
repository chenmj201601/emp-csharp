//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1ac34f9f-5480-4bd4-8f2c-6b0a6fe9de8b
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonToolBarRow
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonToolBarRow
//
//        Created by Charley at 2017/4/11 10:19:24
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Markup;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents size definition for group box
    /// </summary>
    [ContentProperty("Children")]
    [SuppressMessage("Microsoft.Naming", "CA1702", Justification = "We mean here 'bar row' instead of 'barrow'")]
    public class RibbonToolBarRow : DependencyObject
    {
        #region Fields

        // User defined rows
        readonly ObservableCollection<DependencyObject> children = new ObservableCollection<DependencyObject>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets rows
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<DependencyObject> Children
        {
            get { return children; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Default constructor
        /// </summary>
        public RibbonToolBarRow() { }

        #endregion
    }
}
