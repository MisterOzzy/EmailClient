using System;
using EmailClient.DAL;
using EmailClient.Data;

namespace EmailClient.Logic
{
    public class EmailProviderLogic
    {
        private EmailProviderDAO _emailProviderDao = new EmailProviderDAO();

        public EmailProvider this[string provider]
        {
            get
            {
                return _emailProviderDao[provider];
            }

            set
            {
                if(value.Pop3 == null && value.Smtp == null)
                    throw new NotImplementedOneIncomingProtocolException();
                try
                {
                    _emailProviderDao[provider] = value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
