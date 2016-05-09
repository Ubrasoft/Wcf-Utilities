using System.ServiceModel.Channels;
using System.Xml;

namespace Ubrasoft.Utilities.Wcf.AddWseSecurityHeader
{
    /// <summary>
    /// Custom WSE security header
    /// </summary>
    internal class CustomWseSecurityHeader : MessageHeader
    {
        private readonly string _username;
        private readonly string _password;

        public CustomWseSecurityHeader(string username, string password)
        {
            _username = username;
            _password = password;
        }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer,
            MessageVersion messageVersion)
        {
            writer.WriteStartElement("UsernameToken", Namespace);
            writer.WriteElementString("Username", Namespace, _username);
            writer.WriteElementString("Password", Namespace, _password);
            writer.WriteEndElement();
        }

        public override string Name
        {
            get { return "Security"; }
        }

        public override string Namespace
        {
            get { return "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"; }
        }

        public override bool MustUnderstand
        {
            get { return true; }
        }
    }
}
