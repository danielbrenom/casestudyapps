using Foundation.Interfaces;

namespace XamarinBaseApp.Configuration
{
    public static class AppFactory
    {
        private static bool _initialized;

        public static void InitApp<T>() where T : IServiceManager, new()
        {
            if (_initialized)
                return;
            _initialized = true;
            ServiceFactory.Init();
        }
    }
}