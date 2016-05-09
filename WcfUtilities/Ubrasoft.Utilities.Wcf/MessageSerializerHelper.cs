using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Ubrasoft.Utilities.Wcf
{
    public static class MessageSerializerHelper
    {
        static XmlWriterSettings xmlWriterSetings = new XmlWriterSettings() { Indent = true, ConformanceLevel = ConformanceLevel.Fragment };

        public static string LogMessage(this Message msg)
        {
            //Setup StringWriter to use as input for our StreamWriter
            //This is needed in order to capture the body of the message, because the body is streamed.
            StringWriter stringWriter = new StringWriter();

            var xmlTextWriter = XmlTextWriter.Create(stringWriter, xmlWriterSetings);
            msg.WriteMessage(xmlTextWriter);
            xmlTextWriter.Flush();
            xmlTextWriter.Close();

            var rawMessage = stringWriter.ToString();
            stringWriter.Dispose();

            return rawMessage;
        }
    }
}
