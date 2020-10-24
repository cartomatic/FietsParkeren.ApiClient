using Microsoft.Extensions.Configuration;

namespace FietsParkeren.ApiClient
{
    public class ServiceConfig
    {
        public string AuthorizationScheme { get; set; }

        public string Endpoint { get; set; }

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
