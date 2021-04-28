using System.Collections.Generic;
using System.Threading.Tasks;

namespace Foundation.Interfaces
{
    public interface INavigator
    {
        void InitNavigation<TViewModel>(IReadOnlyDictionary<string, object> parameters = null, bool  allowNavbar = true) where TViewModel : INavigableViewModel;
        Task InitNavigationAsync<TViewModel>(IReadOnlyDictionary<string, object> parameters = null, bool  allowNavbar = true) where TViewModel : INavigableViewModel;
        Task NavigateTo<TViewModel>() where TViewModel : INavigableViewModel;
        Task NavigateTo<TViewModel>(IReadOnlyDictionary<string, object> parameters) where TViewModel : INavigableViewModel;
        Task Return(IReadOnlyDictionary<string, object> parameters = null);
        Task ClearStack();
    }
}