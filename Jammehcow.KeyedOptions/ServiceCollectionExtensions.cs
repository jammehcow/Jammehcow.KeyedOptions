using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jammehcow.KeyedOptions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Resolves, binds and registers a given implementation of IKeyedOptions in an IServiceCollection
        /// </summary>
        /// <param name="serviceCollection">The container to resolve the root configuration and register the bound options to</param>
        /// <typeparam name="TOptions">An implementation of IKeyedOptions to be registered in the container</typeparam>
        /// <returns>The OptionsBuilder that TOptions was built with (for extra configuration)</returns>
        public static OptionsBuilder<TOptions> AddKeyedOptions<TOptions>(this IServiceCollection serviceCollection)
            where TOptions : class, IKeyedOptions, new()
        {
            return serviceCollection
                .AddOptions<TOptions>()
                .Configure((TOptions options, IConfiguration configuration) =>
                    configuration.GetSection(options.SectionKey).Bind(options));
        }

        /// <summary>
        /// Resolves, binds and registers a given implementation of IKeyedOptions in an IServiceCollection
        /// </summary>
        /// <param name="serviceCollection">The container to register the bound options to</param>
        /// <param name="configurationResolver">A function which provides an instance of IConfiguration</param>
        /// <typeparam name="TOptions">An implementation of IKeyedOptions to be registered in the container</typeparam>
        /// <returns>The OptionsBuilder that TOptions was built with (for extra configuration)</returns>
        /// <exception cref="InvalidOperationException">When an IConfiguration instance was not provided by the provided function</exception>
        public static OptionsBuilder<TOptions> AddKeyedOptions<TOptions>(this IServiceCollection serviceCollection,
            [InstantHandle]Func<IConfiguration?> configurationResolver) where TOptions : class, IKeyedOptions, new()
        {
            var configuration = configurationResolver.Invoke();
            if (configuration == null)
                throw new InvalidOperationException("Could not resolve an IConfiguration from the provided function");

            return serviceCollection
                .AddOptions<TOptions>()
                .Configure(options => configuration.GetSection(options.SectionKey).Bind(options));
        }
    }
}
