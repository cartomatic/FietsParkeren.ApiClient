using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class Count
    {
        public int? NumberOfVehicles { get; set; }

        public Vehicle Vehicle { get; set; }
    }

    public class CountRaw
    {
        [JsonProperty("numberOfVehicles")]
        public int? NumberOfVehicles { get; set; }

        [JsonProperty("vehicle")]
        public VehicleRaw VehicleRaw { get; set; }
    }

    public static class CountRawExtensions
    {
        public static Count AsCount(this CountRaw obj)
        {
            if (obj != null)
                return new Count
                {
                    NumberOfVehicles = obj.NumberOfVehicles,
                    Vehicle = obj.VehicleRaw.AsVehicle()
                };

            return null;
        }

        public static IEnumerable<Count> AsCounts(this IEnumerable<CountRaw> data)
        {
            return data?.Select(x => x.AsCount());
        }
    }
}
