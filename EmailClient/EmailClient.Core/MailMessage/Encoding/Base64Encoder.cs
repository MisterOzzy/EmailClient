using System;
using System.Runtime.InteropServices;
using CharSetEncoding = System.Text.Encoding;

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
            byte[] data = Convert.FromBase64String(strToDecode);
            CharSetEncoding encoding = CharSetEncoding.GetEncoding(charset);
            return encoding.GetString(data);
        }
    }
}
