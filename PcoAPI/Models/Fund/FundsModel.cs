using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PcoAPI.Models.Fund
{
    public class FundsModel: RootModel
    {
        [JsonProperty("data")]
        public List<FundModel> Data { get; set; }
    }
}
