using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public int EntityId { get; set; }
        public string OperationType { get; set; }
        public DateTime OperationDate { get; set; }
        public string ChangeDetails { get; set; }
        public string PreviousState { get; set; } 
        public string NewState { get; set; } 

        
        
    }
}
