using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace MicrosoftExcelAndAccess
{
    /// <summary>
    /// this class will dear with the connect about excel and access file
    /// </summary>
    public class MDBDB : DBBase
    {
        //public string FileName { get; set; } = null;
        //private bool isConnect = false;
        //public bool IsConnect { get { return isConnect; } }
        protected OleDbConnection myConn = null;
        public OleDbConnection MyConn { get { return myConn; } }
        private string strCon_1 = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
        private string strCon_2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=";
        private bool IsExcel { get; set; } = false;
        /// <summary>
        /// default constructed function
        /// </summary>
        public MDBDB(bool isExcel = false)
        {
            IsExcel = isExcel;
            if (isExcel)
            {
                Type = "excel";
            }
            else
            {
                Type = "access";
            }
            Err_sb.Clear();
        }
        /// <summary>
        /// Try to connection 
        /// </summary>
        /// <returns>is it connected ?</returns>
        public override bool TryToConnect()
        {
            #region Check FileName
            if (string.IsNullOrEmpty(FileName))
            {
                Err_sb.Clear();
                Err_sb.Append("[Error] File Name is not initial .");
                return IsConnect;
            }
            #endregion
            #region Check File Exist
            if (!File.Exists(FileName))
            {
                Err_sb.Clear();
                Err_sb.Append("[Error] File is not exist .");
                return IsConnect;
            }
            #endregion
            if (isConnect)
            {
                Err_sb.Clear();
                Err_sb.Append("[Error] File is connected ,please disconnection first .");
                return isConnect;
            } else
            {
                TryToConnectHelp(strCon_1);
                if (!IsConnect && IsExcel)
                {
                    TryToConnectHelp(strCon_2);
                }
            }
            return isConnect;
        }
        /// <summary>
        /// Try to connetion child help 
        /// </summary>
        /// <param name="con_str">default connection string</param>
        /// <returns>is it connected ?</returns>
        private bool TryToConnectHelp(string con_str)
        {
            StringBuilder con_sb = new StringBuilder();
            con_sb.Append(con_str);
            con_sb.Append(FileName);
            if (IsExcel) {
                con_sb.Append("; Extended Properties = 'Excel 8.0;HDR=Yes;IMEX=1;'");
            }
            myConn = new OleDbConnection(con_sb.ToString());
            #region Try To Connect Database
            try
            {
                myConn.Open();
                isConnect = true;
            }
            catch (InvalidOperationException ex)
            {
                Err_sb.Clear();
                Err_sb.Append(ex.Message);
                Err_Ex = ex;
                isConnect = false;
            }
            catch (OleDbException ex)
            {
                Err_sb.Clear();
                Err_sb.Append(ex.Message);
                Err_Ex = ex;
                isConnect = false;
            }
            finally
            {
            }
            #endregion
            return isConnect;
        }
        /// <summary>
        /// Disconnect the connection to database
        /// </summary>
        public override void DisConnect()
        {
            if (IsConnect)
            {
                if (myConn != null) { 
                    myConn.Close();
                }
                isConnect = false;
            }
            if (myConn != null)
            {
                myConn = null;
            }
        }
        /// <summary>
        /// query from database
        /// </summary>
        /// <param name="sql">query string</param>
        /// <returns>influenced lines count</returns>
        public override int Query_non_result(string sql) {
            int result = 0;
            if (isConnect)
            {
                OleDbCommand sql_com = new OleDbCommand(sql, myConn);
                result = sql_com.ExecuteNonQuery();
            } else
            {
                Err_sb.Clear();
                Err_sb.Append("Please connect first .");
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// query form database
        /// </summary>
        /// <param name="sql">query string</param>
        /// <returns>result</returns>
        public override DataSet Query_with_result(string sql)
        {
            if (isConnect) { 
                DataSet ds = new DataSet();
                OleDbDataAdapter apt = new OleDbDataAdapter(sql, myConn);
                apt.Fill(ds);
                return ds;
            } else
            {
                Err_sb.Clear();
                Err_sb.Append("Please connect first .");
                return null;
            }
        }
        /// <summary>
        /// Get all table from a database file
        /// </summary>
        /// <returns>tables recording table</returns>
        public DataTable GetAllTable()
        {
            return GetSP(OleDbSchemaGuid.Tables, null);
        }
        /// <summary>
        /// Get special format table from the database
        /// </summary>
        /// <param name="odg">special format</param>
        /// <param name="order">every column order rule (please reference the second param tip about GetOleDbSchemaTable)</param>
        /// <returns></returns>
        public DataTable GetSP(Guid odg,object[] order)
        {
            if (IsConnect)
            {
                return myConn.GetOleDbSchemaTable(odg, order);
            } else
            {
                return null;
            }
        }

        public override void Sql_Seting(object sql_set)
        {
        }

        /// <summary>
        /// Get a table header field
        /// </summary>
        /// <param name="dt">table</param>
        /// <returns>header list or null</returns>
        /// <statement>
        /// Fro access file , this method is nice suport
        /// Fro excel file , if the first line is number , the first will be cut 
        ///                  if the first line is null or empty , the first line will be cut 
        ///                         and if the second line is empty too , the second line will be cut too,
        ///                         and the rule will be use to next line to the lase line
        ///                  if the first line is not empty , and some cell at first line is empty ,
        ///                         the result will be Fn , and n is the cell index .
        ///Fro Excel File , the result will be one of the result as follow :
        ///                 First : xxx xxx xxx xxx xxx
        ///                 Second : xxx F2 F3 xxx F4
        ///                 Third : F1 F2 F3 F4
        ///For Excel File , if result is the First , It may be happened that the first line is dispear .
        /// </statement>
    }
    
}
