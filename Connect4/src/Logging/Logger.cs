using System;
using System.IO;

namespace Connect4.src.Logs
{
    internal static class Logger
    {
        private static readonly string FILE_NAME = "logs.txt";

        internal static void ClearLogs()
        {
            File.WriteAllText(FILE_NAME, "");
        }

        internal static void LogException(Exception e)
        {
            string content = $"({DateTime.Now.ToString()}) ERROR: {e.Message}\n{e.StackTrace}\n";

            File.AppendAllText(FILE_NAME, content);
        }

        internal static void LogWarning(string warning)
        {
            string content = $"({DateTime.Now.ToString()}) WARNING: {warning}\n";

            File.AppendAllText(FILE_NAME, content);
        }
    }
}
