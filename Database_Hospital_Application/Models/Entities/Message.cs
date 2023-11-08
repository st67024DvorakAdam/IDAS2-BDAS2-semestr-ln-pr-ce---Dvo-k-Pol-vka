using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Entities
{
    public class Message
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public int Sender { get; set; }
        public int Recipient { get; set; }      
        public OracleTimeStamp DateSent { get; set; }

    }
}
