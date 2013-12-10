using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace WSO2BamRestApi
{
    public class DataPublisher
    {
        string url = RestApiProperties.DEFAULT_URL;
        string username = RestApiProperties.DEFAULT_USERNAME;
        string password = RestApiProperties.DEFAULT_PASSWORD;

        public DataPublisher() { }

        public DataPublisher(RestApiConf rest)
        {
            this.url = RestApiProperties.SECURE_SERVICE_SPECIFIER +
                RestApiProperties.PROTOCOL_HOST_SEPERATOR + rest.getHost() +
                RestApiProperties.HOST_PORT_SEPERATOR + rest.getPort() +
                RestApiProperties.DEFAULT_FILE_RESOURCE_LOCATION;
            this.username = rest.getUserName();
            this.password = rest.getPassword();
        }

        public void defineStream(string streamDef)
        {
            /*StreamReader response = new StreamReader(*/this.sendHttpReq(this.getStrDefUrl(),
                this.username, this.password,
                RestApiProperties.DEFAULT_METHOD, streamDef)/*.GetResponseStream())*/;
            //return response.ReadToEnd();
        }

        public void publish(string events, string streamName, string streamVersion)
        {
            /*StreamReader response = new StreamReader(*/this.sendHttpReq(this.getSendEvntUrl(streamName,
                streamVersion), this.username,
                this.password, RestApiProperties.DEFAULT_METHOD, events)/*.GetResponseStream())*/;
            //return response.ReadToEnd();
        }

        private string getStrDefUrl()
        {
            return this.url + RestApiProperties.STREAM_DEFINING_LOCATION;
        }

        private string getSendEvntUrl(string name, string version)
        {
            return this.url + RestApiProperties.STREAM_EXISTING_LOCATION + name + RestApiProperties.URI_FOLDERS_SEPERATOR
                + version + RestApiProperties.URI_FOLDERS_SEPERATOR;
        }

        private async void sendHttpReq(string url, string username, string password, string method,
            string messageContent)
        {
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(url);
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(messageContent);

            httpWReq.Method = method;
            httpWReq.ContentType = RestApiProperties.DEFAULT_CONTENT_TYPE;
            httpWReq.Accept = RestApiProperties.DEFAULT_ACCEPT;
            httpWReq.ContentLength = data.Length;
            httpWReq.Headers[RestApiProperties.AUTH_HEADER_NAME] = RestApiProperties.HEADER_VALUE_BEGINER
                + Convert.ToBase64String( Encoding.Default.GetBytes(username + RestApiProperties.USERNAME_PASSWORD_SEPERATOR + password));
            //httpWReq.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback
            (
                delegate { return true; }
            );
            
            using (Stream stream = httpWReq.GetRequestStream())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }
            HttpWebResponse response;
            try
            {
                response = httpWReq.GetResponse() as HttpWebResponse;
            }
            catch (WebException ex) { Console.WriteLine(ex.Response.ToString()); }
        }
    }
}