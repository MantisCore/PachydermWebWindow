using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Pachyderm.UI.Services
{
    public class ConfigurationService
    {
        private IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("config.json", true);
            _configuration = builder.Build();
        }

        public string Get(string key)
        {
            return _configuration[key];
        }
    }
}
