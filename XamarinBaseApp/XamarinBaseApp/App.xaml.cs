using Foundation.Abstracts;
using Foundation.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBaseApp.ViewModels;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace XamarinBaseApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ServiceManager.Instance.GetInstance<INavigator>().InitNavigation<HomePageVm>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}