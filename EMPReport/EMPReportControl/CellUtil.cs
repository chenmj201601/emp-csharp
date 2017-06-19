//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    0609a0a0-fb63-495e-9c9f-023800a016ee
//        CLR Version:              4.0.30319.42000
//        Name:                     CellUtil
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                CellUtil
//
//        Created by Charley at 2017/6/5 10:23:16
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    public class CellUtil
    {
        public static readonly char[] ColumnTitles =
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        public static string GetCellTitle(CellBase cell)
        {
            int rowIndex = cell.RowIndex;
            int colIndex = cell.ColumnIndex;
            string col = string.Empty;
            if (colIndex > 0)
            {
                col = ColumnTitles[colIndex - 1].ToString();
            }
            return col + rowIndex;
        }

        public static string GetCellKey(CellBase cell)
        {
            int rowIndex = cell.RowIndex;
            int colIndex = cell.ColumnIndex;
            return string.Format("{0:D03}{1:D03}", rowIndex, colIndex);
        }
    }
}
