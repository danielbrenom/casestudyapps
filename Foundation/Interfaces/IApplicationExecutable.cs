using System.Threading.Tasks;

namespace Foundation.Interfaces
{
    public interface IApplicationExecutable<TExecutable>
    {
        public void ReceiveParameters(params object[] parameters);
        public Task<object> Execute();
    }
}