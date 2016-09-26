using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftExcelAndAccess
{
    public abstract class DBBase
    {
        public string FileName { get; set; } = null;
        protected bool isConnect = false;
        public bool IsConnect { get { return isConnect; } }
        public StringBuilder Err_sb { get; set; } = new StringBuilder();
        public Exception Err_Ex { get; set; } = null;
        protected string Type = "";
        protected SqlDb_Setting sql_ = null;
        public abstract bool TryToConnect();
        public abstract void Sql_Seting(object sql_set);
        public abstract void DisConnect();
        public abstract int Query_non_result(string str);
        public abstract DataSet Query_with_result(string sql);
        public List<string> GetTableHeader(DataTable dt)
        {
            List<string> result = null;
            if (isConnect)
            {
                result = new List<string>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    result.Add(dt.Columns[i].ColumnName);
                }
            }
            return result;
        }
    }
    public static class DBInstance
    {
        public static DBBase GetDBInstance(DBType type)
        {
            DBBase db = null;
            #region
            switch (type)
            {
                case DBType.Access:
                    db = new MDBDB();
                    break;
                case DBType.Excel:
                    db = new MDBDB(true);
                    break;
                case DBType.MDF:
                    db = new MDFDB();
                    break;
                case DBType.SqlServer:
                    db = new SqlDB();
                    break;
                default:
                    db = new MDBDB();
                    break;
            }
            #endregion
            return db;
        }
    }
    public enum DBType {
        Excel,
        Access,
        MDF,
        SqlServer
    }
}
