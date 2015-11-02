using EmailClient.Core.MailProvider;

namespace EmailClient.Core.ImapProvider
{
    public sealed class ImapProviderFactory : MailProviderFactory
    {
        public override MailConnection CreateConnection()
        {
            Connection = new ImapConnection();
            return Connection;
        }

        public override MailClient CreateClient()
        {
            return new ImapClient((ImapConnection)Connection);
        }
    }
}
