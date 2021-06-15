using Easy.Reports.Domain.Interfaces;
using System;
using System.Text;

namespace Easy.Reports.Infra.CrossCutting.Log
{
    public class Logger : ILogger
    {
        private StringBuilder sb;
        public void Error(Exception exception, string message)
        {
            sb = new StringBuilder();
            sb.AppendLine("Level: [Error]");
            sb.AppendLine($"Message: [{message}]");
            sb.AppendLine($"Exception: [{exception}]");
            Console.WriteLine(sb.ToString());
        }
        public void Info(string message)
        {
            sb = new StringBuilder();
            sb.AppendLine("Level: [Information]");
            sb.AppendLine($"Message: [{message}]");
            Console.WriteLine(sb.ToString());
        }
    }
}
