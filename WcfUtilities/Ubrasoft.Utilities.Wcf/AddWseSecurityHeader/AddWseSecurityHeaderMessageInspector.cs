using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Ubrasoft.Utilities.Wcf.AddWseSecurityHeader
{
    /// <summary>
    /// Custom Message Inspector to add WSE security header to the request before sending
    /// </summary>
    internal class MessageLoggingMessageInspector : IClientMessageInspector
    {
        private readonly string username;
        private readonly string password;

        public MessageLoggingMessageInspector(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        { }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            CustomWseSecurityHeader securityHeader =
                new CustomWseSecurityHeader(username, password);

            request.Headers.Add(securityHeader);
            return request;
        }

        #endregion
    }
}
