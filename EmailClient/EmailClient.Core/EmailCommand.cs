using System.IO;
using System.Text;

namespace EmailClient.Core
{
    public abstract class EmailCommand
    {
        protected const string CRLF = "\r\n";
        protected bool _isMultiLineResponse = false;

        public string Command { get; set; }
        private string _response = string.Empty;
        private object _lockResponseObject = new object();

        

        public Stream EmailStream { get; set; }

        public StreamReader EmailStreamReader { get; set; }

        public void ExecuteCommand(bool isMultiLineResponse = false)
        {
            _isMultiLineResponse = isMultiLineResponse;

            lock (_lockResponseObject)
                _response = string.Empty;

            string preparedCommand = InitializeCommand();
            SendCommand(preparedCommand);
            ReceiveResponse();
        }

        protected abstract string InitializeCommand();

        protected virtual void SendCommand(string command)
        {
            EmailConnection.PrintToTrace("Client: " + command.TrimEnd(CRLF.ToCharArray()));
            byte[] bytesCommand = Encoding.ASCII.GetBytes(command.ToCharArray());
            EmailStream.Write(bytesCommand, 0, bytesCommand.Length);
            EmailStream.Flush();
        }

        protected void SetResponse(string response)
        {
            lock (_lockResponseObject)
                _response = response;
        }

        protected abstract void ReceiveResponse();

        public string GetResponse()
        {
            return _response;
        }
    }
}
