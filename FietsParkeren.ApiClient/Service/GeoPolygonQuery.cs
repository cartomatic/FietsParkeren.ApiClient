using System;
using System.Collections.Generic;
using System.Text;

namespace FietsParkeren.ApiClient
{
    public partial class Service
    {
        protected internal static Dictionary<string, object> PrepareGeoPolygonQuery(string geoPolygon, string geoRelation)
        {
            return PrepareGeoPolygonQuery(null, geoPolygon, geoRelation);
        }

        protected internal static Dictionary<string, object> PrepareGeoPolygonQuery(Dictionary<string, object> queryParams, string geoPolygon, string geoRelation)
        {
            if (!string.IsNullOrWhiteSpace(geoPolygon))
            {
                queryParams ??= new Dictionary<string, object>();

                queryParams.Add("geopolygon", geoPolygon);
                queryParams.Add("georelation", !string.IsNullOrWhiteSpace(geoRelation) ? geoRelation : "intersects");
            }

            return queryParams;
        }
    }
}
