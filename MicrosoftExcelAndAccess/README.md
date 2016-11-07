Usage :
1 import the Library "MicrosoftExcelAndAccess.dll"
2 using it
3 start to use

DBBase is a abstract class , and it have three child class {MDBDB,MDFDB,SqlDB}.
So , the first is to get one DBBase instance by DBInstance.GetDBInstance(DBType).
And then , set the FileName attribute .
The last step is try to connect by using TryToConnect , and the method will return a boolean , true is connect successfully or false connect failly .
If connect is success , you can use the Query_with_result or Query_non_result to over you query .

TIP : SQL SERVER is special , it should give a object SqlDb_Setting .
SqlDb_Setting have five prototies (a)Server_Machine and (b)Database and (c)UserName and (d)Password and (e)isWindowsAuth .
[c and d is match and e is odd , you can choose c and d or choose e but none is not agree . a and b is required .]

//###2016-11-07 14:35:50 ACCESS###
#region 
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

//###2016-11-07 16:26:08 Excel###
#region 
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

//###2016-11-07 16:27:50 MDF###
#region  (something easy to error)
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

//###2016-11-07 16:37:29 SQL SERVER###
#region 
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