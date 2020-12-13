using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class Survey
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Authority Authority { get; set; }

        public IEnumerable<Contractor> Contractors { get; set; }
    }

    public class SurveysRaw
    {
        public SurveyRaw[] Surveys { get; set; }
    }

    public class SurveysRawResponse : BaseResponse
    {
        [JsonProperty("surveys")]
        public SurveyRaw[] Surveys { get; set; }
    }

    public class SurveyRaw
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("startdate")]
        public DateTime? StartDate { get; set; }

        [JsonProperty("enddate")]
        public DateTime? EndDate { get; set; }

        [JsonProperty("authority")]
        public AuthorityRaw Authority { get; set; }

        [JsonProperty("contractors")]
        public ContractorRaw[] Contractors { get; set; }
    }

    public static class SurveyRawExtensions
    {
        public static Survey AsSurvey(this SurveyRaw obj)
        {
            if (obj != null)
                return new Survey
                {
                    Id = obj.Id,
                    Name = obj.Name,
                    StartDate = obj.StartDate,
                    EndDate = obj.EndDate,
                    Authority = obj.Authority.AsAuthority(),
                    Contractors = obj.Contractors.AsContractors()
                };

            return null;
        }

        public static IEnumerable<Survey> AsSurveys(this IEnumerable<SurveyRaw> data)
        {
            return data?.Select(x => x.AsSurvey());
        }

        public static IEnumerable<Survey> AsSurveys(this SurveysRaw data)
        {
            return data?.Surveys?.AsSurveys();
        }
    }
}
