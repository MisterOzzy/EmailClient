using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider.SecureConnection
{
    public class SslMailConnectionDecorator : SecureMailConnectionDecorator
    {
        public override void Open()
        {
            base.Open();
            var sslStrm = new SslStream(MailConnection.TcpClient.GetStream(), false);

            try
            {
                sslStrm.AuthenticateAsClient(MailConnection.Host);
            }
            catch (AuthenticationException ex)
            {
                LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex);
                if (ex.InnerException != null)
                {
                    LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex.InnerException);
                }
                MailConnection.TcpClient.Close();
            }

            MailConnection.EmailStream = sslStrm;
            MailConnection.EmailStreamReader = new StreamReader(sslStrm);
            LoggerHolders.ConsoleLogger.Log(MailConnection.EmailStreamReader.ReadLine());
        }
    }
}
