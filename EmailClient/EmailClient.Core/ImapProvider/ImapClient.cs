using EmailClient.Core.MailProvider;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.ImapProvider
{
    public sealed class ImapClient : MailClient
    {
        public ImapClient()
        {
        }

        public ImapClient(ImapConnection connection)
        {
            base.Connection = connection;
        }

        public new ImapConnection Connection
        {
            get { return (ImapConnection)base.Connection; }
            set { base.Connection = value; }
        }

        public override void Authenticate(MailUserInfo userInfo)
        {
            var command = Connection.CreateCommand();
            command.Command = string.Format(Imap.IMAP_LOGIN + " {0} {1}", userInfo.Email, GetPassword(userInfo.Password));
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Success); 
        }

        public override void LogOut()
        {
            MailCommand command = Connection.CreateCommand();
            command.Command = Imap.IMAP_LOGOUT;
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Debug); 
        }
    }
}
