using System;
using Newtonsoft.Json;
using PcoAPI.Interfaces;

namespace PcoAPI.Models.PaymentSource
{
    public class PaymentSourceModel: IRelationship, IDataObject<PaymentSourceAttributesModel>
    {
        [JsonProperty("type")]
        public string PCOType { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("attributes")]
        public PaymentSourceAttributesModel Attributes { get; set; }
    }
}
