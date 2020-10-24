using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FietsParkeren.ApiClient.DataModel
{
    public class Authority
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }

    public class AuthorityRaw
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public static class AuthorityRawExtensions
    {
        public static Authority AsAuthority(this AuthorityRaw obj)
        {
            if (obj != null)
                return new Authority
                {
                    Id = obj.Id,
                    Name = obj.Name
                };

            return null;
        }

        public static IEnumerable<Authority> AsAuthorities(this IEnumerable<AuthorityRaw> data)
        {
            return data?.Select(x => x.AsAuthority());
        }
    }
}
