using System;

namespace Ubrasoft.Utilities.Wcf
{
    public class WebServiceProxyInfo
    {
        public string Url { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public WebServiceProxyInfo(string url, string username = null, string password = null)
        {
            if(string.IsNullOrWhiteSpace(url))
                throw new Exception("UrlIsMissing");
            if(!string.IsNullOrWhiteSpace(username) &&
                string.IsNullOrWhiteSpace(password))
                throw new Exception("PasswordIsMissing");

            Url = url;
            Username = username;
            Password = password;
        }
    }
}