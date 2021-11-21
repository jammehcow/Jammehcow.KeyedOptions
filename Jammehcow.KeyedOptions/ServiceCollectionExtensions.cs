using System;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Jammehcow.KeyedOptions
{
    public static class ServiceCollectionExtensions
    {
        public static OptionsBuilder<TOptions> AddKeyedOptions<TOptions>(this IServiceCollection serviceCollection)
            where TOptions : class, IKeyedOptions, new()
        {
            return serviceCollection
                .AddOptions<TOptions>()
                .Configure((TOptions options, IConfiguration configuration) =>
                    configuration.GetSection(options.SectionKey).Bind(options));
        }

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
