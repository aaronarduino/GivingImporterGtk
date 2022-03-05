using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PcoAPI.Models.Batch
{
    public class BatchesModel: RootModel
    {
        [JsonProperty("data")]
        public List<BatchModel> Data { get; set; }
    }
}
