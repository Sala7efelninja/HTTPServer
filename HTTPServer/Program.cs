using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HTTPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //FTODO: Call CreateRedirectionRulesFile() function to create the rules of redirection 
            CreateRedirectionRulesFile();
            //Start server
            // 1) Make server object on port 1000
            Console.WriteLine(DateTime.Now.ToLocalTime());
            Server server = new Server(10000, "redirectionRules.txt");
            //2) Start Server
            server.StartServer();
        }

        static void CreateRedirectionRulesFile()
        {
            string rules =
                "aboutus.html,aboutus2.html" + "\n";
            //FTODO: Create file named redirectionRules.txt
            // each line in the file specify a redirection rule
            // example: "aboutus.html,aboutus2.html"
            // means that when making request to aboustus.html,, it redirects me to aboutus2
            StreamWriter writer = new StreamWriter("redirectionRules.txt");
            writer.Write(rules);
            writer.Close();
            Console.WriteLine("Redirection Rules Created");
        }
         
    }
}
