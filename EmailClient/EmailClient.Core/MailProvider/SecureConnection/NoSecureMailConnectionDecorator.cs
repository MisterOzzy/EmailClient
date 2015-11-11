using System.IO;
using EmailClient.Tests;

namespace EmailClient.Core.MailProvider.SecureConnection
{
    public class NoSecureMailConnectionDecorator : SecureMailConnectionDecorator
    {
        public override void Open()
        {
            base.Open();
            MailConnection.EmailStream = _tcpClient.GetStream();
            MailConnection.EmailStreamReader = new StreamReader(_tcpClient.GetStream());
            LoggerHolders.ConsoleLogger.Log(MailConnection.EmailStreamReader.ReadLine());
        }
    }
}
