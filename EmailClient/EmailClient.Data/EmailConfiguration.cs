using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public class EmailConfiguration
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public bool IsSslAuthentication { get; set; }

        public EmailProtocolType Protocol { get; set; }
    }
}
