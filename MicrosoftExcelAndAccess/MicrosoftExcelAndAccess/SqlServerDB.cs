using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftExcelAndAccess
{
    public abstract class SqlServerDB : DBBase
    {
        protected SqlConnection conn = null;
        public SqlConnection Conn { get { return conn; } }
        //public SqlServerDB() { }
        public override bool TryToConnect()
        {
            if (!Type.Equals("sql"))
            {
                if (string.IsNullOrEmpty(FileName))
                {
                    Err_sb.Clear();
                    Err_sb.Append("Fill name is null or empty");
                    return false;
                }
                if (!File.Exists(FileName))
                {
                    Err_sb.Clear();
                    Err_sb.Append("File is not exist");
                    return false;
                }
            }
            conn = new SqlConnection(TryToConnectHelp());
            try
            {
                conn.Open();
                isConnect = true;
            }
            catch (InvalidOperationException ex)
            {
                Err_Ex = ex;
                isConnect = false;
            }
            catch (SqlException ex)
            {
                Err_Ex = ex;
                isConnect = false;
            }
            return isConnect;
        }
        public abstract string TryToConnectHelp();
        public override void DisConnect()
        {
            if (isConnect)
            {
                conn.Close();
                isConnect = false;
                conn = null;
            }
        }
        public override int Query_non_result(string str)
        {
            if (isConnect)
            {
                SqlCommand com = new SqlCommand(str, conn);
                return com.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }
        public override DataSet Query_with_result(string sql)
        {
            if (isConnect)
            {
                DataSet ds = new DataSet();
                SqlDataAdapter apt = new SqlDataAdapter(sql, conn);
                apt.Fill(ds);
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}
