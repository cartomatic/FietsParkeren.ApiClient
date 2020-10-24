using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FietsParkeren.ApiClient.DataModel;
using RestSharp;
using Rollbar.Common;

namespace FietsParkeren.ApiClient
{
    public partial class Service
    {
        /// <summary>
        /// Gets survey static data
        /// </summary>
        /// <param name="user">auth user name</param>
        /// <param name="pass">auth user pass</param>
        /// <param name="surveyId"></param>
        /// <remarks>because output is totally dynamic and there is no need to process data on the serverside outputs whatever api returns</remarks>
        /// <returns>whatever source api returns</returns>
        public static async Task<IRestResponse> GetSurveyStaticDataAsync(string user, string pass, string surveyId)
        {
            return await GetSurveyStaticDataAsync(GetAuthorizationHeaderValue(user, pass), surveyId);
        }

        /// <summary>
        /// Gets survey static data
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="surveyId"></param>
        /// <remarks>because output is totally dynamic and there is no need to process data on the serverside outputs whatever api returns</remarks>
        /// <returns>whatever source api returns</returns>
        public static async Task<IRestResponse> GetSurveyStaticDataAsync(string authToken, string surveyId)
        {
            return await GetSurveyStaticDataInternalsAsync(GetAuthorizationHeaderValue(authToken), surveyId);
        }

        /// <summary>
        /// Gets survey static data
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="surveyId"></param>
        /// <remarks>because output is totally dynamic and there is no need to process data on the serverside outputs whatever api returns</remarks>
        /// <returns>whatever source api returns</returns>
        protected internal static async Task<IRestResponse> GetSurveyStaticDataInternalsAsync(string authHdr, string surveyId)
        {
            var cfg = ServiceConfig.Read();
            
            return await ApiCall(
                cfg.Endpoint,
                cfg.Routes.StaticData,
                authHdr,
                queryParams: new Dictionary<string, object>
                {
                    { "surveyId", surveyId}
                }
            );
        }
    }
}
