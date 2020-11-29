using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class SectionStaticDataGeoLocation
    {
        public string Type { get; set; }

        public object Coordinates { get; set; }
    }

    public class SectionStaticDataGeoLocationRaw
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("coordinates")]
        public object Coordinates { get; set; }
    }

    public static class SectionStaticDataGeoLocationRawExtensions
    {
        public static SectionStaticDataGeoLocation AsSectionStaticDataGeoLocation(this SectionStaticDataGeoLocationRaw obj)
        {
            return new SectionStaticDataGeoLocation
            {
                Type = obj.Type,
                Coordinates = obj.Coordinates
            };
        }
    }
}
