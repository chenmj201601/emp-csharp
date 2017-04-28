//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9839f555-4ec9-4bc4-9beb-6a0736fb8a0a
//        CLR Version:              4.0.30319.42000
//        Name:                     GalleryGroupFilter
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                GalleryGroupFilter
//
//        Created by Charley at 2017/4/11 9:43:54
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents gallery group filter definition
    /// </summary>
    public class GalleryGroupFilter : DependencyObject
    {
        #region Properties

        /// <summary>
        /// Gets or sets title of filter
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Title.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string),
            typeof(GalleryGroupFilter), new UIPropertyMetadata("GalleryGroupFilter"));

        /// <summary>
        /// Gets or sets list pf groups splitted by comma
        /// </summary>
        public string Groups
        {
            get { return (string)GetValue(GroupsProperty); }
            set { SetValue(GroupsProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ContextualGroups.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty GroupsProperty =
            DependencyProperty.Register("ContextualGroups", typeof(string),
            typeof(GalleryGroupFilter), new UIPropertyMetadata(""));

        #endregion
    }
}
