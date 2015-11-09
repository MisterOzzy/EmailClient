using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage.Encoding
{
    public interface IMailEncoder
    {
        string Name { get; }

        string Alias { get; }

        string Encode(string strForEncode, string charset);

        string Decode(string strToDecode, string charset);
    }
}
