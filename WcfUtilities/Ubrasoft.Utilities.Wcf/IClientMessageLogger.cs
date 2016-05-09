using System.ServiceModel.Channels;

namespace Ubrasoft.Utilities.Wcf
{
    /// <summary>
    /// Implementing class should handle the logging of soap messages.
    /// </summary>
    public interface IClientMessageLogger
    {
        /// <summary>
        /// Handle request message sent to the soap service.
        /// </summary>
        void HandleRequestMessage(Message message, ILogMetadataProvider logMetadataProvider);

        /// <summary>
        /// Handle response message received from the soap service.
        /// </summary>
        void HandleResponseMessage(Message message, ILogMetadataProvider logMetadataProvider);
    }
}