using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public class ImapCommand : EmailCommand
    {
        private static int IMAP_COMMAND_COUNTER = 0;

        private static string IMAP_PREFIX
        {
            get { return "IMAP00" + IMAP_COMMAND_COUNTER.ToString() + " "; }
        }

        protected override string InitializeCommand()
        {
            IMAP_COMMAND_COUNTER++;
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

            SetResponse(response);
        }
    }
}
