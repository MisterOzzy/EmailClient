using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(charset);
            StringReader strReader = new StringReader(strToDecode);
            string preparedStr = string.Empty;
            string cursor = string.Empty;
            StringBuilder builder = new StringBuilder();

            while ((cursor = strReader.ReadLine()) != null)
                builder.Append(cursor.TrimEnd('='));

            preparedStr = builder.ToString();

            var i = 0;
            var output = new List<byte>();
            while (i < preparedStr.Length)
            {
                if (preparedStr[i] == '=' && preparedStr[i + 1] == '\r' && preparedStr[i + 2] == '\n')
                {
                    //Skip
                    i += 3;
                }
                else if (preparedStr[i] == '=')
                {
                    string sHex = preparedStr;
                    sHex = sHex.Substring(i + 1, 2);
                    int hex = Convert.ToInt32(sHex, 16);
                    byte b = Convert.ToByte(hex);
                    output.Add(b);
                    i += 3;
                }
                else
                {
                    output.Add((byte)preparedStr[i]);
                    i++;
                }
            }
            return System.Text.Encoding.GetEncoding(charset).GetString(output.ToArray());
        }
    }
}
