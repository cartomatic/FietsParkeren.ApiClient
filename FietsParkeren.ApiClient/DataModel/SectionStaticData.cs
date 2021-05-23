using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class SectionStaticData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        #region new v2 proprties
        public string[] SurveyIds { get; set; }

        public string[] AuthorityIds { get; set; }

        public string[] ContractorIds { get; set; }
        #endregion

        #region old v1 properties
        //in api v2 properties below are gone :(
        //public string Buurt { get; set; }

        //public decimal? Area { get; set; }

        //public string AuthorityId { get; set; }

        //public string ContractorId { get; set; }

        //public string SurveyId { get; set; }

        //public SectionStaticDataRelatedObject[] Authorities { get; set; }

        //public SectionStaticDataRelatedObject[] Contractors { get; set; }

        //public SectionStaticDataRelatedObject[] Surveys { get; set; }
        #endregion

        public SectionStaticDataGeoLocation GeoLocation { get; set; }
    }

    public class SectionStaticDataRaw
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        #region new v2 proprties
        [JsonProperty("surveyIds")]
        public string[] SurveyIds { get; set; }

        [JsonProperty("authorityIds")]
        public string[] AuthorityIds { get; set; }

        [JsonProperty("contractorIds")]
        public string[] ContractorIds { get; set; }
        #endregion

        #region old v1 properties
        //in api v2 properties below are gone :(
        //[JsonProperty("buurt")]
        //public string Buurt { get; set; }

        //[JsonProperty("area")]
        //public decimal? Area { get; set; }

        //[JsonProperty("authorityId")]
        //public string AuthorityId { get; set; }

        //[JsonProperty("contractorId")]
        //public string ContractorId { get; set; }

        //[JsonProperty("surveyId")]
        //public string SurveyId { get; set; }

        //[JsonProperty("authorities")]
        //public SectionStaticDataRelatedObjectRaw[] Authorities { get; set; }

        //[JsonProperty("contractors")]
        //public SectionStaticDataRelatedObjectRaw[] Contractors { get; set; }

        //[JsonProperty("surveys")]
        //public SectionStaticDataRelatedObjectRaw[] Surveys { get; set; }
        #endregion

        [JsonProperty("geoLocation")]
        public SectionStaticDataGeoLocationRaw GeoLocation { get; set; }
    }


    public class SectionStaticDataRawResponse : BaseResponse
    {
        [JsonProperty("result")]
        public SectionStaticDataRaw[] Sections { get; set; }
    }

    public static class SectionStaticDataRawResponseExtensions
    {
        public static IEnumerable<SectionStaticData> AsSections(this SectionStaticDataRawResponse obj)
        {
            return obj?.Sections.AsSections();
        }
    }

    public static class SectionStaticDataRawExtensions
    {
        public static SectionStaticData AsSection(this SectionStaticDataRaw obj)
        {
            if (obj != null)
                return new SectionStaticData
                {
                    Id = obj.Id,
                    Name = obj.Name,

                    #region new v2 proprties
                    SurveyIds = obj.SurveyIds,
                    AuthorityIds = obj.AuthorityIds,
                    ContractorIds = obj.ContractorIds,
                    #endregion

                    #region old v1 properties
                    //in api v2 properties below are gone :(
                    //Buurt = obj.Buurt,
                    //Area = obj.Area,
                    //AuthorityId = obj.AuthorityId,
                    //ContractorId = obj.ContractorId,
                    //SurveyId = obj.SurveyId,
                    //Authorities = obj.Authorities.AsSectionStaticDataRelatedObject(),
                    //Contractors = obj.Contractors.AsSectionStaticDataRelatedObject(),
                    //Surveys = obj.Surveys.AsSectionStaticDataRelatedObject(),
                    #endregion

                    GeoLocation = obj.GeoLocation.AsSectionStaticDataGeoLocation()
                };

            return null;
        }

        public static IEnumerable<SectionStaticData> AsSections(this IEnumerable<SectionStaticDataRaw> data)
        {
            return data.Select(x => x.AsSection());
        }

        public static IEnumerable<SectionStaticData> AsSections(this SectionStaticDataRaw data)
        {
            return data?.AsSections();
        }
    }

}
