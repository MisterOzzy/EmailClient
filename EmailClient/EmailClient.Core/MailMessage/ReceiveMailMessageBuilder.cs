using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage
{
    public class ReceiveMailMessageBuilder : IMailMessageBuilder
    {
        private string _responseFromServer = string.Empty;
        private MailMessage _message;

        public ReceiveMailMessageBuilder(string responseFromServer)
        {
            _responseFromServer = responseFromServer;
            _message = new MailMessage();
        }

        public void BuildFrom()
        {
            throw new NotImplementedException();
        }

        public void BuildSubject()
        {
            throw new NotImplementedException();
        }

        public void BuildDate()
        {
            throw new NotImplementedException();
        }

        public MailMessage GetMailMessage()
        {
            throw new NotImplementedException();
        }
    }
}
