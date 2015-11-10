using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage
{
    public class MailMessage
    {
        public int SequenceNumber { get; set; }

        public int UID { get; set; }

        public string From { get; set; }

        public string FromName { get; set; }

        public string Subject { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateLocal
        {
            get { return TimeZone.CurrentTimeZone.ToLocalTime(Date); }
        }

        public Dictionary<MailMessageBodyType, MailMessageBody> Body { get; set; }
    }
}
