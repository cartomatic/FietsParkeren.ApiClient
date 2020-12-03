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
        /// <param name="surveyIds">comma separated survey ids</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task<PagedResult<IEnumerable<SectionDynamicData>>> GetSurveyDynamicDataAsync(string user, string pass, string surveyIds, string geoPolygon, string geoRelation, int? pageSize = null, int? page = null)
        {
            return await GetSurveyDynamicDataAsync(GetAuthorizationHeaderValue(user, pass), surveyIds, geoPolygon, geoRelation, pageSize, page);
        }

        /// <summary>
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="surveyIds">comma separated survey ids</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task<PagedResult<IEnumerable<SectionDynamicData>>> GetSurveyDynamicDataAsync(string authToken, string surveyIds, string geoPolygon, string geoRelation, int? pageSize = null, int? page = null)
        {
            return await GetSurveyDynamicDataInternalsAsync(GetAuthorizationHeaderValue(authToken), surveyIds, geoPolygon, geoRelation, pageSize, page);
        }

        /// <summary>
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="surveyIds">comma separated survey ids</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        protected internal static async Task<PagedResult<IEnumerable<SectionDynamicData>>> GetSurveyDynamicDataInternalsAsync(string authHdr, string surveyIds, string geoPolygon, string geoRelation, int? pageSize = null, int? page = null)
        {
            var cfg = ServiceConfig.Read();

            var queryParams = PrepareGeoPolygonQuery(
                new Dictionary<string, object>
                {
                    {"surveyId", surveyIds}
                },
                geoPolygon,
                geoRelation
            );

            if(pageSize.HasValue)
                queryParams.Add("pageSize", pageSize.Value);

            if (page.HasValue)
                queryParams.Add("page", page.Value);


            var surveysDynamicDataCallResponse = await ApiCallSinglePage<SectionDynamicDataRawResponse>(
                cfg.Endpoint,
                cfg.Routes.DynamicData,
                authHdr,
                queryParams: queryParams
            );

            return new PagedResult<IEnumerable<SectionDynamicData>>
            {
                Data = surveysDynamicDataCallResponse.AsSections(),
                Total = surveysDynamicDataCallResponse.TotalHits ?? 0
            };

        }
    }
}
