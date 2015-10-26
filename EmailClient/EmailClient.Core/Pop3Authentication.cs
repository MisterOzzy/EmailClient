using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public class Pop3Authentication : IEmailAuthentication
    {
        public void Authenticate(string username, string password)
        {
            var connection = EmailConnection.Connection;
            var command = connection.CreateCommand();
            command.Command = string.Format("USER {0}", username);
            command.ExecuteCommand();
            EmailConnection.PrintToTrace(command.GetResponse());
            command.Command = string.Format("PASS {0}", password);
            command.ExecuteCommand();
            EmailConnection.PrintToTrace(command.GetResponse());
        }
    }
}
