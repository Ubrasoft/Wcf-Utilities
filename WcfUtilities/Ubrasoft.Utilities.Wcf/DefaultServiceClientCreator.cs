using System;
using System.ServiceModel;

namespace Ubrasoft.Utilities.Wcf
{
    public static class DefaultServiceClientCreator
    {
        public static TClient CreateClient<TClient, TService>(
            IClientMessageLogger clientMessageLogger,
            ILogMetadataProvider logMetadataProvider, 
            string url, 
            string proxy,
            string username = null, 
            string password = null)
            where TClient : ClientBase<TService> 
            where TService : class
        {
            bool sendCredentials = username != null && password != null;
            BasicHttpBinding binding = new BasicHttpBinding();
            if (proxy != null)
            {
                binding.ProxyAddress = new Uri(proxy);
                binding.UseDefaultWebProxy = false;
            }
            binding.Security.Mode = BasicHttpSecurityMode.Transport;
            if (sendCredentials) binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Basic;
            else binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;

            var address = new EndpointAddress(url);

            var client = Activator.CreateInstance(typeof(TClient), binding, address) as TClient;
            if (sendCredentials)
            {
                client.ClientCredentials.UserName.UserName = username;
                client.ClientCredentials.UserName.Password = password;
            }

            client.Endpoint.EndpointBehaviors.Add(
                new MessageLogging.MessageLoggingEndpointBehavior(clientMessageLogger, logMetadataProvider));

            return client;
        }
    }
}
