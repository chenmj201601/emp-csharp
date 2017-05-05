//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    909e9db2-18a6-4693-a041-df9f631ecc56
//        CLR Version:              4.0.30319.42000
//        Name:                     ColorItem
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Wpf.Controls
//        File Name:                ColorItem
//
//        Created by Charley at 2017/5/3 18:00:19
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Media;


namespace NetInfo.Wpf.Controls
{
    public class ColorItem
    {
        public Color Color
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public ColorItem(Color color, string name)
        {
            Color = color;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            var ci = obj as ColorItem;
            if (ci == null)
                return false;
            return (ci.Color.Equals(Color) && ci.Name.Equals(Name));
        }

        public override int GetHashCode()
        {
            return this.Color.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}
