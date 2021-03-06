using System;

namespace WatchShop.Args
{
    // Класс аргументов для создания записи в логе текущей сессии
    public class LogEventArgs : EventArgs
    {
        public readonly string MethodName;
        public readonly object Argument;
        public readonly bool IsSuccessful;
        public readonly DateTime RequestTime;

        public LogEventArgs(string methodName, object argument, bool isSuccesful, DateTime requestTime)
        {
            MethodName = methodName;
            Argument = argument;
            IsSuccessful = isSuccesful;
            RequestTime = requestTime;
        }
    }
}
