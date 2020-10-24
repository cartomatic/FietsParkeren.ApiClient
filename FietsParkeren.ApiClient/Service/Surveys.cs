using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FietsParkeren.ApiClient.DataModel;
using Rollbar.Common;

namespace FietsParkeren.ApiClient
{
    public partial class Service
    {
        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="user">auth user name</param>
        /// <param name="pass">auth user pass</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Survey>> GetSurveysAsync(string user, string pass, SurveyFilter filter)
        {
            return await GetSurveysAsync(GetAuthorizationHeaderValue(user, pass), filter);
        }

        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Survey>> GetSurveysAsync(string authToken, SurveyFilter filter)
        {
            return await GetSurveyInternalsAsync(GetAuthorizationHeaderValue(authToken), filter);
        }

        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Survey>> GetSurveyInternalsAsync(string authHdr, SurveyFilter filter)
        {
            var cfg = ServiceConfig.Read();
            
            var authCall = await ApiCall<SurveysRaw>(
                cfg.Endpoint,
                cfg.Routes.Surveys,
                authHdr,
                queryParams: filter?.ToQueryParams()
            );

            return authCall?.AsSurveys();
        }
    }
}
