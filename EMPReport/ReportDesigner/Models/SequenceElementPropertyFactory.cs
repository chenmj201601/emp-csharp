//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    4da37a10-dade-4cb6-b2a6-e06f64ae6a5a
//        CLR Version:              4.0.30319.42000
//        Name:                     SequenceElementPropertyFactory
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                SequenceElementPropertyFactory
//
//        Created by Charley at 2017/5/4 10:31:20
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ReportDesigner.Models
{
    /// <summary>
    /// 序列单元格属性列表
    /// </summary>
    public class SequenceElementPropertyFactory
    {
        public static IList<ObjectPropertyInfo> GetPropertyList()
        {
            var listProperties = TextElementPropertyFactory.GetPropertyList();
            ObjectPropertyInfo info = new ObjectPropertyInfo();
            info.ID = PRO_DATASET;
            info.Name = "数据集";
            info.GroupID = 0;
            info.SortID = 1;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "";
            listProperties.Add(info);
            info = new ObjectPropertyInfo();
            info.ID = PRO_DATAFIELD;
            info.Name = "字段";
            info.GroupID = 0;
            info.SortID = 2;
            info.EditFormat = PropertyEditFormat.SingleSelect;
            info.DefaultValue = "";
            listProperties.Add(info);
            return listProperties.OrderBy(p => p.GroupID).ThenBy(p => p.ID).ToList();
        }

        public const int PRO_DATASET = 101;
        public const int PRO_DATAFIELD = 102;
    }
}
