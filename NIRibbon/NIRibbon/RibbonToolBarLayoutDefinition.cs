//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1bbff4af-e463-4121-a151-b82f99f61a26
//        CLR Version:              4.0.30319.42000
//        Name:                     RibbonToolBarLayoutDefinition
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                RibbonToolBarLayoutDefinition
//
//        Created by Charley at 2017/4/11 10:18:43
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Markup;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents size definition for group box
    /// </summary>
    [ContentProperty("Rows")]
    public class RibbonToolBarLayoutDefinition : DependencyObject
    {
        #region Fields

        // User defined rows
        ObservableCollection<RibbonToolBarRow> rows = new ObservableCollection<RibbonToolBarRow>();

        #endregion

        #region Properties


        #region Row Count

        /// <summary>
        /// Gets or sets count of rows in the ribbon toolbar
        /// </summary>
        public int RowCount
        {
            get { return (int)GetValue(RowCountProperty); }
            set { SetValue(RowCountProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for RowCount.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty RowCountProperty =
            DependencyProperty.Register("RowCount", typeof(int), typeof(RibbonToolBar), new UIPropertyMetadata(3));


        #endregion


        /// <summary>
        /// Gets rows
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ObservableCollection<RibbonToolBarRow> Rows
        {
            get { return rows; }
        }

        #endregion
    }
}
