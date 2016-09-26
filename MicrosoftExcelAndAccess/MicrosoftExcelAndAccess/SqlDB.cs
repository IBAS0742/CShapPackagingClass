using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftExcelAndAccess
{
    public class SqlDB : SqlServerDB
    {
        public SqlDB()
        {
            Type = "sql";
//            sql_ = new SqlDb_Setting();
        }

        public override void Sql_Seting(object sql_set)
        {
            sql_ = (SqlDb_Setting)sql_set;
        }
        
        public override string TryToConnectHelp()
        {
            if (sql_ == null)
            {
                return "";
            }
            if (sql_.isWindowsAuth)
            {
                return "Server=" + sql_.Server_Machine + ";Database=" + sql_.Database + ";Trusted_Connection = SSPI";
            }
            else
            {
                return "Server=" + sql_.Server_Machine + ";Database=" + sql_.Database + ";uid=" + sql_.UserName + ";pwd=" + sql_.Password;
            }
        }
    }

    public class SqlDb_Setting
    {
        ///服务器名字
        public string Server_Machine { get; set; } = null;
        ///数据库名字
        public string Database { get; set; } = null;
        ///用户名
        public string UserName { get; set; } = null;
        ///密码
        public string Password { get; set; } = null;
        ///是否为windows身份认证
        public bool isWindowsAuth = false;
    }
}
