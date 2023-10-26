using Database_Hospital_Application.Models.Entities;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.DatabaseTools
{
    public class DatabaseTools
    {
        private readonly DatabaseConnection dbConnection;
        public DatabaseTools() { dbConnection = new DatabaseConnection(); }

        // Otevření připojení
        public void OpenDB()
        {
            OracleConnection conn = dbConnection.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        // Zavření spojení
        public void CloseDB()
        {
            OracleConnection conn = dbConnection.GetConnection();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        



    }
}
