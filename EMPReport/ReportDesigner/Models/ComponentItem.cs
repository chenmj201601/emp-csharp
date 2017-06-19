//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b1fc604c-8b1c-4bd9-b878-fd8629a63c73
//        CLR Version:              4.0.30319.42000
//        Name:                     ComponentItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ComponentItem
//
//        Created by Charley at 2017/5/12 13:59:28
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using NetInfo.EMP.Reports;
using NetInfo.EMP.Reports.Controls;
using NetInfo.Wpf.Controls;


namespace ReportDesigner.Models
{
    public class ComponentItem : INotifyPropertyChanged, IListItemObject
    {
        private string mName;
        private string mDisplay;
        private string mDescription;
        private ImageSource mIcon;
        private string mGroupName;

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

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; OnPropertyChanged("Description"); }
        }

        public ImageSource Icon
        {
            get { return mIcon; }
            set { mIcon = value; OnPropertyChanged("Icon"); }
        }

        public string GroupName
        {
            get { return mGroupName; }
            set { mGroupName = value; OnPropertyChanged("GroupName"); }
        }

        public ComponentInfo Info { get; set; }


        public ICellElement CreateCellElement()
        {
            var info = Info;
            if (info == null)
            {
                return null;
            }
            var reportElement = info.Element;
            if (reportElement == null)
            {
                return null;
            }
            ICellElement cellElement = null;
            var reportText = reportElement as ReportText;
            if (reportText != null)
            {
                TextElement textElement = TextElement.FromReport(reportText);
                cellElement = textElement;
            }
            var reportSequence = reportElement as ReportSequence;
            if (reportSequence != null)
            {
                SequenceElement sequenceElement = SequenceElement.FromReport(reportSequence);
                cellElement = sequenceElement;
            }
            var reportImage = reportElement as ReportImage;
            if (reportImage != null)
            {
                ImageElement imageElement = ImageElement.FromReport(reportImage);
                imageElement.ID = Guid.NewGuid().ToString();        //ID需要重新生成一个
                string strID = reportImage.ID;
                string strExt = imageElement.GetExtName();
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "components");
                path = Path.Combine(path, "resources");
                path = Path.Combine(path, string.Format("{0}{1}", strID, strExt));
                if (File.Exists(path))
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
                    imageElement.ImageSource = bitmap.Clone();
                    imageElement.SourceFile = path;
                    imageElement.IsSourceUpdated = true;
                }
                cellElement = imageElement;
            }
            return cellElement;
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
