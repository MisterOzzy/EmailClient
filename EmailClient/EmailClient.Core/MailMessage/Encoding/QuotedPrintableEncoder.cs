using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage.Encoding
{
    public class QuotedPrintableEncoder : IMailEncoder
    {
        public string Name
        {
            get { return "quoted-printable"; }
        }

        public string Alias
        {
            get { return "Q"; }
        }

        public string Encode(string strForEncode, string charset)
        {
            throw new NotImplementedException();
        }

        public string Decode(string strToDecode, string charset)
        {
            throw new NotImplementedException();
        }
    }
}
