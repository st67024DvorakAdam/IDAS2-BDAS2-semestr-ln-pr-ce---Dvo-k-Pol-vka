using Database_Hospital_Application.Models.Entities;
using Database_Hospital_Application.ViewModels.ViewsVM;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Windows;

namespace Database_Hospital_Application.Models.Repositories
{
    public class MessageRepo
    {
        private DatabaseTools.DatabaseTools dbTools = new DatabaseTools.DatabaseTools();

        public ObservableCollection<Message> Messages { get; set; }

        public MessageRepo()
        {
            Messages = new ObservableCollection<Message>();
        }

        public async Task<ObservableCollection<Message>> GetAllMessagesAsync()
        {
            ObservableCollection<Message> messages = new ObservableCollection<Message>();
            string commandText = "get_all_messages"; 
            DataTable result = await dbTools.ExecuteCommandAsync(commandText, null);

            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    Message message = new Message
                    {
                        ID = Convert.ToInt32(row["ID"]),
                        Content = row["OBSAH"].ToString(),
                        Sender = Convert.ToInt32(row["UZIVATEL_ID"]),
                        Recipient = Convert.ToInt32(row["UZIVATEL_ID2"]),
                        DateSent = new Oracle.ManagedDataAccess.Types.OracleTimeStamp(Convert.ToDateTime(row["CASOVE_RAZITKO"]))
                        
                    };
                    messages.Add(message);
                }
            }
            return messages;
        }

        public void AddMessage(Message message)
        {
           
        }

        public void DeleteMessage(int id)
        {
            
        }

        public void UpdateMessage(Message message)
        {
            
        }

        public void DeleteAllMessages()
        {
            
        }
    }
}


