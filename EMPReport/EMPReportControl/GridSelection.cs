//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    5b60a09a-dd90-400e-ab9a-ff6d7a9fb26e
//        CLR Version:              4.0.30319.42000
//        Name:                     GridSelection
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridSelection
//
//        Created by Charley at 2017/4/13 11:19:13
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    /// <summary>
    /// 网格选择的坐标信息
    /// </summary>
    public class GridSelection
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public override string ToString()
        {
            return string.Format("L:{0:0.0}; T:{1:0.0}; W:{2:0.0}; H:{3:0.0}", Left, Top, Width, Height);
        }
    }
}
