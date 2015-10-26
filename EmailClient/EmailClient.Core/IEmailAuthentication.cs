namespace EmailClient.Core
{
    public interface IEmailAuthentication
    {
        void Authenticate(string username, string password);
    }
}
