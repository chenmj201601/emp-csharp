//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f9f2ceb7-cd0a-41eb-a878-c2922eea8845
//        CLR Version:              4.0.30319.42000
//        Name:                     ImageElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                ImageElement
//
//        Created by Charley at 2017/5/25 10:29:02
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace NetInfo.EMP.Reports.Controls
{
    public class ImageElement : Control, ICellElement
    {

        static ImageElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageElement),
                new FrameworkPropertyMetadata(typeof(ImageElement)));
        }


        #region Text

        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(ImageElement), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion


        #region Stretch

        public static readonly DependencyProperty StretchProperty =
           DependencyProperty.Register("Stretch", typeof(Stretch), typeof(ImageElement), new PropertyMetadata(default(Stretch)));

        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        #endregion


        #region ImageWidth

        public static readonly DependencyProperty ImageWidthProperty =
           DependencyProperty.Register("ImageWidth", typeof(int), typeof(ImageElement), new PropertyMetadata(default(int)));

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        #endregion


        #region ImageHeight

        public static readonly DependencyProperty ImageHeightProperty =
           DependencyProperty.Register("ImageHeight", typeof(int), typeof(ImageElement), new PropertyMetadata(default(int)));

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        #endregion


        #region ImageSource

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageElement), new PropertyMetadata(default(ImageSource)));

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        #endregion


        #region SourceFile

        public string SourceFile { get; set; }

        #endregion


        #region IsSourceUpdated

        public bool IsSourceUpdated { get; set; }

        #endregion


        #region CellElement

        public string LinkUrl { get; set; }

        public GridCell Cell { get; set; }

        #endregion


        #region ReportImage

        public string ID { get; set; }
        public int Extension { get; set; }

        #endregion


        #region Other

        public void SetExt(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) { return; }
            int index = fullName.LastIndexOf('.');
            if (index > 0)
            {
                string strExt = fullName.Substring(index);
                if (strExt.ToLower().Equals(".png"))
                {
                    Extension = ReportImage.EXT_PNG;
                }
                if (strExt.ToLower().Equals(".bmp"))
                {
                    Extension = ReportImage.EXT_BMP;
                }
                if (strExt.ToLower().Equals(".jpg"))
                {
                    Extension = ReportImage.EXT_JPG;
                }
                if (strExt.ToLower().Equals(".jpeg"))
                {
                    Extension = ReportImage.EXT_JPEG;
                }
            }
        }

        public string GetExtName()
        {
            string ext = ".png";
            if (Extension == ReportImage.EXT_PNG)
            {
                ext = ".png";
            }
            if (Extension == ReportImage.EXT_BMP)
            {
                ext = ".bmp";
            }
            if (Extension == ReportImage.EXT_JPG)
            {
                ext = ".jpg";
            }
            if (Extension == ReportImage.EXT_JPEG)
            {
                ext = ".jpeg";
            }
            return ext;
        }

        #endregion


        #region 创建一个ImageElement对象

        public static ImageElement FromReport(ReportImage reportImage)
        {
            ImageElement imageElement = new ImageElement();
            imageElement.ID = reportImage.ID;
            imageElement.Text = reportImage.Alt;
            imageElement.ImageWidth = reportImage.Width;
            imageElement.ImageHeight = reportImage.Height;
            imageElement.Stretch = (Stretch)reportImage.Stretch;
            imageElement.Extension = reportImage.Extension;
            return imageElement;
        }

        #endregion


        #region 生成一个报表对象

        public ReportImage ToReport()
        {
            ReportImage reportImage = new ReportImage();
            reportImage.ID = ID;
            reportImage.Alt = Text;
            reportImage.Width = ImageWidth;
            reportImage.Height = ImageHeight;
            reportImage.Stretch = (int)Stretch;
            reportImage.Extension = Extension;
            return reportImage;
        }

        #endregion

    }
}
