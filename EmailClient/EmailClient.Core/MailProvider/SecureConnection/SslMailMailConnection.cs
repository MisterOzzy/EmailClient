using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider.SecureConnection
{

    public class SslMailMailConnection : ISecureMailConnection
    {
        ////public override void Open()
        ////{
        ////    base.Open();
        ////    var sslStrm = new SslStream(_tcpClient.GetStream(), false);

        ////    try
        ////    {
        ////        sslStrm.AuthenticateAsClient(Host);
        ////    }
        ////    catch (AuthenticationException ex)
        ////    {
        ////        LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex);
        ////        if (ex.InnerException != null)
        ////        {
        ////            LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex.InnerException);
        ////        }
        ////        _tcpClient.Close();
        ////    }

        ////    _emailStream = sslStrm;
        ////    _emailsStreamReader = new StreamReader(sslStrm);
        ////}

        public void Open(MailConnection mailConnection)
        {
            throw new NotImplementedException();
        }
    }
}
