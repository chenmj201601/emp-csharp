//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    a482a398-ac6d-4fbf-a4e9-0682db475e36
//        CLR Version:              4.0.30319.42000
//        Name:                     CellStyleViewModel
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                CellStyleViewModel
//
//        Created by Charley at 2017/4/26 11:54:42
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Media;
using NetInfo.EMP.Reports;


namespace ReportDesigner.Models
{
    public class CellStyleViewModel : ViewModel
    {
        private string mName;

        public string Name
        {
            get { return mName; }
            set { mName = value; OnPropertyChanged("Name"); }
        }

        private FontFamily mFontFamily;

        public FontFamily FontFamily
        {
            get { return mFontFamily; }
            set { mFontFamily = value; OnPropertyChanged("FontFamily"); }
        }

        private int mFontSize;

        public int FontSize
        {
            get { return mFontSize; }
            set { mFontSize = value; OnPropertyChanged("FontSize"); }
        }

        private FontWeight mFontWeight;

        public FontWeight FontWeight
        {
            get { return mFontWeight; }
            set { mFontWeight = value; OnPropertyChanged("FontWeight"); }
        }

        private System.Windows.FontStyle mFontStyle;

        public System.Windows.FontStyle FontStyle
        {
            get { return mFontStyle; }
            set { mFontStyle = value; OnPropertyChanged("FontStyle"); }
        }

        private Brush mForeground;

        public Brush Foreground
        {
            get { return mForeground; }
            set { mForeground = value; OnPropertyChanged("Foreground"); }
        }

        private Brush mBackground;

        public Brush Background
        {
            get { return mBackground; }
            set { mBackground = value; OnPropertyChanged("Background"); }
        }

        private HorizontalAlignment mHorizontalAlignment;

        public HorizontalAlignment HorizontalAlignment
        {
            get { return mHorizontalAlignment; }
            set { mHorizontalAlignment = value; OnPropertyChanged("HorizontalAlignment"); }
        }

        private VerticalAlignment mVerticalAlignment;

        public VerticalAlignment VerticalAlignment
        {
            get { return mVerticalAlignment; }
            set { mVerticalAlignment = value; OnPropertyChanged("VerticalAlignment"); }
        }

        public void SetStyle()
        {
            if (Style == null) { return; }
            FontFamily = new FontFamily(Style.FontFamily);
            FontSize = Style.FontSize;
            FontWeight = (Style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Bold) > 0
                ? FontWeights.Bold
                : FontWeights.Normal;
            FontStyle = (Style.FontStyle & (int)NetInfo.EMP.Reports.FontStyle.Italic) > 0
                ? FontStyles.Italic
                : FontStyles.Normal;
            if (!string.IsNullOrEmpty(Style.Foreground))
            {
                var color = ColorConverter.ConvertFromString(Style.Foreground);
                if (color != null)
                {
                    Foreground = new SolidColorBrush((Color)color);
                }
            }
            if (!string.IsNullOrEmpty(Style.Background))
            {
                var color = ColorConverter.ConvertFromString(Style.Background);
                if (color != null)
                {
                    Background = new SolidColorBrush((Color)color);
                }
            }
            HorizontalAlignment = (HorizontalAlignment)Style.HorizontalAlignment;
            VerticalAlignment = (VerticalAlignment)Style.VerticalAlignment;
        }

        public VisualStyle Style { get; set; }
    }
}
