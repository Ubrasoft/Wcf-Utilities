using System;
using System.Net;
using System.ServiceModel;
using Ubrasoft.Utilities.Wcf.AddWseSecurityHeader;
using Ubrasoft.Utilities.Wcf.MessageLogging;

namespace Ubrasoft.Utilities.Wcf
{
    public static class ServiceClientCreator
    {
        public static TClient CreateClient<TClient, TService>(
            WebServiceAccessInfo webServiceAccessInfo,
            IClientMessageLogger clientMessageLogger,
            object metadataObject)
            where TClient : ClientBase<TService>
            where TService : class
        {
            // Create binding.
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            if (webServiceAccessInfo.AddHttpSecurityHeader)
                binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            else binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            // Set proxy info.
            if (webServiceAccessInfo.Proxy != null)
            {
                var proxyUriBuilder = new UriBuilder(webServiceAccessInfo.Proxy.Url);

                if (!string.IsNullOrWhiteSpace(webServiceAccessInfo.Proxy.Username) &&
                    !string.IsNullOrWhiteSpace(webServiceAccessInfo.Proxy.Password))
                {
                    proxyUriBuilder.UserName = webServiceAccessInfo.Proxy.Username;
                    proxyUriBuilder.Password = webServiceAccessInfo.Proxy.Password;
                }

                binding.UseDefaultWebProxy = false;
                binding.ProxyAddress = proxyUriBuilder.Uri;
            }

            // Create a soap client.
            var client = Activator.CreateInstance(typeof(TClient), binding, new EndpointAddress(webServiceAccessInfo.Url)) as TClient;

            // Validate username and password; if any security header is sent.
            if ((webServiceAccessInfo.AddHttpSecurityHeader ||
                webServiceAccessInfo.AddWseSecurityHeader)
                &&
                (string.IsNullOrWhiteSpace(webServiceAccessInfo.Username) ||
                string.IsNullOrWhiteSpace(webServiceAccessInfo.Password)))
                throw new ArgumentException("WebServiceUsernameAndPasswordShouldNotBeEmpty");

            // Add basic http authentication Header
            if (webServiceAccessInfo.AddHttpSecurityHeader)
            {
                client.ClientCredentials.UserName.UserName = webServiceAccessInfo.Username;
                client.ClientCredentials.UserName.Password = webServiceAccessInfo.Password;
            }

            // Add Wse security Header.
            if (webServiceAccessInfo.AddWseSecurityHeader)
            {
                var securityHeaderBehaviour = new AddWseSecurityHeaderEndpointBehavior(webServiceAccessInfo.Username, webServiceAccessInfo.Password);
                client.Endpoint.EndpointBehaviors.Add(securityHeaderBehaviour);
            }

            // Set message logging inspector.
            {
                var logginBehaviour = new MessageLoggingEndpointBehavior(clientMessageLogger, metadataObject);
                client.Endpoint.EndpointBehaviors.Add(logginBehaviour);
            }

            return client;
        }
    }
}
