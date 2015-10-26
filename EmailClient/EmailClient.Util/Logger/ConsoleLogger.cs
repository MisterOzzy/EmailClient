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
            Console.WriteLine("[{0}] {1}: {2}\n{3}", DateTime.UtcNow, type, message, ex);
        }
    }
}
