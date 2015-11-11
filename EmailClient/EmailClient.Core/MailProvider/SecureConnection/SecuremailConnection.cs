using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider.SecureConnection
{
    public class SecureMailConnection : MailConnection
    {
        public override void Open()
        {

            try
            {
                _tcpClient = new TcpClient(Host, Port);
            }
            catch (Exception ex)
            {
                LoggerHolders.ConsoleLogger.Log("Exception:", LogType.Critical, ex);
            }
        }
    }
}
