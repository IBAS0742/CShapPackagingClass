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
    public class MDFDB : SqlServerDB
    {
        public string connection_string { get; set; }
            = @"Data Source=.\SQLEXPRESS;AttachDbFilename=[mfn];Integrated Security=True;User Instance=True";
        public MDFDB() {
            Type = "mdf";
        }
        public override string TryToConnectHelp()
        {
            return connection_string.Replace("[mfn]",FileName);
        }
        public override void Sql_Seting(object sql_set)
        {
        }
    }
}
