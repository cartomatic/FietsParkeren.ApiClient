using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class SurveyDynamicDataRaw
    {
        [JsonProperty("dynamicData")]
        public SectionsRawWrapper DynamicData { get; set; }

        [JsonProperty("sections")]
        public IEnumerable<SectionRaw> Sections { get; set; }

    }

    public static class SurveyDynamicDataRawExtensions
    {
        public static IEnumerable<Section> AsSection(this SurveyDynamicDataRaw obj)
        {
            return obj?.DynamicData?.AsSections()
                ?? obj?.Sections.AsSections();
        }
    }
}
