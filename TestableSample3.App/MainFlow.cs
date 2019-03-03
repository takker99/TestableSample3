using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;
using TestableSample3.Lib;

namespace TestableSample3.App
{
    public class MainFlow
    {
        [Dependency]
        public IConsoleWrapper ConsoleWrapper { get; set; }

        [Dependency]
        public ITaskWrapper TaskWrapper { get; set; }

        public async Task Run()
        {
            var typedValues = ReadTypedValues();

            foreach (var tv in typedValues)
            {
                await Task.Delay(1000);
                ConsoleWrapper.WriteLine(tv);
            }
        }

        private IEnumerable<TypedValue> ReadTypedValues()
        {
            var valueStrs = ConsoleWrapper.ReadLine("カンマ区切りの値を入力：");

            return valueStrs.Split(",", StringSplitOptions.None)
                            .Select(x => new TypedValue(x));
        }
    }
}
