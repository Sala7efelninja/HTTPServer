﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HTTPServer
{

    public enum StatusCode
    {
        OK = 200,
        InternalServerError = 500,
        NotFound = 404,
        BadRequest = 400,
        Redirect = 301
    }

    class Response
    {
        string br="/r/n";
        string responseString;
        public string ResponseString
        {
            get
            {
                return responseString;
            }
        }
        StatusCode code;
        List<string> headerLines = new List<string>();
        public Response(StatusCode code, string contentType, string content, string redirectoinPath)
        {
            //FTODO: Add headlines (Content-Type, Content-Length,Date, [location if there is redirection])
            headerLines.Add("Content-Type: " +contentType);
            headerLines.Add("Conent-Length: "+ content.Length);
            headerLines.Add("Date: "+ DateTime.Now.ToLocalTime());
            if(redirectoinPath.Length!=0)
                headerLines.Add("location: "+redirectoinPath);


            //FTODO: Create the request string
            responseString =GetStatusLine(code) + br;
            foreach (string header in headerLines)
                responseString += header + br;
            responseString += br + content;
        }

        private string GetStatusLine(StatusCode code)
        { // new did
            // TODO: Create the response status line and return it
            
            string statusLine = string.Empty;
          
            if (StatusCode.OK == code)
            {
                statusLine = Configuration.ServerHTTPVersion + " " + code + " " + "OK";
            }
            else if (StatusCode.BadRequest == code)
            {
                statusLine = Configuration.ServerHTTPVersion + " " + code + " " + "BadRequest";
            }
            else if (StatusCode.NotFound == code)
            {
                statusLine = Configuration.ServerHTTPVersion + " " + code + " " + "NotFound";
            }
            else if (StatusCode.InternalServerError == code)
            {
                statusLine = Configuration.ServerHTTPVersion + " " + code + " " + "InternalServerError";
            }
            else if (StatusCode.Redirect == code)
            {
                statusLine = Configuration.ServerHTTPVersion + " " + code + " " + "Redirect";
            }
            return statusLine;
            
        }
    }
}
