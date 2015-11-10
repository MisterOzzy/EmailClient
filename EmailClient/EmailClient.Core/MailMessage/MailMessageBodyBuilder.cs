﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core.MailMessage
{
    public interface IMailMessageBodyBuilder
    {
        void BuildBody();

        string GetBody();
    }
}
