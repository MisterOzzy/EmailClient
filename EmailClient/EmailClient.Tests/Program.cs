using System;
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
            StreamReader reader = new StreamReader("Test.txt");
            //reader.ReadLine();
            //char[] passChars = pass.ToCharArray();

            SecureString securePass = new SecureString();
            //reader.ReadLine().ToCharArray().ToList().ForEach(securePass.AppendChar);
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
            

            MailProviderFactory emailFactory = new ImapProviderFactory();
            MailConnection connection = emailFactory.CreateConnection();
            //connection.Host = config.Imap.Host;
            //connection.IsSslAuthentication = config.Imap.IsSslAuthentication;
            //connection.Port = config.Imap.Port;
            connection.Host = "imap.gmail.com";
            connection.IsSslAuthentication = true;
            connection.Port = 993;
            connection.Open();
            MailClient client = emailFactory.CreateClient();
            client.Authenticate(new MailUserInfo() { Email = "ozzy2106@gmail.com", Password = securePass });

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

            MailCommand command = client.Connection.CreateCommand();
            //command.Command = "STATUS INBOX (messages)";
            //command.ExecuteCommand();
            //LoggerHolders.ConsoleLogger.Log(command.Response);

            command.Command = "SELECT INBOX";
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response);

            command.Command = "FETCH " + 5343 + " (body[header.fields (from subject date)])";
            command.ExecuteCommand();
            LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Debug);
            StreamWriter streamWriter = new StreamWriter(@"C:\header.txt");
            streamWriter.Write(command.Response);
            streamWriter.Close();
            string pattern = @"\b(From: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q))\?(?<base64Text>[A-Za-z0-9=]+)\?=) \<(?<email>[A-Za-z0-9@.]+)\>";
            string forTest = "From: Mister Ozzy <emaui>";
            string fromWithOutEncoding = @"From: (?<name>[A-Za-z0-9\s]+) \<(?<email>[A-Za-z0-9@.]+)\>";
            string Date = @"Date: (?<date>[A-Za-z0-9,+:\s]+)";
            string SubjectWithEncoding =
                @"Subject: =\?(?<charset>[A-Za-z0-9-]+)\?(?<encoding>(B|Q))\?(?<subject>[A-Za-z0-9=]+)\?=";
            Regex regex = new Regex(SubjectWithEncoding);
            ////Console.WriteLine(pattern.Contains(@"From: =\?"));
            // Получаем совпадения в экземпляре класса Match
            ////Match match = regex.Match(command.Response);
            Match match = regex.Match(command.Response);
            Console.WriteLine(match.Groups["charset"].Value);
            ////Console.WriteLine(match.Groups["charset"].Value);
            Console.WriteLine(match.Groups["encoding"].Value);
            ////Console.WriteLine(match.Groups["email"].Value);
            string fromStr = match.Groups["subject"].Value;
            Console.WriteLine(fromStr);

            //var decodedString = DecodedString("PSttdTSV2CL8KtyuTdicqhHuvhKW2MmW1446681346");



            //LoggerHolders.ConsoleLogger.Log(decodedString, LogType.Debug);

            command.Command = "FETCH " + 5343 + " body[text]";
            command.ExecuteCommand();
            ////StreamWriter writer = new StreamWriter(@"C:\TestMes.txt");
            ////writer.Write(command.Response);
            ////writer.Close();
            
            LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Debug);
            TextWriter writer = new StreamWriter(@"C:\aaaa.txt");
            writer.Write(command.Response);
            writer.Close();
            //LoggerHolders.ConsoleLogger.Log(command.Response, LogType.Debug);

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
