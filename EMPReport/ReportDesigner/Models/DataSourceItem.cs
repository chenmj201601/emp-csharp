//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    0aba7e97-baa5-4336-88dd-81dc295c9e7c
//        CLR Version:              4.0.30319.42000
//        Name:                     DataSourceItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DataSourceItem
//
//        Created by Charley at 2017/4/24 17:41:40
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;
using System.Windows.Media;


namespace ReportDesigner.Models
{
    public class DataSourceItem : INotifyPropertyChanged
    {
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; OnPropertyChanged("Name"); }
        }

        private string mDetail;

        public string Detail
        {
            get { return mDetail; }
            set { mDetail = value; OnPropertyChanged("Detail"); }
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
