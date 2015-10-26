using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public class NotImplementedOneIncomingProtocolException : Exception
    {
        public override string Message
        {
            get { return "Add reference to one protocol of incoming email"; }
        }
    }
}
