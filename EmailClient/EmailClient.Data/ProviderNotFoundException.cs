using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public class ProviderNotFoundException : Exception
    {
        public override string Message
        {
            get { return "Configuration for current provider not found on server!"; }
        }
    }
}
