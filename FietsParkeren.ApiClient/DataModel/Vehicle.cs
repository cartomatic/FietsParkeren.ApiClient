using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class Vehicle
    {
        public string Type { get; set; }
    }

    public class VehicleRaw
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public static class VehicleRawExtensions
    {
        public static Vehicle AsVehicle(this VehicleRaw obj)
        {
            if (obj != null)
                return new Vehicle
                {
                    Type = obj.Type
                };

            return null;
        }
    }
}
