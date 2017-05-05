//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5091c329-115f-48f8-a4a9-fe7ecc9e72bb
//        CLR Version:              4.0.30319.42000
//        Name:                     HsvColor
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls.Primitives
//        File Name:                HsvColor
//
//        Created by Charley at 2017/5/3 18:03:28
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.Wpf.Controls.Primitives
{
    internal struct HsvColor
    {
        public double H;
        public double S;
        public double V;

        public HsvColor(double h, double s, double v)
        {
            H = h;
            S = s;
            V = v;
        }
    }
}
