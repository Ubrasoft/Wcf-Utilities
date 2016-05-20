using System;
using System.Collections.Generic;

namespace Ubrasoft.Utilities.Wcf
{
    public class WebServiceAccessInfo
    {
        public string Url { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool AddHttpSecurityHeader { get; set; }
        public bool AddWseSecurityHeader { get; set; }

        public WebServiceProxyInfo Proxy { get; set; }
    }
}