using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace HTTPServer
{
    class Server
    {
        Socket serverSocket;
        int port;
        public Server(int portNumber, string redirectionMatrixPath)
        {
            //FTODO: call this.LoadRedirectionRules passing redirectionMatrixPath to it
            LoadRedirectionRules(redirectionMatrixPath);
            //FTODO: initialize this.serverSocket
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            port = portNumber;
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, port);
            serverSocket.Bind(iep);
            
        }

        public void StartServer()
        {
            //FTODO: Listen to connections, with large backlog.
            serverSocket.Listen(1000);
            //FTODO: Accept connections in while loop and start a thread for each connection on function "Handle Connection"
            while (true)
            {
                //FTODO: accept connections and start thread for each accepted connection.
                Socket client =serverSocket.Accept();
                Thread thread = new Thread(new ParameterizedThreadStart(HandleConnection));
                thread.Start(client);
            }
        }

        public void HandleConnection(object obj)
        {
            //FTODO: Create client socket 
            // set client socket ReceiveTimeout = 0 to indicate an infinite time-out period
            Socket client = (Socket)obj;
            client.ReceiveTimeout = 0;
            //FTODO: receive requests in while true until remote client closes the socket.
            while (true)
            {
                try
                {
                    byte[] buffer=new byte[1024*1024];
                    //FTODO: Receive request
                    int len=client.Receive(buffer);
                    //FTODO: break the while loop if receivedLen==0
                    if (len == 0)
                        break;
                    //FTODO: Create a Request object using received request string
                    Request request = new Request(ASCIIEncoding.ASCII.GetString(buffer));
                    //FTODO: Call HandleRequest Method that returns the response
                    Response response=HandleRequest(request);
                    //FTODO: Send Response back to client
                    buffer = ASCIIEncoding.ASCII.GetBytes(response.ResponseString);
                    client.Send(buffer);

                }
                catch (Exception ex)
                {
                    //FTODO: log exception using Logger class
                    Logger.LogException(ex);
                    break;//????????????????????????????
                }
            }

            //FTODO: close client socket
            client.Close();
        }

        Response HandleRequest(Request request)
        {
            throw new NotImplementedException();
            string content;
            try
            {
                //TODO: check for bad request 

                //TODO: map the relativeURI in request to get the physical path of the resource.

                //TODO: check for redirect

                //TODO: check file exists

                //TODO: read the physical file

                // Create OK response
            }
            catch (Exception ex)
            {
                // TODO: log exception using Logger class
                // TODO: in case of exception, return Internal Server Error. 
            }
        }

        private string GetRedirectionPagePathIFExist(string relativePath)
        {//new did
            // using Configuration.RedirectionRules return the redirected page path if exists else returns empty
            if (Configuration.RedirectionRules.ContainsKey(relativePath))
                return Configuration.RedirectionRules[relativePath];
            

            return string.Empty;
        }

        private string LoadDefaultPage(string defaultPageName)
        {// new did
            string filePath = Path.Combine(Configuration.RootPath, defaultPageName);
             // TODO: check if filepath not exist log exception using Logger class and return empty string
                                         
            if (!File.Exists(filePath))
            {
                Logger.LogException(new Exception());
                return string.Empty;
            }
            // else read file and return its content
            FileStream files = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            StreamReader streams = new StreamReader(files);
            string Content = streams.ReadToEnd();
            
            return Content;
        }

        private void LoadRedirectionRules(string filePath)
        {
            try
            {
                //FTODO: using the filepath paramter read the redirection rules from file 
                string[] rules = File.ReadAllLines(filePath);
                // then fill Configuration.RedirectionRules dictionary
                Configuration.RedirectionRules = new Dictionary<string, string>();
                foreach(string rule in rules)
                {
                    string[] r = rule.Split(',');
                    Configuration.RedirectionRules[r[0]] = r[1];
                    //Console.WriteLine(r[0]+" "+ Configuration.RedirectionRules[r[0]]);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
        }
    }
}
