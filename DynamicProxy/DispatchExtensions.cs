using System;
namespace Microsoft.Extensions.DependencyInjection
{
    public static class DispatchExtensions
    {
        public static IServiceCollection AddSingletonProxy<TService, TImplementation, TDispatcherEvent>(
            this IServiceCollection serviceCollection)
            where TService : class
            where TDispatcherEvent : class, IDispatcherEvent
            where TImplementation : class, TService ,new()
        {
            serviceCollection.AddSingleton<TImplementation>();
            serviceCollection.AddSingleton<TDispatcherEvent>();
            serviceCollection.AddSingleton<TService>(sp =>
            {
                var watch = sp.GetService<TImplementation>();
                var dispatcherEvent = sp.GetService<TDispatcherEvent>();
                return Dispatch<TService, TImplementation>.Create(watch, dispatcherEvent) ??
                       throw new InvalidOperationException();
            });
            return serviceCollection;
        }


        public static IServiceCollection AddScopeProxy<TService, TImplementation, TDispatcherEvent>(
            this IServiceCollection serviceCollection)
            where TService : class
            where TDispatcherEvent : class, IDispatcherEvent
            where TImplementation : class, TService ,new()
        {
            serviceCollection.AddScoped<TImplementation>();
            serviceCollection.AddScoped<TDispatcherEvent>();
            serviceCollection.AddScoped<TService>(sp =>
            {
                var watch = sp.GetService<TImplementation>();
                var dispatcherEvent = sp.GetService<TDispatcherEvent>();
                return Dispatch<TService, TImplementation>.Create(watch, dispatcherEvent) ??
                       throw new InvalidOperationException();
            });
            return serviceCollection;
        }
        public static IServiceCollection AddTransientProxy<TService, TImplementation, TDispatcherEvent>(
            this IServiceCollection serviceCollection)
            where TService : class
            where TDispatcherEvent : class, IDispatcherEvent
            where TImplementation : class, TService ,new()
        {
            serviceCollection.AddTransient<TImplementation>();
            serviceCollection.AddTransient<TDispatcherEvent>();
            serviceCollection.AddTransient<TService>(sp =>
            {
                var watch = sp.GetService<TImplementation>();
                var dispatcherEvent = sp.GetService<TDispatcherEvent>();
                return Dispatch<TService, TImplementation>.Create(watch, dispatcherEvent) ??
                       throw new InvalidOperationException();
            });

            return serviceCollection;
        }
    }
}