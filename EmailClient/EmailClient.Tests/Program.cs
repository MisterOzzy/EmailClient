using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Core;
using EmailClient.Core.ImapProvider;
using EmailClient.Data;
using Newtonsoft.Json;

namespace EmailClient.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            string result = "";
            string url = @"http://localhost:3627/EmailConfigurationService.svc/EmailProvider";
            EmailProvider config = null; 

            // Создаём объект WebClient
            using (var webClient = new WebClient())
            {
                webClient.QueryString.Add("provider","gmail.com");
                Console.WriteLine(webClient.QueryString);
                
                // Выполняем запрос по адресу и получаем ответ в виде строки
                var response = webClient.DownloadString(url);
                config = JsonConvert.DeserializeObject<EmailProvider>(response);
            }
            //Get password
            StreamReader reader = new StreamReader("Test.txt");
            //reader.ReadLine();
            //char[] passChars = pass.ToCharArray();

            SecureString securePass = new SecureString();
            reader.ReadLine().ToCharArray().ToList().ForEach(securePass.AppendChar);
            IntPtr cvttmpst = Marshal.SecureStringToBSTR(securePass);
            //convert to string using Marshal
            

            //EmailConnection connection = EmailConnection.Connection;
            //connection.Host = config.Pop3.Host;
            //connection.IsSslAuthentication = config.Pop3.IsSslAuthentication;
            //connection.Port = config.Pop3.Port;
            //connection.TypeProtocol = EmailTypeProtocol.POP3;
            //connection.Username = "ozzy2106@gmail.com";
            //connection.Password = Marshal.PtrToStringAuto(cvttmpst);
            //connection.Open();
            //connection.Authenticate();
            //connection.Host = "imap.mail.ru";
            //connection.IsSslAuthentication = false;
            //connection.Port = 143;
            //connection.TypeProtocol = EmailTypeProtocol.IMAP;
            //connection.Username = "ozzy.mister@mail.ru";
            //connection.Open();
            //connection.Authenticate();

            EmailProviderFactory emailFactory = new ImapProviderFactory();
            EmailConnection connection = emailFactory.CreateConnection();
            connection.Host = config.Imap.Host;
            connection.IsSslAuthentication = config.Imap.IsSslAuthentication;
            connection.Port = config.Imap.Port;
            connection.Open();
            EmailClient.Core.EmailClient client = emailFactory.CreateClient();
            client.Authenticate(new EmailUserInfo(){Email = "ozzy2106@gmail.com", Password = securePass});

            Console.ReadLine();
        }
    }
}
