using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Foundation.Abstracts;
using Foundation.Interfaces;
using Xamarin.Forms;

namespace XamarinBaseApp.Services
{
    public class Navigator : INavigator
    {
        private static Page CurrentPage => Application.Current.MainPage;
        private IAlert Alert { get; }

        public Navigator(IAlert alert)
        {
            Alert = alert;
        }

        public void InitNavigation<TViewModel>(IReadOnlyDictionary<string, object> parameters = null, bool allowNavbar = true) where TViewModel : INavigableViewModel
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var page = InstantiatePage(typeof(TViewModel));
                    Application.Current.MainPage = allowNavbar ? new NavigationPage(page) : page;
                    if (!(ServiceManager.Instance.GetInstance(typeof(TViewModel)) is INavigableViewModel viewModel)) return;
                    page.BindingContext = viewModel;
                    viewModel.Prepare();
                    viewModel.Prepare(parameters);
                    await viewModel.Initialize();
                }
                catch (Exception e)
                {
                    await Alert.CallAlertAsync("Error", "Not able to navigate to destination page", new AlertAction("OK"));
                }
            });
        }

        public async Task InitNavigationAsync<TViewModel>(IReadOnlyDictionary<string, object> parameters = null, bool allowNavbar = true) where TViewModel : INavigableViewModel
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    var page = InstantiatePage(typeof(TViewModel));
                    Application.Current.MainPage = allowNavbar ? new NavigationPage(page) : page;
                    if (!(ServiceManager.Instance.GetInstance(typeof(TViewModel)) is INavigableViewModel viewModel)) return;
                    page.BindingContext = viewModel;
                    viewModel.Prepare();
                    viewModel.Prepare(parameters);
                    await viewModel.Initialize();
                }
                catch (Exception e)
                {
                    await Alert.CallAlertAsync("Error", "Not able to navigate to destination page", new AlertAction("OK"));
                }
            });
        }

        public async Task NavigateTo<TViewModel>() where TViewModel : INavigableViewModel
        {
            await NavigateTo<TViewModel>(null);
        }

        public async Task NavigateTo<TViewModel>(IReadOnlyDictionary<string, object> parameters) where TViewModel : INavigableViewModel
        {
            try
            {
                await Navigate(typeof(TViewModel), parameters);
            }
            catch (Exception e)
            {
                await Alert.CallAlertAsync("Error", "Not able to navigate to destination page", new AlertAction("OK"));
            }
        }

        public async Task Return(IReadOnlyDictionary<string, object> parameters = null)
        {
            if (CurrentPage.Navigation.NavigationStack.Count > 0)
            {
                await CurrentPage.Navigation.PopAsync(true);
                if (parameters is null)
                    return;
                var page = CurrentPage.Navigation.NavigationStack.LastOrDefault() ?? Application.Current.MainPage;
                ((INavigableViewModel) page.BindingContext).Prepare(parameters);
                await ((INavigableViewModel) page.BindingContext).Initialize();
            }
        }

        public async Task ClearStack()
        {
            await CurrentPage.Navigation.PopToRootAsync();
        }

        private async Task Navigate(Type tipoVm, IReadOnlyDictionary<string, object> parametros)
        {
            if (!(ServiceManager.Instance.GetInstance(tipoVm) is INavigableViewModel viewModel))
                throw new ArgumentNullException();
            viewModel.Prepare();
            viewModel.Prepare(parametros);
            var page = InstantiatePage(tipoVm);
            page.BindingContext = viewModel;
            await viewModel.Initialize();
            await CurrentPage.Navigation.PushAsync(page);
        }

        private static Page InstantiatePage(Type tipoVm)
        {
            var pageName = tipoVm.FullName?.Replace("Vm", string.Empty).Replace("ViewModels", "Views");
            var viewModelAssembly = tipoVm.GetTypeInfo().Assembly.FullName;
            var pageAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", pageName, viewModelAssembly);
            var pageType = Type.GetType(pageAssemblyName);
            if (pageType is null)
                throw new ArgumentNullException();
            var page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}