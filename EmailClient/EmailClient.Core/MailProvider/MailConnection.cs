using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider
{
    public abstract class MailConnection : IDisposable
    {
        private StreamReader _emailsStreamReader;
        private Stream _emailStream;
        private TcpClient _tcpClient;
        private MailConnectionStateType _state = MailConnectionStateType.None;

        public StreamReader EmailStreamReader
        {
            get { return _emailsStreamReader; }
        }

        public Stream EmailStream
        {
            get { return _emailStream; }
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public bool IsSslAuthentication { get; set; }

        public MailTypeProtocol TypeProtocol { get; set; }

        public void Open()
        {
            try
            {
                _tcpClient = new TcpClient(Host, Port);
            }
            catch (Exception ex)
            {
                LoggerHolders.ConsoleLogger.Log("Exception", LogType.Critical, ex);
            }

            if (IsSslAuthentication)
            {
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
            else
            {
                _emailStream = _tcpClient.GetStream();
                _emailsStreamReader = new StreamReader(_tcpClient.GetStream());
            }

            _state = MailConnectionStateType.Opened;

           var sResult = _emailsStreamReader.ReadLine();
           LoggerHolders.ConsoleLogger.Log(sResult);
        }

        public abstract MailCommand CreateCommand();
        
        public void Close()
        {
            _emailStream.Close();
            _emailsStreamReader.Close();
            _tcpClient.Close();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
