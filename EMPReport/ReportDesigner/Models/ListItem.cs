//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    96ba9fff-9b5b-4d64-af30-b1f91b964755
//        CLR Version:              4.0.30319.42000
//        Name:                     ListItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ListItem
//
//        Created by Charley at 2017/4/26 17:39:45
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;


namespace ReportDesigner.Models
{
    public class ListItem : INotifyPropertyChanged
    {
        private string mName;
        private string mDisplay;
        private string mTip;
        private int mIntValue;
        private string mStrValue;

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

        public int IntValue
        {
            get { return mIntValue; }
            set { mIntValue = value; OnPropertyChanged("IntValue"); }
        }

        public string StrValue
        {
            get { return mStrValue; }
            set { mStrValue = value; OnPropertyChanged("StrValue"); }
        }

        public object Data { get; set; }

        public override string ToString()
        {
            return Display;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
