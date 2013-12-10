using System.ServiceModel.Channels;
using WSO2.WCF.MessageInterceptor;
using System.Xml;
using WSO2BamRestApi;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System;
using System.Net;

namespace WCFMessagePublisher
{
    class BAMPublisherElement : InterceptingElement
    {
        protected override ChannelMessageInterceptor CreateMessageInterceptor()
        {
            return new BAMPublisherInterceptor();
        }
    }

    public class BAMPublisherInterceptor : ChannelMessageInterceptor
    {
        DataPublisher dataPublisher;

        public BAMPublisherInterceptor()
        {
            RestApiConf restApiConf = new RestApiConf();
            set_conf(restApiConf);
            dataPublisher = new DataPublisher(restApiConf);
            string streamDef =
                "{" +
                    "'name':'BAM_WCF_MESSAGE_TRACE'," +
                    "'version':'1.0.0'," +
                    "'nickName':'MessageTracerAgent'," +
                    "'correlationData':[" +
                        "{" +
                            "'name':'activity_id'," +
                            "'type':'STRING'" +
                        "}"+
                    "]," +
                    "'payloadData':[" +
                        "{" +
                            "'name':'message'," +
                            "'type':'STRING'" +
                        "}" +
                    "]" +
                "}";

            dataPublisher.defineStream(streamDef);
        }

        public override void OnReceive(ref Message msg)
        {
            string activityId = new System.Random().Next().ToString();
            string message = msg.ToString() .Replace("\n", "\\n")
                                            .Replace("\'", "\\'")
                                            .Replace("\"", "\\\"")
                                            .Replace("&", "\\&")
                                            .Replace("\r", "\\r")
                                            .Replace("\t", "\\t")
                                            .Replace("\b", "\\b")
                                            .Replace("\f", "\\f");
            string events = "[" +
                    "{" +
                        "\"correlationData\" : [\"" + activityId + "\"]," +
                        "\"payloadData\" : [" +
                            "\"" + message + "\"" +
                        "]" +
                    "}" +
                "]";
            dataPublisher.publish( events, "BAM_WCF_MESSAGE_TRACE", "1.0.0");
        }

        public override ChannelMessageInterceptor Clone()
        {
            return new BAMPublisherInterceptor();
        }

        private void set_conf(RestApiConf restApiConf)
        {
            XmlDocument config = new XmlDocument();
            config.Load("IISMessageInterceptor.config");
            restApiConf.setHost(config.SelectSingleNode("/RestApi/host").InnerXml);
            restApiConf.setPort(config.SelectSingleNode("/RestApi/port").InnerXml);
            restApiConf.setUserName(config.SelectSingleNode("/RestApi/username").InnerXml);
            restApiConf.setPassword(config.SelectSingleNode("/RestApi/password").InnerXml);
        }
    }
}