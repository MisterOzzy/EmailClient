using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage.Encoding
{
    public class Base64Encoder : IMailEncoder
    {
        public string Name
        {
            get { return "base64"; }
        }

        public string Alias
        {
            get { return "B"; }
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
