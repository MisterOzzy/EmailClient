using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.ImapProvider
{
    public sealed class ImapClient : EmailClient
    {
        public ImapClient()
        {
        }

        public ImapClient(ImapConnection connection)
        {
            Connection = connection;
        }

        public ImapConnection Connection { get; set; }

        public override void Authenticate(EmailUserInfo userInfo)
        {
            var command = Connection.CreateCommand();
            command.Command = string.Format(Imap.IMAP_LOGIN + " {0} {1}", userInfo.Email, GetPassword(userInfo.Password));
            command.ExecuteCommand();
            ////EmailConnection.PrintToTrace(command.GetResponse());
            Console.WriteLine(command.GetResponse());
        }

        public override void LogOut()
        {
            EmailCommand command = Connection.CreateCommand();
            command.Command = Imap.IMAP_LOGOUT;
            command.ExecuteCommand();
        }
    }
}
