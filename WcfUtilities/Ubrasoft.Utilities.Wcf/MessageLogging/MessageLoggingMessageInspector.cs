using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace Ubrasoft.Utilities.Wcf.MessageLogging
{
    public class MessageLoggingMessageInspector : IClientMessageInspector
    {
        private readonly IClientMessageLogger _messageLogger;
        private readonly object _metadataObject;

        public MessageLoggingMessageInspector(IClientMessageLogger messageLogger, object metadataObject)
        {
            _messageLogger = messageLogger;
            _metadataObject = metadataObject;
        }

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            // Must use a buffer rather than the origonal message, because the Message's body can be processed only once.
            var messageBuffer = reply.CreateBufferedCopy(Int32.MaxValue);

            // Dispatch message copy to injected message logger.
            try
            {
                var messageForInspection = messageBuffer.CreateMessage();
                _messageLogger.HandleResponseMessage(messageForInspection, _metadataObject);
            }
            catch
            {
                // Silently ignore exception occured in message logger.
                // TODO: Check that error should be logged here.
            }

            reply = messageBuffer.CreateMessage();
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            // Must use a buffer rather than the origonal message, because the Message's body can be processed only once.
            var messageBuffer = request.CreateBufferedCopy(Int32.MaxValue);

            // Dispatch message copy to injected message logger.
            var messageForInspection = messageBuffer.CreateMessage();
            _messageLogger.HandleRequestMessage(messageForInspection, _metadataObject);

            // Return copy of origonal message with unalterd State
            request = messageBuffer.CreateMessage();

            return null;
        }

        #endregion
    }
}
