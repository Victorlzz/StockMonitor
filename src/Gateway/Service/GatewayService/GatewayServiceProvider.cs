using Stock.Contract.GatewayContract;
using System;

namespace Stock.Service.GatewayService
{
    public class GatewayServiceProvider : IGatewayServiceProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public GatewayServiceProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T Get<T>()
        {
            return (T)_serviceProvider.GetService(typeof(T));
        }
    }
}

