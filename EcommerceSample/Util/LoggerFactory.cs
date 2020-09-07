using System;
using EcommerceSample.Interfaces.Util;

namespace EcommerceSample.Util
{
    public class LoggerFactory
    {
        public static ILogger CreateLogger()
        {
            //todo isolate for other loggers
            return new Logger();
        }
    }
}
