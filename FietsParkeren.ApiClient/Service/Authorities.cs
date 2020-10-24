using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FietsParkeren.ApiClient.DataModel;

namespace FietsParkeren.ApiClient
{
    public partial class Service
    {
        /// <summary>
        /// Gets authorities
        /// </summary>
        /// <param name="user">auth user name</param>
        /// <param name="pass">auth user pass</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Authority>> GetAuthoritiesAsync(string user, string pass)
        {
            return await GetAuthoritiesAsync(GetAuthorizationHeaderValue(user, pass));
        }

        /// <summary>
        /// Gets authorities
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Authority>> GetAuthoritiesAsync(string authToken)
        {
            return await GetAuthoritiesInternalAsync(GetAuthorizationHeaderValue(authToken));
        }


        /// <summary>
        /// Gets authorities
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Authority>> GetAuthoritiesInternalAsync(string authHdr)
        {
            var cfg = ServiceConfig.Read();
            
            var authCall = await ApiCall<IEnumerable<AuthorityRaw>>(
                cfg.Endpoint,
                cfg.Routes.Authorities,
                authHdr
            );

            return authCall?.AsAuthorities();
        }
    }
}
