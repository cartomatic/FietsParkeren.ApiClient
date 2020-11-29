using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FietsParkeren.ApiClient.DataModel;
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
        private static async Task<IEnumerable<T>> ApiCall<T>(string url, string route, string auth, Dictionary<string, object> queryParams = null)
            where T : BaseResponse
        {
            var output = new List<T>();
            var hasMoreResults = true;
            var page = 1;

            queryParams ??= new Dictionary<string, object>();
            queryParams["page"] = page;

            while (hasMoreResults)
            {
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
                    {
                        var result = apiCall.Output;
                        output.Add(result);

                        //if the results are paged, keep on poking the api to obtain all of them
                        if ((result.TotalHits ?? 0) > (result.Page ?? 0) * (result.PageSize ?? 0))
                        {
                            page++;
                        }
                        else
                        {
                            hasMoreResults = false;
                        }
                    }
                    else
                    {
                        hasMoreResults = false;
                    }
                }
                catch
                {
                    hasMoreResults = false;
                }
            }

            return output;
        }
    }
}
