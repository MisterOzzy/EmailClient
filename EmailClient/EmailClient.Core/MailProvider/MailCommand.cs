using System.IO;
using System.Text;
using EmailClient.Tests;
using EmailClient.Util.Logger;

namespace EmailClient.Core.MailProvider
{
    public abstract class MailCommand
    {
        protected const string CRLF = "\r\n";
        private readonly object _lockResponseObject = new object();
        protected bool IsMultiLineResponse = false;
        private string _response = string.Empty;       

        public string Command { get; set; }

        public Stream EmailStream { get; set; }

        public StreamReader EmailStreamReader { get; set; }

        public string Response
        {
            get
            {
                return _response;
            }

            set
            {
                lock (_lockResponseObject)
                {
                    _response = value;
                }
            }
        }

        public void ExecuteCommand(bool isMultiLineResponse = false)
        {
            IsMultiLineResponse = isMultiLineResponse;

            lock (_lockResponseObject)
                _response = string.Empty;

            string preparedCommand = InitializeCommand();
            SendCommand(preparedCommand);
            ReceiveResponse();
        }

        protected abstract string InitializeCommand();

        protected virtual void SendCommand(string command)
        {
            LoggerHolders.ConsoleLogger.Log("Client: " + command.TrimEnd(CRLF.ToCharArray()));
            byte[] bytesCommand = Encoding.ASCII.GetBytes(command.ToCharArray());
            EmailStream.Write(bytesCommand, 0, bytesCommand.Length);
            EmailStream.Flush();
        }

        protected abstract void ReceiveResponse();
    }
}
