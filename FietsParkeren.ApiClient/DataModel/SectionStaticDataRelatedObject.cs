using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    #region old v1 objects
    //public class SectionStaticDataRelatedObject
    //{
    //    public DateTime? EndDate { get; set; }

    //    public DateTime? StartDate { get; set; }

    //    public string Id { get; set; }
    //}

    //public class SectionStaticDataRelatedObjectRaw
    //{
    //    [JsonProperty("endDate")]
    //    public DateTime? EndDate { get; set;}

    //    [JsonProperty("startDate")]
    //    public DateTime? StartDate { get; set; }

    //    [JsonProperty("id")]
    //    public string Id { get; set; }
    //}

    //public static class SectionStaticDataRelatedObjectRawExtensions
    //{
    //    public static SectionStaticDataRelatedObject AsSectionStaticDataRelatedObject(this SectionStaticDataRelatedObjectRaw obj)
    //    {
    //        return new SectionStaticDataRelatedObject
    //        {
    //            Id = obj.Id,
    //            StartDate = obj.StartDate,
    //            EndDate = obj.EndDate
    //        };
    //    }

    //    public static SectionStaticDataRelatedObject[] AsSectionStaticDataRelatedObject(this IEnumerable<SectionStaticDataRelatedObjectRaw> obj)
    //    {
    //        return obj.Select(x => new SectionStaticDataRelatedObject
    //        {
    //            Id = x.Id,
    //            StartDate = x.StartDate,
    //            EndDate = x.EndDate
    //        }).ToArray();
    //    }
    //}
    #endregion

}
