using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public class EmailProvider
    {
        public string Name { get; set; }

        public EmailConfiguration Smtp { get; set; }

        public EmailConfiguration Pop3 { get; set; }

        public EmailConfiguration Imap { get; set; }
    }
}
