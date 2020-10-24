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
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="user">auth user name</param>
        /// <param name="pass">auth user pass</param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Section>> GetSurveyDynamicDataAsync(string user, string pass, string surveyId)
        {
            return await GetSurveyDynamicDataAsync(GetAuthorizationHeaderValue(user, pass), surveyId);
        }

        /// <summary>
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<Section>> GetSurveyDynamicDataAsync(string authToken, string surveyId)
        {
            return await GetSurveyDynamicDataInternalsAsync(GetAuthorizationHeaderValue(authToken), surveyId);
        }

        /// <summary>
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="surveyId"></param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Section>> GetSurveyDynamicDataInternalsAsync(string authHdr, string surveyId)
        {
            var cfg = ServiceConfig.Read();
            
            var authCall = await ApiCall<SurveyDynamicDataRaw>(
                cfg.Endpoint,
                cfg.Routes.DynamicData,
                authHdr,
                queryParams: new Dictionary<string, object>
                {
                    {"surveyId", surveyId}
                }
            );

            return authCall?.AsSection();
        }
    }
}
