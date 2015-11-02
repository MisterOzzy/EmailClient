using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.ImapProvider
{
    public sealed class ImapProviderFactory : EmailProviderFactory
    {
        public override EmailConnection CreateConnection()
        {
            Connection = new ImapConnection();
            return Connection;
        }

        public override EmailClient CreateClient()
        {
            return new ImapClient((ImapConnection)this.Connection);
        }
    }
}
