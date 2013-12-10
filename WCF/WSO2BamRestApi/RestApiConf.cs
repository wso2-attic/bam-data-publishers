using System;

namespace WSO2BamRestApi
{
    public class RestApiConf
    {
        public RestApiConf() { }

        private string host;
        private string port;
        private string username;
        private string password;

        internal string getHost()
        {
            return host;
        }

        internal string getPort()
        {
            return port;
        }

        internal string getUserName()
        {
            return username;
        }

        internal string getPassword()
        {
            return password;
        }

        public void setHost(string host)
        {
            this.host = host;
        }

        public void setPort(string port)
        {
            this.port = port;
        }

        public void setUserName(string username)
        {
            this.username = username;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }
    }
}