//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    551151ac-724c-44a4-b1c1-2a48af94d65e
//        CLR Version:              4.0.30319.42000
//        Name:                     ViewModel
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ViewModel
//
//        Created by Charley at 2017/4/17 16:21:07
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;


namespace ReportDesigner.Models
{
    public class ViewModel:INotifyPropertyChanged
    {

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
