//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    0f609bcd-8167-434e-bc7c-b56ec7e4c522
//        CLR Version:              4.0.30319.42000
//        Name:                     ColorViewModel
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                ColorViewModel
//
//        Created by Charley at 2017/4/24 11:30:40
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Media;


namespace ReportDesigner.Models
{
    public class ColorViewModel : ViewModel
    {
        private Color mFontColor;
        private Color mFillColor;
        private Color mBorderColor;

        public ColorViewModel()
        {
            mFontColor = Colors.Black;
            mFillColor = Colors.Transparent;
            mBorderColor = Colors.Gray;
        }

        public Color FontColor
        {
            get { return mFontColor; }
            set { mFontColor = value; OnPropertyChanged("FontColor"); }
        }

        public Color FillColor
        {
            get { return mFillColor; }
            set { mFillColor = value; OnPropertyChanged("FillColor"); }
        }

        public Color BorderColor
        {
            get { return mBorderColor; }
            set { mBorderColor = value; OnPropertyChanged("BorderColor"); }
        }
    }
}
