using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.DatabaseTools
{
    public class DatabaseTools
    {
        private readonly DatabaseConnection dbConnection;

        public DatabaseTools()
        {
            dbConnection = new DatabaseConnection();
        }

        // Asynchronní otevření připojení
        private async Task OpenDBAsync()
        {
            OracleConnection conn = dbConnection.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                await conn.OpenAsync();
            }
        }

        // Asynchronní zavření spojení
        private async Task CloseDBAsync()
        {
            OracleConnection conn = dbConnection.GetConnection();
            if (conn.State == ConnectionState.Open)
            {
                await conn.CloseAsync();
            }
        }

        // Asynchronní vracení datové tabulky z databáze
        public async Task<DataTable> ExecuteCommandAsync(string commandText, Dictionary<string, object> parameters = null)
        {
            DataTable dataTable = new DataTable();
            OracleConnection conn = dbConnection.GetConnection();
            try
            {
                await OpenDBAsync();

                using (OracleCommand command = conn.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.StoredProcedure;

                    OracleParameter outputParameter = new OracleParameter("cursor", OracleDbType.RefCursor)
                    {
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(outputParameter);

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
                        await Task.Run(() => adapter.Fill(dataTable));
                    }
                }
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                await CloseDBAsync();
            }

            return dataTable;
        }
    }
}
