using EmailClient.Core.MailProvider;

namespace EmailClient.Core.ImapProvider
{
    public sealed class ImapConnection : MailConnection
    {
        public override MailCommand CreateCommand()
        {
            return new ImapCommand() { EmailStream = this.EmailStream, EmailStreamReader = this.EmailStreamReader };
        }
    }
}
