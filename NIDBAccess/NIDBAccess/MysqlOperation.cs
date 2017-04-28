//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    4b2c4220-7344-404a-9526-d068ca0fa66f
//        CLR Version:              4.0.30319.42000
//        Name:                     MysqlOperation
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.DBAccesses
//        File Name:                MysqlOperation
//
//        Created by Charley at 2017/4/25 11:08:05
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using NetInfo.Common;


namespace NetInfo.DBAccesses
{
    /// <summary>
    /// MySql数据库的通用操作
    /// 这里的方法都是静态的，可以通过类名直接引用
    /// </summary>
    public class MysqlOperation
    {
        /// <summary>
        /// 创建一个DBConnection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns></returns>
        public static MySqlConnection GetConnection(string strConn)
        {
            return new MySqlConnection(strConn);
        }
        /// <summary>
        /// 创建一个数据库适配器
        /// </summary>
        /// <param name="objConn">DBConnection</param>
        /// <param name="strSql">查询语句</param>
        /// <returns></returns>
        public static MySqlDataAdapter GetDataAdapter(IDbConnection objConn, string strSql)
        {
            return new MySqlDataAdapter(strSql, objConn as MySqlConnection);
        }
        /// <summary>
        /// 创建Command
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static MySqlCommand GetCommand(IDbConnection objConn, string strSql)
        {
            return new MySqlCommand(strSql, objConn as MySqlConnection);
        }
        /// <summary>
        /// 创建Command
        /// </summary>
        /// <returns></returns>
        public static MySqlCommand GetCommand()
        {
            return new MySqlCommand();
        }
        /// <summary>
        /// 创建一个CommandBuilder
        /// </summary>
        /// <param name="objAdapter">DataAdapter</param>
        /// <returns></returns>
        public static MySqlCommandBuilder GetCommandBuilder(IDbDataAdapter objAdapter)
        {
            return new MySqlCommandBuilder(objAdapter as MySqlDataAdapter);
        }
        /// <summary>
        /// 测试数据库是否连接成功
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns></returns>
        public static OperationReturn TestDBConnection(string strConn)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = 0;
            MySqlConnection sqlConnection = new MySqlConnection(strConn);
            try
            {
                sqlConnection.Open();
                optReturn.Message = sqlConnection.Database;
                optReturn.Data = sqlConnection.ConnectionTimeout;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
            return optReturn;
        }
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <param name="strSql">查询语句</param>
        /// <returns></returns>
        public static OperationReturn GetDataSet(string strConn, string strSql)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = 0;
            MySqlConnection sqlConnection = new MySqlConnection(strConn);
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(strSql, sqlConnection);
            DataSet objDataSet = new DataSet();
            try
            {
                sqlAdapter.Fill(objDataSet);
                optReturn.Data = objDataSet;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as MySqlException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MYSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
            return optReturn;
        }
        /// <summary>
        /// 执行存储过程，获取数据集
        /// </summary>
        /// <param name="strConn"></param>
        /// <param name="procedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static OperationReturn GetDataSetFromStoredProcedure(string strConn, string procedureName,
          DbParameter[] parameters)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = 0;
            MySqlConnection sqlConnection = new MySqlConnection(strConn);
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = procedureName;
            MySqlDataAdapter sqlAdapter = new MySqlDataAdapter(sqlCmd);
            DataSet objDataSet = new DataSet();
            for (int i = 0; i < parameters.Length; i++)
            {
                sqlCmd.Parameters.Add(parameters[i]);
            }
            try
            {
                sqlConnection.Open();
                sqlAdapter.Fill(objDataSet);
                optReturn.Data = objDataSet;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
                var err = ex as MySqlException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MYSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
            return optReturn;
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <param name="procedureName">存储过程名称</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public static OperationReturn ExecuteStoredProcedure(string strConn, string procedureName, DbParameter[] parameters)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = 0;
            MySqlConnection sqlConnection = new MySqlConnection(strConn);
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = procedureName;
            for (int i = 0; i < parameters.Length; i++)
            {
                sqlCmd.Parameters.Add(parameters[i]);
            }
            try
            {
                sqlConnection.Open();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as MySqlException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MYSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
            return optReturn;
        }
        /// <summary>
        /// 根据指定的数据类型创建DBCommand的参数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="dataType">数据类型</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static DbParameter GetDbParameter(string name, MysqlDataType dataType, int length)
        {
            switch (dataType)
            {
                case MysqlDataType.Varchar:
                    return new MySqlParameter(name, MySqlDbType.VarChar, length);
                case MysqlDataType.NVarchar:
                    return new MySqlParameter(name, MySqlDbType.VarChar, length);
                case MysqlDataType.Char:
                    return new MySqlParameter(name, MySqlDbType.VarChar);
                case MysqlDataType.Int:
                    return new MySqlParameter(name, MySqlDbType.Int32);
                case MysqlDataType.Bigint:
                    return new MySqlParameter(name, MySqlDbType.Int64, length);
            }
            return null;
        }
        /// <summary>
        /// 执行指定的Sql语句
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <param name="strSql">Sql语句</param>
        /// <returns></returns>
        public static OperationReturn ExecuteSql(string strConn, string strSql)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = 0;
            MySqlConnection sqlConnection = new MySqlConnection(strConn);
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = strSql;
            try
            {
                sqlConnection.Open();
                int count = sqlCmd.ExecuteNonQuery();
                optReturn.Data = count;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as MySqlException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MYSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
            return optReturn;
        }
        /// <summary>
        /// 获取记录条数
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <param name="strSql">查询语句</param>
        /// <returns>OperationReturn的IntValue为记录条数</returns>
        public static OperationReturn GetRecordCount(string strConn, string strSql)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = 0;
            MySqlConnection sqlConnection = new MySqlConnection(strConn);
            MySqlCommand sqlCmd = new MySqlCommand();
            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = strSql;
            try
            {
                sqlConnection.Open();
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                optReturn.IntValue = count;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as MySqlException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MYSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                sqlConnection.Dispose();
            }
            return optReturn;
        }
    }
}
