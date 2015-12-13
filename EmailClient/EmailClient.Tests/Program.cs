﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using EmailClient.Core.ImapProvider;
using EmailClient.Core.MailProvider;
using EmailClient.Core.Pop3Provider;
using EmailClient.Data;
using EmailClient.Core.MailMessage;
using EmailClient.Core.MailProvider.SecureConnection;
using EmailClient.Util.Logger;
using Newtonsoft.Json;

namespace EmailClient.Tests
{

    class Program
    {
        
        static void Main(string[] args)
        {
            //string result = "";
            //string url = @"http://localhost:3627/EmailConfigurationService.svc/EmailProvider";
            //EmailProvider config = null; 

            // Создаём объект WebClient
            //using (var webClient = new WebClient())
            //{
            //    webClient.QueryString.Add("provider","gmail.com");
            //    //Console.WriteLine(webClient.QueryString);
                
            //    // Выполняем запрос по адресу и получаем ответ в виде строки
            //    var response = webClient.DownloadString(url);
            //    config = JsonConvert.DeserializeObject<EmailProvider>(response);
            //}
            //Get password
            //StreamReader reader = new StreamReader("Test.txt");
            ////reader.ReadLine();
            ////char[] passChars = pass.ToCharArray();

            //SecureString securePass = new SecureString();
            ////reader.ReadLine().ToCharArray().ToList().ForEach(securePass.AppendChar);
            //"Qwerty1234".ToCharArray().ToList().ForEach(securePass.AppendChar);
            //IntPtr cvttmpst = Marshal.SecureStringToBSTR(securePass);



            //MailProviderFactory emailFactory = new ImapProviderFactory();
            //MailConnection connection = emailFactory.CreateConnection();
            ////connection.Host = config.Imap.Host;
            ////connection.IsSslAuthentication = config.Imap.IsSslAuthentication;
            ////connection.Port = config.Imap.Port;
            ////connection.SecureTypeConnection = new SslMailConnection();
            //connection.Host = "imap.mail.ru";
            //connection.IsSslAuthentication = true;
            //connection.Port = 143;
            //SecureMailConnectionDecorator sslMailConnection = new TlsMailConnectionDecorator();
            //sslMailConnection.MailConnection = connection;
            //sslMailConnection.Open();
            //MailClient client = emailFactory.CreateClient();
            //client.Authenticate(new MailUserInfo() { Email = "ozzytestmail@mail.ru", Password = securePass });

            //connection.Host = "imap.mail.ru";
            //connection.IsSslAuthentication = true;
            //connection.Port = 143;
            //connection.Open();
            //MailClient client = emailFactory.CreateClient();
            //MailCommand com = client.Connection.CreateCommand();
            //com.Command = "STARTTLS";
            //com.ExecuteCommand();

            //var ssl = client.Connection._tcpClient.GetStream();
            //client.Connection._emailStream = ssl;
            //client.Connection._emailsStreamReader = new StreamReader(ssl);
            //Console.WriteLine(client.Connection._emailsStreamReader.ReadLine());



            //Thread.Sleep(5000);

            //com.Command = "CAPABILITY";
            //com.ExecuteCommand();

            

            //LoggerHolders.ConsoleLogger.Log(com.Response, LogType.Debug);


            //client.Authenticate(new MailUserInfo() { Email = "ozzyTestMail@mail.ru", Password = securePass });
            //com.Command = "AUTH PLAIN";
            //com.ExecuteCommand();

            //////MailCommand command = client.Connection.CreateCommand();
            ////////command.Command = "STATUS INBOX (messages)";
            ////////command.ExecuteCommand();
            ////////LoggerHolders.ConsoleLogger.Log(command.Response);

            //////command.Command = "SELECT INBOX";
            //////command.ExecuteCommand();
            //////LoggerHolders.ConsoleLogger.Log(command.Response);

            //////command.Command = "FETCH " + 5343 + " (body[header.fields (from subject date)])";
            //////command.ExecuteCommand();
            //////LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Debug);
            /// 
            /// 
            /// 




            //StreamReader readerHeader = new StreamReader(@"C:\header.txt");

            //string header = readerHeader.ReadToEnd();
            //readerHeader.Close();
            //LoggerHolders.ConsoleLogger.Log(header, LogType.Debug);

            //string pat =
            //    @"From: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q))\?(?<name>.*)\?=(?s:.)*\<(?<email>[A-Za-z0-9@.]+)\>";

            //Regex regex = new Regex(pat);
            //Match match = regex.Match(header);

            //Console.WriteLine(match.Groups["email"].Value);

            //IMailMessageBuilder builder = new ReceiveMailMessageBuilder(header);
            //MailMessageDirector director = new MailMessageDirector(builder);
            //director.ConstructMailMessage();
            //MailMessage messageHeader = builder.GetMailMessage();
            //LoggerHolders.ConsoleLogger.Log(string.Format("From: {0} <{1}>\n Subject: {2}\n Date: {3}\n{4}",
            //    messageHeader.FromName, messageHeader.From, messageHeader.Subject, messageHeader.Date,
            //    messageHeader.DateLocal));

            Console.ReadLine();
        }
    }
}
