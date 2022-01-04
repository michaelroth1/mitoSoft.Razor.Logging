using System;
using System.Collections.Concurrent;
using System.Threading;

namespace mitoSoft.Razor.Logging.ColorConsole
{
    internal static class NonBlockingConsole
    {
        private static readonly BlockingCollection<ConsoleTextModel> _queue = new();

        //https://stackoverflow.com/questions/3670057/does-console-writeline-block
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        static NonBlockingConsole()
        {
            var thread = new Thread(
              () =>
              {
                  while (true)
                  {
                      var consoleText = _queue.Take();
                      if (consoleText.Type == ConsoleWriteType.WriteLineWithColor)
                      {
                          var tempColor = Console.ForegroundColor;
                          Console.ForegroundColor = consoleText.Color;
                          Console.WriteLine(consoleText.Text);
                          Console.ForegroundColor = tempColor;
                      }
                      else if (consoleText.Type == ConsoleWriteType.WriteLine)
                      {
                          Console.WriteLine(consoleText.Text);
                      }
                      else if (consoleText.Type == ConsoleWriteType.WriteWithColor)
                      {
                          var tempColor = Console.ForegroundColor;
                          Console.ForegroundColor = consoleText.Color;
                          Console.Write(consoleText.Text);
                          Console.ForegroundColor = tempColor;
                      }
                      else if (consoleText.Type == ConsoleWriteType.Write)
                      {
                          Console.Write(consoleText.Text);
                      }                      
                  }
              })
            {
                IsBackground = true
            };
            thread.Start();
        }

        public static void WriteLine(string value)
        {
            _queue.Add(new ConsoleTextModel(ConsoleWriteType.WriteLine, value));
        }

        public static void WriteLine(string value, ConsoleColor color)
        {
            _queue.Add(new ConsoleTextModel(ConsoleWriteType.WriteLineWithColor, color, value));
        }

        public static void Write(string value)
        {
            _queue.Add(new ConsoleTextModel(ConsoleWriteType.WriteWithColor, value));
        }

        public static void Write(string value, ConsoleColor color)
        {
            _queue.Add(new ConsoleTextModel(ConsoleWriteType.WriteWithColor, color, value));
        }
    }
}