using Database_Hospital_Application.Models.Entities;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Repositories
{
    public class LogRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Log> Logs { get; set; }

        public LogRepo()
        {
            Logs = new ObservableCollection<Log>();
        }

        public async Task<ObservableCollection<Log>> GetAllLogsAsync()
        {
            ObservableCollection<Log> logs = new ObservableCollection<Log>();
            string commandText = "get_all_logs"; 
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Log log = new Log
                    {
                        Id = Convert.ToInt32(row["ID"]),
                        EntityType = row["ENTITY_TYPE"].ToString(),
                        EntityId = Convert.ToInt32(row["ENTITY_ID"]),
                        OperationType = row["OPERATION_TYPE"].ToString(),
                        OperationDate = Convert.ToDateTime(row["OPERATION_DATE"]),
                        ChangeDetails = row["CHANGE_DETAILS"].ToString(),
                        PreviousState = row["PREVIOUS_STATE"].ToString(),
                        NewState = row["NEW_STATE"].ToString()
                    };
                    logs.Add(log);
                }
            }
            return logs;
        }

        
        public void AddLog(Log log)
        {
            
        }

        
        public void DeleteLog(int id)
        {
            
        }

        
        public void UpdateLog(Log log)
        {
            
        }
    }
}
