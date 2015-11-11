using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailProvider.SecureConnection
{
    public abstract class SecureMailConnectionDecorator : MailConnectionComponent
    {
        public MailConnection MailConnection { protected get; set; }

        public override void Open()
        {
            if(MailConnection != null)
                MailConnection.Open();
        }
    }
}
