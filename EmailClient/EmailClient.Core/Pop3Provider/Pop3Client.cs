using System;
using EmailClient.Core.MailProvider;
using EmailClient.Util.Logger;

namespace EmailClient.Core.Pop3Provider
{
    public sealed class Pop3Client : MailClient
    {
        public Pop3Client()
        {
        }

        public Pop3Client(Pop3Connection connection)
        {
            base.Connection = connection;
        }

        public new Pop3Connection Connection
        {
            get { return (Pop3Connection)base.Connection; }
            set { base.Connection = value; }
        }

        public override void Authenticate(MailUserInfo userInfo)
        {
            var command = Connection.CreateCommand();
            command.Command = string.Format(Pop3.POP3_USER + " {0}", userInfo.Email);
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response);
            command.Command = string.Format(Pop3.POP3_PASS + " {0}", GetPassword(userInfo.Password));
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Success);
        }

        public override void LogOut()
        {
            MailCommand command = Connection.CreateCommand();
            command.Command = Pop3.POP3_QUIT;
            command.ExecuteCommand();
        }
    }
}
