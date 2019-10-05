using System;
using System.Net;
using System.Text.RegularExpressions;

namespace CRM.WebAPI.Infrastructure
{
    public class GatewayCoreProxy : IWebProxy
    {
        private readonly Uri _url;
        private readonly string _bypassedRegex;
        private readonly ICredentials _credentials;

        public GatewayCoreProxy(Uri url, ICredentials credentials, string bypassedRegex)
        {
            _url = url;
            _credentials = credentials;
            _bypassedRegex = bypassedRegex;
        }

        public ICredentials Credentials
        {
            get { return _credentials; }
            set { }
        }

        public Uri GetProxy(Uri destination)
        {
            return _url;
        }

        public bool IsBypassed(Uri host)
        {
            return new Regex(_bypassedRegex).IsMatch(host.Host);
        }
    }
}