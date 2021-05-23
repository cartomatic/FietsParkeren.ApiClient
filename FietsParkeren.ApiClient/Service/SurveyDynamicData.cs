using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Globalization;
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
        /// <param name="authorityId"></param>
        /// <param name="contractorId"></param>
        /// <param name="dateTo"></param>
        /// <param name="maxOccupation"></param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="dateFrom"></param>
        /// <returns></returns>
        public static async Task<PagedResult<IEnumerable<SectionDynamicData>>> GetSurveyDynamicDataAsync(string user, string pass, string surveyIds, string authorityId, string contractorId, string dateFrom,
            string dateTo, int? maxOccupation, string geoPolygon, string geoRelation, int? pageSize = null, int? page = null)
        {
            return await GetSurveyDynamicDataAsync(GetAuthorizationHeaderValue(user, pass), surveyIds, authorityId, contractorId, dateFrom, dateTo, maxOccupation, geoPolygon, geoRelation, pageSize, page);
        }

        /// <summary>
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="surveyIds">comma separated survey ids</param>
        /// <param name="contractorId"></param>
        /// <param name="dateTo"></param>
        /// <param name="maxOccupation"></param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="authorityId"></param>
        /// <param name="dateFrom"></param>
        /// <returns></returns>
        public static async Task<PagedResult<IEnumerable<SectionDynamicData>>> GetSurveyDynamicDataAsync(string authToken, string surveyIds, string authorityId, string contractorId, string dateFrom,
            string dateTo, int? maxOccupation, string geoPolygon, string geoRelation, int? pageSize = null, int? page = null)
        {
            return await GetSurveyDynamicDataInternalsAsync(GetAuthorizationHeaderValue(authToken), surveyIds, authorityId, contractorId, dateFrom, dateTo, maxOccupation,  geoPolygon, geoRelation, pageSize, page);
        }

        /// <summary>
        /// Gets survey dynamic data
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="surveyIds">comma separated survey ids</param>
        /// <param name="contractorId"></param>
        /// <param name="dateTo"></param>
        /// <param name="maxOccupation"></param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <param name="authorityId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        protected internal static async Task<PagedResult<IEnumerable<SectionDynamicData>>> GetSurveyDynamicDataInternalsAsync(string authHdr, string surveyIds, string authorityId, string contractorId, string dateFrom,
            string dateTo, int? maxOccupation, string geoPolygon, string geoRelation, int? pageSize = null, int? page = null)
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

            if(!string.IsNullOrWhiteSpace(authorityId))
                queryParams.Add("authorityId", authorityId);

            if (!string.IsNullOrWhiteSpace(contractorId))
                queryParams.Add("contractorId", contractorId);

            if (pageSize.HasValue)
                queryParams.Add("pageSize", pageSize.Value);

            if (page.HasValue)
                queryParams.Add("page", page.Value);

            if(!string.IsNullOrWhiteSpace(dateFrom) && DateTime.TryParseExact(dateFrom, "yyyy-MM-dd HH:mm", null, DateTimeStyles.None, out var dateFromParsed))
                queryParams.Add("startDate", dateFromParsed.ToUniversalTime().ToString("O")); //need iso date!

            if (!string.IsNullOrWhiteSpace(dateTo) && DateTime.TryParseExact(dateTo, "yyyy-MM-dd HH:mm", null, DateTimeStyles.None, out var dateToParsed))
                queryParams.Add("endDate", dateToParsed.ToUniversalTime().ToString("O"));//need iso date!
            
            

            if (maxOccupation.HasValue && maxOccupation > 0)
            {
                queryParams.Add("orderBy", "occupiedSpaces");
                queryParams.Add("groupBy", "staticSectionId");
                queryParams.Add("orderDirection", "DESC");

                queryParams["pageSize"] = maxOccupation.Value;
            }


            if (maxOccupation.HasValue && maxOccupation > 0)
            {
                var surveysDynamicDataCallResponse = await ApiCallSinglePage<SectionDynamicDataMaxOccupationRawResponse>(
                    cfg.Endpoint,
                    cfg.Routes.DynamicData,
                    authHdr,
                    queryParams: queryParams
                );

                var output = new List<SectionDynamicData>();

                foreach (var sectionDynamicDataRawResponse in surveysDynamicDataCallResponse.Result)
                {
                    output.AddRange(sectionDynamicDataRawResponse?.AsSections() ?? new SectionDynamicData[0]);
                }

                return new PagedResult<IEnumerable<SectionDynamicData>>
                {
                    Data = output,
                    Total = 0
                };

            }
            else {
                var surveysDynamicDataCallResponse = await ApiCallSinglePage<SectionDynamicDataRawResponse>(
                    cfg.Endpoint,
                    cfg.Routes.DynamicData,
                    authHdr,
                    queryParams: queryParams
                );



                return new PagedResult<IEnumerable<SectionDynamicData>>
                {
                    Data = surveysDynamicDataCallResponse?.AsSections(),
                    Total = surveysDynamicDataCallResponse?.TotalHits ?? 0
                };
            }

        }
    }
}
