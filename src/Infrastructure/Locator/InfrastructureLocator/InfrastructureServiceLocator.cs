using Stock.Infrastructure.ConfigurationContract;
using Stock.Infrastructure.ConfigurationService;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceLocator
    {
        public static IServiceCollection AddInfrastructureServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationServiceProvider, ConfigurationServiceProvider>();
            return services;
        }
    }
}
