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
            string changedPattern = isEncoded ? patternEncoding : patternWithOutEncoding;
            Regex regex = new Regex(changedPattern);
            Match match;

            if (isEncoded)
            {
                match = regex.Match(strForParsing);
                string encoding = match.Groups[strEncoding].Value;
                string charset = match.Groups[strCharset].Value;
                string groupName = match.Groups[strGroupNameToDecode].Value;

                Regex regexEnc = new Regex(MailMessageRegExPattern.EncodedString);
                MatchCollection matchCollection = regexEnc.Matches(groupName);
                string encodedData = string.Empty;
                IMailEncoder mailEncoder = MailEncoding.GetMailEncoder(encoding);
                string decodedString = string.Empty;

                foreach (Match m in matchCollection)
                {
                    encodedData += m.Groups["data"];
                    if (encodedData.EndsWith("="))
                    {
                        string decodedPart = mailEncoder.Decode(encodedData, charset);
                        decodedString += decodedPart;
                        encodedData = string.Empty;
                    }
                }

                if (!string.IsNullOrEmpty(encodedData))
                    decodedString += mailEncoder.Decode(encodedData, charset);

                return decodedString;
            }

            match = regex.Match(strForParsing);
            return match.Groups[strGroupNameToDecode].Value;
        }

        public static string Parse(Match match, bool isEncoded, string strGroupNameToDecode,
            string strCharset = "charset",
            string strEncoding = "encoding")
        {
            string matchedStr = match.Groups[strGroupNameToDecode].Value;
            if (!isEncoded)
                return matchedStr;

            string encoding = match.Groups[strEncoding].Value;
            string charset = match.Groups[strCharset].Value;
            Regex regex = new Regex(MailMessageRegExPattern.EncodedString);
            MatchCollection matchCollection = regex.Matches(matchedStr);
            string encodedData = string.Empty;
            IMailEncoder mailEncoder = MailEncoding.GetMailEncoder(encoding);
            string decodedString = string.Empty;

            foreach (Match m in matchCollection)
            {
                encodedData += m.Groups["data"];
                if (encodedData.EndsWith("="))
                {
                    string decodedPart = mailEncoder.Decode(encodedData, charset);
                    decodedString += decodedPart;
                    encodedData = string.Empty;
                }
            }

            if (!string.IsNullOrEmpty(encodedData))
                decodedString += mailEncoder.Decode(encodedData, charset);

            return decodedString;
        }

        public static string Parse(string strForParsing, string patternFind, string strGroupName)
        {
            Regex regex = new Regex(patternFind);
            Match match = regex.Match(strForParsing);
            return match.Groups[strGroupName].Value;
        }
    }
}
