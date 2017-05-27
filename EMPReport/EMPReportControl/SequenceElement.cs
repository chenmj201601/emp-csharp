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

using System.Windows;
using System.Windows.Controls;

namespace NetInfo.EMP.Reports.Controls
{
    public class SequenceElement : Control, ICellElement
    {
        static SequenceElement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SequenceElement),
                new FrameworkPropertyMetadata(typeof(SequenceElement)));
        }


        #region Template

        private const string PART_TextBlock = "PART_TextBlock";
        private TextBlock mTextBlock;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            mTextBlock = GetTemplateChild(PART_TextBlock) as TextBlock;
            if (mTextBlock != null)
            {

            }
        }

        #endregion


        #region Others

        public TextBlock TextBlock
        {
            get { return mTextBlock; }
        }

        #endregion


        #region Text

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SequenceElement), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion


        #region CellElement

        public string LinkUrl { get; set; }

        public GridCell Cell { get; set; }

        #endregion


        #region ReportSequence

        public ReportDataSet DataSet { get; set; }
        public ReportDataField DataField { get; set; }
        public int ExtMethod { get; set; }
        public bool IsMerge { get; set; }

        #endregion


        #region 创建一个SequenceElement对象

        public static SequenceElement FromReport(ReportSequence reportSequence)
        {
            SequenceElement sequenceElement = new SequenceElement();
            sequenceElement.Text = reportSequence.Expression;
            sequenceElement.ExtMethod = reportSequence.ExtMethod;
            sequenceElement.IsMerge = reportSequence.IsMerge == 1;
            return sequenceElement;
        }

        #endregion


        #region 生成一个报表对象

        public ReportSequence ToReport()
        {
            ReportSequence reportSequence = new ReportSequence();
            reportSequence.Expression = Text;
            reportSequence.ExtMethod = ExtMethod;
            reportSequence.IsMerge = IsMerge ? 1 : 0;
            if (DataSet != null)
            {
                reportSequence.DataSetName = DataSet.Name;
            }
            if (DataField != null)
            {
                reportSequence.DataTableName = DataField.TableName;
                reportSequence.DataFieldName = DataField.Name;
            }
            return reportSequence;
        }

        #endregion

    }
}
