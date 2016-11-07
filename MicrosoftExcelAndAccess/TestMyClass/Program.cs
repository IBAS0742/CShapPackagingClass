using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftExcelAndAccess;
using System.Data;

namespace TestMyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 2016-11-07 14:35:50 ACCESS
            DBBase db = DBInstance.GetDBInstance(DBType.Access);
            db.FileName = @"test.mdb";
            if (db.TryToConnect())
            {
                Console.WriteLine("-----------ACCESS-----------");
                DearDataSet(db.Query_with_result("select * from test"));
            } else
            {
                Console.WriteLine(db.Err_sb.ToString());
            }
            db.DisConnect();
            #endregion

            #region 2016-11-07 16:26:08 Excel
            DBBase db_ = DBInstance.GetDBInstance(DBType.Excel);
            db_.FileName = "test.xls";
            if (db_.TryToConnect())
            {
                Console.WriteLine("-----------Excel-----------");
                DearDataSet(db_.Query_with_result("select * from [Sheet1$]"));
            } else
            {
                Console.WriteLine(db_.Err_sb.ToString());
            }
            db_.DisConnect();
            #endregion

            #region 2016-11-07 16:27:50 MDF (something easy to error)
            ///**
            // * Err : 尝试为文件 test.mdf 附加自动命名的数据库，但失败。
            // *       已存在同名的数据库，或指定的文件无法打开或位于 UNC 共享目录中。
            // */
            //DBBase db__ = DBInstance.GetDBInstance(DBType.MDF);
            //db__.FileName = @"test.mdf";
            //if (db__.TryToConnect())
            //{
            //    Console.WriteLine("-----------MDF-----------");
            //    DearDataSet(db__.Query_with_result("select * from Table"));
            //}
            //else
            //{
            //    Console.WriteLine("Err : " + db__.Err_Ex.Message);
            //}
            //db__.DisConnect();
            #endregion

            #region 2016-11-07 16:37:29 
            DBBase db___ = DBInstance.GetDBInstance(DBType.SqlServer);
            SqlDb_Setting sqlSetting = new SqlDb_Setting();
            sqlSetting.Server_Machine = @"DESKTOP-ESTNB01\SQLEXPRESS";
            sqlSetting.UserName = "";
            sqlSetting.isWindowsAuth = true;
            sqlSetting.Database = "Book";
            db___.Sql_Seting(sqlSetting);
            if (db___.TryToConnect())
            {
                Console.WriteLine("-----------SQL SERVER-----------");
                DearDataSet(db___.Query_with_result("select * from Reader"));
            }
            else
            {
                Console.WriteLine("Err : " + db___.Err_Ex.Message);
            }
            db___.DisConnect();
            #endregion
        }
        public static void DearDataSet(DataSet ds) {
            if (ds == null)
            {
                Console.WriteLine("ds is null");
            }
            else
            {
                for (int i = 0;i < ds.Tables.Count;i++)
                {
                    DearDataTable(ds.Tables[i]);
                }
            }
        }
        public static void DearDataTable(DataTable dt)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    Console.Write(dt.Rows[j][k] + "\t");
                }
                Console.WriteLine("");
            }
        }
    }
    class Database_
    {
        public DBType dbtype { get; set; }
        public string FileName { get; set; }
        public SqlDb_Setting sqlSet { get; set; }
    }
}
