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
    public class TlsMailMailConnection : SecureMailConnection
    {
        public override void Open()
        {
            base.Open();
            MailCommand command = CreateCommand();
            command.Command = "STARTTLS";
            command.ExecuteCommand();
            _emailsStreamReader = new StreamReader(_tcpClient.GetStream());
            LoggerHolders.ConsoleLogger.Log(_emailsStreamReader.ReadLine());

            var sslStrm = new SslStream(_tcpClient.GetStream(), false);

            try
            {
                sslStrm.AuthenticateAsClient(Host);
            }
            catch (AuthenticationException ex)
            {
                LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex);
                if (ex.InnerException != null)
                {
                    LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex.InnerException);
                }
                _tcpClient.Close();
            }

            _emailStream = sslStrm;
            _emailsStreamReader = new StreamReader(sslStrm);
        }
    }
}
