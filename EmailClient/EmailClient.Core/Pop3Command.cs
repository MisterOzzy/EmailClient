using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Core
{
    public class Pop3Command : EmailCommand
    {
        protected override string InitializeCommand()
        {
            return Command + CRLF;
        }

        protected override void ReceiveResponse()
        {
            string response = string.Empty;
            if (!_isMultiLineResponse)
                response = EmailStreamReader.ReadLine();
            else
                response = ReceiveMultiLineResponse();
            SetResponse(response);
        }

        private string ReceiveMultiLineResponse()
        {
            string cursor = EmailStreamReader.ReadLine();
            string response = string.Empty;
            while (cursor != ".")
            {
                response += cursor + '\n';
                cursor = EmailStreamReader.ReadLine();
            }
            return response;
        }
    }
}
