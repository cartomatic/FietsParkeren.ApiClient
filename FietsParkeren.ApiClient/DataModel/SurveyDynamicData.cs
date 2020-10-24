using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class SurveyDynamicDataRaw
    {
        [JsonProperty("sections")]
        public SectionsRawWrapper Sections { get; set; }
    }

    public static class SurveyDynamicDataRawExtensions
    {
        public static IEnumerable<Section> AsSection(this SurveyDynamicDataRaw obj)
        {
            return obj?.Sections?.AsSections();
        }
    }
}
