using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class SystemCatalogRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<DataClass> Data { get; set; }

        public SystemCatalogRepo()
        {
            Data = new ObservableCollection<DataClass>();
        }

        public async Task<ObservableCollection<DataClass>> GetSystemCatalogAsync()
        {
            string owner = "st67024";
            string commandText = "get_objects";
            var parameters = new List<OracleParameter>
            {
                new OracleParameter("owner_in", OracleDbType.Varchar2, owner, ParameterDirection.Input)
            };

            DataTable result = await dbTools.ExecuteCommandAsyncOracle(commandText, parameters);


            Data.Clear(); 

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    DataClass data = new DataClass
                    {
                        Owner = row["OWNER"].ToString(),
                        ObjectName = row["OBJECT_NAME"].ToString(),
                        ObjectType = row["OBJECT_TYPE"].ToString(),
                    };
                    Data.Add(data);
                }
            }
            return Data;
        }

    }

    public class DataClass
    {
        public string Owner { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
    }
}
