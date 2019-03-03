using System;

namespace TestableSample3.Lib
{
    public class ConsoleWrapper : IConsoleWrapper
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }
    }
}
