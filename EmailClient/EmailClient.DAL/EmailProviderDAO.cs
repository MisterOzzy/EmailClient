using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Data;

namespace EmailClient.DAL
{
    public class EmailProviderDAO
    {
        private EmailConfigurationManager _configuration = EmailConfigurationManager.Manager;

        public EmailProvider this[string provider]
        {
            get
            {
                EmailProvider emailProvider = null;
                if(!_configuration.ContainKey(provider))
                    return null;
                try
                {
                    emailProvider = _configuration[provider];
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return emailProvider;
            }

            set
            {
                _configuration[provider] = value;
            }
        }
    }
}
