using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.Interfaces
{
    public interface INavigableViewModel
    {
        void Prepare();
        void Prepare(IReadOnlyDictionary<string, object> parameters);
        Task Initialize();
    }
}