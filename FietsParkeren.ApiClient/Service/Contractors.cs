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
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Contractor>> GetContractorsAsync(string user, string pass, string geoPolygon, string geoRelation)
        {
            return await GetContractorsAsync(GetAuthorizationHeaderValue(user, pass), geoPolygon, geoRelation);
        }

        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authToken">credentials supplied as authorization token</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        public static async Task<IEnumerable<Contractor>> GetContractorsAsync(string authToken, string geoPolygon, string geoRelation)
        {
            return await GetContractorsInternalAsync(GetAuthorizationHeaderValue(authToken), geoPolygon, geoRelation);
        }


        /// <summary>
        /// Gets contractors
        /// </summary>
        /// <param name="authHdr">credentials supplied as authorization header value</param>
        /// <param name="geoPolygon">Polygon to spatially filter the data</param>
        /// <param name="geoRelation">Type of spatial relation to use when filtering data; defaults to 'intersects'</param>
        /// <returns></returns>
        protected internal static async Task<IEnumerable<Contractor>> GetContractorsInternalAsync(string authHdr, string geoPolygon, string geoRelation)
        {
            var cfg = ServiceConfig.Read();
            
            var contractorsCallResponses = await ApiCallWithCombinedPages<ContractorRawResponse>(
                cfg.Endpoint,
                cfg.Routes.Contractors,
                authHdr,
                queryParams: PrepareGeoPolygonQuery(geoPolygon, geoRelation)
            );


            var outData = new List<Contractor>();

            foreach (var resp in contractorsCallResponses)
            {
                outData.AddRange(resp.Contractors?.AsContractors() ?? new Contractor[0]);
            }

            return outData;
        }
    }
}
