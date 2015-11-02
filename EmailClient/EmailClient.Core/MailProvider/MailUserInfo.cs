using System.Security;

namespace EmailClient.Core.MailProvider
{
    public class MailUserInfo
    {
        public string Email { get; set; }

        public SecureString Password { get; set; }
    }
}
