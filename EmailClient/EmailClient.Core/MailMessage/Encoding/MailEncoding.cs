using System.Collections.Generic;

namespace EmailClient.Core.MailMessage.Encoding
{
    public static class MailEncoding
    {
        private static Dictionary<string, IMailEncoder> _aliasEncoders;

        static MailEncoding()
        {
            Base64 = new Base64Encoder();
            QuotedPrintable = new QuotedPrintableEncoder();
            _aliasEncoders = new Dictionary<string, IMailEncoder>()
            {
                { QuotedPrintable.Alias, QuotedPrintable },
                { Base64.Alias, Base64 }
            };
        }

        public static IMailEncoder Base64 { get; private set; }

        public static IMailEncoder QuotedPrintable { get; private set; }
        
        public static IMailEncoder GetMailEncoder(string encoding)
        {
            return _aliasEncoders[encoding.ToUpper()];
        }
    }
}
