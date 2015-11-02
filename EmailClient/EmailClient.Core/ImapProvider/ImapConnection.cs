using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.ImapProvider
{
    public sealed class ImapConnection : EmailConnection
    {
        public override EmailCommand CreateCommand()
        {
            return new ImapCommand() { EmailStream = this.EmailStream, EmailStreamReader = this.EmailStreamReader };
        }
    }
}
