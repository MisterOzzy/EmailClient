using System;
using System.Runtime.InteropServices;
using System.Security;

namespace EmailClient.Core.MailProvider
{
    public abstract class MailClient
    {
        protected MailConnection Connection { get; set; }

        public abstract void Authenticate(MailUserInfo userInfo);

        public abstract void LogOut();

        protected string GetPassword(SecureString passSecureString)
        {
            IntPtr cvttmpst = Marshal.SecureStringToBSTR(passSecureString);
            return Marshal.PtrToStringAuto(cvttmpst);
        }
    }
}
