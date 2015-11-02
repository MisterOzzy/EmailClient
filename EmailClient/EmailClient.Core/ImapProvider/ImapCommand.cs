using System;
using EmailClient.Core.MailProvider;

namespace EmailClient.Core.ImapProvider
{
    public class ImapCommand : MailCommand
    {
        private Guid IMAP_CURRENT_GUID;

        private string IMAP_PREFIX
        {
            get { return "IMAP" + IMAP_CURRENT_GUID.ToString().Substring(23) + " "; }
        }

        protected override string InitializeCommand()
        {
            IMAP_CURRENT_GUID = Guid.NewGuid();
            return IMAP_PREFIX + Command + CRLF;
        }

        protected override void ReceiveResponse()
        {
            bool isEnd = false;
            string response = string.Empty;
            string cursor = string.Empty;

            while (!isEnd)
            {
                cursor = EmailStreamReader.ReadLine();
                response += cursor + "\n";
                if (cursor.StartsWith(IMAP_PREFIX + "OK") || cursor.StartsWith(IMAP_PREFIX + "NO") ||
                    cursor.StartsWith(IMAP_PREFIX + "BAD"))
                    isEnd = true;
            }

            Response = response;
        }
    }
}
