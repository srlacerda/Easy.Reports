using System;

namespace Easy.Reports.Domain.Interfaces
{
    public interface ILogger
    {
        void Error(Exception exception, string message);
        void Info(string message);
        void Warning(string message);
    }
}