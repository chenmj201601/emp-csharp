//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    edaf85e6-3605-4cd3-a1c9-654cc56ecb82
//        CLR Version:              4.0.30319.42000
//        Name:                     XMLHelper
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.Common
//        File Name:                XMLHelper
//
//        Created by Charley at 2017/4/19 15:45:05
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace NetInfo.Common
{
    /// <summary>
    /// XML文件操作帮助类
    /// </summary>
    public class XMLHelper
    {

        #region Xml序列化与反序列化
        /// <summary>
        /// 将对象序列化成字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static OperationReturn SeriallizeObject<T>(T obj)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (obj == null)
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_PARAM_INVALID;
                    optReturn.Message = string.Format("Serialize object is null");
                    return optReturn;
                }
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                XmlTextWriter xmlWriter = new XmlTextWriter(ms, Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 4;                                         //设置xml文档格式，有4位缩进
                xmlSerializer.Serialize(xmlWriter, obj);
                ms.Position = 0;
                StreamReader reader = new StreamReader(ms);
                optReturn.Data = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 将对象序列化成一个xml文件
        /// </summary>
        /// <typeparam name="T">要序列化的对象的类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <param name="fileName">xml文件的完整路径</param>
        /// <returns></returns>
        public static OperationReturn SerializeFile<T>(T obj, string fileName)
        {
            var optReturn = SeriallizeObject(obj);
            if (!optReturn.Result)
            {
                return optReturn;
            }
            try
            {
                string strValue = optReturn.Data.ToString();
                File.WriteAllText(fileName, strValue, Encoding.UTF8);
                optReturn.Message = fileName;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 从字符串中反序列化出对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strContent"></param>
        /// <returns></returns>
        public static OperationReturn DeserializeObject<T>(string strContent)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(strContent);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(sb.ToString()))
                {
                    T obj = (T)xmlSerializer.Deserialize(reader);
                    reader.Close();
                    optReturn.Data = obj;
                }
                return optReturn;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
                return optReturn;
            }
        }
        ///<summary>
        ///从xml文件反序列化出对象
        ///</summary>
        /// <typeparam name="T">要反序列化的对象的类型</typeparam>
        ///<param name="fileName">xml文件的完整路径</param>
        ///<returns></returns>
        public static OperationReturn DeserializeFile<T>(string fileName)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (!File.Exists(fileName))
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_FILE_NOT_EXIST;
                    optReturn.Message = string.Format("File not exist.\t{0}", fileName);
                    return optReturn;
                }
                Stream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                XmlReader reader = new XmlTextReader(fileStream);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                T obj = (T)xmlSerializer.Deserialize(reader);
                reader.Close();
                optReturn.Data = obj;
                return optReturn;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
                return optReturn;
            }
        }
        #endregion


        #region xml文件基本操作
        /// <summary>
        /// 获取指定元素的值
        /// </summary>
        /// <param name="fileName">xml文件的完整路径</param>
        /// <param name="path">元素的路径</param>
        /// <returns></returns>
        public static OperationReturn GetElementValue(string fileName, string path)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (!File.Exists(fileName))
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_FILE_NOT_EXIST;
                    optReturn.Message = string.Format("File not exist.\t{0}", fileName);
                    return optReturn;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode node = xmlDoc.SelectSingleNode(path);
                XmlElement element = node as XmlElement;
                if (element == null)
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_XML_ELE_NOT_EXIST;
                    optReturn.Message = string.Format("XmlElement not exist.\t{0}", path);
                    return optReturn;
                }
                optReturn.StringValue = element.InnerText;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 获取指定元素指定属性的值
        /// </summary>
        /// <param name="fileName">xml文件的完整路径</param>
        /// <param name="path">元素路径</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static OperationReturn GetElementPropertyValue(string fileName, string path, string propertyName)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (!File.Exists(fileName))
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_FILE_NOT_EXIST;
                    optReturn.Message = string.Format("File not exist.\t{0}", fileName);
                    return optReturn;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode node = xmlDoc.SelectSingleNode(path);
                XmlElement element = node as XmlElement;
                if (element == null)
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_XML_ELE_NOT_EXIST;
                    optReturn.Message = string.Format("XmlElement not exist.\t{0}", path);
                    return optReturn;
                }
                var value = element.GetAttribute(propertyName);
                optReturn.Data = value;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 获取指定元素的所有子元素
        /// </summary>
        /// <param name="fileName">xml文件的完整路径</param>
        /// <param name="path">元素路径</param>
        /// <returns></returns>
        public static OperationReturn GetChildElements(string fileName, string path)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (!File.Exists(fileName))
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_FILE_NOT_EXIST;
                    optReturn.Message = string.Format("File not exist.\t{0}", fileName);
                    return optReturn;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode node = xmlDoc.SelectSingleNode(path);
                XmlElement element = node as XmlElement;
                if (element == null)
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_XML_ELE_NOT_EXIST;
                    optReturn.Message = string.Format("XmlElement not exist.\t{0}", path);
                    return optReturn;
                }
                XmlNodeList listChildNodes = element.ChildNodes;
                List<XmlElement> listChildElements = new List<XmlElement>();
                for (int i = 0; i < listChildNodes.Count; i++)
                {
                    XmlElement child = listChildNodes[i] as XmlElement;
                    if (child != null)
                    {
                        listChildElements.Add(child);
                    }
                }
                optReturn.Data = listChildElements;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 设置指定元素的值
        /// </summary>
        /// <param name="fileName">xml文件的完整路径</param>
        /// <param name="path">元素路径</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static OperationReturn SetElementValue(string fileName, string path, string value)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (!File.Exists(fileName))
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_FILE_NOT_EXIST;
                    optReturn.Message = string.Format("File not exist.\t{0}", fileName);
                    return optReturn;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode node = xmlDoc.SelectSingleNode(path);
                XmlElement element = node as XmlElement;
                if (element == null)
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_XML_ELE_NOT_EXIST;
                    optReturn.Message = string.Format("XmlElement not exist.\t{0}", path);
                    return optReturn;
                }
                element.InnerText = value;
                xmlDoc.Save(fileName);
                optReturn.Data = value;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        /// <summary>
        /// 设置指定元素指定属性的值
        /// </summary>
        /// <param name="fileName">xml文件的完整路径</param>
        /// <param name="path">元素路径</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="value">属性值</param>
        /// <returns></returns>
        public static OperationReturn SetElementPropertyValue(string fileName, string path, string propertyName,
            string value)
        {
            OperationReturn optReturn = new OperationReturn();
            optReturn.Result = true;
            optReturn.Code = Defines.RET_SUCCESS;
            try
            {
                if (!File.Exists(fileName))
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_FILE_NOT_EXIST;
                    optReturn.Message = string.Format("File not exist.\t{0}", fileName);
                    return optReturn;
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                XmlNode node = xmlDoc.SelectSingleNode(path);
                XmlElement element = node as XmlElement;
                if (element == null)
                {
                    optReturn.Result = false;
                    optReturn.Code = Defines.RET_XML_ELE_NOT_EXIST;
                    optReturn.Message = string.Format("XmlElement not exist.\t{0}", path);
                    return optReturn;
                }
                element.SetAttribute(propertyName, value);
                xmlDoc.Save(fileName);
                optReturn.Data = value;
            }
            catch (Exception ex)
            {
                optReturn.Result = false;
                optReturn.Code = Defines.RET_FAIL;
                optReturn.Message = ex.Message;
                optReturn.Exception = ex;
            }
            return optReturn;
        }
        #endregion

    }
}
