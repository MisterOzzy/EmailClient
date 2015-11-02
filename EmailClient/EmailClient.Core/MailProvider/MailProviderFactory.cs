namespace EmailClient.Core.MailProvider
{
    public abstract class MailProviderFactory
    {
        protected MailConnection Connection = null;

        public abstract MailConnection CreateConnection();

        public abstract MailClient CreateClient();
    }
}
