using System;
using System.Collections.Generic;

namespace Ubrasoft.Utilities.Wcf
{
    public class WebServiceAccessInfo
    {
        public WebServiceAccessInfo(string url, string username, string password,
            bool addHttpSecurityHeader = false,
            bool addWseSecurityHeader = false,
            WebServiceProxyInfo proxy = null)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new Exception("UrlIsMissing");
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("UsernameIsMissing");
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("PasswordIsMissing");

            Url = url;
            Username = username;
            Password = password;
            AddHttpSecurityHeader = addHttpSecurityHeader;
            AddWseSecurityHeader = addWseSecurityHeader;
            Proxy = proxy;
        }

        public string Url { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public bool AddHttpSecurityHeader { get; private set; }
        public bool AddWseSecurityHeader { get; private set; }

        public WebServiceProxyInfo Proxy { get; private set; }
    }
}