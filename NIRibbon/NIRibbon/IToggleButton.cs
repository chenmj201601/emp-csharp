//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    826743c3-17ff-46db-ab06-b6c677c16ed2
//        CLR Version:              4.0.30319.42000
//        Name:                     IToggleButton
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Ribbon
//        File Name:                IToggleButton
//
//        Created by Charley at 2017/4/11 9:54:45
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Controls.Primitives;


namespace NetInfo.Ribbon
{
    /// <summary>
    /// Interface for controls that support <see cref="ToggleButton"/>-Behavior
    /// </summary>
    public interface IToggleButton
    {
        /// <summary>
        /// Gets or sets the name of the group that the toggle button belongs to. 
        /// Use the GroupName property to specify a grouping of toggle buttons to 
        /// create a mutually exclusive set of controls. You can use the GroupName 
        /// property when only one selection is possible from a list of available 
        /// options. When this property is set, only one ToggleButton in the specified
        /// group can be selected at a time.
        /// </summary>
        string GroupName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SplitButton is checked
        /// </summary>
        bool? IsChecked { get; set; }

        /// <summary>
        /// Gets a value that indicates whether the ToggleButton is fully loaded
        /// </summary>
        bool IsLoaded { get; }
    }
}
