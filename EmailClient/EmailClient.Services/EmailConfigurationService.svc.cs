using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using EmailClient.Data;
using EmailClient.DAL;
using EmailClient.Logic;
using EmailClient.Services.Contracts;
using Newtonsoft.Json;

namespace EmailClient.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class EmailConfigurationService : IEmailConfigurationContract
    {
        public EmailProviderLogic _providerLogic = new EmailProviderLogic();
        public EmailProvider GetEmailConfigurationByProvider(string provider)
        { 
            return _providerLogic[provider];
        }

        public void AddNewProvider(EmailProvider provider) 
        {
            _providerLogic[provider.Name] = provider;
        }
    }
}
