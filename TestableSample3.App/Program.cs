using System.Threading.Tasks;
using Unity;
using TestableSample3.Lib;

namespace TestableSample3.App
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IConsoleWrapper, ConsoleWrapper>();
            container.RegisterType<ITaskWrapper   , TaskWrapper   >();
            container.RegisterType<MainFlow>();

            var mainFlow = container.Resolve<MainFlow>();
            await mainFlow.Run();
        }
    }
}
