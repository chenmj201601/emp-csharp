//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    668141c5-e05d-46a8-a55a-1faa7b6fd2d3
//        CLR Version:              4.0.30319.42000
//        Name:                     DataFieldItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DataFieldItem
//
//        Created by Charley at 2017/5/31 15:23:43
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;
using NetInfo.EMP.Reports;


namespace ReportDesigner.Models
{
    public class DataFieldItem:INotifyPropertyChanged
    {
        private string mName;
        private string mDisplay;
        private string mTip;
        private string mFieldName;
        private string mTableName;
        private int mDataType;

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

        public string FieldName
        {
            get { return mFieldName; }
            set { mFieldName = value; OnPropertyChanged("FieldName"); }
        }

        public string TableName
        {
            get { return mTableName; }
            set { mTableName = value; OnPropertyChanged("TableName"); }
        }

        public int DataType
        {
            get { return mDataType; }
            set { mDataType = value; OnPropertyChanged("DataType"); }
        }

        public ReportDataField Info { get; set; }

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
