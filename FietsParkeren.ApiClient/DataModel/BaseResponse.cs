using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    /// <summary>
    /// Base object for all the api responses
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Total hits the query returns
        /// </summary>
        [JsonProperty("totalHits")]
        public int? TotalHits { get; set; }

        /// <summary>
        /// page size - max results count per page
        /// </summary>
        [JsonProperty("pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// page no
        /// </summary>
        [JsonProperty("page")]
        public int? Page { get; set; }
    }
}
