//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    8a101cbc-d1fb-4cfa-bb41-dcc8673386b7
//        CLR Version:              4.0.30319.42000
//        Name:                     OracleOperation
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.DBAccesses
//        File Name:                OracleOperation
//
//        Created by Charley at 2017/4/25 11:09:46
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Data;
using System.Data.Common;
using NetInfo.Common;
using Oracle.DataAccess.Client;


namespace NetInfo.DBAccesses
{
    /// <summary>
    /// Oracle数据库的通用操作
    /// 这里的方法都是静态的，可以通过类名直接引用
    /// </summary>
    public class OracleOperation
    {
        /// <summary>
        /// 创建一个DBConnection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns></returns>
        public static OracleConnection GetConnection(string strConn)
        {
            return new OracleConnection(strConn);
        }
        /// <summary>
        /// 创建一个数据库适配器
        /// </summary>
        /// <param name="objConn">DBConnection</param>
        /// <param name="strSql">查询语句</param>
        /// <returns></returns>
        public static OracleDataAdapter GetDataAdapter(IDbConnection objConn, string strSql)
        {
            return new OracleDataAdapter(strSql, objConn as OracleConnection);
        }
        /// <summary>
        /// 创建Command
        /// </summary>
        /// <param name="objConn"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static OracleCommand GetCommand(IDbConnection objConn, string strSql)
        {
            return new OracleCommand(strSql, objConn as OracleConnection);
        }
        /// <summary>
        /// 创建Command
        /// </summary>
        /// <returns></returns>
        public static OracleCommand GetCommand()
        {
            return new OracleCommand();
        }
        /// <summary>
        /// 创建一个CommandBuilder
        /// </summary>
        /// <param name="objAdapter">DataAdapter</param>
        /// <returns></returns>
        public static OracleCommandBuilder GetCommandBuilder(IDbDataAdapter objAdapter)
        {
            return new OracleCommandBuilder(objAdapter as OracleDataAdapter);
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
            OracleConnection orclConnection = new OracleConnection(strConn);
            try
            {
                orclConnection.Open();
                optReturn.Message = orclConnection.Database;
                optReturn.Data = orclConnection.ConnectionTimeout;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
            }
            finally
            {
                if (orclConnection.State == ConnectionState.Open)
                {
                    orclConnection.Close();
                }
                orclConnection.Dispose();
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
            OracleConnection orclConnection = new OracleConnection(strConn);
            OracleDataAdapter orclAdapter = new OracleDataAdapter(strSql, orclConnection);
            DataSet objDataSet = new DataSet();
            try
            {
                orclAdapter.Fill(objDataSet);
                optReturn.Data = objDataSet;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as OracleException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MSSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                objDataSet.Dispose();
                if (orclConnection.State == ConnectionState.Open)
                {
                    orclConnection.Close();
                }
                orclConnection.Dispose();
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
            OracleConnection sqlConnection = new OracleConnection(strConn);
            OracleCommand sqlCmd = new OracleCommand();
            sqlCmd.Connection = sqlConnection;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = procedureName;
            OracleDataAdapter sqlAdapter = new OracleDataAdapter(sqlCmd);
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
                var err = ex as OracleException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MSSQL_ERR_OBJECT_NOT_EXIST)
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
            int count = parameters.Length;
            OracleConnection orclConnection = new OracleConnection(strConn);
            OracleCommand orclCmd = new OracleCommand();
            orclCmd.Connection = orclConnection;
            orclCmd.CommandType = CommandType.StoredProcedure;
            orclCmd.CommandText = procedureName;
            for (int i = 0; i < parameters.Length; i++)
            {
                orclCmd.Parameters.Add(parameters[i]);
            }
            try
            {
                orclConnection.Open();
                orclCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as OracleException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MSSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (orclConnection.State == ConnectionState.Open)
                {
                    orclConnection.Close();
                }
                orclConnection.Dispose();
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
        public static DbParameter GetDbParameter(string name, OracleDataType dataType, int length)
        {
            switch (dataType)
            {
                case OracleDataType.Varchar2:
                    return new OracleParameter(name, OracleDbType.Varchar2, length);
                case OracleDataType.Nvarchar2:
                    return new OracleParameter(name, OracleDbType.NVarchar2, length);
                case OracleDataType.Int32:
                    return new OracleParameter(name, OracleDbType.Int32);
                case OracleDataType.Char:
                    return new OracleParameter(name, OracleDbType.Char, length);
                case OracleDataType.Date:
                    return new OracleParameter(name, OracleDbType.Date);
                case OracleDataType.RefCursor:
                    return new OracleParameter(name, OracleDbType.RefCursor);
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
            OracleConnection orclConnection = new OracleConnection(strConn);
            OracleCommand orclCmd = new OracleCommand();
            orclCmd.Connection = orclConnection;
            orclCmd.CommandType = CommandType.Text;
            orclCmd.CommandText = strSql;
            try
            {
                orclConnection.Open();
                int count = orclCmd.ExecuteNonQuery();
                optReturn.Data = count;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as OracleException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MSSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (orclConnection.State == ConnectionState.Open)
                {
                    orclConnection.Close();
                }
                orclConnection.Dispose();
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
            OracleConnection orclConnection = new OracleConnection(strConn);
            OracleCommand orclCmd = new OracleCommand();
            orclCmd.Connection = orclConnection;
            orclCmd.CommandType = CommandType.Text;
            orclCmd.CommandText = strSql;
            try
            {
                orclConnection.Open();
                int count = Convert.ToInt32(orclCmd.ExecuteScalar());
                optReturn.IntValue = count;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_DBACCESS_FAIL;
                optReturn.Message = ex.Message;
                var err = ex as OracleException;
                if (err != null)
                {
                    if (err.Number == DBAccessDefine.MSSQL_ERR_OBJECT_NOT_EXIST)
                    {
                        optReturn.Code = Defines.RET_DBACCESS_TABLE_NOT_EXIST;
                    }
                }
            }
            finally
            {
                if (orclConnection.State == ConnectionState.Open)
                {
                    orclConnection.Close();
                }
                orclConnection.Dispose();
            }
            return optReturn;
        }
    }
}
