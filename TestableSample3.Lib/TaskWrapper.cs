using System.Threading.Tasks;

namespace TestableSample3.Lib
{
    public class TaskWrapper : ITaskWrapper
    {
        public async Task Delay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
