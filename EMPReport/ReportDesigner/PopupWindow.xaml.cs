//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    b5a2047e-41b5-4bba-94dd-5f3d58cec486
//        CLR Version:              4.0.30319.42000
//        Name:                     PopupWindow
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner
//        File Name:                PopupWindow
//
//        Created by Charley at 2017/4/25 9:56:40
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows;

namespace ReportDesigner
{
    /// <summary>
    /// PopupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopupWindow
    {
        private bool mIsInited;

        public PopupWindow()
        {
            InitializeComponent();

            Loaded += PopupWindow_Loaded;
        }

        void PopupWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {

        }
    }
}
