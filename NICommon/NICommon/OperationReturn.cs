//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    f257eab9-db75-40d7-aa36-5b4ffa9646d4
//        CLR Version:              4.0.30319.42000
//        Name:                     OperationReturn
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Common
//        File Name:                OperationReturn
//
//        Created by Charley at 2017/4/19 15:41:35
//        http://www.netinfo.com 
//
//======================================================================

using System;


namespace NetInfo.Common
{
    /// <summary>
    /// 操作返回值
    /// </summary>
    public class OperationReturn
    {
        /// <summary>
        /// 操作结果
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 返回代码，参考Defines中的定义
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 返回值，整型
        /// </summary>
        public int IntValue { get; set; }
        /// <summary>
        /// 返回值，数值型
        /// </summary>
        public decimal NumericValue { get; set; }
        /// <summary>
        /// 返回值，文本型
        /// </summary>
        public string StringValue { get; set; }
        /// <summary>
        /// 返回值，使用的时候可通过 as 转换成对应的对象
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 操作异常
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 以字符串形式返回
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strReturn = string.Empty;
            if (Exception != null)
            {
                strReturn = Exception.Message;
            }
            if (string.IsNullOrEmpty(Message))
            {
                return string.Format("{0}-{1}", Code.ToString("0000"), strReturn);
            }
            return string.Format("{0}-{1}", Code.ToString("0000"), Message);
        }
    }
}
