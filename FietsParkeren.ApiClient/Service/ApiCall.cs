using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using RestSharp;

namespace FietsParkeren.ApiClient
{
    public partial class Service
    {
        /// <summary>
        /// Generic external api caller
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="route"></param>
        /// <param name="auth">Authorization header value</param>
        /// <param name="queryParams">query params</param>
        /// <returns></returns>
        private static async Task<T> ApiCall<T>(string url, string route, string auth, Dictionary<string, object> queryParams = null)
            where T : class
        {
            var output = default(T);

            try
            {
                var apiCall = await Cartomatic.Utils.RestApi.RestApiCall<T>(
                    url,
                    route,
                    Method.GET,
                    authToken: auth,
                    queryParams: queryParams
                );

                if (apiCall.Response.IsSuccessful)
                    output = apiCall.Output;
            }
            catch
            {
                //ignore
            }

            return output;
        }

        /// <summary>
        /// Generic external api caller
        /// </summary>
        /// <param name="url"></param>
        /// <param name="route"></param>
        /// <param name="auth"></param>
        /// <param name="queryParams"></param>
        /// <returns>original api response</returns>
        private static async Task<IRestResponse> ApiCall(string url, string route, string auth, Dictionary<string, object> queryParams = null)
        {
            try
            {
                return await Cartomatic.Utils.RestApi.RestApiCall(
                    url,
                    route,
                    Method.GET,
                    authToken: auth,
                    queryParams: queryParams
                );
            }
            catch
            {
                //ignore
            }

            return null;
        }
    }
}
