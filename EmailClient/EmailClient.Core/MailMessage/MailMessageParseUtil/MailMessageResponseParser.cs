using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EmailClient.Core.MailMessage.Encoding;

namespace EmailClient.Core.MailMessage.MailMessageParseUtil
{
    public static class MailMessageResponseParser
    {
        public static string Parse(string strForParsing, string checkEncodedStr, string patternEncoding,
            string patternWithOutEncoding, string strGroupNameToDecode,
            string strCharset = "charset",
            string strEncoding = "encoding")
        {
            bool isEncoded = strForParsing.Contains(checkEncodedStr);
            Regex regex = new Regex(strForParsing);
            Match match;

            if (isEncoded)
            {
                match = regex.Match(patternEncoding);
                string encoding = match.Groups[strEncoding].Value;
                string charset = match.Groups[strCharset].Value;
                string groupName = match.Groups[strGroupNameToDecode].Value;
                return MailEncoding.GetMailEncoder(encoding).Decode(groupName, charset);
            }
            match = regex.Match(patternWithOutEncoding);
            return match.Groups[strGroupNameToDecode].Value;
        }

        public static string Parse(Match match, bool isEncoded, string strGroupNameToDecode,
            string strCharset = "charset",
            string strEncoding = "encoding")
        {
            string groupName = match.Groups[strGroupNameToDecode].Value;
            if (isEncoded)
                return groupName;
            string encoding = match.Groups[strEncoding].Value;
            string charset = match.Groups[strCharset].Value;
            return MailEncoding.GetMailEncoder(encoding).Decode(groupName, charset);
        }

        public static string Parse(string strForParsing, string patternFind, string strGroupName)
        {
            Regex regex = new Regex(patternFind);
            Match match = regex.Match(strForParsing);
            return match.Groups[strGroupName].Value;
        }
    }
}
