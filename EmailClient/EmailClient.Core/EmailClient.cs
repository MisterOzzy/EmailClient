using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public abstract class EmailClient
    {
        public abstract void Authenticate(EmailUserInfo userInfo);

        public abstract void LogOut();

        protected string GetPassword(SecureString passSecureString)
        {
            IntPtr cvttmpst = Marshal.SecureStringToBSTR(passSecureString);
            return Marshal.PtrToStringAuto(cvttmpst);
        }
    }
}
