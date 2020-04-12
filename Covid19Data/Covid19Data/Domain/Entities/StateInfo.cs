using System.Text.Json.Serialization;

namespace Covid19Data.Domain.Entities
{
    public class StateInfo
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }
        
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
