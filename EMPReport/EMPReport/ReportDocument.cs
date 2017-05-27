//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    725c88d7-88f6-4436-b209-09c47c31c278
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportDocument
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                ReportDocument
//
//        Created by Charley at 2017/4/10 15:45:26
//        http://www.netinfo.com 
//
//======================================================================

using System.Collections.Generic;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    [XmlRoot(Namespace = "http://netinfo.com/emp/reports")]
    public class ReportDocument
    {
        /// <summary>
        /// 报表名称（文件名）
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }
        /// <summary>
        /// 报表标题
        /// </summary>
        [XmlAttribute]
        public string Title { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [XmlIgnore]
        public string Path { get; set; }

        [XmlElement]
        public ReportGrid Grid { get; set; }

        private readonly List<ReportDataSet> mDataSets = new List<ReportDataSet>();

        [XmlArray(ElementName = "DataSets")]
        [XmlArrayItem(ElementName = "DataSet")]
        public List<ReportDataSet> DataSets
        {
            get { return mDataSets; }
        }

        private readonly List<ReportCell> mCells = new List<ReportCell>();

        [XmlArray(ElementName = "Cells")]
        [XmlArrayItem(ElementName = "Cell")]
        public List<ReportCell> Cells
        {
            get { return mCells; }
        }

        private readonly List<VisualStyle> mStyles = new List<VisualStyle>();

        [XmlArray(ElementName = "Styles")]
        [XmlArrayItem(ElementName = "Style")]
        public List<VisualStyle> Styles
        {
            get { return mStyles; }
        }

        public void Init()
        {
            for (int i = 0; i < DataSets.Count; i++)
            {
                var dataSet = DataSets[i];
                dataSet.Init();
            }
        }
    }
}
