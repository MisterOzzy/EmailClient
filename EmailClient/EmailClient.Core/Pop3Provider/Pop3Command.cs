using EmailClient.Core.MailProvider;

namespace EmailClient.Core.Pop3Provider
{
    public class Pop3Command : MailCommand
    {
        protected override string InitializeCommand()
        {
            return Command + CRLF;
        }

        protected override void ReceiveResponse()
        {
            string response = string.Empty;
            if (!IsMultiLineResponse)
                response = EmailStreamReader.ReadLine();
            else
                response = ReceiveMultiLineResponse();
            Response = response;
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
