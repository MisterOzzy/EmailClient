using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public abstract class EmailProviderFactory
    {
        protected EmailConnection Connection = null;

        public abstract EmailConnection CreateConnection();

        public abstract EmailClient CreateClient();
    }
}
