using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class Contractor
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class ContractorRaw
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public static class ContractorRawExtensions
    {
        public static Contractor AsContractor(this ContractorRaw obj)
        {
            if (obj != null)
                return new Contractor
                {
                    Id = obj.Id,
                    Name = obj.Name
                };

            return null;
        }

        public static IEnumerable<Contractor> AsContractors(this IEnumerable<ContractorRaw> data)
        {
            return data?.Select(x => x.AsContractor());
        }
    }
}
