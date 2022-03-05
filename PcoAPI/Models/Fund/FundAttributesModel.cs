using System;
using Newtonsoft.Json;

namespace PcoAPI.Models.Fund
{
    public class FundAttributesModel
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("ledger_code")]
        public string LedgerCode { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        [JsonProperty("default")]
        public bool PCODefault {get; set;}
        [JsonProperty("color")]
        public string Color { get; set; }
        [JsonProperty("deletable")]
        public bool Deletable { get; set; }
    }
}
