//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    86b2c7e2-b432-42fb-bf6a-31ed4b581508
//        CLR Version:              4.0.30319.42000
//        Name:                     EditableElementEventArgs
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports.Controls
//        File Name:                EditableElementEventArgs
//
//        Created by Charley at 2017/5/18 16:22:45
//        http://www.netinfo.com 
//
//======================================================================

namespace NetInfo.EMP.Reports.Controls
{
    public class EditableElementEventArgs
    {
        public int Code { get; set; }
        public EditableElement Element { get; set; }
        public object Data { get; set; }

        /// <summary>
        /// 文本改变
        /// </summary>
        public const int EVT_TEXT_CHANGED = 101;
        /// <summary>
        /// 编辑完成，由编辑状态变回普通状态
        /// </summary>
        public const int EVT_EDIT_END = 201;
    }
}
