using System;
using EmailClient.Core.MailProvider;

namespace EmailClient.Core.Pop3Provider
{
    public sealed class Pop3ProviderFactory : MailProviderFactory
    {
        public override MailConnection CreateConnection()
        {
            Connection = new Pop3Connection();
            return Connection;
        }

        public override MailClient CreateClient()
        {
            return new Pop3Client((Pop3Connection)Connection);
        }
    }
}
