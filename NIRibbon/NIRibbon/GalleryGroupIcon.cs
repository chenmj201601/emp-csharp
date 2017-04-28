//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    40708422-201b-4e84-8154-5924cc5dd229
//        CLR Version:              4.0.30319.42000
//        Name:                     GalleryGroupIcon
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                GalleryGroupIcon
//
//        Created by Charley at 2017/4/11 9:44:33
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Media;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Represents gallery group icon definition
    /// </summary>
    public class GalleryGroupIcon : DependencyObject
    {
        /// <summary>
        /// Gets or sets group name
        /// </summary>
        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for GroupName.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty GroupNameProperty =
            DependencyProperty.Register("GroupName", typeof(string),
            typeof(GalleryGroupIcon), new UIPropertyMetadata(null));


        /// <summary>
        /// Gets or sets group icon
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for Icon.  
        /// This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(GalleryGroupIcon),
                                        new UIPropertyMetadata(null));
    }
}
