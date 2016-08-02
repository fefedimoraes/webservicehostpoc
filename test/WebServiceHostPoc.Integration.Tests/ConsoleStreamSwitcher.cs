using System;
using System.IO;

namespace WebServiceHostPoc.Integration.Tests
{
    public class ConsoleStreamSwitcher : IDisposable
    {
        public ConsoleStreamSwitcher(TextWriter writer)
        {
            CurrentWriter = Console.Out;
            Console.SetOut(writer);
        }

        private TextWriter CurrentWriter { get; }

        public void Dispose()
        {
            Console.SetOut(CurrentWriter);
        }
    }
}