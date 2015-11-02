using EmailClient.Core.MailProvider;

namespace EmailClient.Core.Pop3Provider
{
    public sealed class Pop3Connection : MailConnection
    {
        public override MailCommand CreateCommand()
        {
            return new Pop3Command() { EmailStream = this.EmailStream, EmailStreamReader = this.EmailStreamReader };
        }
    }
}
