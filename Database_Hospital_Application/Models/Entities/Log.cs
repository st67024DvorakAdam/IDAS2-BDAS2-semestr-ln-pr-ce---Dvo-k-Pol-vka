using Oracle.ManagedDataAccess.Types;
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
        public string OperationType { get; set; }
        public OracleTimeStamp TimeStamp { get; set; }
        public string Table { get; set; }
        public string OldState { get; set; } 
        public string NewState { get; set; }   
        
    }
}
