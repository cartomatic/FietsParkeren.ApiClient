using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace FietsParkeren.ApiClient
{
    public class ServiceConfig
    {
        public class Endpoint
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        public string AuthorizationScheme { get; set; }

        public List<Endpoint> Endpoints { get; set; }

        public Routes Routes { get; set; }

        private static ServiceConfig _cfg;

        public static ServiceConfig Read()
        {
            if (_cfg != null)
                return _cfg;

            var cfg = Cartomatic.Utils.NetCoreConfig.GetNetCoreConfig();

            _cfg = cfg.GetSection("FietsParkeren").Get<ServiceConfig>();

            return _cfg;
        }

        /// <summary>
        /// returns endpoints
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Endpoint> GetEndpoints()
        {
            return Endpoints;
        }

        /// <summary>
        /// returns endpoint id
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public Endpoint GetEndpoint(int idx)
        {
            var ep = GetEndpoints().ToArray();
            if (idx < 0 || idx >= ep.Length)
                throw new ArgumentException("Endpoint idx out of range");

            return ep[idx];
        }
    }

    public class Routes
    {
        public string Authorities { get; set; }

        public string Contractors { get; set; }

        public string Surveys { get; set; }

        public string DynamicData { get; set; }

        public string StaticData { get; set; }
    }
}
