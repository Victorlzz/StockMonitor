using Microsoft.Extensions.DependencyInjection;
using Stock.Contract.GatewayContract;
using Stock.Service.GatewayService;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GatewayServiceLocator
    {
        public static IServiceCollection AddGatewayServiceLocator(this IServiceCollection services)
        {
            services.AddSingleton<IGatewayServiceProvider, GatewayServiceProvider>();
            return services;
        }
    }
}

