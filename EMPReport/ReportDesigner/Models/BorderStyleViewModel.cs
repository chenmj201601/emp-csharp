//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    3bac8f01-7abc-40a5-b986-ed691a8ccf54
//        CLR Version:              4.0.30319.42000
//        Name:                     BorderStyleViewModel
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                BorderStyleViewModel
//
//        Created by Charley at 2017/4/17 16:22:09
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Media;


namespace ReportDesigner.Models
{
    public class BorderStyleViewModel : ViewModel
    {
        private string mName;
        private string mDisplay;
        private string mTip;
        private int mType;
        private ImageSource mIcon;

        public string Name
        {
            get { return mName; }
            set { mName = value; OnPropertyChanged("Name"); }
        }

        public string Display
        {
            get { return mDisplay; }
            set { mDisplay = value; OnPropertyChanged("Display"); }
        }

        public string Tip
        {
            get { return mTip; }
            set { mTip = value; OnPropertyChanged("Tip"); }
        }

        public int Type
        {
            get { return mType; }
            set { mType = value; OnPropertyChanged("Type"); }
        }

        public ImageSource Icon
        {
            get { return mIcon; }
            set { mIcon = value; OnPropertyChanged("Icon"); }
        }
    }
}
