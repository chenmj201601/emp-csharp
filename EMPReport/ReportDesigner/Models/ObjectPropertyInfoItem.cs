//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    98020246-83cf-4fdd-9ba3-72b184c5e78f
//        CLR Version:              4.0.30319.42000
//        Name:                     ObjectPropertyInfoItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ObjectPropertyInfoItem
//
//        Created by Charley at 2017/4/28 12:00:01
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;


namespace ReportDesigner.Models
{
    public class ObjectPropertyInfoItem : INotifyPropertyChanged
    {
        private string mPropertyName;

        public string PropertyName
        {
            get { return mPropertyName; }
            set { mPropertyName = value; OnPropertyChanged("PropertyName"); }
        }

        private string mGroupName;

        public string GroupName
        {
            get { return mGroupName; }
            set { mGroupName = value; OnPropertyChanged("GroupName"); }
        }

        private string mValue;

        public string Value
        {
            get { return mValue; }
            set { mValue = value; OnPropertyChanged("Value"); }
        }

        public int ID { get; set; }

        public ObjectPropertyInfo Info { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
