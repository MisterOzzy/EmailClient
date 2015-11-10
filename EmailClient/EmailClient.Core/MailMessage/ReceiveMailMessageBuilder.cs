using System;
using System.Globalization;
using System.Text.RegularExpressions;
using EmailClient.Core.MailMessage.Encoding;
using EmailClient.Core.MailMessage.MailMessageParseUtil;

namespace EmailClient.Core.MailMessage
{
    public class ReceiveMailMessageBuilder : IMailMessageBuilder
    {
        private readonly object _lockObjMail = new object();
        private string _responseFromServer = string.Empty;
        private MailMessage _message;

        public ReceiveMailMessageBuilder(string responseFromServer)
        {
            _responseFromServer = responseFromServer;
            _message = new MailMessage();
        }

        public void BuildFrom()
        {
            string conditionPattern = @"From: =\?";
            string fromPattern = MailMessageRegExPattern.FromWithOutEncoding;
            Regex regex;
            Match match;
            bool isEncoded = false;

            if (_responseFromServer.Contains(conditionPattern))
            {
                fromPattern = MailMessageRegExPattern.FromWithEncoding;
                isEncoded = true;
            }
                    
            regex = new Regex(fromPattern);
            match = regex.Match(_responseFromServer);
            string email = match.Groups["email"].Value;
            string name = MailMessageResponseParser.Parse(match, isEncoded, "name");

            lock (_lockObjMail)
            {
                _message.From = email;
                _message.FromName = name;
            }
        }

        public void BuildSubject()
        {
            string conditionPattern = @"Subject: =\?";
            string subject = MailMessageResponseParser.Parse(_responseFromServer, conditionPattern,
                MailMessageRegExPattern.SubjectWithEncoding, MailMessageRegExPattern.SubjectWithOutEncoding, "subject");
            lock (_lockObjMail)
            {
                _message.Subject = subject;
            }
        }

        public void BuildDate()
        {
            string strDate = MailMessageResponseParser.Parse(_responseFromServer, MailMessageRegExPattern.Date, "date");
            DateTime date = DateTime.Parse(strDate, null, DateTimeStyles.AdjustToUniversal);
            lock (_lockObjMail)
            {
                _message.Date = date;
            }
        }

        public MailMessage GetMailMessage()
        {
            return _message;
        }
    }
}
