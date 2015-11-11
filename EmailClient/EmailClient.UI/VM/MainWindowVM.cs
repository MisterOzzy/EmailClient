using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using EmailClient.Core.ImapProvider;
using EmailClient.Core.MailMessage;
using EmailClient.Core.MailProvider;
using EmailClient.Tests;
using EmailClient.UI.VM.Core;
using EmailClient.Util.Logger;

namespace EmailClient.UI.VM
{
    public class MainWindowVM : ViewModelBase
    {
        private MailClient _client;
        private ObservableCollection<MailMessage> _mailMessages = new ObservableCollection<MailMessage>(); 


        public MainWindowVM(MailClient client)
        {
            _client = client;
            Task.Run(() => LoadFirs100Messages());
        }

        public ObservableCollection<MailMessage> MailMessages
        {
            get { return _mailMessages; }
            set
            {
                _mailMessages = value;
                OnPropertyChanged("MailMessages");
            }
        }

        private void LoadFirs100Messages()
        {
            MailCommand command = _client.Connection.CreateCommand();
            command.Command = "STATUS INBOX (messages)";
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response);
            string res = command.Response;
            Match m = Regex.Match(res, "[0-9]*[0-9]");
            int count = Convert.ToInt32(m.ToString());


            command.Command = "SELECT INBOX";
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response);

            for (int i = count; i > count - 100; i--)
            {
                command.Command = "FETCH " + i + " (body[header.fields (from subject date)])";
                command.ExecuteCommand();
                IMailMessageBuilder builder = new ReceiveMailMessageBuilder(command.Response);
                MailMessageDirector director = new MailMessageDirector(builder);
                director.ConstructMailMessage();
                try
                {
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        _mailMessages.Add(builder.GetMailMessage());
                    });
                }
                catch (Exception ex)
                {
                    LoggerHolders.ConsoleLogger.Log("Exception:", LogType.Error, ex);
                }                      
            }

           


            ////IMailMessageBuilder builder = new ReceiveMailMessageBuilder(header);
            ////MailMessageDirector director = new MailMessageDirector(builder);
            ////director.ConstructMailMessage();
            ////MailMessage messageHeader = builder.GetMailMessage();
            //LoggerHolders.ConsoleLogger.Log(string.Format("From: {0} <{1}>\n Subject: {2}\n Date: {3}\n{4}",
            //    messageHeader.FromName, messageHeader.From, messageHeader.Subject, messageHeader.Date,
            //    messageHeader.DateLocal));
        }
    }
}
