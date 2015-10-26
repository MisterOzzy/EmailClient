﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailClient.Util.Logger
{
    public interface ILogger
    {
        void Log(string message, LogType type = LogType.Info, Exception ex = null);
    }
}
