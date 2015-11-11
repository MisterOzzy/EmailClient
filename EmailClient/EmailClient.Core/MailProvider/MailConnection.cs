﻿using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using EmailClient.Core.MailProvider.SecureConnection;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider
{
    public abstract class MailConnection : IDisposable
    {
        protected StreamReader _emailsStreamReader;
        protected Stream _emailStream;
        protected TcpClient _tcpClient;
        private MailConnectionStateType _state = MailConnectionStateType.None;
        private string _host;
        private int _port;

        public StreamReader EmailStreamReader
        {
            get { return _emailsStreamReader; }
        }

        ////public SecureMailConnection SecureTypeConnection { get; set; }

        public Stream EmailStream
        {
            get { return _emailStream; }
        }

        public string Host
        {
            get { return _host; }
            set { _host = value; }
        }

        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public bool IsSslAuthentication { get; set; }

        public MailTypeProtocol TypeProtocol { get; set; }

        public virtual void Open()
        {
            ////if (SecureTypeConnection != null)
            ////    SecureTypeConnection.Open();
            ////////if (IsSslAuthentication)
            ////{
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
            ////else
            ////{
            ////    _emailStream = _tcpClient.GetStream();
            ////    _emailsStreamReader = new StreamReader(_tcpClient.GetStream());
            ////}

            ////_state = MailConnectionStateType.Opened;

            ////var sResult = _emailsStreamReader.ReadLine();
            ////LoggerHolders.ConsoleLogger.Log(sResult);
        }

        public virtual MailCommand CreateCommand()
        {
            return null;
        }
        
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
