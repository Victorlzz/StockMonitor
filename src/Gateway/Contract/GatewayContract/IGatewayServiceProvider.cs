using System;

namespace Stock.Contract.GatewayContract
{
    public interface IGatewayServiceProvider
    {
        T Get<T>();
    }
}