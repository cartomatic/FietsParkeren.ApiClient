using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace FietsParkeren.ApiClient.DataModel
{
    public class SurveyFilter
    {
        /// <summary>
        /// Only data from this research
        /// </summary>
        public string SurveyId { get; set; }

        /// <summary>
        /// Only data from this client
        /// </summary>
        public string AuthorityId { get; set; }

        /// <summary>
        /// Only data from this data supplier
        /// </summary>
        public string ContractorId { get; set; }

        /// <summary>
        /// Selection by timestamp. Section.timestamp> = startDate
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Selection by timestamp. Section.timestamp <= endDate
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// lat1, lng1, lat2, lng2, lat3, lng3, ..., ..., lat1, lng1
        /// </summary>
        public IEnumerable<double> GeoPolygon { get; set; }

        /// <summary>
        /// 'intersects' (default) or 'within'
        /// </summary>
        public string GeoRelation { get; set; }

        protected internal string GetGeoRelation()
        {
            if (!string.IsNullOrEmpty(GeoRelation) && new[] { "intersects", "within" }.Contains(GeoRelation.ToLower()))
                return GeoRelation.ToLower();

            return "intersects";
        }

        public Dictionary<string, object> ToQueryParams()
        {
            var queryParams = new Dictionary<string, object>();

            if(!string.IsNullOrEmpty(SurveyId))
                queryParams.Add(nameof(SurveyId), SurveyId);

            if (!string.IsNullOrEmpty(AuthorityId))
                queryParams.Add(nameof(AuthorityId), AuthorityId);

            if (!string.IsNullOrEmpty(ContractorId))
                queryParams.Add(nameof(ContractorId), ContractorId);

            if(StartDate.HasValue)
                queryParams.Add(nameof(StartDate), StartDate.Value.ToString("O"));

            if (EndDate.HasValue)
                queryParams.Add(nameof(EndDate), StartDate.Value.ToString("O"));

            if(GeoPolygon != null && GeoPolygon.Any() && GeoPolygon.Count() % 2 == 0)
            {
                queryParams.Add(nameof(GeoPolygon),
                    string.Join(",", GeoPolygon.Select(x => x.ToString(CultureInfo.InvariantCulture))));

                if (!string.IsNullOrEmpty(GeoRelation))
                    queryParams.Add(nameof(GeoRelation), GetGeoRelation());
            }
            
            return queryParams;
        }
    }
}
