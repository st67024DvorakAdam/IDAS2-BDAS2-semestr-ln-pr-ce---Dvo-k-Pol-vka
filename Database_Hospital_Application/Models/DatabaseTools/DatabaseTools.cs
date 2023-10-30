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
        private void OpenDB()
        {
            OracleConnection conn = dbConnection.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        // Zavření spojení
        private void CloseDB()
        {
            OracleConnection conn = dbConnection.GetConnection();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }


        // Vrací danou tabulku dat z db
        public DataTable ExecuteCommand(string commandText, Dictionary<string, object> parameters = null)
        {
            DataTable dataTable = new DataTable();
            OracleConnection conn = dbConnection.GetConnection();

            try
            {
                OpenDB();
                using (OracleCommand command = new OracleCommand(commandText, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            OracleParameter oracleParameter = new OracleParameter(parameter.Key, parameter.Value);
                            command.Parameters.Add(oracleParameter);
                        }
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                CloseDB();
            }
            return dataTable;
        }




    }
}
