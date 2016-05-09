using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Ubrasoft.Utilities.Wcf.AddWseSecurityHeader
{
    /// <summary>
    /// Custom Endpoint Behavior
    /// </summary>
    public class AddWseSecurityHeaderEndpointBehavior : IEndpointBehavior
    {
        private readonly string username;
        private readonly string password;

        public AddWseSecurityHeaderEndpointBehavior(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            MessageLoggingMessageInspector inspector =
                new MessageLoggingMessageInspector(username, password);

            clientRuntime.MessageInspectors.Add(inspector);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        { }

        public void Validate(ServiceEndpoint endpoint)
        { }

        #endregion
    }
}
