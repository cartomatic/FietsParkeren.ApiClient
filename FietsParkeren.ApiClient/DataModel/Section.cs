using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class Section
    {
        public string Id { get; set; }

        public DateTime? TimeStamp { get; set; }

        public string SurveyId { get; set; }

        public string ContractorId { get; set; }

        public int? ParkingCapacity { get; set; }

        public int? VacantSpaces { get; set; }

        public int? OccupiedSpaces { get; set; }

        public IEnumerable<Count> Counts { get; set; }
    }

    public class SectionsRawWrapper
    {
        [JsonProperty("sections")]
        public IEnumerable<SectionRaw> Sections { get; set; }

        [JsonProperty("totalHits")]
        public int? TotalHits { get; set; }

        [JsonProperty("queryTime")]
        public int? QueryTime { get; set; }

        [JsonProperty("resultsPerPage")]
        public int? ResultsPerPage { get; set; }

        [JsonProperty("pge")]
        public int? Page { get; set; }
    }


    public class SectionRaw
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("timestamp")]
        public DateTime? TimeStamp { get; set; }

        [JsonProperty("surveyId")]
        public string SurveyId { get; set; }

        [JsonProperty("contractorId")]
        public string ContractorId { get; set; }

        [JsonProperty("parkingCapacity")]
        public int? ParkingCapacity { get; set; }

        [JsonProperty("vacantSpaces")]
        public int? VacantSpaces { get; set; }

        [JsonProperty("occupiedSpaces")]
        public int? OccupiedSpaces { get; set; }

        [JsonProperty("count")]
        public IEnumerable<CountRaw> CountsRaw { get; set; }
    }

    public static class SectionRawExtensions
    {
        public static Section AsSection(this SectionRaw obj)
        {
            if (obj != null)
                return new Section
                {
                    Id = obj.Id,
                    TimeStamp = obj.TimeStamp,
                    SurveyId = obj.SurveyId,
                    ContractorId = obj.ContractorId,
                    ParkingCapacity = obj.ParkingCapacity,
                    VacantSpaces = obj.VacantSpaces,
                    OccupiedSpaces = obj.OccupiedSpaces,
                    Counts = obj.CountsRaw.AsCounts()
                };

            return null;
        }

        public static IEnumerable<Section> AsSections(this IEnumerable<SectionRaw> data)
        {
            return data.Select(x => x.AsSection());
        }

        public static IEnumerable<Section> AsSections(this SectionsRawWrapper data)
        {
            return data?.Sections?.AsSections();
        }
    }
}
