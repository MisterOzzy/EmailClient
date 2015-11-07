using EmailClient.Core.MailProvider;
using EmailClient.UI.VM.Core;

namespace EmailClient.UI.VM
{
    public class MainWindowVM : ViewModelBase
    {
        public MailClient _client;

        public MainWindowVM(MailClient client)
        {
            _client = client;
        }
    }
}
