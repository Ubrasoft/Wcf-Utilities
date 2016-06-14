using System.Runtime.Remoting.Contexts;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Ubrasoft.Utilities.Wcf.MessageLogging
{
    /// <summary>
    /// Custom Endpoint Behavior
    /// </summary>
    public class MessageLoggingEndpointBehavior : IEndpointBehavior
    {
        private readonly IClientMessageLogger _clientMessageLogger;

        public MessageLoggingEndpointBehavior(IClientMessageLogger clientMessageLogger)
        {
            _clientMessageLogger = clientMessageLogger;
        }


        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint,
            BindingParameterCollection bindingParameters)
        { }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            MessageLoggingMessageInspector inspector =
                new MessageLoggingMessageInspector(_clientMessageLogger);

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
