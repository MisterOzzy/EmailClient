using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailProvider
{
    public abstract class MailConnectionComponent
    {
        protected TcpClient _tcpClient;
        protected string _host;
        protected int _port;
        protected Stream _emailStream;
        protected StreamReader _emailStreamReader;

        public abstract void Open();

        public virtual MailCommand CreateCommand()
        {
            return null;
        }
    }
}
