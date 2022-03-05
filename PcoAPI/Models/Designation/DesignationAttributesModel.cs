using System;
using Newtonsoft.Json;

namespace PcoAPI.Models.Designation
{
    public class DesignationAttributesModel
    {
        [JsonProperty("amount_cents")]
        public int AmountCents { get; set; }
        [JsonProperty("amount_currency")]
        public string AmountCurrency { get; set; }
    }
}
