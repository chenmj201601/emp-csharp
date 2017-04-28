//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    232fcb51-b862-47e2-a5d0-1151a14d5ee3
//        CLR Version:              4.0.30319.42000
//        Name:                     UCDataSetWizard
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCDataSetWizard
//
//        Created by Charley at 2017/4/27 10:12:40
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using NetInfo.Common;
using NetInfo.DBAccesses;
using NetInfo.EMP.Reports;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCDataSetWizard.xaml 的交互逻辑
    /// </summary>
    public partial class UCDataSetWizard
    {

        #region Members

        public bool IsModify;
        public IList<DataSourceItem> ListAllDataSources;
        public IList<DataSetItem> ListAllDataSets;
        public DataSetItem DataSetItem;

        private bool mIsInited;
        private ObservableCollection<DataSourceItem> mListDataSourceItems = new ObservableCollection<DataSourceItem>();
        private ObservableCollection<ListItem> mListTables = new ObservableCollection<ListItem>();
        private ObservableCollection<ListItem> mListColumns = new ObservableCollection<ListItem>();
        private ObservableCollection<ListItem> mListFields = new ObservableCollection<ListItem>();
        private ObservableCollection<ListItem> mListConditions = new ObservableCollection<ListItem>();

        #endregion


        public UCDataSetWizard()
        {
            InitializeComponent();

            Loaded += UCDataSetWizard_Loaded;
            ComboDataSources.SelectionChanged += ComboDataSources_SelectionChanged;
            BtnClose.Click += BtnClose_Click;
            BtnConfirm.Click += BtnConfirm_Click;
            BtnNext.Click += BtnNext_Click;
            BtnPrevious.Click += BtnPrevious_Click;

            BtnFieldAdd.Click += BtnFieldAdd_Click;
            BtnFieldRemove.Click += BtnFieldRemove_Click;
            CheckBoxAllFields.Click += CheckBoxAllFields_Click;
            CheckBoxNoCondition.Click += CheckBoxNoCondition_Click;
        }


        void UCDataSetWizard_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }


        #region Init and Load

        private void Init()
        {
            ComboDataSources.ItemsSource = mListDataSourceItems;
            ListBoxTables.ItemsSource = mListTables;
            ListBoxColumns.ItemsSource = mListColumns;
            ListBoxFields.ItemsSource = mListFields;
            InitDataSourceItems();
            InitInfo();
        }

        private void InitDataSourceItems()
        {
            mListDataSourceItems.Clear();
            if (ListAllDataSources == null) { return; }
            for (int i = 0; i < ListAllDataSources.Count; i++)
            {
                mListDataSourceItems.Add(ListAllDataSources[i]);
            }
        }

        private void InitInfo()
        {
            if (IsModify)
            {
                TxtDataSetName.IsEnabled = false;
                if (DataSetItem == null) { return; }
                var reportDataSet = DataSetItem.Data as ReportDataSet;
                if (reportDataSet == null) { return; }
                string strDataSourceName = reportDataSet.DataSourceName;
                var dataSourceItem = mListDataSourceItems.FirstOrDefault(d => d.Name == strDataSourceName);
                ComboDataSources.SelectedItem = dataSourceItem;
                TxtDataSetName.Text = reportDataSet.Name;
            }
            else
            {
                TxtDataSetName.IsEnabled = true;
            }
        }

        private void LoadDataTables()
        {
            var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
            if (dataSourceItem == null) { return; }
            var dataSource = dataSourceItem.Data as DataSourceInfo;
            if (dataSource == null) { return; }
            var dbInfo = dataSource.DBInfo;
            if (dbInfo == null) { return; }
            string strConn = dbInfo.GetConnectionString();
            mListTables.Clear();
            mListColumns.Clear();
            mListFields.Clear();
            OperationReturn optReturn;
            string strSql;
            switch (dbInfo.TypeID)
            {
                case 1:
                    strSql =
                        string.Format("SELECT TABLE_NAME AS TABLE_NAME from information_schema.`TABLES` WHERE TABLE_SCHEMA = '{0}' ORDER BY TABLE_NAME",
                            dbInfo.DBName);
                    optReturn = MysqlOperation.GetDataSet(strConn, strSql);
                    break;
                case 2:
                    strSql =
                        string.Format("SELECT NAME AS TABLE_NAME FROM SYSOBJECTS WHERE XTYPE='U' ORDER BY NAME");
                    optReturn = MssqlOperation.GetDataSet(strConn, strSql);
                    break;
                case 3:
                    strSql =
                       string.Format("SELECT TABLE_NAME AS TABLE_NAME FROM USER_TABLES ORDER BY TABLE_NAME");
                    optReturn = OracleOperation.GetDataSet(strConn, strSql);
                    break;
                default:
                    ShowException(string.Format("DBType invalid."));
                    return;
            }
            if (!optReturn.Result)
            {
                ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                return;
            }
            DataSet objDataSet = optReturn.Data as DataSet;
            if (objDataSet == null)
            {
                ShowException(string.Format("DataSet is null"));
                return;
            }
            for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
            {
                DataRow dr = objDataSet.Tables[0].Rows[i];
                ListItem listItem = new ListItem();
                listItem.Name = dr["TABLE_NAME"].ToString();
                listItem.Display = listItem.Name;
                listItem.StrValue = listItem.Name;
                mListTables.Add(listItem);
            }
            SetTables();
        }

        private void LoadDataColumns()
        {
            var tableItem = ListBoxTables.SelectedItem as ListItem;
            if (tableItem == null) { return; }
            string strTableName = tableItem.StrValue;
            var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
            if (dataSourceItem == null) { return; }
            var dataSource = dataSourceItem.Data as DataSourceInfo;
            if (dataSource == null) { return; }
            var dbInfo = dataSource.DBInfo;
            if (dbInfo == null) { return; }
            string strConn = dbInfo.GetConnectionString();
            mListColumns.Clear();
            OperationReturn optReturn;
            string strSql;
            switch (dbInfo.TypeID)
            {
                case 1:
                    strSql =
                        string.Format(
                            "SELECT COLUMN_NAME AS COLUMN_NAME FROM information_schema.`COLUMNS` WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' ORDER BY COLUMN_NAME",
                            dbInfo.DBName,
                            strTableName);
                    optReturn = MysqlOperation.GetDataSet(strConn, strSql);
                    break;
                case 2:
                    strSql =
                        string.Format(
                            "SELECT NAME AS COLUMN_NAME FROM  SYSCOLUMNS WHERE ID=OBJECT_ID('{0}') ORDER BY NAME",
                            strTableName);
                    optReturn = MssqlOperation.GetDataSet(strConn, strSql);
                    break;
                case 3:
                    strSql =
                        string.Format(
                            "SELECT COLUMN_NAME AS COLUMN_NAME FROM USER_TAB_COLUMNS WHERE TABLE_NAME='{0}' ORDER BY COLUMN_NAME",
                            strTableName);
                    optReturn = OracleOperation.GetDataSet(strConn, strSql);
                    break;
                default:
                    ShowException(string.Format("DBType invalid."));
                    return;
            }
            if (!optReturn.Result)
            {
                ShowException(string.Format("Fail.\t{0}\t{1}", optReturn.Code, optReturn.Message));
                return;
            }
            DataSet objDataSet = optReturn.Data as DataSet;
            if (objDataSet == null)
            {
                ShowException(string.Format("DataSet is null"));
                return;
            }
            for (int i = 0; i < objDataSet.Tables[0].Rows.Count; i++)
            {
                DataRow dr = objDataSet.Tables[0].Rows[i];
                ListItem listItem = new ListItem();
                listItem.Name = dr["COLUMN_NAME"].ToString();
                listItem.Display = listItem.Name;
                listItem.StrValue = listItem.Name;
                mListColumns.Add(listItem);
            }
            SetFields();
        }

        #endregion


        #region Operations

        private void DoNext()
        {
            var index = TabControlWizard.SelectedIndex;
            if (index == 0)
            {
                //检查是否选择数据源
                var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
                if (dataSourceItem == null)
                {
                    ShowException(string.Format("请选择一个数据源！"));
                    return;
                }
                var dataSource = dataSourceItem.Data as DataSourceInfo;
                if (dataSource == null) { return; }
                var dbInfo = dataSource.DBInfo;
                if (dbInfo == null) { return; }
                if (!CheckConnection(dbInfo)) { return; }
                LoadDataTables();
                TabControlWizard.SelectedIndex = 1;
            }
            else if (index == 1)
            {
                var tableItem = ListBoxTables.SelectedItem as ListItem;
                if (tableItem == null)
                {
                    ShowException(string.Format("请选择一张数据表！"));
                    return;
                }
                LoadDataColumns();
                TabControlWizard.SelectedIndex = 2;
            }
            else if (index == 2)
            {
                if (CheckBoxAllFields.IsChecked != true
                    && mListFields.Count <= 0)
                {
                    ShowException(string.Format("请至少选择一个字段！"));
                    return;
                }
                if (CheckBoxAllFields.IsChecked == true
                    && mListColumns.Count <= 0)
                {
                    ShowException(string.Format("没有任何字段！"));
                    return;
                }
                TabControlWizard.SelectedIndex = 3;
            }
            else if (index == 3)
            {
                SetDataSetInfo();
                TabControlWizard.SelectedIndex = 4;
                BtnNext.Visibility = Visibility.Collapsed;
                BtnConfirm.Visibility = Visibility.Visible;
            }
        }

        private bool CheckConnection(DatabaseInfo dbInfo)
        {
            if (dbInfo == null)
            {
                return false;
            }
            string strConn = dbInfo.GetConnectionString();
            int dbType = dbInfo.TypeID;
            OperationReturn optReturn;
            switch (dbType)
            {
                case 1:
                    optReturn = MysqlOperation.TestDBConnection(strConn);
                    break;
                case 2:
                    optReturn = MssqlOperation.TestDBConnection(strConn);
                    break;
                case 3:
                    optReturn = OracleOperation.TestDBConnection(strConn);
                    break;
                default:
                    ShowException(string.Format("Database type not support!"));
                    return false;
            }
            if (!optReturn.Result)
            {
                ShowException(string.Format("测试连接失败！\r\n\r\n{0}\t{1}", optReturn.Code, optReturn.Message));
                return false;
            }
            return true;
        }

        private void SetDBInfo()
        {
            var item = ComboDataSources.SelectedItem as DataSourceItem;
            if (item == null) { return; }
            var info = item.Data as DataSourceInfo;
            if (info == null) { return; }
            var dbInfo = info.DBInfo;
            if (dbInfo == null) { return; }
            TxtDBType.Text = dbInfo.TypeID == 1
                ? "My SQL"
                : dbInfo.TypeID == 2 ?
                "Microsoft SQL Server"
                : dbInfo.TypeID == 3 ?
                "Oracle" : "";
            TxtDBHost.Text = dbInfo.Host;
            TxtDBPort.Text = dbInfo.Port.ToString();
            TxtDBName.Text = dbInfo.DBName;
            TxtLoginName.Text = dbInfo.LoginName;
        }

        private void SetTables()
        {
            if (!IsModify) { return; }
            if (DataSetItem == null) { return; }
            var dataSet = DataSetItem.Data as ReportDataSet;
            if (dataSet == null) { return; }
            var table = dataSet.Tables.FirstOrDefault();
            if (table == null) { return; }
            string strTableName = table.Name;
            var temp = mListTables.FirstOrDefault(t => t.Name == strTableName);
            ListBoxTables.SelectedItem = temp;
        }

        private void SetFields()
        {
            if (!IsModify) { return; }
            if (DataSetItem == null) { return; }
            var dataSet = DataSetItem.Data as ReportDataSet;
            if (dataSet == null) { return; }
            var fields = dataSet.Fields;
            mListFields.Clear();
            for (int i = 0; i < fields.Count; i++)
            {
                var field = fields[i];
                string strName = field.Name;
                var temp = mListColumns.FirstOrDefault(c => c.Name == strName);
                if (temp != null)
                {
                    mListFields.Add(temp);
                }
            }
        }

        private void SetDataSetInfo()
        {
            TxtDataSource.Text = string.Empty;
            TxtDataTable.Text = string.Empty;
            TxtFieldCount.Text = string.Empty;
            TxtConditionCount.Text = string.Empty;
            var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
            if (dataSourceItem != null)
            {
                var dataSource = dataSourceItem.Data as DataSourceInfo;
                if (dataSource != null)
                {
                    TxtDataSource.Text = dataSource.Name;
                }
            }
            var tableItem = ListBoxTables.SelectedItem as ListItem;
            if (tableItem != null)
            {
                TxtDataTable.Text = tableItem.Name;
            }
            List<ListItem> listFields = new List<ListItem>();
            if (CheckBoxAllFields.IsChecked == true)
            {
                for (int i = 0; i < mListColumns.Count; i++)
                {
                    listFields.Add(mListColumns[i]);
                }
            }
            else
            {
                for (int i = 0; i < mListFields.Count; i++)
                {
                    listFields.Add(mListFields[i]);
                }
            }
            TxtFieldCount.Text = listFields.Count.ToString();
            TxtConditionCount.Text = mListConditions.Count.ToString();
        }

        private void SaveDataSet()
        {
            if (string.IsNullOrEmpty(TxtDataSetName.Text))
            {
                ShowException(string.Format("数据集名称不能为空！"));
                return;
            }
            if (ListAllDataSets == null) { return; }
            string strName = TxtDataSetName.Text;
            if (!IsModify)
            {
                var temp = ListAllDataSets.FirstOrDefault(d => d.Name == strName);
                if (temp != null)
                {
                    ShowException(string.Format("数据集已经存在，请输入其他数据集名称！"));
                    return;
                }
            }
            DataSetItem dataSetItem;
            if (IsModify)
            {
                dataSetItem = DataSetItem;
            }
            else
            {
                dataSetItem = new DataSetItem();
            }
            if (dataSetItem == null) { return; }
            var dataSet = dataSetItem.Data as ReportDataSet;
            if (dataSet == null)
            {
                dataSet = new ReportDataSet();
                dataSet.Name = strName;
            }
            var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
            if (dataSourceItem == null) { return; }
            dataSet.DataSourceName = dataSourceItem.Name;
            var tableItem = ListBoxTables.SelectedItem as ListItem;
            if (tableItem == null) { return; }
            ReportDataTable reportTable = new ReportDataTable();
            reportTable.DataSet = dataSet;
            reportTable.Name = tableItem.StrValue;
            reportTable.Display = reportTable.Name;
            dataSet.Tables.Clear();
            dataSet.Tables.Add(reportTable);
            dataSet.Fields.Clear();
            List<ListItem> listFields = new List<ListItem>();
            if (CheckBoxAllFields.IsChecked == true)
            {
                for (int i = 0; i < mListColumns.Count; i++)
                {
                    listFields.Add(mListColumns[i]);
                }
            }
            else
            {
                for (int i = 0; i < mListFields.Count; i++)
                {
                    listFields.Add(mListFields[i]);
                }
            }
            if (listFields.Count <= 0) { return; }
            for (int i = 0; i < listFields.Count; i++)
            {
                var fieldItem = listFields[i];
                ReportDataField reportField = new ReportDataField();
                reportField.DataSet = dataSet;
                reportField.Table = reportTable;
                reportField.Name = fieldItem.StrValue;
                reportField.Display = reportField.Name;
                dataSet.Fields.Add(reportField);
            }
            dataSetItem.Data = dataSet;
            dataSetItem.Name = dataSet.Name;
            dataSetItem.Tip = string.Format("[{0}][{1}]", dataSet.Name, dataSet.DataSourceName);
            dataSetItem.Icon = new BitmapImage(new Uri("/Images/00043.png", UriKind.RelativeOrAbsolute));
            DataSetItem = dataSetItem;

            var parent = Parent as PopupWindow;
            if (parent != null)
            {
                parent.DialogResult = true;
                parent.Close();
            }
        }

        #endregion


        #region Event Handlers

        void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            var index = TabControlWizard.SelectedIndex;
            if (index > 0)
            {
                index--;
            }
            TabControlWizard.SelectedIndex = index;
            BtnConfirm.Visibility = Visibility.Collapsed;
            BtnNext.Visibility = Visibility.Visible;
        }

        void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            DoNext();
        }

        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            SaveDataSet();
        }

        void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var parent = Parent as PopupWindow;
            if (parent != null)
            {
                parent.Close();
            }
        }

        void ComboDataSources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDBInfo();
        }

        void CheckBoxAllFields_Click(object sender, RoutedEventArgs e)
        {
            var isCheck = CheckBoxAllFields.IsChecked;
            ListBoxFields.IsEnabled = isCheck != true;
            BtnFieldAdd.IsEnabled = isCheck != true;
            BtnFieldRemove.IsEnabled = isCheck != true;
        }

        void CheckBoxNoCondition_Click(object sender, RoutedEventArgs e)
        {

        }

        void BtnFieldRemove_Click(object sender, RoutedEventArgs e)
        {
            var items = ListBoxFields.SelectedItems;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i] as ListItem;
                if (item == null) { continue; }
                mListFields.Remove(item);
            }
        }

        void BtnFieldAdd_Click(object sender, RoutedEventArgs e)
        {
            var items = ListBoxColumns.SelectedItems;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i] as ListItem;
                if (item == null) { continue; }
                if (!mListFields.Contains(item))
                {
                    mListFields.Add(item);
                }
            }
        }

        #endregion


        #region Others

        private void ShowException(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInformation(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion


    }
}
