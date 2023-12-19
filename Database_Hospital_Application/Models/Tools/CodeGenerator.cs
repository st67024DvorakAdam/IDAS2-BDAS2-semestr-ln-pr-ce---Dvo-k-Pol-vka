using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Hospital_Application.Models.Tools
{
    public class CodeGenerator
    {

        private static readonly Random random = new Random();

        public static int Generate4DigitCode()
        {
            int code = random.Next(1000, 10000);

            return code;
        }
    }
}
