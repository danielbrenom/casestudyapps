using System;
using Applications;
using Foundation.Abstracts;
using Foundation.Interfaces;
using SimpleInjector;
using XamarinBaseApp.Services;
using XamarinBaseApp.ViewModels;

namespace XamarinBaseApp.Configuration
{
    public class ServiceFactory : ServiceManager<ServiceFactory>
    {
        protected internal Container Container { get; }

        public ServiceFactory()
        {
            Container = new Container();
            ConfigureViewModels();
            ConfigureRepositories();
            ConfigureServices();
            VerifyContainer();
        }

        private void ConfigureViewModels()
        {
            Container.Register<HomePageVm>();
            Container.Register<BinaryDecimalVm>();
        }

        private void ConfigureRepositories()
        {
            
        }

        private void ConfigureServices()
        {
            Container.Register<IAlert, Alert>();
            Container.Register<INavigator, Navigator>();
            Container.Register(typeof(IApplicationExecutable<>), typeof(Binary2Decimal).Assembly, Lifestyle.Singleton);
        }

        private void VerifyContainer()
        {
            Container.Verify();
        }

        public override TInstance GetInstance<TInstance>()
        {
            return Container.GetInstance<TInstance>();
        }

        public override object GetInstance(Type instance)
        {
            return Container.GetInstance(instance);
        }
    }
    
    public static class FactoryExtension
    {
        public static Container GetContainer(this IServiceManager serviceManager)
        {
            return ((ServiceFactory) serviceManager).Container;
        }
    }
}