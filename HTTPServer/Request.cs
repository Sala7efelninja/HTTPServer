using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HTTPServer
{
    public enum RequestMethod
    {
        GET,
        POST,
        HEAD
    }

    public enum HTTPVersion
    {
        HTTP10,
        HTTP11,
        HTTP09
    }

    class Request
    {
        string[] requestLines;
        RequestMethod method;
        public string relativeURI;
        Dictionary<string, string> headerLines;

        public Dictionary<string, string> HeaderLines
        {
            get { return headerLines; }
        }

        HTTPVersion httpVersion;
        string requestString;
        string[] contentLines;

        public Request(string requestString)
        {
            this.requestString = requestString;
        }
        /// <summary>
        /// Parses the request string and loads the request line, header lines and content, returns false if there is a parsing error
        /// </summary>
        /// <returns>True if parsing succeeds, false otherwise.</returns>
        public bool ParseRequest()
        {

            //TODO: parse the receivedRequest using the \r\n delimeter   
            requestLines = requestString.Replace("\r\n", "\n").Split("\n".ToCharArray());
            // check that there is atleast 3 lines: Request line, Host Header, Blank line (usually 4 lines with the last empty line for empty content)
            if (requestLines.Length >= 3)
            {
                bool f = false;
                f = ParseRequestLine();
                if (f == true)
                {
                    f = false;
                    f = ValidateBlankLine();
                    if (f == true)
                    {
                        f = false;
                        f = LoadHeaderLines();
                        if (f == true)
                        {
                            int ind = -1;
                            for (int i = 1; i <= requestLines.Length; i++)
                            {
                                if (requestLines[i] == "\n")
                                {
                                    ind = i;
                                    break;
                                }

                            }
                            int k = 0;
                            for (int i = ind + 1; i <= requestLines.Length; i++)
                            {
                                contentLines[k] = requestLines[i];
                                k++;
                            }

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }

            else
                return false;
            // Parse Request line

            // Validate blank line exists

            // Load header lines into HeaderLines dictionary
        }

        private bool ParseRequestLine()
        {
            string[] requestLine = requestLines[0].Split(' ');
            if (requestLine.Length >= 3)
            {
                if (requestLine[0] == "GET")
                {
                    method = RequestMethod.GET;
                }
                else if (requestLine[0] == "POST")
                {
                    method = RequestMethod.POST;
                }
                else if (requestLine[0] == "HEAD")
                {
                    method = RequestMethod.HEAD;
                }
                relativeURI = requestLine[1];
                if (ValidateIsURI(relativeURI) == false)
                {
                    return false;
                }
                if (requestLine[2] == "HTTP/1.0")
                {
                    httpVersion = HTTPVersion.HTTP10;
                }
                else if (requestLine[2] == "HTTP/1.1")
                {
                    httpVersion = HTTPVersion.HTTP11;
                }
                else
                {
                    httpVersion = HTTPVersion.HTTP09;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateIsURI(string uri)
        {
            return Uri.IsWellFormedUriString(uri, UriKind.RelativeOrAbsolute);
        }

        private bool LoadHeaderLines()
        {
            int ind = -1;
            for (int i = 1; i <= requestLines.Length; i++)
            {
                if (requestLines[i] == "\n")
                {
                    ind = i;
                    break;
                }

            }
            bool f = false;
            for (int i = 1; i < ind; i++)
            {
                string[] h = requestLines[i].Split(':');
                if (h[0] == "Host")
                {
                    f = true;
                }
                headerLines.Add(h[0], h[1]);
            }
            if (f == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateBlankLine()
        {
            int ind = -1;
            for (int i = 0; i <= requestLines.Length; i++)
            {
                if (requestLines[i] == "\n")
                {
                    ind = i;
                }
            }
            if (ind != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
