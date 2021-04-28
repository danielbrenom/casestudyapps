using System;

namespace Foundation.Interfaces
{
    public interface IServiceManager
    {
        TInstance GetInstance<TInstance>() where TInstance : class;
        object GetInstance(Type instance);
    }
}