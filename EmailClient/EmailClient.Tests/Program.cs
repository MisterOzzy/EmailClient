using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using EmailClient.Core;
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

            EmailConnection connection = EmailConnection.Connection;
            connection.Host = config.Imap.Host;
            connection.IsSslAuthentication = config.Pop3.IsSslAuthentication;
            connection.Port = config.Pop3.Port;
            connection.TypeProtocol = EmailTypeProtocol.POP3;
            connection.Username = "ozzy2106@gmail.com";
            connection.Open();
            connection.Authenticate();
            Console.ReadLine();
        }
    }
}
