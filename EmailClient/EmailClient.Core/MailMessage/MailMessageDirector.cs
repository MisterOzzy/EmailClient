using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage
{
    public class MailMessageDirector
    {
        private IMailMessageBuilder _mailMessageBuilder;

        public MailMessageDirector(IMailMessageBuilder messageBuilder)
        {
            _mailMessageBuilder = messageBuilder;
        }

        public void ConstructMailMessage()
        {
            try
            {
                _mailMessageBuilder.BuildDate();                
                _mailMessageBuilder.BuildFrom();
                _mailMessageBuilder.BuildSubject();
            }
            catch(Exception ex)
            {
                string aaa = ex.ToString();
            }
        }
    }
}
