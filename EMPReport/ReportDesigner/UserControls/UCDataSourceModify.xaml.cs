//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    2cc26a57-4f18-41b1-ab86-26bdc642b988
//        CLR Version:              4.0.30319.42000
//        Name:                     UCDataSourceModify
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.UserControls
//        File Name:                UCDataSourceModify
//
//        Created by Charley at 2017/4/25 9:59:10
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using NetInfo.Common;
using NetInfo.DBAccesses;
using ReportDesigner.Models;

namespace ReportDesigner.UserControls
{
    /// <summary>
    /// UCDataSourceModify.xaml 的交互逻辑
    /// </summary>
    public partial class UCDataSourceModify
    {
        public bool IsModify;
        public IList<DataSourceItem> ListAllDataSourceItems;
        public DataSourceItem DataSourceItem;

        private bool mIsInited;
        private ObservableCollection<ComboItem> mListDBTypes = new ObservableCollection<ComboItem>();

        public UCDataSourceModify()
        {
            InitializeComponent();

            Loaded += UCDataSourceModify_Loaded;
            BtnTest.Click += BtnTest_Click;
            BtnConfirm.Click += BtnConfirm_Click;
            BtnClose.Click += BtnClose_Click;
        }

        void UCDataSourceModify_Loaded(object sender, RoutedEventArgs e)
        {
            if (!mIsInited)
            {
                Init();
                mIsInited = true;
            }
        }

        private void Init()
        {
            ComboDBType.ItemsSource = mListDBTypes;
            InitDBTypes();
            InitDBInfo();
        }

        private void InitDBTypes()
        {
            mListDBTypes.Clear();
            ComboItem item = new ComboItem();
            item.Name = "MYSQL";
            item.Display = "My SQL";
            item.IntValue = 1;
            mListDBTypes.Add(item);
            item = new ComboItem();
            item.Name = "MSSQL";
            item.Display = "SQL Server";
            item.IntValue = 2;
            mListDBTypes.Add(item);
            item = new ComboItem();
            item.Name = "ORCL";
            item.Display = "Oracle";
            item.IntValue = 3;
            mListDBTypes.Add(item);
        }

        private void InitDBInfo()
        {
            if (IsModify)
            {
                if (DataSourceItem == null) { return; }
                var info = DataSourceItem.Data as DataSourceInfo;
                if (info == null) { return; }
                var dbInfo = info.DBInfo;
                if (dbInfo == null) { return; }
                TxtDataSourceName.Text = info.Name;
                var item = mListDBTypes.FirstOrDefault(d => d.IntValue == dbInfo.TypeID);
                ComboDBType.SelectedItem = item;
                TxtDBHost.Text = dbInfo.Host;
                TxtDBPort.Text = dbInfo.Port.ToString();
                TxtDBName.Text = dbInfo.DBName;
                TxtLoginName.Text = dbInfo.LoginName;
                TxtPassword.Password = dbInfo.RealPassword;
            }
        }

        void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            var parent = Parent as PopupWindow;
            if (parent == null) { return; }
            parent.DialogResult = false;
            parent.Close();
        }

        void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInput()) { return; }
            DatabaseInfo dbInfo = new DatabaseInfo();
            var item = ComboDBType.SelectedItem as ComboItem;
            if (item == null) { return; }
            dbInfo.TypeID = item.IntValue;
            dbInfo.Host = TxtDBHost.Text;
            dbInfo.Port = int.Parse(TxtDBPort.Text);
            dbInfo.DBName = TxtDBName.Text;
            dbInfo.LoginName = TxtLoginName.Text;
            dbInfo.RealPassword = TxtPassword.Password;
            dbInfo.Password = dbInfo.RealPassword;
            DataSourceInfo dataSource = new DataSourceInfo();
            dataSource.Name = TxtDataSourceName.Text;
            dataSource.DBInfo = dbInfo;
            if (!IsModify)
            {
                DataSourceItem = new DataSourceItem();
            }
            DataSourceItem.Name = dataSource.Name;
            DataSourceItem.Detail = dataSource.DBInfo.ToString();
            if (dbInfo.TypeID == 1)
            {
                DataSourceItem.Icon = new BitmapImage(new Uri("/Images/00037.png", UriKind.RelativeOrAbsolute));
            }
            if (dbInfo.TypeID == 2)
            {
                DataSourceItem.Icon = new BitmapImage(new Uri("/Images/00038.png", UriKind.RelativeOrAbsolute));
            }
            if (dbInfo.TypeID == 3)
            {
                DataSourceItem.Icon = new BitmapImage(new Uri("/Images/00039.png", UriKind.RelativeOrAbsolute));
            }
            DataSourceItem.Data = dataSource;
            var parent = Parent as PopupWindow;
            if (parent != null)
            {
                parent.DialogResult = true;
                parent.Close();
            }
        }

        void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInput()) { return; }
            DatabaseInfo dbInfo = new DatabaseInfo();
            var item = ComboDBType.SelectedItem as ComboItem;
            if (item == null) { return; }
            dbInfo.TypeID = item.IntValue;
            dbInfo.Host = TxtDBHost.Text;
            dbInfo.Port = int.Parse(TxtDBPort.Text);
            dbInfo.DBName = TxtDBName.Text;
            dbInfo.LoginName = TxtLoginName.Text;
            dbInfo.RealPassword = TxtPassword.Password;
            dbInfo.Password = dbInfo.RealPassword;
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
                    return;
            }
            if (!optReturn.Result)
            {
                ShowException(string.Format("测试连接失败！\r\n\r\n{0}\t{1}", optReturn.Code, optReturn.Message));
                return;
            }
            ShowInformation(string.Format("连接成功！"));
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(TxtDataSourceName.Text))
            {
                ShowException(string.Format("数据源名称不能为空！"));
                return false;
            }
            if (!IsModify)
            {
                if (ListAllDataSourceItems == null)
                {
                    ShowException(string.Format("ListAllDataSourceItems is null!"));
                    return false;
                }
                var temp = ListAllDataSourceItems.FirstOrDefault(d => d.Name == TxtDataSourceName.Text);
                if (temp != null)
                {
                    ShowException(string.Format("数据源已经存在！"));
                    return false;
                }
            }
            var item = ComboDBType.SelectedItem as ComboItem;
            if (item == null)
            {
                ShowException("数据库类型无效！");
                return false;
            }
            if (string.IsNullOrEmpty(TxtDBHost.Text))
            {
                ShowException(string.Format("服务器地址不能为空！"));
                return false;
            }
            int intValue;
            if (!int.TryParse(TxtDBPort.Text, out intValue))
            {
                ShowException(string.Format("端口无效！"));
                return false;
            }
            if (string.IsNullOrEmpty(TxtDBName.Text))
            {
                ShowException(string.Format("数据库名不能为空！"));
                return false;
            }
            if (string.IsNullOrEmpty(TxtLoginName.Text))
            {
                ShowException(string.Format("登录名不能为空！"));
                return false;
            }
            if (string.IsNullOrEmpty(TxtPassword.Password))
            {
                ShowException(string.Format("登录密码不能为空！"));
                return false;
            }
            return true;
        }

        private void ShowException(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ShowInformation(string msg)
        {
            MessageBox.Show(msg, App.AppTitle, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
