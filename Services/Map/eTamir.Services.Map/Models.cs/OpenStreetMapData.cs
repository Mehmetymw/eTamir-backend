using Newtonsoft.Json;
using System.Collections.Generic;

namespace eTamir.Services.Map.Models
{
    public class OpenStreetMapData
    {
        [JsonProperty("address")]
        public UserAddress Address { get; set; }
    }

    [JsonObject("address")]
    public class UserAddress
    {
        [JsonProperty("suburb")]
        public string Suburb { get; set; }
        [JsonProperty("road")]
        public string Road { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }

    }
}
