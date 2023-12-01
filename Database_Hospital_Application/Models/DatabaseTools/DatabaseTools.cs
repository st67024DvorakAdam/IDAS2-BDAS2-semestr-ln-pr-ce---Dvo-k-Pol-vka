using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows;

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

                    using (OracleDataReader reader = (OracleDataReader)await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader); 
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await CloseDBAsync();
            }

            return dataTable;
        }


        public async Task<string> ExecuteCommandAsyncReturnString(string commandText)
        {
            string output = "";
            OracleConnection conn = dbConnection.GetConnection();
            try
            {
                await OpenDBAsync();

                using (OracleCommand command = new OracleCommand(commandText, conn))
                {
                    object result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        output = result.ToString();
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await CloseDBAsync();
            }

            return output;
        }


        public async Task<DataTable> ExecuteCommandAsyncOracle(string commandText, List<OracleParameter> parameters = null)
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
                        foreach (OracleParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                    }

                    using (OracleDataReader reader = (OracleDataReader)await command.ExecuteReaderAsync())
                    {
                        dataTable.Load(reader);
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await CloseDBAsync();
            }

            return dataTable;
        }


        public async Task<int> ExecuteNonQueryAsync(string commandText, Dictionary<string, object> parameters = null)
        {
            OracleConnection conn = dbConnection.GetConnection();
            int affectedRows = 0;
            try
            {
                await OpenDBAsync();

                using (OracleCommand command = conn.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var parameter in parameters)
                        {
                            OracleParameter oracleParameter = new OracleParameter(parameter.Key, parameter.Value);
                            command.Parameters.Add(oracleParameter);
                        }
                    }

                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await CloseDBAsync();
            }

            return affectedRows;
        }

        public async Task<int> ExecuteNonQueryAsync(string commandText, List<OracleParameter> parameters = null)
        {
            
            OracleConnection conn = dbConnection.GetConnection();
            int affectedRows = 0;
            try
            {
                await OpenDBAsync();

                using (OracleCommand command = conn.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                    }

                    affectedRows = await command.ExecuteNonQueryAsync();
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                await CloseDBAsync();
            }

            return affectedRows;
        }
    }
}
