using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider.SecureConnection
{
    public class TlsMailConnectionDecorator : SecureMailConnectionDecorator
    {
        public override void Open()
        {
            base.Open();
            MailConnection.EmailStreamReader = new StreamReader(MailConnection.TcpClient.GetStream());
            MailConnection.EmailStream = MailConnection.TcpClient.GetStream();
            LoggerHolders.ConsoleLogger.Log(MailConnection.EmailStreamReader.ReadLine());
            MailCommand command = MailConnection.CreateCommand();
            command.Command = "STARTTLS";
            command.ExecuteCommand();

            LoggerHolders.ConsoleLogger.Log(command.Response);

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
        }
    }
}
