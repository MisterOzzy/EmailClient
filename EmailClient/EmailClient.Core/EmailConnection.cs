using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;

namespace EmailClient.Core
{
    public abstract class EmailConnection : IDisposable
    {
        private StreamReader _emailsStreamReader;
        private Stream _emailStream;
        private TcpClient _tcpClient;
        private EmailConnectionStateType _state = EmailConnectionStateType.None;

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

        public EmailTypeProtocol TypeProtocol { get; set; }

        //public static void PrintToTrace(string message)
        //{
        //    _traceHandler(message);
        //}


        public void Open()
        {
            try
            {
                _tcpClient = new TcpClient(Host, Port);
            }
            catch (Exception ex)
            {
                //_traceHandler(string.Format("Exception: {0}", ex.Message));
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
                    //_traceHandler(string.Format("Exception: {0}", ex.Message));
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner exception: {0}", ex.InnerException.Message);
                    }
                    Console.WriteLine("Authentication failed - closing the connection.");
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

            _state = EmailConnectionStateType.Opened;

           var sResult = _emailsStreamReader.ReadLine();
            ////_traceHandler(sResult);
            Console.WriteLine(sResult);
        }


        ////public void Authenticate()
        ////{
        ////    IEmailAuthentication authentication = null;
        ////    if(TypeProtocol == EmailTypeProtocol.None)
        ////        throw new Exception("Please select type of email protocol");

        ////    switch (TypeProtocol)
        ////    {
        ////        case EmailTypeProtocol.IMAP:
        ////            authentication = new ImapAuthentication();
        ////            break;
        ////        case EmailTypeProtocol.POP3:
        ////            authentication = new Pop3Authentication();
        ////            break;
        ////    }

        ////    authentication.Authenticate(Username, Password);
        ////}

        public abstract EmailCommand CreateCommand();
        ////{
        ////    if (TypeProtocol == EmailTypeProtocol.None)
        ////        throw new Exception("Please select type of email protocol");

        ////    switch (TypeProtocol)
        ////    {
        ////        case EmailTypeProtocol.IMAP:
        ////            return new ImapCommand() { EmailStream = _emailStream, EmailStreamReader = _emailsStreamReader };
        ////            break;
        ////        case EmailTypeProtocol.POP3:
        ////            return new Pop3Command() { EmailStream = _emailStream, EmailStreamReader = _emailsStreamReader };
        ////            break;
        ////    }

        ////    return null;
        ////}

        ////public void Logout()
        ////{
        ////    string logOut = string.Empty;

        ////    if (TypeProtocol == EmailTypeProtocol.None)
        ////        throw new Exception("Please select type of email protocol");

        ////    switch (TypeProtocol)
        ////    {
        ////        case EmailTypeProtocol.IMAP:
        ////            logOut = "LOGOUT";
        ////            break;
        ////        case EmailTypeProtocol.POP3:
        ////            logOut = "QUIT";
        ////            break;
        ////    }

        ////    EmailCommand command = CreateCommand();
        ////    command.Command = logOut;
        ////    command.ExecuteCommand();
        ////    _state = EmailConnectionStateType.Closed;
        ////    ////_traceHandler(command.GetResponse());
        ////}

        public void Close()
        {
            ////Logout();
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
