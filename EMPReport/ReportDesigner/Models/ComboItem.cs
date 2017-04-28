//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    564526c5-44f7-4211-90d4-98bf14f64a02
//        CLR Version:              4.0.30319.42000
//        Name:                     ComboItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ComboItem
//
//        Created by Charley at 2017/4/17 14:34:55
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;


namespace ReportDesigner.Models
{
    public class ComboItem : INotifyPropertyChanged
    {
        private string mName;
        private string mDisplay;
        private string mTip;
        private int mIntValue;

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
