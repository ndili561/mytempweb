using System;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace CRM.WebAPI.Infrastructure
{
    public class ProxyBuilder
    {
        private readonly IConfiguration _configuration;

        public ProxyBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IWebProxy Build()
        {
            var url = _configuration.GetSection("Proxy:Url").Value;
            var username = _configuration.GetSection("Proxy:Username").Value;
            var password = _configuration.GetSection("Proxy:Password").Value;
            var domain = _configuration.GetSection("Proxy:Domain").Value;
            var bypassRegex = _configuration.GetSection("Proxy:BypassRegex").Value;

            var address = new Uri(url);
            var credentials = string.IsNullOrWhiteSpace(domain) ? new NetworkCredential(username, password) : new NetworkCredential(username, password, domain);

            return string.IsNullOrWhiteSpace(bypassRegex)
                ? (IWebProxy)new WebProxy { Address = address, Credentials = credentials }
                : new GatewayCoreProxy(address, credentials, bypassRegex);
        }
    }
}