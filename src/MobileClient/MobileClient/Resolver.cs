namespace MobileClient
{
    public static class Resolver
    {
        private static IServiceProvider _serviceProvider;
        public static IServiceProvider ServiceProvider => _serviceProvider ?? throw new Exception("Service provider has not been initialized");

        /// <summary>
        /// Register the service provider
        /// </summary>
        public static void RegisterServiceProvider(IServiceProvider sp)
        {
            _serviceProvider = sp;
        }
        /// <summary>
        /// Get service of type <typeparamref name="T"/> from the service provider.
        /// </summary>
        public static T Resolve<T>() where T : class
            => ServiceProvider.GetRequiredService<T>();
    }
}
