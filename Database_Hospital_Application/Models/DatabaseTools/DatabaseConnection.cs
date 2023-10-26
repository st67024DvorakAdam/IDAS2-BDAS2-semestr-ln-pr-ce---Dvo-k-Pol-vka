using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.DatabaseTools
{
    public class DatabaseConnection
    {

        private static OracleConnection instance;
        private static readonly object lockObject = new object();

        public OracleConnection GetConnection()
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        string oradb = "User Id=st67289;Password=abcde;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=fei-sql3.upceucebny.cz)(PORT=1521))(CONNECT_DATA=(SID=BDAS)(SERVER=DEDICATED)))";
                        instance = new OracleConnection(oradb);
                    }
                }
            }
            return instance;
        }
    }
}
