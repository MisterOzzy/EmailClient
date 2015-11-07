using System;
using System.Runtime.InteropServices;
using System.Security;

namespace EmailClient.Core.MailProvider
{
    public abstract class MailClient
    {
        public MailConnection Connection { get; protected set; }

        public abstract void Authenticate(MailUserInfo userInfo);

        public abstract void LogOut();

        protected string GetPassword(SecureString passSecureString)
        {
            IntPtr cvttmpst = Marshal.SecureStringToBSTR(passSecureString);
            return Marshal.PtrToStringAuto(cvttmpst);
        }
    }
}
