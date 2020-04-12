using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Covid19Data.Domain.Entities
{
    public class DayData
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonPropertyName("lastUpdatedAtSource")]
        public DateTime LastUpdatedAtSource { get; set; }

        [JsonPropertyName("sourceUrl")]
        public string SourceUrl { get; set; }

        [JsonPropertyName("infected")]
        public int Infected { get; set; }

        [JsonPropertyName("deceased")]
        public int Deceased { get; set; }

        [JsonPropertyName("totalTested")]
        public int TotalTested { get; set; }

        [JsonPropertyName("testedNotInfected")]
        public int TotalNotInfected { get; set; }

        [JsonPropertyName("infectedByRegion")]
        public List<StateInfo> InfectedByRegion { get; set; }

        [JsonPropertyName("deceasedByRegion")]
        public List<StateInfo> DeceasedByRegion { get; set; }
    }
}
