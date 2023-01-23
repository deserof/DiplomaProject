namespace MobileClient.Extensions
{
    public static class MauiAppExtensions
    {
        public static void UseResolver(this MauiApp app)
        {
            Resolver.RegisterServiceProvider(app.Services);
        }
    }
}
