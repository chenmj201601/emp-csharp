//======================================================================
//
//        Copyright © 2017 NetInfo Technologies Ltd.
//        All rights reserved
//        guid1:                    cacf5ef4-665c-4001-9df0-4b176e7ca763
//        CLR Version:              4.0.30319.42000
//        Name:                     DatabaseInfo
//        Computer:                 CHARLEY-PC
//        Organization:             NetInfo
//        Namespace:                ReportDesigner.Models
//        File Name:                DatabaseInfo
//
//        Created by Charley at 2017/4/24 18:22:43
//        http://www.netinfo.com 
//
//======================================================================

using System.Runtime.InteropServices;
using System.Xml.Serialization;


namespace ReportDesigner.Models
{
    public class DatabaseInfo
    {
        [XmlAttribute]
        public int TypeID { get; set; }
        [XmlAttribute]
        public string Host { get; set; }
        [XmlAttribute]
        public int Port { get; set; }
        [XmlAttribute]
        public string DBName { get; set; }
        [XmlAttribute]
        public string LoginName { get; set; }
        [XmlAttribute]
        public string Password { get; set; }
        [XmlIgnore]
        public string RealPassword { get; set; }


        public string GetConnectionString()
        {
            string strReturn = string.Empty;
            switch (TypeID)
            {
                case 1:
                    strReturn = string.Format("Server={0};Port={1};Database={2};Uid={3};Pwd={4}", Host, Port, DBName,
                        LoginName, RealPassword);
                    break;
                case 2:
                    strReturn = string.Format("Data Source={0},{1};Initial Catalog={2};User Id={3};Password={4}", Host,
                        Port, DBName, LoginName, RealPassword);
                    break;
                case 3:
                    strReturn =
                        string.Format(
                            "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVICE_NAME={2})));User Id={3}; Password={4}",
                            Host, Port, DBName, LoginName, RealPassword);
                    break;
            }
            return strReturn;
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}:{2}-{3}-{4}", TypeID == 1 ? "MYSQL" : TypeID == 2 ? "MSSQL" : TypeID == 3 ? "ORCL" : TypeID.ToString(),
                Host,
                Port,
                DBName,
                LoginName);
        }
    }
}
