using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HTTPServer
{
    class Logger
    {
        static StreamWriter sr = new StreamWriter("log.txt");
        public static void LogException(Exception ex)
        {
            //FTODO: Create log file named log.txt to log exception details in it
            //Datetime:
            //message:
            // for each exception write its details associated with datetime 
            sr.WriteLine("Datetime: {0} {1}", DateTime.Now.ToLocalTime());
            sr.WriteLine("Message: {0}", ex.Message);
            
        }
    }
}
