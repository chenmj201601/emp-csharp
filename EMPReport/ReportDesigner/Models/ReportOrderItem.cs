//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    0e9a8cff-b574-45a5-b94f-8004d005e208
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportOrderItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ReportOrderItem
//
//        Created by Charley at 2017/6/1 15:31:59
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;
using NetInfo.EMP.Reports;


namespace ReportDesigner.Models
{
    public class ReportOrderItem : INotifyPropertyChanged
    {
        private DataFieldItem mField;

        public DataFieldItem Field
        {
            get { return mField; }
            set { mField = value; OnPropertyChanged("Field"); }
        }

        private ListItem mDirection;

        public ListItem Direction
        {
            get { return mDirection; }
            set { mDirection = value; OnPropertyChanged("Direction"); }
        }

        public ReportOrder Info { get; set; }


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
