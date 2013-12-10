using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSO2BamRestApi
{
    public class RestApiProperties
    {
        internal static string DEFAULT_USERNAME = "admin";
        internal static string DEFAULT_PASSWORD = "admin";
        internal static string DEFAULT_URL = "https://localhost:9443/datareceiver/1.0.0/";
        internal static string SECURE_SERVICE_SPECIFIER = "https";
        internal static string PROTOCOL_HOST_SEPERATOR = "://";
        internal static string HOST_PORT_SEPERATOR = ":";
        internal static string DEFAULT_FILE_RESOURCE_LOCATION = "/datareceiver/1.0.0/";
        internal static string STREAM_DEFINING_LOCATION = "streams/";
        internal static string STREAM_EXISTING_LOCATION = "stream/";
        internal static string DEFAULT_CONTENT_TYPE = "application/json";
        internal static string DEFAULT_ACCEPT = "application/json";
        internal static string DEFAULT_METHOD = "POST";
        internal static string AUTH_HEADER_NAME = "Authorization";
        internal static string HEADER_VALUE_BEGINER = "Basic ";
        internal static string URI_FOLDERS_SEPERATOR = "/";
        internal static string USERNAME_PASSWORD_SEPERATOR = ":";
    }
}