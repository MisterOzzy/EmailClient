using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;

namespace EmailClient.Core
{
    internal delegate void TraceHandler(string str);

    public class EmailConnection : IDisposable
    {
        private static readonly TraceHandler _traceHandler = Console.WriteLine;
        private static EmailConnection _currentConnection = null;
        private StreamReader _emailsStreamReader;
        private Stream _emailStream;
        private TcpClient _tcpClient;

        protected EmailConnection()
        {
        }

        public static EmailConnection Connection
        {
            get
            {
                if (_currentConnection == null)
                    _currentConnection = new EmailConnection();
                return _currentConnection;
            }
        }


        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsSslAuthentication { get; set; }

        public EmailTypeProtocol TypeProtocol { get; set; }

        public static void PrintToTrace(string message)
        {
            _traceHandler(message);
        }


        public void Open()
        {
            try
            {
                _tcpClient = new TcpClient(Host, Port);
            }
            catch (Exception ex)
            {
                _traceHandler(string.Format("Exception: {0}", ex.Message));
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
                    _traceHandler(string.Format("Exception: {0}", ex.Message));
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


            var sResult = _emailsStreamReader.ReadLine();
            _traceHandler(sResult);
        }


        public void Authenticate()
        {
            IEmailAuthentication authentication = null;
            if(TypeProtocol == EmailTypeProtocol.None)
                throw new Exception("Please select type of email protocol");

            switch (TypeProtocol)
            {
                case EmailTypeProtocol.IMAP:
                    authentication = new ImapAuthentication();
                    break;
                case EmailTypeProtocol.POP3:
                    authentication = new Pop3Authentication();
                    break;
            }

            authentication.Authenticate(Username, Password);
        }

        public EmailCommand CreateCommand()
        {
            if (TypeProtocol == EmailTypeProtocol.None)
                throw new Exception("Please select type of email protocol");

            switch (TypeProtocol)
            {
                case EmailTypeProtocol.IMAP:
                    return new ImapCommand() { EmailStream = _emailStream, EmailStreamReader = _emailsStreamReader };
                    break;
                case EmailTypeProtocol.POP3:
                    return new Pop3Command() { EmailStream = _emailStream, EmailStreamReader = _emailsStreamReader };
                    break;
            }

            return null;
        }

        public void Logout()
        {
            string logOut = string.Empty;

            if (TypeProtocol == EmailTypeProtocol.None)
                throw new Exception("Please select type of email protocol");

            switch (TypeProtocol)
            {
                case EmailTypeProtocol.IMAP:
                    logOut = "LOGOUT";
                    break;
                case EmailTypeProtocol.POP3:
                    logOut = "QUIT";
                    break;
            }

            EmailCommand command = CreateCommand();
            command.Command = logOut;
            command.ExecuteCommand();
            _traceHandler(command.GetResponse());
        }

        public void Close()
        {
            Logout();
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
