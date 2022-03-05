using System;
using Newtonsoft.Json;

namespace PcoAPI.Models.PaymentSource
{
    public class PaymentSourceAttributesModel
    {
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}