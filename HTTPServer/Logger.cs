using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HTTPServer
{
    class Logger
    {
        public static void LogException(Exception ex)
        {
            //FTODO: Create log file named log.txt to log exception details in it
            //Datetime:
            //message:
            // for each exception write its details associated with datetime 
            using (StreamWriter sr = new StreamWriter("log.txt",true))
            {
                sr.WriteLine("Datetime: " + DateTime.Now.ToLocalTime());
                sr.WriteLine("Message: " + ex.Message);
            }
            
            //sr.Close();
            
            
            
        }
    }
}
