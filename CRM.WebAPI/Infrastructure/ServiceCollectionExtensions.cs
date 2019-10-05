using System;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.WebAPI.Infrastructure
{
    /// <summary>
    /// http://dotnetliberty.com/index.php/2016/05/09/asp-net-core-factory-pattern-dependency-injection/
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingletonFactory<T, TFactory>(this IServiceCollection collection)
            where T : class where TFactory : class, IServiceFactory<T>
        {
            collection.AddTransient<TFactory>();

            return AddInternal<T, TFactory>(collection, p => ServiceProviderServiceExtensions.GetRequiredService<TFactory>(p), ServiceLifetime.Singleton);
        }

        private static IServiceCollection AddInternal<T, TFactory>(
            this IServiceCollection collection,
            Func<IServiceProvider, TFactory> factoryProvider,
            ServiceLifetime lifetime) where T : class where TFactory : class, IServiceFactory<T>
        {
            Func<IServiceProvider, object> factoryFunc = provider =>
            {
                var factory = factoryProvider(provider);
                return factory.Build();
            };

            var descriptor = new ServiceDescriptor(typeof(T), factoryFunc, lifetime);
            collection.Add(descriptor);
            return collection;
        }
    }
}
