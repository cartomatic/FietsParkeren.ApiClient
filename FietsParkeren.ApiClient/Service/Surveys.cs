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
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Survey>> GetSurveysAsync(string user, string pass, string geoPolygon, string geoRelation)
        {
            return await GetSurveysAsync(GetAuthorizationHeaderValue(user, pass), geoPolygon, geoRelation);
        }

        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Survey>> GetSurveysAsync(string authToken, string geoPolygon, string geoRelation)
        {
            return await GetSurveyInternalsAsync(GetAuthorizationHeaderValue(authToken), geoPolygon, geoRelation);
        }

        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Survey>> GetSurveyInternalsAsync(string authHdr, string geoPolygon, string geoRelation)
        {
            var cfg = ServiceConfig.Read();
            
            var surveysCallResponses = await ApiCall<SurveysRawResponse>(
                cfg.Endpoint,
                cfg.Routes.Surveys,
                authHdr,
                queryParams: PrepareGeoPolygonQuery(geoPolygon, geoRelation)
            );

            var outData = new List<Survey>();

            foreach (var resp in surveysCallResponses)
            {
                outData.AddRange(resp.Surveys?.AsSurveys() ?? new Survey[0]);
            }

            return outData;
        }
    }
}
