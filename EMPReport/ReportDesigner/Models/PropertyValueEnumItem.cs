//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    7bd5f5fd-0dc4-4e55-97c3-c0c401027254
//        CLR Version:              4.0.30319.42000
//        Name:                     PropertyValueEnumItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                PropertyValueEnumItem
//
//        Created by Charley at 2017/4/28 17:32:31
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.ComponentModel;


namespace ReportDesigner.Models
{
    public class PropertyValueEnumItem
    {
        private string mValue;
        public string Value
        {
            get { return mValue; }
            set { mValue = value; OnPropertyChanged("Value"); }
        }

        private string mDisplay;

        public string Display
        {
            get { return mDisplay; }
            set { mDisplay = value; OnPropertyChanged("Display"); }
        }

        private string mDescription;

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; OnPropertyChanged("Description"); }
        }

        private bool mIsChecked;

        public bool IsChecked
        {
            get { return mIsChecked; }
            set { mIsChecked = value; OnPropertyChanged("IsChecked"); OnIsCheckedChanged(); }
        }

        private bool mIsSelected;

        public bool IsSelected
        {
            get { return mIsSelected; }
            set { mIsSelected = value; OnPropertyChanged("IsSelected"); }
        }

        private int mSortID;

        public int SortID
        {
            get { return mSortID; }
            set { mSortID = value; OnPropertyChanged("SortID"); }
        }

        private string mIcon { get; set; }

        public string Icon
        {
            get { return mIcon; }
            set { mIcon = value; OnPropertyChanged("Icon"); }
        }
        public object Info { get; set; }

        #region PropertyChangedEvent

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        #endregion


        #region IsCheckedChangedEvent

        public event Action IsCheckedChanged;

        private void OnIsCheckedChanged()
        {
            if (IsCheckedChanged != null)
            {
                IsCheckedChanged();
            }
        }

        #endregion


        public override string ToString()
        {
            string str;
            if (!string.IsNullOrEmpty(Display))
            {
                str = Display;
            }
            else
            {
                str = string.Format("[{0}]", Value);
            }
            return str;
        }
    }
}
