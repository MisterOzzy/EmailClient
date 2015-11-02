using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public class ImapAuthentication : IEmailAuthentication
    {
        public void Authenticate(string username, string password)
        {
            //var connection = EmailConnection.Connection;
            //var command = connection.CreateCommand();
            //command.Command = string.Format("LOGIN {0} {1}", username, password);
            //command.ExecuteCommand();
            //////EmailConnection.PrintToTrace(command.GetResponse());
            //Console.WriteLine(command.GetResponse());
        }
    }
}
