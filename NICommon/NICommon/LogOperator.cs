//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    1cd047a1-913c-4e8d-9ffd-5d8a518ea0dc
//        CLR Version:              4.0.30319.42000
//        Name:                     LogOperator
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Common
//        File Name:                LogOperator
//
//        Created by Charley at 2017/4/19 15:42:54
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;


namespace NetInfo.Common
{
    /// <summary>
    /// 日志级别,可任意组合
    /// </summary>
    [Flags]
    public enum LogMode
    {
        /// <summary>
        /// 调试
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warn = 4,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 8,
        /// <summary>
        /// 致命
        /// </summary>
        Fatal = 16,
        /// <summary>
        /// 所有消息
        /// </summary>
        All = Debug | Info | Warn | Error | Fatal,
        /// <summary>
        /// 一般（默认）
        /// </summary>
        General = Info | Warn | Error | Fatal
    }

    /// <summary>
    /// 日志操作类，写日志，备份以及回删日志
    /// 1、使用属性进行相关配置
    /// 2、调用Start启动日志写入器
    /// 3、调用Stop停止日志写入器
    /// 4、可以任意设置LogMode参数来记录指定类型的日志
    /// </summary>
    public class LogOperator
    {
        #region Members

        private const int LOG_FILE_SIZE = 1000;
        private const int LOG_SAVE_DAYS = 10;
        private const String LOG_FILE_NAME = "log";
        private const string LOG_PATH = "log";

        private StreamWriter mLogWriter;
        private object mLogWriterLocker;
        private Thread mThreadLogDelete;
        private object mThreadLogDeleteLocker;
        private int mLogFileSize;
        private int mLogSaveDays;
        private string mLogFileName;
        private string mLogPath;
        private LogMode mLogMode;
        private string mLogFile;
        private DateTime mLastDate;

        #endregion


        #region Properties
        /// <summary>
        /// 设置日志文件的大小，超过此值将备份日志文件
        /// </summary>
        public int LogFileSize
        {
            //get { return mLogFileSize; }
            set { mLogFileSize = value; }
        }
        /// <summary>
        /// 设置日志文件保留天数
        /// </summary>
        public int LogSaveDays
        {
            //get { return mLogSaveDays; }
            set { mLogSaveDays = value; }
        }
        /// <summary>
        /// 设置日志文件的前缀名
        /// </summary>
        public string LogFileName
        {
            //get { return mLogFileName; }
            set { mLogFileName = value; }
        }
        /// <summary>
        /// 设置日志文件的保存路径，可以是绝对路径，也可以是相对路径
        /// </summary>
        public string LogPath
        {
            //get { return mLogPath; }
            set { mLogPath = value; }
        }
        /// <summary>
        /// 日志记录模式，可以是任意模式或各模式的组合
        /// </summary>
        public LogMode LogMode
        {
            //get { return mLogMode; }
            set { mLogMode = value; }
        }
        #endregion

        /// <summary>
        /// 创建一个日志操作类型
        /// </summary>
        public LogOperator()
        {
            mLogWriterLocker = new object();
            mThreadLogDeleteLocker = new object();
            mLogMode = LogMode.General;
            mLogFileName = LOG_FILE_NAME;
            mLogPath = LOG_PATH;
            mLogFileSize = LOG_FILE_SIZE;
            mLogSaveDays = LOG_SAVE_DAYS;
            mLastDate = DateTime.Now;
        }
        /// <summary>
        /// 启动日志写入器
        /// </summary>
        /// <returns></returns>
        public OperationReturn Start()
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                string strDir = Path.IsPathRooted(mLogPath)
                    ? mLogPath
                    : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, mLogPath);
                if (!Directory.Exists(strDir))
                {
                    Directory.CreateDirectory(strDir);
                }
                DateTime now = DateTime.Now;
                string strFile = Path.Combine(strDir,
                    string.Format("{0}{1}.txt", mLogFileName, now.ToString("yyyyMMdd")));
                int i = 0;
                while (File.Exists(strFile))
                {
                    FileInfo fileInfo = new FileInfo(strFile);
                    if (fileInfo.Length < mLogFileSize * 1024)
                    {
                        break;
                    }
                    i++;
                    strFile = Path.Combine(strDir,
                        string.Format("{0}{1}{2}.txt", mLogFileName, now.ToString("yyyyMMdd"), i.ToString("0000")));
                }
                mLogFile = strFile;
                mLastDate = now;
                lock (mLogWriterLocker)
                {
                    if (mLogWriter != null)
                    {
                        try
                        {
                            mLogWriter.Close();
                            mLogWriter = null;
                        }
                        catch { }
                    }
                    mLogWriter = new StreamWriter(mLogFile, true, Encoding.Default);
                }
                mLogWriter.WriteLine("{0}\tLog started.\t{1}\t{2}", LogMode.Info, mLogMode, now.ToString("yyyy/MM/dd HH:mm:ss"));
                mLogWriter.Flush();
                StartDeleteThread();
            }
            catch (Exception ex)
            {
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 停止日志写入器
        /// </summary>
        public void Stop()
        {
            StopDeleteThread();
            if (mLogWriter != null)
            {
                try
                {
                    mLogWriter.Close();
                    mLogWriter = null;
                }
                catch { }
            }
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="mode">日志模式</param>
        /// <param name="category">分类</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public OperationReturn WriteLog(LogMode mode, string category, string msg)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if ((mode & mLogMode) == 0) { return optReturn; }
                DateTime now = DateTime.Now;
                FileInfo fileInfo = new FileInfo(mLogFile);
                if (mLastDate.Date != now.Date
                    || mLogWriter == null
                    || (File.Exists(mLogFile) && fileInfo.Length > mLogFileSize * 1024))
                {
                    optReturn = Start();
                    if (!optReturn.Result)
                    {
                        return optReturn;
                    }
                }
                int index = msg.IndexOf("\r\n", StringComparison.Ordinal);
                string strThis = msg;
                string strOther = string.Empty;
                if (index > 0)
                {
                    strThis = msg.Substring(0, index);
                    strOther = msg.Substring(index + 2);
                }
                string formatstr = string.Format("{0,-8} |{1,-15} |({2})| {3,-20} {4}"
                        , mode
                        , DateTime.Now.ToString("HH:mm:ss.fff")
                        , Thread.CurrentThread.ManagedThreadId.ToString("00000")
                        , category
                        , strThis);
                lock (mLogWriterLocker)
                {
                    if (mLogWriter != null)
                    {
                        mLogWriter.WriteLine(formatstr);
                        mLogWriter.Flush();
                    }
                    else
                    {
                        optReturn.Result = false;
                        optReturn.Code = Defines.RET_OBJECT_NULL;
                        optReturn.Message = "LogWriter is null";
                        return optReturn;
                    }
                }
                if (!string.IsNullOrEmpty(strOther))
                {
                    WriteLog(mode, category, strOther);
                }
            }
            catch (Exception ex)
            {
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 写入调试日志
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public OperationReturn LogDebug(string category, string msg)
        {
            return WriteLog(LogMode.Debug, category, msg);
        }
        /// <summary>
        /// 写入信息日志
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public OperationReturn LogInfo(string category, string msg)
        {
            return WriteLog(LogMode.Info, category, msg);
        }
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="msg">消息内容</param>
        /// <returns></returns>
        public OperationReturn LogError(string category, string msg)
        {
            return WriteLog(LogMode.Error, category, msg);
        }
        /// <summary>
        /// 获取当前日志文件的完整路径
        /// </summary>
        /// <returns></returns>
        public string GetLogFileName()
        {
            return mLogFile;
        }

        #region DeleteFiles

        private void StartDeleteThread()
        {
            lock (mThreadLogDeleteLocker)
            {
                if (mThreadLogDelete != null)
                {
                    try
                    {
                        mThreadLogDelete.Abort();
                        mThreadLogDelete = null;
                    }
                    catch { }
                }
                try
                {
                    mThreadLogDelete = new Thread(DeleteLogWorker);
                    mThreadLogDelete.Start();
                }
                catch (Exception)
                {
                    //WriteOperationLog(LogMode.Error, "StartDeleteThread",string.Format("Start delete thread fail.\t{0}", ex.Message));
                }
            }
        }

        private void StopDeleteThread()
        {
            lock (mThreadLogDeleteLocker)
            {
                if (mThreadLogDelete != null)
                {
                    try
                    {
                        mThreadLogDelete.Abort();
                        mThreadLogDelete = null;
                    }
                    catch { }
                }
            }
        }

        private void DeleteLogWorker()
        {
            while (true)
            {
                try
                {
                    string dir = Path.IsPathRooted(mLogPath) ? mLogPath : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, mLogPath);
                    DirectoryInfo logdir = new DirectoryInfo(dir);
                    var listfiles = logdir.GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();
                    DateTime now = DateTime.Now;
                    for (int i = listfiles.Count - 1; i >= 0; i--)
                    {
                        if ((now - listfiles[i].LastWriteTime).TotalDays > mLogSaveDays)
                        {
                            listfiles[i].Delete();
                        }
                        //以下回删会导致程序慢
                        //dirSize = getDirSize(dir);
                        //if (dirSize <= mLogDirMaxSize * 1024 * 1024)
                        //{
                        //    continue;
                        //}
                        //if (dirSize > mLogDirMaxSize * 1024 * 1024 / 2)
                        //{
                        //    listfiles[i].Delete();
                        //}
                    }
                }
                catch (Exception)
                {
                    //WriteOperationLog(LogMode.Warn, "DeleteLogFile", string.Format("Delete log file fail!\t{0}", ex.Message));
                }
                Thread.Sleep(1000 * 60);
            }
        }

        #endregion
    }
}
