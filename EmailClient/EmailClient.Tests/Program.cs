using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using EmailClient.Core.ImapProvider;
using EmailClient.Core.MailProvider;
using EmailClient.Data;
using EmailClient.Util.Logger;
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

            MailProviderFactory emailFactory = new ImapProviderFactory();
            MailConnection connection = emailFactory.CreateConnection();
            connection.Host = config.Imap.Host;
            connection.IsSslAuthentication = config.Imap.IsSslAuthentication;
            connection.Port = config.Imap.Port;
            connection.Open();
            MailClient client = emailFactory.CreateClient();
            client.Authenticate(new MailUserInfo() { Email = "ozzy2106@gmail.com", Password = securePass });

            MailCommand command = client.Connection.CreateCommand();
            //command.Command = "STATUS INBOX (messages)";
            //command.ExecuteCommand();
            //LoggerHolders.ConsoleLogger.Log(command.Response);

            command.Command = "SELECT INBOX";
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response);

            command.Command = "FETCH " + 5320 + " (body[header.fields (from subject date)])";
            command.ExecuteCommand();
            //byte[] resBytes =  Encoding.ASCII.GetBytes(command.Response);
            //string str = Convert.ToBase64String(resBytes);


            LoggerHolders.ConsoleLogger.Log(command.Response);

            string pattern = @"\b(From: =\?UTF\-8\?B\?(?<base64Text>\w+=)\?=)";
            Regex regex = new Regex(pattern);

            //var newStr = Regex.Replace(command.Response, pattern, "${base64Text}");



            // Получаем совпадения в экземпляре класса Match
            Match match = regex.Match(command.Response);

             //отображаем все совпадения
            while (match.Success)
            {
                // Т.к. мы выделили в шаблоне одну группу (одни круглые скобки),
                // ссылаемся на найденное значение через свойство Groups класса Match
                Console.WriteLine(match.Groups["base64Text"]);

                // Переходим к следующему совпадению
                match = match.NextMatch();
            }

            //LoggerHolders.ConsoleLogger.Log(newStr, LogType.Warning);

            Match fromMatch = Regex.Match(command.Response, @"From:");
            LoggerHolders.ConsoleLogger.Log(fromMatch.Value, LogType.Warning);
            Console.ReadLine();

            string from = command.Response.Substring(command.Response.IndexOf("From: ") + "From: ".Length, 28);

            
            LoggerHolders.ConsoleLogger.Log(from);

            Console.WriteLine("!!!!!!!!!!!!!!!!!" + from.Substring("=?utf-8?B?".Length));

            string forBase64 = from.Substring("=?utf-8?B?".Length).TrimEnd('=').TrimEnd('?');
            Console.WriteLine(forBase64);
            var decodedString = DecodedString(forBase64);

            LoggerHolders.ConsoleLogger.Log(decodedString);

            command.Command = "FETCH " + 5320 + " body[text]";
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Debug);

            LoggerHolders.ConsoleLogger.Log(DecodedString("IFBSSVZFRUVFRUVFRUVFRUVFRUVFRUVFRUVU"), LogType.Debug);
            LoggerHolders.ConsoleLogger.Log(DecodedString("CjxIVE1MPjxCT0RZPlBSSVZFRUVFRUVFRUVFRUVFRUVFRUVFRUVUPC9CT0RZPjwvSFRNTD4K"), LogType.Debug);


            //StreamWriter writer = new StreamWriter("C:\\new.txt");
            //writer.WriteLine(command.Response);
            //writer.Close();

            //command.Command = "STATUS INBOX (messages)";
            //command.ExecuteCommand();
            //LoggerHolders.ConsoleLogger.Log(command.Response);

            Console.ReadLine();
        }

        private static string DecodedString(string forBase64)
        {
            byte[] data = Convert.FromBase64String(forBase64);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }

        private static string ConvertAsciiToUTF8(string inAsciiString)
        {
            // Create encoding ASCII.
            Encoding inAsciiEncoding = Encoding.ASCII;
            // Create encoding UTF8.

            Encoding outUTF8Encoding = Encoding.UTF8;

            // Convert the input string into a byte[].

            byte[] inAsciiBytes = inAsciiEncoding.GetBytes(inAsciiString);
            // Conversion string in ASCII encoding to UTF8 encoding byte array.


            byte[] outUTF8Bytes = Encoding.Convert(inAsciiEncoding, outUTF8Encoding, inAsciiBytes);

            // Convert the byte array into a char[] and then into a string.
            char[] inUTF8Chars = new char[outUTF8Encoding.GetCharCount(outUTF8Bytes, 0, outUTF8Bytes.Length)];
            outUTF8Encoding.GetChars(outUTF8Bytes, 0, outUTF8Bytes.Length, inUTF8Chars, 0);

            string outUTF8String = new string (inUTF8Chars);
            return outUTF8String;
        }
    }
}
