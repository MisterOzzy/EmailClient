using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EmailClient.Core.ImapProvider;
using EmailClient.Core.MailProvider;
using EmailClient.Core.Pop3Provider;
using EmailClient.Data;
using EmailClient.UI.View;
using EmailClient.UI.VM.Core;
using Newtonsoft.Json;

namespace EmailClient.UI.VM
{
    public class LoginWindowVM : ViewModelBase
    {
        private string _login = string.Empty;
        private SecureString _securePassword = new SecureString();
        private Visibility _gridSettingsVisibility = Visibility.Hidden;
        private Visibility _stackButtonsVisibility = Visibility.Hidden;
        private ObservableCollection<EmailConfiguration> _emailConfigurations = new ObservableCollection<EmailConfiguration>();
        private EmailConfiguration _selectedReceiveConfiguration = new EmailConfiguration();
        private EmailConfiguration _smtpConfiguration = new EmailConfiguration();

        #region Properties
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public SecureString SecurePassword
        {
            get { return _securePassword; }

            set
            {
                _securePassword = value;
                OnPropertyChanged("SecurePassword");
            }
        }

        public Visibility GridSettingsVisibility
        {
            get { return _gridSettingsVisibility; }
            set
            {
                _gridSettingsVisibility = value;
                OnPropertyChanged("GridSettingsVisibility");
            }
        }

        public Visibility StackButtonsVisibility
        {
            get { return _stackButtonsVisibility; }
            set
            {
                _stackButtonsVisibility = value;
                OnPropertyChanged("StackButtonsVisibility");
            }
        }

        public ObservableCollection<EmailConfiguration> EmailConfigurations
        {
            get { return _emailConfigurations; }
            set
            {
                _emailConfigurations = value;
                OnPropertyChanged("EmailConfigurations");
            }
        }

        public EmailConfiguration SelectedReceiveConfiguration
        {
            get { return _selectedReceiveConfiguration; }
            set
            {
                _selectedReceiveConfiguration = value;
                OnPropertyChanged("SelectedReceiveConfiguration");
            }
        }

        public EmailConfiguration SmtpConfiguration
        {
            get { return _smtpConfiguration; }
            set
            {
                _smtpConfiguration = value;
                OnPropertyChanged("SmtpConfiguration");
            }
        }
#endregion

        public ICommand LoginClick
        {
            get { return Commands.GetOrCreateCommand(() => LoginClick, LoginCommand, CanExecuteLoginCommand); }
        }

        public ICommand OkClick
        {
            get { return Commands.GetOrCreateCommand(() => OkClick, OkCommand, CanExecuteOkCommand); }
        }

        public void LoginCommand()
        {
            GetConfigurations("gmail.com");
            GridSettingsVisibility = Visibility.Visible;
            StackButtonsVisibility = Visibility.Visible;
        }

        public void OkCommand(object ownerWindow)
        {
            MailProviderFactory emailFactory = null;

            switch (_selectedReceiveConfiguration.Protocol)
            {
                case EmailProtocolType.Imap:
                    emailFactory = new ImapProviderFactory();
                    break;
                case EmailProtocolType.Pop3:
                    emailFactory = new Pop3ProviderFactory();
                    break;
                default:
                    break;
            }
            
            MailConnection connection = emailFactory.CreateConnection();
            connection.Host = _selectedReceiveConfiguration.Host;
            connection.IsSslAuthentication = _selectedReceiveConfiguration.IsSslAuthentication;
            connection.Port = _selectedReceiveConfiguration.Port;
            connection.Open();
            MailClient client = emailFactory.CreateClient();
            client.Authenticate(new MailUserInfo() { Email = _login, Password = _securePassword });

            new MainWindow() { DataContext = new MainWindowVM(client) }.Show();
            ((Window)ownerWindow).Close();
        }

        private void GetConfigurations(string provider)
        {
            string url = @"http://localhost:3627/EmailConfigurationService.svc/EmailProvider";
            EmailProvider config = null;

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("provider", provider);
                Console.WriteLine(webClient.QueryString);

                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                config = JsonConvert.DeserializeObject<EmailProvider>(response);
            }
            _emailConfigurations.Add(config.Pop3);
            _emailConfigurations.Add(config.Imap);

            SmtpConfiguration = config.Smtp;
        }

        public bool CanExecuteLoginCommand()
        {
            return GridSettingsVisibility == Visibility.Hidden;
        }

        public bool CanExecuteOkCommand(object obj)
        {
            return GridSettingsVisibility == Visibility.Visible;
        }

        protected string GetPassword(SecureString passSecureString)
        {
            IntPtr cvttmpst = Marshal.SecureStringToBSTR(passSecureString);
            return Marshal.PtrToStringAuto(cvttmpst);
        }
    }
}
