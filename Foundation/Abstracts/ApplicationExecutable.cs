using System.Threading.Tasks;
using Foundation.Interfaces;

namespace Foundation.Abstracts
{
    public abstract class ApplicationExecutable<T> : IApplicationExecutable<T>
    {
        public abstract void ReceiveParameters(params object[] parameters);

        public abstract Task<object> Execute();
    }
}