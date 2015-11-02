using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Util.Logger
{
    public class ConsoleLogger : ILogger
    {       
        public void Log(string message, LogType type = LogType.Info, Exception ex = null)
        {
#if DEBUG
            switch (type)
            {
                case LogType.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case LogType.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case LogType.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogType.Critical:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogType.Debug:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case LogType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;  
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }

            Console.WriteLine("[{0}] {1}: {2}{3}", DateTime.UtcNow, type, message, ex == null ? null : Environment.NewLine + ex);
            Console.ForegroundColor = ConsoleColor.Gray;
#endif
        }
    }
}
