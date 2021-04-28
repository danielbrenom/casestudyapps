using System;
using Foundation.Abstracts;
using SimpleInjector;
using XamarinBaseApp.Configuration;

namespace XamarinBaseApp.Android
{
    public class AndroidFactory : ServiceManager<AndroidFactory>
    {
        private Container Container { get; }
        public AndroidFactory()
        {
            Container = Instance.GetContainer();
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
}