using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public class EmailUserInfo
    {
        public string Email { get; set; }

        public SecureString Password { get; set; }
    }
}
