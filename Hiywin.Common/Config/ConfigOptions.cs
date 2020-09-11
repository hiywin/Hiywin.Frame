using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hiywin.Common
{
    public class ConfigOptions
    {
        /// <summary>
        /// Mysql操作连接路径
        /// </summary>
        public static string MysqlOptConn { get; set; }
        /// <summary>
        /// Mysql查询连接路径
        /// </summary>
        public static string MysqlSearchConn { get; set; }

        /// <summary>
        /// Mssql操作连接路径
        /// </summary>
        public static string MssqlOptConn { get; set; }
        /// <summary>
        /// Mssql查询连接路径
        /// </summary>
        public static string MssqlSearchConn { get; set; }


    }
}
