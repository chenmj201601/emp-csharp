//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    58434255-521e-45dc-8ca0-fbc314fb7fbe
//        CLR Version:              4.0.30319.42000
//        Name:                     XmlUtil
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                NetInfo.EMP.Reports
//        File Name:                XmlUtil
//
//        Created by Charley at 2017/4/18 13:51:30
//        http://www.netinfo.com 
//
//======================================================================

using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace NetInfo.EMP.Reports
{
    public class XmlUtil
    {
        /// <summary>
        /// 将对象序列化成字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <returns></returns>
        public static string SerializeObject<T>(T obj)
        {
            if (obj == null)
            {
                throw new NullReferenceException("Object is null.");
            }
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            XmlTextWriter xmlWriter = new XmlTextWriter(ms, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.Indentation = 4;                                         //设置xml文档格式，有4位缩进
            xmlSerializer.Serialize(xmlWriter, obj);
            ms.Position = 0;
            StreamReader reader = new StreamReader(ms);
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }
        /// <summary>
        /// 将对象序列化并保存到文件中
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <param name="fileName">文件完整路径</param>
        public static void SerializeFile<T>(T obj, string fileName)
        {
            string str = SerializeObject(obj);
            File.WriteAllText(fileName, str, Encoding.UTF8);
        }
        /// <summary>
        /// 从字符串反序列化出对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="strContent">反序列化的内容</param>
        /// <returns>对象实例</returns>
        public static T DeserializeObject<T>(string strContent)
        {
            T obj = default(T);
            StringBuilder sb = new StringBuilder();
            sb.Append(strContent);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(sb.ToString()))
            {
                obj = (T)xmlSerializer.Deserialize(reader);
                reader.Close();
            }
            return obj;
        }
        /// <summary>
        /// 从文件反序列化出对象实例
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="fileName">文件完整路径</param>
        /// <returns>对象实例</returns>
        public static T DeserializeFile<T>(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(string.Format("File {0} not exist.", fileName));
            }
            Stream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            XmlReader reader = new XmlTextReader(fileStream);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T obj = (T)xmlSerializer.Deserialize(reader);
            reader.Close();
            return obj;
        }
    }
}
