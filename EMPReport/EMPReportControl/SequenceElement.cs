//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    e8c577b8-cfef-4fc4-ab84-d1b8d4264cc7
//        CLR Version:              4.0.30319.42000
//        Name:                     SequenceElement
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                SequenceElement
//
//        Created by Charley at 2017/4/26 15:58:42
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    public class SequenceElement : EditableElement
    {
        public ReportDataSet DataSet { get; set; }
        public ReportDataField DataField { get; set; }
    }
}
