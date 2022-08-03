using SignalRwithEntityFramework.SubscribeTableDependencies;

namespace SignalRwithEntityFramework.MiddlewareExtensions
{
    public static class ApplicationBuilderExtension
    {
        public static void UseSqlTableDependency<T>(this IApplicationBuilder applicationBuilder, string connectionString)
            where T : ISubscribeTableDependency
        {
            var serviceProvider = applicationBuilder.ApplicationServices;
            var service = serviceProvider.GetService<T>();
            service?.SubscribeTableDependency(connectionString);
        }
    }
}
