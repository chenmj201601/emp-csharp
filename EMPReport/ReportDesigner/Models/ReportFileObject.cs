//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cff37cd3-69d1-4201-8415-6ca325aeed4f
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportFileObject
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ReportFileObject
//
//        Created by Charley at 2017/4/19 16:06:15
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ReportDesigner.Models
{
    public class ReportFileObject : INotifyPropertyChanged
    {
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; OnPropertyChanged("Name"); }
        }

        private int mType;

        /// <summary>
        /// 类型：0：根目录；1：文件夹；2：文件
        /// </summary>
        public int Type
        {
            get { return mType; }
            set { mType = value; OnPropertyChanged("Type"); }
        }

        private string mIcon;

        public string Icon
        {
            get { return mIcon; }
            set { mIcon = value; OnPropertyChanged("Icon"); }
        }

        private bool mIsExpanded;

        public bool IsExpanded
        {
            get { return mIsExpanded; }
            set { mIsExpanded = value; OnPropertyChanged("IsExpanded"); }
        }

        public ReportFileObject Parent { get; set; }

        public object Data { get; set; }

        private ObservableCollection<ReportFileObject> mChildren = new ObservableCollection<ReportFileObject>();

        public ObservableCollection<ReportFileObject> Children
        {
            get { return mChildren; }
        }

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
