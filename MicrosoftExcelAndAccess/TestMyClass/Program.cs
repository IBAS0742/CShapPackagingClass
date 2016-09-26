using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrosoftExcelAndAccess;
using System.Data;
using Newtonsoft.Json;

namespace TestMyClass
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Test MDBDB mdb
            //DBBase mdbdb = new MDBDB();
            //mdbdb.FileName = @"D:\C#\mdf\Data.mdb";
            //if (!mdbdb.TryToConnect())
            //{
            //    Console.WriteLine(mdbdb.Err_sb.ToString());
            //}
            ////DearDataTable(mdbdb.GetAllTable());
            //DataSet ds = mdbdb.Query_with_result("select * from test");
            //DearDataSet(ds);
            //mdbdb.GetTableHeader(ds.Tables[0]).ForEach(
            //        n =>
            //        {
            //            Console.Write(n + "\t");
            //        }
            //    );
            //Console.WriteLine();
            #endregion

            #region Test MDBDB excel
            //MDBDB mdb = new MDBDB();
            //mdb.FileName = @"D:\C#\mdf\Data.xls";
            //mdb.IsExcel = true;
            //if (!mdb.TryToConnect())
            //{
            //    Console.Write(mdb.Err_sb.ToString());
            //}
            //Console.Write("\n");
            //DearDataTable(mdb.GetAllTable());
            //Console.WriteLine("");
            //DataSet ds = mdb.Query_with_result("select * from [Sheet1$]");
            //DearDataTable(ds.Tables[0]);
            //mdb
            //    .GetTableHeader(
            //        ds.Tables[0]
            //            ).ForEach(n => {
            //                Console.Write(n + "\t");
            //            });
            #endregion

            #region MDF file
            //MDFDB mdf = new MDFDB();
            //mdf.FileName = @"D:\C#\测试\net_disk\新建文件夹\NET_DISK_FILE_SYSTEM_r.mdf";
            //if (mdf.TryToConnect())
            //{
            //    DearDataSet(mdf.Query_with_result("SELECT * FROM dbo.ND_USER"));
            //}
            //else
            //{
            //    Console.WriteLine(mdf.Err_sb.ToString());
            //}
            ////Console.WriteLine(mdf
            ////    .Query_non_result("insert into nd_user(Nick,PWD,ROLES) values('Test1','zzZZ1100','1')"));
            //mdf.DisConnect();
            #endregion

            #region SQL SERVER 
            //SqlDB sql = new SqlDB();
            //sql.Sql_Setting.Server_Machine = @"DESKTOP-ESTNB01\SQLEXPRESS";
            //sql.Sql_Setting.Database = "net_disk";
            //sql.Sql_Setting.isWindowsAuth = true;
            ////sql.UserName = @"DESKTOP-ESTNB01\Administrator";
            ////sql.Password = "zzZZ1100";
            //if (sql.TryToConnect())
            //{
            //    DearDataSet(sql.Query_with_result("select * from nd_user"));
            //} else
            //{
            //    Console.WriteLine(sql.Err_Ex.Message);
            //}
            //sql.DisConnect();
            #endregion

            #region DBInstance
            //DBBase db = DBInstance.GetDBInstance(DBType.SqlServer);
            //db.Sql_Seting(new SqlDb_Setting() {
            //    Server_Machine = @"DESKTOP-ESTNB01\SQLEXPRESS",
            //    Database = "net_disk",
            //    isWindowsAuth = true
            //});
            //db.TryToConnect();
            //DearDataSet(db.Query_with_result("select * from nd_user"));
            #endregion

            //#region Test MDF
            DBBase db___ = DBInstance.GetDBInstance(DBType.MDF);
            db___.FileName = @"D:\NET_DISK_FILE_SYSTEM_r.mdf";
            db___.TryToConnect();
            Console.WriteLine(db___.Err_sb.ToString());
            DearDataSet(db___.Query_with_result("select * from nd_user"));
            db___.DisConnect();
            //#endregion

            #region 
            string json = "{\"dbtype\":2,\"FileName\":\"D:\\\\NET_DISK_FILE_SYSTEM_r.mdf\"}";
            Database_ db = JsonConvert.DeserializeObject<Database_>(json);
            DBBase db_ = DBInstance.GetDBInstance(db.dbtype);
            db_.FileName = db.FileName;
            db_.TryToConnect();
            DearDataSet(db_.Query_with_result("select * from nd_user"));
            db_.DisConnect();
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
