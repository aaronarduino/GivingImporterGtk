using System;
using Newtonsoft.Json;

namespace PcoAPI.Models.Fund
{
    public class FundModel: IDataObject<FundAttributesModel>
    {
        [JsonProperty("type")]
        public string PCOType { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("attributes")]
        public FundAttributesModel Attributes { get; set; }
    }
}
