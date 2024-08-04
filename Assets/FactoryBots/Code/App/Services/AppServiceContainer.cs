namespace FactoryBots.App.Services
{
    public class AppServiceContainer
    {
        public void RegisterSingle<TService>(TService implementation) where TService : class, IAppService => 
            Implementation<TService>.ServiceInstance = implementation;

        public TService Single<TService>() where TService : class, IAppService =>
            Implementation<TService>.ServiceInstance;

        private static class Implementation<TService> where TService : class, IAppService
        {
            public static TService ServiceInstance;
        }
    }
}
