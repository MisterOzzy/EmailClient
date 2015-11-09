using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage.Encoding
{
    public static class MailEncoding
    {
        public static IMailEncoder Base64;
        public static IMailEncoder QuotedPrintable;

        static MailEncoding()
        {
            Base64 = new Base64Encoder();
            QuotedPrintable = new QuotedPrintableEncoder();
        }
    }
}
