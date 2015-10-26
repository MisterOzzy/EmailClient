using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Data
{
    public enum EmailProtocolType : byte
    {
        None = 0,
        Imap = 1,
        Pop3 = 2,
        Smtp = 3
    }
}
