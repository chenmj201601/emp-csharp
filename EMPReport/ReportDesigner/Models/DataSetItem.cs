//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    41e2d5ed-9323-40db-b506-8e48df33c7cc
//        CLR Version:              4.0.30319.42000
//        Name:                     DataSetItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DataSetItem
//
//        Created by Charley at 2017/4/26 16:22:46
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;
using System.Windows.Media;


namespace ReportDesigner.Models
{
    public class DataSetItem : INotifyPropertyChanged
    {
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; OnPropertyChanged("Name"); }
        }

        private string mTip;

        public string Tip
        {
            get { return mTip; }
            set { mTip = value; OnPropertyChanged("Tip"); }
        }

        private ImageSource mIcon;

        public ImageSource Icon
        {
            get { return mIcon; }
            set { mIcon = value; OnPropertyChanged("Icon"); }
        }

        public object Data { get; set; }

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
