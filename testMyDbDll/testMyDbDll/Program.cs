using System;
using System.Collections.Generic;
using System.Data;
using GetAllRequestDLL;
using Newtonsoft.Json;
using MicrosoftExcelAndAccess;

namespace testMyDbDll
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Test Database
            //DBBase db = DBInstance.GetDBInstance(DBType.SqlServer);
            //db.Sql_Seting(
            //        new SqlDb_Setting()
            //        {
            //            isWindowsAuth = true,
            //            Server_Machine = @"DESKTOP-ESTNB01\SQLEXPRESS",
            //            Database = "net_disk",
            //        }
            //    );
            //if (db.TryToConnect())
            //{
            //    Console.WriteLine("Success To Connection DB .");
            //    DearDataSet(db.Query_with_result("select * from nd_user"));
            //} else
            //{
            //    Console.WriteLine("Fail To Connection DB .");
            //    Console.WriteLine(db.Err_Ex.Message);
            //    Console.WriteLine(db.Err_sb.ToString());
            //}
            //db.DisConnect();
            #endregion

            #region Test Request
            //Server server = new Server(Server.DearMessage);
            //server.Create_Service(8080);
            //server.Listen();
            //server.Close_Service();
            #endregion

            #region Test To JSON
            //TestJson tj = new TestJson()
            //{
            //    Id = 0,
            //    Name = "IBAS",
            //    Age = 20
            //};
            //string json = JsonConvert.SerializeObject(tj);
            //Console.WriteLine(json);
            //TestJson o = JsonConvert.DeserializeObject<TestJson>(json);
            //Type t = o.GetType();//typeof(TestJson);
            //foreach (var i in t.GetProperties())
            //{
            //    Console.WriteLine(i.Name + ":" + i.GetValue(o));
            //}
            #endregion

            #region Test Instance
            //TestJson tj = InstanceClass.GetClass();
            //tj.Id = 1;
            //tj.Name = "IBAS";
            //tj.Age = 20;
            //Console.WriteLine(JsonConvert.SerializeObject(tj));
            #endregion

            #region Test Request And Database
            Server server = new Server(myFun);
            server.Create_Service(8080);
            server.Listen();
            server.Close_Service();
            #endregion

            #region 
            //string json = "{\"dbtype\":2,\"FileName\":\"D:/NET_DISK_FILE_SYSTEM_r.mdf\"}";
            //Database_ db = JsonConvert.DeserializeObject<Database_>(json);
            //DBBase db_ = DBInstance.GetDBInstance(db.dbtype);
            //db_.FileName = db_.FileName;
            //db_.TryToConnect();
            //DearDataSet(db_.Query_with_result("select * from nd_user"));
            #endregion
        }
        public static ServerDear myFun(Dictionary<string, string> dic)
        {
            DBBase db = null;
            DataSet ds = null;
            int result = 0;
            bool isConnect = false;
            if (dic.ContainsKey("connect"))
            {
                Database_ db_ = JsonConvert.DeserializeObject<Database_>(dic["connect"]);
                db = DBInstance.GetDBInstance(db_.dbtype);
                db.Sql_Seting(db_.sqlSet);
                db.FileName = db_.FileName;
                isConnect = db.TryToConnect();
                if (isConnect)
                {
                    if (dic.ContainsKey("sql"))
                    {
                        ds = db.Query_with_result(dic["sql"]);
                    }
                    if (dic.ContainsKey("non_sql"))
                    {
                        result = db.Query_non_result(dic["sql"]);
                    }
                    db.DisConnect();
                }
            }
            else
            {
                return new ServerDear()
                {
                    isOver = false,
                    data = new
                    {
                        statue = "success",
                        data = "nothing"
                    }
                };
            }
            return new ServerDear()
            {
                isOver = false,
                data = new
                {
                    isConnect = isConnect,
                    influenceline = result,
                    searchResult = JsonConvert.SerializeObject(ds)
                }
            };
        }
        public static void DearDataSet(DataSet ds)
        {
            if (ds == null)
            {
                Console.WriteLine("ds is null");
            }
            else
            {
                for (int i = 0; i < ds.Tables.Count; i++)
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
        public DBType dbtype {get;set;}
        public string FileName { get; set; }
        public SqlDb_Setting sqlSet { get; set; }
    }
    class TestJson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    class InstanceClass
    {
        public static TestJson GetClass()
        {
            return (TestJson)Activator.CreateInstance(Type.GetType("testMyDbDll.TestJson"));
        }
    }
}
