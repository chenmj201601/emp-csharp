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
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using NetInfo.Common;
using NetInfo.DBAccesses;
using NetInfo.EMP.Reports;
using ReportDesigner.Commands;
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
        private readonly ObservableCollection<DataSourceItem> mListDataSourceItems = new ObservableCollection<DataSourceItem>();
        private readonly ObservableCollection<ListItem> mListTables = new ObservableCollection<ListItem>();
        private readonly ObservableCollection<DataFieldItem> mListColumns = new ObservableCollection<DataFieldItem>();
        private readonly ObservableCollection<DataFieldItem> mListFields = new ObservableCollection<DataFieldItem>();
        private readonly ObservableCollection<ReportConditionItem> mListConditions = new ObservableCollection<ReportConditionItem>();
        private readonly ObservableCollection<ReportOrderItem> mListOrders = new ObservableCollection<ReportOrderItem>();
        private readonly ObservableCollection<ListItem> mListJugeTypes = new ObservableCollection<ListItem>();
        private readonly ObservableCollection<ListItem> mListRelationTypes = new ObservableCollection<ListItem>();
        private readonly ObservableCollection<ListItem> mListOrderDirections = new ObservableCollection<ListItem>();

        public ObservableCollection<DataFieldItem> ListFields
        {
            get { return mListFields; }
        }

        public ObservableCollection<ListItem> ListJugeTypes
        {
            get { return mListJugeTypes; }
        }

        public ObservableCollection<ListItem> ListRelationTypes
        {
            get { return mListRelationTypes; }
        }

        public ObservableCollection<ListItem> ListOrderDirections
        {
            get { return mListOrderDirections; }
        }

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
            CheckBoxNoOrder.Click += CheckBoxNoOrder_Click;
            CbEnableEdit.Click += CbEnableEdit_Click;
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
            ListViewConditions.ItemsSource = mListConditions;
            ListViewOrders.ItemsSource = mListOrders;
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.ConditionAddCommand, ConditionAdd_Executed,
                (s, e) => e.CanExecute = true));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.ConditionRemoveCommand, ConditionRemove_Executed,
               (s, e) => e.CanExecute = true));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.ConditionValueEditCommand, ConditionValueEdit_Executed,
               (s, e) => e.CanExecute = true));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.OrderAddCommand, OrderAdd_Executed,
                (s, e) => e.CanExecute = true));
            CommandBindings.Add(new CommandBinding(ReportDesignerCommands.OrderRemoveCommand, OrderRemove_Executed,
               (s, e) => e.CanExecute = true));
            InitDataSourceItems();
            InitJugeTypes();
            InitRelationTypes();
            InitOrderDirections();
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
                listItem.Data = dr;
                string strValue = dr["TABLE_NAME"].ToString();
                listItem.Name = strValue;
                listItem.Display = strValue;
                listItem.StrValue = strValue;
                mListTables.Add(listItem);
            }
        }

        private void LoadDataColumns()
        {
            var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
            if (dataSourceItem == null) { return; }
            var dataSource = dataSourceItem.Data as DataSourceInfo;
            if (dataSource == null) { return; }
            var dbInfo = dataSource.DBInfo;
            if (dbInfo == null) { return; }
            OperationReturn optReturn;
            mListColumns.Clear();
            string strConn = dbInfo.GetConnectionString();
            string strSql;
            var tableItems = ListBoxTables.SelectedItems;
            if (tableItems.Count <= 0) { return; }
            for (int i = 0; i < tableItems.Count; i++)
            {
                var tableItem = tableItems[i] as ListItem;
                if (tableItem == null) { continue; }
                string strTableName = tableItem.StrValue;
                switch (dbInfo.TypeID)
                {
                    case 1:
                        strSql =
                            string.Format(
                                "SELECT COLUMN_NAME AS COLUMN_NAME, DATA_TYPE AS DATA_TYPE, COLUMN_TYPE AS COLUMN_TYPE FROM information_schema.`COLUMNS` WHERE TABLE_SCHEMA = '{0}' AND TABLE_NAME = '{1}' ORDER BY COLUMN_NAME",
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
                for (int j = 0; j < objDataSet.Tables[0].Rows.Count; j++)
                {
                    DataRow dr = objDataSet.Tables[0].Rows[j];
                    string strName = dr["COLUMN_NAME"].ToString();
                    string strValue = string.Format("{0}_{1}", strTableName, strName);
                    int intDataType = 0;
                    if (dbInfo.TypeID == 1)
                    {
                        string strDataType = dr["DATA_TYPE"].ToString();
                        if (strDataType.ToLower().Equals("int"))
                        {
                            intDataType = (int)DBDataType.Int;
                        }
                        if (strDataType.ToLower().Equals("datetime"))
                        {
                            intDataType = (int)DBDataType.Datetime;
                        }
                        if (strDataType.ToLower().Equals("decimal"))
                        {
                            intDataType = (int)DBDataType.Number;
                        }
                        if (strDataType.ToLower().Equals("varchar"))
                        {
                            intDataType = (int) DBDataType.NVarchar;
                        }
                    }
                    DataFieldItem item = new DataFieldItem();
                    item.Name = strValue;
                    item.Display = strValue;
                    item.Tip = strValue;
                    item.FieldName = strName;
                    item.TableName = strTableName;
                    item.DataType = intDataType;
                    mListColumns.Add(item);
                }
            }

        }

        private void InitJugeTypes()
        {
            mListJugeTypes.Clear();
            ListItem item = new ListItem();
            item.Name = "Equal";
            item.Display = "等于";
            item.Tip = item.Display;
            item.IntValue = (int)JudgeType.Equal;
            mListJugeTypes.Add(item);
            item = new ListItem();
            item.Name = "Larger";
            item.Display = "大于";
            item.Tip = item.Display;
            item.IntValue = (int)JudgeType.Larger;
            mListJugeTypes.Add(item);
            item = new ListItem();
            item.Name = "Lower";
            item.Display = "小于";
            item.Tip = item.Display;
            item.IntValue = (int)JudgeType.Lower;
            mListJugeTypes.Add(item);
        }

        private void InitRelationTypes()
        {
            mListRelationTypes.Clear();
            ListItem item = new ListItem();
            item.Name = "And";
            item.Display = "与";
            item.Tip = item.Display;
            item.IntValue = (int)RelationType.And;
            mListRelationTypes.Add(item);
            item = new ListItem();
            item.Name = "Or";
            item.Display = "或";
            item.Tip = item.Display;
            item.IntValue = (int)RelationType.Or;
            mListRelationTypes.Add(item);
        }

        private void InitOrderDirections()
        {
            mListOrderDirections.Clear();
            ListItem item = new ListItem();
            item.Name = "Asending";
            item.Display = "升序";
            item.Tip = item.Display;
            item.IntValue = (int)OrderDirection.Ascending;
            mListOrderDirections.Add(item);
            item = new ListItem();
            item.Name = "Desending";
            item.Display = "降序";
            item.Tip = item.Display;
            item.IntValue = (int)OrderDirection.Descending;
            mListOrderDirections.Add(item);
        }

        private void InitFieldItems()
        {
            if (CheckBoxAllFields.IsChecked == true)
            {
                mListFields.Clear();
                for (int i = 0; i < mListColumns.Count; i++)
                {
                    mListFields.Add(mListColumns[i]);
                }
            }
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
                SetTables();
            }
            else if (index == 1)
            {
                var tableItems = ListBoxTables.SelectedItems;
                if (tableItems.Count <= 0)
                {
                    ShowException(string.Format("请选择数据表！"));
                    return;
                }
                LoadDataColumns();
                TabControlWizard.SelectedIndex = 2;
                SetFields();
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
                InitFieldItems();
                SetConditions();
                TabControlWizard.SelectedIndex = 3;
            }
            else if (index == 3)
            {
                SetOrderDirections();
                TabControlWizard.SelectedIndex = 4;
            }
            else if (index == 4)
            {
                SetDataSetInfo();
                TabControlWizard.SelectedIndex = 5;
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
            var tables = dataSet.Tables;
            ListBoxTables.SelectedItems.Clear();
            for (int i = 0; i < tables.Count; i++)
            {
                var table = tables[i];
                string strName = table.Name;
                var temp = mListTables.FirstOrDefault(t => t.Name == strName);
                if (temp != null)
                {
                    ListBoxTables.SelectedItems.Add(temp);
                }
            }
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

        private void SetConditions()
        {
            mListConditions.Clear();
            if (!IsModify) { return; }
            if (DataSetItem == null) { return; }
            var dataSet = DataSetItem.Data as ReportDataSet;
            if (dataSet == null) { return; }
            var conditions = dataSet.Conditions;
            if (conditions.Count > 0)
            {
                CheckBoxNoCondition.IsChecked = false;
                ListViewConditions.IsEnabled = true;
            }
            for (int i = 0; i < conditions.Count; i++)
            {
                var condition = conditions[i];
                ReportConditionItem item = new ReportConditionItem();
                item.Info = condition;
                item.Field = mListFields.FirstOrDefault(f => f.Name == condition.Field);
                item.Judge = mListJugeTypes.FirstOrDefault(j => j.IntValue == condition.Judge);
                item.Relation = mListRelationTypes.FirstOrDefault(r => r.IntValue == condition.Relation);
                item.Value = condition.Value;
                mListConditions.Add(item);
            }
        }

        private void SetOrderDirections()
        {
            mListOrders.Clear();
            if (!IsModify) { return; }
            if (DataSetItem == null) { return; }
            var dataSet = DataSetItem.Data as ReportDataSet;
            if (dataSet == null) { return; }
            var orders = dataSet.Orders;
            if (orders.Count > 0)
            {
                CheckBoxNoOrder.IsChecked = false;
                ListViewOrders.IsEnabled = true;
            }
            for (int i = 0; i < orders.Count; i++)
            {
                var order = orders[i];
                ReportOrderItem item = new ReportOrderItem();
                item.Info = order;
                item.Field = mListFields.FirstOrDefault(f => f.Name == order.Field);
                item.Direction = mListOrderDirections.FirstOrDefault(d => d.IntValue == order.Direction);
                mListOrders.Add(item);
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
            var tableItems = ListBoxTables.SelectedItems;
            string strTables = string.Empty;
            for (int i = 0; i < tableItems.Count; i++)
            {
                var tableItem = tableItems[i] as ListItem;
                if (tableItem == null) { continue; }
                strTables += string.Format("{0},", tableItem.StrValue);
            }
            strTables = strTables.TrimEnd(',');
            TxtDataTable.Text = strTables;
            List<DataFieldItem> listFields = new List<DataFieldItem>();
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
            bool isCustomized = false;
            string strSql = string.Empty;
            var dataSetItem = DataSetItem;
            if (dataSetItem != null)
            {
                var dataSet = dataSetItem.Data as ReportDataSet;
                if (dataSet != null && dataSet.IsSqlCustomized == 1)
                {
                    isCustomized = true;
                    strSql = dataSet.Sql;
                }
            }
            CbEnableEdit.IsChecked = isCustomized;
            TxtSql.IsReadOnly = !isCustomized;
            if (!isCustomized)
            {
                strSql = CreateSql();
            }
            TxtSql.Text = strSql;
        }

        private void SaveDataSet()
        {
            if (string.IsNullOrEmpty(TxtDataSetName.Text))
            {
                ShowException(string.Format("数据集名称不能为空！"));
                return;
            }
            if (ListAllDataSets == null) { return; }
            string strDataSetName = TxtDataSetName.Text;
            if (!IsModify)
            {
                var temp = ListAllDataSets.FirstOrDefault(d => d.Name == strDataSetName);
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
                dataSet.Name = strDataSetName;
            }

            var dataSourceItem = ComboDataSources.SelectedItem as DataSourceItem;
            if (dataSourceItem == null) { return; }
            dataSet.DataSourceName = dataSourceItem.Name;

            dataSet.Tables.Clear();
            var tableItems = ListBoxTables.SelectedItems;
            for (int i = 0; i < tableItems.Count; i++)
            {
                var tableItem = tableItems[i] as ListItem;
                if (tableItem == null) { continue; }
                ReportDataTable reportTable = new ReportDataTable();
                reportTable.DataSet = dataSet;
                reportTable.Key = string.Format("{0}.{1}", dataSet.Name, tableItem.Name);
                reportTable.Name = tableItem.StrValue;
                reportTable.Display = reportTable.Name;
                dataSet.Tables.Add(reportTable);
            }

            dataSet.Fields.Clear();
            List<DataFieldItem> listFields = new List<DataFieldItem>();
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
            if (listFields.Count > 0)
            {
                for (int i = 0; i < listFields.Count; i++)
                {
                    var fieldItem = listFields[i];
                    ReportDataField reportField = new ReportDataField();
                    reportField.DataSet = dataSet;
                    string strTableName = fieldItem.TableName;
                    var reportTable = dataSet.Tables.FirstOrDefault(t => t.Name == strTableName);
                    reportField.Table = reportTable;
                    reportField.Name = string.Format("{0}_{1}", fieldItem.TableName, fieldItem.FieldName);
                    reportField.FieldName = fieldItem.FieldName;
                    reportField.DataType = fieldItem.DataType;
                    reportField.TableName = fieldItem.TableName;
                    reportField.Display = fieldItem.Display;
                    reportField.Key = string.Format("{0}.{1}", strDataSetName, reportField.Name);
                    dataSet.Fields.Add(reportField);
                }
            }

            dataSet.Conditions.Clear();
            for (int i = 0; i < mListConditions.Count; i++)
            {
                var conditionItem = mListConditions[i];
                ReportCondition condition = conditionItem.Info;
                if (condition == null)
                {
                    condition = new ReportCondition();
                }
                condition.DataSet = dataSet;
                var fieldItem = conditionItem.Field;
                if (fieldItem != null)
                {
                    condition.Field = fieldItem.Name;
                }
                var jugeItem = conditionItem.Judge;
                if (jugeItem != null)
                {
                    condition.Judge = jugeItem.IntValue;
                }
                var relationItem = conditionItem.Relation;
                if (relationItem != null)
                {
                    condition.Relation = relationItem.IntValue;
                }
                condition.Value = conditionItem.Value;
                dataSet.Conditions.Add(condition);
            }

            dataSet.Orders.Clear();
            for (int i = 0; i < mListOrders.Count; i++)
            {
                var orderItem = mListOrders[i];
                ReportOrder order = orderItem.Info;
                if (order == null)
                {
                    order = new ReportOrder();
                }
                order.DataSet = dataSet;
                var fieldItem = orderItem.Field;
                if (fieldItem != null)
                {
                    order.Field = fieldItem.Name;
                }
                var directionItem = orderItem.Direction;
                if (directionItem != null)
                {
                    order.Direction = directionItem.IntValue;
                }
                dataSet.Orders.Add(order);
            }

            dataSet.IsSqlCustomized = CbEnableEdit.IsChecked == true ? 1 : 0;
            dataSet.Sql = TxtSql.Text;

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

        private string CreateSql()
        {
            string strSql = string.Empty;
            string strFrom = string.Empty;
            string strSelect = string.Empty;
            string strWhere = string.Empty;
            string strOrder = string.Empty;
            Regex fieldRegex = new Regex("^{\\w+}$");
            var tableItems = ListBoxTables.SelectedItems;
            for (int i = 0; i < tableItems.Count; i++)
            {
                var tableItem = tableItems[i] as ListItem;
                if (tableItem == null) { continue; }
                strFrom += string.Format(" {0},", tableItem.StrValue);
            }
            strFrom = strFrom.TrimEnd(',');
            List<DataFieldItem> listFields = new List<DataFieldItem>();
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
            for (int i = 0; i < listFields.Count; i++)
            {
                var field = ListFields[i];
                string fieldName = field.FieldName;
                string tableName = field.TableName;
                string display = field.Display;
                strSelect += string.Format(" {0}.{1} AS {2},", tableName, fieldName, display);
            }
            strSelect = strSelect.TrimEnd(',');
            for (int i = 0; i < mListConditions.Count; i++)
            {
                var condition = mListConditions[i];
                var field = condition.Field;
                if (field == null) { continue; }
                var judgeItem = condition.Judge;
                if (judgeItem == null) { continue; }
                var relationItem = condition.Relation;
                if (relationItem == null) { continue; }
                string strJudge = string.Empty;
                string strRelation = string.Empty;
                string fieldName = field.FieldName;
                string tableName = field.TableName;
                if (judgeItem.IntValue == (int)JudgeType.Equal)
                {
                    strJudge = "=";
                }
                if (i > 0)
                {
                    if (relationItem.IntValue == (int)RelationType.And)
                    {
                        strRelation = "AND";
                    }
                    if (relationItem.IntValue == (int)RelationType.Or)
                    {
                        strRelation = "OR";
                    }
                }
                string value = condition.Value;
                if (fieldRegex.IsMatch(value))
                {
                    value = value.TrimStart('{').TrimEnd('}');
                    DataFieldItem valueField = listFields.FirstOrDefault(f => f.Name == value);
                    if (valueField == null) { continue; }
                    string valueFieldName = valueField.FieldName;
                    string valueTableName = valueField.TableName;
                    value = string.Format("{0}.{1}", valueTableName, valueFieldName);
                }
                strWhere += string.Format(" {0} {1}.{2} {3} {4}", strRelation, tableName, fieldName, strJudge, value);
            }
            for (int i = 0; i < mListOrders.Count; i++)
            {
                var order = mListOrders[i];
                var field = order.Field;
                if (field == null) { continue; }
                var directionItem = order.Direction;
                if (directionItem == null) { continue; }
                string strDirection = string.Empty;
                string fieldName = field.FieldName;
                string tableName = field.TableName;
                if (directionItem.IntValue == (int)OrderDirection.Ascending)
                {
                    strDirection = "ASC";
                }
                if (directionItem.IntValue == (int)OrderDirection.Descending)
                {
                    strDirection = "DESC";
                }
                strOrder += string.Format(" {0}.{1} {2},", tableName, fieldName, strDirection);
            }
            strOrder = strOrder.TrimEnd(',');
            if (strSelect.Length > 0)
            {
                strSql += string.Format(" SELECT {0}", strSelect);
            }
            if (strFrom.Length > 0)
            {
                strSql += string.Format(" FROM {0}", strFrom);
            }
            if (strWhere.Length > 0)
            {
                strSql += string.Format(" WHERE {0}", strWhere);
            }
            if (strOrder.Length > 0)
            {
                strSql += string.Format(" ORDER BY {0}", strOrder);
            }
            return strSql;
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
            var ischeck = CheckBoxNoCondition.IsChecked;
            ListViewConditions.IsEnabled = ischeck != true;
            if (ischeck == true)
            {
                mListConditions.Clear();
            }
        }

        void CheckBoxNoOrder_Click(object sender, RoutedEventArgs e)
        {
            var ischeck = CheckBoxNoOrder.IsChecked;
            ListViewOrders.IsEnabled = ischeck != true;
            if (ischeck == true)
            {
                mListOrders.Clear();
            }
        }

        void BtnFieldRemove_Click(object sender, RoutedEventArgs e)
        {
            var items = ListBoxFields.SelectedItems;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i] as DataFieldItem;
                if (item == null) { continue; }
                mListFields.Remove(item);
            }
        }

        void BtnFieldAdd_Click(object sender, RoutedEventArgs e)
        {
            var items = ListBoxColumns.SelectedItems;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i] as DataFieldItem;
                if (item == null) { continue; }
                if (!mListFields.Contains(item))
                {
                    mListFields.Add(item);
                }
            }
        }

        void CbEnableEdit_Click(object sender, RoutedEventArgs e)
        {
            TxtSql.IsReadOnly = CbEnableEdit.IsChecked != true;
            if (CbEnableEdit.IsChecked == false)
            {
                TxtSql.Text = CreateSql();
            }
        }

        #endregion


        #region Commands

        private void ConditionAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ReportConditionItem item = new ReportConditionItem();
            item.Field = mListFields.FirstOrDefault();
            item.Judge = mListJugeTypes.FirstOrDefault(j => j.IntValue == (int)JudgeType.Equal);
            item.Relation = mListRelationTypes.FirstOrDefault(r => r.IntValue == (int)RelationType.And);
            item.Value = string.Empty;
            mListConditions.Add(item);
        }

        private void ConditionRemove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.Parameter as ReportConditionItem;
            if (item == null) { return; }
            mListConditions.Remove(item);
        }

        private void ConditionValueEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.Parameter as ReportConditionItem;
            if (item == null) { return; }
            UCConditionValueEditor uc = new UCConditionValueEditor();
            uc.ConditionItem = item;
            uc.ListAllFieldItems = mListFields;
            PopupWindow popup = new PopupWindow();
            popup.Title = string.Format("条件值编辑器");
            popup.Content = uc;
            var result = popup.ShowDialog();
            //if (result != true) { return; }
        }

        private void OrderAdd_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ReportOrderItem item = new ReportOrderItem();
            item.Field = mListFields.FirstOrDefault();
            item.Direction = mListOrderDirections.FirstOrDefault(d => d.IntValue == (int)OrderDirection.Ascending);
            mListOrders.Add(item);
        }

        private void OrderRemove_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var item = e.Parameter as ReportOrderItem;
            if (item == null) { return; }
            mListOrders.Remove(item);
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
