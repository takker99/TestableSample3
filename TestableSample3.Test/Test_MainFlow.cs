using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Unity;
using Xunit;
using TestableSample3.App;
using TestableSample3.Lib;

namespace TestableSample3.Test
{
    public class Test_MainFlow
    {
        private MainFlow MainFlow { get; }

        private Mock<IConsoleWrapper> ConsoleWrapperMock { get; }
        private Mock<ITaskWrapper   > TaskWrapperMock    { get; }

        public Test_MainFlow()
        {
            ConsoleWrapperMock = new Mock<IConsoleWrapper>();
            TaskWrapperMock    = new Mock<ITaskWrapper>();

            var container = new UnityContainer();
            container.RegisterInstance<IConsoleWrapper>(ConsoleWrapperMock.Object);
            container.RegisterInstance<ITaskWrapper   >(TaskWrapperMock   .Object);
            container.RegisterType<MainFlow>();

            MainFlow = container.Resolve<MainFlow>();
        }

        [Theory]
        [MemberData(nameof(Run_Data))]
        public async Task Run(IEnumerable<TypedValue> expecteds, string value)
        {
            ConsoleWrapperMock.Setup(x => x.ReadLine(It.IsAny<string>()))
                              .Returns<string>(x => value);
            
            TaskWrapperMock.Setup(x => x.Delay(It.IsAny<int>()))
                           .Returns<int>(x => Task.CompletedTask);

            await MainFlow.Run();

            var count = expecteds.Count();
            TaskWrapperMock   .Verify(x => x.Delay    (It.IsAny<int   >()), Times.Exactly(count));
            ConsoleWrapperMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Exactly(count));

            foreach (var e in expecteds)
            {
                ConsoleWrapperMock.Verify(x => x.WriteLine(e.ToString()), Times.Once);
            }
        }

        public static IEnumerable<object[]> Run_Data()
        {
            return new[]
            {
                new object[]
                {
                    new[]
                    {
                        new TypedValue(""),
                        new TypedValue("null"),
                        new TypedValue("true"),
                        new TypedValue("false"),
                        new TypedValue("-1"),
                        new TypedValue("2.3"),
                        new TypedValue(" "),
                        new TypedValue("abcde")
                    },
                    ",null,true,false,-1,2.3, ,abcde"
                },
            };
        }
    }
}
