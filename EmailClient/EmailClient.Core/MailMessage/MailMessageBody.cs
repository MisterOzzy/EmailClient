using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage
{
    public class MailMessageBody
    {
        public string Body { get; set; }

        public MailMessageBodyType BodyType { get; set; }

        public List<MailAttachment> MailAttachments { get; set; } 
    }
}
