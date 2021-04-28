using System;
using Foundation.Interfaces;

namespace Foundation.Abstracts
{
    public abstract class ServiceManager : IServiceManager
    {
        public static IServiceManager Instance { get; set; }
        public abstract TInstance GetInstance<TInstance>() where TInstance : class;
        public abstract object GetInstance(Type instance);
    }

    public abstract class ServiceManager<T> : ServiceManager where T : IServiceManager, new()
    {
        public static void Init()
        {
            Instance ??= new T();
        }
    }
}