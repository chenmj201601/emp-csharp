//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    9f3044dd-ceab-4d32-8d8f-c161ce646c3a
//        CLR Version:              4.0.30319.42000
//        Name:                     ReportDesignerCommand
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Commands
//        File Name:                ReportDesignerCommand
//
//        Created by Charley at 2017/4/17 18:40:43
//        http://www.netinfo.com 
//
//======================================================================

using System.Windows.Input;


namespace ReportDesigner.Commands
{
    public class ReportDesignerCommands
    {
        private static RoutedUICommand mAppAboutCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mPreviewCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mReportFileDeleteCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mDirCreateCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mCellMergeCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mCellUnmergeCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mInsertTextCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mInsertImageCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mInsertSequenceCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mInsertChartCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mDataSourceAddCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mDataSourceModifyCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mDataSourceDeleteCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mDataSetAddCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mDataSetModifyCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mDataSetDeleteCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mViewPanelCheckCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mLayoutSaveCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mLayoutRestoreCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mSaveAsStyleCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mSaveAsComponentCommand = new RoutedUICommand();

        private static readonly RoutedUICommand mCellStyleDeleteCommand = new RoutedUICommand();
        private static readonly RoutedUICommand mComponentDeleteCommand = new RoutedUICommand();

        public static RoutedUICommand AppAboutCommand
        {
            get { return mAppAboutCommand; }
        }

        public static RoutedUICommand PreviewCommand
        {
            get { return mPreviewCommand; }
        }

        public static RoutedUICommand ReportFileDeleteCommand
        {
            get { return mReportFileDeleteCommand; }
        }

        public static RoutedUICommand DirCreateCommand
        {
            get { return mDirCreateCommand; }
        }

        public static RoutedUICommand CellMergeCommand
        {
            get { return mCellMergeCommand; }
        }

        public static RoutedUICommand CellUnmergeCommand
        {
            get { return mCellUnmergeCommand; }
        }

        public static RoutedUICommand InsertTextCommand
        {
            get { return mInsertTextCommand; }
        }

        public static RoutedUICommand InsertImageCommand
        {
            get { return mInsertImageCommand; }
        }

        public static RoutedUICommand InsertSequenceCommand
        {
            get { return mInsertSequenceCommand; }
        }

        public static RoutedUICommand InsertChartCommand
        {
            get { return mInsertChartCommand; }
        }

        public static RoutedUICommand DataSourceAddCommand
        {
            get { return mDataSourceAddCommand; }
        }

        public static RoutedUICommand DataSourceModifyCommand
        {
            get { return mDataSourceModifyCommand; }
        }

        public static RoutedUICommand DataSourceDeleteCommand
        {
            get { return mDataSourceDeleteCommand; }
        }

        public static RoutedUICommand DataSetAddCommand
        {
            get { return mDataSetAddCommand; }
        }

        public static RoutedUICommand DataSetModifyCommand
        {
            get { return mDataSetModifyCommand; }
        }

        public static RoutedUICommand DataSetDeleteCommand
        {
            get { return mDataSetDeleteCommand; }
        }

        public static RoutedUICommand ViewPanelCheckCommand
        {
            get { return mViewPanelCheckCommand; }
        }

        public static RoutedUICommand LayoutSaveCommand
        {
            get { return mLayoutSaveCommand; }
        }

        public static RoutedUICommand LayoutRestoreCommand
        {
            get { return mLayoutRestoreCommand; }
        }

        public static RoutedUICommand SaveAsStyleCommand
        {
            get { return mSaveAsStyleCommand; }
        }

        public static RoutedUICommand SaveAsComponentCommand
        {
            get { return mSaveAsComponentCommand; }
        }

        public static RoutedUICommand CellStyleDeleteCommand
        {
            get { return mCellStyleDeleteCommand; }
        }

        public static RoutedUICommand ComponentDeleteCommand
        {
            get { return mComponentDeleteCommand; }
        }
    }
}
