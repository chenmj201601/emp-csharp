//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    79dad26e-f534-447b-b216-ef8f8058346b
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportConditionItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ReportConditionItem
//
//        Created by Charley at 2017/5/31 11:12:25
//        http://www.netinfo.com 
//
//======================================================================

using System.ComponentModel;
using NetInfo.EMP.Reports;


namespace ReportDesigner.Models
{
    public class ReportConditionItem : INotifyPropertyChanged
    {

        private DataFieldItem mField;

        public DataFieldItem Field
        {
            get { return mField; }
            set { mField = value; OnPropertyChanged("Field"); }
        }

        private ListItem mJudge;

        public ListItem Judge
        {
            get { return mJudge; }
            set { mJudge = value; OnPropertyChanged("Judge"); }
        }

        private string mValue;

        public string Value
        {
            get { return mValue; }
            set { mValue = value; OnPropertyChanged("Value"); }
        }

        private ListItem mRelation;

        public ListItem Relation
        {
            get { return mRelation; }
            set { mRelation = value; OnPropertyChanged("Relation"); }
        }

        public ReportCondition Info { get; set; }


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
