//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    03ece68a-87d1-4728-adda-66ed3e3e56e4
//        CLR Version:              4.0.30319.42000
//        Name:                     GridSelectedEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                GridSelectedEventArgs
//
//        Created by Charley at 2017/4/14 17:15:57
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;


namespace NetInfo.EMP.Reports.Controls
{
    public class GridSelectedEventArgs
    {
        public DesignGrid Grid { get; set; }
        public GridSelection Selection { get; set; }

        private readonly List<GridCell> mSelectedCells = new List<GridCell>();

        public List<GridCell> SelectedCells
        {
            get { return mSelectedCells; }
        }
    }
}
