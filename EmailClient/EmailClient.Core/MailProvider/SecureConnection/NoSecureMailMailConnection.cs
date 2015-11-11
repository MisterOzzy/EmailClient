using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailProvider.SecureConnection
{
    public class NoSecureMailMailConnection : SecureMailConnection
    {
        public override void Open()
        {
            base.Open();
            _emailStream = _tcpClient.GetStream();
            _emailsStreamReader = new StreamReader(_tcpClient.GetStream());
        }
    }
}
