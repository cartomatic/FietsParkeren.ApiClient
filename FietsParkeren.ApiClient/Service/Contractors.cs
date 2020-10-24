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
        /// Gets contractors
        /// </summary>
        /// <param name="user">auth user name</param>
        /// <param name="pass">auth user pass</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Contractor>> GetContractorsAsync(string user, string pass)
        {
            return await GetContractorsAsync(GetAuthorizationHeaderValue(user, pass));
        }

        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Contractor>> GetContractorsAsync(string authToken)
        {
            return await GetContractorsInternalAsync(GetAuthorizationHeaderValue(authToken));
        }


        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Contractor>> GetContractorsInternalAsync(string authHdr)
        {
            var cfg = ServiceConfig.Read();
            
            var authCall = await ApiCall<IEnumerable<ContractorRaw>>(
                cfg.Endpoint,
                cfg.Routes.Contractors,
                authHdr
            );

            return authCall?.AsContractors();
        }
    }
}
