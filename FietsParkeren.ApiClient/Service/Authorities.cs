using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FietsParkeren.ApiClient.DataModel;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FietsParkeren.ApiClient
{
    public partial class Service
    {
        /// <summary>
        /// Gets authorities
        /// </summary>
        /// <param name="user">auth user name</param>
        /// <param name="pass">auth user pass</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Authority>> GetAuthoritiesAsync(string user, string pass, string geoPolygon, string geoRelation)
        {
            return await GetAuthoritiesAsync(GetAuthorizationHeaderValue(user, pass), geoPolygon, geoRelation);
        }

        /// <summary>
        /// Gets authorities
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Authority>> GetAuthoritiesAsync(string authToken, string geoPolygon, string geoRelation)
        {
            return await GetAuthoritiesInternalAsync(GetAuthorizationHeaderValue(authToken), geoPolygon, geoRelation);
        }


        /// <summary>
        /// Gets authorities
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Authority>> GetAuthoritiesInternalAsync(string authHdr, string geoPolygon, string geoRelation)
        {
            var cfg = ServiceConfig.Read();

           

            var authoritiesCallResponses = await ApiCallWithCombinedPages<AuthorityRawResponse>(
                cfg.Endpoint,
                cfg.Routes.Authorities,
                authHdr,
                queryParams: PrepareGeoPolygonQuery(geoPolygon, geoRelation)
            );

            var outData = new List<Authority>();

            foreach (var resp in authoritiesCallResponses)
            {
                outData.AddRange(resp.Authorities?.AsAuthorities() ?? new Authority[0]);
            }

            return outData;
        }
    }
}
