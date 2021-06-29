using System;

namespace Stock.Infrastructure.ConfigurationContract
{
    public interface IConfigurationServiceProvider
    {
        T Get<T>(string key);
    }
}