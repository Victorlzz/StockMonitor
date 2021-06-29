using Microsoft.Extensions.Configuration;
using Stock.Infrastructure.ConfigurationContract;
using System;
using System.Collections.Generic;

namespace Stock.Infrastructure.ConfigurationService
{
    public class ConfigurationServiceProvider : IConfigurationServiceProvider
    {
        private readonly IConfiguration _configuration;
        private Dictionary<string, string> _data = new Dictionary<string, string>();

        public ConfigurationServiceProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Get<T>(string key)
        {
            //if (this._data.ContainsKey(key))
            //    return (T)Convert.ChangeType(this._data[key], typeof(T));
            return (T)Convert.ChangeType(this._configuration[key], typeof(T));
        }
    }
}