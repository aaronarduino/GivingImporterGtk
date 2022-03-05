using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PcoAPI.Models.PaymentSource
{
    public class PaymentSourcesModel: RootModel
    {
        [JsonProperty("data")]
        public List<PaymentSourceModel> Data { get; set; }
    }
}
